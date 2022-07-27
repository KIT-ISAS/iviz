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

    RosCallback<TMessage>[] cachedCallbacks = EmptyCallback; // cache to iterate through callbacks quickly
    RclSubscriber? subscriber;
    Task? task;

    int totalSubscribers;
    bool disposed;

    PublisherStats[] publisherStats = new PublisherStats[32];
    int numPublishers;

    bool IsAlive => !runningTs.IsCancellationRequested;

    internal RclSubscriber Subscriber
    {
        private get => subscriber ?? throw new NullReferenceException("Subscriber has not been initialized!");
        set => subscriber = value;
    }

    public CancellationToken CancellationToken => runningTs.Token;
    public string Topic => Subscriber.Topic;
    public string TopicType => Subscriber.TopicType;
    public int NumPublishers => Subscriber.GetNumPublishers();
    public bool IsPaused { get; set; }

    internal Ros2Subscriber(Ros2Client client)
    {
        this.client = client;
    }

    internal void Start()
    {
        task = TaskUtils.Run(() => Run().AwaitNoThrow(this), CancellationToken);
    }

    async ValueTask Run()
    {
        var instance = new TMessage();
        if (instance is not IDeserializableRos2<TMessage> generator)
        {
            Logger.LogErrorFormat("{0}: Failed to create generator for type '{1}'!", this, instance.RosMessageType);
            return;
        }

        var receiverInfo = new Ros2Receiver(Topic, TopicType);

        while (true)
        {
            await signal.WaitAsync(CancellationToken);
            ProcessMessages();
        }

        void ProcessMessages()
        {
            while (Subscriber.TryTakeMessage(out var span, out receiverInfo.guid))
            {
                if (IsPaused) continue;
                var msg = ReadBuffer2.Deserialize(generator, span);
                MessageCallback(msg, receiverInfo);
                UpdateReceiverInfo(receiverInfo.guid, span.Length);
            }
        }
    }

    void ISignalizable.Signal() => signal.Release();

    void UpdateReceiverInfo(in Guid guid, int lengthInBytes)
    {
        bool success = numPublishers <= 3
            ? LinearSearch(publisherStats, numPublishers, guid, out int index)
            : BinarySearch(publisherStats, numPublishers, guid, out index);
        
        if (success)
        {
            ref var publisherStat = ref publisherStats[index];
            publisherStat.bytesReceived += lengthInBytes;
            publisherStat.numReceived++;
            return;
        }

        if (numPublishers == publisherStats.Length)
        {
            Array.Resize(ref publisherStats, 2 * publisherStats.Length);
        }
        
        publisherStats[numPublishers++] = new PublisherStats
        {
            guid = guid,
            numReceived = 1,
            bytesReceived = lengthInBytes
        };
        
        Array.Sort(publisherStats, 0, numPublishers);
    }

    static bool LinearSearch(PublisherStats[] arr, int length, in Guid key, out int index)
    {
        for (int i = 0; i < length; i++)
        {
            if (arr[i].guid == key)
            {
                index = i;
                return true;
            }
        }

        index = 0;
        return false;
    }


    static bool BinarySearch(PublisherStats[] arr, int length, in Guid key, out int index)
    {
        int min = 0;
        int max = length - 1;

        while (min <= max)
        {
            int mid = (min + max) / 2;
            ref readonly var guid = ref arr[mid].guid;
            
            if (key == guid)
            {
                index = mid;
                return true;
            }

            if (key < guid)
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }

        index = default;
        return false;
    }

    void MessageCallback(in TMessage msg, IRosConnection receiver)
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

    public async ValueTask<SubscriberState> GetStateAsync(CancellationToken token)
    {
        await client.Rcl.GetPublisherInfoAsync(Topic, token);

        var publishers = await client.Rcl.GetPublisherInfoAsync(Topic, token);

        var knownPublishers = publishers.ToDictionary(info => info.Guid);

        var contactedPublishers =
            publisherStats.Take(numPublishers).ToDictionary(stats => stats.guid);

        var receiverStates = new List<Ros2ReceiverState>(knownPublishers.Count);

        foreach (var publisher in knownPublishers.Values)
        {
            var state = contactedPublishers.TryGetValue(publisher.Guid, out var stats)
                ? new Ros2ReceiverState(publisher.Guid, publisher.Profile)
                {
                    RemoteId = publisher.NodeName.ToString(),
                    BytesReceived = stats.bytesReceived,
                    NumReceived = stats.numReceived,
                    TopicType = publisher.TopicType
                }
                : new Ros2ReceiverState(publisher.Guid, publisher.Profile)
                {
                    RemoteId = publisher.NodeName.ToString(),
                    BytesReceived = 0,
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
                static stats => new Ros2ReceiverState
                {
                    RemoteId = null,
                    BytesReceived = stats.bytesReceived,
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

    string IRosSubscriber.Subscribe(Action<IMessage, IRosConnection> callback) =>
        Subscribe((in TMessage msg, IRosConnection receiver) => callback(msg, receiver));

    public string Subscribe(Action<TMessage> callback) =>
        Subscribe((in TMessage t, IRosConnection _) => callback(t));

    public string Subscribe(RosCallback<TMessage> callback)
    {
        if (callback is null) BuiltIns.ThrowArgumentNull(nameof(callback));

        AssertIsAlive();

        string id = GenerateId();
        callbacksById.Add(id, callback);
        cachedCallbacks = callbacksById.Values.ToArray();
        return id;
    }

    public bool Unsubscribe(string id)
    {
        if (id == null) BuiltIns.ThrowArgumentNull(nameof(id));

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
        if (disposed) return;
        disposed = true;

        await client.Rcl.UnsubscribeAsync(Subscriber, default).AwaitNoThrow(this);

        runningTs.Cancel();
        await task.AwaitNoThrow(2000, this, token);

        callbacksById.Clear();
        cachedCallbacks = EmptyCallback;

        client.RemoveSubscriber(this);
    }

    struct PublisherStats
    {
        public Guid guid;
        public long bytesReceived;
        public int numReceived;
    }
}