using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.XmlRpc;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    /// <summary>
    /// Parent class for the exceptions of this library.
    /// </summary>
    public class RoslibException : Exception
    {
        protected RoslibException(string message) : base(message)
        {
        }

        protected RoslibException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Thrown when the provided message type is not correct.
    /// </summary>
    public class InvalidMessageTypeException : RoslibException
    {
        public InvalidMessageTypeException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>
    public class ConnectionException : RoslibException
    {
        public ConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Thrown when the uri provided for the caller (this node) is not reachable.
    /// </summary>
    public class UnreachableUriException : RoslibException
    {
        public UnreachableUriException(string message) : base(message)
        {
        }

        public UnreachableUriException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Wrapper around a <see cref="XmlRpcException"/>
    /// </summary>
    public class RosRpcException : RoslibException
    {
        public RosRpcException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Class that manages a client connection to a ROS master. 
    /// </summary>
    public sealed class RosClient : IDisposable
    {
        public const int AnyPort = 0;

        readonly NodeServer listener;

        readonly Dictionary<string, IRosSubscriber> subscribersByTopic = new Dictionary<string, IRosSubscriber>();
        readonly Dictionary<string, IRosPublisher> publishersByTopic = new Dictionary<string, IRosPublisher>();

        readonly Dictionary<string, ServiceReceiver> subscribedServicesByName =
            new Dictionary<string, ServiceReceiver>();

        readonly Dictionary<string, ServiceSenderManager> advertisedServicesByName =
            new Dictionary<string, ServiceSenderManager>();

        public delegate void ShutdownActionCall(
            string callerId, string reason,
            out int status, out string response);

        /// <summary>
        /// Handler of 'shutdown' XMLRPC calls from the slave API
        /// </summary>
        public ShutdownActionCall ShutdownAction { get; set; }

        public delegate void ParamUpdateActionCall(
            string callerId, string parameterKey, object parametervalue,
            out int status, out string response);

        /// <summary>
        /// Handler of 'paramUpdate' XMLRPC calls from the slave API
        /// </summary>
        public ParamUpdateActionCall ParamUpdateAction { get; set; }

        /// <summary>
        /// ID of this node.
        /// </summary>
        public string CallerId { get; }

        /// <summary>
        /// Wrapper for XML-RPC calls to the master.
        /// </summary>
        public Master Master { get; }

        /// <summary>
        /// Timeout in milliseconds for XML-RPC communications with the master.
        /// </summary>
        public TimeSpan RpcMasterTimeout
        {
            get => TimeSpan.FromMilliseconds(Master.TimeoutInMs);
            set
            {
                if (value.TotalMilliseconds <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                Master.TimeoutInMs = (int) value.TotalMilliseconds;
            }
        }

        TimeSpan rpcNodeTimeout = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Timeout in milliseconds for XML-RPC communications with another node.
        /// </summary>
        public TimeSpan RpcNodeTimeout
        {
            get => rpcNodeTimeout;
            set
            {
                if (value.TotalMilliseconds <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                rpcNodeTimeout = value;
            }
        }

        TimeSpan tcpRosTimeout = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Timeout in milliseconds for TCP-ROS communications (topics, services).
        /// </summary>
        public TimeSpan TcpRosTimeout
        {
            get => tcpRosTimeout;
            set
            {
                if (value.TotalMilliseconds <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                tcpRosTimeout = value;
                int valueInMs = (int) value.TotalMilliseconds;
                foreach (IRosSubscriber subscriber in subscribersByTopic.Values)
                {
                    subscriber.TimeoutInMs = valueInMs;
                }

                foreach (IRosPublisher publisher in publishersByTopic.Values)
                {
                    publisher.TimeoutInMs = valueInMs;
                }
            }
        }


        /// <summary>
        /// Wrapper for XML-RPC calls to the master.
        /// </summary>
        public ParameterClient Parameters { get; }

        /// <summary>
        /// URI of the master node.
        /// </summary>
        public Uri MasterUri => Master.MasterUri;

        /// <summary>
        /// URI of this node.
        /// </summary>
        public Uri CallerUri { get; }


        /// <summary>
        /// Constructs and connects a ROS client.
        /// </summary>
        /// <param name="masterUri">URI to the master node. Example: new Uri("http://localhost:11311")</param>
        /// <param name="callerId">The ROS name of this node</param>
        /// <param name="callerUri">URI of this node. Leave empty to generate one automatically</param>
        /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
        public RosClient(Uri masterUri = null, string callerId = null, Uri callerUri = null,
            bool ensureCleanSlate = true)
        {
            masterUri ??= EnvironmentMasterUri;

            if (masterUri is null) { throw new ArgumentException("No valid master uri provided", nameof(masterUri)); }

            if (masterUri.Scheme != "http")
            {
                throw new ArgumentException("URI scheme must be http", nameof(masterUri));
            }

            callerUri ??= TryGetCallerUri();

            if (callerUri.Scheme != "http")
            {
                throw new ArgumentException("URI scheme must be http", nameof(callerUri));
            }

            callerId ??= "/iviz_rosclient";

            if (!IsValidGlobalResourceName(callerId))
            {
                throw new ArgumentException($"Caller id '{callerId}' is not a valid global ROS resource name");
            }

            CallerId = callerId;
            CallerUri = callerUri;

            try
            {
                // Create an XmlRpc server. This will tell us fast whether the port is taken.
                listener = new NodeServer(this);
            }
            catch (SocketException e)
            {
                listener?.Dispose();
                throw new ConnectionException($"Failed to bind to local URI '{callerUri}'", e);
            }

            if (CallerUri.Port == AnyPort || CallerUri.IsDefaultPort)
            {
                string absolutePath = Uri.UnescapeDataString(callerUri.AbsolutePath);
                CallerUri = new Uri($"http://{CallerUri.Host}:{listener.ListenerUri.Port}{absolutePath}");
            }

            Master = new Master(masterUri, CallerId, CallerUri);
            Parameters = new ParameterClient(masterUri, CallerId, CallerUri);

            try
            {
                // Do a simple ping to the master. This will tell us whether the master is reachable.
                Master.GetUri();
            }
            catch (Exception e)
            {
                listener.Dispose();
                throw new ConnectionException($"Failed to contact the master URI '{masterUri}'", e);
            }

            // Start the XmlRpc server.
            listener.Start();

            NodeClient.GetPidResponse response;
            try
            {
                response = CreateTalker(CallerUri).GetPid();
            }
            catch (Exception e)
            {
                listener.Dispose();
                throw new UnreachableUriException($"My own uri '{CallerUri}' does not appear to be reachable!", e);
            }

            if (!response.IsValid)
            {
                Logger.LogError("RosClient: Failed to validate reachability response.");
            }
            else if (response.Pid != Process.GetCurrentProcess().Id)
            {
                listener.Dispose();
                throw new UnreachableUriException($"My uri '{CallerUri}' appears to belong to someone else!");
            }

            Logger.Log("RosClient: Initialized.");

            if (ensureCleanSlate)
            {
                EnsureCleanSlate();
            }
        }


        /// <summary>
        /// Constructs and connects a ROS client.
        /// </summary>
        /// <param name="masterUri">URI to the master node. Example: "http://localhost:11311"</param>
        /// <param name="callerId">The name of this node</param>
        /// <param name="callerUri">URI of this node. Leave empty to generate one automatically</param>
        public RosClient(string masterUri, string callerId = null, string callerUri = null) :
            this(masterUri is null ? EnvironmentMasterUri : new Uri(masterUri),
                callerId,
                callerUri is null ? EnvironmentCallerUri : new Uri(callerUri))
        {
        }

        /// <summary>
        /// Retrieves the environment variable ROS_HOSTNAME as a uri.
        /// If this fails, retrieves ROS_IP.
        /// </summary>
        public static Uri EnvironmentCallerUri
        {
            get
            {
                string envStr = Environment.GetEnvironmentVariable("ROS_HOSTNAME") ??
                                Environment.GetEnvironmentVariable("ROS_IP");

                if (envStr is null) { return null; }

                if (Uri.TryCreate(envStr, UriKind.Absolute, out Uri uri))
                {
                    return uri;
                }

                Logger.Log($"RosClient: Environment variable for caller uri '{envStr}' is not a valid uri!");
                return null;
            }
        }

        /// <summary>
        /// Try to retrieve a valid caller uri.
        /// </summary>
        public static Uri TryGetCallerUri(int usingPort = AnyPort)
        {
            return EnvironmentCallerUri ??
                   GetUriFromInterface(NetworkInterfaceType.Wireless80211, usingPort) ??
                   GetUriFromInterface(NetworkInterfaceType.Ethernet, usingPort) ??
                   new Uri($"http://{Dns.GetHostName()}:{usingPort}/");
        }

        static Uri GetUriFromInterface(NetworkInterfaceType type, int usingPort)
        {
            UnicastIPAddressInformation ipInfo =
                NetworkInterface.GetAllNetworkInterfaces()
                    .Where(@interface =>
                        @interface.NetworkInterfaceType == type && @interface.OperationalStatus == OperationalStatus.Up)
                    .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
                    .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
            // TODO: Consider ipv6 too

            return ipInfo is null ? null : new Uri($"http://{ipInfo.Address}:{usingPort}/");
        }

        /// <summary>
        /// Retrieves the environment variable ROS_MASTER_URI as a uri.
        /// </summary>
        public static Uri EnvironmentMasterUri
        {
            get
            {
                string envStr = Environment.GetEnvironmentVariable("ROS_MASTER_URI");
                if (envStr is null)
                {
                    return null;
                }

                if (Uri.TryCreate(envStr, UriKind.Absolute, out Uri uri))
                {
                    return uri;
                }

                Logger.Log($"RosClient: Environment variable for master uri '{envStr}' is not a valid uri!");
                return null;
            }
        }

        /// <summary>
        /// Checks if the given name is a valid ROS resource name
        /// </summary>  
        public static bool IsValidResourceName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            static bool IsAlpha(char c) => ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z');

            if (!IsAlpha(name[0]) && name[0] != '/' && name[0] != '~')
            {
                return false;
            }

            for (int i = 1; i < name.Length; i++)
            {
                if (!IsAlpha(name[i]) && !char.IsDigit(name[i]) && name[i] != '_' && name[i] != '/')
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the given name is a valid global ROS resource name
        /// </summary>         
        public static bool IsValidGlobalResourceName(string name)
        {
            return IsValidResourceName(name) && name[0] == '/';
        }

        /// <summary>
        /// Try to retrieve a valid master uri.
        /// </summary>        
        public static Uri TryGetMasterUri()
        {
            return EnvironmentMasterUri ?? new Uri("http://localhost:11311/");
        }

        /// <summary>
        /// Asks the master which topics we advertise and are subscribed to, and removes them.
        /// </summary>
        public void EnsureCleanSlate()
        {
            try
            {
                SystemState state = GetSystemState();
                foreach (TopicTuple tuple in state.Subscribers.Where(tuple => tuple.Members.Contains(CallerId)))
                {
                    Master.UnregisterSubscriber(tuple.Topic);
                }

                foreach (TopicTuple tuple in state.Publishers.Where(tuple => tuple.Members.Contains(CallerId)))
                {
                    Master.UnregisterPublisher(tuple.Topic);
                }
            }
            catch (Exception e)
            {
                throw new ConnectionException($"Failed to contact the master URI '{MasterUri}'", e);
            }
        }

        /// <summary>
        /// Asks the master which topics we advertise and are subscribed to, and removes them.
        /// If you are interested in the async version, make sure to set ensureCleanSlate to false in the constructor.
        /// </summary>
        public async Task EnsureCleanSlateAsync()
        {
            SystemState state = await GetSystemStateAsync();
            List<Task> tasks = new List<Task>();
            tasks.AddRange(
                state.Subscribers
                    .Where(tuple => tuple.Members.Contains(CallerId))
                    .Select(tuple => (Task) Master.UnregisterSubscriberAsync(tuple.Topic))
            );

            tasks.AddRange(
                state.Publishers
                    .Where(tuple => tuple.Members.Contains(CallerId))
                    .Select(tuple => Master.UnregisterPublisherAsync(tuple.Topic))
            );

            try
            {
                await Task.WhenAll(tasks).Caf();
            }
            catch (Exception e)
            {
                throw new ConnectionException($"Failed to contact the master URI '{MasterUri}'", e);
            }
        }

        internal NodeClient CreateTalker(Uri otherUri)
        {
            return new NodeClient(CallerId, CallerUri, otherUri, (int) RpcNodeTimeout.TotalMilliseconds);
        }

        RosSubscriber<T> CreateSubscriber<T>(string topic, bool requestNoDelay)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (!IsValidResourceName(topic))
            {
                throw new ArgumentException($"'{topic}' is not a valid resource name");
            }

            TopicInfo<T> topicInfo = new TopicInfo<T>(CallerId, topic, new T());
            int timeoutInMs = (int) TcpRosTimeout.TotalMilliseconds;
            TcpReceiverManager<T> manager = new TcpReceiverManager<T>(this, topicInfo, requestNoDelay)
                {TimeoutInMs = timeoutInMs};
            RosSubscriber<T> subscription = new RosSubscriber<T>(this, manager);

            subscribersByTopic[topic] = subscription;

            var masterResponse = Master.RegisterSubscriber(topic, topicInfo.Type);
            if (!masterResponse.IsValid)
            {
                subscribersByTopic.Remove(topic);
                throw new RosRpcException(
                    $"Error registering publisher for topic {topic}: {masterResponse.StatusMessage}");
            }

            Task.Run(async () => await manager.PublisherUpdateRpcAsync(masterResponse.Publishers));

            return subscription;
        }

        async Task<RosSubscriber<T>> CreateSubscriberAsync<T>(string topic, bool requestNoDelay)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (!IsValidResourceName(topic))
            {
                throw new ArgumentException($"'{topic}' is not a valid resource name");
            }

            TopicInfo<T> topicInfo = new TopicInfo<T>(CallerId, topic, new T());
            int timeoutInMs = (int) TcpRosTimeout.TotalMilliseconds;
            TcpReceiverManager<T> manager = new TcpReceiverManager<T>(this, topicInfo, requestNoDelay)
                {TimeoutInMs = timeoutInMs};
            RosSubscriber<T> subscription = new RosSubscriber<T>(this, manager);

            subscribersByTopic[topic] = subscription;

            var masterResponse = await Master.RegisterSubscriberAsync(topic, topicInfo.Type).Caf();
            if (!masterResponse.IsValid)
            {
                subscribersByTopic.Remove(topic);
                throw new RosRpcException(
                    $"Error registering publisher for topic {topic}: {masterResponse.StatusMessage}");
            }

            await manager.PublisherUpdateRpcAsync(masterResponse.Publishers).Caf();

            return subscription;
        }

        /// <summary>
        /// Subscribes to the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <returns>A token that can be used to unsubscribe from this topic.</returns>
        public string Subscribe<T>(string topic, Action<T> callback, bool requestNoDelay = false)
            where T : IMessage, IDeserializable<T>, new()
        {
            return Subscribe(topic, callback, out RosSubscriber<T> _, requestNoDelay);
        }

        /// <summary>
        /// Subscribes to the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="subscriber">
        /// The shared subscriber for this topic, used by all subscribers from this client.
        /// </param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <returns>A token that can be used to unsubscribe from this topic.</returns>
        public string Subscribe<T>(string topic, Action<T> callback, out RosSubscriber<T> subscriber,
            bool requestNoDelay = false)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (!TryGetSubscriber(topic, out IRosSubscriber baseSubscriber))
            {
                subscriber = CreateSubscriber<T>(topic, requestNoDelay);
            }
            else
            {
                subscriber = baseSubscriber as RosSubscriber<T>;
                if (subscriber == null)
                {
                    throw new InvalidMessageTypeException(
                        $"Existing subscriber message type {baseSubscriber.TopicType} does not match the given type.");
                }
            }

            return subscriber.Subscribe(callback);
        }

        /// <summary>
        /// Generic version of the subscriber function if the type is not known. You should probably use the templated versions.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="type">The C# message type.</param>
        /// <param name="subscriber">
        /// The shared subscriber for this topic, used by all subscribers from this client.
        /// </param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <returns>A token that can be used to unsubscribe from this topic.</returns>
        public string Subscribe(string topic, Action<IMessage> callback, Type type, out IRosSubscriber subscriber,
            bool requestNoDelay = false)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!TryGetSubscriber(topic, out IRosSubscriber baseSubscriber))
            {
                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                MethodInfo baseMethod = GetType().GetMethod(nameof(CreateSubscriber), flags);
                MethodInfo method = baseMethod?.MakeGenericMethod(type);
                if (method == null)
                {
                    throw new InvalidMessageTypeException("Type is not a message object");
                }

                object subscriberObj = method.Invoke(this, flags, null,
                    new object[] {topic, requestNoDelay}, BuiltIns.Culture);

                subscriber = (IRosSubscriber) subscriberObj;
            }
            else
            {
                if (!baseSubscriber.MessageTypeMatches(type))
                {
                    throw new InvalidMessageTypeException(
                        $"Existing subscriber message type {baseSubscriber.TopicType} does not match the given type.");
                }

                subscriber = baseSubscriber;
            }

            return subscriber.Subscribe(callback);
        }

        /// <summary>
        /// Subscribes to the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <returns>A pair containing a token that can be used to unsubscribe from this topic, and the subscriber object.</returns>
        public async Task<(string id, RosSubscriber<T> subscriber)>
            SubscribeAsync<T>(string topic, Action<T> callback, bool requestNoDelay = false)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            RosSubscriber<T> subscriber;
            if (!TryGetSubscriber(topic, out IRosSubscriber baseSubscriber))
            {
                subscriber = await CreateSubscriberAsync<T>(topic, requestNoDelay).Caf();
            }
            else
            {
                subscriber = baseSubscriber as RosSubscriber<T>;
                if (subscriber == null)
                {
                    throw new InvalidMessageTypeException(
                        $"Existing subscriber message type {baseSubscriber.TopicType} does not match the given type.");
                }
            }


            return (subscriber.Subscribe(callback), subscriber);
        }

        /// <summary>
        /// Unsubscribe from the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Subscribe().</param>
        /// <returns>Whether the unsubscription succeeded.</returns>
        public bool Unsubscribe(string topicId)
        {
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            var subscriber = subscribersByTopic.Values.FirstOrDefault(s => s.ContainsId(topicId));
            return subscriber != null && subscriber.Unsubscribe(topicId);
        }

        /// <summary>
        /// Unsubscribe from the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Subscribe().</param>
        /// <returns>Whether the unsubscription succeeded.</returns>
        public async Task<bool> UnsubscribeAsync(string topicId)
        {
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            var subscriber = subscribersByTopic.Values.FirstOrDefault(s => s.ContainsId(topicId));
            return subscriber != null && await subscriber.UnsubscribeAsync(topicId);
        }

        internal void RemoveSubscriber(IRosSubscriber subscriber)
        {
            subscribersByTopic.Remove(subscriber.Topic);
            Master.UnregisterSubscriber(subscriber.Topic);
        }

        internal async Task RemoveSubscriberAsync(IRosSubscriber subscriber)
        {
            subscribersByTopic.Remove(subscriber.Topic);
            await Master.UnregisterSubscriberAsync(subscriber.Topic);
        }

        /// <summary>
        /// Tries to retrieve the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="subscriber">Subscriber for the given topic.</param>
        /// <returns>Whether the subscriber was found.</returns>
        public bool TryGetSubscriber(string topic, out IRosSubscriber subscriber)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            return subscribersByTopic.TryGetValue(topic, out subscriber);
        }

        /// <summary>
        /// Retrieves the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <returns></returns>
        public IRosSubscriber GetSubscriber(string topic)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (TryGetSubscriber(topic, out IRosSubscriber subscriber))
            {
                return subscriber;
            }

            throw new KeyNotFoundException($"Cannot find subscriber for topic '{topic}'");
        }

        RosPublisher<T> CreatePublisher<T>(string topic) where T : IMessage
        {
            if (!IsValidResourceName(topic))
            {
                throw new ArgumentException($"'{topic}' is not a valid resource name");
            }

            TopicInfo<T> topicInfo = new TopicInfo<T>(CallerId, topic);
            TcpSenderManager<T> manager = new TcpSenderManager<T>(topicInfo)
                {TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds};
            RosPublisher<T> publisher = new RosPublisher<T>(this, manager);

            publishersByTopic[topic] = publisher;

            var response = Master.RegisterPublisher(topic, topicInfo.Type);
            if (response.IsValid)
            {
                return publisher;
            }

            publishersByTopic.Remove(topic);
            throw new ArgumentException($"Error registering publisher: {response.StatusMessage}", nameof(topic));
        }

        async Task<RosPublisher<T>> CreatePublisherAsync<T>(string topic) where T : IMessage
        {
            if (!IsValidResourceName(topic))
            {
                throw new ArgumentException($"'{topic}' is not a valid resource name");
            }

            TopicInfo<T> topicInfo = new TopicInfo<T>(CallerId, topic);
            TcpSenderManager<T> manager = new TcpSenderManager<T>(topicInfo)
                {TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds};
            RosPublisher<T> publisher = new RosPublisher<T>(this, manager);

            publishersByTopic[topic] = publisher;

            var response = await Master.RegisterPublisherAsync(topic, topicInfo.Type).Caf();
            if (response.IsValid)
            {
                return publisher;
            }

            publishersByTopic.Remove(topic);
            throw new ArgumentException($"Error registering publisher: {response.StatusMessage}", nameof(topic));
        }

        /// <summary>
        /// Advertises the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="publisher">
        /// The shared publisher for this topic, used by all advertisers from this client.
        /// Use this structure to publish messages to your topic.
        /// </param>
        /// <returns>A token that can be used to unadvertise from this publisher.</returns>
        public string Advertise<T>(string topic, out RosPublisher<T> publisher) where T : IMessage
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (!TryGetPublisher(topic, out IRosPublisher basePublisher))
            {
                publisher = CreatePublisher<T>(topic);
            }
            else
            {
                publisher = basePublisher as RosPublisher<T>;
                if (publisher == null)
                {
                    throw new InvalidMessageTypeException(
                        $"Existing subscriber message type {basePublisher.TopicType} does not match the given type.");
                }
            }

            return publisher.Advertise();
        }

        /// <summary>
        /// Advertises the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <returns>A pair containing a token that can be used to unadvertise from this publisher, and the publisher object.</returns>
        public async Task<(string id, RosPublisher<T> publisher)> AdvertiseAsync<T>(string topic) where T : IMessage
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            RosPublisher<T> publisher;
            if (!TryGetPublisher(topic, out IRosPublisher basePublisher))
            {
                publisher = await CreatePublisherAsync<T>(topic).Caf();
            }
            else
            {
                publisher = basePublisher as RosPublisher<T>;
                if (publisher == null)
                {
                    throw new InvalidMessageTypeException(
                        $"Existing subscriber message type {basePublisher.TopicType} does not match the given type.");
                }
            }

            return (publisher.Advertise(), publisher);
        }

        public string Advertise(string topic, Type type, out IRosPublisher publisher)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!TryGetPublisher(topic, out IRosPublisher basePublisher))
            {
                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                MethodInfo baseMethod = GetType().GetMethod(nameof(CreatePublisher), flags);
                MethodInfo method = baseMethod?.MakeGenericMethod(type);
                if (method == null)
                {
                    throw new InvalidMessageTypeException($"Type {type} is not a message object");
                }

                object publisherObj = method.Invoke(this, flags, null, new object[] {topic}, BuiltIns.Culture);
                publisher = (IRosPublisher) publisherObj;
            }
            else
            {
                if (!basePublisher.MessageTypeMatches(type))
                {
                    throw new InvalidMessageTypeException(
                        $"Type {type} does not match existing publisher type {basePublisher.TopicType}.");
                }

                publisher = basePublisher;
            }

            return publisher.Advertise();
        }


        /// <summary>
        /// Unadvertise the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Advertise().</param>
        /// <returns>Whether the unadvertisement succeeded.</returns>
        public bool Unadvertise(string topicId)
        {
            if (topicId is null) { throw new ArgumentNullException(nameof(topicId)); }

            IRosPublisher publisher = publishersByTopic.Values.FirstOrDefault(p => p.ContainsId(topicId));

            return publisher != null && publisher.Unadvertise(topicId);
        }

        /// <summary>
        /// Unadvertise the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Advertise().</param>
        /// <returns>Whether the unadvertisement succeeded.</returns>
        public async Task<bool> UnadvertiseAsync(string topicId)
        {
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            IRosPublisher publisher = publishersByTopic.Values.FirstOrDefault(p => p.ContainsId(topicId));

            return publisher != null && await publisher.UnadvertiseAsync(topicId);
        }

        internal void RemovePublisher(IRosPublisher publisher)
        {
            publishersByTopic.Remove(publisher.Topic);
            Master.UnregisterPublisher(publisher.Topic);
        }

        internal async Task RemovePublisherAsync(IRosPublisher publisher)
        {
            publishersByTopic.Remove(publisher.Topic);
            await Master.UnregisterPublisherAsync(publisher.Topic);
        }

        /// <summary>
        /// Tries to retrieve the publisher of the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="publisher">Publisher of the given topic.</param>
        /// <returns>Whether the publisher was found.</returns>
        public bool TryGetPublisher(string topic, out IRosPublisher publisher)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            return publishersByTopic.TryGetValue(topic, out publisher);
        }

        /// <summary>
        /// Retrieves the publisher of the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <returns></returns>
        public IRosPublisher GetPublisher(string topic)
        {
            if (TryGetPublisher(topic, out IRosPublisher publisher))
            {
                return publisher;
            }

            throw new KeyNotFoundException($"Cannot find publisher for topic '{topic}'");
        }

        /// <summary>
        /// Asks the master for all the published nodes in the system.
        /// Corresponds to the function 'getPublishedTopics' in the ROS Master API.
        /// </summary>
        /// <returns>List of topic names and message types.</returns>
        public ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics()
        {
            var response = Master.GetPublishedTopics();
            if (response.IsValid)
            {
                return response.Topics
                    .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
                    .ToArray().AsReadOnly();
            }

            throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
        }

        /// <summary>
        /// Asks the master for all the published nodes in the system.
        /// Corresponds to the function 'getPublishedTopics' in the ROS Master API.
        /// </summary>
        /// <returns>List of topic names and message types.</returns>
        public async Task<ReadOnlyCollection<BriefTopicInfo>> GetSystemPublishedTopicsAsync()
        {
            var response = await Master.GetPublishedTopicsAsync();
            if (response.IsValid)
            {
                return response.Topics
                    .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
                    .ToArray().AsReadOnly();
            }

            throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
        }

        /// <summary>
        /// Gets the topics published by this node.
        /// </summary>
        public ReadOnlyCollection<BriefTopicInfo> SubscribedTopics => GetSubscriptionsRcp().AsReadOnly();


        /// <summary>
        /// Asks the master for the nodes and topics in the system.
        /// Corresponds to the function 'getSystemState' in the ROS Master API.
        /// </summary>
        /// <returns>List of advertised topics, subscribed topics, and offered services, together with the involved nodes.</returns>
        public SystemState GetSystemState()
        {
            var response = Master.GetSystemState();
            if (response.IsValid)
            {
                return new SystemState(response.Publishers, response.Subscribers, response.Services);
            }

            throw new RosRpcException($"Failed to retrieve system state: {response.StatusMessage}");
        }

        /// <summary>
        /// Asks the master for the nodes and topics in the system.
        /// Corresponds to the function 'getSystemState' in the ROS Master API.
        /// </summary>
        /// <returns>List of advertised topics, subscribed topics, and offered services, together with the involved nodes.</returns>
        public async Task<SystemState> GetSystemStateAsync()
        {
            var response = await Master.GetSystemStateAsync().Caf();
            if (response.IsValid)
            {
                return new SystemState(response.Publishers, response.Subscribers, response.Services);
            }

            throw new RosRpcException($"Failed to retrieve system state: {response.StatusMessage}");
        }

        /// <summary>
        /// Gets the topics published by this node.
        /// </summary>
        public ReadOnlyCollection<BriefTopicInfo> PublishedTopics => GetPublicationsRcp().AsReadOnly();


        internal BriefTopicInfo[] GetSubscriptionsRcp()
        {
            return subscribersByTopic.Values
                .Select(subscriber => new BriefTopicInfo(subscriber.Topic, subscriber.TopicType))
                .ToArray();
        }


        internal BriefTopicInfo[] GetPublicationsRcp()
        {
            return publishersByTopic.Values
                .Select(publisher => new BriefTopicInfo(publisher.Topic, publisher.TopicType))
                .ToArray();
        }

        internal async Task PublisherUpdateRcpAsync(string topic, IEnumerable<Uri> publishers)
        {
            if (!TryGetSubscriber(topic, out IRosSubscriber subscriber))
            {
                Logger.Log($"{this}: PublisherUpdate called for nonexisting topic '{topic}'");
                return;
            }

            try
            {
                await subscriber.PublisherUpdateRcpAsync(publishers).Caf();
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: PublisherUpdateRcp failed: {e}");
            }
        }

        internal Endpoint RequestTopicRpc(string remoteCallerId, string topic)
        {
            if (!TryGetPublisher(topic, out IRosPublisher publisher))
            {
                Logger.Log($"{this}: '{remoteCallerId} is requesting topic '{topic}' but we don't publish it");
                return null;
            }

            try
            {
                return publisher.RequestTopicRpc(remoteCallerId);
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: RequestTopicRpc failed: {e}");
                return null;
            }
        }

        /// <summary>
        /// Close this connection. Unsubscribes and unadvertises all topics.
        /// </summary>
        public void Close()
        {
            listener.Dispose();

            IRosPublisher[] publishers = publishersByTopic.Values.ToArray();
            publishersByTopic.Clear();

            foreach (IRosPublisher publisher in publishers)
            {
                try
                {
                    publisher.Stop();
                    Master.UnregisterPublisher(publisher.Topic);
                }
                catch (Exception e)
                {
                    Logger.LogDebug($"Error unregistering publisher {publisher}: {e}");
                }
            }

            IRosSubscriber[] subscribers = subscribersByTopic.Values.ToArray();
            subscribersByTopic.Clear();

            foreach (IRosSubscriber subscriber in subscribers)
            {
                try
                {
                    subscriber.Stop();
                    Master.UnregisterSubscriber(subscriber.Topic);
                }
                catch (Exception e)
                {
                    Logger.LogDebug($"Error unregistering subscriber {subscriber}: {e}");
                }
            }

            ServiceReceiver[] receivers = subscribedServicesByName.Values.ToArray();
            subscribedServicesByName.Clear();

            foreach (ServiceReceiver receiver in receivers)
            {
                receiver.Stop();
            }

            ServiceSenderManager[] serviceManagers = advertisedServicesByName.Values.ToArray();
            advertisedServicesByName.Clear();

            foreach (ServiceSenderManager serviceSender in serviceManagers)
            {
                try
                {
                    serviceSender.Stop();
                    Master.UnregisterService(serviceSender.Service, serviceSender.Uri);
                }
                catch (Exception e)
                {
                    Logger.LogDebug($"Error unregistering subscriber {serviceSender}: {e}");
                }
            }
        }

        /// <summary>
        /// Close this connection. Unsubscribes and unadvertises all topics.
        /// </summary>
        public async Task CloseAsync()
        {
            listener.Dispose();

            var publishers = publishersByTopic.Values.ToArray();
            publishersByTopic.Clear();

            List<Task> tasks = new List<Task>();
            tasks.AddRange(publishers.Select(async publisher =>
            {
                publisher.Stop();
                await Master.UnregisterPublisherAsync(publisher.Topic);
            }));

            var subscribers = subscribersByTopic.Values.ToArray();
            subscribersByTopic.Clear();

            tasks.AddRange(subscribers.Select(async subscriber =>
            {
                subscriber.Stop();
                await Master.UnregisterSubscriberAsync(subscriber.Topic);
            }));

            ServiceReceiver[] receivers = subscribedServicesByName.Values.ToArray();
            subscribedServicesByName.Clear();

            foreach (ServiceReceiver receiver in receivers)
            {
                receiver.Stop();
            }

            ServiceSenderManager[] serviceManagers = advertisedServicesByName.Values.ToArray();
            advertisedServicesByName.Clear();

            tasks.AddRange(serviceManagers.Select(async serviceSender =>
            {
                await serviceSender.StopAsync();
                await Master.UnregisterServiceAsync(serviceSender.Service, serviceSender.Uri);                
            }));

            await Task.WhenAll(tasks).Caf();
        }

        public SubscriberState GetSubscriberStatistics()
        {
            return new SubscriberState(subscribersByTopic.Values.Select(subscriber => subscriber.GetState()).ToArray());
        }

        public PublisherState GetPublisherStatistics()
        {
            return new PublisherState(publishersByTopic.Values.Select(publisher => publisher.GetState()).ToArray());
        }

        internal IEnumerable<BusInfo> GetBusInfoRcp()
        {
            List<BusInfo> busInfos = new List<BusInfo>();

            SubscriberState state = GetSubscriberStatistics();
            foreach (var topic in state.Topics)
            {
                foreach (var receiver in topic.Receivers)
                {
                    busInfos.Add(new BusInfo(busInfos.Count, topic.Topic, receiver));
                }
            }

            try
            {
                PublisherState pstate = GetPublisherStatistics();
                foreach (var topic in pstate.Topics)
                {
                    foreach (var sender in topic.Senders)
                    {
                        LookupNodeResponse response;
                        try
                        {
                            response = Master.LookupNode(sender.RemoteId);
                        }
                        catch (Exception e)
                        {
                            Logger.Log($"{this}: LookupNode for {sender.RemoteId} failed: {e}");
                            continue;
                        }

                        if (!response.IsValid)
                        {
                            continue;
                        }

                        BusInfo busInfo = new BusInfo(busInfos.Count, response.Uri, "o", topic.Topic, sender.IsAlive);
                        busInfos.Add(busInfo);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: GetBusInfoRcp failed: {e}");
            }

            return busInfos;
        }

        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <param name="persistent">Whether a persistent connection with the provider should be maintained.</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <returns>Whether the call succeeded.</returns>
        public bool CallService<T>(string serviceName, T service, bool persistent = false) where T : IService
        {
            if (subscribedServicesByName.TryGetValue(serviceName, out var oldServiceReceiver))
            {
                // is there a persistent connection? use it
                if (oldServiceReceiver.IsAlive)
                {
                    return oldServiceReceiver.Execute(service);
                }

                oldServiceReceiver.Stop();
                subscribedServicesByName.Remove(serviceName);
            }
            
            // otherwise, create a new transient one
            LookupServiceResponse response = Master.LookupService(serviceName);
            if (!response.IsValid)
            {
                throw new RosRpcException($"Failed to call service: {response.StatusMessage}");
            }

            Uri serviceUri = response.ServiceUrl;
            ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T));
            try
            {
                using var serviceReceiver = new ServiceReceiver(serviceInfo, serviceUri, true, persistent);
                
                serviceReceiver.Start();
                bool result = serviceReceiver.Execute(service);

                if (persistent && serviceReceiver.IsAlive)
                {
                    subscribedServicesByName.Add(serviceName, serviceReceiver);
                }

                return result;
            }
            catch (Exception e)
            {
                throw new TimeoutException($"Service uri '{serviceUri}' is not reachable", e);
            }
        }

        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <returns>Whether the call succeeded.</returns>
        public async Task<bool> CallServiceAsync<T>(string serviceName, T service) where T : IService
        {
            LookupServiceResponse response = await Master.LookupServiceAsync(serviceName);
            if (!response.IsValid)
            {
                throw new RosRpcException($"Failed to call service: {response.StatusMessage}");
            }

            Uri serviceUri = response.ServiceUrl;
            ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T));
            try
            {
                using ServiceReceiverAsync serviceReceiver =
                    new ServiceReceiverAsync(serviceInfo, serviceUri, true, false);
                await serviceReceiver.StartAsync();
                return await serviceReceiver.ExecuteAsync(service);
            }
            catch (Exception e)
            {
                throw new TimeoutException($"Service uri '{serviceUri}' is not reachable", e);
            }
        }


        /// <summary>
        /// Advertises the given service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service.</param>
        /// <param name="callback">Function to be called when a service request arrives. The response should be written in the response field.</param>
        /// <typeparam name="T">Service type.</typeparam>
        public void AdvertiseService<T>(string serviceName, Action<T> callback) where T : IService, new()
        {
            if (advertisedServicesByName.ContainsKey(serviceName))
            {
                throw new ArgumentException("Service already exists", nameof(serviceName));
            }

            async Task Wrapper(IService x)
            {
                callback((T) x);
                await Task.CompletedTask;
            }

            ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T), new T());
            var advertisedService = new ServiceSenderManager(serviceInfo, CallerUri.Host, Wrapper);

            advertisedServicesByName.Add(serviceName, advertisedService);

            Master.RegisterService(serviceName, advertisedService.Uri);
        }

        /// <summary>
        /// Advertises the given service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service.</param>
        /// <param name="callback">Function to be called when a service request arrives. The response should be written in the response field.</param>
        /// <typeparam name="T">Service type.</typeparam>
        public async Task AdvertiseServiceAsync<T>(string serviceName, Func<T, Task> callback) where T : IService, new()
        {
            if (advertisedServicesByName.ContainsKey(serviceName))
            {
                throw new ArgumentException("Service already exists", nameof(serviceName));
            }

            // local lambda wrapper for casting
            async Task Wrapper(IService x)
            {
                await callback((T) x);
            }

            ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T), new T());
            var advertisedService = new ServiceSenderManager(serviceInfo, CallerUri.Host, Wrapper);

            advertisedServicesByName.Add(serviceName, advertisedService);

            await Master.RegisterServiceAsync(serviceName, advertisedService.Uri);
        }

        public void UnadvertiseService(string name)
        {
            if (!advertisedServicesByName.TryGetValue(name, out var advertisedService))
            {
                throw new ArgumentException("Service does not exist", nameof(name));
            }

            advertisedServicesByName.Remove(name);

            advertisedService.Stop();

            Master.UnregisterService(name, advertisedService.Uri);
        }

        public void Dispose()
        {
            Close();
        }

        public override string ToString()
        {
            return $"[RosClient MyUri='{CallerUri}' MyId='{CallerId}' MasterUri='{MasterUri}']";
        }
    }
}