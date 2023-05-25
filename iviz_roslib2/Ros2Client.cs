using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.RclInterop;
using Iviz.Roslib2.RclInterop.Wrappers;
using Iviz.Tools;

namespace Iviz.Roslib2;

public sealed class Ros2Client : IRosClient
{
    readonly ConcurrentDictionary<string, IRos2Subscriber> subscribersByTopic = new();
    readonly ConcurrentDictionary<string, IRos2Publisher> publishersByTopic = new();
    readonly ConcurrentDictionary<string, Ros2ServiceCaller> callersByService = new();
    readonly ConcurrentDictionary<string, Ros2ServiceListener> listenersByService = new();
    readonly string namespacePrefix;
    bool disposed;

    struct Cache<T>
    {
        public T? cache;
        public long ticks;
    }

    Cache<IReadOnlyList<SubscriberState>> cachedSubscriberStats;
    Cache<IReadOnlyList<PublisherState>> cachedPublisherStats;
    Cache<SystemState> cachedSystemState;

    internal AsyncRclClient Rcl { get; }

    public string CallerId => Rcl.FullName;

    string ResolveResourceName(string name)
    {
        RosNameUtils.ValidateResourceName(name);

        return name[0] switch
        {
            '/' => name,
            '~' => $"{CallerId}/{name[1..]}",
            _ => $"{namespacePrefix}{name}"
        };
    }

    public Ros2ParameterClient ParameterClient { get; }
    public Ros2ParameterServer ParameterServer { get; }

    public Ros2Client(string? ownId = null, string? @namespace = null, int domainId = 0, RclWrapper? wrapperType = null)
    {
        if (string.IsNullOrWhiteSpace(ownId))
        {
            ownId = RosNameUtils.CreateCallerId();
        }

        RclClient.SetRclWrapper(wrapperType ??
#if NETSTANDARD2_1
                                throw new ArgumentNullException(nameof(wrapperType)));
#else
                                new RclGenericWrapper());
#endif

        namespacePrefix = @namespace == null ? "/" : $"/{@namespace}/";
        Rcl = new AsyncRclClient(ownId, @namespace ?? "", domainId);
        ParameterClient = new Ros2ParameterClient(this);
        ParameterServer = new Ros2ParameterServer(this);
    }

#if NET5_0_OR_GREATER
    public static void RemapRclWrapperLibrary(string rclLibraryPath)
    {
        IntPtr MapAndLoad(string libraryName, System.Reflection.Assembly assembly,
            DllImportSearchPath? dllImportSearchPath)
        {
            return NativeLibrary.Load(
                libraryName != "iviz_ros2_rcl"
                    ? libraryName
                    : rclLibraryPath,
                assembly, dllImportSearchPath);
        }

        if (string.IsNullOrWhiteSpace(rclLibraryPath)) BuiltIns.ThrowArgumentNull(nameof(rclLibraryPath));

        NativeLibrary.SetDllImportResolver(typeof(Ros2Client).Assembly, MapAndLoad);
    }
