#define LOG_ENABLED

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib.XmlRpc;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Nito.AsyncEx;
using UnityEngine;
using Iviz.Roslib.Utils;
using UnityEditor;
using Logger = Iviz.Msgs.Logger;
using Random = System.Random;

namespace Iviz.Ros
{
    public sealed class RoslibConnection : RosConnection
    {
        static readonly Action<string> LogInternalIfHololens =
            Settings.IsHololens ? (Action<string>) Core.Logger.Internal : Logger.LogDebug;

        static readonly ReadOnlyCollection<string> EmptyParameters = Array.Empty<string>().AsReadOnly();
        readonly ConcurrentDictionary<int, IRosPublisher> publishers = new ConcurrentDictionary<int, IRosPublisher>();
        readonly Dictionary<string, IAdvertisedTopic> publishersByTopic = new Dictionary<string, IAdvertisedTopic>();
        readonly Dictionary<string, IAdvertisedService> servicesByTopic = new Dictionary<string, IAdvertisedService>();
        readonly Dictionary<string, ISubscribedTopic> subscribersByTopic = new Dictionary<string, ISubscribedTopic>();

        [NotNull] ReadOnlyCollection<string> cachedParameters = EmptyParameters;
        [NotNull] ReadOnlyCollection<BriefTopicInfo> cachedTopics = EmptyTopics;
        [CanBeNull] RosClient client;
        [CanBeNull] BagListener bagListener;

        [NotNull] CancellationTokenSource connectionTs = new CancellationTokenSource();

        Task watchdogTask;
        Uri masterUri;
        string myId;
        Uri myUri;

        public bool Connected => client != null;

        [NotNull] public RosClient Client => client ?? throw new InvalidOperationException("Client not connected");

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

        [CanBeNull]
        public BagListener BagListener
        {
            get => bagListener;
            set
            {
                AddTask(async () =>
                {
                    if (bagListener == value)
                    {
                        return;
                    }
                    
                    if (bagListener != null)
                    {
                        await bagListener.StopAsync();
                    }
                    
                    bagListener = value;
                    foreach (var subscriber in subscribersByTopic.Values)
                    {
                        subscriber.BagListener = value;
                    }
                });
            }
        }

        async Task DisposeClient()
        {
            if (client == null)
            {
                return;
            }

            connectionTs.Cancel();
            connectionTs = new CancellationTokenSource();
            await client.CloseAsync(connectionTs.Token).AwaitNoThrow(this);
            client = null;
        }

