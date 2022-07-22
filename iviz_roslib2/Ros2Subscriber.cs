using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.Rcl;
using Iviz.Tools;

namespace Iviz.Roslib2;

public sealed class Ros2Subscriber<TMessage> : IRos2Subscriber, IRosSubscriber<TMessage>, ISignalizable
    where TMessage : IMessage, new()
{
    static RosCallback<TMessage>[] EmptyCallback => Array.Empty<RosCallback<TMessage>>();

    readonly Dictionary<string, RosCallback<TMessage>> callbacksById = new();
    readonly CancellationTokenSource runningTs = new();
    readonly Ros2Client client;
    readonly SemaphoreSlim signal = new(0);
    Task? task;

    RosCallback<TMessage>[] cachedCallbacks = EmptyCallback; // cache to iterate through callbacks quickly
    int totalSubscribers;
    bool disposed;

    bool IsAlive => !runningTs.IsCancellationRequested;

    RclSubscriber? subscriber;

    internal RclSubscriber Subscriber
    {
        private get => subscriber ?? throw new NullReferenceException("Subscriber has not been initialized!");
        set => subscriber = value;
    }
    
    public CancellationToken CancellationToken => runningTs.Token;
    public string Topic => Subscriber.Topic;
    public string TopicType => Subscriber.TopicType;
    public int NumPublishers => Subscriber.NumPublishers;
    public bool IsPaused { get; set; }
   
    readonly PublisherStats[] publisherStats = new PublisherStats[8];
    int numPublishers;

    internal Ros2Subscriber(Ros2Client client)
    {
        this.client = client;
    }

    internal void Start()
    {
        task = Task.Run(Run, CancellationToken);
    }

    async Task Run()
    {
        var instance = new TMessage();
        if (instance is not IDeserializableRos2<TMessage> generator)
        {
            Logger.LogErrorFormat("{0}: Failed to create generator for type '{1}'!", this, instance.RosMessageType);
            return;
        }

        var receiverInfo = new Ros2Receiver
        {
            Topic = Topic,
            Type = TopicType
        };

        while (true)
        {
            try
            {
                await signal.WaitAsync(CancellationToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }

            void ProcessMessages()
            {
                while (Subscriber.TryTakeMessage(out var span, out var guid) || IsPaused)
                {
                    var msg = ReadBuffer2.Deserialize(generator, span);
                    MessageCallback(msg, receiverInfo);
                    UpdateReceiverInfo(guid, span.Length);
                }
            }
            
            ProcessMessages();
        }
    }

    void ISignalizable.Signal() => signal.Release();

    void UpdateReceiverInfo(in Guid guid, int length)
    {
        for (int i = 0; i < numPublishers; i++)
        {
            ref var publisherStat = ref publisherStats[i];
            if (publisherStat.guid != guid)
            {
                continue;
            }

            publisherStat.bytes += length;
            publisherStat.num++;
            return;
        }

        publisherStats[numPublishers++] = new PublisherStats
        {
            guid = guid,
            num = 1,
            bytes = length
        };
    }

    void MessageCallback(in TMessage msg, IRosReceiver receiver)
    {
        foreach (var callback in cachedCallbacks)
        {
            try
            {
                callback(in msg, receiver);
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Exception from " + nameof(MessageCallback) + ": {1}", this, e);
            }
        }
    }

    void AssertIsAlive()
    {
        if (!IsAlive)
        {
            throw new ObjectDisposedException(nameof(Ros2Subscriber<TMessage>), "This is not a valid subscriber");
        }
    }

    string GenerateId()
    {
        Interlocked.Increment(ref totalSubscribers);
        int prevNumSubscribers = totalSubscribers - 1;
        return prevNumSubscribers == 0 ? Topic : $"{Topic}-{prevNumSubscribers.ToString()}";
    }

    public SubscriberState GetState() => TaskUtils.RunSync(GetStateAsync);

    public async ValueTask<SubscriberState> GetStateAsync()
    {
        var publishers = await client.Rcl.GetPublisherInfoAsync(Topic);

        var knownPublishers = publishers.ToDictionary(info => info.Guid);

        var contactedPublishers =
            publisherStats.Take(numPublishers).ToDictionary(stats => stats.guid);

        var receiverStates = new List<Ros2ReceiverState>(knownPublishers.Count);

        foreach (var publisher in knownPublishers.Values)
        {
            var state = contactedPublishers.TryGetValue(publisher.Guid, out var stats)
                ? new Ros2ReceiverState
                {
                    RemoteId = publisher.NodeName.ToString(),
                    BytesReceived = stats.bytes,
                    Guid = publisher.Guid,
                    NumReceived = stats.num,
                    TopicType = publisher.TopicType
                }
                : new Ros2ReceiverState
                {
                    RemoteId = publisher.NodeName.ToString(),
                    BytesReceived = 0,
                    Guid = publisher.Guid,
                    NumReceived = 0,
                    TopicType = publisher.TopicType
                };

            receiverStates.Add(state);
        }

        var missingPublishers = contactedPublishers
            .Where(key => !knownPublishers.ContainsKey(key.Key))
            .Select(pair => pair.Value);
        
        receiverStates.AddRange(
            missingPublishers.Select(
                stats => new Ros2ReceiverState
                {
                    RemoteId = null,
                    BytesReceived = stats.bytes,
                    Guid = stats.guid,
                    NumReceived = 0,
                    TopicType = null
                }));

        return new SubscriberState(Topic, TopicType, callbacksById.Keys.ToArray(), receiverStates);
    }

    public bool ContainsId(string id)
    {
        if (id is null) BuiltIns.ThrowArgumentNull(nameof(id));
        return callbacksById.ContainsKey(id);
    }

    public bool MessageTypeMatches(Type type)
    {
        return type == typeof(TMessage);
    }

    string IRosSubscriber.Subscribe(Action<IMessage> callback) =>
        Subscribe(msg => callback(msg));

    string IRosSubscriber.Subscribe(Action<IMessage, IRosReceiver> callback) =>
        Subscribe((in TMessage msg, IRosReceiver receiver) => callback(msg, receiver));

    public string Subscribe(Action<TMessage> callback) =>
        Subscribe((in TMessage t, IRosReceiver _) => callback(t));

    public string Subscribe(RosCallback<TMessage> callback)
    {
        if (callback is null)
        {
            BuiltIns.ThrowArgumentNull(nameof(callback));
        }

        AssertIsAlive();

        string id = GenerateId();
        callbacksById.Add(id, callback);
        cachedCallbacks = callbacksById.Values.ToArray();
        return id;
    }

    public bool Unsubscribe(string id)
    {
        if (id == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(id));
        }

        if (!IsAlive)
        {
            return true;
        }

        bool removed = callbacksById.Remove(id);
        cachedCallbacks = callbacksById.Values.ToArray();

        if (callbacksById.Count == 0)
        {
            Dispose();
        }

        return removed;
    }

    public async ValueTask<bool> UnsubscribeAsync(string id, CancellationToken token = default)
    {
        if (id is null) BuiltIns.ThrowArgumentNull(nameof(id));

        if (!IsAlive)
        {
            return true;
        }

        bool removed = callbacksById.Remove(id);
        if (callbacksById.Count != 0)
        {
            cachedCallbacks = callbacksById.Values.ToArray();
        }
        else
        {
            cachedCallbacks = EmptyCallback;
            await DisposeAsync(token).AwaitNoThrow(this);
        }

        return removed;
    }

    public override string ToString() => $"[{nameof(Ros2Subscriber<TMessage>)} {Topic} [{TopicType}] ]";

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        await client.Rcl.DoDisposeAsync(Subscriber);

        runningTs.Cancel();
        await task.AwaitNoThrow(2000, this, token);

        callbacksById.Clear();
        cachedCallbacks = EmptyCallback;

        client.RemoveSubscriber(this);
    }

    struct PublisherStats
    {
        public Guid guid;
        public long bytes;
        public int num;
    }
}

public sealed class Ros2Receiver : IRosReceiver
{
    public string Topic { get; set; }
    public string Type { get; set; }

    public string? RemoteId { get; set; }
    public Endpoint RemoteEndpoint { get; set; }
    public Endpoint Endpoint { get; set; }
    public IReadOnlyCollection<string> RosHeader => Array.Empty<string>();
}