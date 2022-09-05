using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// A helper class that wraps a subscriber. It employs a queue that stores messages
/// in the background, and can be accessed without having to use a separate callback.
/// <example>
/// This initializes a ROS channel reader:
/// <code>
///     string masterUri = "http://localhost:11311";
///     string callerId = "my_ros_id";
///     string callerUri = "http://localhost:7615";
///     var client = new RosClient(masterUri, callerId, callerUri);
///     var reader = client.CreateReader&lt;T&gt;("/my_topic");
///     foreach (T msg in reader.ReadAll()) // blocks
///     {
///          // process msg
///     }
/// </code>
/// </example>
/// </summary>
/// <typeparam name="T">The message type</typeparam>
public sealed class RosChannelReader<T> : BaseRosChannelReader<T> where T : IMessage, new()
{
    /// <summary>
    /// Initializes the channel. <see cref="Start"/> or <see cref="StartAsync"/> must be called after this.
    /// </summary>
    public RosChannelReader()
    {
    }

    /// <summary>
    /// Initializes the channel, and calls <see cref="Start"/> with the arguments.
    /// A better alternative is to use the extension <code>client.CreateWriter&lt;T&gt;(topic)</code>
    /// </summary>
    /// <param name="client">A connected IRosClient</param>
    /// <param name="topic">The topic to listen to</param>
    public RosChannelReader(IRosClient client, string topic)
    {
        Start(client, topic);
    }

    public override async ValueTask StartAsync(IRosClient client, string topic, CancellationToken token = default)
    {
        if (client == null) BuiltIns.ThrowArgumentNull(nameof(client));
        if (subscriber != null) throw new InvalidOperationException("Channel has already been started!");

        var (newId, newSubscriber) = await client.SubscribeAsync(topic, this, token: token);

        subscriberId = newId;
        subscriber = newSubscriber;
        subscriberToken = subscriber.CancellationToken.Register(OnSubscriberDisposed);
    }

    public override void Start(IRosClient client, string topic)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        subscriberId = client.Subscribe(topic, this, out subscriber);
        subscriberToken = subscriber.CancellationToken.Register(OnSubscriberDisposed);
    }

    public override void Handle(in T t, IRosConnection _)
    {
        if (disposed)
        {
            return;
        }

        try
        {
            messageQueue.Add(t);
        }
        catch (InvalidOperationException)
        {
            // ignore, reader was disposed but some messages were still in queue
        }
    }
}

/// <summary>
/// A helper class that wraps a generic subscriber. It employs a queue that stores messages
/// in the background, and can be accessed without having to use a separate callback.
/// This is the generic version of <see cref="RosChannelReader{T}"/>, if you know the message type a priori,
/// you should use that instead.
/// This variant reconstructs the message type from the publisher handshake and should only
/// be used if the message definition is unknown beforehand.
/// </summary>

/*
public sealed class RosChannelReader : BaseRosChannelReader<IMessage>
{
    public RosChannelReader()
    {
    }

    public RosChannelReader(IRosClient client, string topic)
    {
        Start(client, topic);
    }

    public override async ValueTask StartAsync(IRosClient client, string topic, CancellationToken token = default)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        if (subscriber != null)
        {
            throw new InvalidOperationException("Channel has already been started!");
        }

        var (newId, newSubscriber) = await client.SubscribeAsync(topic, Callback, token: token);

        subscriberId = newId;
        subscriber = (RosSubscriber<IMessage>) newSubscriber;
        subscriberToken = subscriber.CancellationToken.Register(OnSubscriberDisposed);
    }

    public override void Start(IRosClient client, string topic)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        subscriberId = client.Subscribe(topic, Callback, out var newSubscriber);
        subscriber = (RosSubscriber<IMessage>) newSubscriber;
        subscriberToken = subscriber.CancellationToken.Register(OnSubscriberDisposed);
    }

    void Callback(IMessage t)
    {
        if (disposed)
        {
            return;
        }

        try
        {
            messageQueue.Add(t);
        }
        catch (InvalidOperationException)
        {
            // ignore, reader was disposed but some messages were still in queue
        }
    }
}
*/

public static class RosChannelReaderUtils
{
    /// <summary>
    /// Creates a channel reader for the given topic. Returns whatever message type is published there.
    /// </summary>
    /// <param name="client">The ROS client</param>
    /// <param name="topic">The topic name</param>
    /// <returns>The channel reader</returns>
    
    /*
    public static RosChannelReader CreateReader(this IRosClient client, string topic)
    {
        return new RosChannelReader(client, topic);
    }
    */
        
    /// <summary>
    /// Creates a channel reader for the given topic and message type. 
    /// </summary>
    /// <param name="client">The ROS client</param>
    /// <param name="topic">The topic name</param>
    /// <typeparam name="T">The message type</typeparam>
    /// <returns>The channel reader</returns>
    public static RosChannelReader<T> CreateReader<T>(this IRosClient client, string topic) 
        where T : IMessage, new()
    {
        return new RosChannelReader<T>(client, topic);
    }        

    public static async ValueTask<RosChannelReader<T>> CreateReaderAsync<T>(this IRosClient client, string topic,
        CancellationToken token = default)
        where T : IMessage, new()
    {
        var writer = new RosChannelReader<T>();
        await writer.StartAsync(client, topic, token);
        return writer;
    }

    /*
    public static async ValueTask<RosChannelReader> CreateReaderAsync(this IRosClient client, string topic,
        CancellationToken token = default)
    {
        var writer = new RosChannelReader();
        await writer.StartAsync(client, topic, token);
        return writer;
    }
    */

    public static IRosChannelReader CreateInstance(Type msgType)
    {
        if (typeof(IMessage) == msgType || !typeof(IMessage).IsAssignableFrom(msgType))
        {
            throw new ArgumentException("msgType is not a message type", nameof(msgType));
        }

        Type readerType = typeof(RosChannelReader<>).MakeGenericType(msgType);
        return (IRosChannelReader) Activator.CreateInstance(readerType)!;
    }
}