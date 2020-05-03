using System;
using System.Collections.Generic;
using System.Linq;
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

        readonly Dictionary<string, Listener> listeners = new Dictionary<string, Listener>();
        readonly Dictionary<string, Sender> senders = new Dictionary<string, Sender>();

        string endPoint = "";

        //readonly Queue<PublishMessage> queue = new Queue<PublishMessage>();
        //readonly Task task;
        //bool keepGoing = true;
        readonly ParallelQueue<PublishMessage> queue;


        public SocketConnection()
        {
            //task = Task.Run(Run);
            queue = new ParallelQueue<PublishMessage>(Process);
            queue.MaxSize = 3;
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

            lock (listeners)
            {
                foreach (var listener in listeners.Values)
                {
                    listener.Stop();
                }
                listeners.Clear();
            }
            lock (senders)
            {
                foreach (var sender in senders.Values)
                {
                    sender.Stop();
                }
                senders.Clear();
            }

            client.RemoveConnection(this);

            /*
            keepGoing = false;
            lock (condVar)
            {
                Monitor.Pulse(condVar);
            }
            task.Wait();
            */
            queue.Stop();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);

            try
            {
                string data;
                if (e.IsBinary)
                {
                    data = BuiltIns.UTF8.GetString(e.RawData);
                }
                else
                {
                    data = e.Data;
                }

                Subscription subscription;
                Advertisement advertisement;

                GenericMessage msg = JsonSerializer.Deserialize<GenericMessage>(data);
                switch (msg.Op)
                {
                    case "publish":
                        if (!TryGet(advertisements, msg.Topic, out advertisement) ||
                            advertisement.publisher.NumSubscribers == 0)
                        {
                            break;
                        }
                        IMessage outMsg = advertisement.Deserialize(data);
                        advertisement.publisher.Publish(outMsg);
                        break;
                    case "subscribe":
                        if (!TryGet(subscriptions, msg.Topic, out subscription))
                        {
                            client.Types.TryGetType(msg.Type, out TypeInfo typeInfo);
                            string subscriptionId = client.RosClient.Subscribe(
                                msg.Topic,
                                x => MessageCallback(msg.Topic, x),
                                typeInfo.msgType,
                                out RosSubscriber subscriber,
                                true);
                            subscription = new Subscription(
                                msg.Topic, typeInfo, subscriber, subscriptionId);
                            Add(subscriptions, msg.Topic, subscription);
                        }
                        subscription.AddId(msg.Id);
                        break;
                    case "unsubscribe":
                        if (TryGet(subscriptions, msg.Topic, out subscription))
                        {
                            subscription.RemoveId(msg.Id);
                            if (subscription.Empty())
                            {
                                subscription.Close();
                                Remove(subscriptions, msg.Topic);
                            }
                        }
                        break;
                    case "advertise":
                        if (!TryGet(advertisements, msg.Topic, out advertisement))
                        {
                            client.Types.TryGetType(msg.Type, out TypeInfo typeInfo);
                            string advertisementId = client.RosClient.Advertise(
                                msg.Topic,
                                typeInfo.msgType,
                                out RosPublisher publisher
                                );
                            advertisement = new Advertisement(
                                msg.Topic, typeInfo, publisher, advertisementId);
                            Add(advertisements, msg.Topic, advertisement);
                        }
                        advertisement.AddId(msg.Id);
                        break;
                    case "unadvertise":
                        if (TryGet(advertisements, msg.Topic, out advertisement))
                        {
                            advertisement.RemoveId(msg.Id);
                            if (advertisement.Empty())
                            {
                                advertisement.Close();
                                Remove(advertisements, msg.Topic);
                            }
                        }
                        break;
                    case "iviz:advertise":
                        {
                            //Console.WriteLine("iviz:advertise");
                            if (!TryGet(senders, msg.Topic, out Sender sender))
                            {
                                client.Types.TryGetType(msg.Type, out TypeInfo typeInfo);

                                sender = Sender.Instantiate(typeInfo.msgType);
                                sender.Start(client.RosClient, msg.Topic, typeInfo);
                                Add(senders, msg.Topic, sender);
                            }
                            GenericResponse response = new GenericResponse()
                            {
                                Op = msg.Op,
                                Id = msg.Id,
                                Value = sender.Port.ToString()
                            };
                            Send(JsonSerializer.ToJsonString(response));
                            break;
                        }
                    case "iviz:subscribe":
                        {
                            //Console.WriteLine("iviz:subscribe");
                            if (!TryGet(listeners, msg.Topic, out Listener listener))
                            {
                                client.Types.TryGetType(msg.Type, out TypeInfo typeInfo);

                                listener = Listener.Instantiate(typeInfo.msgType);
                                listener.Start(client.RosClient, msg.Topic, typeInfo);
                                Add(listeners, msg.Topic, listener);
                            }
                            GenericResponse response = new GenericResponse()
                            {
                                Op = msg.Op,
                                Id = msg.Id,
                                Value = listener.Port.ToString()
                            };
                            Send(JsonSerializer.ToJsonString(response));
                            break;
                        }
                }
            }
            catch (Exception ee)
            {
                Console.Error.WriteLine(ee);
            }
        }

        public void MessageCallback(string topic, IMessage inMsg)
        {
            if (!TryGet(subscriptions, topic, out Subscription subscription))
            {
                return;
            }
            PublishMessage msg = subscription.GeneratePublishMessage();
            msg.op = "publish";
            msg.topic = topic;
            msg.SetMessage(inMsg);

            /*
            lock (condVar)
            {
                queue.Enqueue(msg);
                if (queue.Count > 3)
                {
                    queue.Dequeue();
                }
                Monitor.Pulse(condVar);
            }
            */
            queue.Enqueue(msg);
        }

        public void Cleanup()
        {
            lock (listeners)
            {
                foreach (var listener in listeners.Values)
                {
                    listener.Cleanup();
                }
                var empties = listeners.Where(x => !x.Value.IsAlive);
                if (empties.Any())
                {
                    var deads = empties.ToArray();
                    foreach (var dead in deads)
                    {
                        dead.Value.Stop();
                        listeners.Remove(dead.Key);
                    }
                }
            }
            lock (senders)
            {
                foreach (var sender in senders.Values)
                {
                    sender.Cleanup();
                }
                var empties = senders.Where(x => !x.Value.IsAlive);
                if (empties.Any())
                {
                    var deads = empties.ToArray();
                    foreach (var dead in deads)
                    {
                        dead.Value.Stop();
                        senders.Remove(dead.Key);
                    }
                }
            }
        }

        /*
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
                Console.Error.WriteLine(e);
            }
        }
        */
        bool Process(PublishMessage msg)
        {
            Send(msg.Serialize());
            return true;
        }


        static bool TryGet<T>(Dictionary<string, T> dict, string topic, out T t)
        {
            lock (dict)
            {
                return dict.TryGetValue(topic, out t);
            }
        }

        static void Add<T>(Dictionary<string, T> dict, string topic, T t)
        {
            lock (dict)
            {
                dict.Add(topic, t);
            }
        }

        static bool Remove<T>(Dictionary<string, T> dict, string topic)
        {
            lock (dict)
            {
                return dict.Remove(topic);
            }
        }
    }
}
