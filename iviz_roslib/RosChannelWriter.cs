using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    public sealed class RosChannelWriter<T> : IRosChannelWriter
        where T : IMessage
    {
        IRosPublisher<T>? publisher;
        string? publisherId;
        bool disposed;
        bool latchingEnabled;
        bool forceTcpNoDelay;

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

        public RosChannelWriter(IRosClient client, string topic, bool requestNoDelay = true)
        {
            Start(client, topic, requestNoDelay);
        }

        public void Start(IRosClient client, string topic, bool requestNoDelay = true)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            publisherId = client.Advertise(topic, out publisher);
            publisher.LatchingEnabled = LatchingEnabled;
            publisher.ForceTcpNoDelay = ForceTcpNoDelay;
        }

        public async Task StartAsync(IRosClient client, string topic, bool requestNoDelay = true)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            (publisherId, publisher) = await client.AdvertiseAsync<T>(topic);
            publisher.LatchingEnabled = LatchingEnabled;
            publisher.ForceTcpNoDelay = ForceTcpNoDelay;
        }

        public void Write(T msg)
        {
            Publisher.Publish(msg);
        }

        public Task WriteAsync(T msg, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
            CancellationToken token = default)
        {
            return Publisher.PublishAsync(msg, policy, token);
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
                Logger.Log($"{this}: {e}");
            }
        }

        public async Task DisposeAsync()
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
                await Publisher.UnadvertiseAsync(publisherId!);
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: {e}");
            }
        }

#if !NETSTANDARD2_0
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await DisposeAsync();
        }
#endif
    }

    public static class RosChannelWriter
    {
        public static IRosChannelWriter CreateInstance(Type msgType)
        {
            if (typeof(IMessage) == msgType || !typeof(IMessage).IsAssignableFrom(msgType))
            {
                throw new ArgumentException("msgType is not a message type", nameof(msgType));
            }

            Type writerType = typeof(RosChannelWriter<>).MakeGenericType(msgType);
            return (IRosChannelWriter) Activator.CreateInstance(writerType)!;
        }
    }
}