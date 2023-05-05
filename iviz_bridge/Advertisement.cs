using System;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using Utf8Json;

namespace Iviz.Bridge;

public abstract class Advertisement : IdCollection
{
    public abstract ValueTask<bool> DisposeAsync();
    public abstract void Publish(byte[] data);

    public static Advertisement? CreateForType(string msgType, string topic, IRosClient client, SocketConnection parent)
    {
        var type = BuiltIns.TryGetTypeFromMessageName(msgType);
        if (type == null) return null;
        var publishMsgType = typeof(Advertisement<>).MakeGenericType(type);
        return (Advertisement?)Activator.CreateInstance(publishMsgType, topic, client, parent);
    }
}

public sealed class Advertisement<T> : Advertisement where T : IMessage, new()
{
    readonly IRosPublisher<T> publisher;
    readonly string publisherId;
    readonly SocketConnection parent;

    public Advertisement(string topic, IRosClient client, SocketConnection parent)
    {
        publisherId = client.Advertise(topic, out publisher);
        this.parent = parent;
    }

    public override ValueTask<bool> DisposeAsync()
    {
        return publisher.UnadvertiseAsync(publisherId);
    }

    public override void Publish(byte[] data)
    {
        if (publisher.NumSubscribers == 0)
        {
            return;
        }

        var jsonMsg = JsonSerializer.Deserialize<PublishMessage<T>>(data);
        if (jsonMsg is { Msg: not null })
        {
            publisher.Publish(jsonMsg.Msg);
        }
        else
        {
            parent.LogError("Failed to deserialize JSON message!");
        }
    }
}