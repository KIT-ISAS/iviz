using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.RosapiMsgs;
using Iviz.Roslib;
using Iviz.Tools;
using Newtonsoft.Json;

namespace Iviz.Bridge.Client;

public sealed class RosbridgeClient : IRosClient, IAsyncDisposable
{
    readonly ClientWebSocket webSocket;
    readonly ChannelWriter<Command> outputWriter;
    readonly ChannelReader<Command> outputReader;
    readonly CancellationTokenSource runningTs = new();
    readonly Task sendQueueTask;
    readonly Task receiveQueueTask;

    readonly Dictionary<string, RosbridgeSubscriber> subscribersByTopic = new();
    readonly Dictionary<string, BaseRosPublisher> publishersByTopic = new();
    readonly ConcurrentDictionary<(string, int), Action<byte[]?, string?>> calledServices = new();
    readonly string namespacePrefix;

    volatile int currentServiceRequestId;
    bool disposed;

    Cache<SystemState> cachedSystemState;
    Cache<SubscriberState[]> cachedSubscriberStats;
    Cache<PublisherState[]> cachedPublisherStats;

    public string CallerId { get; }
    public Uri BridgeUri { get; }

    RosbridgeClient(ClientWebSocket webSocket, Uri bridgeUri, string? ownId, string? namespaceOverride)
    {
        CustomJsonFormatters.Initialize();

        if (webSocket == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(webSocket));
        }

        this.webSocket = webSocket;
        BridgeUri = bridgeUri;

        string? ns = namespaceOverride ?? RosNameUtils.EnvironmentRosNamespace;
        namespacePrefix = ns == null ? "/" : $"/{ns}/";

        CallerId = RosNameUtils.CreateOwnIdFrom(namespacePrefix, ownId);

        if (webSocket.State != WebSocketState.Open)
        {
            BuiltIns.ThrowArgument(nameof(webSocket), "Client is not connected");
        }

        var channel = Channel.CreateUnbounded<Command>(
            new UnboundedChannelOptions
            {
                SingleWriter = false,
                SingleReader = true
            });

        outputWriter = channel.Writer;
        outputReader = channel.Reader;

