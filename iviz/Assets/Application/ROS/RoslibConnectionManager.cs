using Iviz.Msgs;
using Iviz.RoslibSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class RoslibConnection : RosConnection
    {
        RosClient client;

        abstract class AdvertisedTopic
        {
            public string Topic { get; }
            public RosPublisher Publisher { get; protected set; }
            public virtual int Id { get; set; }

            public AdvertisedTopic(string topic)
            {
                Topic = topic;
            }

            public abstract void Add(RosSender subscriber);
            public abstract void Remove(RosSender subscriber);
            public abstract int Count { get; }
            public abstract void Advertise(RosClient client);

            public void Unadvertise(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                Publisher?.Unadvertise(topic);
            }

            public void Invalidate()
            {
                Id = -1;
                Publisher = null;
            }
        }

        class AdvertisedTopic<T> : AdvertisedTopic where T : IMessage
        {
            readonly HashSet<RosSender<T>> senders = new HashSet<RosSender<T>>();

            public AdvertisedTopic(string topic) : base(topic) { }

            public override int Id
            {
                get => base.Id;
                set
                {
                    base.Id = value;
                    foreach (RosSender<T> sender in senders)
                    {
                        sender.SetId(value);
                    }
                }
            }

            public override void Add(RosSender publisher)
            {
                senders.Add((RosSender<T>)publisher);
            }

            public override void Remove(RosSender publisher)
            {
                senders.Remove((RosSender<T>)publisher);
            }

            public override int Count => senders.Count;

            public override void Advertise(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                RosPublisher publisher = null;
                client?.Advertise<T>(topic, out publisher);
                Publisher = publisher;
            }
        }

        abstract class SubscribedTopic
        {
            public string Topic { get; }
            public RosSubscriber Subscriber { get; protected set; }
            public abstract int Count { get; }

            public SubscribedTopic(string topic)
            {
                Topic = topic;
            }

            public abstract void Add(RosListener subscriber);
            public abstract void Remove(RosListener subscriber);
            public abstract void Subscribe(RosClient client);

            public void Unsubscribe(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                Subscriber?.Unsubscribe(topic);
            }

            public void Invalidate()
            {
                Subscriber = null;
            }
        }

        class SubscribedTopic<T> : SubscribedTopic where T : IMessage, new()
        {
            readonly HashSet<RosListener<T>> listeners = new HashSet<RosListener<T>>();

            public SubscribedTopic(string topic) : base(topic) { }

            public override void Add(RosListener subscriber)
            {
                listeners.Add((RosListener<T>)subscriber);
            }

            public override void Remove(RosListener subscriber)
            {
                listeners.Remove((RosListener<T>)subscriber);
            }

            public void Callback(T msg)
            {
                foreach (RosListener<T> listener in listeners)
                {
                    listener.EnqueueMessage(msg);
                }
            }

            public override void Subscribe(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                RosSubscriber subscriber = null;
                client?.Subscribe<T>(topic, Callback, out subscriber);
                Subscriber = subscriber;
            }

            public override int Count => listeners.Count;

        }

        abstract class AdvertisedService
        {
            public string Service { get; }

            public AdvertisedService(string service)
            {
                Service = service;
            }

            public abstract void Advertise(RosClient client);
        }

        class AdvertisedService<T> : AdvertisedService where T : IService, new()
        {
            readonly Action<T> callback;

            public AdvertisedService(string service, Action<T> callback) : base(service)
            {
                this.callback = callback;
            }

            public override void Advertise(RosClient client)
            {
                string service = (Service[0] == '/') ? Service : $"{client.CallerId}/{Service}";
                client?.AdvertiseService(service, callback);
            }
        }

        readonly Dictionary<string, AdvertisedTopic> publishersByTopic = new Dictionary<string, AdvertisedTopic>();
        readonly Dictionary<string, SubscribedTopic> subscribersByTopic = new Dictionary<string, SubscribedTopic>();
        readonly Dictionary<string, AdvertisedService> servicesByTopic = new Dictionary<string, AdvertisedService>();
        readonly List<RosPublisher> publishers = new List<RosPublisher>();

        public override Uri MasterUri
        {
            get => base.MasterUri;
            set
            {
                base.MasterUri = value;
                Disconnect();
            }
        }

        //public override string Id => "/Iviz";
        public override string MyId
        {
            get => base.MyId;
            set
            {
                base.MyId = value;
                Disconnect();
            }
        }

        public override Uri MyUri
        {
            get => base.MyUri;
            set
            {
                base.MyUri = value;
                Disconnect();
            }
        }

        protected override bool Connect()
        {
            //Debug.Log("Connecting! '" + Uri + "'");
            if (MasterUri == null ||
                MasterUri.Scheme != "http" ||
                MyId == null ||
                MyUri == null ||
                MyUri.Scheme != "http")
            {
                return false;
            }

            //Debug.Log("Valid");

            try
            {

                //RoslibSharp.Logger.LogDebug = x => Logger.Debug(x);
                RoslibSharp.Logger.LogError = x => Logger.Error(x);
                RoslibSharp.Logger.Log = x => Logger.Info(x);

                client = new RosClient(MasterUri, MyId, MyUri);

                foreach (var entry in publishersByTopic)
                {
                    //Logger.Debug("Late advertisement for " + entry.Key);
                    entry.Value.Advertise(client);
                    entry.Value.Id = publishers.Count;
                    publishers.Add(entry.Value.Publisher);
                }
                foreach (var entry in subscribersByTopic)
                {
                    //Logger.Debug("Late subscription for " + entry.Key);
                    entry.Value.Subscribe(client);
                }
                foreach (var entry in servicesByTopic)
                {
                    //Logger.Debug("Late subscription for " + entry.Key);
                    entry.Value.Advertise(client);
                }

                return true;
            }
            catch (Exception e) when
            (e is UnreachableUriException || e is ConnectionException || e is XmlRpcException)
            {
                Logger.Debug(e);
                Logger.Internal("Error:", e);
                client = null;
                return false;
            }
            catch (Exception e) when
            (e is IOException)
            {
                Logger.Debug(e);
                Logger.Internal("Error:", e);
                client.Close();
                client = null;
                return false;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                client?.Close();
                client = null;
                return false;
            }
        }

        public override void Disconnect()
        {
            base.Disconnect();

            if (client == null)
            {
                return;
            }

            AddTask(() =>
            {
               Debug.Log("RosLibConnection: Disconnecting...");
               client?.Close();
               client = null;
               foreach (var entry in publishersByTopic)
               {
                   entry.Value.Invalidate();
               }
               foreach (var entry in subscribersByTopic)
               {
                   entry.Value.Invalidate();
               }
               publishers.Clear();
               Debug.Log("RosLibConnection: Disconnection finished.");
            }
            );
        }

        public override void Advertise<T>(RosSender<T> advertiser)
        {
            AddTask(() =>
            {
                try
                {
                    AdvertiseImpl(advertiser);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        void AdvertiseImpl<T>(RosSender<T> advertiser) where T : IMessage
        {
            if (!publishersByTopic.TryGetValue(advertiser.Topic, out AdvertisedTopic advertisedTopic))
            {
                RosPublisher publisher = null;
                AdvertisedTopic<T> newAdvertisedTopic = new AdvertisedTopic<T>(advertiser.Topic);

                int id;
                if (client != null)
                {
                    newAdvertisedTopic.Advertise(client);

                    publisher = newAdvertisedTopic.Publisher;
                    //Logger.Debug("Direct advertisement for " + advertiser.Topic);

                    //client?.Advertise<T>(advertiser.Topic, out publisher);
                    id = publishers.FindIndex(x => x is null);
                    if (id == -1)
                    {
                        id = publishers.Count;
                        publishers.Add(publisher);
                        //Logger.Debug("Id is " + id);
                    }
                    else
                    {
                        publishers[id] = publisher;
                        //Logger.Debug("Id is " + id);
                    }
                    PublishedTopics = client.PublishedTopics;
                }
                else
                {
                    id = -1;
                }

                advertisedTopic = newAdvertisedTopic;
                advertisedTopic.Id = id;
                publishersByTopic.Add(advertiser.Topic, advertisedTopic);
            }
            advertisedTopic.Add(advertiser);
            advertiser.SetId(advertisedTopic.Id);
        }

        public override void AdvertiseService<T>(string service, Action<T> callback)
        {
            AddTask(() =>
            {
                try
                {
                    AdvertiseServiceImpl(service, callback);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        void AdvertiseServiceImpl<T>(string service, Action<T> callback) where T : IService, new()
        {
            if (!servicesByTopic.ContainsKey(service))
            {
                AdvertisedService<T> newAdvertisedService = new AdvertisedService<T>(service, callback);

                if (client != null)
                {
                    newAdvertisedService.Advertise(client);
                }
                servicesByTopic.Add(service, newAdvertisedService);
            }
        }


        public override void Publish(RosSender advertiser, IMessage msg)
        {
            AddTask(() =>
            {
                try
                {
                    PublishImpl(advertiser, msg);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }


        void PublishImpl(RosSender advertiser, IMessage msg)
        {
            if (advertiser.Id == -1)
            {
                return;
            }
            //Debug.Log("Trying to publish: " + advertiser.Id + " -> " + publishers[advertiser.Id]);
            publishers[advertiser.Id]?.Publish(msg);
        }

        public override void Subscribe<T>(RosListener<T> listener)
        {
            AddTask(() =>
            {
                try
                {
                    SubscribeImpl(listener);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        void SubscribeImpl<T>(RosListener<T> listener) where T : IMessage, new()
        {
            if (!subscribersByTopic.TryGetValue(listener.Topic, out SubscribedTopic subscribedTopic))
            {
                SubscribedTopic<T> newSubscribedTopic = new SubscribedTopic<T>(listener.Topic);

                newSubscribedTopic.Subscribe(client);
                //client?.Subscribe<T>(listener.Topic, newSubscribedTopic.Callback, out subscriber);

                subscribedTopic = newSubscribedTopic;
                subscribersByTopic.Add(listener.Topic, subscribedTopic);
            }
            subscribedTopic.Add(listener);
        }

        public override void Unadvertise(RosSender advertiser)
        {
            AddTask(() =>
            {
                try
                {
                    UnadvertiseImpl(advertiser);

                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        void UnadvertiseImpl(RosSender advertiser)
        {
            if (publishersByTopic.TryGetValue(advertiser.Topic, out AdvertisedTopic advertisedTopic))
            {
                advertisedTopic.Remove(advertiser);
                if (advertisedTopic.Count == 0)
                {
                    publishersByTopic.Remove(advertiser.Topic);
                    if (advertiser.Id != -1)
                    {
                        publishers[advertiser.Id] = null;
                    }

                    if (client != null)
                    {
                        advertisedTopic.Unadvertise(client);
                        PublishedTopics = client.PublishedTopics;
                    }
                }
            }
        }

        public override void Unsubscribe(RosListener subscriber)
        {
            AddTask(() =>
            {
                try
                {
                    UnsubscribeImpl(subscriber);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }


        void UnsubscribeImpl(RosListener subscriber)
        {
            if (subscribersByTopic.TryGetValue(subscriber.Topic, out SubscribedTopic subscribedTopic))
            {
                subscribedTopic.Remove(subscriber);
                if (subscribedTopic.Count == 0)
                {
                    subscribersByTopic.Remove(subscriber.Topic);
                    subscribedTopic.Unsubscribe(client);
                }
            }
        }

        ReadOnlyCollection<BriefTopicInfo> cachedTopics = EmptyTopics;

        public override ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics()
        {
            AddTask(() =>
            {
                try
                {
                    if (client == null)
                    {
                        cachedTopics = EmptyTopics;
                        return;
                    }
                    cachedTopics = client.GetSystemPublishedTopics();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            });

            return cachedTopics;
        }

        public override string GetParameter(string parameter)
        {
            if (client is null)
            {
                return null;
            }

            client.GetParameter(parameter, out object obj);
            return obj as string;
        }

        protected override void Update()
        {
            AddTask(() =>
            {
                client?.Cleanup();
            }
            );
        }

        public override int GetNumPublishers(string topic)
        {
            subscribersByTopic.TryGetValue(topic, out SubscribedTopic subscribedTopic);
            return subscribedTopic?.Subscriber?.NumPublishers ?? 0;
        }

        public override int GetNumSubscribers(string topic)
        {
            publishersByTopic.TryGetValue(topic, out AdvertisedTopic advertisedTopic);
            return advertisedTopic?.Publisher?.NumSubscribers ?? 0;
        }

        public override void Stop()
        {
            base.Stop();
            client?.Close();
            client = null;
        }
    }
}