        protected override async ValueTask<bool> Connect()
        {
            if (MasterUri == null ||
                MasterUri.Scheme != "http")
            {
                Core.Logger.Internal("Connection request failed. Invalid master uri.");
                return false;
            }

            if (MyId == null)
            {
                Core.Logger.Internal("Connection request failed. Invalid id.");
                return false;
            }

            if (MyUri == null ||
                MyUri.Scheme != "http")
            {
                Core.Logger.Internal("Connection request failed. Invalid own uri.");
                return false;
            }

            if (client != null)
            {
                Debug.LogWarning("Warning: New client requested, but old client still running?!");
                await DisposeClient();
            }

            try
            {
                const int rpcTimeoutInMs = 3000;

#if LOG_ENABLED
                //Logger.LogDebug = Core.Logger.Debug;
                Logger.LogError = Core.Logger.Error;
                Logger.Log = Core.Logger.Info;
#endif
                Core.Logger.Internal("Connecting...");

                connectionTs.Cancel();
                connectionTs = new CancellationTokenSource();

                CancellationToken token = connectionTs.Token;

                RosClient newClient = await RosClient.CreateAsync(MasterUri, MyId, MyUri, token: token);
                client = newClient;

                await client.CheckOwnUriAsync(token);

                client.RosMasterClient.TimeoutInMs = rpcTimeoutInMs;

                AddTask(async () =>
                {
                    Core.Logger.Internal("Resubscribing and readvertising...");
                    token.ThrowIfCancellationRequested();

                    //ConnectionUtils.GlobalResolver["cpr-nextr17"] = "192.168.131.1";
                    //ConnectionUtils.GlobalResolver["ids-robdekon-1"] = "192.168.1.11";
                    (bool success, XmlRpcValue hosts) =
                        await client.Parameters.GetParameterAsync("/iviz/hosts", token);
                    if (success)
                    {
                        ParseHostsParam(hosts);
                    }

                    LogInternalIfHololens("--- Advertising services...");
                    token.ThrowIfCancellationRequested();
                    await servicesByTopic.Values
                        .Select(topic => ReAdvertiseService(topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    LogInternalIfHololens("+++ Done advertising services");

                    LogInternalIfHololens("--- Readvertising...");
                    token.ThrowIfCancellationRequested();
                    await publishersByTopic.Values
                        .Select(topic => ReAdvertise(topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    LogInternalIfHololens("+++ Done readvertising");

                    LogInternalIfHololens("--- Resubscribing...");
                    token.ThrowIfCancellationRequested();
                    await subscribersByTopic.Values
                        .Select(topic => ReSubscribe(topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    LogInternalIfHololens("+++ Done resubscribing");

                    LogInternalIfHololens("--- Requesting topics...");
                    token.ThrowIfCancellationRequested();
                    cachedTopics = await client.GetSystemPublishedTopicsAsync(token);
                    LogInternalIfHololens("+++ Done requesting topics");

                    Core.Logger.Internal("Finished resubscribing and readvertising!");

                    watchdogTask = WatchdogAsync(client.RosMasterClient, token);
                });

                Core.Logger.Debug("*** Connected!");

                Core.Logger.Internal("<b>Connected!</b>");
                LogConnectionCheck(token);

                return true;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case RosUnreachableUriException _:
                        Core.Logger.Internal($"<b>Error:</b> Own uri validation failed. Reason: {e.Message}");
                        break;
                    case RosUriBindingException _:
                        Core.Logger.Internal(
                            $"<b>Error:</b> Failed to bind to port {MyUri?.Port}. Maybe another iviz instance is running?");
                        break;
                    case RoslibException _:
                    case TimeoutException _:
                    case XmlRpcException _:
                    {
                        Core.Logger.Internal("Error:", e);
                        if (RosServerManager.IsActive && RosServerManager.MasterUri == MasterUri)
                        {
                            Core.Logger.Internal(
                                "Note: This appears to be my own master. Are you sure the uri network is reachable?");
                        }

                        break;
                    }
                    case OperationCanceledException _:
                        Core.Logger.Info("Connection cancelled!");
                        break;
                    default:
                        Core.Logger.Error("Exception during Connect():", e);
                        break;
                }
            }

            await DisconnectImpl();
            return false;
        }

        static void ParseHostsParam([CanBeNull] XmlRpcValue hostsObj)
        {
            try
            {
                if (hostsObj.IsEmpty)
                {
                    return;
                }

                if (!hostsObj.TryGetArray(out XmlRpcValue[] array))
                {
                    Core.Logger.Error("Error reading /iviz/hosts. Expected array of string pairs.");
                    return;
                }

                Dictionary<string, string> hosts = new Dictionary<string, string>();
                foreach (XmlRpcValue entry in array)
                {
                    if (!entry.TryGetArray(out XmlRpcValue[] pair) ||
                        pair.Length != 2 ||
                        !pair[0].TryGetString(out string hostname) ||
                        !pair[1].TryGetString(out string address))
                    {
                        Core.Logger.Error(
                            "Error reading /iviz/hosts entry '" + entry + "'. Expected a pair of strings.");
                        return;
                    }

                    hosts[hostname] = address;
                }

                ConnectionUtils.GlobalResolver.Clear();
                foreach (var pair in hosts)
                {
                    Core.Logger.Info($"Adding custom host {pair.Key} -> {pair.Value}");
                    ConnectionUtils.GlobalResolver[pair.Key] = pair.Value;
                }
            }
            catch (Exception e)
            {
                Core.Logger.Error("Unexpected exception in ParseHostParams", e);
            }
        }

        static async void LogConnectionCheck(CancellationToken token)
        {
            try
            {
                await Task.Delay(10000, token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            var sender = ConnectionManager.LogSender;
            if (sender == null)
            {
                return;
            }

            if (sender.NumSubscribers == 0)
            {
                Core.Logger.Internal("<b>Warning:</b> Our logger has no subscriptions yet. " +
                                     "Maybe /rosout hasn't seen us yet. " +
                                     "But it also may be that a firewall is limiting incoming connections.");
            }
        }

        static async Task WatchdogAsync(
            [NotNull] RosMasterClient masterApi,
            CancellationToken token)
        {
            const int maxTimeMasterUnseenInMs = 10000;
            DateTime lastMasterAccess = GameThread.Now;
            bool warningSet = false;
            Uri lastRosOutUri = null;
            var instance = ConnectionManager.Connection;

            instance.SetConnectionWarningState(false);
            try
            {
                while (!token.IsCancellationRequested)
                {
                    DateTime now = GameThread.Now;
                    try
                    {
                        LookupNodeResponse response = await masterApi.LookupNodeAsync("/rosout", token);

                        TimeSpan timeSinceLastAccess = now - lastMasterAccess;
                        lastMasterAccess = now;
                        if (warningSet)
                        {
                            Core.Logger.Internal("The master is visible again, but we may be out of sync. Restarting!");
                            instance.Disconnect();
                            break;
                        }

                        if (timeSinceLastAccess.TotalMilliseconds > maxTimeMasterUnseenInMs)
                        {
                            // we haven't seen the master in a while, but no error has been thrown
                            // by the routine that checks every 5 seconds. maybe the app was suspended?
                            Core.Logger.Internal(
                                "Haven't seen the master in a while. We may be out of sync. Restarting!");
                            instance.Disconnect();
                            break;
                        }

                        if (response.IsValid)
                        {
                            if (lastRosOutUri == null)
                            {
                                lastRosOutUri = response.Uri;
                            }
                            else if (lastRosOutUri != response.Uri)
                            {
                                Core.Logger.Internal("<b>Warning:</b> The master appears to have changed. Restarting!");
                                instance.Disconnect();
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (!warningSet)
                        {
                            Core.Logger.Internal("<b>Warning:</b> The master is not responding. It was last seen at" +
                                                 $" [{lastMasterAccess:HH:mm:ss}].");
                            instance.SetConnectionWarningState(true);
                            warningSet = true;
                        }
                    }

                    await Task.Delay(5000, token);
                }
            }
            catch (OperationCanceledException)
            {
            }

            instance.SetConnectionWarningState(false);
        }

        static readonly Random Random = new Random();

        [NotNull]
        static Task DelayByPlatform(CancellationToken token) => Settings.IsHololens
            ? Task.Delay(Random.Next(0, 1000), token)
            : Task.CompletedTask;

        async Task ReAdvertise([NotNull] IAdvertisedTopic topic, CancellationToken token)
        {
            await DelayByPlatform(token);
            await topic.AdvertiseAsync(client, token);
            topic.Id = publishers.Count;
            publishers[topic.Id] = topic.Publisher;
        }

        async Task ReSubscribe([NotNull] ISubscribedTopic topic, CancellationToken token)
        {
            await DelayByPlatform(token);
            //Logger.LogDebug("Resubscribing " + topic.Subscriber?.Topic);
            await topic.SubscribeAsync(client, token: token);
            //Logger.LogDebug("Finished resubscribing " + topic.Subscriber?.Topic);
        }

        async Task ReAdvertiseService([NotNull] IAdvertisedService service, CancellationToken token)
        {
            await DelayByPlatform(token);
            await service.AdvertiseAsync(client, token);
        }

        public override void Disconnect()
        {
            connectionTs.Cancel();

            if (client == null)
            {
                Signal();
                return;
            }

            AddTask(DisconnectImpl);
        }

        async Task DisconnectImpl()
        {
            foreach (var entry in publishersByTopic.Values)
            {
                entry.Invalidate();
            }

            foreach (var entry in subscribersByTopic.Values)
            {
                entry.Invalidate();
            }

            publishers.Clear();

            Core.Logger.Internal("Disconnecting...");
            await DisposeClient();
            Core.Logger.Internal("<b>Disconnected.</b>");
            if (watchdogTask != null)
            {
                await watchdogTask.AwaitNoThrow(this);
                watchdogTask = null;
            }

            base.Disconnect();
        }

        internal void Advertise<T>([NotNull] Sender<T> advertiser) where T : IMessage
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

            CancellationToken token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await AdvertiseImpl(advertiser, token);
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.Advertise()", e);
                }
            });
        }


        async Task AdvertiseImpl<T>([NotNull] Sender<T> advertiser, CancellationToken token) where T : IMessage
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
                await newAdvertisedTopic.AdvertiseAsync(client, token);

                var publisher = newAdvertisedTopic.Publisher;
                id = publishers.Where(pair => pair.Value == null).TryGetFirst(out var freePair)
                    ? freePair.Key
                    : publishers.Count;

                publishers[id] = publisher;
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

            AdvertiseService(service, (T t) =>
            {
                callback(t);
                return Task.CompletedTask;
            });
        }

        public void AdvertiseService<T>([NotNull] string service, [NotNull] Func<T, Task> callback)
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

            CancellationToken token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await AdvertiseServiceImpl(service, callback, token);
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.AdvertiseService(): ", e);
                }
            });
        }

