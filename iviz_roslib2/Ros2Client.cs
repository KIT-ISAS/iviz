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

    public Ros2Client(string callerId, string? @namespace = null)
    {
        namespacePrefix = @namespace == null ? "/" : $"/{@namespace}/";
        Rcl = new AsyncRclClient(callerId, @namespace ?? "");
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
        (string id, subscriber) = TaskUtils.RunSync(() => SubscribeAsync(topic, callback));
        return id;
    }

    public string Subscribe<T>(string topic, Action<T> callback, out Ros2Subscriber<T> subscriber)
        where T : IMessage, new()
    {
        void Callback(in T message, IRosReceiver info) => callback(message);
        return Subscribe(topic, Callback, out subscriber);
    }

    public ValueTask<(string id, Ros2Subscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, Action<T> callback, CancellationToken token)
        where T : IMessage, new()
    {
        void Callback(in T message, IRosReceiver info) => callback(message);
        return SubscribeAsync<T>(topic, Callback, token);
    }

    public async ValueTask<(string id, Ros2Subscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, RosCallback<T> callback, CancellationToken token = default)
        where T : IMessage, new()
    {
        if (topic is null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (callback is null) BuiltIns.ThrowArgumentNull(nameof(callback));

        string messageType = BuiltIns.GetMessageType<T>();
        if (!AsyncRclClient.IsTypeSupported(messageType))
        {
            ThrowUnsupportedMessageTypeException(messageType);
        }

        string resolvedTopic = ResolveResourceName(topic);
        if (!TryGetSubscriberImpl(resolvedTopic, out var baseSubscriber))
        {
            var subscriber = new Ros2Subscriber<T>(this);
            var rclSubscriber = await Rcl.SubscribeAsync(resolvedTopic, messageType, subscriber, token);
            subscriber.Subscriber = rclSubscriber;

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
        (string id, publisher) = TaskUtils.RunSync(() => AdvertiseAsync<T>(topic));
        return id;
    }

    public async ValueTask<(string id, Ros2Publisher<T> publisher)> AdvertiseAsync<T>(string topic,
        CancellationToken token = default) where T : IMessage, new()
    {
        string resolvedTopic = ResolveResourceName(topic);

        string messageType = BuiltIns.GetMessageType<T>();
        if (!AsyncRclClient.IsTypeSupported(messageType))
        {
            ThrowUnsupportedMessageTypeException(messageType);
        }

        if (!TryGetPublisher(resolvedTopic, out var existingPublisher))
        {
            var rclPublisher = await Rcl.AdvertiseAsync(resolvedTopic, messageType, token);
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
        return await subscribers.Select(subscriber => subscriber.GetStateAsync()).WhenAll();
    }

    public async ValueTask<IReadOnlyList<PublisherState>> GetPublisherStatisticsAsync()
    {
        var publishers = publishersByTopic.Values.ToArray();
        return await publishers.Select(publisher => publisher.GetStateAsync()).WhenAll();
    }

    public bool IsServiceAvailable(string service)
    {
        return false;
    }

    public ValueTask<bool> IsServiceAvailableAsync(string service, CancellationToken token = default) =>
        new(IsServiceAvailable(service));

    public TopicNameType[] GetSystemPublishedTopics() => GetSystemTopics();

    public ValueTask<TopicNameType[]> GetSystemPublishedTopicsAsync(CancellationToken token = default) =>
        Rcl.GetPublishedTopicNamesAndTypesAsync(token).AsValueTask();

    public TopicNameType[] GetSystemTopics() => TaskUtils.RunSync(GetSystemTopicsAsync);

    public ValueTask<TopicNameType[]> GetSystemTopicsAsync(CancellationToken token = default) =>
        Rcl.GetTopicNamesAndTypesAsync(token).AsValueTask();

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
        return TaskUtils.RunSync(GetSystemStateAsync);
    }

    public ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default)
    {
        return Rcl.GetSystemStateAsync(token).AsValueTask();
    }

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync);
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
            tasks.Add(publisher.DisposeAsync(default).AwaitNoThrow(this));
        }

        var subscribers = subscribersByTopic.Values.ToArray();
        foreach (var subscriber in subscribers)
        {
            tasks.Add(subscriber.DisposeAsync(default).AwaitNoThrow(this));
        }

        await tasks.WhenAll();

        await Rcl.DisposeAsync(default);
    }

    [DoesNotReturn]
    static void ThrowUnsupportedMessageTypeException(string messageType) =>
        throw new RosUnsupportedMessageException(messageType);
}