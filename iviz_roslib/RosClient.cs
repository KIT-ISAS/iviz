using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Roslib.Utils;
using Iviz.Roslib.XmlRpc;
using Iviz.XmlRpc;
using Nito.AsyncEx;

namespace Iviz.Roslib
{
    /// <summary>
    /// Class that manages a client connection to a ROS master. 
    /// </summary>
    public sealed class RosClient : IRosClient
    {
        public const int AnyPort = 0;

        RosNodeServer? listener;

        readonly ConcurrentDictionary<string, IRosSubscriber> subscribersByTopic = new();
        readonly ConcurrentDictionary<string, IRosPublisher> publishersByTopic = new();
        readonly ConcurrentDictionary<string, IServiceCaller> subscribedServicesByName = new();
        readonly ConcurrentDictionary<string, IServiceRequestManager> advertisedServicesByName = new();

        readonly string namespacePrefix;

        public delegate void ShutdownActionCall(
            string callerId, string reason,
            out int status, out string response);

        /// <summary>
        /// Handler of 'shutdown' XMLRPC calls from the slave API
        /// </summary>
        public ShutdownActionCall? ShutdownAction { get; set; }

        public delegate void ParamUpdateActionCall(
            string callerId, string parameterKey, XmlRpcValue parameterValue,
            out int status, out string response);

        /// <summary>
        /// Handler of 'paramUpdate' XMLRPC calls from the slave API
        /// </summary>
        public ParamUpdateActionCall? ParamUpdateAction { get; set; }

        /// <summary>
        /// ID of this node.
        /// </summary>
        public string CallerId { get; }

        /// <summary>
        /// Wrapper for XML-RPC calls to the master.
        /// </summary>
        public RosMasterClient RosMasterClient { get; private set; }

