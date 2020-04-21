using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.RoslibSharp;
using Utf8Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Iviz.Bridge
{
    class SocketConnection : WebSocketBehavior
    {
        public static RosBridge client;

        readonly Dictionary<string, Subscription> subscriptions = new Dictionary<string, Subscription>();
        readonly Dictionary<string, Advertisement> advertisements = new Dictionary<string, Advertisement>();
        readonly object condVar = new object();
        string endPoint = "";

        readonly Queue<PublishMessage> queue = new Queue<PublishMessage>();
        readonly Task task;
        bool keepGoing = true;

        public SocketConnection()
        {
            task = Task.Run(Run);
        }

        protected override void OnOpen()
        {
            base.OnOpen();

            endPoint = Context.UserEndPoint.ToString();
            Console.WriteLine("++ " + endPoint);
            client.AddConnection(this);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
            Console.WriteLine("EE " + endPoint + ": " + e.Message);
            Close();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            Console.WriteLine("-- " + endPoint + ": " + e.Reason);
            Close();
        }

        void Close()
        {
            lock (subscriptions)
            {
                foreach (Subscription subscription in subscriptions.Values)
                {
                    subscription.Close();
                }
                subscriptions.Clear();
            }
            lock (advertisements)
            {
                foreach (Advertisement advertisement in advertisements.Values)
                {
                    advertisement.Close();
                }
                advertisements.Clear();
            }

            client.RemoveConnection(this);

            keepGoing = false;
            lock (condVar)
            {
                Monitor.Pulse(condVar);
            }
            task.Wait();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);

            try
            {
                string data;
                if (e.IsBinary)
                {
                    data = Encoding.UTF8.GetString(e.RawData);
                }
                else
                {
                    data = e.Data;
                }

                Subscription subscription;
                Advertisement advertisement;
                GenericMessage msg = JsonSerializer.Deserialize<GenericMessage>(data);
                switch (msg.op)
                {
                    case "publish":
                        if (!TryGetAdvertisement(msg.topic, out advertisement) ||
                            advertisement.publisher.NumSubscribers == 0)
                        {
                            break;
                        }
                        IMessage outMsg = advertisement.Deserialize(data);
                        advertisement.publisher.Publish(outMsg);
                        break;
                    case "subscribe":
                        if (!TryGetSubscription(msg.topic, out subscription))
                        {
                            client.types.TryGetType(msg.type, out TypeInfo typeInfo);
                            string subscriptionId = client.rosClient.Subscribe(
                                msg.topic,
                                x => MessageCallback(msg.topic, x),
                                typeInfo.msgType,
                                out RosSubscriber subscriber,
                                true);
                            subscription = new Subscription(
                                msg.topic, typeInfo, subscriber, subscriptionId);
                            AddSubscription(msg.topic, subscription);
                        }
                        subscription.AddId(msg.id);
                        break;
                    case "unsubscribe":
                        if (TryGetSubscription(msg.topic, out subscription))
                        {
                            subscription.RemoveId(msg.id);
                            if (subscription.Empty())
                            {
                                subscription.Close();
                                RemoveSubscription(msg.topic);
                            }
                        }
                        break;
                    case "advertise":
                        if (!TryGetAdvertisement(msg.topic, out advertisement))
                        {
                            client.types.TryGetType(msg.type, out TypeInfo typeInfo);
                            string advertisementId = client.rosClient.Advertise(
                                msg.topic,
                                typeInfo.msgType,
                                out RosPublisher publisher
                                );
                            advertisement = new Advertisement(
                                msg.topic, typeInfo, publisher, advertisementId);
                            AddAdvertisement(msg.topic, advertisement);
                        }
                        advertisement.AddId(msg.id);
                        break;
                    case "unadvertise":
                        if (TryGetAdvertisement(msg.topic, out advertisement))
                        {
                            advertisement.RemoveId(msg.id);
                            if (advertisement.Empty())
                            {
                                advertisement.Close();
                                RemoveAdvertisement(msg.topic);
                            }
                        }
                        break;
                }
            }
            catch (Exception ee)
            {
                Console.Error.WriteLine(ee);
            }
        }

        public void MessageCallback(string topic, IMessage inMsg)
        {
            if (!TryGetSubscription(topic, out Subscription subscription))
            {
                return;
            }
            PublishMessage msg = subscription.GeneratePublishMessage();
            msg.op = "publish";
            msg.topic = topic;
            msg.SetMessage(inMsg);

            lock (condVar)
            {
                queue.Enqueue(msg);
                if (queue.Count > 3)
                {
                    queue.Dequeue();
                }
                Monitor.Pulse(condVar);
            }
        }

        void Run()
        {
            try
            {
                while (keepGoing)
                {
                    lock (condVar)
                    {
                        Monitor.Wait(condVar, 1000);
                    }
                    while (true)
                    {
                        PublishMessage msg;
                        lock (condVar)
                        {
                            if (queue.Count == 0 || !keepGoing)
                            {
                                break;
                            }
                            msg = queue.Dequeue();
                        }
                        Send(msg.Serialize());
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }
        }

        bool TryGetSubscription(string topic, out Subscription subscription)
        {
            lock (subscriptions)
            {
                return subscriptions.TryGetValue(topic, out subscription);
            }
        }

        bool TryGetAdvertisement(string topic, out Advertisement advertisement)
        {
            lock (subscriptions)
            {
                return advertisements.TryGetValue(topic, out advertisement);
            }
        }

        void AddSubscription(string topic, Subscription subscription)
        {
            lock (subscriptions)
            {
                subscriptions.Add(topic, subscription);
            }
        }

        void AddAdvertisement(string topic, Advertisement advertisement)
        {
            lock (advertisements)
            {
                advertisements.Add(topic, advertisement);
            }
        }

        void RemoveSubscription(string topic)
        {
            lock (subscriptions)
            {
                subscriptions.Remove(topic);
            }
        }

        void RemoveAdvertisement(string topic)
        {
            lock (advertisements)
            {
                advertisements.Remove(topic);
            }
        }
    }
}
