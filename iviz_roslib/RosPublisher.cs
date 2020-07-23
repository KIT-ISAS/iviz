using System;
using System.Collections.Generic;
using System.Net;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    public class RosPublisher
    {
        readonly TcpSenderManager Manager;
        readonly List<string> Ids = new List<string>();
        readonly RosClient Client;
        int totalPublishers;

        public bool IsAlive { get; private set; }
        public string Topic => Manager.Topic;
        public string TopicType => Manager.TopicType;
        public int NumSubscribers => Manager.NumConnections;
        public int NumIds => Ids.Count;
        public bool LatchingEnabled
        {
            get => Manager.Latching;
            set => Manager.Latching = value;
        }
        public int MaxQueueSize
        {
            get => Manager.MaxQueueSize;
            set => Manager.MaxQueueSize = value;
        }
        public int TimeoutInMs
        {
            get => Manager.TimeoutInMs;
            set => Manager.TimeoutInMs = value;
        }

        internal RosPublisher(RosClient client, TcpSenderManager manager)
        {
            Client = client;
            Manager = manager;
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
            return new PublisherTopicState(Topic, TopicType, Ids.ToArray(), Manager.GetStates());
        }

        public void Publish(IMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            message.RosValidate();
            AssertIsAlive();
            Manager.Publish(message);
        }

        public void Cleanup()
        {
            Manager.Cleanup();
        }

        internal void RequestTopicRpc(string remoteCallerId, out string hostname, out int port)
        {
            IPEndPoint endPoint = Manager.CreateConnection(remoteCallerId);
            hostname = Manager.CallerUri.Host;
            port = endPoint.Port;
        }

        internal void Stop()
        {
            Ids.Clear();
            Manager.Stop();
        }

        public string Advertise()
        {
            AssertIsAlive();

            string id = GenerateId();
            Ids.Add(id);

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
            int index = Ids.IndexOf(topicId);
            if (index < 0)
            {
                return false;
            }
            Ids.RemoveAt(index);

#if DEBUG__
            Logger.LogDebug($"{this}: Unadvertising '{Topic}' with type {TopicType} and id '{topicId}'");
#endif

            if (Ids.Count == 0)
            {
                Stop();
                Client.RemovePublisher(this);

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

            return Ids.Contains(id);
        }
    }
}
