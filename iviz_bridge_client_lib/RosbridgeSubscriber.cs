using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Bridge.Client;

public abstract class RosbridgeSubscriber
{
    int totalSubscribers;

    protected readonly CancellationTokenSource runningTs = new();

    protected bool IsAlive => !runningTs.IsCancellationRequested;

    public string Topic { get; }
    public string TopicType { get; }
    public CancellationToken CancellationToken => runningTs.Token;
    public bool IsPaused { get; set; }

    protected RosbridgeSubscriber(string topic, string topicType)
    {
        Topic = topic;
        TopicType = topicType;
    }

    protected string GenerateId()
    {
        int currentCount = Interlocked.Increment(ref totalSubscribers);
        int lastId = currentCount - 1;
        return lastId == 0 ? Topic : $"{Topic}-{lastId.ToString()}";
    }

    protected void AssertIsAlive()
    {
        if (!IsAlive)
        {
            BuiltIns.ThrowObjectDisposed(nameof(RosbridgeSubscriber), "This is not a valid subscriber");
        }
    }

    internal abstract void HandleJson(byte[] array);

    internal abstract void HandleRaw(ReadOnlySpan<byte> array);

    public abstract ValueTask<SubscriberState> GetStateAsync(CancellationToken token = default);

    public abstract ValueTask DisposeAsync(CancellationToken token = default);

    public override string ToString() => $"[{nameof(RosbridgeSubscriber)} {Topic} [{TopicType}] ]";
}

public sealed class RosbridgeSubscriber<TMessage> : RosbridgeSubscriber, IRosSubscriber<TMessage>
    where TMessage : IMessage, new()
{
    static RosCallback<TMessage>[] EmptyCallback => Array.Empty<RosCallback<TMessage>>();

    readonly Dictionary<string, RosCallback<TMessage>> callbacksById = new();
    readonly MessageInfo info;
    readonly RosbridgeClient client;
    readonly Deserializer<TMessage> deserializer;

    Cache<SubscriberState> cachedState;

    RosCallback<TMessage>[] callbacks = EmptyCallback;
    bool disposed;

    public int NumPublishers
    {
        get
        {
            if (!cachedState.TryGet(out var state))
            {
                Task.Run(() => GetStateCoreAsync());
            }

            return state?.Receivers.Length ?? 0;
        }
    }

    public RosbridgeSubscriber(RosbridgeClient client, string topic, string topicType,
        Deserializer<TMessage> deserializer) :
        base(topic, topicType)
    {
        this.client = client;
        this.deserializer = deserializer;
        info = new MessageInfo(new RosbridgeConnectionInfo(topic, topicType));
    }

    internal override void HandleJson(byte[] array)
    {
        if (IsPaused) return;

        PublishResponseMessage<TMessage>? response;
        try
        {
            response = Utf8Json.JsonSerializer.Deserialize<PublishResponseMessage<TMessage>>(array);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Exception from " + nameof(HandleJson) + ": {1}", this, e);
            return;
        }

        if (response is null || response.Msg is not { } message)
        {
            Logger.LogErrorFormat("{0}: Ignoring null message", this);
            return;
        }

        info.MessageSize = 0;
        foreach (var callback in callbacks)
        {
            try
            {
                callback.Handle(message, info);
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Exception from " + nameof(HandleJson) + ": {1}", this, e);
            }
        }
    }

    internal override void HandleRaw(ReadOnlySpan<byte> array)
    {
        if (IsPaused) return;

        TMessage message;
        try
        {
            message = ReadBuffer.Deserialize(deserializer, array);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Exception from " + nameof(HandleRaw) + ": {1}", this, e);
            return;
        }

        info.MessageSize = array.Length;
        foreach (var callback in callbacks)
        {
            try
            {
                callback.Handle(message, info);
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Exception from " + nameof(HandleRaw) + ": {1}", this, e);
            }
        }
    }

    public SubscriberState GetState() => TaskUtils.RunSync(GetStateAsync);

    async ValueTask<SubscriberState> GetStateCoreAsync(CancellationToken token = default)
    {
        AssertIsAlive();
        string[] subscribers = await client.GetSystemSubscribers(Topic, token);
        var state = new SubscriberState(Topic, TopicType, callbacksById.Keys.ToArray(),
            subscribers
                .Select(subscriber => new ReceiverState { RemoteId = subscriber })
                .ToArray()
        );
        cachedState.Value = state;
        return state;
    }

    public override ValueTask<SubscriberState> GetStateAsync(CancellationToken token = default)
    {
        return cachedState.TryGet() is { } systemState
            ? systemState.AsTaskResult()
            : GetStateCoreAsync(token);
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

    string IRosSubscriber.Subscribe(Action<IMessage> callback)
    {
        return Subscribe(new GenericRosCallback<TMessage>(callback));
    }

    string IRosSubscriber.Subscribe(Action<IMessage, MessageInfo> callback)
    {
        return Subscribe(new Generic2RosCallback<TMessage>(callback));
    }

    public string Subscribe(Action<TMessage> callback)
    {
        return Subscribe(new ActionRosCallback<TMessage>(callback));
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

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public override ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return default;
        disposed = true;

        runningTs.CancelNoThrow(this);

        callbacksById.Clear();
        callbacks = EmptyCallback;

        return client.RemoveSubscriberAsync(Topic).AwaitNoThrow(this).AsValueTask();
    }
}