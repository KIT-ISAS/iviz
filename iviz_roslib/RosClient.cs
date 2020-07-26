using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    /// <summary>
    /// The provided message type is not correct.
    /// </summary>
    public class InvalidMessageTypeException : Exception
    {
        public InvalidMessageTypeException(string message) : base(message) { }
        public InvalidMessageTypeException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidMessageTypeException() { }
    }

    /// <summary>
    /// An error happened during the connection.
    /// </summary>
    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message) { }
        public ConnectionException(string message, Exception innerException) : base(message, innerException) { }
        public ConnectionException() { }
    }

    /// <summary>
    /// The uri provided for the caller (this node) is not reachable.
    /// </summary>
    public class UnreachableUriException : Exception
    {
        public UnreachableUriException(string message) : base(message) { }
        public UnreachableUriException(string message, Exception innerException) : base(message, innerException) { }
        public UnreachableUriException() { }
    }

    public class XmlRpcException : Exception
    {
        public XmlRpcException(string message) : base(message) { }
        public XmlRpcException(string message, Exception innerException) : base(message, innerException) { }
        public XmlRpcException() { }
    }

    public sealed class RosClient : IDisposable
    {
        readonly XmlRpc.NodeServer Listener;

        readonly Dictionary<string, RosSubscriber> subscribersByTopic = new Dictionary<string, RosSubscriber>();
        readonly Dictionary<string, RosPublisher> publishersByTopic = new Dictionary<string, RosPublisher>();

        readonly Dictionary<string, ServiceReceiver> subscribedServicesByName = new Dictionary<string, ServiceReceiver>();
        readonly Dictionary<string, ServiceSenderManager> advertisedServicesByName = new Dictionary<string, ServiceSenderManager>();

        public delegate void ShutdownActionCall(
            string callerId, string reason,
            out XmlRpc.StatusCode status, out string response);

        /// <summary>
        /// Handler of 'shutdown' XMLRPC calls from the slave API
        /// </summary>
        public ShutdownActionCall ShutdownAction { get; set; }

        public delegate void ParamUpdateActionCall(
            string callerId, string parameterKey, object parametervalue,
            out XmlRpc.StatusCode status, out string response);

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
        public XmlRpc.Master Master { get; }

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

        TimeSpan rpcNodeTimeout = TimeSpan.FromSeconds(5);

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

        TimeSpan tcpRosTimeout = TimeSpan.FromSeconds(5);

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
                lock (subscribersByTopic)
                {
                    subscribersByTopic.Values.ForEach(
                        x => x.TimeoutInMs = (int) value.TotalMilliseconds);
                }

                lock (publishersByTopic)
                {
                    publishersByTopic.Values.ForEach(
                        x => x.TimeoutInMs = (int) value.TotalMilliseconds);
                }
            }
        }


        /// <summary>
        /// Wrapper for XML-RPC calls to the master.
        /// </summary>
        public XmlRpc.ParameterClient Parameters { get; }

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
            if (masterUri is null)
            {
                throw new ArgumentException("No valid master uri provided", nameof(masterUri));
            }

            if (masterUri.Scheme != "http")
            {
                throw new ArgumentException("URI scheme must be http", nameof(masterUri));
            }

            if (callerUri == null)
            {
                callerUri = TryGetCallerUri();
            }

            if (callerUri.Scheme != "http")
            {
                throw new ArgumentException("URI scheme must be http", nameof(callerUri));
            }

            CallerId = callerId ?? "/RosClient";
            CallerUri = callerUri;

            try
            {
                Listener = new XmlRpc.NodeServer(this);
                Listener.Start();
                if (CallerUri.Port == 0 || CallerUri.IsDefaultPort)
                {
                    CallerUri = new Uri($"http://{CallerUri.Host}:{Listener.ListenerUri.Port}{CallerUri.AbsolutePath}");
                }
            }
            catch (SocketException e)
            {
                Listener?.Stop();
                throw new ConnectionException($"Failed to bind to local URI '{callerUri}'", e);
            }

            Master = new XmlRpc.Master(masterUri, CallerId, CallerUri);
            Parameters = new XmlRpc.ParameterClient(masterUri, CallerId, CallerUri);

            Logger.Log($"** RosClient: Starting!\n" +
                       $"** My Id is {CallerId}\n" +
                       $"** My Uri is {CallerUri}\n" +
                       $"** I'm talking to {MasterUri}");

            try
            {
                // check if the system thinks we are subscribed to or advertise topic,
                // possibly from a previous session. if so, remove them.
                // this also doubles as a reachability test for the master
                EnsureCleanSlate();
            }
            catch (Exception e) when
                (e is SocketException || e is TimeoutException || e is AggregateException || e is IOException)
            {
                Listener.Stop();
                throw new ConnectionException($"Failed to contact the master URI '{masterUri}'", e);
            }

            Logger.Log("RosClient: Initialized.");


            try
            {
                var response = CreateTalker(CallerUri).GetPid();
                if (!response.IsValid)
                {
                    Logger.LogError("RosClient: Failed to validate reachability response.");
                }
                else
                {
                    if (response.Pid != Process.GetCurrentProcess().Id)
                    {
                        Listener.Stop();
                        throw new UnreachableUriException($"My uri '{CallerUri}' appears to belong to someone else!");
                    }
                }
            }
            catch (Exception e) when
                (e is SocketException || e is TimeoutException || e is AggregateException)
            {
                Listener.Stop();
                throw new UnreachableUriException($"My uri '{CallerUri}' does not appear to be reachable!");
            }
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
                if (envStr is null)
                {
                    return null;
                }

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
        public static Uri TryGetCallerUri()
        {
            return EnvironmentCallerUri ?? new Uri($"http://{Dns.GetHostName()}:7613/");
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
        /// Try to retrieve a valid master uri.
        /// </summary>        
        public static Uri TryGetMasterUri()
        {
            return EnvironmentMasterUri ?? new Uri($"http://{Dns.GetHostName()}:11311/");
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
        /// Asks the master which topics we advertise and are subscribed to, and removes them.
        /// </summary>
        void EnsureCleanSlate()
        {
            SystemState state = GetSystemState();
            state.Subscribers.Where(x => x.Members.Contains(CallerId))
                .ForEach(x => Master.UnregisterSubscriber(x.Topic));
            state.Publishers.Where(x => x.Members.Contains(CallerId)).ForEach(x => Master.UnregisterPublisher(x.Topic));
        }

        public void Cleanup()
        {
            lock (subscribersByTopic)
            {
                subscribersByTopic.Values.ForEach(x => x.Cleanup());
            }

            lock (publishersByTopic)
            {
                publishersByTopic.Values.ForEach(x => x.Cleanup());
            }
        }

        internal XmlRpc.NodeClient CreateTalker(Uri otherUri)
        {
            return new XmlRpc.NodeClient(CallerId, CallerUri, otherUri, (int) RpcNodeTimeout.TotalMilliseconds);
        }

        RosSubscriber CreateSubscriber(string topic, bool requestNoDelay, Type type, IMessage generator)
        {
            TopicInfo topicInfo = new TopicInfo(CallerId, topic, type, generator);
            TcpReceiverManager manager = new TcpReceiverManager(topicInfo, requestNoDelay)
            {
                TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds
            };
            RosSubscriber subscription = new RosSubscriber(this, manager);

            lock (subscribersByTopic)
            {
                subscribersByTopic[topic] = subscription;
            }

            var masterResponse = Master.RegisterSubscriber(topic, topicInfo.Type);
            if (!masterResponse.IsValid)
            {
                lock (subscribersByTopic)
                {
                    subscribersByTopic.Remove(topic);
                }

                throw new ArgumentException("Error registering publisher: " + masterResponse.StatusMessage,
                    nameof(topic));
            }

            manager.PublisherUpdateRpc(this, masterResponse.Publishers);

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
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

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
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

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

            RosSubscriber subscriber;
            lock (subscribersByTopic)
            {
                subscriber = subscribersByTopic.Values.FirstOrDefault(x => x.ContainsId(topicId));
            }

            return subscriber != null && subscriber.Unsubscribe(topicId);
        }

        internal void RemoveSubscriber(RosSubscriber subscriber)
        {
            lock (subscribersByTopic)
            {
                subscribersByTopic.Remove(subscriber.Topic);
            }

            Master.UnregisterSubscriber(subscriber.Topic);
        }

        /// <summary>
        /// Tries to retrieve the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="subscriber">Subscriber for the given topic.</param>
        /// <returns>Whether the subscriber was found.</returns>
        public bool TryGetSubscriber(string topic, out RosSubscriber subscriber)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            lock (subscribersByTopic)
            {
                return subscribersByTopic.TryGetValue(topic, out subscriber);
            }
        }

        /// <summary>
        /// Retrieves the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <returns></returns>
        public RosSubscriber GetSubscriber(string topic)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

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
            {
                TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds
            };
            RosPublisher publisher = new RosPublisher(this, manager);

            lock (publishersByTopic)
            {
                publishersByTopic[topic] = publisher;
            }

            var response = Master.RegisterPublisher(topic, topicInfo.Type);
            if (!response.IsValid)
            {
                lock (publishersByTopic)
                {
                    publishersByTopic.Remove(topic);
                }

                throw new ArgumentException("Error registering publisher: " + response.StatusMessage, nameof(topic));
            }

            return publisher;
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
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!TryGetPublisher(topic, out publisher))
            {
                publisher = CreatePublisher(topic, type);
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
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            RosPublisher publisher;
            lock (publishersByTopic)
            {
                publisher = publishersByTopic.Values.FirstOrDefault(x => x.ContainsId(topicId));
            }

            return publisher != null && publisher.Unadvertise(topicId);
        }

        internal void RemovePublisher(RosPublisher publisher)
        {
            lock (publishersByTopic)
            {
                publishersByTopic.Remove(publisher.Topic);
            }

            Master.UnregisterPublisher(publisher.Topic);
        }

        /// <summary>
        /// Tries to retrieve the publisher of the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="publisher">Publisher of the given topic.</param>
        /// <returns>Whether the publisher was found.</returns>
        public bool TryGetPublisher(string topic, out RosPublisher publisher)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            lock (publishersByTopic)
            {
                return publishersByTopic.TryGetValue(topic, out publisher);
            }
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
            else
            {
                throw new KeyNotFoundException($"Cannot find publisher for topic '{topic}'");
            }
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
                return Master.GetPublishedTopics().Topics.
                    Select(x => new BriefTopicInfo(x.Item1, x.Item2)).
                    ToArray().AsReadOnly();
            }
            
            throw new XmlRpcException("Failed to retrieve topics: " + response.StatusMessage);
        }

        /// <summary>
        /// Gets the topics published by this node.
        /// </summary>
        public ReadOnlyCollection<BriefTopicInfo> SubscribedTopics =>
            GetSubscriptionsRcp().Select(x => new BriefTopicInfo(x[0], x[1])).ToArray().AsReadOnly();


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
            else
            {
                throw new XmlRpcException("Failed to retrieve system state: " + response.StatusMessage);
            }
        }

        /// <summary>
        /// Gets the topics published by this node.
        /// </summary>
        public ReadOnlyCollection<BriefTopicInfo> PublishedTopics =>
            GetPublicationsRcp().Select(x => new BriefTopicInfo(x[0], x[1])).ToArray().AsReadOnly();


        internal string[][] GetSubscriptionsRcp()
        {
            try
            {
                lock (subscribersByTopic)
                {
                    string[][] result = new string[subscribersByTopic.Count][];
                    int i = 0;
                    foreach (var entry in subscribersByTopic)
                    {
                        result[i++] = new[] {entry.Key, entry.Value.TopicType};
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: GetSubscriptionsRcp failed: " + e);
                return Array.Empty<string[]>();
            }
        }


        internal string[][] GetPublicationsRcp()
        {
            try
            {
                lock (publishersByTopic)
                {
                    string[][] result = new string[publishersByTopic.Count][];
                    int i = 0;
                    foreach (var entry in publishersByTopic)
                    {
                        result[i++] = new[] {entry.Key, entry.Value.TopicType};
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: GetPublicationsRcp failed: " + e);
                return Array.Empty<string[]>();
            }
        }


        internal void PublisherUpdateRcp(string topic, Uri[] publishers)
        {
            try
            {
                if (!TryGetSubscriber(topic, out RosSubscriber subscriber))
                {
                    Logger.Log($"{this}: PublisherUpdate called for nonexisting topic '{topic}'");
                    return;
                }

                subscriber.PublisherUpdateRcp(publishers);
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: PublisherUpdateRcp failed: " + e);
            }
        }

        internal bool RequestTopicRpc(string remoteCallerId, string topic, out string hostname, out int port)
        {
            try
            {
                if (!TryGetPublisher(topic, out RosPublisher publisher))
                {
                    Logger.Log($"{this}: '{remoteCallerId} is requesting nonexisting topic '{topic}'");
                    hostname = null;
                    port = 0;
                    return false;
                }

                publisher.RequestTopicRpc(remoteCallerId, out hostname, out port);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: RequestTopicRpc failed: " + e);
                hostname = "";
                port = 0;
                return false;
            }
        }

        /// <summary>
        /// Close this connection. Unsubscribes and unadvertises all topics.
        /// </summary>
        public void Close()
        {
            RosPublisher[] publishers;
            lock (publishersByTopic)
            {
                publishers = publishersByTopic.Values.ToArray();
                publishersByTopic.Clear();
            }

            publishers.ForEach(x => x.Stop());

            RosSubscriber[] subscribers;
            lock (subscribersByTopic)
            {
                subscribers = subscribersByTopic.Values.ToArray();
                subscribersByTopic.Clear();
            }

            subscribers.ForEach(x => x.Stop());

            lock (subscribedServicesByName)
            {
                subscribedServicesByName.ForEach(x => x.Value.Stop());
                subscribedServicesByName.Clear();
            }

            lock (advertisedServicesByName)
            {
                advertisedServicesByName.ForEach(x => x.Value.Stop());
                advertisedServicesByName.Clear();
            }

            Listener.Stop();
        }

        public SubscriberState GetSubscriberStatistics()
        {
            lock (subscribersByTopic)
                return new SubscriberState(subscribersByTopic.Values.Select(x => x.GetState()).ToArray());
        }

        public PublisherState GetPublisherStatistics()
        {
            lock (publishersByTopic)
                return new PublisherState(publishersByTopic.Values.Select(x => x.GetState()).ToArray());
        }

        internal List<BusInfo> GetBusInfoRcp()
        {
            List<BusInfo> busInfos = new List<BusInfo>();
            try
            {
                SubscriberState sstate = GetSubscriberStatistics();
                foreach (var topic in sstate.Topics)
                {
                    foreach (var receiver in topic.Receivers)
                    {
                        busInfos.Add(new BusInfo(
                            busInfos.Count,
                            receiver.RemoteUri,
                            "i", "TCPROS",
                            topic.Topic,
                            1));
                    }
                }

                PublisherState pstate = GetPublisherStatistics();
                foreach (var topic in pstate.Topics)
                {
                    foreach (var sender in topic.Senders)
                    {
                        Uri remoteUri;
                        try
                        {
                            var response = Master.LookupNode(sender.RemoteId);
                            remoteUri = response.IsValid ? response.Uri : null;
                        }
                        catch (Exception e)
                        {
                            Logger.Log($"{this}: LookupNode for {sender.RemoteId} failed: " + e);
                            remoteUri = null;
                        }

                        busInfos.Add(new BusInfo(
                            busInfos.Count,
                            remoteUri,
                            "o", "TCPROS",
                            topic.Topic,
                            1));
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

            if (serviceReceiver != null)
            {
                return serviceReceiver.Execute(service);
            }

            XmlRpc.LookupServiceResponse response = Master.LookupService(serviceName);
            if (!response.IsValid)
            {
                throw new XmlRpcException("Failed to call service: " + response.StatusMessage);
            }

            Uri serviceUri = response.ServiceUrl;
            ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T), null);
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

                void Wrapper(IService x) { callback((T)x); }

                ServiceInfo serviceInfo = new ServiceInfo(CallerId, serviceName, typeof(T), new T());
                advertisedService = new ServiceSenderManager(serviceInfo, CallerUri.Host, Wrapper);

                advertisedServicesByName.Add(serviceName, advertisedService);
            }

            // local lambda wrapper for casting
            Master.RegisterService(serviceName, advertisedService.Uri);
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

        public XmlRpc.StatusCode SetParameter(string key, string value)
        {
            return Parameters.SetParam(key, new XmlRpc.Arg(value)).Code;
        }

        public XmlRpc.StatusCode SetParameter(string key, int value)
        {
            return Parameters.SetParam(key, new XmlRpc.Arg(value)).Code;
        }

        public XmlRpc.StatusCode SetParameter(string key, bool value)
        {
            return Parameters.SetParam(key, new XmlRpc.Arg(value)).Code;
        }

        public XmlRpc.StatusCode SetParameter(string key, double value)
        {
            return Parameters.SetParam(key, new XmlRpc.Arg(value)).Code;
        }

        public XmlRpc.StatusCode SetParameter(string key, string[] value)
        {
            return Parameters.SetParam(key, new XmlRpc.Arg(value)).Code;
        }

        public XmlRpc.StatusCode GetParameter(string key, out object value)
        {
            var response = Parameters.GetParam(key);
            value = response.ParameterValue;
            return response.Code;
        }
        
        public ReadOnlyCollection<string> GetParameterNames()
        {
            var response = Parameters.GetParamNames();
            if (response.IsValid)
            {
                return response.ParameterNameList;
            }

            throw new XmlRpcException("Failed to retrieve parameter names: " + response.StatusMessage);
        }

        public XmlRpc.StatusCode DeleteParameter(string key)
        {
            return Parameters.DeleteParam(key).Code;
        }

        public bool HasParameter(string key)
        {
            return Parameters.HasParam(key).HasParam;
        }

        public XmlRpc.StatusCode SubscribeParameter(string key)
        {
            return Parameters.SubscribeParam(key).Code;
        }

        public XmlRpc.StatusCode UnsubscribeParameter(string key)
        {
            return Parameters.UnsubscribeParam(key).Code;
        }

        public void Dispose()
        {
            Close();
            Listener.Dispose();
        }

        public override string ToString()
        {
            return $"[RosClient MyUri='{CallerUri}' MyId='{CallerId}' MasterUri='{MasterUri}']";
        }
    }
}
