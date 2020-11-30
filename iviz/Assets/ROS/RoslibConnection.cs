#define LOG_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Msgs.Logger;

namespace Iviz.Ros
{
    public sealed class RoslibConnection : RosConnection
    {
        static readonly ReadOnlyCollection<string> EmptyParameters = Array.Empty<string>().AsReadOnly();
        readonly List<IRosPublisher> publishers = new List<IRosPublisher>();
        readonly Dictionary<string, IAdvertisedTopic> publishersByTopic = new Dictionary<string, IAdvertisedTopic>();
        readonly Dictionary<string, IAdvertisedService> servicesByTopic = new Dictionary<string, IAdvertisedService>();
        readonly Dictionary<string, ISubscribedTopic> subscribersByTopic = new Dictionary<string, ISubscribedTopic>();

        [NotNull] ReadOnlyCollection<string> cachedParameters = EmptyParameters;
        [NotNull] ReadOnlyCollection<BriefTopicInfo> cachedTopics = EmptyTopics;
        [CanBeNull] RosClient client;

        Uri masterUri;
        string myId;
        Uri myUri;

        [CanBeNull]
        public Uri MasterUri
        {
            get => masterUri;
            set
            {
                masterUri = value;
                Disconnect();
            }
        }

        [CanBeNull]
        public string MyId
        {
            get => myId;
            set
            {
                myId = value;
                Disconnect();
            }
        }

