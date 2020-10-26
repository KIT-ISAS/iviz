using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal sealed class TcpSenderManager<T> where T : IMessage
    {
        const int NewSenderTimeoutInMs = 100;
        const int DefaultTimeoutInMs = 5000;

        readonly ConcurrentDictionary<string, TcpSenderAsync<T>> connectionsByCallerId =
            new ConcurrentDictionary<string, TcpSenderAsync<T>>();

        readonly TopicInfo<T> topicInfo;
        int maxQueueSizeInBytes;
        
        bool latching;
        bool hasLatchedMessage;
        T latchedMessage;

        public RosPublisher<T> Publisher { set; private get; }

        public TcpSenderManager(TopicInfo<T> topicInfo)
        {
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
                foreach (TcpSenderAsync<T> sender in connectionsByCallerId.Values)
                {
                    sender.MaxQueueSizeInBytes = value;
                }
            }
        }

        public Endpoint CreateConnectionRpc(string remoteCallerId)
        {
            Logger.LogDebug($"{this}: '{remoteCallerId}' is requesting {Topic}");
            TcpSenderAsync<T> newSender = new TcpSenderAsync<T>(remoteCallerId, topicInfo, Latching);

            Endpoint endPoint;
            using (SemaphoreSlim managerSignal = new SemaphoreSlim(0, 1))
            {
                endPoint = newSender.Start(TimeoutInMs, managerSignal);

                connectionsByCallerId.AddOrUpdate(remoteCallerId, newSender, (_, oldSender) =>
                {
                    Logger.LogDebug(
                        $"{this}: '{oldSender.RemoteCallerId}' is requesting {Topic} again? Closing old connection.");
                    oldSender.Dispose();
                    return newSender;
                });

                // while we're here
                Cleanup();

                // wait until newSender is ready to accept
                // should last only a couple of ms
                if (!managerSignal.Wait(NewSenderTimeoutInMs))
                {
                    // shouldn't happen
                    Logger.Log($"{this}: Sender start timeout?");
                }
            }

            if (Latching && hasLatchedMessage)
            {
                newSender.Publish(latchedMessage);
            }

            newSender.MaxQueueSizeInBytes = MaxQueueSizeInBytes;
            Publisher.RaiseNumConnectionsChanged();

            return endPoint;
        }

        void Cleanup()
        {
            bool subscribersChanged;
            TcpSenderAsync<T>[] toDelete = connectionsByCallerId.Values.Where(sender => !sender.IsAlive).ToArray();
            foreach (TcpSenderAsync<T> sender in toDelete)
            {
                sender.Dispose();
                if (connectionsByCallerId.RemovePair(sender.RemoteCallerId, sender))
                {
                    Logger.LogDebug($"{this}: Removing connection with '{sender}' - dead x_x");
                }
            }

            subscribersChanged = toDelete.Length != 0;
            if (subscribersChanged)
            {
                Publisher.RaiseNumConnectionsChanged();
            }
        }

        public void Publish(in T msg)
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
            foreach (TcpSenderAsync<T> sender in connectionsByCallerId.Values)
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