#endif

    public static void SetLoggingLevel(RclLogSeverity severity) => RclClient.SetLoggingLevel(severity);

    public void InitializeParameterServer()
    {
        TaskUtils.RunSync(ParameterServer.RegisterServicesAsync);
    }

    public ValueTask InitializeParameterServerAsync(CancellationToken token = default)
    {
        return ParameterServer.RegisterServicesAsync(token);
    }

    public bool TryGetSubscriber(string topic, [NotNullWhen(true)] out IRos2Subscriber? subscriber)
    {
        string resolvedTopic = ResolveResourceName(topic);
        return TryGetSubscriberImpl(resolvedTopic, out subscriber);
    }

    bool TryGetSubscriberImpl(string resolvedTopic, [NotNullWhen(true)] out IRos2Subscriber? subscriber)
    {
        return subscribersByTopic.TryGetValue(resolvedTopic, out subscriber);
    }

    public string Subscribe<T>(string topic, RosCallback<T> callback, out Ros2Subscriber<T> subscriber,
        RosTransportHint transportHint = RosTransportHint.PreferTcp)
        where T : IMessage, new()
    {
        (string id, subscriber) = TaskUtils.RunSync(() => SubscribeAsync(topic, callback, transportHint));
        return id;
    }

    public string Subscribe<T>(string topic, Action<T> callback, out Ros2Subscriber<T> subscriber,
        RosTransportHint transportHint = RosTransportHint.PreferTcp)
        where T : IMessage, new()
    {
        return Subscribe(topic, new ActionRosCallback<T>(callback), out subscriber, transportHint);
    }

    public ValueTask<(string id, Ros2Subscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, Action<T> callback,
            RosTransportHint transportHint = RosTransportHint.PreferTcp,
            CancellationToken token = default)
        where T : IMessage, new()
    {
        return SubscribeAsync(topic, new ActionRosCallback<T>(callback), transportHint, token);
    }

    public ValueTask<(string id, Ros2Subscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, RosCallback<T> callback,
            RosTransportHint transportHint = RosTransportHint.PreferTcp,
            CancellationToken token = default)
        where T : IMessage, new()
    {
        var profile = transportHint switch
        {
            RosTransportHint.OnlyUdp or RosTransportHint.PreferUdp => QosProfile.SensorData,
            RosTransportHint.OnlyTcp or RosTransportHint.PreferTcp => QosProfile.SubscriberDefault,
            _ => throw new IndexOutOfRangeException()
        };

        return SubscribeAsync(topic, callback, profile, token);
    }

    public async ValueTask<(string id, Ros2Subscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, RosCallback<T> callback,
            QosProfile profile, CancellationToken token = default)
        where T : IMessage, new()
    {
        if (topic is null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (callback is null) BuiltIns.ThrowArgumentNull(nameof(callback));

        string messageType = BuiltIns.GetMessageType<T>();
        string resolvedTopic = ResolveResourceName(topic);
        
        if (!TryGetSubscriberImpl(resolvedTopic, out var baseSubscriber))
        {
            var subscriber = new Ros2Subscriber<T>(this);
            var rclSubscriber = await Rcl.SubscribeAsync(resolvedTopic, messageType, subscriber, profile, token);
            subscriber.Subscriber = rclSubscriber;

            subscribersByTopic[resolvedTopic] = subscriber;
            string id = subscriber.Subscribe(callback);
            subscriber.Start();
            return (id, subscriber);
        }

        var newSubscriber = baseSubscriber as Ros2Subscriber<T> ?? throw new RosInvalidMessageTypeException(topic,
            baseSubscriber.TopicType, messageType);
        return (newSubscriber.Subscribe(callback), newSubscriber);
    }

    public bool TryGetPublisher(string topic, [NotNullWhen(true)] out IRos2Publisher? publisher)
    {
        string resolvedTopic = ResolveResourceName(topic);
        return publishersByTopic.TryGetValue(resolvedTopic, out publisher);
    }

    public string Advertise<T>(string topic, out Ros2Publisher<T> publisher) where T : IMessage, new()
    {
        (string id, publisher) = TaskUtils.RunSync(() => AdvertiseAsync<T>(topic));
        return id;
    }

    public ValueTask<(string id, Ros2Publisher<T> publisher)> AdvertiseAsync<T>(string topic,
        bool latchingEnabled = false, CancellationToken token = default) where T : IMessage, new()
    {
        return AdvertiseAsync<T>(topic, latchingEnabled
                ? QosProfile.PublisherLatchingProfile
                : QosProfile.PublisherDefault,
            token);
    }

    public async ValueTask<(string id, Ros2Publisher<T> publisher)> AdvertiseAsync<T>(string topic,
        QosProfile profile, CancellationToken token = default) where T : IMessage, new()
    {
        if (topic is null) BuiltIns.ThrowArgumentNull(nameof(topic));

        string resolvedTopic = ResolveResourceName(topic);
        string messageType = BuiltIns.GetMessageType<T>();

        if (!TryGetPublisher(resolvedTopic, out var existingPublisher))
        {
            var rclPublisher = await Rcl.AdvertiseAsync(resolvedTopic, messageType, profile, token);
            var publisher = new Ros2Publisher<T>(this, rclPublisher);
            publishersByTopic[resolvedTopic] = publisher;
            return (publisher.Advertise(), publisher);
        }

        if (existingPublisher is not Ros2Publisher<T> validatedPublisher)
        {
            throw new RosInvalidMessageTypeException(resolvedTopic, existingPublisher.TopicType, messageType);
        }

        return (validatedPublisher.Advertise(), validatedPublisher);
    }

    async ValueTask<(string id, IRosPublisher<T> publisher)> IRosClient.AdvertiseAsync<T>(string topic,
        bool latchingEnabled, CancellationToken token)
    {
        return await AdvertiseAsync<T>(topic, latchingEnabled, token);
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(string topic,
        Action<T> callback, RosTransportHint transportHint, CancellationToken token)
    {
        return await SubscribeAsync(topic, callback, transportHint, token);
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(string topic,
        RosCallback<T> callback, RosTransportHint transportHint, CancellationToken token)
    {
        return await SubscribeAsync(topic, callback, transportHint, token);
    }

    internal void RemoveSubscriber(IRosSubscriber subscriber)
    {
        subscribersByTopic.TryRemove(subscriber.Topic, out _);
    }

    internal void RemovePublisher(IRosPublisher subscriber)
    {
        publishersByTopic.TryRemove(subscriber.Topic, out _);
    }

    ValueTask IRosClient.CallServiceAsync<T>(string serviceName, T service, bool persistent, CancellationToken token)
    {
        return CallServiceAsync(serviceName, service, persistent, token: token);
    }

    public async ValueTask CallServiceAsync<T>(string serviceName, T service, bool persistent = false,
        QosProfile? profile = null, CancellationToken token = default) where T : IService, new()
    {
        if (serviceName is null) BuiltIns.ThrowArgumentNull(nameof(serviceName));
        if (service is null) BuiltIns.ThrowArgumentNull(nameof(service));
        if (service.Request is null) BuiltIns.ThrowArgumentNull(nameof(service.Request));
        if (service.Response is null) BuiltIns.ThrowArgumentNull(nameof(service.Response));

        service.Request.RosValidate();

        string resolvedServiceName = ResolveResourceName(serviceName);
        string serviceType = service.RosServiceType;
        
        token.ThrowIfCancellationRequested();

        Ros2ServiceCaller serviceCaller;
        if (!callersByService.TryGetValue(resolvedServiceName, out var existingCaller))
        {
            var generator = (IDeserializable<IResponse>)new T().Response;
            serviceCaller = new Ros2ServiceCaller(this, generator);
            var rclServiceClient =
                await Rcl.CreateServiceClientAsync(resolvedServiceName, serviceType, serviceCaller,
                    profile ?? QosProfile.ServicesDefault, token);
            serviceCaller.ServiceClient = rclServiceClient;
            serviceCaller.Start();

            if (persistent)
            {
                callersByService[resolvedServiceName] = serviceCaller;
            }
        }
        else if (existingCaller.ServiceType == serviceType)
        {
            serviceCaller = existingCaller;
            if (!persistent)
            {
                callersByService.TryRemove(resolvedServiceName, out _);
            }
        }
        else
        {
            throw new RosInvalidMessageTypeException(
                $"Existing connection of {resolvedServiceName} with service type {existingCaller.ServiceType} " +
                "does not match the new given type.");
        }

        try
        {
            service.Response = await serviceCaller.ExecuteAsync(service.Request, token);
        }
        finally
        {
            if (!persistent)
            {
                await serviceCaller.DisposeAsync(default);
            }
        }
    }

    internal void RemoveServiceCaller(Ros2ServiceCaller caller)
    {
        callersByService.TryRemove(caller.Service, out _);
    }

    internal void RemoveServiceListener(Ros2ServiceListener server)
    {
        listenersByService.TryRemove(server.Service, out _);
    }

    public ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, ValueTask> callback,
        CancellationToken token = default) where T : IService, new()
    {
        return AdvertiseServiceAsync(serviceName, callback, null, token);
    }

    public async ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, ValueTask> callback,
        QosProfile? profile = null, CancellationToken token = default) where T : IService, new()
    {
        if (serviceName is null) BuiltIns.ThrowArgumentNull(nameof(serviceName));
        if (callback is null) BuiltIns.ThrowArgumentNull(nameof(callback));

        string resolvedServiceName = ResolveResourceName(serviceName);
        string serviceType = BuiltIns.GetServiceType<T>();

        token.ThrowIfCancellationRequested();

        if (!listenersByService.TryGetValue(resolvedServiceName, out var existingListener))
        {
            ValueTask Callback(IService service) => callback((T)service);

            var serviceListener = new Ros2ServiceListener(this, () => new T(), Callback);
            var rclServiceClient =
                await Rcl.AdvertiseServiceAsync(resolvedServiceName, serviceType, serviceListener,
                    profile ?? QosProfile.ServicesDefault, token);
            serviceListener.ServiceServer = rclServiceClient;
            serviceListener.Start();

            listenersByService[resolvedServiceName] = serviceListener;
            return true;
        }

        if (existingListener.ServiceType == serviceType)
        {
            return false;
        }

        throw new RosInvalidMessageTypeException(
            $"Existing connection of {resolvedServiceName} with service type {existingListener.ServiceType} " +
            "does not match the new given type.");
    }

    public ValueTask UnadvertiseServiceAsync(string name, CancellationToken token = default)
    {
        string resolvedServiceName = ResolveResourceName(name);
        return listenersByService.TryGetValue(resolvedServiceName, out var advertisedService)
            ? advertisedService.DisposeAsync(token)
            : default;
    }

    public IReadOnlyList<SubscriberState> GetSubscriberStatistics() => TaskUtils.RunSync(GetSubscriberStatisticsAsync);

    public IReadOnlyList<PublisherState> GetPublisherStatistics() => TaskUtils.RunSync(GetPublisherStatisticsAsync);

    public ValueTask<IReadOnlyList<SubscriberState>> GetSubscriberStatisticsAsync(CancellationToken token = default)
    {
        return cachedSubscriberStats.ticks > Rcl.GraphChangedTicks && cachedSubscriberStats.cache is { } stats
            ? stats.AsTaskResult()
            : GetSubscriberStatisticsCoreAsync(token);
    }

    async ValueTask<IReadOnlyList<SubscriberState>> GetSubscriberStatisticsCoreAsync(CancellationToken token)
    {
        var subscribers = subscribersByTopic.Values.ToArray();
        cachedSubscriberStats.ticks = DateTime.Now.Ticks;
        cachedSubscriberStats.cache = await subscribers.Select(subscriber => subscriber.GetStateAsync(token)).WhenAll();
        return cachedSubscriberStats.cache;
    }

    public ValueTask<IReadOnlyList<PublisherState>> GetPublisherStatisticsAsync(CancellationToken token = default)
    {
        return cachedPublisherStats.ticks > Rcl.GraphChangedTicks && cachedPublisherStats.cache is { } stats
            ? stats.AsTaskResult()
            : GetPublisherStatisticsCoreAsync(token);
    }

    async ValueTask<IReadOnlyList<PublisherState>> GetPublisherStatisticsCoreAsync(CancellationToken token)
    {
        var publishers = publishersByTopic.Values.ToArray();
        cachedPublisherStats.ticks = DateTime.Now.Ticks;
        cachedPublisherStats.cache = await publishers.Select(publisher => publisher.GetStateAsync(token)).WhenAll();
        return cachedPublisherStats.cache;
    }

    public async ValueTask<bool> IsServiceAvailableAsync(string service, CancellationToken token = default)
    {
        var services = await Rcl.GetServiceNamesAndTypesAsync(token);
        return services.Any(type => type.Topic == service);
    }

    public ValueTask<TopicNameType[]> GetSystemPublishedTopicsAsync(CancellationToken token = default) =>
        Rcl.GetPublishedTopicNamesAndTypesAsync(token).AsValueTask();

    public ValueTask<TopicNameType[]> GetSystemTopicsAsync(CancellationToken token = default) =>
        Rcl.GetTopicNamesAndTypesAsync(token).AsValueTask();

    public string[] GetParameterNames(string? node = null)
    {
        return node is null
            ? ParameterServer.Parameters.Keys.ToArray()
            : TaskUtils.RunSync(() => GetParameterNamesAsync(node));
    }

    public ValueTask<string[]> GetParameterNamesAsync(string? node = null, CancellationToken token = default)
    {
        return node is null
            ? ParameterServer.Parameters.Keys.ToArray().AsTaskResult()
            : ParameterClient.GetParameterNamesAsync(node, token);
    }

    public RosValue GetParameter(string key, string? node = null)
    {
        if (node == null)
        {
            return ParameterServer.Parameters.TryGetValue(key, out var value)
                ? value
                : throw new RosParameterNotFoundException();
        }

        return TaskUtils.RunSync(() => GetParameterAsync(key, node));
    }

    public ValueTask<RosValue> GetParameterAsync(string key, string? node = null, CancellationToken token = default)
    {
        if (node == null)
        {
            return ParameterServer.Parameters.TryGetValue(key, out var value)
                ? value.AsTaskResult()
                : Task.FromException<RosValue>(new RosParameterNotFoundException()).AsValueTask();
        }

        return ParameterClient.GetParameterAsync(node, key, token);
    }

    public SystemState GetSystemState()
    {
        return TaskUtils.RunSync(GetSystemStateAsync);
    }

    public ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default)
    {
        return cachedSystemState.ticks > Rcl.GraphChangedTicks && cachedSystemState.cache is { } state
            ? state.AsTaskResult()
            : GetSystemStateCoreAsync(token);
    }

    async ValueTask<SystemState> GetSystemStateCoreAsync(CancellationToken token = default)
    {
        cachedSystemState.ticks = DateTime.Now.Ticks;
        cachedSystemState.cache = await Rcl.GetSystemStateAsync(token);
        return cachedSystemState.cache;
    }

    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return;
        disposed = true;

        const int timeoutInMs = 2000;

        var closeConnectionsTask = CloseConnectionsAsync().AsTask();
        if (token.IsCancellationRequested)
        {
            Logger.LogFormat("{0}: Pre-cancelled token passed to " + nameof(DisposeAsync), this);
            return; // closeConnectionsTask will keep running in the background
        }

        // the token determines when we stop awaiting. the connections will still keep closing in the background  
        await closeConnectionsTask.AwaitNoThrow(timeoutInMs, this, token);
    }

    async ValueTask CloseConnectionsAsync()
    {
        CancellationToken token = default; // do not expire

        var tasks = new List<Task>();
        var publishers = publishersByTopic.Values.ToArray();
        foreach (var publisher in publishers)
        {
            tasks.Add(publisher.DisposeAsync(token).AwaitNoThrow(this));
        }

        var subscribers = subscribersByTopic.Values.ToArray();
        foreach (var subscriber in subscribers)
        {
            tasks.Add(subscriber.DisposeAsync(token).AwaitNoThrow(this));
        }

        var callers = callersByService.Values.ToArray();
        foreach (var caller in callers)
        {
            tasks.Add(caller.DisposeAsync(token).AwaitNoThrow(this));
        }

        var listeners = listenersByService.Values.ToArray();
        foreach (var listener in listeners)
        {
            tasks.Add(listener.DisposeAsync(token).AwaitNoThrow(this));
        }

        await Task.WhenAll(tasks);

        await Rcl.DisposeAsync(token);
    }
    
    #region parameters

    string[] IRosClient.GetParameterNames() => GetParameterNames();

    ValueTask<string[]> IRosClient.GetParameterNamesAsync(CancellationToken token) =>
        GetParameterNamesAsync(token: token);

    RosValue IRosClient.GetParameter(string key) => GetParameter(key);

    ValueTask<RosValue> IRosClient.GetParameterAsync(string key, CancellationToken token) =>
        GetParameterAsync(key, token: token);

    #endregion
}