        sendQueueTask = TaskUtils.Run(async () => await SendQueue().AwaitNoThrow(this));
        receiveQueueTask = TaskUtils.Run(async () => await ReceiveQueue().AwaitNoThrow(this));
    }

    public static async ValueTask<RosbridgeClient> CreateAsync(Uri uri, string? ownId = null,
        string? namespaceOverride = null, CancellationToken token = default)
    {
        if (uri == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(uri));
        }

        if (uri.Scheme != "ws")
        {
            throw new RosbridgeException("Uri scheme must be 'ws'");
        }

        var webSocket = new ClientWebSocket();
        try
        {
            await webSocket.ConnectAsync(uri, token);
        }
        catch (Exception e)
        {
            webSocket.Dispose();
            throw new RosbridgeConnectionException($"Failed to contact the bridge at '{uri}'", e);
        }

        return new RosbridgeClient(webSocket, uri, ownId, namespaceOverride);
    }

    #region queue

    async ValueTask SendQueue()
    {
        using var stream = new MemoryStream();
        var token = runningTs.Token;

        while (true)
        {
            Command entry;
            try
            {
                entry = await outputReader.ReadAsync(token);
            }
            catch (InvalidOperationException)
            {
                return;
            }

            if (entry.token.IsCancellationRequested)
            {
                entry.tcs?.TrySetCanceled(entry.token);
                return;
            }

            stream.SetLength(0);
            entry.message.SerializeTo(stream);

            try
            {
                var segment = new ArraySegment<byte>(stream.GetBuffer(), 0, (int)stream.Length);
                await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, entry.token);
                entry.tcs?.TrySetResult();
            }
            catch (OperationCanceledException e)
            {
                entry.tcs?.TrySetCanceled(e.CancellationToken);
            }
            catch (WebSocketException)
            {
                entry.tcs?.TrySetCanceled(default);
                _ = Task.Run(() => DisposeAsync(default), default);
            }
            catch (Exception e)
            {
                entry.tcs?.TrySetException(e);
            }
        }
    }

    async ValueTask ReceiveQueue()
    {
        using var socketBuffer = new ResizableRent(2048);
        var token = runningTs.Token;

        while (webSocket.State == WebSocketState.Open)
        {
            ValueWebSocketReceiveResult receiveResult;

            int offset = 0;
            while (true)
            {
                try
                {
                    receiveResult =
                        await webSocket.ReceiveAsync(socketBuffer.Array.AsMemory(offset), token);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (WebSocketException)
                {
                    _ = Task.Run(() => DisposeAsync(default), default);
                    return;
                }
                catch (Exception e)
                {
                    Logger.LogErrorFormat("{0}: Error in " + nameof(webSocket) + "." + nameof(ReceiveQueue) + "(): {1}",
                        this, e);
                    return;
                }

                if (receiveResult.EndOfMessage) break;

                int length = socketBuffer.Array.Length;
                offset = length;
                socketBuffer.EnsureCapacity(2 * length, true);
            }

            switch (receiveResult.MessageType)
            {
                case WebSocketMessageType.Close:
                    _ = Task.Run(() => DisposeAsync(default), default);
                    return;
                case WebSocketMessageType.Text:
                    OnReceive(socketBuffer.Array);
                    break;
                case WebSocketMessageType.Binary:
                    FastDecodeCbor(socketBuffer.Array);
                    break;
            }
        }
    }

    void OnReceive(byte[] array)
    {
        ResponseMessage? message;
        try
        {
            message = Utf8Json.JsonSerializer.Deserialize<ResponseMessage>(array);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Failed to JSON-deserialize received message: {1}", this, e);
            return;
        }

        if (message is null)
        {
            Logger.LogErrorFormat("{0}: Ignoring null message", this);
            return;
        }

        switch (message.Op)
        {
            case "publish":
                if (!subscribersByTopic.TryGetValue(message.Topic, out var manager))
                {
                    //Logger.LogFormat("{0}: Received unknown message from topic '{1}'", this, message.Topic);
                    return;
                }

                manager.HandleJson(array);
                break;

            case "service_response":
                if (!int.TryParse(message.Id, out int id))
                {
                    Logger.LogFormat("{0}: Service response for {1} did not return a valid id", this, message.Service);
                    return;
                }

                if (!calledServices.TryGetValue((message.Service, id), out var action))
                {
                    //Logger.LogFormat("{0}: Received unknown response from service '{1}'", this, message.Service);
                    return;
                }

                if (message.Result)
                {
                    action(array, null);
                }
                else
                {
                    try
                    {
                        var errorMessage = Utf8Json.JsonSerializer.Deserialize<ServiceResponseErrorMessage>(array);
                        action(null, errorMessage.Values);
                    }
                    catch (Exception e)
                    {
                        Logger.LogErrorFormat("{0}: Failed to JSON-deserialize service response: {1}", this, e);
                        action(null, null);
                    }
                }

                break;
            case "status":
            {
                try
                {
                    var errorMessage = Utf8Json.JsonSerializer.Deserialize<StatusMessage>(array);
                    Logger.LogFormat("{0}: Received status message from server: {1}", this, errorMessage.Msg);
                }
                catch (Exception e)
                {
                    Logger.LogErrorFormat("{0}: Exception from " + nameof(OnReceive) + ": {1}", this, e);
                }

                break;
            }
        }
    }

    void FastDecodeCbor(byte[] array)
    {
        var d = new SimpleCborDecoder(array);

        if (d.GetNextMapCount() != 3) return;

        if (!d.CompareNextString("op") || !d.CompareNextString("publish")) return;

        if (!d.CompareNextString("topic") || d.DecodeNextString() is not { } messageTopic) return;

        if (!d.CompareNextString("msg") || d.GetNextMapCount() != 3) return;

        if (!d.CompareNextString("secs") || d.GetNextInt() == null) return;

        if (!d.CompareNextString("nsecs") || d.GetNextInt() == null) return;

        if (!d.CompareNextString("bytes") || d.GetNextByteArraySegment() is not var (msgOffset, msgLength)) return;

        if (!subscribersByTopic.TryGetValue(messageTopic, out var manager))
        {
            //Logger.LogFormat("{0}: Received unknown message from topic '{1}'", this, messageTopic);
            return;
        }

        manager.HandleRaw(new ReadOnlySpan<byte>(array, msgOffset, msgLength));
    }

    internal Task PostAsync(SerializableMessage message, CancellationToken token = default)
    {
        var tcs = TaskUtils.CreateCompletionSource();
        outputWriter.TryWrite(new Command(message, tcs, token));
        return tcs.Task;
    }

    internal void Post(SerializableMessage message)
    {
        outputWriter.TryWrite(new Command(message, null));
    }

    #endregion

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

    public string Advertise<T>(string topic, out RosbridgePublisher<T> publisher) where T : IMessage, new()
    {
        (string id, publisher) = TaskUtils.RunSync(() => AdvertiseAsync<T>(topic));
        return id;
    }

    public async ValueTask<(string id, RosbridgePublisher<T> publisher)>
        AdvertiseAsync<T>(string topic, CancellationToken token = default)
        where T : IMessage, new()
    {
        if (topic is null) BuiltIns.ThrowArgumentNull(nameof(topic));

        AssertIsAlive();

        string resolvedTopic = ResolveResourceName(topic);
        string messageType = BuiltIns.GetMessageType<T>();

        if (publishersByTopic.TryGetValue(topic, out var existingPublisher))
        {
            if (existingPublisher is not RosbridgePublisher<T> validatedPublisher)
            {
                throw new RosInvalidMessageTypeException(topic, existingPublisher.TopicType, messageType);
            }

            return (validatedPublisher.Advertise(), validatedPublisher);
        }

        var publisher = new RosbridgePublisher<T>(this, topic, messageType);
        publishersByTopic[resolvedTopic] = publisher;

        var message = new GenericMessage
        {
            Op = "advertise",
            Topic = topic,
            Type = messageType
        };

        try
        {
            await PostAsync(message, token);
        }
        catch
        {
            publishersByTopic.Remove(resolvedTopic);
            throw;
        }

        return (publisher.Advertise(), publisher);
    }

    async ValueTask<(string id, IRosPublisher<T> publisher)> IRosClient.AdvertiseAsync<T>(
        string topic, bool latchingEnabled, CancellationToken token)
    {
        var (id, publisher) = await AdvertiseAsync<T>(topic, token);
        return (id, publisher);
    }

    internal Task RemovePublisherAsync(string topic)
    {
        var message = new GenericMessage
        {
            Op = "unadvertise",
            Topic = topic,
        };

        return PostAsync(message);
    }

    public string Subscribe<T>(string topic, RosCallback<T> callback, out RosbridgeSubscriber<T> subscriber,
        IRosbridgeSubscriptionProfile? profile = null)
        where T : IMessage, new()
    {
        (string id, subscriber) = TaskUtils.RunSync(() => SubscribeAsync(topic, callback, profile));
        return id;
    }

    public string Subscribe<T>(string topic, Action<T> callback, out RosbridgeSubscriber<T> subscriber,
        IRosbridgeSubscriptionProfile? profile = null)
        where T : IMessage, new()
    {
        return Subscribe(topic, new ActionRosCallback<T>(callback), out subscriber, profile);
    }

    public ValueTask<(string id, RosbridgeSubscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, Action<T> callback, IRosbridgeSubscriptionProfile? profile = null,
            CancellationToken token = default)
        where T : IMessage, new()
    {
        return SubscribeAsync(topic, new ActionRosCallback<T>(callback), profile, token);
    }

    public async ValueTask<(string id, RosbridgeSubscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, RosCallback<T> callback, IRosbridgeSubscriptionProfile? profile = null,
            CancellationToken token = default)
        where T : IMessage, new()
    {
        if (topic is null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (callback is null) BuiltIns.ThrowArgumentNull(nameof(callback));
        AssertIsAlive();

        var generator = new T();
        string messageType = generator.RosMessageType;
        string resolvedTopic = ResolveResourceName(topic);

        if (subscribersByTopic.TryGetValue(topic, out var existingSubscriber))
        {
            if (existingSubscriber is not RosbridgeSubscriber<T> validatedSubscriber)
            {
                throw new RosInvalidMessageTypeException(topic, existingSubscriber.TopicType, messageType);
            }

            return (validatedSubscriber.Subscribe(callback), validatedSubscriber);
        }

        var subscriber = new RosbridgeSubscriber<T>(this, topic, messageType, generator.CreateDeserializer());
        subscribersByTopic[resolvedTopic] = subscriber;
        string id = subscriber.Subscribe(callback);

        SerializableMessage message = profile != null
            ? new FullSubscribeMessage
            {
                Topic = topic,
                Type = messageType,
                ThrottleRate = profile.ThrottleRate,
                QueueLength = profile.QueueLength
            }
            : new SubscribeMessage
            {
                Topic = topic,
                Type = messageType
            };

        try
        {
            await PostAsync(message, token);
        }
        catch
        {
            subscribersByTopic.Remove(resolvedTopic);
            throw;
        }

        return (id, subscriber);
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(string topic,
        Action<T> callback, IRosSubscriptionProfile? profile, CancellationToken token)
    {
        var rosbridgeProfile = profile as IRosbridgeSubscriptionProfile;
        var (id, subscriber) = await SubscribeAsync(topic, callback, rosbridgeProfile, token);
        return (id, subscriber);
    }

    async ValueTask<(string id, IRosSubscriber<T> subscriber)> IRosClient.SubscribeAsync<T>(string topic,
        RosCallback<T> callback, IRosSubscriptionProfile? profile, CancellationToken token)
    {
        var rosbridgeProfile = profile as IRosbridgeSubscriptionProfile;
        var (id, subscriber) = await SubscribeAsync(topic, callback, rosbridgeProfile, token);
        return (id, subscriber);
    }

    internal Task RemoveSubscriberAsync(string topic)
    {
        subscribersByTopic.Remove(topic);

        var message = new GenericMessage
        {
            Op = "unsubscribe",
            Topic = topic,
        };

        return PostAsync(message);
    }

    public Task AdvertiseServiceAsync(string service, string serviceType)
    {
        var message = new GenericServiceMessage
        {
            Op = "advertise_service",
            Service = service,
            Type = serviceType
        };

        return PostAsync(message);
    }

    public ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, ValueTask> callback,
        CancellationToken token = default) where T : IService, new()
    {
        return true.AsTaskResult();
    }


    public Task UnadvertiseServiceAsync(string service, CancellationToken token = default)
    {
        var message = new GenericServiceMessage
        {
            Op = "unadvertise_service",
            Service = service,
        };

        return PostAsync(message, token);
    }

    ValueTask IRosClient.UnadvertiseServiceAsync(string name, CancellationToken token)
    {
        return default;
    }

    public async ValueTask CallServiceAsync<TRequest, TResponse>(string serviceName,
        IService<TRequest, TResponse> service, CancellationToken token = default)
        where TRequest : IRequest
        where TResponse : IResponse
    {
        AssertIsAlive();

        int currentId = Interlocked.Increment(ref currentServiceRequestId);
        string currentIdStr = currentId.ToString();
        var message = new CallServiceMessage<TRequest>
        {
            Id = currentIdStr,
            Service = serviceName,
            Args = (TRequest)service.Request
        };

        var tcs = TaskUtils.CreateCompletionSource<TResponse>();

        void ProcessResponse(byte[]? array, string? errorMessage)
        {
            if (token.IsCancellationRequested)
            {
                tcs.TrySetCanceled(token);
                return;
            }

            if (errorMessage != null)
            {
                tcs.TrySetException(new RosbridgeException(
                    $"Service call to '{serviceName}' failed! Reason: {errorMessage}"));
                return;
            }

            if (array is null)
            {
                tcs.TrySetException(new RosbridgeException(
                    $"Service call to '{serviceName}' failed! Reason: Not given."));
                return;
            }

            try
            {
                var response = Utf8Json.JsonSerializer.Deserialize<ServiceResponseMessage<TResponse>>(array);

                if (response is null || response.Values is not { } values)
                {
                    tcs.TrySetException(new RosbridgeException(
                        $"Service call to '{serviceName}' with id {currentIdStr} " +
                        $"returned a null response"));
                    return;
                }

                tcs.TrySetResult(values);
            }
            catch (Exception e)
            {
                tcs.TrySetException(new RosbridgeException($"Service call to '{serviceName}' failed!", e));
            }
        }

        calledServices.TryAdd((serviceName, currentId), ProcessResponse);

        // ReSharper disable once UseAwaitUsing
        using (token.Register(() => tcs.TrySetCanceled(token)))
        {
            try
            {
                await PostAsync(message, token);
                service.Response = await tcs.Task;
            }
            catch (Exception e) when (e is not (RosbridgeException or OperationCanceledException))
            {
                throw new RosbridgeException($"Service call '{serviceName}' failed", e);
            }
            finally
            {
                calledServices.TryRemove((serviceName, currentId), out _);
            }
        }
    }

    ValueTask IRosClient.CallServiceAsync<TRequest, TResponse>(string serviceName,
        IService<TRequest, TResponse> service, bool persistent, CancellationToken token)
    {
        return CallServiceAsync(serviceName, service, token);
    }

    internal async ValueTask<string[]> GetSystemPublishers(string topic, CancellationToken token = default)
    {
        var service = new Publishers(new PublishersRequest(topic));
        await CallServiceAsync("/rosapi/publishers", service, token);
        return service.Response.Publishers_;
    }

    internal async ValueTask<string[]> GetSystemSubscribers(string topic, CancellationToken token = default)
    {
        var service = new Subscribers(new SubscribersRequest(topic));
        await CallServiceAsync("/rosapi/subscribers", service, token);
        return service.Response.Subscribers_;
    }

    async ValueTask<SubscriberState[]> GetSubscriberStatisticsCoreAsync(CancellationToken token)
    {
        var tasks = subscribersByTopic.Select(
            pair => pair.Value.GetStateAsync(token).AsTask());
        var result = await Task.WhenAll(tasks);
        cachedSubscriberStats.Value = result;
        return result;
    }

    public ValueTask<SubscriberState[]> GetSubscriberStatisticsAsync(
        CancellationToken token = default)
    {
        return cachedSubscriberStats.TryGet() is { } systemState
            ? systemState.AsTaskResult()
            : GetSubscriberStatisticsCoreAsync(token);
    }

    async ValueTask<PublisherState[]> GetPublisherStatisticsCoreAsync(CancellationToken token)
    {
        var tasks = publishersByTopic.Select(
            pair => pair.Value.GetStateAsync(token).AsTask());
        var result = await Task.WhenAll(tasks);
        cachedPublisherStats.Value = result;
        return result;
    }

    public ValueTask<PublisherState[]> GetPublisherStatisticsAsync(CancellationToken token = default)
    {
        return cachedPublisherStats.TryGet() is { } systemState
            ? systemState.AsTaskResult()
            : GetPublisherStatisticsCoreAsync(token);
    }

    public async ValueTask<TopicNameType[]> GetSystemPublishedTopicsAsync(CancellationToken token = default)
    {
        var systemPublishers = (await GetSystemStateAsync(token)).Publishers;
        var topicsWithPublisher = new HashSet<string>(systemPublishers.Select(tuple => tuple.Topic));

        var systemTopics = await GetSystemTopicsAsync(token);
        return systemTopics.Where(topic => topicsWithPublisher.Contains(topic.Topic)).ToArray();
    }

    public async ValueTask<TopicNameType[]> GetSystemTopicsAsync(CancellationToken token = default)
    {
        var service = new Topics();
        await CallServiceAsync("/rosapi/topics", service, token);
        return service.Response.Topics_.Zip(service.Response.Types)
            .Select(((string topic, string type) pair) => new TopicNameType(pair.topic, pair.type))
            .ToArray();
    }

    public async ValueTask<string[]> GetParameterNamesAsync(CancellationToken token = default)
    {
        const string serviceName = "/rosapi/get_param_names";
        var service = new GetParamNames();
        await CallServiceAsync(serviceName, service, token);
        return service.Response.Names;
    }

    public async ValueTask<RosValue> GetParameterAsync(string key, CancellationToken token = default)
    {
        const string serviceName = "/rosapi/get_param";
        var service = new GetParam(new GetParamRequest(key, ""));
        await CallServiceAsync(serviceName, service, token);
        return new RosValue(service.Response.Value);
    }

    async ValueTask<string[]> GetSystemServicesAsync(CancellationToken token = default)
    {
        const string serviceName = "/rosapi/services";
        var service = new Services();
        await CallServiceAsync(serviceName, service, token);
        return service.Response.Services_;
    }

    async ValueTask<string[]> GetSystemServiceProvidersAsync(string serviceToQuery, CancellationToken token)
    {
        const string serviceName = "/rosapi/service_providers";
        var service = new ServiceProviders(new ServiceProvidersRequest(serviceToQuery));
        await CallServiceAsync(serviceName, service, token);
        return service.Response.Providers;
    }

    public async ValueTask<bool> IsServiceAvailableAsync(string service, CancellationToken token = default)
    {
        return (await GetSystemServiceProvidersAsync(service, token)).Length != 0;
    }

    public async ValueTask<string[]> GetSystemNodesAsync(CancellationToken token = default)
    {
        const string serviceName = "/rosapi/nodes";
        var service = new Nodes();
        await CallServiceAsync(serviceName, service, token);
        return service.Response.Nodes_;
    }

    async ValueTask<(string[], string[], string[])> GetSystemNodeDetailsAsync(string node, CancellationToken token)
    {
        const string serviceName = "/rosapi/node_details";
        var service = new NodeDetails(new NodeDetailsRequest(node));
        await CallServiceAsync(serviceName, service, token);
        return (service.Response.Publishing, service.Response.Subscribing, service.Response.Services);
    }

    async ValueTask<SystemState> GetSystemStateCoreAsync(CancellationToken token)
    {
        string[] nodes = await GetSystemNodesAsync(token);
        var publishers = new Dictionary<string, List<string>>();
        var subscribers = new Dictionary<string, List<string>>();
        var providers = new Dictionary<string, List<string>>();

        foreach (string node in nodes)
        {
            var (publishing, subscribing, providing) = await GetSystemNodeDetailsAsync(node, token);
            foreach (string topic in publishing)
            {
                EnsureHasEntry(publishers, topic, node);
            }

            foreach (string topic in subscribing)
            {
                EnsureHasEntry(subscribers, topic, node);
            }

            foreach (string topic in providing)
            {
                EnsureHasEntry(providers, topic, node);
            }
        }

        var systemState = new SystemState(
            ToTopicTuples(publishers),
            ToTopicTuples(subscribers),
            ToTopicTuples(providers)
        );

        cachedSystemState.Value = systemState;
        return systemState;

        static void EnsureHasEntry(Dictionary<string, List<string>> dict, string key, string value)
        {
            if (dict.TryGetValue(key, out var list))
            {
                list.Add(value);
                return;
            }

            dict.Add(key, new List<string> { value });
        }

        static TopicTuple[] ToTopicTuples(Dictionary<string, List<string>> dict) =>
            dict.Select(pair => new TopicTuple(pair.Key, pair.Value.ToArray())).ToArray();
    }

    public ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default)
    {
        return cachedSystemState.TryGet() is { } systemState
            ? systemState.AsTaskResult()
            : GetSystemStateCoreAsync(token);
    }
    // ------------------

    void AssertIsAlive()
    {
        if (runningTs.IsCancellationRequested)
        {
            BuiltIns.ThrowObjectDisposed(nameof(RosbridgeClient), "Client is no longer valid");
        }
    }

    public override string ToString()
    {
        return $"[{nameof(RosbridgeClient)} BridgeUri='{BridgeUri}']";
    }

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

        var serviceCallbacks = calledServices.Values.ToArray();
        foreach (var serviceCallback in serviceCallbacks)
        {
            serviceCallback(null, "Shutting down client!");
        }

        calledServices.Clear();

        await Task.WhenAll(tasks);

        outputWriter.Complete();
        runningTs.CancelNoThrow(this);

        if (webSocket.State is WebSocketState.Open or WebSocketState.Connecting)
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", default)
                .AwaitNoThrow(this);
        }

        await sendQueueTask;
        await receiveQueueTask;

        webSocket.Dispose();
    }
}

internal readonly struct Command
{
    public readonly SerializableMessage message;
    public readonly TaskCompletionSource? tcs;
    public readonly CancellationToken token;

    public Command(SerializableMessage message, TaskCompletionSource? tcs, CancellationToken token = default)
    {
        this.message = message;
        this.tcs = tcs;
        this.token = token;
    }
}

public interface IRosbridgeSubscriptionProfile : IRosSubscriptionProfile
{
    public int ThrottleRate { get; }
    public int QueueLength { get; }
}