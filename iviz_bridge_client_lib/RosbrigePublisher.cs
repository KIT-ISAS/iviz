using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Bridge.Client;

public sealed class RosbridgePublisher<TMessage> : BaseRosPublisher<TMessage>
    where TMessage : IMessage, new()
{
    readonly RosbridgeClient client;
    readonly Serializer<TMessage> serializer;
    bool disposed;
    
    Cache<PublisherState> cachedState;

    public override int NumSubscribers
    {
        get
        {
            if (!cachedState.TryGet(out var state))
            {
                Task.Run(() => GetStateCoreAsync());
            }

            return state?.Senders.Length ?? 0;
        }
    }
    public RosbridgePublisher(RosbridgeClient client, string topic, string topicType) : base(topic, topicType)
    {
        serializer = new TMessage().CreateSerializer();
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

    async ValueTask<PublisherState> GetStateCoreAsync(CancellationToken token = default)
    {
        AssertIsAlive();
        string[] publishers = await client.GetSystemPublishers(Topic, token);
        var state = new PublisherState(Topic, TopicType, ids,
            publishers
                .Select(publisher => new SenderState { RemoteId = publisher })
                .ToArray()
        );
        cachedState.Value = state;
        return state;
    }
    
    public override ValueTask<PublisherState> GetStateAsync(CancellationToken token = default)
    {
        return cachedState.TryGet() is { } systemState
            ? systemState.AsTaskResult()
            : GetStateCoreAsync(token);
    }

    public override ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return default;
        disposed = true;

        runningTs.CancelNoThrow(this);

        ids.Clear();
        return client.RemovePublisherAsync(Topic).AwaitNoThrow(this).AsValueTask();
    }

    public override void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public override void Publish(in TMessage message)
    {
        AssertIsAlive();
        serializer.RosValidate(message);
        
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
        AssertIsAlive();
        serializer.RosValidate(message);
        
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
            ? client.PostAsync(publishMessage, token).AsValueTask()
            : default;
    }
}