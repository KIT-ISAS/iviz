using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.Roslib
{
    internal sealed class SenderManager<TMessage> where TMessage : IMessage
    {
        const int DefaultTimeoutInMs = 5000;

        readonly AsyncLock mutex = new();
        readonly HashSet<IProtocolSender<TMessage>> senders = new();
        IProtocolSender<TMessage>[] cachedSenders = Array.Empty<IProtocolSender<TMessage>>();

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

        public Endpoint Endpoint => new((IPEndPoint)listener.LocalEndpoint);

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

                var tcpSenders = cachedSenders.OfType<TcpSender<TMessage>>();
                foreach (var tcpSender in tcpSenders)
                {
                    tcpSender.TcpNoDelay = true;
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
                foreach (var sender in cachedSenders)
                {
                    sender.MaxQueueSizeInBytes = value;
                }
            }
        }

        public SenderManager(RosPublisher<TMessage> publisher, TopicInfo<TMessage> topicInfo)
        {
            this.publisher = publisher;
            this.topicInfo = topicInfo;

            listener = new TcpListener(IPAddress.IPv6Any, 0) { Server = { DualMode = true } };
            listener.Start();

            task = TaskUtils.StartLongTask(RunTcpReceiverLoop);

            Logger.LogDebugFormat("{0}: Starting at :{1}", this, Endpoint.Port.ToString());
        }


        async Task RunTcpReceiverLoop()
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

                    var sender = new TcpSender<TMessage>(client, topicInfo, latchedMessage);
                    if (ForceTcpNoDelay)
                    {
                        sender.TcpNoDelay = true;
                    }

                    if (publisher.TryGetLoopbackReceiver(sender.RemoteEndpoint, out var loopbackReceiver))
                    {
                        sender.LoopbackReceiver = loopbackReceiver;
                    }

                    using (await mutex.LockAsync(tokenSource.Token))
                    {
                        senders.Add(sender);
                        await CleanupAsync(tokenSource.Token);
                    }
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
            var allSenders = senders.ToArray();
            if (allSenders.All(deadSender => deadSender.IsAlive))
            {
                cachedSenders = senders.ToArray();
                return;
            }

            var sendersToDelete = allSenders.Where(deadSender => !deadSender.IsAlive).ToArray();

            var tasks = sendersToDelete.Select(async deadSender =>
            {
                await deadSender.DisposeAsync(token).AwaitNoThrow(this);
            }).ToArray();

            await tasks.WhenAll().AwaitNoThrow(this);

            foreach (var deadSender in sendersToDelete)
            {
                senders.Remove(deadSender);
                Logger.LogDebugFormat("{0}: Removing connection with '{1}' - dead x_x", this, deadSender);
            }

            publisher.RaiseNumSubscribersChanged();

            cachedSenders = senders.ToArray();
        }

        public RpcUdpTopicResponse CreateUdpConnection(RpcUdpTopicRequest request, string hostname)
        {
            var newSender = new UdpSender<TMessage>(request, topicInfo, latchedMessage, out byte[] responseHeader);

            if (publisher.TryGetLoopbackReceiver(newSender.RemoteEndpoint, out var loopbackReceiver))
            {
                newSender.LoopbackReceiver = loopbackReceiver;
            }

            Task.Run(async () =>
            {
                using (await mutex.LockAsync(tokenSource.Token))
                {
                    senders.Add(newSender);
                    cachedSenders = senders.ToArray();
                    await CleanupAsync(tokenSource.Token);
                }
            }).WaitNoThrow(this);

            return new RpcUdpTopicResponse(hostname, newSender.Endpoint.Port, 0, request.MaxPacketSize, responseHeader);
        }

        public void Publish(in TMessage msg)
        {
            if (Latching)
            {
                latchedMessage = new NullableMessage<TMessage>(msg);
            }

            foreach (var sender in cachedSenders)
            {
                sender.Publish(msg);
            }
        }

        public Task PublishAndWaitAsync(in TMessage msg, CancellationToken token)
        {
            if (Latching)
            {
                latchedMessage = new NullableMessage<TMessage>(msg);
            }

            var localSenders = cachedSenders;
            switch (localSenders.Length)
            {
                case 0:
                    return Task.CompletedTask;
                case 1:
                    return localSenders[0].PublishAndWaitAsync(msg, token);
                default:
                    var tasks = new Task[localSenders.Length];
                    for (int i = 0; i < localSenders.Length; i++)
                    {
                        tasks[i] = localSenders[i].PublishAndWaitAsync(msg, token);
                    }
            
                    return tasks.WhenAll();
            }
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

            using (var client = new TcpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } })
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

            cachedSenders = Array.Empty<IProtocolSender<TMessage>>();
            latchedMessage = default;
        }

        public PublisherSenderState[] GetStates() => cachedSenders.Select(sender => sender.State).ToArray();

        public override string ToString()
        {
            return $"[SenderManager '{Topic}']";
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