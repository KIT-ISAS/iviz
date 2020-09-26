using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal class TcpSenderManager
    {
        readonly TopicInfo topicInfo;

        readonly ConcurrentDictionary<string, TcpSenderAsync> connectionsByCallerId =
            new ConcurrentDictionary<string, TcpSenderAsync>();

        public Uri CallerUri { get; }
        public string Topic => topicInfo.Topic;
        public string CallerId => topicInfo.CallerId;
        public string TopicType => topicInfo.Type;

        public int NumConnections => connectionsByCallerId.Count;

        public int TimeoutInMs { get; set; } = 5000;

        public IMessage LatchedMessage { get; private set; }

        bool latching;

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

        int maxQueueSizeInBytes;

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

        public TcpSenderManager(TopicInfo topicInfo, Uri callerUri)
        {
            this.topicInfo = topicInfo;
            CallerUri = callerUri;
        }

        public IPEndPoint CreateConnection(string remoteCallerId)
        {
            Logger.LogDebug($"{this}: '{remoteCallerId}' is requesting {Topic}");
            TcpSenderAsync newSender = new TcpSenderAsync(CallerUri, remoteCallerId, topicInfo, Latching);

            if (connectionsByCallerId.TryGetValue(remoteCallerId, out TcpSenderAsync oldSender) &&
                oldSender.IsAlive)
            {
                Logger.LogDebug($"{this}: '{remoteCallerId}' is requesting {Topic} again? Closing old connection.");
                oldSender.Dispose();
            }

            SemaphoreSlim managerSignal = new SemaphoreSlim(0, 1);
            var endPoint = newSender.Start(TimeoutInMs, managerSignal);
            connectionsByCallerId[remoteCallerId] = newSender;

            // while we're here
            Cleanup();

            // wait until newSender is ready to accept
            const int waitInMs = 100;
            if (!managerSignal.Wait(waitInMs))
            {
                Logger.Log($"{this}: Sender start timeout?");
            }

            if (Latching && LatchedMessage != null)
            {
                newSender.Publish(LatchedMessage);
            }

            newSender.MaxQueueSizeInBytes = MaxQueueSizeInBytes;
            return endPoint;
        }

        bool Cleanup()
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

            return subscribersChanged;
        }

        public void Publish(IMessage msg)
        {
            if (Latching)
            {
                LatchedMessage = msg;
            }

            foreach (var connection in connectionsByCallerId.Values)
            {
                connection.Publish(msg);
            }
        }

        public void Stop()
        {
            foreach (var sender in connectionsByCallerId.Values)
            {
                sender.Dispose();
            }

            connectionsByCallerId.Clear();
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