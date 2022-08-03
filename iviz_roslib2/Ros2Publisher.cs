using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.Rcl;
using Iviz.Tools;

namespace Iviz.Roslib2;

public sealed class Ros2Publisher<TMessage> : IRos2Publisher, IRosPublisher<TMessage> where TMessage : IMessage
{
    readonly Ros2Client client;
    readonly RclPublisher publisher;
    readonly List<string> ids = new();
    readonly CancellationTokenSource runningTs = new();
    int totalPublishers;
    bool disposed;

    int numSent;
    long bytesSent;

    public CancellationToken CancellationToken => runningTs.Token;
    public string Topic => publisher.Topic;
    public string TopicType => publisher.TopicType;
    public int NumSubscribers => publisher.GetNumSubscribers();
    public bool LatchingEnabled { get; set; }
    public bool IsAlive => !CancellationToken.IsCancellationRequested;
    public QosProfile Profile => publisher.Profile;

    internal Ros2Publisher(Ros2Client client, RclPublisher publisher)
    {
        this.client = client;
        this.publisher = publisher;
    }

    void AssertIsAlive()
    {
        if (!IsAlive)
        {
            throw new ObjectDisposedException("this", "This is not a valid publisher");
        }
    }

    public string Advertise()
    {
        AssertIsAlive();

        string id = GenerateId();
        ids.Add(id);
        return id;
    }

    public bool Unadvertise(string id, CancellationToken token = default)
    {
        if (id is null) BuiltIns.ThrowArgumentNull(nameof(id));
        if (!IsAlive)
        {
            return true;
        }

        bool removed = RemoveId(id);
        if (ids.Count == 0)
        {
            Dispose();
        }

        return removed;
    }

    public async ValueTask<bool> UnadvertiseAsync(string id, CancellationToken token = default)
    {
        if (id is null) BuiltIns.ThrowArgumentNull(nameof(id));
        if (!IsAlive)
        {
            return true;
        }

        bool removed = RemoveId(id);
        if (ids.Count == 0)
        {
            await DisposeAsync(token);
        }

        return removed;
    }

    string GenerateId()
    {
        int currentCount = Interlocked.Increment(ref totalPublishers);
        int lastId = currentCount - 1;
        return lastId == 0 ? Topic : $"{Topic}-{lastId.ToString()}";
    }

    bool RemoveId(string topicId)
    {
        return ids.Remove(topicId);
    }

    public bool ContainsId(string id)
    {
        if (id is null) BuiltIns.ThrowArgumentNull(nameof(id));
        return ids.Contains(id);
    }

    public bool MessageTypeMatches(Type type)
    {
        return type == typeof(TMessage);
    }

    public PublisherState GetState() => TaskUtils.RunSync(GetStateAsync);

    public async ValueTask<PublisherState> GetStateAsync(CancellationToken token)
    {
        var subscribers = await client.Rcl.GetSubscriberInfoAsync(Topic, token);

        var senderStates = subscribers.Select(subscriber =>
            new Ros2SenderState(subscriber.Guid, subscriber.Profile)
            {
                RemoteId = subscriber.NodeName.ToString(),
                BytesSent = bytesSent,
                NumSent = numSent,
                TopicType = publisher.TopicType
            }
        ).ToArray();

        return new Ros2PublisherState(Topic, TopicType, ids, senderStates, Profile);
    }

    public void Publish(in TMessage message)
    {
        if (message is null) BuiltIns.ThrowArgumentNull(nameof(message));

        AssertIsAlive();
        message.RosValidate();

        int newBytesSent = publisher.Publish(message);
        Interlocked.Increment(ref numSent);
        Interlocked.Add(ref bytesSent, newBytesSent);
    }

    public ValueTask PublishAsync(in TMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        Publish(message);
        return default;
    }

    void IRosPublisher.Publish(IMessage message)
    {
        if (message is TMessage tMessage)
        {
            Publish(tMessage);
            return;
        }

        RosInvalidMessageTypeException.Throw();
    }

    ValueTask IRosPublisher.PublishAsync(IMessage message, RosPublishPolicy policy, CancellationToken token)
    {
        if (message is TMessage tMessage)
        {
            return PublishAsync(tMessage, policy, token);
        }
        
        RosInvalidMessageTypeException.Throw();
        return default; // unreachable
    }

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return default;
        disposed = true;
        
        runningTs.Cancel();
        ids.Clear();

        client.RemovePublisher(this);
        return client.Rcl.DisposePublisher(publisher, default).AwaitNoThrow(this).AsValueTask();
    }
}