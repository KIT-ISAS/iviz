using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.Roslib;

/// <summary>
/// Class that manages a client connection to a ROS master.
/// <example>
/// This initializes a ROS master:
/// <code>
///     string masterUri = "http://localhost:11311";
///     string ownId = "my_ros_id";
///     string ownUri = "http://localhost:7615";
///     var client = new RosClient(masterUri, ownId, ownUri);
/// </code>
/// </example>
/// </summary>
public sealed class RosClient : IRosClient
{
    public const int AnyPort = 0;

    readonly ConcurrentDictionary<string, IRosSubscriber> subscribersByTopic = new();
    readonly ConcurrentDictionary<string, IRosPublisher> publishersByTopic = new();
    readonly ConcurrentDictionary<string, ServiceCaller> subscribedServicesByName = new();
    readonly ConcurrentDictionary<string, ServiceRequestManager> publishedServicesByName = new();
    readonly string namespacePrefix;

    RosNodeServer? listener;
    TimeSpan tcpRosTimeout = TimeSpan.FromSeconds(3);
    TimeSpan rpcNodeTimeout = TimeSpan.FromSeconds(3);

    public delegate void ShutdownActionCall(string callerId, string reason);

    /// <summary>
    /// Handler of 'shutdown' XML-RPC calls from the slave API
    /// </summary>
    public ShutdownActionCall? ShutdownAction { get; set; }

    public delegate void ParamUpdateActionCall(string callerId, string parameterKey, XmlRpcValue parameterValue);

    /// <summary>
    /// Handler of 'paramUpdate' XML-RPC calls from the slave API
    /// </summary>
    public ParamUpdateActionCall? ParamUpdateAction { get; set; }

    /// <summary>
    /// Own ID of this node.
    /// </summary>
    public string CallerId { get; }