        /// <summary>
        /// Timeout in milliseconds for XML-RPC communications with the master.
        /// </summary>
        public TimeSpan RpcMasterTimeout
        {
            get => TimeSpan.FromMilliseconds(RosMasterClient.TimeoutInMs);
            set
            {
                if (value.TotalMilliseconds <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                RosMasterClient.TimeoutInMs = (int) value.TotalMilliseconds;
            }
        }

        TimeSpan rpcNodeTimeout = TimeSpan.FromSeconds(3);

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

        TimeSpan tcpRosTimeout = TimeSpan.FromSeconds(3);

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
        public ParameterClient Parameters { get; private set; }

        /// <summary>
        /// URI of the master node.
        /// </summary>
        public Uri MasterUri => RosMasterClient.MasterUri;

        /// <summary>
        /// URI of this node.
        /// </summary>
        public Uri CallerUri { get; private set; }


        RosClient(Uri? masterUri, string? callerId, Uri? callerUri, string? namespaceOverride)
        {
            masterUri ??= EnvironmentMasterUri;

            if (masterUri is null)
            {
                throw new ArgumentException("No valid master uri provided, and ROS_MASTER_URI is not set",
                    nameof(masterUri));
            }

            if (masterUri.Scheme != "http")
            {
                throw new ArgumentException("URI scheme must be http", nameof(masterUri));
            }

            callerUri ??= TryGetCallerUriFor(masterUri) ?? TryGetCallerUri();

            if (callerUri.Scheme != "http")
            {
                throw new ArgumentException("URI scheme must be http", nameof(callerUri));
            }

            if (string.IsNullOrWhiteSpace(callerId))
            {
                callerId = CreateCallerId();
            }

            string? ns = namespaceOverride ?? EnvironmentRosNamespace;
            namespacePrefix = ns == null ? "/" : $"/{ns}/";

            if (callerId![0] != '/')
            {
                callerId = $"{namespacePrefix}{callerId}";
            }

            if (!IsValidResourceName(callerId))
            {
                throw new ArgumentException($"Caller id '{callerId}' is not a valid global ROS resource name");
            }

            CallerId = callerId;
            CallerUri = callerUri;

            RosMasterClient = new RosMasterClient(masterUri, CallerId, CallerUri, 3);
            Parameters = new ParameterClient(RosMasterClient);
        }

        /// <summary>
        /// Constructs and connects a ROS client in a sync way.
        /// You should use <see cref="CreateAsync"/> in async contexts.
        /// </summary>
        /// <param name="masterUri">
        /// URI to the master node. Example: new Uri("http://localhost:11311").
        /// </param>
        /// <param name="callerId">
        /// The ROS name of this node.
        /// This is your identity in the network, and must be unique. Example: /my_new_node
        /// Leave empty to generate one automatically.
        /// </param>
        /// <param name="callerUri">
        /// URI of this node.
        /// Other clients will use this address to connect to this node.
        /// Leave empty to generate one automatically. </param>
        /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
        /// <param name="namespaceOverride">Set this to override ROS_NAMESPACE.</param>
        public RosClient(Uri? masterUri = null, string? callerId = null, Uri? callerUri = null,
            bool ensureCleanSlate = true, string? namespaceOverride = null) : this(masterUri, callerId, callerUri,
            namespaceOverride)
        {
            try
            {
                // Do a simple ping to the master. This will tell us whether the master is reachable.
                RosMasterClient.GetUri();
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                throw new RosConnectionException($"Failed to contact the master URI '{masterUri}'", e);
            }

            try
            {
                // Create an XmlRpc server. This will tell us quickly whether the port is taken.
                listener = new RosNodeServer(this);
            }
            catch (SocketException e)
            {
                throw new RosUriBindingException($"Failed to bind to local URI '{callerUri}'", e);
            }

            // Start the XmlRpc server.
            listener.Start();

            if (CallerUri.Port == AnyPort || CallerUri.IsDefaultPort)
            {
                string absolutePath = Uri.UnescapeDataString(CallerUri.AbsolutePath);
                CallerUri = new Uri($"http://{CallerUri.Host}:{listener.ListenerPort.ToString()}{absolutePath}");

                // caller uri has changed;
                RosMasterClient = new RosMasterClient(MasterUri, CallerId, CallerUri, 3);
                Parameters = new ParameterClient(RosMasterClient);
            }


            Logger.LogDebugFormat("** {0}: Initialized.", this);

            if (ensureCleanSlate)
            {
                try
                {
                    EnsureCleanSlate();
                }
                catch (Exception)
                {
                    Logger.LogDebugFormat("** {0}: EnsureCleanState failed.", this);
                    listener.Dispose();
                    throw;
                }
            }
        }

        /// <summary>
        /// Constructs and connects a ROS client in async.
        /// </summary>
        /// <param name="masterUri">
        /// URI to the master node. Example: new Uri("http://localhost:11311").
        /// </param>
        /// <param name="callerId">
        /// The ROS name of this node.
        /// This is your identity in the network, and must be unique. Example: /my_new_node
        /// Leave empty to generate one automatically.
        /// </param>
        /// <param name="callerUri">
        /// URI of this node.
        /// Other clients will use this address to connect to this node.
        /// Leave empty to generate one automatically. </param>
        /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
        /// <param name="namespaceOverride">Set this to override ROS_NAMESPACE.</param>
        /// <param name="token">An optional cancellation token.</param>        
        public static async ValueTask<RosClient> CreateAsync(Uri? masterUri = null, string? callerId = null,
            Uri? callerUri = null, bool ensureCleanSlate = true, string? namespaceOverride = null,
            CancellationToken token = default)
        {
            RosClient client = new(masterUri, callerId, callerUri, namespaceOverride);

            try
            {
                // Do a simple ping to the master. This will tell us whether the master is reachable.
                await client.RosMasterClient.GetUriAsync(token);
            }
            catch (Exception e)
            {
                throw new RosConnectionException($"Failed to contact the master URI '{masterUri}'", e);
            }

            try
            {
                // Create an XmlRpc server. This will tell us quickly whether the port is taken.
                client.listener = new RosNodeServer(client);
            }
            catch (SocketException e)
            {
                throw new RosUriBindingException($"Failed to bind to local URI '{callerUri}'", e);
            }

            client.listener.Start();

            if (client.CallerUri.Port == AnyPort || client.CallerUri.IsDefaultPort)
            {
                string absolutePath = Uri.UnescapeDataString(client.CallerUri.AbsolutePath);
                client.CallerUri =
                    new Uri($"http://{client.CallerUri.Host}:{client.listener.ListenerPort}{absolutePath}");

                // caller uri has changed;
                client.RosMasterClient = new RosMasterClient(client.MasterUri, client.CallerId, client.CallerUri);
                client.Parameters = new ParameterClient(client.RosMasterClient);
            }

            Logger.LogFormat("** {0}: Initialized.", client);

            if (ensureCleanSlate)
            {
                try
                {
                    await client.EnsureCleanSlateAsync(token);
                }
                catch (Exception)
                {
                    Logger.LogDebugFormat("** {0}: EnsureCleanState failed.", client);
                    await client.listener.DisposeAsync();
                    throw;
                }
            }

            return client;
        }
        
        /// <summary>
        /// Constructs and connects a ROS client. Same as calling new() directly.
        /// </summary>
        /// <param name="masterUri">
        /// URI to the master node. Example: new Uri("http://localhost:11311").
        /// </param>
        /// <param name="callerId">
        /// The ROS name of this node.
        /// This is your identity in the network, and must be unique. Example: /my_new_node
        /// Leave empty to generate one automatically.
        /// </param>
        /// <param name="callerUri">
        /// URI of this node.
        /// Other clients will use this address to connect to this node.
        /// Leave empty to generate one automatically. </param>
        /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
        /// <param name="namespaceOverride">Set this to override ROS_NAMESPACE.</param>
        public static RosClient Create(Uri? masterUri = null, string? callerId = null,
            Uri? callerUri = null, bool ensureCleanSlate = true, string? namespaceOverride = null) =>
            new RosClient(masterUri, callerId, callerUri, ensureCleanSlate, namespaceOverride);
        

        /// <summary>
        /// Constructs and connects a ROS client.
        /// </summary>
        /// <param name="masterUri">
        /// URI to the master node. Example: http://localhost:11311.
        /// </param>
        /// <param name="callerId">
        /// The ROS name of this node.
        /// This is your identity in the network, and must be unique. Example: /my_new_node
        /// Leave empty to generate one automatically.
        /// </param>
        /// <param name="callerUri">
        /// URI of this node.
        /// Other clients will use this address to connect to this node.
        /// Leave empty to generate one automatically. </param>
        /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
        public RosClient(string? masterUri,
            string? callerId = null,
            string? callerUri = null,
            bool ensureCleanSlate = true) :
            this(masterUri != null ? new Uri(masterUri) : null,
                callerId,
                callerUri != null ? new Uri(callerUri) : null,
                ensureCleanSlate
            )
        {
        }

        /// <summary>
        /// Creates a unique id based on the project and computer name
        /// </summary>
        /// <returns>A more or less unique id</returns>
        public static string CreateCallerId()
        {
            string seed = EnvironmentCallerHostname + Assembly.GetCallingAssembly().GetName().Name;
            return $"iviz_{seed.GetDeterministicHashCode():x8}";
        }

        /// <summary>
        /// Retrieves the environment variable ROS_HOSTNAME as a uri.
        /// If this fails, retrieves ROS_IP.
        /// </summary>
        public static string? EnvironmentCallerHostname =>
            Environment.GetEnvironmentVariable("ROS_HOSTNAME") ??
            Environment.GetEnvironmentVariable("ROS_IP");

        /// <summary>
        /// Retrieves a valid caller uri for this node, by checking the local addresses
        /// of the wireless and ethernet interfaces.
        /// If this fails, returns a caller uri with the local hostname.
        /// You should probably use <see cref="TryGetCallerUriFor"/> first and then this as a fallback.  
        /// </summary>
        /// <param name="usingPort">Port for the caller uri, or 0 for a random free port.</param>
        /// <returns>A caller uri</returns>
        public static Uri TryGetCallerUri(int usingPort = AnyPort)
        {
            string? envHostname = EnvironmentCallerHostname;
            if (envHostname != null)
            {
                return new Uri($"http://{envHostname}:{usingPort}/");
            }

            return BaseUtils.GetUriFromInterface(NetworkInterfaceType.Wireless80211, usingPort) ??
                   BaseUtils.GetUriFromInterface(NetworkInterfaceType.Ethernet, usingPort) ??
                   new Uri($"http://{Dns.GetHostName()}:{usingPort}/");
        }


        /// <summary>
        /// Tries to retrieve a valid caller uri for this node given a master address, by checking
        /// the active interfaces and searching for one in the same IPv4 subnet.
        /// Returns null if none is found.
        /// </summary>
        /// <param name="masterUri">The uri of the ROS master</param>
        /// <param name="usingPort">Port for the caller uri, or 0 for a random free port.</param>
        /// <returns>A caller uri, or null if none found.</returns>
        public static Uri? TryGetCallerUriFor(Uri masterUri, int usingPort = AnyPort)
        {
            if (masterUri == null)
            {
                throw new ArgumentNullException(nameof(masterUri));
            }

            string? envHostname = EnvironmentCallerHostname;
            if (envHostname != null)
            {
                return new Uri($"http://{envHostname}:{usingPort}/");
            }

            IPAddress? masterAddress =
                IPAddress.TryParse(masterUri.Host, out IPAddress? parsedAddress)
                    ? parsedAddress
                    : Dns.GetHostEntry(masterUri.Host).AddressList.FirstOrDefault(
                        address => address.AddressFamily == AddressFamily.InterNetwork);

            if (masterAddress == null)
            {
                return null; // IPv6 not implemented yet!
            }

            var candidates = NetworkInterface.GetAllNetworkInterfaces()
                .Where(@interface => @interface.OperationalStatus == OperationalStatus.Up)
                .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
                .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);

            bool CanAccessMaster(UnicastIPAddressInformation info) =>
                BaseUtils.IsInSameSubnet(info.Address, masterAddress, info.IPv4Mask);

            IPAddress? myAddress = candidates
                .Select(info => (info, success: CanAccessMaster(info)))
                .FirstOrDefault(tuple => tuple.success)
                .info?.Address;

            return myAddress == null ? null : new Uri($"http://{myAddress}:{usingPort}/");
        }