        [CanBeNull]
        public Uri MyUri
        {
            get => myUri;
            set
            {
                myUri = value;
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
                Debug.LogWarning("Warning: New client requested, but old client still running?!");
                await DisposeClient();
            }

            try
            {
#if LOG_ENABLED
                Logger.LogDebug = Core.Logger.Debug;
                Logger.LogError = Core.Logger.Error;
                Logger.Log = Core.Logger.Info;
#endif
                Core.Logger.Internal("Connecting...");

                client = new RosClient(MasterUri, MyId, MyUri, false);

                await client.EnsureCleanSlateAsync();

                if (publishersByTopic.Count != 0 || subscribersByTopic.Count != 0)
                {
                    Core.Logger.Internal("Resubscribing and readvertising...");
                }

                await Task.WhenAll(publishersByTopic.Values.Select(Readvertise));
                await Task.WhenAll(subscribersByTopic.Values.Select(Resubscribe));

                foreach (var entry in servicesByTopic.Values)
                {
                    await entry.AdvertiseAsync(client);
                }

                Core.Logger.Internal("<b>Connected.</b>");

                return true;
            }
            catch (Exception e) when
            (e is UnreachableUriException ||
             e is ConnectionException ||
             e is RosRpcException ||
             e is XmlRpcException)
            {
                Core.Logger.Internal("Error:", e);
                if (RosServerManager.IsActive && RosServerManager.MasterUri == MasterUri)
                {
                    Core.Logger.Internal(
                        "Note: This appears to be my own master. Are you sure the uri network is reachable?");
                }
            }
            catch (Exception e)
            {
                Core.Logger.Warn(e);
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
                    Core.Logger.Internal("Disconnecting...");
                    await DisposeClient();
                    Core.Logger.Internal("<b>Disconnected.</b>");

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

        internal void Advertise<T>([NotNull] Sender<T> advertiser) where T : IMessage
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
                    Core.Logger.Error(e);
                    Disconnect();
                }
            });
        }

        async Task AdvertiseImpl<T>([NotNull] Sender<T> advertiser) where T : IMessage
        {
            if (publishersByTopic.TryGetValue(advertiser.Topic, out var advertisedTopic))
            {
                advertisedTopic.Add(advertiser);
                advertiser.SetId(advertisedTopic.Id);
                return;
            }

            var newAdvertisedTopic = new AdvertisedTopic<T>(advertiser.Topic);

            int id;
            if (client != null)
            {
                await newAdvertisedTopic.AdvertiseAsync(client);

                var publisher = newAdvertisedTopic.Publisher;

                id = publishers.FindIndex(x => x is null);
                if (id == -1)
                {
                    id = publishers.Count;
                    publishers.Add(publisher);
                }
                else
                {
                    publishers[id] = publisher;
                }

                PublishedTopics = client.PublishedTopics;
            }
            else
            {
                id = -1;
            }

            newAdvertisedTopic.Id = id;
            publishersByTopic.Add(advertiser.Topic, newAdvertisedTopic);
            newAdvertisedTopic.Add(advertiser);
            advertiser.SetId(newAdvertisedTopic.Id);
        }

        public void AdvertiseService<T>([NotNull] string service, [NotNull] Action<T> callback)
            where T : IService, new()
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
                    Core.Logger.Error(e);
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

            Core.Logger.Internal(
                $"Advertising service <b>{service}</b> <i>[{BuiltIns.GetServiceType(typeof(T))}]</i>.");

            var newAdvertisedService = new AdvertisedService<T>(service, callback);

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

            var signal = new SemaphoreSlim(0, 1);
            bool result = false;

            AddTask(async () =>
            {
                try
                {
                    result = client != null && await client.CallServiceAsync(service, srv);
                }
                catch (Exception e)
                {
                    Core.Logger.Debug(e);
                }

                signal.Release();
            });

            signal.Wait();
            return result;
        }

        internal void Publish<T>(Sender<T> advertiser, T msg) where T : IMessage
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
                    Core.Logger.Error(e);
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

            var basePublisher = publishers[advertiser.Id];
            if (basePublisher != null && basePublisher is IRosPublisher<T> publisher)
            {
                publisher.Publish(msg);
            }
        }

        internal void Subscribe<T>([NotNull] Listener<T> listener) where T : IMessage, IDeserializable<T>, new()
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
                    Core.Logger.Error(e);
                    Disconnect();
                }
            });
        }

        async Task SubscribeImpl<T>([NotNull] IListener listener) where T : IMessage, IDeserializable<T>, new()
        {
            if (subscribersByTopic.TryGetValue(listener.Topic, out var subscribedTopic))
            {
                subscribedTopic.Add(listener);
                return;
            }

            var newSubscribedTopic = new SubscribedTopic<T>(listener.Topic);
            await newSubscribedTopic.SubscribeAsync(client, listener);
            subscribersByTopic.Add(listener.Topic, newSubscribedTopic);
        }

        internal void Unadvertise([NotNull] ISender advertiser)
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
                    Core.Logger.Error(e);
                    Disconnect();
                }
            });
        }

        async Task UnadvertiseImpl([NotNull] ISender advertiser)
        {
            if (!publishersByTopic.TryGetValue(advertiser.Topic, out var advertisedTopic))
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

        internal void Unsubscribe([NotNull] IListener subscriber)
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
                    Core.Logger.Error(e);
                    Disconnect();
                }
            });
        }


        async Task UnsubscribeImpl([NotNull] IListener subscriber)
        {
            if (!subscribersByTopic.TryGetValue(subscriber.Topic, out var subscribedTopic))
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

        public ReadOnlyCollection<BriefTopicInfo> GetSystemTopicTypes(
            RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedOnly)
            {
                return cachedTopics;
            }

            SemaphoreSlim signal = type == RequestType.WaitForRequest
                ? new SemaphoreSlim(0, 1)
                : null;

            AddTask(async () =>
            {
                try
                {
                    cachedTopics = client == null ? EmptyTopics : await client.GetSystemTopicTypesAsync();
                }
                catch (Exception e)
                {
                    Core.Logger.Error(e);
                }
                finally
                {
                    signal?.Release();
                }
            });

            signal?.Wait();
            return cachedTopics;
        }

        public ReadOnlyCollection<string> GetSystemParameterList()
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
                    Core.Logger.Error(e);
                }
            });

            return cachedParameters;
        }

        public object GetParameter([NotNull] string parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            var signal = new SemaphoreSlim(0, 1);
            object result = null;

            AddTask(async () =>
            {
                try
                {
                    if (client?.Parameters != null)
                    {
                        (_, result) = await client.Parameters.GetParameterAsync(parameter);
                    }
                }
                catch (Exception e)
                {
                    Core.Logger.Warn(e);
                }

                signal.Release();
            });

            signal.Wait();
            return result;
        }

        public int GetNumPublishers([NotNull] string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            subscribersByTopic.TryGetValue(topic, out var subscribedTopic);
            return subscribedTopic?.Subscriber?.NumPublishers ?? 0;
        }

        public int GetNumSubscribers([NotNull] string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            publishersByTopic.TryGetValue(topic, out var advertisedTopic);
            return advertisedTopic?.Publisher?.NumSubscribers ?? 0;
        }

        internal override void Stop()
        {
            Disconnect();
            base.Stop();
        }

        public void GenerateReport(StringBuilder builder)
        {
            var mClient = client;
            if (mClient == null)
            {
                return;
            }

            var subscriberStats = mClient.GetSubscriberStatistics();
            var publisherStats = mClient.GetPublisherStatistics();

            foreach (var stat in subscriberStats.Topics)
            {
                builder.Append("<b>** Subscribed to ").Append(stat.Topic).Append("</b>").AppendLine();
                builder.Append("<b>Type: </b><i>").Append(stat.Type).Append("</i>").AppendLine();
                builder.Append("<b>Connections:</b>").AppendLine();

                if (stat.Receivers.Count == 0)
                {
                    builder.Append("(None)").AppendLine();
                    builder.AppendLine();
                    continue;
                }

                foreach (var receiver in stat.Receivers)
                {
                    var isConnected = receiver.IsConnected;
                    var isAlive = receiver.IsAlive;
                    builder.Append("<b>→</b> ")
                        .Append(receiver.RemoteUri == mClient.CallerUri ? "[Me]" : receiver.RemoteUri.Host);
                    builder.Append(":").Append(receiver.RemoteUri.Port);
                    if (isAlive && isConnected)
                    {
                        int kbytes = receiver.BytesReceived / 1000;
                        builder.Append(" ↓").Append(kbytes.ToString("N0")).Append("kB");
                    }
                    else if (!isAlive)
                    {
                        builder.Append(" <color=red>(dead)</color>");
                    }
                    else
                    {
                        builder.Append(" <color=navy>(connecting)</color>");
                    }

                    if (receiver.ErrorDescription != null)
                    {
                        builder.AppendLine();
                        builder.Append("<color=brown>\"").Append(receiver.ErrorDescription).Append("\"</color>");
                    }

                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            builder.AppendLine();

            foreach (var stat in publisherStats.Topics)
            {
                builder.Append("<b>** Publishing to ").Append(stat.Topic).Append("</b>").AppendLine();
                builder.Append("<b>Type: </b><i>").Append(stat.Type).Append("</i>").AppendLine();
                builder.Append("<b>Connections:</b>").AppendLine();

                if (stat.Senders.Count == 0)
                {
                    builder.Append("(None)").AppendLine();
                    continue;
                }

                foreach (var receiver in stat.Senders)
                {
                    var isAlive = receiver.IsAlive;
                    builder.Append("<b>→</b> ");
                    if (receiver.RemoteId == mClient.CallerId)
                    {
                        builder.Append("[Me]");
                    }
                    else
                    {
                        builder.Append("[").Append(receiver.RemoteId).Append("] ")
                            .Append(receiver.RemoteEndpoint?.Hostname ?? "(Unknown address)");
                    }

                    if (isAlive)
                    {
                        int kbytes = receiver.BytesSent / 1000;
                        builder.Append(" ↑").Append(kbytes.ToString("N0")).Append("kB");
                    }
                    else
                    {
                        builder.Append(" <color=red>(dead)</color>");
                    }

                    builder.AppendLine();
                }

                builder.AppendLine();
            }
        }

        interface IAdvertisedTopic
        {
            [CanBeNull] IRosPublisher Publisher { get; }
            int Id { get; set; }
            int Count { get; }
            void Add([NotNull] ISender subscriber);
            void Remove([NotNull] ISender subscriber);
            Task AdvertiseAsync([CanBeNull] RosClient client);
            Task UnadvertiseAsync([NotNull] RosClient client);
            void Invalidate();
        }

        class AdvertisedTopic<T> : IAdvertisedTopic where T : IMessage
        {
            readonly HashSet<Sender<T>> senders = new HashSet<Sender<T>>();
            [NotNull] readonly string topic;
            int id;

            public AdvertisedTopic([NotNull] string topic)
            {
                this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
            }

            public IRosPublisher Publisher { get; private set; }

            public int Id
            {
                get => id;
                set
                {
                    id = value;
                    foreach (var sender in senders)
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
                var fullTopic = topic[0] == '/' ? topic : $"{client?.CallerId}/{topic}";
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

                var fullTopic = topic[0] == '/' ? topic : $"{client.CallerId}/{topic}";
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
            readonly HashSet<Listener<T>> listeners = new HashSet<Listener<T>>();
            [NotNull] readonly string topic;

            public SubscribedTopic([NotNull] string topic)
            {
                this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
            }

            public IRosSubscriber Subscriber { get; private set; }

            public void Add(IListener subscriber)
            {
                listeners.Add((Listener<T>) subscriber);
            }

            public void Remove(IListener subscriber)
            {
                listeners.Remove((Listener<T>) subscriber);
            }

            public async Task SubscribeAsync(RosClient client, IListener listener)
            {
                var fullTopic = topic[0] == '/' ? topic : $"{client?.CallerId}/{topic}";
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
                var fullTopic = topic[0] == '/' ? topic : $"{client.CallerId}/{topic}";

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

            void Callback(T msg)
            {
                foreach (var listener in listeners)
                {
                    listener.EnqueueMessage(msg);
                }
            }
        }

        interface IAdvertisedService
        {
            Task AdvertiseAsync([CanBeNull] RosClient client);
        }

        class AdvertisedService<T> : IAdvertisedService where T : IService, new()
        {
            [NotNull] readonly Func<T, Task> callback;
            [NotNull] readonly string service;

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

            /*
            public AdvertisedService([NotNull] string service, [NotNull] Func<T, Task> callback)
            {
                this.service = service ?? throw new ArgumentNullException(nameof(service));
                this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            }
            */

            public async Task AdvertiseAsync(RosClient client)
            {
                var fullService = service[0] == '/' ? service : $"{client?.CallerId}/{service}";
                if (client != null)
                {
                    await client.AdvertiseServiceAsync(fullService, callback);
                }
            }
        }
    }
}