using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.Roslib
{
    internal sealed class TcpSenderManager<TMessage> where TMessage : IMessage
    {
        const int DefaultTimeoutInMs = 5000;

        readonly ConcurrentSet<TcpSenderAsync<TMessage>> senders = new();
        readonly RosPublisher<TMessage> publisher;
        readonly TopicInfo<TMessage> topicInfo;
        readonly CancellationTokenSource tokenSource = new();
        readonly TcpListener listener;
        readonly Task task;

        NullableMessage<TMessage> latchedMessage;

        bool KeepRunning => !tokenSource.IsCancellationRequested;

        int maxQueueSizeInBytes;
        bool latching;
        bool forceTcpNoDelay;
        bool disposed;

        public Endpoint Endpoint => new((IPEndPoint) listener.LocalEndpoint);

        public bool ForceTcpNoDelay
        {
            get => forceTcpNoDelay;
            set
            {
                forceTcpNoDelay = value;
                if (!value)
                {
                    return;
                }

                foreach (var sender in senders)
                {
                    sender.TcpNoDelay = true;
                }
            }
        }

        public string Topic => topicInfo.Topic;
        public string TopicType => topicInfo.Type;

        public int NumConnections => senders.Count(sender => sender.IsAlive);

        public int TimeoutInMs { get; set; } = DefaultTimeoutInMs;

        public bool Latching
        {
            get => latching;
            set
            {
                latching = value;
                latchedMessage = default;
            }
        }

        public int MaxQueueSizeInBytes
        {
            get => maxQueueSizeInBytes;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException($"Cannot set max queue size to {value}");
                }

                maxQueueSizeInBytes = value;
                foreach (TcpSenderAsync<TMessage> sender in senders)
                {
                    sender.MaxQueueSizeInBytes = value;
                }
            }
        }

        public TcpSenderManager(RosPublisher<TMessage> publisher, TopicInfo<TMessage> topicInfo)
        {
            this.publisher = publisher;
            this.topicInfo = topicInfo;

            listener = new TcpListener(IPAddress.IPv6Any, 0) {Server = {DualMode = true}};
            listener.Start();

            task = TaskUtils.StartLongTask(RunLoop);

            Logger.LogDebugFormat("{0}: Starting at :{1}", this, Endpoint.Port.ToString());
        }


        async Task RunLoop()
        {
            try
            {
                while (KeepRunning)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    if (!KeepRunning)
                    {
                        break;
                    }

                    if (client.Client.RemoteEndPoint == null || client.Client.LocalEndPoint == null)
                    {
                        Logger.LogFormat("{0}: Received a request, but failed to initialize connection.", this);
                        continue;
                    }

                    var sender = new TcpSenderAsync<TMessage>(client, topicInfo, latchedMessage);
                    if (ForceTcpNoDelay)
                    {
                        sender.TcpNoDelay = true;
                    }
                    
                    if (publisher.TryGetLoopbackReceiver(sender.RemoteEndpoint, out var loopbackReceiver))
                    {
                        sender.LoopbackReceiver = loopbackReceiver;
                    }

                    senders.Add(sender);
                    await CleanupAsync(tokenSource.Token);
                }
            }
            catch (Exception e)
            {
                if (e is not (ObjectDisposedException or OperationCanceledException))
                {
                    Logger.LogFormat("{0}: Stopped thread {1}", this, e);
                }

                return;
            }

            Logger.LogDebugFormat("{0}: Leaving task", this); // also expected
        }

        async Task CleanupAsync(CancellationToken token)
        {
            if (senders.Count == 0)
            {
                return;
            }

            var sendersToDelete = senders.Where(sender => !sender.IsAlive).ToArray();
            if (sendersToDelete.Length == 0)
            {
                return;
            }

            var tasks = sendersToDelete.Select(async sender =>
            {
                await sender.DisposeAsync(token).AwaitNoThrow(this);
                if (senders.Remove(sender))
                {
                    Logger.LogDebugFormat("{0}: Removing connection with '{1}' - dead x_x", this, sender);
                }
            });
            await tasks.WhenAll().AwaitNoThrow(this);

            publisher.RaiseNumConnectionsChanged();
        }

        public void Publish(in TMessage msg)
        {
            if (Latching)
            {
                latchedMessage = new NullableMessage<TMessage>(msg);
            }

            foreach (var sender in senders)
            {
                sender.Publish(msg);
            }
        }

        public async Task<bool> PublishAndWaitAsync(TMessage msg, CancellationToken token)
        {
            if (Latching)
            {
                latchedMessage = new NullableMessage<TMessage>(msg);
            }

            if (senders.Count == 0)
            {
                return false;
            }

            await senders.Select(sender => sender.PublishAndWaitAsync(msg, token)).WhenAll();
            return true;
        }

        public void Dispose()
        {
            Task.Run(() => DisposeAsync(default)).WaitNoThrow(this);
        }

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            tokenSource.Cancel();

            using (TcpClient client = new(AddressFamily.InterNetworkV6) {Client = {DualMode = true}})
            {
                await client.ConnectAsync(IPAddress.Loopback, Endpoint.Port);
            }

            listener.Stop();
            if (!await task.AwaitFor(2000, token))
            {
                Logger.LogDebugFormat("{0}: Listener stuck. Abandoning.", this);
            }

            await senders.Select(sender => sender.DisposeAsync(token)).WhenAll().AwaitNoThrow(this);
            senders.Clear();
            latchedMessage = default;
        }

        public ReadOnlyCollection<PublisherSenderState> GetStates()
        {
            return new(
                senders.Select(sender => sender.State).ToArray()
            );
        }

        public ReadOnlyCollection<IRosTcpSender> GetConnections()
        {
            return senders.Cast<IRosTcpSender>().ToArray().AsReadOnly();
        }

        public override string ToString()
        {
            return $"[TcpSenderManager '{Topic}']";
        }
    }

    internal readonly struct NullableMessage<T> where T : IMessage
    {
        readonly T? element;
        public bool HasValue { get; }
        public T Value => HasValue ? element! : throw new NullReferenceException();
        public NullableMessage(T? element) => (this.element, HasValue) = (element, true);
    }
}