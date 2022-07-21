using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.Rcl;
using Iviz.Tools;
using Iviz.XmlRpc;
using Nito.AsyncEx;

namespace Iviz.Roslib2;

public sealed class Ros2Client : IRosClient
{
    readonly ConcurrentDictionary<string, IRos2Subscriber> subscribersByTopic = new();
    readonly ConcurrentDictionary<string, IRos2Publisher> publishersByTopic = new();
    readonly string namespacePrefix;
    bool disposed;

    internal AsyncRclClient AsyncClient { get; }
    public string CallerId => AsyncClient.FullName;

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
    
    Ros2Client(AsyncRclClient asyncClient, string? @namespace = null)
    {
        namespacePrefix = @namespace == null ? "/" : $"/{@namespace}/";
        AsyncClient = asyncClient;
    }

    public Ros2Client(string callerId, string? @namespace = null)
    {
        namespacePrefix = @namespace == null ? "/" : $"/{@namespace}/";
        AsyncClient = AsyncRclClient.Create(callerId, @namespace ?? "");
    }

    public static async ValueTask<Ros2Client> CreateAsync(string callerId, string? @namespace = null)
    {
        var client = await AsyncRclClient.CreateAsync(callerId, @namespace ?? "");
        return new Ros2Client(client, @namespace);
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

    public string Subscribe<T>(string topic, RosCallback<T> callback, out Ros2Subscriber<T> subscriber)
        where T : IMessage, new()
    {
        (string id, subscriber) = TaskUtils.Run(() => SubscribeAsync(topic, callback).AsTask()).WaitAndRethrow();
        return id;
    }

    public string Subscribe<T>(string topic, Action<T> callback, out Ros2Subscriber<T> subscriber)
        where T : IMessage, new()
    {
        void Callback(in T message, IRosReceiver info) => callback(message);
        return Subscribe(topic, Callback, out subscriber);
    }

    public ValueTask<(string id, Ros2Subscriber<T> subscriber)> SubscribeAsync<T>(string topic,
        Action<T> callback, CancellationToken token) where T : IMessage, new()
    {
        void Callback(in T message, IRosReceiver info) => callback(message);
        return SubscribeAsync<T>(topic, Callback, token);
    }

    public async ValueTask<(string id, Ros2Subscriber<T> subscriber)> SubscribeAsync<T>(string topic,
        RosCallback<T> callback, CancellationToken token = default)
        where T : IMessage, new()
    {
        if (topic is null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (callback is null) BuiltIns.ThrowArgumentNull(nameof(callback));

        string messageType = BuiltIns.GetMessageType<T>();
        if (!RclClient.IsTypeSupported(messageType))
        {
            ThrowUnsupportedMessageTypeException(messageType);
        }

        string resolvedTopic = ResolveResourceName(topic);
        if (!TryGetSubscriberImpl(resolvedTopic, out var baseSubscriber))
        {
            var rclSubscriber = await AsyncClient.SubscribeAsync(resolvedTopic, messageType);
            var subscriber = new Ros2Subscriber<T>(this, rclSubscriber);
            subscribersByTopic[topic] = subscriber;
            string id = subscriber.Subscribe(callback);
            subscriber.Start();
            return (id, subscriber);
        }

        var newSubscriber = baseSubscriber as Ros2Subscriber<T> ?? throw new RosInvalidMessageTypeException(topic,
            baseSubscriber.TopicType, messageType);
        return (newSubscriber.Subscribe(callback), newSubscriber);
    }

    string IRosClient.Subscribe<T>(string topic, Action<T> callback, out IRosSubscriber<T> subscriber,
        RosTransportHint transportHint)
    {
        string id = Subscribe(topic, callback, out var newSubscriber);
        subscriber = newSubscriber;
        return id;
    }

    public bool TryGetPublisher(string topic, [NotNullWhen(true)] out IRos2Publisher? publisher)
    {
        string resolvedTopic = ResolveResourceName(topic);
        return publishersByTopic.TryGetValue(resolvedTopic, out publisher);
    }

    public string Advertise<T>(string topic, out Ros2Publisher<T> publisher) where T : IMessage, new()
    {
        (string id, publisher) = TaskUtils.Run(async () => await AdvertiseAsync<T>(topic)).WaitAndRethrow();
        return id;
    }

    public async ValueTask<(string id, Ros2Publisher<T> publisher)> AdvertiseAsync<T>(string topic,
        CancellationToken token = default) where T : IMessage, new()
    {
        string resolvedTopic = ResolveResourceName(topic);

        string messageType = BuiltIns.GetMessageType<T>();
        if (!RclClient.IsTypeSupported(messageType))
        {
            ThrowUnsupportedMessageTypeException(messageType);
        }

        if (!TryGetPublisher(resolvedTopic, out var existingPublisher))
        {
            var rclPublisher = await AsyncClient.AdvertiseAsync(topic, messageType);
            var publisher = new Ros2Publisher<T>(this, rclPublisher);
            publishersByTopic[topic] = publisher;
            return (publisher.Advertise(), publisher);
        }

        if (existingPublisher is not Ros2Publisher<T> validatedPublisher)
        {
            throw new RosInvalidMessageTypeException(topic, existingPublisher.TopicType, messageType);
        }

        return (validatedPublisher.Advertise(), validatedPublisher);
    }

    string IRosClient.Advertise<T>(string topic, out IRosPublisher<T> publisher)
    {
        string id = Advertise<T>(topic, out var newPublisher);
        publisher = newPublisher;
        return id;
    }

    async ValueTask<(string id, IRosPublisher<T> publisher)> IRosClient.AdvertiseAsync<T>(string topic,
        CancellationToken token)
    {
        return await AdvertiseAsync<T>(topic, token);
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(string topic,
        Action<T> callback, RosTransportHint transportHint, CancellationToken token)
    {
        return await SubscribeAsync(topic, callback, token);
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(string topic,
        RosCallback<T> callback, RosTransportHint transportHint, CancellationToken token)
    {
        return await SubscribeAsync(topic, callback, token);
    }

    internal void RemoveSubscriber(IRosSubscriber subscriber)
    {
        subscribersByTopic.TryRemove(subscriber.Topic, out _);
    }

    internal void RemovePublisher(IRosPublisher subscriber)
    {
        subscribersByTopic.TryRemove(subscriber.Topic, out _);
    }

    public bool AdvertiseService<T>(string serviceName, Action<T> callback, CancellationToken token = default)
        where T : IService, new()
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, ValueTask> callback,
        CancellationToken token = default) where T : IService, new()
    {
        throw new NotImplementedException();
    }

    public T CallService<T>(string serviceName, T service, bool persistent = false, int timeoutInMs = 5000)
        where T : IService, new()
    {
        throw new NotImplementedException();
    }

    public ValueTask<T> CallServiceAsync<T>(string serviceName, T service, bool persistent = false,
        CancellationToken token = default) where T : IService, new()
    {
        throw new NotImplementedException();
    }

    public bool UnadvertiseService(string name, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> UnadvertiseServiceAsync(string name, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<SubscriberState> GetSubscriberStatistics() =>
        subscribersByTopic.Values.Select(subscriber => subscriber.GetState()).ToArray();

    public IReadOnlyList<PublisherState> GetPublisherStatistics() =>
        publishersByTopic.Values.Select(publisher => publisher.GetState()).ToArray();

    public async ValueTask<IReadOnlyList<SubscriberState>> GetSubscriberStatisticsAsync()
    {
        var subscribers = subscribersByTopic.Values.ToArray();
        var states = new SubscriberState[subscribers.Length];
        for (int i = 0; i < subscribers.Length; i++)
        {
            states[i] = await subscribers[i].GetStateAsync();
        }

        return states;
    }

    public async ValueTask<IReadOnlyList<PublisherState>> GetPublisherStatisticsAsync()
    {
        var publishers = publishersByTopic.Values.ToArray();
        var states = new PublisherState[publishers.Length];
        for (int i = 0; i < publishers.Length; i++)
        {
            states[i] = await publishers[i].GetStateAsync();
        }

        return states;
    }

    public bool IsServiceAvailable(string service)
    {
        return false;
    }

    public ValueTask<bool> IsServiceAvailableAsync(string service, CancellationToken token = default) =>
        new(IsServiceAvailable(service));

    public TopicNameType[] GetSystemPublishedTopics() =>
        GetSystemTopics();

    public ValueTask<TopicNameType[]> GetSystemPublishedTopicsAsync(CancellationToken token = default) =>
        GetSystemTopicsAsync(token);

    public TopicNameType[] GetSystemTopics() =>
        TaskUtils.Run(() => GetSystemTopicsAsync().AsTask()).WaitAndRethrow();

    public ValueTask<TopicNameType[]> GetSystemTopicsAsync(CancellationToken token = default) =>
        AsyncClient.GetTopicNamesAndTypesAsync().AsValueTask();

    public string[] GetParameterNames()
    {
        return Array.Empty<string>();
    }

    public ValueTask<string[]> GetParameterNamesAsync(CancellationToken token = default)
    {
        return new ValueTask<string[]>(GetParameterNames());
    }

    public bool GetParameter(string key, out XmlRpcValue value)
    {
        value = default;
        return false;
    }

    public ValueTask<(bool success, XmlRpcValue value)> GetParameterAsync(string key, CancellationToken token = default)
    {
        return new ValueTask<(bool, XmlRpcValue)>((false, default));
    }

    public SystemState GetSystemState()
    {
        /*
        var topics = Client.GetTopicNamesAndTypes();

        var subscribers = topics.Select(topic =>
            new TopicTuple(topic.Topic, GetSubscribers(topic.Topic))).ToArray();

        var publishers = topics.Select(topic =>
            new TopicTuple(topic.Topic, GetPublishers(topic.Topic))).ToArray();

        return new SystemState(publishers, subscribers, Array.Empty<TopicTuple>());

        string[] GetSubscribers(string topic) =>
            Client.GetSubscriberInfo(topic).Select(info => info.NodeName.ToString()).ToArray();

        string[] GetPublishers(string topic) =>
            Client.GetPublisherInfo(topic).Select(info => info.NodeName.ToString()).ToArray();
            */
        return TaskUtils.Run(() => GetSystemStateAsync().AsTask()).WaitAndRethrow();
    }

    public async ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default)
    {
        var topics = await AsyncClient.GetTopicNamesAndTypesAsync();

        var subscribers = new TopicTuple[topics.Length];

        for (int i = 0; i < topics.Length; i++)
        {
            string topic = topics[i].Topic;
            string[] subscriberNodes =
                (await AsyncClient.GetSubscriberInfoAsync(topic))
                .Select(NodeToString)
                .ToArray();
            subscribers[i] = new TopicTuple(topic, subscriberNodes);
        }

        var publishers = new TopicTuple[topics.Length];
        for (int i = 0; i < topics.Length; i++)
        {
            string topic = topics[i].Topic;
            string[] publisherNodes =
                (await AsyncClient.GetPublisherInfoAsync(topic))
                .Select(NodeToString)
                .ToArray();
            subscribers[i] = new TopicTuple(topic, publisherNodes);
        }

        return new SystemState(publishers, subscribers, Array.Empty<TopicTuple>());

        static string NodeToString(EndpointInfo info) => info.NodeName.ToString();
    }

    public void Dispose()
    {
        TaskUtils.Run(() => DisposeAsync().AsTask()).WaitAndRethrow();
    }

    ValueTask IAsyncDisposable.DisposeAsync() => DisposeAsync();

    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return;
        disposed = true;

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

        await tasks.WhenAll();

        await AsyncClient.DisposeAsync();
    }

    [DoesNotReturn]
    static void ThrowUnsupportedMessageTypeException(string messageType) =>
        throw new RosUnsupportedMessageException(messageType);
}