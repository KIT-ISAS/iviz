using System;
using System.Collections.Generic;
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

        public IRosPublisher<T> Publisher =>
            publisher ?? throw new InvalidOperationException("Channel has not been started!");

        IRosPublisher IRosChannelWriter.Publisher => Publisher;

        bool latchingEnabled;
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

        bool forceTcpNoDelay;
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

        public RosChannelWriter(IRosClient client, string topic, bool requestNoDelay = false)
        {
            Start(client, topic, requestNoDelay);
        }

        public void Start(IRosClient client, string topic, bool requestNoDelay = false)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            publisherId = client.Advertise(topic, out publisher);
            publisher.LatchingEnabled = LatchingEnabled;
            publisher.ForceTcpNoDelay = ForceTcpNoDelay;
        }

        public async Task StartAsync(IRosClient client, string topic, bool requestNoDelay = false)
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

        public async Task WriteAsync(T msg)
        {
            await Publisher.PublishAsync(msg);
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
        public async ValueTask WriteAllAsync(IAsyncEnumerable<T> messages)
        {
            if (messages == null)
            {
                throw new ArgumentNullException(nameof(messages));
            }
            
            await foreach (T msg in messages)
            {
                await Publisher.PublishAsync(msg);
            }
        }

        async ValueTask IRosChannelWriter.WriteAllAsync(IAsyncEnumerable<IMessage> msgs)
        {
            if (msgs == null)
            {
                throw new ArgumentNullException(nameof(msgs));
            }
            
            await foreach (IMessage msg in msgs)
            {
                await Publisher.PublishAsync(msg);
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

#if !NETSTANDARD2_0
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

            try
            {
                await Publisher.UnadvertiseAsync(publisherId!);
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: {e}");
            }
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