    /// <summary>
    /// Wrapper that implements and manages XML-RPC queries to the master.
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
                BuiltIns.ThrowArgumentNull(nameof(value));
            }

            RosMasterClient.TimeoutInMs = (int)value.TotalMilliseconds;
        }
    }

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
                BuiltIns.ThrowArgumentNull(nameof(value));
            }

            rpcNodeTimeout = value;
        }
    }

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
                BuiltIns.ThrowArgumentNull(nameof(value));
            }

            tcpRosTimeout = value;
            int valueInMs = (int)value.TotalMilliseconds;
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
    /// Own URI of this node.
    /// </summary>
    public Uri CallerUri { get; private set; }

    RosClient(Uri? masterUri, string? ownId, Uri? ownUri, string? namespaceOverride)
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

        ownUri ??= TryGetCallerUriFor(masterUri) ?? TryGetCallerUri();

        if (ownUri.Scheme != "http")
        {
            throw new ArgumentException("URI scheme must be http", nameof(ownUri));
        }

        if (string.IsNullOrWhiteSpace(ownId))
        {
            ownId = CreateCallerId();
        }

        string? ns = namespaceOverride ?? EnvironmentRosNamespace;
        namespacePrefix = ns == null ? "/" : $"/{ns}/";

        if (ownId![0] != '/')
        {
            ownId = $"{namespacePrefix}{ownId}";
        }

        if (ownId[0] == '~')
        {
            throw new RosInvalidResourceName("ROS node names may not start with a '~'");
        }

        ValidateResourceName(ownId);

        CallerId = ownId;
        CallerUri = ownUri;

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
    /// <param name="ownId">
    /// The ROS name of this node.
    /// This is your identity in the network, and must be unique. Example: /my_new_node
    /// Leave empty to generate one automatically.
    /// </param>
    /// <param name="ownUri">
    /// URI of this node.
    /// Other clients will use this address to connect to this node.
    /// Leave empty to generate one automatically. </param>
    /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
    /// <param name="namespaceOverride">Set this to override ROS_NAMESPACE.</param>
    public RosClient(Uri? masterUri = null, string? ownId = null, Uri? ownUri = null,
        bool ensureCleanSlate = true, string? namespaceOverride = null) :
        this(masterUri, ownId, ownUri, namespaceOverride)
    {
        try
        {
            // Do a simple ping to the master. This will tell us whether the master is reachable.
            // (there is nothing special about GetUri, it's just a cheap call)
            RosMasterClient.GetUri();
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            throw new RosConnectionException($"Failed to contact the master URI at '{masterUri}'", e);
        }

        try
        {
            // Create an XmlRpc server. This will tell us quickly whether the port is taken.
            listener = new RosNodeServer(this);
        }
        catch (SocketException e)
        {
            throw new RosUriBindingException($"Failed to bind to local URI '{ownUri}'", e);
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


        Logger.LogDebugFormat("{0}: Initialized.", this);

        if (!ensureCleanSlate)
        {
            return;
        }

        try
        {
            EnsureCleanSlate();
        }
        catch
        {
            Logger.LogDebugFormat("{0}: EnsureCleanState failed.", this);
            listener.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Constructs and connects a ROS client in async.
    /// </summary>
    /// <param name="masterUri">
    /// URI to the master node. Example: new Uri("http://localhost:11311").
    /// </param>
    /// <param name="ownId">
    /// The ROS name of this node.
    /// This is your identity in the network, and must be unique. Example: /my_new_node
    /// Leave empty to generate one automatically.
    /// </param>
    /// <param name="ownUri">
    /// URI of this node. 
    /// Other clients will use this address to connect to this node.
    /// Leave empty to generate one automatically.
    /// </param>
    /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
    /// <param name="namespaceOverride">Set this to override ROS_NAMESPACE.</param>
    /// <param name="token">An optional cancellation token.</param>        
    public static async ValueTask<RosClient> CreateAsync(Uri? masterUri = null, string? ownId = null,
        Uri? ownUri = null, bool ensureCleanSlate = true, string? namespaceOverride = null,
        CancellationToken token = default)
    {
        RosClient client = new(masterUri, ownId, ownUri, namespaceOverride);

        try
        {
            // Do a simple ping to the master. This will tell us whether the master is reachable.
            // (there is nothing special about GetUri, it's just a cheap call)
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
            throw new RosUriBindingException($"Failed to bind to local URI '{ownUri}'", e);
        }

        client.listener.Start();

        if (client.CallerUri.Port == AnyPort || client.CallerUri.IsDefaultPort)
        {
            string absolutePath = Uri.UnescapeDataString(client.CallerUri.AbsolutePath);
            client.CallerUri =
                new Uri($"http://{client.CallerUri.Host}:{client.listener.ListenerPort.ToString()}{absolutePath}");

            // own uri has changed;
            client.RosMasterClient = new RosMasterClient(client.MasterUri, client.CallerId, client.CallerUri);
            client.Parameters = new ParameterClient(client.RosMasterClient);
        }

        Logger.LogDebugFormat("{0}: Initialized.", client);

        if (!ensureCleanSlate)
        {
            return client;
        }

        try
        {
            await client.EnsureCleanSlateAsync(token);
        }
        catch
        {
            Logger.LogDebugFormat("{0}: EnsureCleanState failed.", client);
            await client.listener.DisposeAsync();
            throw;
        }

        return client;
    }

    /// <summary>
    /// Constructs and connects a ROS client. Same as calling new() directly.
    /// This constructor exists only to have a sync equivalent to <see cref="CreateAsync"/> (which you should use in async contexts).
    /// </summary>
    /// <param name="masterUri">
    /// URI to the master node. Example: new Uri("http://localhost:11311").
    /// </param>
    /// <param name="ownId">
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
    public static RosClient Create(Uri? masterUri = null, string? ownId = null,
        Uri? callerUri = null, bool ensureCleanSlate = true, string? namespaceOverride = null) =>
        new RosClient(masterUri, ownId, callerUri, ensureCleanSlate, namespaceOverride);

    /// <summary>
    /// Constructs and connects a ROS client.
    /// </summary>
    /// <param name="masterUri">
    /// URI to the master node. Example: http://localhost:11311.
    /// </param>
    /// <param name="ownId">
    /// The ROS name of this node.
    /// This is your identity in the network, and must be unique. Example: /my_new_node
    /// Leave empty to generate one automatically.
    /// </param>
    /// <param name="ownUri">
    /// URI of this node.
    /// Other clients will use this address to connect to this node.
    /// Leave empty to generate one automatically. </param>
    /// <param name="ensureCleanSlate">Checks if masterUri has any previous subscriptions or advertisements, and unregisters them.</param>
    /// <param name="namespaceOverride">Set this to override ROS_NAMESPACE.</param>
    public RosClient(string? masterUri,
        string? ownId = null,
        string? ownUri = null,
        bool ensureCleanSlate = true,
        string? namespaceOverride = null) :
        this(TryToCreateUri(masterUri, true), ownId, TryToCreateUri(ownUri, false), ensureCleanSlate,
            namespaceOverride)
    {
    }

    public static RosClient Create(string? masterUri, string? ownId = null,
        string? ownUri = null, bool ensureCleanSlate = true, string? namespaceOverride = null) =>
        new RosClient(masterUri, ownId, ownUri, ensureCleanSlate, namespaceOverride);

    static Uri? TryToCreateUri(string? uri, bool isMaster)
    {
        if (uri == null)
        {
            return null;
        }

        if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri? validatedUri))
        {
            throw new ArgumentException("Argument '" + uri + "' cannot be parsed as an URI. " + (isMaster
                ? "Try something like http://localhost:11311"
                : "Try something like http://your_ip:1024, or http://your_ip:0 to use as a random port."));
        }

        return validatedUri;
    }

    /// <summary>
    /// Creates a unique id based on the project and computer name
    /// </summary>
    /// <returns>A more or less unique id</returns>
    public static string CreateCallerId()
    {
        string seed = EnvironmentCallerHostname + Assembly.GetCallingAssembly().GetName().Name;
        return $"iviz_{seed.GetDeterministicHashCode().ToString("x8")}";
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
            return new Uri($"http://{envHostname}:{usingPort.ToString()}/");
        }

        string portStr = usingPort.ToString();

        return RosUtils.GetUriFromInterface(NetworkInterfaceType.Wireless80211, portStr) ??
               RosUtils.GetUriFromInterface(NetworkInterfaceType.Ethernet, portStr) ??
               new Uri($"http://{Dns.GetHostName()}:{portStr}/");
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
            BuiltIns.ThrowArgumentNull(nameof(masterUri));
        }

        string? envHostname = EnvironmentCallerHostname;
        if (envHostname != null)
        {
            return new Uri($"http://{envHostname}:{usingPort.ToString()}/");
        }

        var masterAddress = RosUtils.TryGetAddress(masterUri.Host);
        if (masterAddress == null)
        {
            return null;
        }

        var myAddress = RosUtils.TryGetAccessibleAddress(masterAddress);

        return myAddress == null ? null : new Uri($"http://{myAddress}:{usingPort.ToString()}/");
    }

    /// <summary>
    /// Retrieves a list of possible valid caller uri with the given port.
    /// Other clients will connect to this node using this address.
    /// If the port is 0, uses a random free port.
    /// </summary>
    /// <param name="usingPort">Port for the caller uri, or 0 for a random free port.</param>
    /// <returns>A list of possible caller uris.</returns>
    public static Uri[] GetCallerUriCandidates(int usingPort = AnyPort)
    {
        var candidates = new List<Uri>();
        string? envHostname = EnvironmentCallerHostname;
        string portStr = usingPort.ToString();

        if (envHostname != null)
        {
            candidates.Add(new Uri($"http://{envHostname}:{portStr}/"));
        }

        candidates.Add(new Uri($"http://{Dns.GetHostName()}:{portStr}/"));

        var uris = RosUtils.GetAllLocalAddresses()
            .Select(address => new Uri($"http://{address}:{portStr}/"));

        candidates.AddRange(uris);

        return candidates.ToArray();
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

        char c0 = name[0];
        if (c0 is not '/' && c0 is not '~' && !char.IsLetter(c0))
        {
            return false;
        }

        foreach (int i in 1..name.Length)
        {
            char c = name[i];
            if (!char.IsLetterOrDigit(c) && c is not '_' && c is not '/')
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Checks if the given name is a valid ROS resource name, and throws an exception with an error message if not.
    /// </summary>  
    public static void ValidateResourceName([NotNull] string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new RosInvalidResourceName("Resource name is empty");
        }

        char c0 = name[0];
        if (!char.IsLetter(c0) && c0 is not '/' && c0 is not '~')
        {
            throw new RosInvalidResourceName(
                $"Resource name '{name}' is not valid. It must start with an alphanumeric character, " +
                $"'/' or '~'. Current start is '{c0}'");
        }

        foreach (int i in 1..name.Length)
        {
            char c = name[i];
            if (!char.IsLetterOrDigit(c) && c is not '_' && c is not '/')
            {
                throw new RosInvalidResourceName(
                    $"Resource name '{name}' is not valid. It must only contain alphanumeric characters, " +
                    $"'/' or '_'. Character at position {i} is '{c}'");
            }
        }
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
        TaskUtils.Run(() => EnsureCleanSlateAsync(token).AsTask(), token).WaitAndRethrow();
    }

    /// <summary>
    /// Asks the master which topics we advertise and are subscribed to, and removes them.
    /// If you are interested in the async version, make sure to set ensureCleanSlate to false in the constructor.
    /// </summary>
    public async ValueTask EnsureCleanSlateAsync(CancellationToken token = default)
    {
        var state = await GetSystemStateAsync(token);
        var tasks = new List<Task>();
        tasks.AddRange(
            state.Subscribers
                .Where(tuple => tuple.Members.Contains(CallerId))
                .Select(tuple => RosMasterClient.UnregisterSubscriberAsync(tuple.Topic, token).AsTask())
        );

        tasks.AddRange(
            state.Publishers
                .Where(tuple => tuple.Members.Contains(CallerId))
                .Select(tuple => RosMasterClient.UnregisterPublisherAsync(tuple.Topic, token).AsTask()
                )
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

    /// <summary>
    /// Sanity check to see if the client we just made can receive connections, or whether there is another
    /// client wih the same ports interfering with ours.
    /// </summary>
    /// <param name="token">An optional cancellation token</param>
    public void CheckOwnUri(CancellationToken token = default)
    {
        TaskUtils.Run(() => CheckOwnUriAsync(token).AsTask(), token).WaitAndRethrow();
    }

    /// <summary>
    /// Sanity check to see if the client we just made can receive connections, or whether there is another
    /// client wih the same ports interfering with ours.
    /// </summary>
    /// <param name="token">An optional cancellation token</param>
    public async ValueTask CheckOwnUriAsync(CancellationToken token = default)
    {
        GetPidResponse response;
        var ownUri = CallerUri;

        try
        {
            response = await CreateNodeClient(ownUri).GetPidAsync(token);
        }
        catch (Exception e)
        {
            throw new RosUnreachableUriException(
                $"The hostname '{ownUri.Host}' in the own uri is not reachable. " +
                "Make sure that your address is correct and your node is in " +
                "the correct network.", e);
        }

        if (!response.IsValid)
        {
            Logger.LogErrorFormat("{0}: Failed to validate reachability response.", this);
        }
        else if (response.Pid != ConnectionUtils.GetProcessId())
        {
            throw new RosUnreachableUriException(
                $"The given own uri '{ownUri}' appears to belong to another " +
                $"ROS node in the network.");
        }
    }

    internal RosNodeClient CreateNodeClient(Uri otherUri) =>
        new(CallerId, CallerUri, otherUri, (int)RpcNodeTimeout.TotalMilliseconds);

    (string id, RosSubscriber<T> subscriber)
        CreateSubscriber<T>(string topic, bool requestNoDelay, Action<T> firstCallback,
            in T generator, RosTransportHint transportHint)
        where T : IMessage
    {
        var topicInfo = new TopicInfo<T>(CallerId, topic, generator);
        int timeoutInMs = (int)TcpRosTimeout.TotalMilliseconds;

        var subscription = new RosSubscriber<T>(this, topicInfo, requestNoDelay, timeoutInMs, transportHint);
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
        CreateSubscriberAsync<T>(string topic, bool requestNoDelay, RosCallback<T> firstCallback,
            T generator, RosTransportHint transportHint, CancellationToken token)
        where T : IMessage
    {
        var topicInfo = new TopicInfo<T>(CallerId, topic, generator);
        int timeoutInMs = (int)TcpRosTimeout.TotalMilliseconds;
        var subscription = new RosSubscriber<T>(this, topicInfo, requestNoDelay, timeoutInMs, transportHint);
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
    /// <returns>An identifier that can be used to unsubscribe from this topic.</returns>
    public string Subscribe<T>(string topic, Action<T> callback)
        where T : IMessage, IDeserializable<T>, new()
    {
        return Subscribe(topic, callback, out RosSubscriber<T> _);
    }

    /// <summary>
    /// Subscribes to the given topic.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="subscriber"> The shared subscriber for this topic, used by all subscribers from this client. </param>
    /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <returns>An identifier that can be used to unsubscribe from this topic.</returns>
    public string Subscribe<T>(string topic, Action<T> callback, out RosSubscriber<T> subscriber,
        bool requestNoDelay = true, RosTransportHint transportHint = RosTransportHint.PreferTcp)
        where T : IMessage, IDeserializable<T>, new()
    {
        if (callback is null)
        {
            BuiltIns.ThrowArgumentNull(nameof(callback));
        }

        string resolvedTopic = ResolveResourceName(topic);
        if (!TryGetSubscriber(resolvedTopic, out IRosSubscriber? baseSubscriber))
        {
            string id;
            (id, subscriber) = CreateSubscriber(resolvedTopic, requestNoDelay, callback, new T(), transportHint);
            return id;
        }

        var newSubscriber = baseSubscriber as RosSubscriber<T>;
        subscriber = newSubscriber ?? throw new RosInvalidMessageTypeException(
            $"There is already a subscriber for '{topic}' with a different type [{baseSubscriber.TopicType}] - " +
            $"requested type was [{BuiltIns.GetMessageType<T>()}]");
        return subscriber.Subscribe(callback);
    }

    string IRosClient.Subscribe<T>(string topic, Action<T> callback, out IRosSubscriber<T> subscriber,
        bool requestNoDelay, RosTransportHint transportHint)
    {
        string id = Subscribe(topic, callback, out RosSubscriber<T> newSubscriber, requestNoDelay, transportHint);
        subscriber = newSubscriber;
        return id;
    }

    /// <summary>
    /// Subscribes to the given topic. The subscriber will try to reconstruct the message based on information
    /// transmitted during handshake. The message type will be <see cref="DynamicMessage"/> if the message type is not known. 
    /// </summary>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="subscriber"> The shared subscriber for this topic, used by all subscribers from this client. </param>
    /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>        
    public string Subscribe(string topic, Action<IMessage> callback, out RosSubscriber<IMessage> subscriber,
        bool requestNoDelay = true, RosTransportHint transportHint = RosTransportHint.PreferTcp)
    {
        if (callback is null)
        {
            BuiltIns.ThrowArgumentNull(nameof(callback));
        }

        string resolvedTopic = ResolveResourceName(topic);
        if (!TryGetSubscriber(resolvedTopic, out IRosSubscriber? baseSubscriber))
        {
            string id;
            (id, subscriber) =
                CreateSubscriber(resolvedTopic, requestNoDelay, callback, new DynamicMessage(), transportHint);
            return id;
        }

        var newSubscriber = baseSubscriber as RosSubscriber<IMessage>;
        subscriber = newSubscriber ?? throw new RosInvalidMessageTypeException(
            $"There is already a subscriber for '{topic}' with a different type [{baseSubscriber.TopicType}] - " +
            $"requested type was [IMessage](generic)");
        return subscriber.Subscribe(callback);
    }

    string IRosClient.Subscribe(string topic, Action<IMessage> callback, out IRosSubscriber subscriber,
        bool requestNoDelay, RosTransportHint transportHint)
    {
        string id = Subscribe(topic, callback, out RosSubscriber<IMessage> newSubscriber, requestNoDelay,
            transportHint);
        subscriber = newSubscriber;
        return id;
    }

    /// <summary>
    /// Subscribes to the given topic.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>
    public ValueTask<(string id, RosSubscriber<T> subscriber)> SubscribeAsync<T>(
        string topic, Action<T> callback, bool requestNoDelay = true,
        RosTransportHint transportHint = RosTransportHint.PreferTcp, CancellationToken token = default)
        where T : IMessage, IDeserializable<T>, new()
    {
        void Callback(in T t, IRosReceiver _) => callback(t);
        return SubscribeAsync(topic, (RosCallback<T>)Callback, requestNoDelay, transportHint, token);
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(
        string topic, Action<T> callback, bool requestNoDelay,
        RosTransportHint transportHint, CancellationToken token)
    {
        return await SubscribeAsync(topic, callback, requestNoDelay, transportHint, token);
    }

    /// <summary>
    /// Subscribes to the given topic. The subscriber will try to reconstruct the message based on information
    /// transmitted during handshake. The message type will be <see cref="DynamicMessage"/> if the message type is not known. 
    /// </summary>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>
    public ValueTask<(string id, RosSubscriber<IMessage> subscriber)> SubscribeAsync(
        string topic, Action<IMessage> callback, bool requestNoDelay = true,
        RosTransportHint transportHint = RosTransportHint.PreferTcp, CancellationToken token = default)
    {
        if (callback is null)
        {
            BuiltIns.ThrowArgumentNull(nameof(callback));
        }

        string resolvedTopic = ResolveResourceName(topic);
        if (!TryGetSubscriberImpl(resolvedTopic, out IRosSubscriber? existingSubscriber))
        {
            void Callback(in IMessage t, IRosReceiver _) => callback(t);
            return CreateSubscriberAsync(resolvedTopic, requestNoDelay, (RosCallback<IMessage>)Callback,
                new DynamicMessage(), transportHint, token);
        }

        if (existingSubscriber is not RosSubscriber<IMessage> validatedSubscriber)
        {
            throw new RosInvalidMessageTypeException(
                $"There is already a subscriber for '{topic}' with a different type [{existingSubscriber.TopicType}] - " +
                $"requested type was [IMessage](generic)");
        }

        return (validatedSubscriber.Subscribe(callback), subscriber: validatedSubscriber).AsTaskResult();
    }

    async ValueTask<(string id, IRosSubscriber subscriber)> IRosClient.SubscribeAsync(
        string topic, Action<IMessage> callback, bool requestNoDelay, RosTransportHint transportHint,
        CancellationToken token)
    {
        return await SubscribeAsync(topic, callback, requestNoDelay, transportHint, token);
    }

    /// <summary>
    /// Subscribes to the given topic. This variant uses a callback that includes information about
    /// the receiver socket and who sent the message.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>
    public ValueTask<(string id, RosSubscriber<T> subscriber)> SubscribeAsync<T>(
        string topic, RosCallback<T> callback, bool requestNoDelay = true,
        RosTransportHint transportHint = RosTransportHint.PreferTcp, CancellationToken token = default)
        where T : IMessage, IDeserializable<T>, new()
    {
        if (callback is null)
        {
            BuiltIns.ThrowArgumentNull(nameof(callback));
        }

        string resolvedTopic = ResolveResourceName(topic);
        if (!TryGetSubscriberImpl(resolvedTopic, out IRosSubscriber? existingSubscriber))
        {
            return CreateSubscriberAsync(resolvedTopic, requestNoDelay, callback, new T(), transportHint, token);
        }

        if (existingSubscriber is not RosSubscriber<T> validatedSubscriber)
        {
            throw new RosInvalidMessageTypeException(
                $"There is already a subscriber for '{topic}' with a different type [{existingSubscriber.TopicType}] - " +
                $"requested type was [{BuiltIns.GetMessageType<T>()}]");
        }

        return (validatedSubscriber.Subscribe(callback), validatedSubscriber).AsTaskResult();
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(
        string topic, RosCallback<T> callback, bool requestNoDelay,
        RosTransportHint transportHint, CancellationToken token)
    {
        return await SubscribeAsync(topic, callback, requestNoDelay, transportHint, token);
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
            BuiltIns.ThrowArgumentNull(nameof(topicId));
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
            BuiltIns.ThrowArgumentNull(nameof(topicId));
        }

        var subscriber = subscribersByTopic.Values.FirstOrDefault(s => s.ContainsId(topicId));
        return subscriber != null && await subscriber.UnsubscribeAsync(topicId);
    }

    internal void RemoveSubscriber(IRosSubscriber subscriber)
    {
        subscribersByTopic.TryRemove(subscriber.Topic, out _);
        RosMasterClient.UnregisterSubscriber(subscriber.Topic);
    }

    internal async ValueTask RemoveSubscriberAsync(IRosSubscriber subscriber, CancellationToken token)
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
#if NETSTANDARD2_0
        public bool TryGetSubscriber(string topic, out IRosSubscriber subscriber)
#else
    public bool TryGetSubscriber(string topic, [NotNullWhen(true)] out IRosSubscriber? subscriber)
#endif
    {
        string resolvedTopic = ResolveResourceName(topic);
        return TryGetSubscriberImpl(resolvedTopic, out subscriber);
    }

#if NETSTANDARD2_0
        bool TryGetSubscriberImpl(string resolvedTopic, out IRosSubscriber subscriber)
#else
    bool TryGetSubscriberImpl(string resolvedTopic, [NotNullWhen(true)] out IRosSubscriber? subscriber)
#endif
    {
        return subscribersByTopic.TryGetValue(resolvedTopic, out subscriber);
    }

    /// <summary>
    /// Retrieves the subscriber for the given topic.
    /// </summary>
    /// <param name="topic">Name of the topic.</param>
    /// <returns></returns>
    public IRosSubscriber GetSubscriber(string topic)
    {
        if (TryGetSubscriber(topic, out IRosSubscriber? subscriber))
        {
            return subscriber;
        }

        throw new KeyNotFoundException($"Cannot find subscriber for topic '{topic}'");
    }

    string ResolveResourceName(string name)
    {
        ValidateResourceName(name);

        return name[0] switch
        {
            '/' => name,
            '~' => $"{CallerId}/{name.Substring(1)}",
            _ => $"{namespacePrefix}{name}"
        };
    }

    RosPublisher<T> CreatePublisher<T>(string topic, T generator) where T : IMessage
    {
        var topicInfo = generator is DynamicMessage dynamicMessage
            ? new TopicInfo<T>(CallerId, topic, dynamicMessage)
            : new TopicInfo<T>(CallerId, topic, generator);

        var publisher = new RosPublisher<T>(this, topicInfo)
            { TimeoutInMs = (int)TcpRosTimeout.TotalMilliseconds };

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
        T generator) where T : IMessage
    {
        var topicInfo = generator is DynamicMessage dynamicMessage
            ? new TopicInfo<T>(CallerId, topic, dynamicMessage)
            : new TopicInfo<T>(CallerId, topic, generator);

        var publisher = new RosPublisher<T>(this, topicInfo)
            { TimeoutInMs = (int)TcpRosTimeout.TotalMilliseconds };

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
    /// <returns>An identifier that can be used to unadvertise from this publisher.</returns>
    public string Advertise<T>(string topic, out RosPublisher<T> publisher) where T : IMessage, new()
    {
        string resolvedTopic = ResolveResourceName(topic);

        if (!TryGetPublisher(resolvedTopic, out IRosPublisher? existingPublisher))
        {
            publisher = CreatePublisher(resolvedTopic, new T());
            return publisher.Advertise();
        }

        if (existingPublisher is not RosPublisher<T> validatedPublisher)
        {
            throw new RosInvalidMessageTypeException(
                $"There is already an advertiser for '{topic}' with a different type [{existingPublisher.TopicType}] - " +
                $"requested type was [{BuiltIns.GetMessageType<T>()}]");
        }

        publisher = validatedPublisher;
        return publisher.Advertise();
    }

    string IRosClient.Advertise<T>(string topic, out IRosPublisher<T> publisher)
    {
        string id = Advertise<T>(topic, out var newPublisher);
        publisher = newPublisher;
        return id;
    }

    /// <summary>
    /// Advertises the given topic with the given dynamic message.
    /// The dynamic message must have been initialized beforehand.
    /// </summary>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="generator">The dynamic message containing the ROS definition.</param>
    /// <param name="publisher">
    /// The shared publisher for this topic, used by all advertisers from this client.
    /// Use this structure to publish messages to your topic.
    /// </param>
    /// <returns>An identifier that can be used to unadvertise from this publisher.</returns>
    public string Advertise(string topic, DynamicMessage generator, out RosPublisher<DynamicMessage> publisher)
    {
        if (generator == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(generator));
        }

        if (!generator.IsInitialized)
        {
            throw new InvalidOperationException("Generator has not been initialized");
        }

        string resolvedTopic = ResolveResourceName(topic);

        if (!TryGetPublisher(resolvedTopic, out IRosPublisher? existingPublisher))
        {
            publisher = CreatePublisher<DynamicMessage>(resolvedTopic, generator);
            return publisher.Advertise();
        }

        if (existingPublisher is not RosPublisher<DynamicMessage> validatedPublisher)
        {
            throw new RosInvalidMessageTypeException(
                $"There is already an advertiser for '{topic}' with a different type [{existingPublisher.TopicType}] - " +
                $"requested type was [{generator.RosMessageType}](dynamic)");
        }

        publisher = validatedPublisher;
        return publisher.Advertise();
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
    /// <returns>A pair containing an identifier that can be used to unadvertise from this publisher, and the publisher object.</returns>
    public async ValueTask<(string id, RosPublisher<T> publisher)> AdvertiseAsync<T>(string topic,
        CancellationToken token = default) where T : IMessage, new()
    {
        string resolvedTopic = ResolveResourceName(topic);

        RosPublisher<T> publisher;
        if (!TryGetPublisher(topic, out IRosPublisher? existingPublisher))
        {
            publisher = (RosPublisher<T>)await CreatePublisherAsync(resolvedTopic, token, new T());
            return (publisher.Advertise(), publisher);
        }

        if (existingPublisher is not RosPublisher<T> validatedPublisher)
        {
            throw new RosInvalidMessageTypeException(
                $"There is already an advertiser for '{topic}' with a different type [{existingPublisher.TopicType}] - " +
                $"requested type was [{BuiltIns.GetMessageType<T>()}]");
        }

        publisher = validatedPublisher;
        return (publisher.Advertise(), publisher);
    }

    async ValueTask<(string id, IRosPublisher<T> publisher)> IRosClient.AdvertiseAsync<T>(string topic,
        CancellationToken token)
    {
        return await AdvertiseAsync<T>(topic, token);
    }

    /// <summary>
    /// Advertises the given topic with the given dynamic message.
    /// The dynamic message must have been initialized beforehand.
    /// </summary>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="generator">The dynamic message containing the ROS definition.</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unadvertise from this publisher, and the publisher object.</returns>
    public async ValueTask<(string id, RosPublisher<DynamicMessage> publisher)> AdvertiseAsync(string topic,
        DynamicMessage generator, CancellationToken token = default)
    {
        if (generator == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(generator));
        }

        if (!generator.IsInitialized)
        {
            throw new InvalidOperationException("Generator has not been initialized");
        }

        string resolvedTopic = ResolveResourceName(topic);

        RosPublisher<DynamicMessage> publisher;
        if (!TryGetPublisher(topic, out var basePublisher))
        {
            publisher = (RosPublisher<DynamicMessage>)
                await CreatePublisherAsync(resolvedTopic, token, generator);
        }
        else
        {
            var newPublisher = basePublisher as RosPublisher<DynamicMessage>;
            publisher = newPublisher ?? throw new RosInvalidMessageTypeException(
                $"There is already an advertiser for '{topic}' with a different type [{basePublisher.TopicType}] - " +
                $"requested type was [{generator.RosMessageType}](dynamic)");
        }

        return (publisher.Advertise(), publisher);
    }

    async ValueTask<(string id, IRosPublisher<DynamicMessage> publisher)> IRosClient.AdvertiseAsync(string topic,
        DynamicMessage generator, CancellationToken token)
    {
        return await AdvertiseAsync(topic, generator, token);
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
            BuiltIns.ThrowArgumentNull(nameof(topicId));
        }

        IRosPublisher? publisher = publishersByTopic.Values.FirstOrDefault(p => p.ContainsId(topicId));

        return publisher != null && publisher.Unadvertise(topicId);
    }

    /// <summary>
    /// Unadvertise the given topic.
    /// </summary>
    /// <param name="topicId">Token returned by Advertise().</param>
    /// <returns>Whether the unadvertisement succeeded.</returns>
    public ValueTask<bool> UnadvertiseAsync(string topicId)
    {
        if (topicId == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(topicId));
        }

        IRosPublisher? publisher =
            publishersByTopic.Values.FirstOrDefault(tmpPublisher => tmpPublisher.ContainsId(topicId));

        return publisher?.UnadvertiseAsync(topicId) ?? false.AsTaskResult();
    }

    internal async ValueTask RemovePublisherAsync(IRosPublisher publisher, CancellationToken token)
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
#if NETSTANDARD2_0
        public bool TryGetPublisher(string topic, out IRosPublisher publisher)
#else
    public bool TryGetPublisher(string topic, [NotNullWhen(true)] out IRosPublisher? publisher)
#endif
    {
        string resolvedTopic = ResolveResourceName(topic);
        return publishersByTopic.TryGetValue(resolvedTopic, out publisher);
    }

    /// <summary>
    /// Retrieves the publisher of the given topic.
    /// </summary>
    /// <param name="topic">Name of the topic.</param>
    /// <returns></returns>
    public IRosPublisher GetPublisher(string topic)
    {
        if (TryGetPublisher(topic, out IRosPublisher? publisher))
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
    public BriefTopicInfo[] GetSystemPublishedTopics()
    {
        var response = RosMasterClient.GetPublishedTopics();
        if (!response.IsValid)
        {
            throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
        }

        return response.Topics
            .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
            .ToArray();
    }

    /// <summary>
    /// Asks the master for all the published topics in the system with at least one publisher.
    /// Corresponds to the function 'getPublishedTopics' in the ROS Master API.
    /// </summary>
    /// <returns>List of topic names and message types.</returns>
    public async ValueTask<BriefTopicInfo[]> GetSystemPublishedTopicsAsync(
        CancellationToken token = default)
    {
        var response = await RosMasterClient.GetPublishedTopicsAsync(token: token);
        if (!response.IsValid)
        {
            throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
        }

        return response.Topics
            .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
            .ToArray();
    }

    /// <summary>
    /// Asks the master for all the topics in the system.
    /// Corresponds to the function 'getTopicTypes' in the ROS Master API.
    /// </summary>
    /// <returns>List of topic names and message types.</returns>
    public BriefTopicInfo[] GetSystemTopicTypes()
    {
        var response = RosMasterClient.GetTopicTypes();
        if (response.IsValid)
        {
            return response.Topics
                .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
                .ToArray();
        }

        throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
    }

    /// <summary>
    /// Asks the master for all the topics in the system.
    /// Corresponds to the function 'getTopicTypes' in the ROS Master API.
    /// </summary>
    /// <returns>List of topic names and message types.</returns>
    public async ValueTask<BriefTopicInfo[]> GetSystemTopicTypesAsync(
        CancellationToken token = default)
    {
        var response = await RosMasterClient.GetTopicTypesAsync(token: token);
        if (!response.IsValid)
        {
            throw new RosRpcException($"Failed to retrieve topics: {response.StatusMessage}");
        }

        return response.Topics
            .Select(tuple => new BriefTopicInfo(tuple.name, tuple.type))
            .ToArray();
    }


    /// <summary>
    /// Gets the topics published by this node.
    /// </summary>
    public BriefTopicInfo[] SubscribedTopics => GetSubscriptionsRpc();

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
    public BriefTopicInfo[] PublishedTopics => GetPublicationsRpc();

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

    internal async ValueTask PublisherUpdateRpcAsync(string topic, IEnumerable<Uri> publishers, CancellationToken token)
    {
        if (!TryGetSubscriber(topic, out IRosSubscriber? subscriber))
        {
            Logger.LogDebugFormat("{0}: PublisherUpdate called for nonexisting topic '{1}'", this, topic);
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

    internal TopicRequestRpcResult TryRequestTopicRpc(string remoteCallerId, string topic, bool requestsTcp,
        RpcUdpTopicRequest? requestsUdp, out Endpoint? tcpResponse, out RpcUdpTopicResponse? udpResponse)
    {
        if (TryGetPublisher(topic, out IRosPublisher? publisher))
        {
            return publisher.RequestTopicRpc(requestsTcp, requestsUdp, out tcpResponse, out udpResponse);
        }

        Logger.LogDebugFormat("{0}: '{1} is requesting topic '{2}' but we don't publish it", this, remoteCallerId,
            topic);
        tcpResponse = null;
        udpResponse = null;
        return TopicRequestRpcResult.NoSuchTopic;
    }

    /// <summary>
    /// Close this connection. Unsubscribes and unadvertises all topics.
    /// </summary>
    public void Close(CancellationToken token = default)
    {
        TaskUtils.Run(() => CloseAsync(token).AsTask(), token).WaitNoThrow(this);
    }

    /// <summary>
    /// Close this connection. Unsubscribes and unadvertises all topics.
    /// </summary>
    public async ValueTask CloseAsync(CancellationToken token = default)
    {
        const int timeoutInMs = 4000;
        using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        tokenSource.CancelAfter(timeoutInMs);
        var innerToken = tokenSource.Token;

        var tasks = new List<Task>();

        if (listener != null)
        {
            var listenerDispose = listener.DisposeAsync().AsTask();
            tasks.Add(listenerDispose);
        }

        var publishers = publishersByTopic.Values.ToArray();
        publishersByTopic.Clear();

        foreach (var publisher in publishers)
        {
            tasks.Add(publisher.DisposeAsync(innerToken).AwaitNoThrow(this));
            tasks.Add(RosMasterClient.UnregisterPublisherAsync(publisher.Topic, innerToken).AwaitNoThrow(this));
        }

        var subscribers = subscribersByTopic.Values.ToArray();
        subscribersByTopic.Clear();

        foreach (var subscriber in subscribers)
        {
            tasks.Add(subscriber.DisposeAsync(innerToken).AwaitNoThrow(this));
            tasks.Add(RosMasterClient.UnregisterSubscriberAsync(subscriber.Topic, innerToken).AwaitNoThrow(this));
        }

        var receivers = subscribedServicesByName.Values.ToArray();
        subscribedServicesByName.Clear();

        foreach (var receiver in receivers)
        {
            receiver.Dispose();
        }

        var serviceManagers = publishedServicesByName.Values.ToArray();
        publishedServicesByName.Clear();

        foreach (var serviceManager in serviceManagers)
        {
            tasks.Add(serviceManager.DisposeAsync(innerToken).AwaitNoThrow(this));
            tasks.Add(RosMasterClient.UnregisterServiceAsync(serviceManager.Service, serviceManager.Uri, innerToken)
                .AwaitNoThrow(this));
        }

        var timeoutTask = Task.Delay(timeoutInMs, innerToken);
        var finalTask = await (tasks.WhenAll(), timeoutTask).WhenAny();
        if (finalTask == timeoutTask)
        {
            Logger.LogErrorFormat("EE {0}: Close() tasks timed out.", this);
        }

        await finalTask.AwaitNoThrow(this);

        RosMasterClient.Dispose();
    }

    public SubscriberState GetSubscriberStatistics() =>
        new(subscribersByTopic.Values.Select(subscriber => subscriber.GetState()).ToArray());

    public PublisherState GetPublisherStatistics() =>
        new(publishersByTopic.Values.Select(publisher => publisher.GetState()).ToArray());

    internal List<BusInfo> GetBusInfoRpc()
    {
        var busInfos = new List<BusInfo>();

        var state = GetSubscriberStatistics();
        foreach (var topic in state.Topics)
        {
            var receiverStates = topic.Receivers.Where(subscriber => subscriber.TransportType != null);
            foreach (var receiver in receiverStates)
            {
                busInfos.Add(new BusInfo(busInfos.Count, topic.Topic, receiver));
            }
        }

        var publisherState = GetPublisherStatistics();
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

                if (!response.IsValid || response.Uri == null)
                {
                    continue;
                }

                busInfos.Add(new BusInfo(busInfos.Count, topic.Topic, response.Uri, sender));
            }
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
        where T : IService, new()
    {
        using CancellationTokenSource timeoutTs = new(timeoutInMs);
        return CallService(serviceName, service, persistent, timeoutTs.Token);
    }

    public TU CallService<TT, TU>(string serviceName, IRequest<TT, TU> request,
        bool persistent = false, CancellationToken token = default)
        where TT : IService, new() where TU : IResponse
    {
        var service = new TT { Request = request };
        CallService(serviceName, service, persistent, token);
        return (TU)service.Response;
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
        where T : IService, new()
    {
        return TaskUtils.Run(() => CallServiceAsync(serviceName, service, persistent, token).AsTask(), token)
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
        int timeoutInMs) where T : IService, new()
    {
        using var timeoutTs = new CancellationTokenSource(timeoutInMs);
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
        TT service = new() { Request = request };
        await CallServiceAsync(serviceName, service, persistent, token);
        return (TU)service.Response;
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
    public async ValueTask<T> CallServiceAsync<T>(string serviceName, T service, bool persistent = false,
        CancellationToken token = default)
        where T : IService, new()
    {
        string resolvedServiceName = ResolveResourceName(serviceName);

        if (persistent && subscribedServicesByName.TryGetValue(resolvedServiceName, out var existingReceiver))
        {
            if (service.RosServiceType != existingReceiver.ServiceType)
            {
                throw new RosInvalidMessageTypeException(
                    $"Existing connection of {resolvedServiceName} with service type {existingReceiver.ServiceType} " +
                    "does not match the new given type.");
            }

            if (!existingReceiver.IsAlive)
            {
                existingReceiver.Dispose();
                subscribedServicesByName.TryRemove(resolvedServiceName, out _);
                // continues below
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
                    if (e is OperationCanceledException or RosServiceCallFailed) throw;
                    throw new RoslibException($"Service call '{resolvedServiceName}' to " +
                                              $"{existingReceiver.RemoteUri?.ToString() ?? "[unknown]"} failed", e);
                }
            }
        }

        var response = await RosMasterClient.LookupServiceAsync(resolvedServiceName, token);
        if (!response.IsValid || response.ServiceUri == null)
        {
            throw new RosServiceNotFoundException(resolvedServiceName, response.StatusMessage);
        }

        var serviceUri = response.ServiceUri;
        var serviceInfo = ServiceInfo.Instantiate<T>(CallerId, resolvedServiceName);
        if (persistent)
        {
            var serviceCaller = new ServiceCaller(serviceInfo);
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
                if (e is OperationCanceledException or RosServiceCallFailed) throw;
                throw new RoslibException($"Service call '{resolvedServiceName}' to " +
                                          $"{serviceUri?.ToString() ?? "[unknown]"} failed", e);
            }
        }

        try
        {
            using var serviceCaller = new ServiceCaller(serviceInfo);
            await serviceCaller.StartAsync(serviceUri, persistent, token);
            await serviceCaller.ExecuteAsync(service, token);
            return service;
        }
        catch (Exception e) when (e is not (OperationCanceledException or RosServiceCallFailed))
        {
            throw new RoslibException($"Service call '{resolvedServiceName}' to {serviceUri} failed", e);
        }
    }

    bool ServiceAlreadyAdvertised(string serviceName, string serviceType)
    {
        if (!publishedServicesByName.TryGetValue(serviceName, out var existingSender))
        {
            return false;
        }

        if (existingSender.ServiceType != serviceType)
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
        return TaskUtils.Run(() => AdvertiseServiceAsync(serviceName, callback, token).AsTask(), token)
            .WaitAndRethrow();
    }

    /// <summary>
    /// Advertises the given service.
    /// </summary>
    /// <param name="serviceName">Name of the ROS service.</param>
    /// <param name="callback">Function to be called when a service request arrives. The response should be written in the response field.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <typeparam name="T">Service type.</typeparam>        
    public ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Action<T> callback,
        CancellationToken token = default)
        where T : IService, new()
    {
        ValueTask Wrapper(T x)
        {
            callback(x);
            return default;
        }

        return AdvertiseServiceAsync<T>(serviceName, Wrapper, token);
    }

    /// <summary>
    /// Advertises the given service. The callback function may be async.
    /// </summary>
    /// <param name="serviceName">Name of the ROS service.</param>
    /// <param name="callback">Async function to be called when a service request arrives. The response should be written in the response field.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <typeparam name="T">Service type.</typeparam>
    public async ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, ValueTask> callback,
        CancellationToken token = default)
        where T : IService, new()
    {
        string resolvedServiceName = ResolveResourceName(serviceName);

        var serviceInfo = ServiceInfo.Instantiate<T>(CallerId, resolvedServiceName);

        if (ServiceAlreadyAdvertised(resolvedServiceName, serviceInfo.Type))
        {
            return false;
        }

        ValueTask Callback(IService service) => callback((T)service);

        var advertisedService = new ServiceRequestManager(serviceInfo, CallerUri.Host, Callback);

        publishedServicesByName.TryAdd(resolvedServiceName, advertisedService);

        try
        {
            await RosMasterClient.RegisterServiceAsync(resolvedServiceName, advertisedService.Uri, token);
        }
        catch (Exception e)
        {
            publishedServicesByName.TryRemove(resolvedServiceName, out _);
            throw new RosRpcException($"Failed to advertise service '{serviceName}'", e);
        }

        return true;
    }

    /// <summary>
    /// Unadvertises the service.
    /// </summary>
    /// <param name="name">Name of the service</param>
    /// <param name="token">An optional cancellation token</param>
    /// <exception cref="ArgumentException">Thrown if name is null</exception>
    public bool UnadvertiseService(string name, CancellationToken token = default)
    {
        return TaskUtils.Run(() => UnadvertiseServiceAsync(name, token).AsTask(), token).WaitAndRethrow();
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

        if (!publishedServicesByName.TryGetValue(resolvedServiceName, out var advertisedService))
        {
            return false;
        }

        publishedServicesByName.TryRemove(resolvedServiceName, out _);

        await advertisedService.DisposeAsync(token);
        await RosMasterClient.UnregisterServiceAsync(resolvedServiceName, advertisedService.Uri, token);
        return true;
    }

    internal bool TryGetLoopbackReceiver<T>(string topic, in Endpoint endpoint, out ILoopbackReceiver<T>? receiver)
        where T : IMessage
    {
        if (subscribersByTopic.TryGetValue(topic, out var existingSubscriber) &&
            existingSubscriber is IRosSubscriber<T> subscriber)
        {
            return subscriber.TryGetLoopbackReceiver(endpoint, out receiver);
        }

        receiver = null;
        return false;
    }

    public void Dispose()
    {
        Close();
    }

    public ValueTask DisposeAsync() => CloseAsync();

    public override string ToString()
    {
        return $"[RosClient '{CallerId}' MasterUri='{MasterUri}']";
    }
}