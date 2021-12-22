using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;

namespace Iviz.Roslib;

public sealed class RosChannelWriter<T> : IRosChannelWriter
    where T : IMessage
{
    IRosPublisher<T>? publisher;
    string? publisherId;
    bool disposed;
    bool latchingEnabled;
    bool forceTcpNoDelay = true;

    public bool Started => publisher != null;

    public IRosPublisher<T> Publisher =>
        publisher ?? throw new InvalidOperationException("Publisher has not been started!");

    public bool IsAlive => publisher != null && !disposed;

    IRosPublisher IRosChannelWriter.Publisher => Publisher;

    public bool LatchingEnabled
    {
        get => latchingEnabled;
        set
        {
            latchingEnabled = value;
            if (publisher != null)
            {
                Publisher.LatchingEnabled = value;
            }
        }
    }

    public bool ForceTcpNoDelay
    {
        get => forceTcpNoDelay;
        set
        {
            forceTcpNoDelay = value;
            if (publisher != null)
            {
                Publisher.ForceTcpNoDelay = value;
            }
        }
    }

    public string Topic => Publisher.Topic;

    public RosChannelWriter()
    {
    }

    public RosChannelWriter(IRosClient client, string topic)
    {
        Start(client, topic);
    }

    public void Start(IRosClient client, string topic)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (DynamicMessage.IsDynamic<T>())
        {
            throw new InvalidOperationException(
                "This function cannot be used in channels for dynamic messages. Use the overload with the generator.");
        }

        publisherId = client.Advertise(topic, out publisher);
        publisher.LatchingEnabled = LatchingEnabled;
        publisher.ForceTcpNoDelay = ForceTcpNoDelay;
    }

    public void Start(IRosClient client, string topic, DynamicMessage generator)
    {
        if (!DynamicMessage.IsDynamic<T>())
        {
            throw new InvalidOperationException("This function can only be used in channels for dynamic messages");
        }

        if (!generator.IsInitialized)
        {
            throw new InvalidOperationException("The generator has not been initialized");
        }

        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (generator == null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        publisherId = client.Advertise(topic, generator, out var dynamicPublisher);
        publisher = (IRosPublisher<T>) dynamicPublisher;
        publisher.LatchingEnabled = LatchingEnabled;
        publisher.ForceTcpNoDelay = ForceTcpNoDelay;
    }

    public async ValueTask StartAsync(IRosClient client, string topic, CancellationToken token = default)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        (publisherId, publisher) = await client.AdvertiseAsync<T>(topic, token);
        publisher.LatchingEnabled = LatchingEnabled;
        publisher.ForceTcpNoDelay = ForceTcpNoDelay;
    }

    public async ValueTask StartAsync(IRosClient client, string topic, DynamicMessage generator,
        CancellationToken token = default)
    {
        if (!DynamicMessage.IsDynamic<T>())
        {
            throw new InvalidOperationException("This function can only be used in channels for dynamic messages");
        }

        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (generator == null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        IRosPublisher<DynamicMessage> dynamicPublisher;
        (publisherId, dynamicPublisher) = await client.AdvertiseAsync(topic, generator, token);

        publisher = (IRosPublisher<T>) dynamicPublisher;
        publisher.LatchingEnabled = LatchingEnabled;
        publisher.ForceTcpNoDelay = ForceTcpNoDelay;
    }


    public void Write(in T msg)
    {
        Publisher.Publish(msg);
    }

    public async ValueTask WriteAsync(T msg, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default)
    {
        await Publisher.PublishAsync(msg, policy, token);
    }

    void IRosChannelWriter.Write(IMessage msg)
    {
        Publisher.Publish(msg);
    }

    public void WriteAll(IEnumerable<T> msgs)
    {
        if (msgs == null)
        {
            throw new ArgumentNullException(nameof(msgs));
        }

        foreach (T msg in msgs)
        {
            Publisher.Publish(msg);
        }
    }

    void IRosChannelWriter.WriteAll(IEnumerable<IMessage> msgs)
    {
        if (msgs == null)
        {
            throw new ArgumentNullException(nameof(msgs));
        }

        foreach (IMessage msg in msgs)
        {
            Publisher.Publish(msg);
        }
    }

#if !NETSTANDARD2_0
    public async ValueTask WriteAllAsync(IAsyncEnumerable<T> messages, RosPublishPolicy policy =
        RosPublishPolicy.DoNotWait, CancellationToken token = default)
    {
        if (messages == null)
        {
            throw new ArgumentNullException(nameof(messages));
        }

        await foreach (T msg in messages.WithCancellation(token))
        {
            await Publisher.PublishAsync(msg, policy, token);
        }
    }

    async ValueTask IRosChannelWriter.WriteAllAsync(IAsyncEnumerable<IMessage> messages, RosPublishPolicy policy,
        CancellationToken token)
    {
        if (messages == null)
        {
            throw new ArgumentNullException(nameof(messages));
        }

        await foreach (IMessage msg in messages.WithCancellation(token))
        {
            await Publisher.PublishAsync(msg, policy, token);
        }
    }
#endif

    public override string ToString()
    {
        if (publisher == null)
        {
            return "[RosChannelWriter (uninitialized)]";
        }

        return disposed
            ? "[RosChannelWriter (disposed)]"
            : $"[RosChannelWriter {Publisher.Topic} [{Publisher.TopicType}]]";
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

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
            Logger.LogErrorFormat("{0}: Error in Dispose: {1}", this, e);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        if (publisher == null)
        {
            return; // not started
        }

        await Publisher.UnadvertiseAsync(publisherId!).AsTask().AwaitNoThrow(this);
    }
}

public static class RosChannelWriterUtils
{
    public static RosChannelWriter<T> CreateWriter<T>(this IRosClient client, string topic, bool latchingEnabled = false)
        where T : IMessage
    {
        return new RosChannelWriter<T>(client, topic) {LatchingEnabled = latchingEnabled};
    }
        
    public static async ValueTask<RosChannelWriter<T>> CreateWriterAsync<T>(this IRosClient client, string topic, bool latchingEnabled = false, CancellationToken token = default)
        where T : IMessage
    {
        var writer = new RosChannelWriter<T> {LatchingEnabled = latchingEnabled};
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
        if (typeof(IMessage) == msgType)
        {
            return new RosChannelWriter<IMessage>();
        }

        if (!typeof(IMessage).IsAssignableFrom(msgType))
        {
            throw new ArgumentException("msgType is not a message type", nameof(msgType));
        }

        Type writerType = typeof(RosChannelWriter<>).MakeGenericType(msgType);
        return (IRosChannelWriter) Activator.CreateInstance(writerType)!;
    }
}