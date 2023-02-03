using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.RclInterop;
using Iviz.Tools;

namespace Iviz.Roslib2;

public sealed class Ros2Subscriber<TMessage> : Ros2Subscriber, IRos2Subscriber, IRosSubscriber<TMessage>
    where TMessage : IMessage, new()
{
    static RosCallback<TMessage>[] EmptyCallback => Array.Empty<RosCallback<TMessage>>();

    readonly Dictionary<string, RosCallback<TMessage>> callbacksById = new();
    readonly Ros2Client client;

    RosCallback<TMessage>[] callbacks = EmptyCallback; // cache to iterate through callbacks quickly
    Task? task;
    int totalSubscribers;
    bool disposed;

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
        var token = CancellationToken;
        var rclSubscriber = Subscriber;
        var deserializer = new TMessage().CreateDeserializer();
        var receiverInfo = new Ros2Receiver(Topic, TopicType);
        var messageHandler = new RclDeserializeHandler<TMessage>(this, deserializer);
        using var gcHandle = new GCHandleWrapper(messageHandler);

        while (true)
        {
            messageHandler.Clear();

            await signal.WaitAsync(token);

            if (!rclSubscriber.Take(gcHandle.ptr, out receiverInfo.guid))
            {
                continue;
            }

            UpdateReceiverInfo(receiverInfo.guid, messageHandler.messageLength);
            if (!messageHandler.hasMessage) // false if paused or exception
            {
                continue;
            }

            var msg = messageHandler.message!;
            foreach (var callback in callbacks)
            {
                try
                {
                    callback.Handle(msg, receiverInfo);
                }
                catch (Exception e)
                {
                    Logger.LogErrorFormat("{0}: Exception from " + nameof(Run) + ": {1}", this, e);
                }
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
        int currentCount = Interlocked.Increment(ref totalSubscribers);
        int lastId = currentCount - 1;
        return lastId == 0 ? Topic : $"{Topic}-{lastId.ToString()}";
    }

    public SubscriberState GetState() => TaskUtils.RunSync(GetStateAsync);

    public async ValueTask<SubscriberState> GetStateAsync(CancellationToken token)
    {
        var receiverStates = await GetReceiverStates(client, token);
        return new Ros2SubscriberState(Topic, TopicType, callbacksById.Keys.ToArray(), receiverStates, Profile);
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

    string IRosSubscriber.Subscribe(Action<IMessage, IRosConnection> callback)
    {
        void Callback(in TMessage msg, IRosConnection receiver) => callback(msg, receiver);
        return Subscribe(new ActionRosCallback<TMessage>(Callback));
    }

    public string Subscribe(Action<TMessage> callback)
    {
        return Subscribe(new DirectRosCallback<TMessage>(callback));
    }

    public string Subscribe(RosCallback<TMessage> callback)
    {
        if (callback is null) BuiltIns.ThrowArgumentNull(nameof(callback));

        AssertIsAlive();

        string id = GenerateId();
        callbacksById.Add(id, callback);
        callbacks = callbacksById.Values.ToArray();
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
        callbacks = callbacksById.Values.ToArray();

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
            callbacks = callbacksById.Values.ToArray();
        }
        else
        {
            callbacks = EmptyCallback;
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

        runningTs.Cancel();

        await task.AwaitNoThrow(2000, this, token);
        await client.Rcl.UnsubscribeAsync(Subscriber, default).AwaitNoThrow(this);

        callbacksById.Clear();
        callbacks = EmptyCallback;

        client.RemoveSubscriber(this);
    }
}

public class Ros2Subscriber : Signalizable
{
    RclSubscriber? subscriber;
    PublisherStats[] publisherStats = new PublisherStats[32];
    int numPublishers;

    protected readonly CancellationTokenSource runningTs = new();

    protected bool IsAlive => !runningTs.IsCancellationRequested;

    internal RclSubscriber Subscriber
    {
        get => subscriber ?? throw new NullReferenceException("Subscriber has not been initialized!");
        set => subscriber = value;
    }

    public bool IsPaused { get; set; }
    public CancellationToken CancellationToken => runningTs.Token;
    public string Topic => Subscriber.Topic;
    public string TopicType => Subscriber.TopicType;
    public int NumPublishers => Subscriber.GetNumPublishers();
    public QosProfile Profile => Subscriber.Profile;

    protected void UpdateReceiverInfo(in Guid guid, int lengthInBytes)
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

        AddNewEntry(in guid, lengthInBytes);
    }

    void AddNewEntry(in Guid guid, int lengthInBytes)
    {
        if (numPublishers == publisherStats.Length)
        {
            Array.Resize(ref publisherStats, 2 * publisherStats.Length);
        }

        publisherStats[numPublishers++] = new PublisherStats(guid, lengthInBytes);

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

            int cmp = key.CompareTo(in arr[mid].guid);

            switch (cmp)
            {
                case 0:
                    index = mid;
                    return true;
                case < 0:
                    max = mid - 1;
                    break;
                default:
                    min = mid + 1;
                    break;
            }
        }

        index = default;
        return false;
    }

    protected async ValueTask<List<Ros2ReceiverState>> GetReceiverStates(Ros2Client client, CancellationToken token)
    {
        var publishers = await client.Rcl.GetPublisherInfoAsync(Topic, token);

        var knownPublishers = publishers.ToDictionary(static info => info.Guid);

        var contactedPublishers =
            publisherStats.Take(numPublishers).ToDictionary(static stats => stats.guid);

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

        return receiverStates;
    }
    
    struct PublisherStats : IComparable<PublisherStats>
    {
        public readonly Guid guid;
        public long bytesReceived;
        public int numReceived;

        public PublisherStats(in Guid guid, int bytesReceived)
        {
            this.guid = guid;
            this.bytesReceived = bytesReceived;
            numReceived = 1;
        }

        public int CompareTo(PublisherStats other) => guid.CompareTo(other.guid);
    }    
}