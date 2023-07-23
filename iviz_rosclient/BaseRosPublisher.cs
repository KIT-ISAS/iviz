using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

public abstract class BaseRosPublisher : IRosPublisher
{
    protected readonly List<string> ids = new();
    protected readonly CancellationTokenSource runningTs = new();

    int totalPublishers;

    public string Topic { get; }
    public string TopicType { get; }
    
    /// <summary>
    ///     Whether this publisher is valid.
    /// </summary>
    public bool IsAlive => !runningTs.IsCancellationRequested;

    /// <summary>
    ///     A cancellation token that gets canceled when the publisher is disposed.
    /// </summary>
    public CancellationToken CancellationToken => runningTs.Token;

    public abstract int NumSubscribers { get; }

    protected BaseRosPublisher(string topic, string topicType)
    {
        Topic = topic;
        TopicType = topicType;
    }

    string GenerateId()
    {
        int currentCount = Interlocked.Increment(ref totalPublishers);
        int lastId = currentCount - 1;
        return lastId == 0 ? Topic : $"{Topic}-{lastId.ToString()}";
    }
    
    protected void AssertIsAlive()
    {
        if (!IsAlive)
        {
            BuiltIns.ThrowObjectDisposed(nameof(IRosPublisher), "This publisher has been disposed " +
                                                                "and its connection is no longer valid");
        }
    }
    
    public string Advertise()
    {
        AssertIsAlive();

        string id = GenerateId();
        ids.Add(id);
        return id;
    }    

    protected bool RemoveId(string topicId)
    {
        return ids.Remove(topicId);
    }

    public bool ContainsId(string id)
    {
        if (id is null) BuiltIns.ThrowArgumentNull(nameof(id));
        return ids.Contains(id);
    }
    
    public abstract void Publish(IMessage message);

    public abstract ValueTask PublishAsync(IMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default);

    public abstract bool Unadvertise(string id, CancellationToken token = default);
    public abstract ValueTask<bool> UnadvertiseAsync(string id, CancellationToken token = default);
    public abstract PublisherState GetState();
    public abstract ValueTask<PublisherState> GetStateAsync(CancellationToken token = default);
    public abstract void Dispose();
    public abstract ValueTask DisposeAsync(CancellationToken token);
}

public abstract class BaseRosPublisher<T> : BaseRosPublisher, IRosPublisher<T> where T : IMessage
{
    protected BaseRosPublisher(string topic, string topicType) : base(topic, topicType)
    {
    }

    public abstract void Publish(in T message);

    public abstract ValueTask PublishAsync(in T message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default);

    public override void Publish(IMessage message)
    {
        if (message is T tMessage) // check is rather fast if message is actually T
        {
            Publish(tMessage);
            return;
        }

        RosExceptionUtils.ThrowInvalidMessageType(TopicType, message);
    }

    public override ValueTask PublishAsync(IMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        if (message is T tMessage)
        {
            return PublishAsync(tMessage, policy, token);
        }

        RosExceptionUtils.ThrowInvalidMessageType(TopicType, message);
        return default; // unreachable
    }
}