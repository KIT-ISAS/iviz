using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal sealed class TcpSenderManager<TMessage> where TMessage : IMessage
    {
        const int NewSenderTimeoutInMs = 1000;
        const int DefaultTimeoutInMs = 5000;

        readonly ConcurrentDictionary<string, TcpSenderAsync<TMessage>> connectionsByCallerId =
            new ConcurrentDictionary<string, TcpSenderAsync<TMessage>>();

        readonly RosPublisher<TMessage> publisher;
        readonly TopicInfo<TMessage> topicInfo;
        int maxQueueSizeInBytes;

        bool latching;
        bool hasLatchedMessage;
        TMessage latchedMessage = default!;

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
                Cleanup();
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
                if (!value)
                {
                    hasLatchedMessage = false;
                }
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
            Logger.LogDebugFormat("{0}: '{1}' is requesting {2}", this, remoteCallerId, Topic);
            TcpSenderAsync<TMessage> newSender = new TcpSenderAsync<TMessage>(remoteCallerId, topicInfo, Latching);

            Endpoint endPoint;
            using (SemaphoreSlim managerSignal = new SemaphoreSlim(0, 1))
            {
                endPoint = newSender.Start(TimeoutInMs, managerSignal);

                connectionsByCallerId.AddOrUpdate(remoteCallerId, newSender, (_, oldSender) =>
                {
                    // in case of double requests, we kill the old connection
                    // this happens if our uri is set multiple times because a previous client did not
                    // shut down gracefully

                    Logger.LogDebugFormat("{0}: '{1}' duplicate.\n--Retaining \t{2}\n--Killing \t{3}",
                        this, oldSender.RemoteCallerId, newSender, oldSender);
                    return newSender;
                });

                // while we're here
                Cleanup();

                // wait until newSender is ready to accept
                // should last only a couple of ms
                if (!managerSignal.Wait(NewSenderTimeoutInMs))
                {
                    // or maybe not. either the requester took too long, or did a double request,
                    // and this is the one that got killed
                    Logger.LogFormat("{0}: Sender start timed out!", this);
                }
            }

            if (Latching && hasLatchedMessage)
            {
                newSender.Publish(latchedMessage);
            }

            newSender.MaxQueueSizeInBytes = MaxQueueSizeInBytes;
            publisher.RaiseNumConnectionsChanged();

            // return null if this connection got killed, which gets translated later as an error response
            return !newSender.IsAlive ? null : endPoint;
        }

        void Cleanup()
        {
            TcpSenderAsync<TMessage>[] toDelete = connectionsByCallerId.Values.Where(sender => !sender.IsAlive).ToArray();
            foreach (TcpSenderAsync<TMessage> sender in toDelete)
            {
                sender.Dispose();
                if (connectionsByCallerId.RemovePair(sender.RemoteCallerId, sender))
                {
                    Logger.LogDebugFormat("{0}: Removing connection with '{1}' - dead x_x", this, sender);
                }
            }

            var subscribersChanged = toDelete.Length != 0;
            if (subscribersChanged)
            {
                publisher.RaiseNumConnectionsChanged();
            }
        }

        public void Publish(in TMessage msg)
        {
            if (Latching)
            {
                hasLatchedMessage = true;
                latchedMessage = msg;
            }

            foreach (var pair in connectionsByCallerId)
            {
                pair.Value.Publish(msg);
            }
        }

        public void Stop()
        {
            foreach (TcpSenderAsync<TMessage> sender in connectionsByCallerId.Values)
            {
                sender.Dispose();
            }

            connectionsByCallerId.Clear();
        }

        public ReadOnlyCollection<PublisherSenderState> GetStates()
        {
            return new ReadOnlyCollection<PublisherSenderState>(
                connectionsByCallerId.Values.Select(sender => sender.State).ToArray()
            );
        }

        public override string ToString()
        {
            return $"[TcpSenderManager '{Topic}']";
        }
    }
}