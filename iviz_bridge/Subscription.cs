using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Bridge;

internal abstract class Subscription : IdCollection
{
    public abstract ValueTask<bool> DisposeAsync();

    public static Subscription? CreateForType(string msgType, string topic, IRosClient client, SocketConnection parent)
    {
        var type = BuiltIns.TryGetTypeFromMessageName(msgType);
        if (type == null) return null;
        var publishMsgType = typeof(Subscription<>).MakeGenericType(type);
        return (Subscription?)Activator.CreateInstance(publishMsgType, topic, client, parent);
    }
}

internal sealed class Subscription<T> : Subscription where T : IMessage, new()
{
    readonly IRosSubscriber<T> subscriber;
    readonly string subscriberId;
    readonly string topic;
    readonly SocketConnection parent;

    public Subscription(string topic, IRosClient client, SocketConnection parent)
    {
        this.parent = parent;
        this.topic = topic;
        subscriberId = client.Subscribe(topic, Callback, out subscriber);
    }

    void Callback(T msg)
    {
        var subscriberMessage = new SubscriberMessage
        {
            Topic = topic, 
            Msg = msg
        };
        parent.MessageCallback(subscriberMessage);
    }

    public override ValueTask<bool> DisposeAsync()
    {
        return subscriber.UnsubscribeAsync(subscriberId);
    }
}