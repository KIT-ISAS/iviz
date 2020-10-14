using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal class TcpSenderManager
    {
        readonly ConcurrentDictionary<string, TcpSenderAsync> connectionsByCallerId =
            new ConcurrentDictionary<string, TcpSenderAsync>();

        readonly TopicInfo topicInfo;
        bool latching;
        int maxQueueSizeInBytes;

        public event Action NumConnectionsChanged;

        public TcpSenderManager(TopicInfo topicInfo, Uri callerUri)
        {
            this.topicInfo = topicInfo;
            CallerUri = callerUri;
        }

        public Uri CallerUri { get; }
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

        public int TimeoutInMs { get; set; } = 5000;
        IMessage LatchedMessage { get; set; }

        public bool Latching
        {
            get => latching;
            set
            {
                latching = value;
                if (!value)
                {
                    LatchedMessage = null;
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
                connectionsByCallerId.Values.ForEach(x => x.MaxQueueSizeInBytes = value);
            }
        }

        public IPEndPoint CreateConnection(string remoteCallerId)
        {
            Logger.LogDebug($"{this}: '{remoteCallerId}' is requesting {Topic}");
            TcpSenderAsync newSender = new TcpSenderAsync(remoteCallerId, topicInfo, Latching);

            if (connectionsByCallerId.TryGetValue(remoteCallerId, out TcpSenderAsync oldSender) &&
                oldSender.IsAlive)
            {
                Logger.LogDebug($"{this}: '{remoteCallerId}' is requesting {Topic} again? Closing old connection.");
                oldSender.Dispose();
            }

            SemaphoreSlim managerSignal = new SemaphoreSlim(0, 1);
            IPEndPoint endPoint = newSender.Start(TimeoutInMs, managerSignal);
            connectionsByCallerId[remoteCallerId] = newSender;

            // while we're here
            Cleanup();

            // wait until newSender is ready to accept
            const int maxWaitInMs = 100;
            if (!managerSignal.Wait(maxWaitInMs))
            {
                Logger.Log($"{this}: Sender start timeout?");
            }

            if (Latching && LatchedMessage != null)
            {
                newSender.Publish(LatchedMessage);
            }

            newSender.MaxQueueSizeInBytes = MaxQueueSizeInBytes;
            NumConnectionsChanged?.Invoke();
            
            return endPoint;
        }

        void Cleanup()
        {
            bool subscribersChanged;
            TcpSenderAsync[] toDelete = connectionsByCallerId.Values.Where(sender => !sender.IsAlive).ToArray();
            foreach (TcpSenderAsync sender in toDelete)
            {
                Logger.LogDebug($"{this}: Removing connection with '{sender}' - dead x_x");
                sender.Dispose();
                connectionsByCallerId.TryRemove(sender.RemoteCallerId, out _);
            }

            subscribersChanged = toDelete.Length != 0;
            if (subscribersChanged)
            {
                NumConnectionsChanged?.Invoke();
            }
        }

        public void Publish(IMessage msg)
        {
            if (Latching)
            {
                LatchedMessage = msg;
            }

            foreach (var sender in connectionsByCallerId)
            {
                sender.Value.Publish(msg);
            }
        }

        public void Stop()
        {
            foreach (TcpSenderAsync sender in connectionsByCallerId.Values)
            {
                sender.Dispose();
            }

            connectionsByCallerId.Clear();
            NumConnectionsChanged = null;
        }

        public ReadOnlyCollection<PublisherSenderState> GetStates()
        {
            return new ReadOnlyCollection<PublisherSenderState>(
                connectionsByCallerId.Values.Select(x => x.State).ToArray()
            );
        }

        public override string ToString()
        {
            return $"[TcpSenderManager '{Topic}']";
        }
    }
}