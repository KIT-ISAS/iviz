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
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.XmlRpc;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    /// <summary>
    /// The provided message type is not correct.
    /// </summary>
    public class InvalidMessageTypeException : Exception
    {
        public InvalidMessageTypeException(string message) : base(message)
        {
        }

        public InvalidMessageTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidMessageTypeException()
        {
        }
    }

    /// <summary>
    /// An error happened during the connection.
    /// </summary>
    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message)
        {
        }

        public ConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConnectionException()
        {
        }
    }

    /// <summary>
    /// The uri provided for the caller (this node) is not reachable.
    /// </summary>
    public class UnreachableUriException : Exception
    {
        public UnreachableUriException(string message) : base(message)
        {
        }

        public UnreachableUriException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnreachableUriException()
        {
        }
    }

    public class XmlRpcException : Exception
    {
        public XmlRpcException(string message) : base(message)
        {
        }

        public XmlRpcException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public XmlRpcException()
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

        readonly ConcurrentDictionary<string, RosSubscriber> subscribersByTopic =
            new ConcurrentDictionary<string, RosSubscriber>();

        readonly ConcurrentDictionary<string, RosPublisher> publishersByTopic =
            new ConcurrentDictionary<string, RosPublisher>();

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
                subscribersByTopic.Values.ForEach(subscriber => subscriber.TimeoutInMs = valueInMs);
                publishersByTopic.Values.ForEach(publisher => publisher.TimeoutInMs = valueInMs);
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
        public RosClient(Uri masterUri = null, string callerId = null, Uri callerUri = null)
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
                throw new ArgumentException("Caller id is not a valid global ROS resource name");
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
            catch (Exception e) when
                (e is SocketException || e is TimeoutException || e is AggregateException || e is IOException)
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
            catch (Exception e) when
                (e is SocketException || e is TimeoutException || e is AggregateException || e is IOException)
            {
                listener.Dispose();
                throw new UnreachableUriException($"My own uri '{CallerUri}' does not appear to be reachable!");
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

                Logger.Log("RosClient: Environment variable for caller uri is not a valid uri!");
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
                    .Where(iface =>
                        iface.NetworkInterfaceType == type && iface.OperationalStatus == OperationalStatus.Up)
                    .SelectMany(iface => iface.GetIPProperties().UnicastAddresses)
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

                Logger.Log("RosClient: Environment variable for master uri is not a valid uri!");
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

            if (!IsAlpha(name[0]) && name[0] != '/' && name[0] != '~') { return false; }

            for (int i = 1; i < name.Length; i++)
            {
                if (!IsAlpha(name[i]) && !char.IsDigit(name[i]) && name[i] != '_' && name[i] != '/') { return false; }
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
                state.Subscribers
                    .Where(tuple => tuple.Members.Contains(CallerId))
                    .ForEach(tuple => Master.UnregisterSubscriber(tuple.Topic));
                state.Publishers
                    .Where(tuple => tuple.Members.Contains(CallerId))
                    .ForEach(tuple => Master.UnregisterPublisher(tuple.Topic));
            }
            catch (Exception e) when
                (e is SocketException || e is TimeoutException || e is AggregateException || e is IOException)
            {
                throw new ConnectionException($"Failed to contact the master URI '{MasterUri}'", e);
            }
        }

        /// <summary>
        /// Asks the master which topics we advertise and are subscribed to, and removes them.
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
                    .Where(x => x.Members.Contains(CallerId))
                    .Select(x => Master.UnregisterPublisherAsync(x.Topic))
            );

            try
            {
                await Task.WhenAll(tasks).Caf();
            }
            catch (Exception e) when
                (e is SocketException || e is TimeoutException || e is IOException)
            {
                throw new ConnectionException($"Failed to contact the master URI '{MasterUri}'", e);
            }
        }

        public void Cleanup()
        {
        }

        internal NodeClient CreateTalker(Uri otherUri)
        {
            return new NodeClient(CallerId, CallerUri, otherUri, (int) RpcNodeTimeout.TotalMilliseconds);
        }

        RosSubscriber CreateSubscriber(string topic, bool requestNoDelay, Type type, IMessage generator)
        {
            TopicInfo topicInfo = new TopicInfo(CallerId, topic, type, generator);
            int timeoutInMs = (int) TcpRosTimeout.TotalMilliseconds;
            TcpReceiverManager manager = new TcpReceiverManager(topicInfo, requestNoDelay) {TimeoutInMs = timeoutInMs};
            RosSubscriber subscription = new RosSubscriber(this, manager);

            subscribersByTopic[topic] = subscription;

            var masterResponse = Master.RegisterSubscriber(topic, topicInfo.Type);
            if (!masterResponse.IsValid)
            {
                subscribersByTopic.TryRemove(topic, out _);
                throw new XmlRpcException(
                    $"Error registering publisher for topic {topic}: {masterResponse.StatusMessage}");
            }

            manager.PublisherUpdateRpc(this, masterResponse.Publishers);

            return subscription;
        }

        async Task<RosSubscriber> CreateSubscriberAsync(string topic, bool requestNoDelay, Type type,
            IMessage generator)
        {
            TopicInfo topicInfo = new TopicInfo(CallerId, topic, type, generator);
            int timeoutInMs = (int) TcpRosTimeout.TotalMilliseconds;
            TcpReceiverManager manager = new TcpReceiverManager(topicInfo, requestNoDelay) {TimeoutInMs = timeoutInMs};
            RosSubscriber subscription = new RosSubscriber(this, manager);

            subscribersByTopic[topic] = subscription;

            var masterResponse = await Master.RegisterSubscriberAsync(topic, topicInfo.Type).Caf();
            if (!masterResponse.IsValid)
            {
                subscribersByTopic.TryRemove(topic, out _);
                throw new XmlRpcException(
                    $"Error registering publisher for topic {topic}: {masterResponse.StatusMessage}");
            }

            await manager.PublisherUpdateRpcAsync(this, masterResponse.Publishers).Caf();

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
            where T : IMessage, new()
        {
            return Subscribe(topic, callback, out RosSubscriber _, requestNoDelay);
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
        public string Subscribe<T>(string topic, Action<T> callback, out RosSubscriber subscriber,
            bool requestNoDelay = false)
            where T : IMessage, new()
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            if (callback is null) { throw new ArgumentNullException(nameof(callback)); }

            if (!TryGetSubscriber(topic, out subscriber))
            {
                subscriber = CreateSubscriber(topic, requestNoDelay, typeof(T), new T());
            }

            if (!subscriber.MessageTypeMatches(typeof(T)))
            {
                throw new InvalidMessageTypeException("Type does not match subscriber.");
            }

            // local lambda wrapper for casting
            void wrapper(IMessage x)
            {
                callback((T) x);
            }

            return subscriber.Subscribe(wrapper);
        }

        public string Subscribe(string topic, Action<IMessage> callback, Type type, out RosSubscriber subscriber,
            bool requestNoDelay = false)
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            if (callback is null) { throw new ArgumentNullException(nameof(callback)); }

            if (!typeof(IMessage).IsAssignableFrom(type))
            {
                throw new InvalidMessageTypeException("Expected IMessage object");
            }

            if (!TryGetSubscriber(topic, out subscriber))
            {
                subscriber = CreateSubscriber(topic, requestNoDelay, type, BuiltIns.CreateGenerator(type));
            }

            if (!subscriber.MessageTypeMatches(type))
            {
                throw new InvalidMessageTypeException("Expected " + subscriber.TopicType);
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
        public async Task<(string id, RosSubscriber subscriber)>
            SubscribeAsync<T>(string topic, Action<T> callback, bool requestNoDelay = false)
            where T : IMessage, new()
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            if (callback is null) { throw new ArgumentNullException(nameof(callback)); }

            if (!TryGetSubscriber(topic, out RosSubscriber subscriber))
            {
                subscriber = await CreateSubscriberAsync(topic, requestNoDelay, typeof(T), new T()).Caf();
            }

            if (!subscriber.MessageTypeMatches(typeof(T)))
            {
                throw new InvalidMessageTypeException("Type does not match subscriber.");
            }

            // local lambda wrapper for casting
            void wrapper(IMessage x)
            {
                callback((T) x);
            }

            return (subscriber.Subscribe(wrapper), subscriber);
        }

        /// <summary>
        /// Unsubscribe from the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Subscribe().</param>
        /// <returns>Whether the unsubscription succeeded.</returns>
        public bool Unsubscribe(string topicId)
        {
            if (topicId is null) { throw new ArgumentNullException(nameof(topicId)); }

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
            if (topicId is null) { throw new ArgumentNullException(nameof(topicId)); }

            var subscriber = subscribersByTopic.Values.FirstOrDefault(s => s.ContainsId(topicId));
            return subscriber != null && await subscriber.UnsubscribeAsync(topicId);
        }

        internal void RemoveSubscriber(RosSubscriber subscriber)
        {
            subscribersByTopic.TryRemove(subscriber.Topic, out _);
            Master.UnregisterSubscriber(subscriber.Topic);
        }

        internal async Task RemoveSubscriberAsync(RosSubscriber subscriber)
        {
            subscribersByTopic.TryRemove(subscriber.Topic, out _);
            await Master.UnregisterSubscriberAsync(subscriber.Topic);
        }

        /// <summary>
        /// Tries to retrieve the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="subscriber">Subscriber for the given topic.</param>
        /// <returns>Whether the subscriber was found.</returns>
        public bool TryGetSubscriber(string topic, out RosSubscriber subscriber)
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            return subscribersByTopic.TryGetValue(topic, out subscriber);
        }

        /// <summary>
        /// Retrieves the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <returns></returns>
        public RosSubscriber GetSubscriber(string topic)
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            if (TryGetSubscriber(topic, out RosSubscriber subscriber))
            {
                return subscriber;
            }

            throw new KeyNotFoundException($"Cannot find subscriber for topic '{topic}'");
        }

        RosPublisher CreatePublisher(string topic, Type type)
        {
            TopicInfo topicInfo = new TopicInfo(CallerId, topic, type);
            TcpSenderManager manager = new TcpSenderManager(topicInfo, CallerUri)
                {TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds};
            RosPublisher publisher = new RosPublisher(this, manager, type);

            publishersByTopic[topic] = publisher;

            var response = Master.RegisterPublisher(topic, topicInfo.Type);
            if (response.IsValid)
            {
                return publisher;
            }

            publishersByTopic.TryRemove(topic, out _);
            throw new ArgumentException($"Error registering publisher: {response.StatusMessage}", nameof(topic));
        }

        async Task<RosPublisher> CreatePublisherAsync(string topic, Type type)
        {
            TopicInfo topicInfo = new TopicInfo(CallerId, topic, type);
            TcpSenderManager manager = new TcpSenderManager(topicInfo, CallerUri)
                {TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds};
            RosPublisher publisher = new RosPublisher(this, manager, type);

            publishersByTopic[topic] = publisher;

            var response = await Master.RegisterPublisherAsync(topic, topicInfo.Type).Caf();
            if (response.IsValid)
            {
                return publisher;
            }

            publishersByTopic.TryRemove(topic, out _);
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
        public string Advertise<T>(string topic, out RosPublisher publisher) where T : IMessage
        {
            return Advertise(topic, typeof(T), out publisher);
        }

        public string Advertise(string topic, Type type, out RosPublisher publisher)
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            if (type is null) { throw new ArgumentNullException(nameof(type)); }

            if (!TryGetPublisher(topic, out publisher))
            {
                publisher = CreatePublisher(topic, type);
            }

            if (!publisher.MessageTypeMatches(type))
            {
                throw new InvalidMessageTypeException("Type does not match existing publisher.");
            }

            return publisher.Advertise();
        }

        /// <summary>
        /// Advertises the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <returns>A pair containing a token that can be used to unadvertise from this publisher, and the publisher object.</returns>
        public async Task<(string id, RosPublisher publisher)> AdvertiseAsync<T>(string topic) where T : IMessage
        {
            return await AdvertiseAsync(topic, typeof(T));
        }

        public async Task<(string id, RosPublisher publisher)> AdvertiseAsync(string topic, Type type)
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            if (type is null) { throw new ArgumentNullException(nameof(type)); }

            if (!TryGetPublisher(topic, out RosPublisher publisher))
            {
                publisher = await CreatePublisherAsync(topic, type).Caf();
            }

            if (!publisher.MessageTypeMatches(type))
            {
                throw new InvalidMessageTypeException("Type does not match existing publisher.");
            }

            return (publisher.Advertise(), publisher);
        }

        /// <summary>
        /// Unadvertise the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Advertise().</param>
        /// <returns>Whether the unadvertisement succeeded.</returns>
        public bool Unadvertise(string topicId)
        {
            if (topicId is null) { throw new ArgumentNullException(nameof(topicId)); }

            RosPublisher publisher = publishersByTopic.Values.FirstOrDefault(p => p.ContainsId(topicId));

            return publisher != null && publisher.Unadvertise(topicId);
        }

        /// <summary>
        /// Unadvertise the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Advertise().</param>
        /// <returns>Whether the unadvertisement succeeded.</returns>
        public async Task<bool> UnadvertiseAsync(string topicId)
        {
            if (topicId is null) { throw new ArgumentNullException(nameof(topicId)); }

            RosPublisher publisher = publishersByTopic.Values.FirstOrDefault(p => p.ContainsId(topicId));

            return publisher != null && await publisher.UnadvertiseAsync(topicId);
        }

        internal void RemovePublisher(RosPublisher publisher)
        {
            publishersByTopic.TryRemove(publisher.Topic, out _);
            Master.UnregisterPublisher(publisher.Topic);
        }

        internal async Task RemovePublisherAsync(RosPublisher publisher)
        {
            publishersByTopic.TryRemove(publisher.Topic, out _);
            await Master.UnregisterPublisherAsync(publisher.Topic);
        }

        /// <summary>
        /// Tries to retrieve the publisher of the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="publisher">Publisher of the given topic.</param>
        /// <returns>Whether the publisher was found.</returns>
        public bool TryGetPublisher(string topic, out RosPublisher publisher)
        {
            if (topic is null) { throw new ArgumentNullException(nameof(topic)); }

            return publishersByTopic.TryGetValue(topic, out publisher);
        }

        /// <summary>
        /// Retrieves the publisher of the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <returns></returns>
        public RosPublisher GetPublisher(string topic)
        {
            if (TryGetPublisher(topic, out RosPublisher publisher))
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

            throw new XmlRpcException("Failed to retrieve topics: " + response.StatusMessage);
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

            throw new XmlRpcException("Failed to retrieve topics: " + response.StatusMessage);
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

            throw new XmlRpcException("Failed to retrieve system state: " + response.StatusMessage);
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

            throw new XmlRpcException("Failed to retrieve system state: " + response.StatusMessage);
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
            if (!TryGetSubscriber(topic, out RosSubscriber subscriber))
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
                Logger.Log($"{this}: PublisherUpdateRcp failed: " + e);
            }
        }

        internal (string hostname, int port) RequestTopicRpc(string remoteCallerId, string topic)
        {
            if (!TryGetPublisher(topic, out RosPublisher publisher))
            {
                Logger.Log($"{this}: '{remoteCallerId} is requesting non-existing topic '{topic}'");
                return default;
            }

            try
            {
                return publisher.RequestTopicRpc(remoteCallerId);
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: RequestTopicRpc failed: " + e);
                return default;
            }
        }

        /// <summary>
        /// Close this connection. Unsubscribes and unadvertises all topics.
        /// </summary>
        public void Close()
        {
            RosPublisher[] publishers = publishersByTopic.Values.ToArray();
            publishersByTopic.Clear();

            publishers.ForEach(publisher =>
            {
                try
                {
                    publisher.Stop();
                    Master.UnregisterPublisher(publisher.Topic);
                }
                catch (Exception e)
                {
                    Logger.LogDebug($"Error closing publisher {publisher}: {e}");
                }
            });

            RosSubscriber[] subscribers = subscribersByTopic.Values.ToArray();
            subscribersByTopic.Clear();

            subscribers.ForEach(subscriber =>
            {
                try
                {
                    subscriber.Stop();
                    Master.UnregisterSubscriber(subscriber.Topic);
                }
                catch (Exception e)
                {
                    Logger.LogDebug($"Error closing subscriber {subscriber}: {e}");
                }
            });

            ServiceReceiver[] receivers;
            lock (subscribedServicesByName)
            {
                receivers = subscribedServicesByName.Values.ToArray();
                subscribedServicesByName.Clear();
            }

            receivers.ForEach(x => x.Stop());

            ServiceSenderManager[] serviceManagers;
            lock (advertisedServicesByName)
            {
                serviceManagers = advertisedServicesByName.Values.ToArray();
                advertisedServicesByName.Clear();
            }

            serviceManagers.ForEach(serviceSender =>
            {
                try
                {
                    serviceSender.Stop();
                    Master.UnregisterService(serviceSender.Service, serviceSender.Uri);
                }
                catch (Exception e)
                {
                    Logger.LogDebug($"Error closing subscriber {serviceSender}: {e}");
                }
            });


            listener.Dispose();
        }

        public async Task CloseAsync()
        {
            var publishers = publishersByTopic.Values.ToArray();
            publishersByTopic.Clear();

            List<Task> tasks = new List<Task>();
            tasks.AddRange(publishers.Select(publisher =>
            {
                publisher.Stop();
                return Master.UnregisterPublisherAsync(publisher.Topic);
            }));

            var subscribers = subscribersByTopic.Values.ToArray();
            subscribersByTopic.Clear();

            tasks.AddRange(subscribers.Select(subscriber =>
            {
                subscriber.Stop();
                return Master.UnregisterSubscriberAsync(subscriber.Topic);
            }));

            ServiceReceiver[] receivers;
            lock (subscribedServicesByName)
            {
                receivers = subscribedServicesByName.Values.ToArray();
                subscribedServicesByName.Clear();
            }

            receivers.ForEach(x => x.Stop());

            ServiceSenderManager[] serviceManagers;
            lock (advertisedServicesByName)
            {
                serviceManagers = advertisedServicesByName.Values.ToArray();
                advertisedServicesByName.Clear();
            }

            tasks.AddRange(serviceManagers.Select(serviceSender =>
            {
                serviceSender.Stop();
                return Master.UnregisterServiceAsync(serviceSender.Service, serviceSender.Uri);
            }));

            await Task.WhenAll(tasks).Caf();

            listener.Dispose();
        }

        public SubscriberState GetSubscriberStatistics()
        {
            return new SubscriberState(subscribersByTopic.Values.Select(x => x.GetState()).ToArray());
        }

        public PublisherState GetPublisherStatistics()
        {
            return new PublisherState(publishersByTopic.Values.Select(x => x.GetState()).ToArray());
        }

        internal IEnumerable<BusInfo> GetBusInfoRcp()
        {
            List<BusInfo> busInfos = new List<BusInfo>();

            SubscriberState state = GetSubscriberStatistics();
            foreach (var topic in state.Topics)
            {
                busInfos.AddRange(topic.Receivers.Select(
                    receiver => new BusInfo(busInfos.Count, topic.Topic, receiver)));
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
                            Logger.Log($"{this}: LookupNode for {sender.RemoteId} failed: " + e);
                            continue;
                        }

                        if (!response.IsValid)
                        {
                            continue;
                        }

                        BusInfo busInfo = new BusInfo(busInfos.Count, response.Uri, "o", topic.Topic,
                            status: sender.IsAlive);
                        busInfos.Add(busInfo);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: GetBusInfoRcp failed: " + e);
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
            ServiceReceiver serviceReceiver = null;
            lock (subscribedServicesByName)
            {
                if (subscribedServicesByName.TryGetValue(serviceName, out serviceReceiver))
                {
                    if (!serviceReceiver.IsAlive)
                    {
                        serviceReceiver.Stop();
                        subscribedServicesByName.Remove(serviceName);
                        serviceReceiver = null;
                    }
                }
            }

            // is there a persistent connection? use it
            if (serviceReceiver != null)
            {
                return serviceReceiver.Execute(service);
            }

            // otherwise, create a new transient one
            LookupServiceResponse response = Master.LookupService(serviceName);
            if (!response.IsValid)
            {
                throw new XmlRpcException("Failed to call service: " + response.StatusMessage);
            }

            Uri serviceUri = response.ServiceUrl;
            ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T));
            try
            {
                using (serviceReceiver = new ServiceReceiver(serviceInfo, serviceUri, true, persistent))
                {
                    serviceReceiver.Start();
                    bool result = serviceReceiver.Execute(service);

                    if (persistent && serviceReceiver.IsAlive)
                    {
                        lock (subscribedServicesByName)
                        {
                            subscribedServicesByName.Add(serviceName, serviceReceiver);
                        }
                    }

                    return result;
                }
            }
            catch (Exception e) when (e is SocketException)
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
                throw new XmlRpcException("Failed to call service: " + response.StatusMessage);
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
            catch (Exception e) when (e is SocketException)
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
            ServiceSenderManager advertisedService;

            lock (advertisedServicesByName)
            {
                if (advertisedServicesByName.ContainsKey(serviceName))
                {
                    throw new ArgumentException("Service already exists", nameof(serviceName));
                }

                async Task Wrapper(IService x)
                {
                    await Task.Run(() => callback((T) x));
                }

                ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T), new T());
                advertisedService = new ServiceSenderManager(serviceInfo, CallerUri.Host, Wrapper);

                advertisedServicesByName.Add(serviceName, advertisedService);
            }

            // local lambda wrapper for casting
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
            ServiceSenderManager advertisedService;

            lock (advertisedServicesByName)
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
                advertisedService = new ServiceSenderManager(serviceInfo, CallerUri.Host, Wrapper);

                advertisedServicesByName.Add(serviceName, advertisedService);
            }

            await Master.RegisterServiceAsync(serviceName, advertisedService.Uri);
        }

        public void UnadvertiseService(string name)
        {
            ServiceSenderManager advertisedService;

            lock (advertisedServicesByName)
            {
                if (!advertisedServicesByName.TryGetValue(name, out advertisedService))
                {
                    throw new ArgumentException("Service does not exist", nameof(name));
                }

                advertisedServicesByName.Remove(name);
            }

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