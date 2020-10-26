using Iviz.Msgs;
using Iviz.Roslib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.XmlRpc;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class RoslibConnection : RosConnection
    {
        RosClient client;

        abstract class AdvertisedTopic
        {
            public string Topic { get; }
            public IRosPublisher Publisher { get; protected set; }
            public virtual int Id { get; set; }

            protected AdvertisedTopic(string topic)
            {
                Topic = topic;
            }

            public abstract void Add(RosSender subscriber);
            public abstract void Remove(RosSender subscriber);
            public abstract int Count { get; }
            public abstract Task AdvertiseAsync(RosClient client);

            public async Task UnadvertiseAsync(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                if (Publisher != null)
                {
                    await Publisher.UnadvertiseAsync(topic);
                }
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

            public override async Task AdvertiseAsync(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                IRosPublisher publisher;
                if (client != null)
                {
                    (_, publisher) = await client.AdvertiseAsync<T>(topic);
                }
                else
                {
                    publisher = null;
                }

                Publisher = publisher;
            }
        }

        abstract class SubscribedTopic
        {
            public string Topic { get; }
            public IRosSubscriber Subscriber { get; protected set; }
            public abstract int Count { get; }

            protected SubscribedTopic(string topic)
            {
                Topic = topic;
            }

            public abstract void Add(RosListener subscriber);
            public abstract void Remove(RosListener subscriber);
            public abstract Task SubscribeAsync(RosClient client);

            public async Task UnsubscribeAsync(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                if (Subscriber != null)
                {
                    await Subscriber.UnsubscribeAsync(topic);
                }
            }

            public void Invalidate()
            {
                Subscriber = null;
            }
        }

        class SubscribedTopic<T> : SubscribedTopic where T : IMessage, IDeserializable<T>, new()
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

            public override async Task SubscribeAsync(RosClient client)
            {
                string topic = (Topic[0] == '/') ? Topic : $"{client.CallerId}/{Topic}";
                IRosSubscriber subscriber ;
                if (client != null)
                {
                    (_, subscriber) = await client.SubscribeAsync<T>(topic, Callback);
                }
                else
                {
                    subscriber = null;
                }

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

            public abstract Task AdvertiseAsync(RosClient client);
        }

        class AdvertisedService<T> : AdvertisedService where T : IService, new()
        {
            readonly Func<T, Task> callback;

            public AdvertisedService(string service, Action<T> callback) : base(service)
            {
                this.callback = async t => await Task.Run(() => callback(t));
            }

            public AdvertisedService(string service, Func<T, Task> callback) : base(service)
            {
                this.callback = callback;
            }

            public override async Task AdvertiseAsync(RosClient client)
            {
                string service = (Service[0] == '/') ? Service : $"{client.CallerId}/{Service}";
                if (client != null)
                {
                    await client.AdvertiseServiceAsync(service, callback);
                }
            }
        }

        readonly Dictionary<string, AdvertisedTopic> publishersByTopic = new Dictionary<string, AdvertisedTopic>();
        readonly Dictionary<string, SubscribedTopic> subscribersByTopic = new Dictionary<string, SubscribedTopic>();
        readonly Dictionary<string, AdvertisedService> servicesByTopic = new Dictionary<string, AdvertisedService>();
        readonly List<IRosPublisher> publishers = new List<IRosPublisher>();

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

        protected override async Task<bool> Connect()
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
                await client.CloseAsync();
                client = null;
            }

            try
            {
                //Msgs.Logger.LogDebug = x => Logger.Debug(x);
                //Msgs.Logger.LogError = x => Logger.Error(x);
                //Msgs.Logger.Log = x => Logger.Info(x);

                Logger.Internal("Connecting...");

                client = new RosClient(MasterUri, MyId, MyUri, false);

                await client.EnsureCleanSlateAsync();

                if (publishersByTopic.Count != 0 || subscribersByTopic.Count != 0)
                {
                    Logger.Internal("Resubscribing and republishing...");
                }

                await Task.WhenAll(publishersByTopic.Values.Select(Readvertise));
                await Task.WhenAll(subscribersByTopic.Values.Select(Resubscribe));

                foreach (var entry in servicesByTopic.Values)
                {
                    await entry.AdvertiseAsync(client);
                }

                Logger.Internal("<b>Connected.</b>");

                return true;
            }
            catch (Exception e) when
                (e is UnreachableUriException || e is ConnectionException || e is RosRpcException || e is XmlRpcException)
            {
                Logger.Internal("Error:", e);
                if (RosServerManager.IsActive && RosServerManager.MasterUri == MasterUri)
                {
                    Logger.Internal(
                        "Note: This appears to be my own master. Are you sure the uri network is reachable?");
                }
            }
            catch (Exception e)
            {
                Logger.Warn(e);
            }

            if (client != null)
            {
                await client.CloseAsync();
            }

            client = null;
            return false;
        }

        async Task Readvertise(AdvertisedTopic topic)
        {
            await topic.AdvertiseAsync(client);
            topic.Id = publishers.Count;
            publishers.Add(topic.Publisher);
        }

        async Task Resubscribe(SubscribedTopic topic)
        {
            await topic.SubscribeAsync(client);
        }

        public override void Disconnect()
        {
            if (client == null)
            {
                Signal();
                return;
            }

            AddTask(async () =>
                {
                    Logger.Internal("Disconnecting...");
                    if (client != null)
                    {
                        try
                        {
                            await client.CloseAsync();
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }
                    }

                    client = null;
                    Logger.Internal("<b>Disconnected.</b>");

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
            AddTask(async () =>
            {
                try
                {
                    await AdvertiseImpl(advertiser);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        async Task AdvertiseImpl<T>(RosSender<T> advertiser) where T : IMessage
        {
            if (!publishersByTopic.TryGetValue(advertiser.Topic, out AdvertisedTopic advertisedTopic))
            {
                AdvertisedTopic<T> newAdvertisedTopic = new AdvertisedTopic<T>(advertiser.Topic);

                int id;
                if (client != null)
                {
                    await newAdvertisedTopic.AdvertiseAsync(client);

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
            AddTask(async () =>
            {
                try
                {
                    await AdvertiseServiceImpl(service, callback);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        async Task AdvertiseServiceImpl<T>(string service, Action<T> callback) where T : IService, new()
        {
            if (servicesByTopic.ContainsKey(service))
            {
                return;
            }

            Logger.Internal($"Advertising service <b>{service}</b> <i>[{BuiltIns.GetServiceType(typeof(T))}]</i>.");

            AdvertisedService<T> newAdvertisedService = new AdvertisedService<T>(service, callback);

            if (client != null)
            {
                await newAdvertisedService.AdvertiseAsync(client);
            }

            servicesByTopic.Add(service, newAdvertisedService);
        }

        public override bool CallService<T>(string service, T srv)
        {
            SemaphoreSlim signal = new SemaphoreSlim(0, 1);
            bool[] result = {false};

            AddTask(async () =>
            {
                try
                {
                    result[0] = client != null && await client.CallServiceAsync(service, srv);
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
            AddTask(async () =>
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

                await Task.CompletedTask;
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
            AddTask(async () =>
            {
                try
                {
                    await SubscribeImpl<T>(listener);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        async Task SubscribeImpl<T>(RosListener listener) where T : IMessage, IDeserializable<T>, new()
        {
            if (!subscribersByTopic.TryGetValue(listener.Topic, out SubscribedTopic subscribedTopic))
            {
                SubscribedTopic<T> newSubscribedTopic = new SubscribedTopic<T>(listener.Topic);

                await newSubscribedTopic.SubscribeAsync(client);

                subscribedTopic = newSubscribedTopic;
                subscribersByTopic.Add(listener.Topic, subscribedTopic);
            }

            subscribedTopic.Add(listener);
        }

        public override void Unadvertise(RosSender advertiser)
        {
            AddTask(async () =>
            {
                try
                {
                    await UnadvertiseImpl(advertiser);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }

        async Task UnadvertiseImpl(RosSender advertiser)
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
                await advertisedTopic.UnadvertiseAsync(client);
                PublishedTopics = client.PublishedTopics;
            }
        }

        public override void Unsubscribe(RosListener subscriber)
        {
            AddTask(async () =>
            {
                try
                {
                    await UnsubscribeImpl(subscriber);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Disconnect();
                }
            });
        }


        async Task UnsubscribeImpl(RosListener subscriber)
        {
            if (!subscribersByTopic.TryGetValue(subscriber.Topic, out SubscribedTopic subscribedTopic))
            {
                return;
            }

            subscribedTopic.Remove(subscriber);
            if (subscribedTopic.Count == 0)
            {
                subscribersByTopic.Remove(subscriber.Topic);
                await subscribedTopic.UnsubscribeAsync(client);
            }
        }

        ReadOnlyCollection<BriefTopicInfo> cachedTopics = EmptyTopics;

        public override ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics(
            RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedOnly)
            {
                return cachedTopics;
            }

            SemaphoreSlim signal = null;
            if (type == RequestType.WaitForRequest)
            {
                signal = new SemaphoreSlim(0, 1);
            }

            AddTask(async () =>
            {
                try
                {
                    if (client is null)
                    {
                        cachedTopics = EmptyTopics;
                        return;
                    }

                    cachedTopics = await client.GetSystemPublishedTopicsAsync();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }

                if (type == RequestType.WaitForRequest)
                {
                    signal.Release();
                }
            });

            if (type == RequestType.WaitForRequest)
            {
                signal.Wait();
            }

            return cachedTopics;
        }

        static readonly ReadOnlyCollection<string> EmptyParameters = Array.Empty<string>().AsReadOnly();

        ReadOnlyCollection<string> cachedParameters = EmptyParameters;

        public override ReadOnlyCollection<string> GetSystemParameterList()
        {
            AddTask(async () =>
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
            SemaphoreSlim signal = new SemaphoreSlim(0, 1);
            object[] result = {null};

            AddTask(async () =>
            {
                try
                {
                    if (client?.Parameters != null)
                    {
                        (_, result[0]) = await client.Parameters.GetParameterAsync(parameter);
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
            Disconnect();
            base.Stop();
        }
    }
}