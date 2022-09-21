using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib;

public sealed class RosChannelWriter<TMessage> : IRosChannelWriter
    where TMessage : IMessage, new()
{
    IRosPublisher<TMessage>? publisher;
    string? publisherId;
    bool disposed;

    public bool Started => publisher != null;

    public IRosPublisher<TMessage> Publisher =>
        publisher ?? throw new InvalidOperationException("Publisher has not been started!");

    public bool IsAlive => publisher != null && !disposed;

    IRosPublisher IRosChannelWriter.Publisher => Publisher;

    public bool LatchingEnabled { get; }

    public string Topic => Publisher.Topic;

    public RosChannelWriter(bool latchingEnabled = false)
    {
        LatchingEnabled = latchingEnabled;
    }

    public RosChannelWriter(IRosClient client, string topic, bool latchingEnabled = false)
    {
        LatchingEnabled = latchingEnabled;
        Start(client, topic);
    }

    public void Start(IRosClient client, string topic)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        /*
        if (DynamicMessage.IsDynamic(typeof(TMessage)))
        {
            throw new InvalidOperationException(
                "This function cannot be used in channels for dynamic messages. Use the overload with the generator.");
        }
        */

        publisherId = client.Advertise(topic, out publisher, LatchingEnabled);
    }

    /*
    public void Start(IRosClient client, string topic, DynamicMessage generator)
    {
        if (!DynamicMessage.IsDynamic(typeof(TMessage)))
        {
            throw new InvalidOperationException("This function can only be used in channels for dynamic messages");
        }

        if (!generator.IsInitialized)
        {
            throw new InvalidOperationException("The generator has not been initialized");
        }

        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        if (generator == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(generator));
        }

        publisherId = client.Advertise(topic, generator, out var dynamicPublisher);
        publisher = (IRosPublisher<TMessage>) dynamicPublisher;
        publisher.LatchingEnabled = LatchingEnabled;
    }
    */

    public async ValueTask StartAsync(IRosClient client, string topic, CancellationToken token = default)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        (publisherId, publisher) = await client.AdvertiseAsync<TMessage>(topic, LatchingEnabled, token);
    }

    /*
    public async ValueTask StartAsync(IRosClient client, string topic, DynamicMessage generator,
        CancellationToken token = default)
    {
        if (!DynamicMessage.IsDynamic(typeof(TMessage)))
        {
            throw new InvalidOperationException("This function can only be used in channels for dynamic messages");
        }

        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        if (generator == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(generator));
        }

        IRosPublisher<DynamicMessage> dynamicPublisher;
        (publisherId, dynamicPublisher) = await client.AdvertiseAsync(topic, generator, token);

        publisher = (IRosPublisher<TMessage>) dynamicPublisher;
        publisher.LatchingEnabled = LatchingEnabled;
        publisher.ForceTcpNoDelay = ForceTcpNoDelay;
    }
    */

    public void Write(in TMessage msg)
    {
        Publisher.Publish(msg);
    }

    public async ValueTask WriteAsync(TMessage msg, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        await Publisher.PublishAsync(msg, policy, token);
    }

    void IRosChannelWriter.Write(IMessage msg)
    {
        Publisher.Publish(msg);
    }

    public void WriteAll(IEnumerable<TMessage> msgs)
    {
        if (msgs == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(msgs));
        }

        foreach (TMessage msg in msgs)
        {
            Publisher.Publish(msg);
        }
    }

    void IRosChannelWriter.WriteAll(IEnumerable<IMessage> msgs)
    {
        if (msgs == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(msgs));
        }

        foreach (IMessage msg in msgs)
        {
            Publisher.Publish(msg);
        }
    }

    public async ValueTask WriteAllAsync(IAsyncEnumerable<TMessage> messages, RosPublishPolicy policy =
        RosPublishPolicy.DoNotWait, CancellationToken token = default)
    {
        if (messages == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(messages));
        }

        await foreach (TMessage msg in messages.WithCancellation(token))
        {
            await Publisher.PublishAsync(msg, policy, token);
        }
    }

    async ValueTask IRosChannelWriter.WriteAllAsync(IAsyncEnumerable<IMessage> messages, RosPublishPolicy policy,
        CancellationToken token)
    {
        if (messages == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(messages));
        }

        await foreach (IMessage msg in messages.WithCancellation(token))
        {
            await Publisher.PublishAsync(msg, policy, token);
        }
    }

    public override string ToString()
    {
        if (publisher == null)
        {
            return $"[{nameof(RosChannelWriter<TMessage>)} (uninitialized)]";
        }

        return disposed
            ? $"[{nameof(RosChannelWriter<TMessage>)} (disposed)]"
            : $"[{nameof(RosChannelWriter<TMessage>)} {Publisher.Topic} [{Publisher.TopicType}]]";
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;

        if (publisher == null)
        {
            return; // not started
        }

        try
        {
            Publisher.Unadvertise(publisherId!);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Error in " + nameof(Dispose) + ": {1}", this, e);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (disposed) return;
        disposed = true;

        if (publisher == null)
        {
            return; // not started
        }

        await Publisher.UnadvertiseAsync(publisherId!).AwaitNoThrow(this);
    }
}

public static class RosChannelWriterUtils
{
    public static RosChannelWriter<T> CreateWriter<T>(this IRosClient client, string topic,
        bool latchingEnabled = false)
        where T : IMessage, new()
    {
        return new RosChannelWriter<T>(client, topic, latchingEnabled);
    }

    public static async ValueTask<RosChannelWriter<T>> CreateWriterAsync<T>(this IRosClient client, string topic,
        bool latchingEnabled = false, CancellationToken token = default)
        where T : IMessage, new()
    {
        var writer = new RosChannelWriter<T>(latchingEnabled);
        await writer.StartAsync(client, topic, token);
        return writer;
    }

    /// <summary>
    /// Creates a writer suitable for the given dynamically generated message type.
    /// </summary>
    public static async ValueTask<IRosChannelWriter> CreateWriterForMessageAsync(this IRosClient client, IMessage msg,
        string topic, CancellationToken token = default)
    {
        var writer = CreateWriterForMessage(msg);
        await writer.StartAsync(client, topic, token);
        return writer;
    }

    /// <summary>
    /// Creates a writer suitable for the given dynamically generated message type.
    /// </summary>
    public static IRosChannelWriter CreateWriterForMessage(this IRosClient client, IMessage msg, string topic)
    {
        var writer = CreateWriterForMessage(msg.GetType());
        writer.Start(client, topic);
        return writer;
    }

    static IRosChannelWriter CreateWriterForMessage(IMessage msg)
    {
        return CreateWriterForMessage(msg.GetType());
    }

    static IRosChannelWriter CreateWriterForMessage(Type msgType)
    {
        if (!typeof(IMessage).IsAssignableFrom(msgType))
        {
            throw new ArgumentException("msgType is not a message type", nameof(msgType));
        }

        Type writerType = typeof(RosChannelWriter<>).MakeGenericType(msgType);
        return (IRosChannelWriter)Activator.CreateInstance(writerType)!;
    }
}