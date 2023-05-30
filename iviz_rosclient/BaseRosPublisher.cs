using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

public abstract class BaseRosPublisher : IRosPublisher
{
    protected readonly CancellationTokenSource runningTs = new();

    /// <summary>
    ///     Whether this publisher is valid.
    /// </summary>
    public bool IsAlive => !runningTs.IsCancellationRequested;

    protected void AssertIsAlive()
    {
        if (!IsAlive)
        {
            BuiltIns.ThrowObjectDisposed(nameof(BaseRosPublisher), "This is not a valid publisher");
        }
    }

    /// <summary>
    ///     A cancellation token that gets canceled when the publisher is disposed.
    /// </summary>
    public CancellationToken CancellationToken => runningTs.Token;

    public abstract string Topic { get; }
    public abstract string TopicType { get; }
    public abstract int NumSubscribers { get; }
    public abstract void Publish(IMessage message);

    public abstract ValueTask PublishAsync(IMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default);

    public abstract bool Unadvertise(string id, CancellationToken token = default);
    public abstract ValueTask<bool> UnadvertiseAsync(string id, CancellationToken token = default);
    public abstract string Advertise();
    public abstract bool ContainsId(string id);
    public abstract PublisherState GetState();
    public abstract ValueTask<PublisherState> GetStateAsync(CancellationToken token = default);
    public abstract void Dispose();
    public abstract ValueTask DisposeAsync(CancellationToken token);
}

public abstract class BaseRosPublisher<T> : BaseRosPublisher, IRosPublisher<T> where T : IMessage
{
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

        RosExceptionUtils.ThrowInvalidMessageType();
    }

    public override ValueTask PublishAsync(IMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        if (message is T tMessage)
        {
            return PublishAsync(tMessage, policy, token);
        }

        RosExceptionUtils.ThrowInvalidMessageType();
        return default; // unreachable
    }
}