using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace iviz_bridge_client;

public sealed class RosbridgePublisher<TMessage> : BaseRosPublisher<TMessage>
    where TMessage : IMessage, new()
{
    readonly RosbridgeClient client;
    bool disposed;

    public override int NumSubscribers => 0;

    public RosbridgePublisher(RosbridgeClient client, string topic, string topicType) : base(topic, topicType)
    {
        this.client = client;
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

    public override PublisherState GetState() => TaskUtils.RunSync(GetStateAsync);

    public override async ValueTask<PublisherState> GetStateAsync(CancellationToken token = default)
    {
        AssertIsAlive();
        string[] subscribers = await client.GetSystemSubscribers(Topic, token);
        return new PublisherState(Topic, TopicType, ids,
            subscribers
                .Select(subscriber => new SenderState { RemoteId = subscriber })
                .ToArray()
        );
    }

    public override ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return default;
        disposed = true;

        runningTs.Cancel();

        ids.Clear();
        return client.RemovePublisherAsync(Topic).AsValueTask();
    }

    public override void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public override void Publish(in TMessage message)
    {
        var publishMessage = new PublishMessage<TMessage>
        {
            Topic = Topic,
            Msg = message
        };

        client.Post(publishMessage);
    }


    public override ValueTask PublishAsync(in TMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        var publishMessage = new PublishMessage<TMessage>
        {
            Topic = Topic,
            Msg = message
        };

        if (policy == RosPublishPolicy.DoNotWait)
        {
            client.Post(publishMessage);
            return default;
        }

        return policy == RosPublishPolicy.WaitUntilSent
            ? client.PostAsync(publishMessage).AsValueTask()
            : default;
    }
}