        /// <summary>
        /// Retrieves a list of possible valid caller uri with the given port.
        /// Other clients will connect to this node using this address.
        /// If the port is 0, uses a random free port.
        /// </summary>
        /// <param name="usingPort">Port for the caller uri, or 0 for a random free port.</param>
        /// <returns>A list of possible caller uris.</returns>
        public static ReadOnlyCollection<Uri> GetCallerUriCandidates(int usingPort = AnyPort)
        {
            List<Uri> candidates = new();
            string? envHostname = EnvironmentCallerHostname;
            if (envHostname != null)
            {
                candidates.Add(new Uri($"http://{envHostname}:{usingPort}/"));
            }

            candidates.Add(new Uri($"http://{Dns.GetHostName()}:{usingPort}/"));

            IEnumerable<Uri> GetInfosAsUri() =>
                NetworkInterface.GetAllNetworkInterfaces()
                    .Where(@interface => @interface.OperationalStatus == OperationalStatus.Up)
                    .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
                    .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    .Select(info => new Uri($"http://{info.Address}:{usingPort}/")).ToArray();

            candidates.AddRange(GetInfosAsUri());

            return candidates.AsReadOnly();
        }

        /// <summary>
        /// Retrieves the environment variable ROS_MASTER_URI as a uri.
        /// </summary>
        public static Uri? EnvironmentMasterUri
        {
            get
            {
                string? envStr = Environment.GetEnvironmentVariable("ROS_MASTER_URI");
                if (envStr is null)
                {
                    return null;
                }

                if (Uri.TryCreate(envStr, UriKind.Absolute, out Uri? uri))
                {
                    return uri;
                }

                Logger.LogErrorFormat("EE Environment variable for master uri '{0}' is not a valid uri!", envStr);
                return null;
            }
        }

        static string? EnvironmentRosNamespace
        {
            get
            {
                string? ns = Environment.GetEnvironmentVariable("ROS_NAMESPACE");
                if (string.IsNullOrEmpty(ns))
                {
                    return null;
                }

                return !IsValidResourceName(ns) ? null : ns;
            }
        }


        /// <summary>
        /// Checks if the given name is a valid ROS resource name
        /// </summary>  
        public static bool IsValidResourceName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            static bool IsAlpha(char c) => ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z');

            if (!IsAlpha(name![0]) && name[0] != '/' && name[0] != '~')
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
        /// Try to retrieve a valid master uri.
        /// </summary>        
        public static Uri TryGetMasterUri()
        {
            return EnvironmentMasterUri ?? new Uri("http://localhost:11311/");
        }

        /// <summary>
        /// Asks the master which topics we advertise and are subscribed to, and removes them.
        /// </summary>
        public void EnsureCleanSlate(CancellationToken token = default)
        {
            Task.Run(() => EnsureCleanSlateAsync(token), token).WaitAndRethrow();
        }


        /// <summary>
        /// Asks the master which topics we advertise and are subscribed to, and removes them.
        /// If you are interested in the async version, make sure to set ensureCleanSlate to false in the constructor.
        /// </summary>
        public async Task EnsureCleanSlateAsync(CancellationToken token = default)
        {
            SystemState state = await GetSystemStateAsync(token);
            List<Task> tasks = new();
            tasks.AddRange(
                state.Subscribers
                    .Where(tuple => tuple.Members.Contains(CallerId))
                    .Select(tuple => RosMasterClient.UnregisterSubscriberAsync(tuple.Topic, token).AsTask())
            );

            tasks.AddRange(
                state.Publishers
                    .Where(tuple => tuple.Members.Contains(CallerId))
                    .Select(tuple => RosMasterClient.UnregisterPublisherAsync(tuple.Topic, token).AsTask())
            );

            try
            {
                await tasks.WhenAll();
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                throw new RosConnectionException($"Failed to contact the master URI '{MasterUri}'", e);
            }
        }

        public void CheckOwnUri(CancellationToken token = default)
        {
            Task.Run(() => CheckOwnUriAsync(token), token).WaitAndRethrow();
        }

        public async Task CheckOwnUriAsync(CancellationToken token = default)
        {
            RosNodeClient.GetPidResponse response;
            try
            {
                response = await CreateTalker(CallerUri).GetPidAsync(token);
            }
            catch (Exception e)
            {
                throw new RosUnreachableUriException($"The given own uri '{CallerUri}' is not reachable.", e);
            }

            static int GetProcessId()
            {
#if NET5_0
                return Environment.ProcessId;
#else
                return Process.GetCurrentProcess().Id;
#endif
            }


            if (!response.IsValid)
            {
                Logger.LogErrorFormat("{0}: Failed to validate reachability response.", this);
            }
            else if (response.Pid != GetProcessId())
            {
                throw new RosUnreachableUriException(
                    $"The given own uri '{CallerUri}' appears to belong to another node.");
            }
        }

        internal RosNodeClient CreateTalker(Uri otherUri)
        {
            return new(CallerId, CallerUri, otherUri, (int) RpcNodeTimeout.TotalMilliseconds);
        }

        (string id, RosSubscriber<T> subscriber)
            CreateSubscriber<T>(string topic, bool requestNoDelay, Action<T> firstCallback,
                IDeserializable<T> generator)
            where T : IMessage
        {
            TopicInfo<T> topicInfo = new(CallerId, topic, generator);
            int timeoutInMs = (int) TcpRosTimeout.TotalMilliseconds;

            RosSubscriber<T> subscription = new(this, topicInfo, requestNoDelay, timeoutInMs);
            string id = subscription.Subscribe(firstCallback);

            subscribersByTopic[topic] = subscription;

            var masterResponse = RosMasterClient.RegisterSubscriber(topic, topicInfo.Type);
            if (!masterResponse.IsValid)
            {
                subscribersByTopic.TryRemove(topic, out _);
                throw new RosRpcException(
                    $"Error registering publisher for topic {topic}: {masterResponse.StatusMessage}");
            }

            subscription.PublisherUpdateRcp(masterResponse.Publishers, default);
            return (id, subscription);
        }

        async ValueTask<(string id, RosSubscriber<T> subscriber)>
            CreateSubscriberAsync<T>(string topic, bool requestNoDelay, Action<T> firstCallback,
                IDeserializable<T> generator,
                CancellationToken token)
            where T : IMessage
        {
            TopicInfo<T> topicInfo = new(CallerId, topic, generator);
            int timeoutInMs = (int) TcpRosTimeout.TotalMilliseconds;

            RosSubscriber<T> subscription = new(this, topicInfo, requestNoDelay, timeoutInMs);

            string id = subscription.Subscribe(firstCallback);

            subscribersByTopic[topic] = subscription;

            RegisterSubscriberResponse masterResponse;
            try
            {
                masterResponse = await RosMasterClient.RegisterSubscriberAsync(topic, topicInfo.Type, token);
            }
            catch (Exception e)
            {
                subscribersByTopic.TryRemove(topic, out _);
                if (e is OperationCanceledException)
                {
                    throw;
                }

                throw new RosRpcException($"Error registering publisher for topic {topic}", e);
            }

            if (!masterResponse.IsValid)
            {
                subscribersByTopic.TryRemove(topic, out _);
                throw new RosRpcException(
                    $"Error registering publisher for topic {topic}: {masterResponse.StatusMessage}");
            }

            subscription.PublisherUpdateRcp(masterResponse.Publishers, token);
            return (id, subscription);
        }

