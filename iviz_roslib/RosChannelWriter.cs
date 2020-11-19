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
        }

        public async Task StartAsync(IRosClient client, string topic, bool requestNoDelay = false)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            (publisherId, publisher) = await client.AdvertiseAsync<T>(topic);
        }

        void ThrowIfNotStarted()
        {
            if (publisher == null)
            {
                throw new InvalidOperationException("Writer has not been started");
            }
        }

        public void Write(T msg)
        {
            ThrowIfNotStarted();
            publisher!.Publish(msg);
        }

        void IRosChannelWriter.Write(IMessage msg)
        {
            ThrowIfNotStarted();
            publisher!.Publish(msg);
        }

        public void WriteAll(IEnumerable<T> msgs)
        {
            ThrowIfNotStarted();
            foreach (T msg in msgs)
            {
                if (!publisher!.IsAlive())
                {
                    break;
                }

                publisher!.Publish(msg);
            }
        }

        void IRosChannelWriter.WriteAll(IEnumerable<IMessage> msgs)
        {
            ThrowIfNotStarted();
            foreach (IMessage msg in msgs)
            {
                if (!publisher!.IsAlive())
                {
                    break;
                }
                
                publisher!.Publish(msg);
            }
        }

#if !NETSTANDARD2_0
        public async Task WriteAllAsync(IAsyncEnumerable<T> msgs)
        {
            ThrowIfNotStarted();
            await foreach (T msg in msgs)
            {
                if (!publisher!.IsAlive())
                {
                    break;
                }
                
                publisher!.Publish(msg);
            }
        }

        async ValueTask IRosChannelWriter.WriteAllAsync(IAsyncEnumerable<IMessage> msgs)
        {
            ThrowIfNotStarted();
            await foreach (IMessage msg in msgs)
            {
                if (!publisher!.IsAlive())
                {
                    break;
                }
                
                publisher!.Publish(msg);
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
                : $"[RosChannelWriter {publisher.Topic} [{publisher.TopicType}]]";
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
                publisher.Unadvertise(publisherId!);
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
                await publisher.UnadvertiseAsync(publisherId!);
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