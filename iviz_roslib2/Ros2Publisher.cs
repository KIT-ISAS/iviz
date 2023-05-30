using System.Collections.Concurrent;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.RclInterop;
using Iviz.Tools;

namespace Iviz.Roslib2;

public sealed class Ros2Publisher<TMessage> : BaseRosPublisher<TMessage>, IRos2Publisher
    where TMessage : IMessage, new()
{
    readonly Ros2Client client;
    readonly RclPublisher publisher;
    readonly List<string> ids = new();
    readonly Serializer<TMessage> serializer;

    readonly SemaphoreSlim signal = new(0);
    readonly ConcurrentQueue<TMessage> queue = new();
    readonly Task task;

    int totalPublishers;
    bool disposed;

    int numSent;
    long bytesSent;

    public override int NumSubscribers => publisher.GetNumSubscribers();
    public QosProfile Profile => publisher.Profile;

    internal Ros2Publisher(Ros2Client client, RclPublisher publisher) : base(publisher.Topic, publisher.TopicType)
    {
        this.client = client;
        this.publisher = publisher;
        serializer = new TMessage().CreateSerializer();
        task = Task.Run(() => Run().AwaitNoThrow(this));
    }

    async Task Run()
    {
        var token = CancellationToken;
        var handler = new RclSerializeHandler<TMessage>(serializer);
        using var handle = new GCHandleWrapper(handler);

        while (true)
        {
            await signal.WaitAsync(token);
            if (!queue.TryDequeue(out var message))
            {
                continue;
            }

            handler.message = message;
            publisher.Publish(handle.ptr);

            numSent++;
            bytesSent += handler.messageLength;
        }
    }

    public override string Advertise()
    {
        AssertIsAlive();

        string id = GenerateId();
        ids.Add(id);
        return id;
    }

    public override bool Unadvertise(string id, CancellationToken token = default)
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

    public override async ValueTask<bool> UnadvertiseAsync(string id, CancellationToken token = default)
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

    public override bool ContainsId(string id)
    {
        if (id is null) BuiltIns.ThrowArgumentNull(nameof(id));
        return ids.Contains(id);
    }

    public override PublisherState GetState() => TaskUtils.RunSync(GetStateAsync);

    public override async ValueTask<PublisherState> GetStateAsync(CancellationToken token = default)
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

    public override void Publish(in TMessage message)
    {
        AssertIsAlive();
        serializer.RosValidate(message);

        queue.Enqueue(message);
        signal.Release();
    }

    public override ValueTask PublishAsync(in TMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        Publish(message);
        return default;
    }

    public override void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public override async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return;
        disposed = true;

        runningTs.Cancel();

        await task.AwaitNoThrow(2000, this, token);
        await client.Rcl.DisposePublisher(publisher, default).AwaitNoThrow(this);

        ids.Clear();
        client.RemovePublisher(this);
    }
}