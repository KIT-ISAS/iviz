using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.Rcl;
using Iviz.Tools;

namespace Iviz.Roslib2;

public class Ros2Publisher<TMessage> : IRos2Publisher, IRosPublisher<TMessage> where TMessage : IMessage
{
    readonly Ros2Client client;
    readonly RclPublisher publisher;
    readonly List<string> ids = new();
    readonly CancellationTokenSource runningTs = new();
    int totalPublishers;
    bool disposed;

    public CancellationToken CancellationToken => runningTs.Token;
    public string Topic => publisher.Topic;
    public string TopicType => publisher.TopicType;
    public int NumSubscribers => publisher.NumSubscribers;
    public bool LatchingEnabled { get; set; }

    public bool IsAlive => !CancellationToken.IsCancellationRequested;

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
        if (!IsAlive)
        {
            return true;
        }

        bool removed = RemoveId(id ?? throw new ArgumentNullException(nameof(id)));

        if (ids.Count == 0)
        {
            Dispose();
        }

        return removed;
    }

    public async ValueTask<bool> UnadvertiseAsync(string id, CancellationToken token = default)
    {
        if (!IsAlive)
        {
            return true;
        }

        bool removed = RemoveId(id ?? throw new ArgumentNullException(nameof(id)));

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

    public PublisherState GetState() =>
        new(Topic, TopicType, ids, Array.Empty<SenderState>());

    public ValueTask<PublisherState> GetStateAsync()
    {
        return new ValueTask<PublisherState>(GetState());
    }

    public void Publish(in TMessage message)
    {
        if (message is null) BuiltIns.ThrowArgumentNull(nameof(message));

        AssertIsAlive();
        message.RosValidate();

        publisher.Publish(message);
    }

    public ValueTask PublishAsync(in TMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        Publish(message);
        return new ValueTask();
    }

    void IRosPublisher.Publish(IMessage message)
    {
        if (message is not TMessage tMessage)
        {
            throw new RosInvalidMessageTypeException("Type does not match publisher.");
        }

        Publish(tMessage);
    }

    ValueTask IRosPublisher.PublishAsync(IMessage message, RosPublishPolicy policy, CancellationToken token)
    {
        if (message is not TMessage tMessage)
        {
            throw new RosInvalidMessageTypeException("Type does not match publisher.");
        }

        return PublishAsync(tMessage, policy, token);
    }

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return;
        disposed = true;
        runningTs.Cancel();
        ids.Clear();

        client.RemovePublisher(this);
        await client.Rcl.UnadvertiseAsync(publisher, token).AwaitNoThrow(this);
    }
}