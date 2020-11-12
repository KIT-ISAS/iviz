#define LOG_ENABLED

using Iviz.Msgs;
using Iviz.Roslib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
{
    internal sealed class RoslibConnection : RosConnection
    {
        [CanBeNull] RosClient client;

        interface IAdvertisedTopic
        {
            [CanBeNull] IRosPublisher Publisher { get; }
            int Id { get; set; }
            void Add([NotNull] ISender subscriber);
            void Remove([NotNull] ISender subscriber);
            int Count { get; }
            Task AdvertiseAsync([CanBeNull] RosClient client);
            Task UnadvertiseAsync([NotNull] RosClient client);
            void Invalidate();
        }

        class AdvertisedTopic<T> : IAdvertisedTopic where T : IMessage
        {
            int id;
            [NotNull] readonly string topic;

            readonly HashSet<Sender<T>> senders = new HashSet<Sender<T>>();

            public IRosPublisher Publisher { get; private set; }

            public AdvertisedTopic([NotNull] string topic)
            {
                this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
            }

            public int Id
            {
                get => id;
                set
                {
                    id = value;
                    foreach (Sender<T> sender in senders)
                    {
                        sender.SetId(value);
                    }
                }
            }

            public void Add(ISender publisher)
            {
                senders.Add((Sender<T>) publisher);
            }

            public void Remove(ISender publisher)
            {
                senders.Remove((Sender<T>) publisher);
            }

            public int Count => senders.Count;

            public async Task AdvertiseAsync(RosClient client)
            {
                string fullTopic = (topic[0] == '/') ? topic : $"{client?.CallerId}/{topic}";
                IRosPublisher publisher;
                if (client != null)
                {
                    (_, publisher) = await client.AdvertiseAsync<T>(fullTopic);
                }
                else
                {
                    publisher = null;
                }

                Publisher = publisher;
            }

            public async Task UnadvertiseAsync(RosClient client)
            {
                if (client == null)
                {
                    throw new ArgumentNullException(nameof(client));
                }

                string fullTopic = (topic[0] == '/') ? topic : $"{client.CallerId}/{topic}";
                if (Publisher != null)
                {
                    await Publisher.UnadvertiseAsync(fullTopic);
                }
            }

            public void Invalidate()
            {
                Id = -1;
                Publisher = null;
            }
        }

        interface ISubscribedTopic
        {
            [CanBeNull] IRosSubscriber Subscriber { get; }
            int Count { get; }
            void Add([NotNull] IListener subscriber);
            void Remove([NotNull] IListener subscriber);
            Task SubscribeAsync([CanBeNull] RosClient client, [CanBeNull] IListener listener = null);
            Task UnsubscribeAsync([NotNull] RosClient client);
            void Invalidate();
        }

        class SubscribedTopic<T> : ISubscribedTopic where T : IMessage, IDeserializable<T>, new()
        {
            [NotNull] readonly string topic;

            public IRosSubscriber Subscriber { get; private set; }

            readonly HashSet<Listener<T>> listeners = new HashSet<Listener<T>>();

            public SubscribedTopic([NotNull] string topic)
            {
                this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
            }

            public void Add(IListener subscriber)
            {
                listeners.Add((Listener<T>) subscriber);
            }

            public void Remove(IListener subscriber)
            {
                listeners.Remove((Listener<T>) subscriber);
            }

            void Callback(T msg)
            {
                foreach (Listener<T> listener in listeners)
                {
                    listener.EnqueueMessage(msg);
                }
            }

            public async Task SubscribeAsync(RosClient client, IListener listener)
            {
                string fullTopic = (topic[0] == '/') ? topic : $"{client?.CallerId}/{topic}";
                IRosSubscriber subscriber;
                if (listener != null)
                {
                    listeners.Add((Listener<T>) listener);
                }

                if (client != null)
                {
                    (_, subscriber) = await client.SubscribeAsync<T>(fullTopic, Callback);
                }
                else
                {
                    subscriber = null;
                }

                Subscriber = subscriber;
            }

            public async Task UnsubscribeAsync(RosClient client)
            {
                string fullTopic = topic[0] == '/' ? topic : $"{client.CallerId}/{topic}";

                if (Subscriber != null)
                {
                    await Subscriber.UnsubscribeAsync(fullTopic);
                }
            }

            public int Count => listeners.Count;

            public void Invalidate()
            {
                Subscriber = null;
            }
        }

        interface IAdvertisedService
        {
            Task AdvertiseAsync([CanBeNull] RosClient client);
        }

        class AdvertisedService<T> : IAdvertisedService where T : IService, new()
        {
            [NotNull] readonly string service;
            [NotNull] readonly Func<T, Task> callback;

            public AdvertisedService([NotNull] string service, [NotNull] Action<T> callback)
            {
                this.service = service ?? throw new ArgumentNullException(nameof(service));
                if (callback == null)
                {
                    throw new ArgumentNullException(nameof(callback));
                }

                this.callback = async t =>
                {
                    callback(t);
                    await Task.CompletedTask;
                };
            }

            public AdvertisedService([NotNull] string service, [NotNull] Func<T, Task> callback)
            {
                this.service = service ?? throw new ArgumentNullException(nameof(service));
                this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            }

            public async Task AdvertiseAsync(RosClient client)
            {
                string fullService = (service[0] == '/') ? service : $"{client?.CallerId}/{service}";
                if (client != null)
                {
                    await client.AdvertiseServiceAsync(fullService, callback);
                }
            }
        }

        readonly Dictionary<string, IAdvertisedTopic> publishersByTopic = new Dictionary<string, IAdvertisedTopic>();
        readonly Dictionary<string, ISubscribedTopic> subscribersByTopic = new Dictionary<string, ISubscribedTopic>();
        readonly Dictionary<string, IAdvertisedService> servicesByTopic = new Dictionary<string, IAdvertisedService>();
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

        async Task DisposeClient()
        {
            if (client == null)
            {
                return;
            }

            try
            {
                await client.CloseAsync();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            client = null;
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
                await DisposeClient();
            }

            try
            {
#if LOG_ENABLED
                Msgs.Logger.LogDebug = Logger.Debug;
                Msgs.Logger.LogError = Logger.Error;
                Msgs.Logger.Log = Logger.Info;
#endif


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
            (e is UnreachableUriException ||
             e is ConnectionException ||
             e is RosRpcException ||
             e is XmlRpcException)
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

            await DisposeClient();
            return false;
        }

        async Task Readvertise([NotNull] IAdvertisedTopic topic)
        {
            await topic.AdvertiseAsync(client);
            topic.Id = publishers.Count;
            publishers.Add(topic.Publisher);
        }

        async Task Resubscribe([NotNull] ISubscribedTopic topic)
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
                    await DisposeClient();
                    Logger.Internal("<b>Disconnected.</b>");

                    foreach (var entry in publishersByTopic.Values)
                    {
                        entry.Invalidate();
                    }

                    foreach (var entry in subscribersByTopic.Values)
                    {
                        entry.Invalidate();
                    }

                    publishers.Clear();
                    base.Disconnect();
                }
            );
        }

        internal override void Advertise<T>(Sender<T> advertiser)
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

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

        async Task AdvertiseImpl<T>([NotNull] Sender<T> advertiser) where T : IMessage
        {
            if (!publishersByTopic.TryGetValue(advertiser.Topic, out IAdvertisedTopic advertisedTopic))
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
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

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

        async Task AdvertiseServiceImpl<T>([NotNull] string service, [NotNull] Action<T> callback)
            where T : IService, new()
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
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (srv == null)
            {
                throw new ArgumentNullException(nameof(srv));
            }

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

        internal override void Publish<T>(Sender<T> advertiser, T msg)
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

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


        void PublishImpl<T>([NotNull] ISender advertiser, T msg) where T : IMessage
        {
            if (advertiser.Id == -1)
            {
                return;
            }

            IRosPublisher basePublisher = publishers[advertiser.Id];
            if (basePublisher != null && basePublisher is IRosPublisher<T> publisher)
            {
                publisher.Publish(msg);
            }
        }

        internal override void Subscribe<T>(Listener<T> listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

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

        async Task SubscribeImpl<T>([NotNull] IListener listener) where T : IMessage, IDeserializable<T>, new()
        {
            if (!subscribersByTopic.TryGetValue(listener.Topic, out ISubscribedTopic subscribedTopic))
            {
                SubscribedTopic<T> newSubscribedTopic = new SubscribedTopic<T>(listener.Topic);

                await newSubscribedTopic.SubscribeAsync(client, listener);

                subscribedTopic = newSubscribedTopic;
                subscribersByTopic.Add(listener.Topic, subscribedTopic);
                return;
            }

            subscribedTopic.Add(listener);
        }

        internal override void Unadvertise(ISender advertiser)
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

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

        async Task UnadvertiseImpl([NotNull] ISender advertiser)
        {
            if (!publishersByTopic.TryGetValue(advertiser.Topic, out IAdvertisedTopic advertisedTopic))
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

        internal override void Unsubscribe(IListener subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

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


        async Task UnsubscribeImpl([NotNull] IListener subscriber)
        {
            if (!subscribersByTopic.TryGetValue(subscriber.Topic, out ISubscribedTopic subscribedTopic))
            {
                return;
            }

            subscribedTopic.Remove(subscriber);
            if (subscribedTopic.Count == 0)
            {
                subscribersByTopic.Remove(subscriber.Topic);
                if (client != null)
                {
                    await subscribedTopic.UnsubscribeAsync(client);
                }
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
                    signal?.Release();
                }
            });

            if (type == RequestType.WaitForRequest)
            {
                signal?.Wait();
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
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

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
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            subscribersByTopic.TryGetValue(topic, out ISubscribedTopic subscribedTopic);
            return subscribedTopic?.Subscriber?.NumPublishers ?? 0;
        }

        public override int GetNumSubscribers(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            publishersByTopic.TryGetValue(topic, out IAdvertisedTopic advertisedTopic);
            return advertisedTopic?.Publisher?.NumSubscribers ?? 0;
        }

        internal override void Stop()
        {
            Disconnect();
            base.Stop();
        }
    }
}