        /// <summary>
        /// Subscribes to the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <returns>A token that can be used to unsubscribe from this topic.</returns>
        public string Subscribe<T>(string topic, Action<T> callback, bool requestNoDelay = true)
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
            bool requestNoDelay = true)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            string resolvedTopic = ResolveResourceName(topic);
            if (!TryGetSubscriber(resolvedTopic, out IRosSubscriber baseSubscriber))
            {
                string id;
                (id, subscriber) = CreateSubscriber(resolvedTopic, requestNoDelay, callback, new T());
                return id;
            }

            RosSubscriber<T>? newSubscriber = baseSubscriber as RosSubscriber<T>;
            subscriber = newSubscriber ?? throw new RosInvalidMessageTypeException(
                $"There is already a subscriber with a different type [{baseSubscriber.TopicType}]");
            return subscriber.Subscribe(callback);
        }

        string IRosClient.Subscribe<T>(string topic, Action<T> callback, out IRosSubscriber<T> subscriber,
            bool requestNoDelay)
        {
            string id = Subscribe(topic, callback, out RosSubscriber<T> newSubscriber, requestNoDelay);
            subscriber = newSubscriber;
            return id;
        }

        public string Subscribe(string topic, Action<IMessage> callback, out RosSubscriber<IMessage> subscriber,
            bool requestNoDelay = true)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            string resolvedTopic = ResolveResourceName(topic);
            if (!TryGetSubscriber(resolvedTopic, out IRosSubscriber baseSubscriber))
            {
                string id;
                (id, subscriber) = CreateSubscriber(resolvedTopic, requestNoDelay, callback, new DynamicMessage());
                return id;
            }

            RosSubscriber<IMessage>? newSubscriber = baseSubscriber as RosSubscriber<IMessage>;
            subscriber = newSubscriber ?? throw new RosInvalidMessageTypeException(
                $"There is already a subscriber with a different type [{baseSubscriber.TopicType}]");
            return subscriber.Subscribe(callback);
        }

        string IRosClient.Subscribe(string topic, Action<IMessage> callback, out IRosSubscriber subscriber, bool requestNoDelay)
        {
            string id = Subscribe(topic, callback, out RosSubscriber<IMessage> newSubscriber, requestNoDelay);
            subscriber = newSubscriber;
            return id;            
        }


        /// <summary>
        /// Generic version of the subscriber function if the type is not known. You should probably use the templated versions.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="msgType">The C# message type.</param>
        /// <param name="subscriber">
        /// The shared subscriber for this topic, used by all subscribers from this client.
        /// </param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <returns>A token that can be used to unsubscribe from this topic.</returns>
        public string Subscribe(string topic, Action<IMessage> callback, Type msgType, out IRosSubscriber subscriber,
            bool requestNoDelay = false)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (msgType == null)
            {
                throw new ArgumentNullException(nameof(msgType));
            }

            string resolvedTopic = ResolveResourceName(topic);
            if (!TryGetSubscriberImpl(resolvedTopic, out IRosSubscriber baseSubscriber))
            {
                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                MethodInfo? baseMethod = GetType().GetMethod(nameof(CreateSubscriber), flags);
                MethodInfo? method = baseMethod?.MakeGenericMethod(msgType);
                if (method == null)
                {
                    throw new RosInvalidMessageTypeException("Type is not a message object");
                }

                object? subscriberObj = method.Invoke(this, flags, null,
                    new object[] {resolvedTopic, requestNoDelay}, BuiltIns.Culture);

                subscriber = (IRosSubscriber?) subscriberObj ??
                             throw new RosInvalidMessageTypeException("Failed to call 'CreateSubscriber'!");
            }
            else
            {
                if (!baseSubscriber.MessageTypeMatches(msgType))
                {
                    throw new RosInvalidMessageTypeException(
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
        /// <param name="token">An optional cancellation token</param>
        /// <returns>A pair containing a token that can be used to unsubscribe from this topic, and the subscriber object.</returns>
        public ValueTask<(string id, RosSubscriber<T> subscriber)> SubscribeAsync<T>(
            string topic, Action<T> callback, bool requestNoDelay = true, CancellationToken token = default)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            string resolvedTopic = ResolveResourceName(topic);
            if (!TryGetSubscriberImpl(resolvedTopic, out IRosSubscriber baseSubscriber))
            {
                return CreateSubscriberAsync(resolvedTopic, requestNoDelay, callback, new T(), token);
            }

            var newSubscriber = baseSubscriber as RosSubscriber<T>;
            RosSubscriber<T> subscriber = newSubscriber ?? throw new RosInvalidMessageTypeException(
                $"Existing subscriber message type {baseSubscriber.TopicType} does not match the given type.");
            return new((subscriber.Subscribe(callback), subscriber));
        }

        async ValueTask<(string id, IRosSubscriber<T> subscriber)>
            IRosClient.SubscribeAsync<T>(string topic, Action<T> callback, bool requestNoDelay, CancellationToken token)
        {
            return await SubscribeAsync(topic, callback, requestNoDelay, token);
        }

        public ValueTask<(string id, RosSubscriber<IMessage> subscriber)> SubscribeAsync(
            string topic, Action<IMessage> callback, bool requestNoDelay = true, CancellationToken token = default)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            string resolvedTopic = ResolveResourceName(topic);
            if (!TryGetSubscriberImpl(resolvedTopic, out IRosSubscriber baseSubscriber))
            {
                return CreateSubscriberAsync(resolvedTopic, requestNoDelay, callback, new DynamicMessage(), token);
            }

            var newSubscriber = baseSubscriber as RosSubscriber<IMessage>;
            RosSubscriber<IMessage> subscriber = newSubscriber ?? throw new RosInvalidMessageTypeException(
                $"Existing subscriber message type {baseSubscriber.TopicType} does not match the given type.");
            return new((subscriber.Subscribe(callback), subscriber));
        }

        async ValueTask<(string id, IRosSubscriber subscriber)> IRosClient.SubscribeAsync(
            string topic, Action<IMessage> callback, bool requestNoDelay, CancellationToken token)
        {
            return await SubscribeAsync(topic, callback, requestNoDelay, token);
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
        public async ValueTask<bool> UnsubscribeAsync(string topicId)
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
            subscribersByTopic.TryRemove(subscriber.Topic, out _);
            RosMasterClient.UnregisterSubscriber(subscriber.Topic);
        }

        internal async Task RemoveSubscriberAsync(IRosSubscriber subscriber, CancellationToken token)
        {
            subscribersByTopic.TryRemove(subscriber.Topic, out _);
            await RosMasterClient.UnregisterSubscriberAsync(subscriber.Topic, token);
        }

        /// <summary>
        /// Tries to retrieve the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="subscriber">Subscriber for the given topic.</param>
        /// <returns>Whether the subscriber was found.</returns>
        public bool TryGetSubscriber(string topic, out IRosSubscriber subscriber)
        {
            string resolvedTopic = ResolveResourceName(topic);
            return TryGetSubscriberImpl(resolvedTopic, out subscriber);
        }

        bool TryGetSubscriberImpl(string resolvedTopic, out IRosSubscriber subscriber)
        {
            return subscribersByTopic.TryGetValue(resolvedTopic, out subscriber!);
        }

        /// <summary>
        /// Retrieves the subscriber for the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <returns></returns>
        public IRosSubscriber GetSubscriber(string topic)
        {
            if (TryGetSubscriber(topic, out IRosSubscriber subscriber))
            {
                return subscriber;
            }

            throw new KeyNotFoundException($"Cannot find subscriber for topic '{topic}'");
        }

        string ResolveResourceName(string name)
        {
            if (!IsValidResourceName(name))
            {
                throw new ArgumentException($"'{name}' is not a valid resource name");
            }

            return name[0] switch
            {
                '/' => name,
                '~' => $"{CallerId}/{name.Substring(1)}",
                _ => $"{namespacePrefix}{name}"
            };
        }

        RosPublisher<T> CreatePublisher<T>(string topic, DynamicMessage? generator = null) where T : IMessage
        {
            TopicInfo<T> topicInfo = generator == null
                ? new TopicInfo<T>(CallerId, topic)
                : new TopicInfo<T>(CallerId, topic, generator);

            RosPublisher<T> publisher = new(this, topicInfo)
                {TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds};

            publishersByTopic[topic] = publisher;

            RegisterPublisherResponse? response;
            try
            {
                response = RosMasterClient.RegisterPublisher(topic, topicInfo.Type);
            }
            catch (Exception e)
            {
                publishersByTopic.TryRemove(topic, out _);
                throw new RosRpcException("Error registering publisher", e);
            }

            if (response.IsValid)
            {
                return publisher;
            }

            publishersByTopic.TryRemove(topic, out _);
            throw new RosRpcException($"Error registering publisher: {response.StatusMessage}");
        }

        async ValueTask<IRosPublisher> CreatePublisherAsync<T>(string topic, CancellationToken token,
            DynamicMessage? generator = null) where T : IMessage
        {
            TopicInfo<T> topicInfo = generator == null
                ? new TopicInfo<T>(CallerId, topic)
                : new TopicInfo<T>(CallerId, topic, generator);

            RosPublisher<T> publisher = new(this, topicInfo)
                {TimeoutInMs = (int) TcpRosTimeout.TotalMilliseconds};

            publishersByTopic[topic] = publisher;

            RegisterPublisherResponse? response;
            try
            {
                response = await RosMasterClient.RegisterPublisherAsync(topic, topicInfo.Type, token);
            }
            catch (Exception e)
            {
                publishersByTopic.TryRemove(topic, out _);
                if (e is OperationCanceledException)
                {
                    throw;
                }

                throw new RosRpcException("Error registering publisher", e);
            }

            if (response.IsValid)
            {
                return publisher;
            }

            publishersByTopic.TryRemove(topic, out _);
            throw new RosRpcException($"Error registering publisher: {response.StatusMessage}");
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
            string resolvedTopic = ResolveResourceName(topic);

            if (!TryGetPublisher(resolvedTopic, out IRosPublisher basePublisher))
            {
                publisher = CreatePublisher<T>(resolvedTopic);
            }
            else
            {
                var newPublisher = basePublisher as RosPublisher<T>;
                publisher = newPublisher ?? throw new RosInvalidMessageTypeException(
                    $"There is already an advertiser with a different type [{basePublisher.TopicType}]");
            }

            return publisher.Advertise();
        }

        public string Advertise(string topic, DynamicMessage generator, out RosPublisher<DynamicMessage> publisher)
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (!generator.IsInitialized)
            {
                throw new InvalidOperationException("Generator has not been initialized");
            }

            string resolvedTopic = ResolveResourceName(topic);

            if (!TryGetPublisher(resolvedTopic, out IRosPublisher basePublisher))
            {
                publisher = CreatePublisher<DynamicMessage>(resolvedTopic, generator);
            }
            else
            {
                var newPublisher = basePublisher as RosPublisher<DynamicMessage>;
                publisher = newPublisher ?? throw new RosInvalidMessageTypeException(
                    $"There is already an advertiser with a different type [{basePublisher.TopicType}]");
            }

            return publisher.Advertise();
        }

        string IRosClient.Advertise<T>(string topic, out IRosPublisher<T> publisher)
        {
            string id = Advertise<T>(topic, out var newPublisher);
            publisher = newPublisher;
            return id;
        }

        string IRosClient.Advertise(string topic, DynamicMessage generator, out IRosPublisher<DynamicMessage> publisher)
        {
            string id = Advertise(topic, generator, out var newPublisher);
            publisher = newPublisher;
            return id;
        }

        /// <summary>
        /// Advertises the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>A pair containing a token that can be used to unadvertise from this publisher, and the publisher object.</returns>
        public async ValueTask<(string id, RosPublisher<T> publisher)> AdvertiseAsync<T>(string topic,
            CancellationToken token = default) where T : IMessage
        {
            string resolvedTopic = ResolveResourceName(topic);

            RosPublisher<T> publisher;
            if (!TryGetPublisher(topic, out IRosPublisher basePublisher))
            {
                publisher = (RosPublisher<T>) await CreatePublisherAsync<T>(resolvedTopic, token);
            }
            else
            {
                var newPublisher = basePublisher as RosPublisher<T>;
                publisher = newPublisher ?? throw new RosInvalidMessageTypeException(
                    $"Existing subscriber message type {basePublisher.TopicType} does not match the given type.");
            }

            return (publisher.Advertise(), publisher);
        }

        public async ValueTask<(string id, RosPublisher<DynamicMessage> publisher)> AdvertiseAsync(string topic,
            DynamicMessage generator, CancellationToken token = default)
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (!generator.IsInitialized)
            {
                throw new InvalidOperationException("Generator has not been initialized");
            }

            string resolvedTopic = ResolveResourceName(topic);

            RosPublisher<DynamicMessage> publisher;
            if (!TryGetPublisher(topic, out IRosPublisher basePublisher))
            {
                publisher = (RosPublisher<DynamicMessage>)
                    await CreatePublisherAsync<DynamicMessage>(resolvedTopic, token, generator);
            }
            else
            {
                var newPublisher = basePublisher as RosPublisher<DynamicMessage>;
                publisher = newPublisher ?? throw new RosInvalidMessageTypeException(
                    $"Existing subscriber message type {basePublisher.TopicType} does not match the given type.");
            }

            return (publisher.Advertise(), publisher);
        }

        async ValueTask<(string id, IRosPublisher<T> publisher)> IRosClient.AdvertiseAsync<T>(string topic,
            CancellationToken token)
        {
            return await AdvertiseAsync<T>(topic, token);
        }

        async ValueTask<(string id, IRosPublisher<DynamicMessage> publisher)> IRosClient.AdvertiseAsync(string topic,
            DynamicMessage generator, CancellationToken token)
        {
            return await AdvertiseAsync(topic, generator, token);
        }

        string IRosClient.Advertise(string topic, Type msgType, out IRosPublisher publisher)
        {
            string resolvedTopic = ResolveResourceName(topic);

            if (msgType is null)
            {
                throw new ArgumentNullException(nameof(msgType));
            }

            if (!TryGetPublisher(resolvedTopic, out IRosPublisher basePublisher))
            {
                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                MethodInfo? baseMethod = GetType().GetMethod(nameof(CreatePublisher), flags);
                MethodInfo? method = baseMethod?.MakeGenericMethod(msgType);
                if (method == null)
                {
                    throw new RosInvalidMessageTypeException($"Type {msgType} is not a message object");
                }

                object? publisherObj = method.Invoke(this, flags, null, new object[] {resolvedTopic}, BuiltIns.Culture);
                publisher = (IRosPublisher?) publisherObj ??
                            throw new RosInvalidMessageTypeException("Failed to call 'CreatePublisher'!");
            }
            else
            {
                if (!basePublisher.MessageTypeMatches(msgType))
                {
                    throw new RosInvalidMessageTypeException(
                        $"Type {msgType} does not match existing publisher type {basePublisher.TopicType}.");
                }

                publisher = basePublisher;
            }

            return publisher.Advertise();
        }

        async ValueTask<(string id, IRosPublisher publisher)> IRosClient.AdvertiseAsync(string topic, Type msgType,
            CancellationToken token)
        {
            string resolvedTopic = ResolveResourceName(topic);

            if (msgType is null)
            {
                throw new ArgumentNullException(nameof(msgType));
            }

            IRosPublisher publisher;
            if (!TryGetPublisher(resolvedTopic, out IRosPublisher basePublisher))
            {
                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                MethodInfo? baseMethod = GetType().GetMethod(nameof(CreatePublisherAsync), flags);
                MethodInfo? method = baseMethod?.MakeGenericMethod(msgType);
                if (method == null)
                {
                    throw new RosInvalidMessageTypeException($"Type {msgType} is not a message object");
                }

                object? result = method.Invoke(this, flags, null, new object[] {resolvedTopic, token},
                    BuiltIns.Culture);
                if (result == null)
                {
                    throw new RosInvalidMessageTypeException("Failed to call 'CreatePublisherAsync'!");
                }

                ValueTask<IRosPublisher> task = (ValueTask<IRosPublisher>) result;
                publisher = await task;
            }
            else
            {
                if (!basePublisher.MessageTypeMatches(msgType))
                {
                    throw new RosInvalidMessageTypeException(
                        $"Type {msgType} does not match existing publisher type {basePublisher.TopicType}.");
                }

                publisher = basePublisher;
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
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            IRosPublisher? publisher = publishersByTopic.Values.FirstOrDefault(p => p.ContainsId(topicId));

            return publisher != null && publisher.Unadvertise(topicId);
        }

        /// <summary>
        /// Unadvertise the given topic.
        /// </summary>
        /// <param name="topicId">Token returned by Advertise().</param>
        /// <returns>Whether the unadvertisement succeeded.</returns>
        public async ValueTask<bool> UnadvertiseAsync(string topicId)
        {
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            IRosPublisher? publisher =
                publishersByTopic.Values.FirstOrDefault(tmpPublisher => tmpPublisher.ContainsId(topicId));

            return publisher != null && await publisher.UnadvertiseAsync(topicId);
        }

        internal async Task RemovePublisherAsync(IRosPublisher publisher, CancellationToken token)
        {
            publishersByTopic.TryRemove(publisher.Topic, out _);
            await RosMasterClient.UnregisterPublisherAsync(publisher.Topic, token);
        }

        /// <summary>
        /// Tries to retrieve the publisher of the given topic.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="publisher">Publisher of the given topic.</param>
        /// <returns>Whether the publisher was found.</returns>
        public bool TryGetPublisher(string topic, out IRosPublisher publisher)
        {
            string resolvedTopic = ResolveResourceName(topic);
            return publishersByTopic.TryGetValue(resolvedTopic, out publisher!);
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
        /// Asks the master for all the published topics in the system with at least one publisher.
        /// Corresponds to the function 'getPublishedTopics' in the ROS Master API.
        /// </summary>
        /// <returns>List of topic names and message types.</returns>
        public ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics()
        {
            var response = RosMasterClient.GetPublishedTopics();
            if (!response.IsValid)
            {
                throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
            }

            return response.Topics
                .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
                .ToArray().AsReadOnly();
        }

        /// <summary>
        /// Asks the master for all the published topics in the system with at least one publisher.
        /// Corresponds to the function 'getPublishedTopics' in the ROS Master API.
        /// </summary>
        /// <returns>List of topic names and message types.</returns>
        public async ValueTask<ReadOnlyCollection<BriefTopicInfo>> GetSystemPublishedTopicsAsync(
            CancellationToken token = default)
        {
            var response = await RosMasterClient.GetPublishedTopicsAsync(token: token);
            if (!response.IsValid)
            {
                throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
            }

            return response.Topics
                .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
                .ToArray().AsReadOnly();
        }

        /// <summary>
        /// Asks the master for all the topics in the system.
        /// Corresponds to the function 'getTopicTypes' in the ROS Master API.
        /// </summary>
        /// <returns>List of topic names and message types.</returns>
        public ReadOnlyCollection<BriefTopicInfo> GetSystemTopicTypes()
        {
            var response = RosMasterClient.GetTopicTypes();
            if (response.IsValid)
            {
                return response.Topics
                    .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
                    .ToArray().AsReadOnly();
            }

            throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
        }

        /// <summary>
        /// Asks the master for all the topics in the system.
        /// Corresponds to the function 'getTopicTypes' in the ROS Master API.
        /// </summary>
        /// <returns>List of topic names and message types.</returns>
        public async ValueTask<ReadOnlyCollection<BriefTopicInfo>> GetSystemTopicTypesAsync(
            CancellationToken token = default)
        {
            var response = await RosMasterClient.GetTopicTypesAsync(token: token);
            if (!response.IsValid)
            {
                throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
            }

            return response.Topics
                .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
                .ToArray().AsReadOnly();
        }


        /// <summary>
        /// Gets the topics published by this node.
        /// </summary>
        public ReadOnlyCollection<BriefTopicInfo> SubscribedTopics => GetSubscriptionsRpc().AsReadOnly();

        /// <summary>
        /// Asks the master for the nodes and topics in the system.
        /// Corresponds to the function 'getSystemState' in the ROS Master API.
        /// </summary>
        /// <returns>List of advertised topics, subscribed topics, and offered services, together with the involved nodes.</returns>
        public SystemState GetSystemState()
        {
            var response = RosMasterClient.GetSystemState();
            if (!response.IsValid)
            {
                throw new RosRpcException($"Failed to retrieve system state: {response.StatusMessage}");
            }

            return new SystemState(response.Publishers, response.Subscribers, response.Services);
        }

        /// <summary>
        /// Asks the master for the nodes and topics in the system.
        /// Corresponds to the function 'getSystemState' in the ROS Master API.
        /// </summary>
        /// <returns>List of advertised topics, subscribed topics, and offered services, together with the involved nodes.</returns>
        public async ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default)
        {
            var response = await RosMasterClient.GetSystemStateAsync(token);
            if (!response.IsValid)
            {
                throw new RosRpcException($"Failed to retrieve system state: {response.StatusMessage}");
            }

            return new SystemState(response.Publishers, response.Subscribers, response.Services);
        }

        /// <summary>
        /// Gets the topics published by this node.
        /// </summary>
        public ReadOnlyCollection<BriefTopicInfo> PublishedTopics => GetPublicationsRpc().AsReadOnly();

        internal BriefTopicInfo[] GetSubscriptionsRpc()
        {
            return subscribersByTopic.Values
                .Select(subscriber => new BriefTopicInfo(subscriber.Topic, subscriber.TopicType))
                .ToArray();
        }

        internal BriefTopicInfo[] GetPublicationsRpc()
        {
            return publishersByTopic.Values
                .Select(publisher => new BriefTopicInfo(publisher.Topic, publisher.TopicType))
                .ToArray();
        }

        internal async Task PublisherUpdateRpcAsync(string topic, IEnumerable<Uri> publishers, CancellationToken token)
        {
            if (!TryGetSubscriber(topic, out IRosSubscriber subscriber))
            {
                Logger.LogDebugFormat("** {0}: PublisherUpdate called for nonexisting topic '{1}'", this, topic);
                return;
            }

            try
            {
                await subscriber.PublisherUpdateRpcAsync(publishers, token);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                Logger.LogErrorFormat("EE {0}: PublisherUpdateRcp failed: {1}", this, e);
            }
        }

        internal Endpoint? RequestTopicRpc(string remoteCallerId, string topic)
        {
            if (!TryGetPublisher(topic, out IRosPublisher publisher))
            {
                Logger.LogDebugFormat("{0}: '{1} is requesting topic '{2}' but we don't publish it", this,
                    remoteCallerId, topic);
                return null;
            }

            return publisher.RequestTopicRpc();
        }

        /// <summary>
        /// Close this connection. Unsubscribes and unadvertises all topics.
        /// </summary>
        public void Close(CancellationToken token = default)
        {
            Task.Run(() => CloseAsync(token), token).WaitNoThrow(this);
        }

        /// <summary>
        /// Close this connection. Unsubscribes and unadvertises all topics.
        /// </summary>
        public async Task CloseAsync(CancellationToken token = default)
        {
            const int timeoutInMs = 4000;
            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            tokenSource.CancelAfter(timeoutInMs);
            var innerToken = tokenSource.Token;

            List<Task> tasks = new();

            if (listener != null)
            {
                Task listenerDispose = listener.DisposeAsync();
                tasks.Add(listenerDispose);
            }

            var publishers = publishersByTopic.Values.ToArray();
            publishersByTopic.Clear();

            EnumeratorUtils.AddRange(tasks, publishers.Select(async publisher =>
            {
                await publisher.DisposeAsync(innerToken).AwaitNoThrow(this);
                await RosMasterClient.UnregisterPublisherAsync(publisher.Topic, innerToken).AwaitNoThrow(this);
            }));

            var subscribers = subscribersByTopic.Values.ToArray();
            subscribersByTopic.Clear();

            EnumeratorUtils.AddRange(tasks, subscribers.Select(async subscriber =>
            {
                await subscriber.DisposeAsync(innerToken).AwaitNoThrow(this);
                await RosMasterClient.UnregisterSubscriberAsync(subscriber.Topic, innerToken).AwaitNoThrow(this);
            }));

            IServiceCaller[] receivers = subscribedServicesByName.Values.ToArray();
            subscribedServicesByName.Clear();

            foreach (IServiceCaller receiver in receivers)
            {
                receiver.Dispose();
            }

            IServiceRequestManager[] serviceManagers = advertisedServicesByName.Values.ToArray();
            advertisedServicesByName.Clear();

            EnumeratorUtils.AddRange(tasks, serviceManagers.Select(async senderManager =>
            {
                await senderManager.DisposeAsync(innerToken).AwaitNoThrow(this);
                await RosMasterClient
                    .UnregisterServiceAsync(senderManager.Service, senderManager.Uri, innerToken)
                    .AwaitNoThrow(this);
            }));

            Task timeoutTask = Task.Delay(timeoutInMs, innerToken);
            Task finalTask = await (tasks.WhenAll(), timeoutTask).WhenAny();
            if (finalTask == timeoutTask)
            {
                Logger.LogErrorFormat("EE {0}: Close() tasks timed out.", this);
            }

            RosMasterClient.Dispose();
        }

        public SubscriberState GetSubscriberStatistics()
        {
            return new(subscribersByTopic.Values.Select(subscriber => subscriber.GetState()).ToArray());
        }

        public PublisherState GetPublisherStatistics()
        {
            return new(publishersByTopic.Values.Select(publisher => publisher.GetState()).ToArray());
        }

        internal List<BusInfo> GetBusInfoRpc()
        {
            List<BusInfo> busInfos = new();

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
                PublisherState publisherState = GetPublisherStatistics();
                foreach (var topic in publisherState.Topics)
                {
                    foreach (var sender in topic.Senders)
                    {
                        LookupNodeResponse response;
                        try
                        {
                            response = RosMasterClient.LookupNode(sender.RemoteId);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebugFormat("{0}: LookupNode for {1} failed: {2}", this, sender.RemoteId, e);
                            continue;
                        }

                        if (!response.IsValid)
                        {
                            continue;
                        }

                        BusInfo busInfo = new(busInfos.Count, response.Uri, BusInfo.DirectionType.Out,
                            topic.Topic, sender.IsAlive);
                        busInfos.Add(busInfo);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("EE {0}: GetBusInfoRcp failed: {1}", this, e);
            }

            return busInfos;
        }

        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <param name="persistent">Whether a persistent connection with the provider should be maintained.</param>
        /// <param name="timeoutInMs">Maximal time to wait.</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <returns>Whether the call succeeded.</returns>
        /// <exception cref="TaskCanceledException">The operation timed out.</exception>
        public T CallService<T>(string serviceName, T service, bool persistent, int timeoutInMs)
            where T : IService
        {
            using CancellationTokenSource timeoutTs = new(timeoutInMs);
            return CallService(serviceName, service, persistent, timeoutTs.Token);
        }

        public TU CallService<TT, TU>(string serviceName, IRequest<TT, TU> request,
            bool persistent = false, CancellationToken token = default)
            where TT : IService, new() where TU : IResponse
        {
            TT service = new() {Request = request};
            CallService(serviceName, service, persistent, token);
            return (TU) service.Response;
        }

        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <param name="persistent">Whether a persistent connection with the provider should be maintained.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <returns>Whether the call succeeded.</returns>
        /// <exception cref="TaskCanceledException">Thrown if the operation timed out.</exception>
        /// <exception cref="RosServiceCallFailed">Thrown if the server could not process the call.</exception>
        public T CallService<T>(string serviceName, T service, bool persistent = false,
            CancellationToken token = default)
            where T : IService
        {
            return Task.Run(() => CallServiceAsync(serviceName, service, persistent, token).AsTask(), token)
                .WaitAndRethrow();
        }

        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <param name="persistent">Whether a persistent connection with the provider should be maintained.</param>
        /// <param name="timeoutInMs">Maximal time to wait.</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <exception cref="TaskCanceledException">Thrown if the timeout expired.</exception>
        /// <exception cref="RosServiceCallFailed">Thrown if the server could not process the call.</exception>
        public async ValueTask<T> CallServiceAsync<T>(string serviceName, T service, bool persistent,
            int timeoutInMs) where T : IService
        {
            using CancellationTokenSource timeoutTs = new(timeoutInMs);
            try
            {
                return await CallServiceAsync(serviceName, service, persistent, timeoutTs.Token);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException($"Call to '{serviceName}' timed out");
            }
        }

        public async ValueTask<TU> CallServiceAsync<TT, TU>(string serviceName, IRequest<TT, TU> request,
            bool persistent = false, CancellationToken token = default)
            where TT : IService, new() where TU : IResponse
        {
            TT service = new() {Request = request};
            await CallServiceAsync(serviceName, service, persistent, token);
            return (TU) service.Response;
        }


        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <param name="persistent">
        /// Whether a persistent connection with the provider should be maintained.
        /// The connection will be stopped if any exception is thrown or the token is canceled.
        /// </param>
        /// <param name="token">A cancellation token</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <returns>Whether the call succeeded.</returns>
        /// <exception cref="TaskCanceledException">Thrown if the token expired.</exception>
        /// <exception cref="RosServiceCallFailed">Thrown if the server could not process the call.</exception>
        public async ValueTask<T> CallServiceAsync<T>(string serviceName, T service, bool persistent = false,
            CancellationToken token = default)
            where T : IService
        {
            string resolvedServiceName = ResolveResourceName(serviceName);

            if (persistent && subscribedServicesByName.TryGetValue(resolvedServiceName, out var baseExistingReceiver))
            {
                if (!(baseExistingReceiver is ServiceCallerAsync<T> existingReceiver))
                {
                    throw new RosInvalidMessageTypeException(
                        $"Existing connection of {resolvedServiceName} with service type {baseExistingReceiver.ServiceType} " +
                        "does not match the new given type.");
                }

                if (!existingReceiver.IsAlive)
                {
                    existingReceiver.Dispose();
                    subscribedServicesByName.TryRemove(resolvedServiceName, out _);
                    // continue below
                }
                else
                {
                    // is there a persistent connection? use it
                    try
                    {
                        await existingReceiver.ExecuteAsync(service, token);
                        return service;
                    }
                    catch (Exception e)
                    {
                        existingReceiver.Dispose();
                        subscribedServicesByName.TryRemove(resolvedServiceName, out _);
                        ThrowExceptionHelper(e, resolvedServiceName, existingReceiver.RemoteUri);
                        throw; // unreachable
                    }
                }
            }

            LookupServiceResponse response = await RosMasterClient.LookupServiceAsync(resolvedServiceName, token);
            if (!response.IsValid)
            {
                throw new RosServiceNotFoundException(resolvedServiceName, response.StatusMessage);
            }

            Uri serviceUri = response.ServiceUrl!;
            ServiceInfo<T> serviceInfo = new(CallerId, resolvedServiceName);
            if (persistent)
            {
                ServiceCallerAsync<T> serviceCaller = new(serviceInfo);
                try
                {
                    subscribedServicesByName.TryAdd(resolvedServiceName, serviceCaller);
                    await serviceCaller.StartAsync(serviceUri, persistent, token);
                    await serviceCaller.ExecuteAsync(service, token);
                    return service;
                }
                catch (Exception e)
                {
                    serviceCaller.Dispose();
                    subscribedServicesByName.TryRemove(resolvedServiceName, out _);
                    ThrowExceptionHelper(e, resolvedServiceName, serviceUri);
                    throw; // unreachable
                }
            }

            try
            {
                using var serviceCaller = new ServiceCallerAsync<T>(serviceInfo);
                await serviceCaller.StartAsync(serviceUri, persistent, token);
                await serviceCaller.ExecuteAsync(service, token);
                return service;
            }
            catch (Exception e) when (!(e is OperationCanceledException || e is RosServiceCallFailed))
            {
                throw new RoslibException($"Service call '{resolvedServiceName}' to {serviceUri} failed", e);
            }
        }

        static void ThrowExceptionHelper(Exception e, string name, Uri? uri)
        {
            if (e is OperationCanceledException || e is RosServiceCallFailed)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
            }

            throw new RoslibException($"Service call '{name}' to {uri?.ToString() ?? "[unknown]"} failed", e);
        }

        bool ServiceAlreadyAdvertised<T>(string serviceName) where T : IService
        {
            if (!advertisedServicesByName.TryGetValue(serviceName, out var existingSender))
            {
                return false;
            }

            if (!(existingSender is ServiceRequestManager<T>))
            {
                throw new RosInvalidMessageTypeException(
                    $"Existing advertised service type {existingSender.ServiceType} for {serviceName} does not match the given type.");
            }

            return true;
        }

        /// <summary>
        /// Advertises the given service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service.</param>
        /// <param name="callback">Function to be called when a service request arrives. The response should be written in the response field.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>Whether the advertisement was new. If false, the service already existed, but can still be used.</returns>
        /// <typeparam name="T">Service type.</typeparam>
        public bool AdvertiseService<T>(string serviceName, Action<T> callback, CancellationToken token = default)
            where T : IService, new()
        {
            return Task.Run(() => AdvertiseServiceAsync(serviceName, callback, token).AsTask(), token).WaitAndRethrow();
        }

        public ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Action<T> callback,
            CancellationToken token = default)
            where T : IService, new()
        {
            Task Wrapper(T x)
            {
                callback(x);
                return Task.CompletedTask;
            }

            return AdvertiseServiceAsync<T>(serviceName, Wrapper, token);
        }

        /// <summary>
        /// Advertises the given service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service.</param>
        /// <param name="callback">Function to be called when a service request arrives. The response should be written in the response field.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <typeparam name="T">Service type.</typeparam>
        public async ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, Task> callback,
            CancellationToken token = default)
            where T : IService, new()
        {
            string resolvedServiceName = ResolveResourceName(serviceName);

            if (ServiceAlreadyAdvertised<T>(resolvedServiceName))
            {
                return false;
            }

            ServiceInfo<T> serviceInfo = new(CallerId, resolvedServiceName, new T());

            var advertisedService = new ServiceRequestManager<T>(serviceInfo, CallerUri.Host, callback);

            advertisedServicesByName.TryAdd(resolvedServiceName, advertisedService);

            try
            {
                await RosMasterClient.RegisterServiceAsync(resolvedServiceName, advertisedService.Uri, token);
            }
            catch (Exception e)
            {
                advertisedServicesByName.TryRemove(resolvedServiceName, out _);
                throw new RosRpcException($"Failed to advertise service '{serviceName}'", e);
            }

            return true;
        }

        /// <summary>
        /// Unadvertises the service.
        /// </summary>
        /// <param name="name">Name of the service</param>
        /// <exception cref="ArgumentException">Thrown if name is null</exception>
        public bool UnadvertiseService(string name, CancellationToken token = default)
        {
            return Task.Run(() => UnadvertiseServiceAsync(name, token).AsTask(), token).WaitAndRethrow();
        }

        /// <summary>
        /// Unadvertises the service.
        /// </summary>
        /// <param name="name">Name of the service</param>
        /// <param name="token">An optional cancellation token</param>
        /// <exception cref="ArgumentException">Thrown if name is null</exception>        
        public async ValueTask<bool> UnadvertiseServiceAsync(string name, CancellationToken token = default)
        {
            string resolvedServiceName = ResolveResourceName(name);

            if (!advertisedServicesByName.TryGetValue(resolvedServiceName, out var advertisedService))
            {
                return false;
            }

            advertisedServicesByName.TryRemove(resolvedServiceName, out _);

            await advertisedService.DisposeAsync(token);
            await RosMasterClient.UnregisterServiceAsync(resolvedServiceName, advertisedService.Uri, token);
            return true;
        }

        public void Dispose()
        {
            Close();
        }

        public Task DisposeAsync()
        {
            return CloseAsync();
        }

#if !NETSTANDARD2_0
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await DisposeAsync();
        }
#endif

        public override string ToString()
        {
            return $"[RosClient MyUri='{CallerUri}' MyId='{CallerId}' MasterUri='{MasterUri}']";
        }
    }
}