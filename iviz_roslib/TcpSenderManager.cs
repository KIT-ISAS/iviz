using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Nito.AsyncEx;

namespace Iviz.Roslib
{
    internal sealed class TcpSenderManager<TMessage> where TMessage : IMessage
    {
        const int NewSenderTimeoutInMs = 1000;
        const int DefaultTimeoutInMs = 5000;

        readonly ConcurrentDictionary<string, TcpSenderAsync<TMessage>> connectionsByCallerId =
            new();

        readonly RosPublisher<TMessage> publisher;
        readonly TopicInfo<TMessage> topicInfo;
        int maxQueueSizeInBytes;

        bool latching;
        readonly Nullable<TMessage> latchedMessage = new ();

        bool forceTcpNoDelay;

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

                foreach (var pair in connectionsByCallerId)
                {
                    pair.Value.TcpNoDelay = true;
                }
            }
        }

        public TcpSenderManager(RosPublisher<TMessage> publisher, TopicInfo<TMessage> topicInfo)
        {
            this.publisher = publisher;
            this.topicInfo = topicInfo;
        }

        public string Topic => topicInfo.Topic;
        public string TopicType => topicInfo.Type;

        public int NumConnections
        {
            get
            {
                TryToCleanup().WaitNoThrow(this);
                return connectionsByCallerId.Count;
            }
        }

        public int TimeoutInMs { get; set; } = DefaultTimeoutInMs;

        public bool Latching
        {
            get => latching;
            set
            {
                latching = value;
                latchedMessage.Unset();
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
                foreach (TcpSenderAsync<TMessage> sender in connectionsByCallerId.Values)
                {
                    sender.MaxQueueSizeInBytes = value;
                }
            }
        }

        public Endpoint? CreateConnectionRpc(string remoteCallerId)
        {
            // while we're here
            Task cleanupTask = TryToCleanup();

            Logger.LogDebugFormat("{0}: '{1}' is requesting {2}", this, remoteCallerId, Topic);
            TcpSenderAsync<TMessage> newSender; //= new(remoteCallerId, topicInfo, Latching);

            using (SemaphoreSlim managerSignal = new(0))
            {
                newSender =
                    new TcpSenderAsync<TMessage>(remoteCallerId, topicInfo, Latching, TimeoutInMs, managerSignal);
                //endPoint = newSender.Start(TimeoutInMs, managerSignal);

                connectionsByCallerId.AddOrUpdate(remoteCallerId, newSender, (_, oldSender) =>
                {
                    // in case of double requests, we kill the old connection
                    // this happens if our uri is set multiple times because a previous client did not
                    // shut down gracefully

                    Logger.LogDebugFormat("{0}: '{1}' duplicate.\n--Retaining \t{2}\n--Killing \t{3}",
                        this, oldSender.RemoteCallerId, newSender, oldSender);
                    return newSender;
                });

                // wait until newSender is ready to accept
                // should last only a couple of ms
                if (!managerSignal.Wait(NewSenderTimeoutInMs))
                {
                    // or maybe not. either the requester took too long, or did a double request,
                    // and this is the one that got killed
                    Logger.LogFormat("{0}: Sender start timed out!", this);
                }
            }

            if (Latching && latchedMessage.HasValue)
            {
                newSender.Publish(latchedMessage.Value);
            }

            newSender.MaxQueueSizeInBytes = MaxQueueSizeInBytes;
            publisher.RaiseNumConnectionsChanged();

            cleanupTask.WaitNoThrow(this);

            // return null if this connection got killed, which gets translated later as an error response
            //return !newSender.IsAlive ? null : endPoint;
            return !newSender.IsAlive ? null : newSender.Endpoint;
        }

        Task TryToCleanup()
        {
            var toDelete = connectionsByCallerId.Values.Where(sender => !sender.IsAlive).ToArray();
            if (toDelete.Length == 0)
            {
                return Task.CompletedTask;
            }

            return Task.Run(async () =>
            {
                var tasks = toDelete.Select(async sender =>
                {
                    await sender.DisposeAsync().AwaitNoThrow(this);
                    if (connectionsByCallerId.RemovePair(sender.RemoteCallerId, sender))
                    {
                        Logger.LogDebugFormat("{0}: Removing connection with '{1}' - dead x_x", this, sender);
                    }
                });
                await tasks.WhenAll().AwaitNoThrow(this);

                publisher.RaiseNumConnectionsChanged();
            });
        }

        public void Publish(in TMessage msg)
        {
            if (Latching)
            {
                latchedMessage.Value = msg;
            }

            foreach (var pair in connectionsByCallerId)
            {
                pair.Value.Publish(msg);
            }
        }

        public async Task<bool> PublishAndWaitAsync(TMessage msg, CancellationToken token)
        {
            if (Latching)
            {
                latchedMessage.Value = msg;
            }

            if (connectionsByCallerId.IsEmpty)
            {
                return false;
            }

            await connectionsByCallerId
                .Select(pair => pair.Value.PublishAndWaitAsync(msg, token))
                .WhenAll();
            return true;
        }

        public void Stop()
        {
            Task.Run(StopAsync).WaitNoThrow(this);
        }

        public async Task StopAsync()
        {
            await connectionsByCallerId.Values.Select(sender => sender.DisposeAsync()).WhenAll().AwaitNoThrow(this);
            connectionsByCallerId.Clear();
            latchedMessage.Unset();
        }

        public ReadOnlyCollection<PublisherSenderState> GetStates()
        {
            return new(
                connectionsByCallerId.Values.Select(sender => sender.State).ToArray()
            );
        }

        public ReadOnlyCollection<IRosTcpSender> GetConnections()
        {
            return connectionsByCallerId.Values.Cast<IRosTcpSender>().ToArray().AsReadOnly();
        }

        public override string ToString()
        {
            return $"[TcpSenderManager '{Topic}']";
        }

        class Nullable<T>
        {
            T? element;
            public bool HasValue { get; private set; }

            public T Value
            {
                get => HasValue ? element! : throw new NullReferenceException();
                set
                {
                    element = value ?? throw new ArgumentNullException(nameof(value));
                    HasValue = true;
                }
            }

            public void Unset()
            {
                element = default;
                HasValue = false;
            }
        }
    }
}