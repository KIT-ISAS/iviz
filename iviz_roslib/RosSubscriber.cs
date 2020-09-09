using System;
using System.Collections.Generic;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    public class RosSubscriber
    {
        readonly List<string> ids = new List<string>();
        readonly List<Action<IMessage>> callbackList = new List<Action<IMessage>>();
        readonly TcpReceiverManager manager;
        readonly RosClient client;

        int totalSubscribers;

        public bool IsAlive { get; private set; }
        public string Topic => manager.Topic;
        public string TopicType => manager.TopicType;
        public int NumPublishers => manager.NumConnections;
        public int NumIds => ids.Count;
        public bool RequestNoDelay => manager.RequestNoDelay;

        public event Action<RosSubscriber> NumPublishersChanged;
        
        public int TimeoutInMs
        {
            get => manager.TimeoutInMs;
            set => manager.TimeoutInMs = value;
        }

        internal RosSubscriber(RosClient client, TcpReceiverManager manager)
        {
            this.client = client;
            this.manager = manager;
            IsAlive = true;

            manager.Subscriber = this;
        }

        internal void MessageCallback(IMessage msg)
        {
            lock (callbackList)
            {
                foreach (Action<IMessage> callback in callbackList)
                {
                    callback(msg);
                }
            }
        }

        string GenerateId()
        {
            string newId = totalSubscribers == 0 ? Topic : $"{Topic}-{totalSubscribers}";
            totalSubscribers++;
            return newId;
        }

        void AssertIsAlive()
        {
            if (!IsAlive)
            {
                throw new ObjectDisposedException("This is not a valid subscriber");
            }
        }

        public void Cleanup()
        {
            if (manager.Cleanup())
            {
                NumPublishersChanged?.Invoke(this);
            }
        }

        public SubscriberTopicState GetState()
        {
            AssertIsAlive();
            Cleanup();
            return new SubscriberTopicState(Topic, TopicType, ids, manager.GetStates());
        }

        internal void PublisherUpdateRcp(Uri[] publisherUris)
        {
            if (manager.PublisherUpdateRpc(client, publisherUris))
            {
                NumPublishersChanged?.Invoke(this);
            }
        }

        internal void Stop()
        {
            ids.Clear();
            manager.Stop();
            NumPublishersChanged = null;
            IsAlive = false;
        }

        public bool MessageTypeMatches(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type == manager.TopicInfo.Generator.GetType();
        }

        public string Subscribe(Action<IMessage> callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            AssertIsAlive();

#if DEBUG__
            Logger.LogDebug($"{this}: Subscribing to '{Topic}' with type '{TopicType}'");
#endif

            lock (callbackList)
            {
                string id = GenerateId();
                ids.Add(id);
                callbackList.Add(callback);
                return id;
            }
        }

        public bool ContainsId(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return ids.Contains(id);
        }

        public bool Unsubscribe(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            AssertIsAlive();
            lock (callbackList)
            {
                int index = ids.IndexOf(id);
                if (index < 0)
                {
                    return false;
                }
                ids.RemoveAt(index);
                callbackList.RemoveAt(index);
            }

#if DEBUG__
            Logger.LogDebug($"{this}: Unsubscribing to '{Topic}' with type '{TopicType}'");
#endif

            if (ids.Count == 0)
            {
                Stop();
                client.RemoveSubscriber(this);

#if DEBUG__
                Logger.LogDebug($"{this}: Removing subscription '{Topic}' with type '{TopicType}'");
#endif
            }
            return true;
        }
    }
}
