using System;
using System.Collections.Generic;
using System.Net;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    public class RosPublisher
    {
        readonly TcpSenderManager manager;
        readonly List<string> ids = new List<string>();
        readonly RosClient client;
        int totalPublishers;

        public bool IsAlive { get; private set; }
        public string Topic => manager.Topic;
        public string TopicType => manager.TopicType;
        public int NumSubscribers => manager.NumConnections;
        public int NumIds => ids.Count;

        public bool LatchingEnabled
        {
            get => manager.Latching;
            set => manager.Latching = value;
        }

        public int MaxQueueSize
        {
            get => manager.MaxQueueSize;
            set => manager.MaxQueueSize = value;
        }

        public int TimeoutInMs
        {
            get => manager.TimeoutInMs;
            set => manager.TimeoutInMs = value;
        }

        public event Action<RosPublisher> NumSubscribersChanged;

        internal RosPublisher(RosClient client, TcpSenderManager manager)
        {
            this.client = client;
            this.manager = manager;
            MaxQueueSize = 3;
            IsAlive = true;
        }

        void AssertIsAlive()
        {
            if (!IsAlive)
            {
                throw new ObjectDisposedException("This is not a valid publisher");
            }
        }

        string GenerateId()
        {
            string newId = totalPublishers == 0 ? Topic : $"{Topic}-{totalPublishers}";
            totalPublishers++;
            return newId;
        }

        public PublisherTopicState GetState()
        {
            AssertIsAlive();
            Cleanup();
            return new PublisherTopicState(Topic, TopicType, ids.ToArray(), manager.GetStates());
        }

        public void Publish(IMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.RosValidate();
            AssertIsAlive();
            manager.Publish(message);
        }

        public void Cleanup()
        {
            if (manager.Cleanup())
            {
                NumSubscribersChanged?.Invoke(this);
            }
        }

        internal void RequestTopicRpc(string remoteCallerId, out string hostname, out int port)
        {
            IPEndPoint endPoint = manager.CreateConnection(remoteCallerId);
            hostname = manager.CallerUri.Host;
            port = endPoint.Port;

            NumSubscribersChanged?.Invoke(this);
        }

        internal void Stop()
        {
            ids.Clear();
            manager.Stop();
            NumSubscribersChanged = null;
            IsAlive = false;
        }

        public string Advertise()
        {
            AssertIsAlive();

            string id = GenerateId();
            ids.Add(id);

#if DEBUG__
            Logger.LogDebug($"{this}: Advertising '{Topic}' with type {TopicType} and id '{id}'");
#endif

            return id;
        }

        public bool Unadvertise(string topicId)
        {
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            AssertIsAlive();
            int index = ids.IndexOf(topicId);
            if (index < 0)
            {
                return false;
            }

            ids.RemoveAt(index);

#if DEBUG__
            Logger.LogDebug($"{this}: Unadvertising '{Topic}' with type {TopicType} and id '{topicId}'");
#endif

            if (ids.Count == 0)
            {
                Stop();
                client.RemovePublisher(this);

#if DEBUG__
                Logger.LogDebug($"{this}: Removing publisher '{Topic}' with type {TopicType}");
#endif
            }

            return true;
        }

        public bool ContainsId(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return ids.Contains(id);
        }
    }
}