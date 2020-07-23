using System;
using System.Collections.Generic;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    public class RosSubscriber
    {
        readonly List<string> Ids = new List<string>();

        readonly List<Action<IMessage>> CallbackList = new List<Action<IMessage>>();

        readonly TcpReceiverManager Manager;
        readonly RosClient Client;

        int totalSubscribers;

        public bool IsAlive { get; private set; }
        public string Topic => Manager.Topic;
        public string TopicType => Manager.TopicType;
        public int NumPublishers => Manager.NumConnections;
        public int NumIds => Ids.Count;
        public bool RequestNoDelay => Manager.RequestNoDelay;

        public int TimeoutInMs
        {
            get => Manager.TimeoutInMs;
            set => Manager.TimeoutInMs = value;
        }

        internal RosSubscriber(RosClient client, TcpReceiverManager manager)
        {
            Client = client;
            Manager = manager;
            IsAlive = true;

            manager.Callback = MessageCallback;
        }

        void MessageCallback(IMessage msg)
        {
            lock (CallbackList)
            {
                foreach (Action<IMessage> callback in CallbackList)
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
                throw new ObjectDisposedException("This is not a valid subscriber");
        }

        public void Cleanup()
        {
            Manager.Cleanup();
        }

        public SubscriberTopicState GetState()
        {
            AssertIsAlive();
            Cleanup();
            return new SubscriberTopicState(Topic, TopicType, Ids, Manager.GetStates());
        }

        internal void PublisherUpdateRcp(Uri[] publisherUris)
        {
            Manager.PublisherUpdateRpc(Client, publisherUris);
        }

        internal void Stop()
        {
            Ids.Clear();
            Manager.Stop();
            IsAlive = false;
        }

        public bool MessageTypeMatches(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.Equals(Manager.TopicInfo.Generator.GetType());
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

            lock (CallbackList)
            {
                string id = GenerateId();
                Ids.Add(id);
                CallbackList.Add(callback);
                return id;
            }
        }

        public bool ContainsId(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return Ids.Contains(id);
        }

        public bool Unsubscribe(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            AssertIsAlive();
            lock (CallbackList)
            {
                int index = Ids.IndexOf(id);
                if (index < 0)
                {
                    return false;
                }
                Ids.RemoveAt(index);
                CallbackList.RemoveAt(index);
            }

#if DEBUG__
            Logger.LogDebug($"{this}: Unsubscribing to '{Topic}' with type '{TopicType}'");
#endif

            if (Ids.Count == 0)
            {
                Stop();
                Client.RemoveSubscriber(this);

#if DEBUG__
                Logger.LogDebug($"{this}: Removing subscription '{Topic}' with type '{TopicType}'");
#endif
            }
            return true;
        }
    }
}
