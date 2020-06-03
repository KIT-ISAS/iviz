using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Iviz.Msgs;

namespace Iviz.RoslibSharp
{
    class TcpSenderManager
    {
        readonly TopicInfo topicInfo;
        readonly Dictionary<string, TcpSender> connectionsByCallerId = new Dictionary<string, TcpSender>();

        public readonly Uri CallerUri;
        public string Topic => topicInfo.Topic;
        public string CallerId => topicInfo.CallerId;
        public string TopicType => topicInfo.Type;
        public int NumConnections => connectionsByCallerId.Count;

        public int TimeoutInMs { get; set; }

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

        int maxQueueSize;
        public int MaxQueueSize
        {
            get => maxQueueSize;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException($"Cannot set max queue size to {value}");
                }
                maxQueueSize = value;
                lock (connectionsByCallerId)
                {
                    connectionsByCallerId.Values.ForEach(x => x.MaxQueueSizeBytes = value);
                }
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
            TcpSender connection = new TcpSender(CallerUri, remoteCallerId, topicInfo, Latching);

            IPEndPoint endPoint = null;
            lock (connectionsByCallerId)
            {
                if (connectionsByCallerId.TryGetValue(remoteCallerId, out TcpSender sender))
                {
                    if (sender.IsAlive)
                    {
                        Logger.LogDebug($"{this}: '{remoteCallerId} is requesting {Topic} again?");
                        sender.Stop();
                    }
                }

                endPoint = connection.Start(TimeoutInMs);
                connectionsByCallerId[remoteCallerId] = connection;
            }

            // while we're here
            Cleanup();

            for (int i = 0; i < 10 && connection.Status == SenderStatus.Inactive; i++)
            {
                Thread.Sleep(10);
            }

            if (Latching && LatchedMessage != null)
            {
                connection.Publish(LatchedMessage);
            }
            connection.MaxQueueSizeBytes = MaxQueueSize;
            return endPoint;
        }

        public void Cleanup()
        {
            lock (connectionsByCallerId)
            {
                string[] toDelete = connectionsByCallerId.
                    Where(x => !x.Value.IsAlive).
                    Select(x => x.Key).
                    ToArray();
                foreach (string callerId in toDelete)
                {
                    Logger.LogDebug($"{this}: Removing connection with '{callerId}' - dead x_x");
                    connectionsByCallerId[callerId].Stop();
                    connectionsByCallerId.Remove(callerId);
                }
            }
        }

        public void Publish(IMessage msg)
        {
            if (Latching)
            {
                LatchedMessage = msg;
            }
            lock (connectionsByCallerId)
            {
                //Logger.Log($"{this}: Sending to {connectionsByCallerId.Count} listeners!");
                foreach (TcpSender connection in connectionsByCallerId.Values)
                {
                    connection.Publish(msg);
                }
            }
        }

        public void Stop()
        {
            lock (connectionsByCallerId)
            {
                connectionsByCallerId.Values.ForEach(x => x.Stop());
                connectionsByCallerId.Clear();
            }
        }

        public ReadOnlyCollection<PublisherSenderState> GetStates()
        {
            lock (connectionsByCallerId)
            {
                return new ReadOnlyCollection<PublisherSenderState>(
                    connectionsByCallerId.Values.Select(x => x.State).ToArray()
                    );
            }
        }

        public ReadOnlyDictionary<string, TcpSender> GetConnections()
        {
            lock (connectionsByCallerId)
            {
                return new ReadOnlyDictionary<string, TcpSender>(connectionsByCallerId);
            }
        }

        public override string ToString()
        {
            return $"[TcpSenderManager '{Topic}']";
        }
    }
}
