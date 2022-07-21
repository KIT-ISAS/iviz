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

    internal RclClient Client { get; }
    public string CallerId => Client.FullName;

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
        Client = new RclClient(callerId, @namespace ?? "");
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
            var rclSubscriber = Client.Subscribe(resolvedTopic, messageType);
            subscriber = new Ros2Subscriber<T>(this, rclSubscriber);
            subscribersByTopic[topic] = subscriber;
            string id = subscriber.Subscribe(callback);
            subscriber.Start();
            return id;
        }

        var newSubscriber = baseSubscriber as Ros2Subscriber<T>;
        subscriber = newSubscriber ?? throw new RosInvalidMessageTypeException(topic,
            baseSubscriber.TopicType, messageType);
        return subscriber.Subscribe(callback);
    }

    public string Subscribe<T>(string topic, Action<T> callback, out Ros2Subscriber<T> subscriber)
        where T : IMessage, new()
    {
        void Callback(in T message, IRosReceiver info) => callback(message);
        return Subscribe(topic, Callback, out subscriber);
    }

    public ValueTask<(string id, Ros2Subscriber<T> subscriber)> SubscribeAsync<T>(string topic, Action<T> callback,
        CancellationToken token) where T : IMessage, new()
    {
        string id = Subscribe(topic, callback, out var subscriber);
        return new ValueTask<(string, Ros2Subscriber<T>)>((id, subscriber));
    }

    public ValueTask<(string id, Ros2Subscriber<T> subscriber)> SubscribeAsync<T>(string topic, RosCallback<T> callback,
        CancellationToken token = default)
        where T : IMessage, new()
    {
        string id = Subscribe(topic, callback, out var subscriber);
        return new ValueTask<(string, Ros2Subscriber<T>)>((id, subscriber));
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
        string resolvedTopic = ResolveResourceName(topic);

        string messageType = BuiltIns.GetMessageType<T>();
        if (!RclClient.IsTypeSupported(messageType))
        {
            ThrowUnsupportedMessageTypeException(messageType);
        }

        if (!TryGetPublisher(resolvedTopic, out var existingPublisher))
        {
            var rclPublisher = Client.Advertise(topic, messageType);
            publisher = new Ros2Publisher<T>(this, rclPublisher);
            publishersByTopic[topic] = publisher;
            return publisher.Advertise();
        }

        if (existingPublisher is not Ros2Publisher<T> validatedPublisher)
        {
            throw new RosInvalidMessageTypeException(topic, existingPublisher.TopicType, messageType);
        }

        publisher = validatedPublisher;
        return publisher.Advertise();
    }

    public ValueTask<(string id, Ros2Publisher<T> publisher)> AdvertiseAsync<T>(string topic,
        CancellationToken token = default) where T : IMessage, new()
    {
        string id = Advertise<T>(topic, out var publisher);
        return new ValueTask<(string, Ros2Publisher<T>)>((id, publisher));
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

    public bool IsServiceAvailable(string service)
    {
        return false;
    }

    public ValueTask<bool> IsServiceAvailableAsync(string service, CancellationToken token = default)
    {
        return new ValueTask<bool>(IsServiceAvailable(service));
    }

    public TopicNameType[] GetSystemPublishedTopics() => GetSystemTopics();

    public ValueTask<TopicNameType[]> GetSystemPublishedTopicsAsync(CancellationToken token = default)
    {
        return GetSystemTopicsAsync(token);
    }

    public TopicNameType[] GetSystemTopics() => Client.GetTopicNamesAndTypes();

    public ValueTask<TopicNameType[]> GetSystemTopicsAsync(CancellationToken token = default)
    {
        return new ValueTask<TopicNameType[]>(GetSystemTopics());
    }

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
    }

    public ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default)
    {
        return new ValueTask<SystemState>(GetSystemState());
    }

    public void Dispose()
    {
        foreach (var publisher in publishersByTopic.Values)
        {
            publisher.Dispose();
        }

        foreach (var subscriber in subscribersByTopic.Values)
        {
            subscriber.Dispose();
        }

        Client.Dispose();
    }

    ValueTask IAsyncDisposable.DisposeAsync() => DisposeAsync();

    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        var tasks = new List<Task>();
        foreach (var publisher in publishersByTopic.Values)
        {
            tasks.Add(publisher.DisposeAsync(token).AwaitNoThrow(this));
        }

        foreach (var subscriber in subscribersByTopic.Values)
        {
            tasks.Add(subscriber.DisposeAsync(token).AwaitNoThrow(this));
        }

        await tasks.WhenAll();

        Dispose();
    }

    [DoesNotReturn]
    static void ThrowUnsupportedMessageTypeException(string messageType) =>
        throw new RosUnsupportedMessageException(messageType);
}