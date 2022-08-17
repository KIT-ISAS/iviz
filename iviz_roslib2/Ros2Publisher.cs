using System.Collections.Concurrent;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib2.RclInterop;
using Iviz.Tools;

namespace Iviz.Roslib2;

public sealed class Ros2Publisher<TMessage> : IRos2Publisher, IRosPublisher<TMessage> where TMessage : IMessage
{
    readonly Ros2Client client;
    readonly RclPublisher publisher;
    readonly List<string> ids = new();
    readonly CancellationTokenSource runningTs = new();

    readonly SemaphoreSlim signal = new(0);
    readonly ConcurrentQueue<TMessage> queue = new();
    readonly Task task;

    int totalPublishers;
    bool disposed;

    int numSent;
    long bytesSent;

    public CancellationToken CancellationToken => runningTs.Token;
    public string Topic => publisher.Topic;
    public string TopicType => publisher.TopicType;
    public int NumSubscribers => publisher.GetNumSubscribers();
    public bool IsAlive => !CancellationToken.IsCancellationRequested;
    public QosProfile Profile => publisher.Profile;

    internal Ros2Publisher(Ros2Client client, RclPublisher publisher)
    {
        this.client = client;
        this.publisher = publisher;
        task = Task.Run(() => Run().AwaitNoThrow(this));
    }

    void AssertIsAlive()
    {
        if (!IsAlive)
        {
            throw new ObjectDisposedException("this", "This is not a valid publisher");
        }
    }

    async Task Run()
    {
        var token = CancellationToken;
        var handler = new RclSerializeHandler<TMessage>();
        using var handle = new GCHandleWrapper(handler);

        while (true)
        {
            await signal.WaitAsync(token);
            while (queue.TryDequeue(out var message))
            {
                handler.message = message;
                publisher.Publish(handle.ptr);

                numSent++;
                bytesSent += handler.messageLength;
            }
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
        AssertIsAlive();
        message.RosValidate();

        queue.Enqueue(message);
        signal.Release();
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

    public async ValueTask DisposeAsync(CancellationToken token = default)
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