        async Task AdvertiseServiceImpl<T>([NotNull] string serviceName, [NotNull] Func<T, Task> callback,
            CancellationToken token)
            where T : IService, new()
        {
            if (servicesByTopic.TryGetValue(serviceName, out var baseService) &&
                baseService is AdvertisedService<T> service)
            {
                service.Callback = callback;
                return;
            }

            Core.Logger.Info(
                $"Advertising service <b>{serviceName}</b> <i>[{BuiltIns.GetServiceType(typeof(T))}]</i>.");

            var newAdvertisedService = new AdvertisedService<T>(serviceName, callback);
            if (client != null)
            {
                await newAdvertisedService.AdvertiseAsync(client, token);
            }

            servicesByTopic.Add(serviceName, newAdvertisedService);
        }

        public override async ValueTask<bool> CallServiceAsync<T>(string service, T srv, int timeoutInMs,
            CancellationToken token)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (srv == null)
            {
                throw new ArgumentNullException(nameof(srv));
            }

            if (client == null || connectionTs.IsCancellationRequested)
            {
                return false;
            }

            token.ThrowIfCancellationRequested();

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token))
            {
                tokenSource.CancelAfter(timeoutInMs);
                try
                {
                    await client.CallServiceAsync(service, srv, true, tokenSource.Token);
                    return true;
                }
                catch (OperationCanceledException) when (token.IsCancellationRequested ||
                                                         connectionTs.IsCancellationRequested)
                {
                    throw;
                }
                catch (OperationCanceledException)
                {
                    throw new TimeoutException($"Service call to '{service}' timed out");
                }
            }
        }

        internal void Publish<T>([NotNull] Sender<T> advertiser, [NotNull] in T msg) where T : IMessage
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            if (connectionTs.IsCancellationRequested)
            {
                return;
            }

            try
            {
                PublishImpl(advertiser, msg);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Exception during RoslibConnection.Publish(): {e.Message}");
            }
        }

        void PublishImpl<T>([NotNull] ISender advertiser, in T msg) where T : IMessage
        {
            if (advertiser.Id == -1)
            {
                return;
            }

            if (publishers.TryGetValue(advertiser.Id, out var basePublisher) &&
                basePublisher is IRosPublisher<T> publisher)
            {
                publisher.Publish(msg);
            }
        }

        internal bool TryGetResolvedTopicName([NotNull] ISender advertiser, [CanBeNull] out string topicName)
        {
            if (advertiser.Id == -1 || !publishers.TryGetValue(advertiser.Id, out var basePublisher))
            {
                topicName = default;
                return false;
            }

            topicName = basePublisher.Topic;
            return true;
        }

        internal void Subscribe<T>([NotNull] Listener<T> listener) where T : IMessage, IDeserializable<T>, new()
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            CancellationToken token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await SubscribeImpl<T>(listener, token);
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.Subscribe()", e);
                }
            });
        }

        async Task SubscribeImpl<T>([NotNull] IListener listener, CancellationToken token)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (subscribersByTopic.TryGetValue(listener.Topic, out var subscribedTopic))
            {
                subscribedTopic.Add(listener);
                return;
            }

            var newSubscribedTopic = new SubscribedTopic<T>(listener.Topic);
            await newSubscribedTopic.SubscribeAsync(client, listener, token);
            subscribersByTopic.Add(listener.Topic, newSubscribedTopic);
        }

        internal void SetPause([NotNull] IListener listener, bool value)
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            AddTask(() =>
            {
                if (subscribersByTopic.TryGetValue(listener.Topic, out var subscribedTopic) &&
                    subscribedTopic.Subscriber != null)
                {
                    subscribedTopic.Subscriber.IsPaused = value;
                }

                return Task.CompletedTask;
            });
        }

        internal void Unadvertise([NotNull] ISender advertiser)
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

            CancellationToken token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await UnadvertiseImpl(advertiser, token);
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.Unadvertise()", e);
                }
            });
        }

        async Task UnadvertiseImpl([NotNull] ISender advertiser, CancellationToken token)
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
                await advertisedTopic.UnadvertiseAsync(client, token);
                PublishedTopics = client.PublishedTopics;
            }
        }

        internal void Unsubscribe([NotNull] IListener subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            CancellationToken token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await UnsubscribeImpl(subscriber, token);
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.Unsubscribe()", e);
                }
            });
        }

        async Task UnsubscribeImpl([NotNull] IListener subscriber, CancellationToken token)
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
                    await subscribedTopic.UnsubscribeAsync(client, token);
                }
            }
        }

        [NotNull]
        public ReadOnlyCollection<BriefTopicInfo> GetSystemTopicTypes(
            RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedOnly)
            {
                return cachedTopics;
            }

            CancellationToken token = connectionTs.Token;
            TaskUtils.StartLongTask(async () =>
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    cachedTopics = client == null
                        ? EmptyTopics
                        : await client.GetSystemPublishedTopicsAsync(token);
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.GetSystemTopicTypes()", e);
                }
            }, token);
            return cachedTopics;
        }

        [ItemCanBeNull]
        public async ValueTask<ReadOnlyCollection<BriefTopicInfo>> GetSystemTopicTypesAsync(int timeoutInMs,
            CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                return null;
            }

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token))
            {
                tokenSource.CancelAfter(timeoutInMs);

                try
                {
                    cachedTopics = client == null
                        ? EmptyTopics
                        : await client.GetSystemPublishedTopicsAsync(tokenSource.Token);
                    return cachedTopics;
                }
                catch (OperationCanceledException)
                {
                    return null;
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.GetSystemTopicTypes()", e);
                    return null;
                }
            }
        }

        [NotNull, ItemNotNull]
        public IEnumerable<string> GetSystemParameterList(CancellationToken token = default)
        {
            CancellationToken internalToken = connectionTs.Token;
            Task.Run(async () =>
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    if (client?.Parameters is null)
                    {
                        cachedParameters = EmptyParameters;
                        return;
                    }

                    using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, internalToken))
                    {
                        cachedParameters = await client.Parameters.GetParameterNamesAsync(tokenSource.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.GetSystemParameterList()", e);
                }
            }, internalToken);
            return cachedParameters;
        }

        public async ValueTask<(XmlRpcValue result, string errorMsg)> GetParameterAsync([NotNull] string parameter,
            int timeoutInMs, CancellationToken token = default)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (token.IsCancellationRequested)
            {
                return (default, "Cancellation requested");
            }

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token))
            {
                tokenSource.CancelAfter(timeoutInMs);

                try
                {
                    if (client?.Parameters == null)
                    {
                        return (default, "Not connected");
                    }

                    (bool success, XmlRpcValue param) =
                        await client.Parameters.GetParameterAsync(parameter, tokenSource.Token);
                    if (!success)
                    {
                        return (default, $"'{parameter}' not found");
                    }

                    return (param, null);
                }
                catch (OperationCanceledException)
                {
                    return (default, "Operation timed out");
                }
                catch (XmlRpcException)
                {
                    return (default, "Failed to read parameter");
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.GetParameter()", e);
                    return (default, "Unknown error");
                }
            }
        }

        [ItemCanBeNull]
        public async ValueTask<SystemState> GetSystemStateAsync(int timeoutInMs = 2000,
            CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                return null;
            }

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token))
            {
                tokenSource.CancelAfter(timeoutInMs);

                try
                {
                    if (client?.Parameters == null)
                    {
                        return null;
                    }

                    return await client.GetSystemStateAsync(tokenSource.Token);
                }
                catch (Exception e)
                {
                    Core.Logger.Error("Exception during RoslibConnection.GetParameter()", e);
                    return null;
                }
            }
        }

        public (int Active, int Total) GetNumPublishers([NotNull] string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            subscribersByTopic.TryGetValue(topic, out var subscribedTopic);
            var subscriber = subscribedTopic?.Subscriber;
            return subscriber != null ? (subscriber.NumActivePublishers, subscriber.NumPublishers) : default;
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

        interface IAdvertisedTopic
        {
            [CanBeNull] IRosPublisher Publisher { get; }
            int Id { get; set; }
            int Count { get; }
            void Add([NotNull] ISender subscriber);
            void Remove([NotNull] ISender subscriber);
            Task AdvertiseAsync([CanBeNull] RosClient client, CancellationToken token);
            Task UnadvertiseAsync([NotNull] RosClient client, CancellationToken token);
            void Invalidate();
        }

        sealed class AdvertisedTopic<T> : IAdvertisedTopic where T : IMessage
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

            public async Task AdvertiseAsync(RosClient client, CancellationToken token)
            {
                token.ThrowIfCancellationRequested();
                string fullTopic = topic[0] == '/' ? topic : $"{client?.CallerId}/{topic}";
                IRosPublisher publisher;
                if (client != null)
                {
                    (_, publisher) = await client.AdvertiseAsync<T>(fullTopic, token);
                }
                else
                {
                    publisher = null;
                }

                Publisher = publisher;
            }

            public async Task UnadvertiseAsync(RosClient client, CancellationToken token)
            {
                if (client == null)
                {
                    throw new ArgumentNullException(nameof(client));
                }

                token.ThrowIfCancellationRequested();
                var fullTopic = topic[0] == '/' ? topic : $"{client.CallerId}/{topic}";
                if (Publisher != null)
                {
                    await Publisher.UnadvertiseAsync(fullTopic, token);
                }
            }

            public void Invalidate()
            {
                Id = -1;
                Publisher = null;
            }

            [NotNull]
            public override string ToString()
            {
                return $"[AdvertisedTopic '{topic}']";
            }
        }

        interface ISubscribedTopic
        {
            [CanBeNull] IRosSubscriber Subscriber { get; }
            int Count { get; }
            void Add([NotNull] IListener subscriber);
            void Remove([NotNull] IListener subscriber);

            Task SubscribeAsync([CanBeNull] RosClient client, [CanBeNull] IListener listener = null,
                CancellationToken token = default);

            Task UnsubscribeAsync([NotNull] RosClient client, CancellationToken token);
            void Invalidate();

            BagListener BagListener { set; }
        }

        sealed class SubscribedTopic<T> : ISubscribedTopic where T : IMessage, IDeserializable<T>, new()
        {
            readonly HashSet<Listener<T>> listeners = new HashSet<Listener<T>>();
            [NotNull] readonly string topic;
            [CanBeNull] string clientId;

            [CanBeNull] BagListener bagListener;
            [CanBeNull] string bagId;

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

            public async Task SubscribeAsync(RosClient client, IListener listener,
                CancellationToken token)
            {
                token.ThrowIfCancellationRequested();
                string fullTopic = topic[0] == '/' ? topic : $"{client?.CallerId}/{topic}";
                IRosSubscriber subscriber;
                if (listener != null)
                {
                    listeners.Add((Listener<T>) listener);
                }

                if (client != null)
                {
                    (clientId, subscriber) = await client.SubscribeAsync<T>(fullTopic, Callback, token: token);
                    if (bagListener != null)
                    {
                        bagId = subscriber.Subscribe(bagListener.EnqueueMessage);
                    }
                }
                else
                {
                    subscriber = null;
                }

                Subscriber = subscriber;
            }

            public async Task UnsubscribeAsync(RosClient client, CancellationToken token)
            {
                token.ThrowIfCancellationRequested();

                if (Subscriber == null)
                {
                    return;
                }

                BagListener = null;

                if (clientId != null)
                {
                    await Subscriber.UnsubscribeAsync(clientId, token);
                    clientId = null;
                }

                Subscriber = null;
            }

            [CanBeNull]
            public BagListener BagListener
            {
                set
                {
                    bagListener = value;
                    if (Subscriber == null)
                    {
                        return;
                    }

                    if (bagListener != null)
                    {
                        if (bagId != null)
                        {
                            Subscriber.Unsubscribe(bagId);
                        }

                        bagId = Subscriber.Subscribe(bagListener.EnqueueMessage);
                    }
                    else if (bagId != null)
                    {
                        Subscriber.Unsubscribe(bagId);
                        bagId = null;
                    }
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
                    try
                    {
                        listener.EnqueueMessage(msg);
                    }
                    catch (Exception e)
                    {
                        Core.Logger.Error($"{this}: Error in callback", e);
                    }
                }
            }

            [NotNull]
            public override string ToString()
            {
                return $"[SubscribedTopic '{topic}']";
            }
        }

        interface IAdvertisedService
        {
            Task AdvertiseAsync([CanBeNull] RosClient client, CancellationToken token);
        }

        sealed class AdvertisedService<T> : IAdvertisedService where T : IService, new()
        {
            [NotNull] Func<T, Task> callback;
            [NotNull] readonly string service;

            public Func<T, Task> Callback
            {
                set => callback = value ?? throw new ArgumentNullException(nameof(value));
            }

            public AdvertisedService([NotNull] string service, [NotNull] Func<T, Task> callback)
            {
                this.service = service ?? throw new ArgumentNullException(nameof(service));
                this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            }

            public async Task AdvertiseAsync(RosClient client, CancellationToken token)
            {
                token.ThrowIfCancellationRequested();
                var fullService = service[0] == '/' ? service : $"{client?.CallerId}/{service}";
                if (client != null)
                {
                    await client.AdvertiseServiceAsync<T>(fullService, CallbackImpl, token);
                }
            }

            Task CallbackImpl(T t) => callback(t);

            [NotNull]
            public override string ToString()
            {
                return $"[AdvertisedService '{service}']";
            }
        }
    }
}