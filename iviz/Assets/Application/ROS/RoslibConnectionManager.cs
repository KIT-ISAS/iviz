﻿using Iviz.Msgs;
using Iviz.Roslib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using Iviz.Displays;
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

            protected AdvertisedTopic(string topic)
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

            public AdvertisedTopic(string topic) : base(topic)
            {
            }

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
                senders.Add((RosSender<T>) publisher);
            }

            public override void Remove(RosSender publisher)
            {
                senders.Remove((RosSender<T>) publisher);
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

            protected SubscribedTopic(string topic)
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

            public SubscribedTopic(string topic) : base(topic)
            {
            }

            public override void Add(RosListener subscriber)
            {
                listeners.Add((RosListener<T>) subscriber);
            }

            public override void Remove(RosListener subscriber)
            {
                listeners.Remove((RosListener<T>) subscriber);
            }

            void Callback(T msg)
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
            protected string Service { get; }

            protected AdvertisedService(string service)
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
            if (MasterUri == null ||
                MasterUri.Scheme != "http" ||
                MyId == null ||
                MyUri == null ||
                MyUri.Scheme != "http")
            {
                return false;
            }

            if (client != null)
            {
                Debug.LogWarning("Warning: New client requested, but old client still running!");
                client.Close();
                client = null;
            }

            try
            {
                Msgs.Logger.LogDebug = x => Logger.Debug(x);
                Msgs.Logger.LogError = x => Logger.Error(x);
                Msgs.Logger.Log = x => Logger.Info(x);

                Logger.Internal("Connecting...");
                client = new RosClient(MasterUri, MyId, MyUri);

                if (publishersByTopic.Count != 0 || subscribersByTopic.Count != 0)
                {
                    Logger.Internal("Resubscribing and republishing...");
                }

                foreach (var entry in publishersByTopic)
                {
                    entry.Value.Advertise(client);
                    entry.Value.Id = publishers.Count;
                    publishers.Add(entry.Value.Publisher);
                }

                foreach (var entry in subscribersByTopic)
                {
                    entry.Value.Subscribe(client);
                }

                foreach (var entry in servicesByTopic)
                {
                    entry.Value.Advertise(client);
                }

                Logger.Internal("Connected.");

                return true;
            }
            catch (Exception e) when
                (e is UnreachableUriException || e is ConnectionException || e is XmlRpcException)
            {
                Logger.Debug(e);
                Logger.Internal("Error:", e);
            }
            catch (Exception e)
            {
                Logger.Warn(e);
            }

            client?.Close();
            client = null;
            return false;
        }

        public override void Disconnect()
        {
            Disconnect(true);
        }

        void Disconnect(bool waitForUnregister)
        {
            if (client == null)
            {
                return;
            }

            AddTask(() =>
                {
                    Logger.Internal("Disconnecting...");
                    client?.Close(waitForUnregister);
                    client = null;
                    Logger.Internal("Disconnection finished.");

                    foreach (var entry in publishersByTopic)
                    {
                        entry.Value.Invalidate();
                    }

                    foreach (var entry in subscribersByTopic)
                    {
                        entry.Value.Invalidate();
                    }

                    publishers.Clear();
                    base.Disconnect();
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
                AdvertisedTopic<T> newAdvertisedTopic = new AdvertisedTopic<T>(advertiser.Topic);

                int id;
                if (client != null)
                {
                    newAdvertisedTopic.Advertise(client);

                    var publisher = newAdvertisedTopic.Publisher;
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
            if (servicesByTopic.ContainsKey(service))
            {
                return;
            }

            AdvertisedService<T> newAdvertisedService = new AdvertisedService<T>(service, callback);

            if (client != null)
            {
                newAdvertisedService.Advertise(client);
            }

            servicesByTopic.Add(service, newAdvertisedService);
        }

        public override void CallServiceAsync<T>(string service, T srv, Action<T> callback)
        {
            AddTask(() =>
            {
                try
                {
                    if (client != null && client.CallService(service, srv))
                    {
                        GameThread.RunOnce(() => callback(srv));
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                }
            });
        }

        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        public override bool CallService<T>(string service, T srv)
        {
            bool[] result = {false};
            
            AddTask(() =>
            {
                try
                {
                    result[0] = client != null && client.CallService(service, srv);
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                }

                signal.Release();
            });

            signal.Wait();
            return result[0];
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
            if (!publishersByTopic.TryGetValue(advertiser.Topic, out AdvertisedTopic advertisedTopic))
            {
                return;
            }

            advertisedTopic.Remove(advertiser);
            if (advertisedTopic.Count != 0)
            {
                return;
            }

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
            if (!subscribersByTopic.TryGetValue(subscriber.Topic, out SubscribedTopic subscribedTopic))
            {
                return;
            }

            subscribedTopic.Remove(subscriber);
            if (subscribedTopic.Count == 0)
            {
                subscribersByTopic.Remove(subscriber.Topic);
                subscribedTopic.Unsubscribe(client);
            }
        }

        ReadOnlyCollection<BriefTopicInfo> cachedTopics = EmptyTopics;

        public override ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics()
        {
            AddTask(() =>
            {
                try
                {
                    if (client is null)
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

        static readonly ReadOnlyCollection<string> EmptyParameters = Array.Empty<string>().AsReadOnly();

        ReadOnlyCollection<string> cachedParameters = EmptyParameters;

        public override ReadOnlyCollection<string> GetSystemParameterList()
        {
            AddTask(async() =>
            {
                try
                {
                    if (client?.Parameters is null)
                    {
                        cachedParameters = EmptyParameters;
                        return;
                    }

                    cachedParameters = await client.Parameters.GetParameterNamesAsync();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            });

            return cachedParameters;
        }

        public override object GetParameter(string parameter)
        {
            object[] result = {null};
            
            AddTask(async() =>
            {
                try
                {
                    if (client?.Parameters != null)
                    {
                        var tuple = await client.Parameters.GetParameterAsync(parameter);
                        result[0] = tuple.value;
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                }

                signal.Release();
            });

            signal.Wait();
            return result[0];
            
        }

        protected override void Update()
        {
            // TODO: get rid of RosClient.Cleanup()! 
            if (client != null)
            {
                AddTask(client.Cleanup);
            }
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
            Disconnect(false); // do not wait for topic, services unregistering
            base.Stop();
        }
    }
}