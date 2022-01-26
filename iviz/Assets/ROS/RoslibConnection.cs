﻿#nullable enable

#define LOG_ENABLED

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Ntp;
using Iviz.Roslib;
using Iviz.Roslib.XmlRpc;
using Iviz.XmlRpc;
using Nito.AsyncEx;
using UnityEngine;
using Iviz.Tools;
using Random = System.Random;

namespace Iviz.Ros
{
    public sealed class RoslibConnection : RosConnection
    {
        internal const int InvalidId = -1;

        static readonly IReadOnlyCollection<BriefTopicInfo> EmptyTopics = Array.Empty<BriefTopicInfo>();
        static readonly Random Random = new();
        static readonly IReadOnlyCollection<string> EmptyParameters = Array.Empty<string>();

        readonly ConcurrentDictionary<int, IRosPublisher?> publishers = new();
        readonly Dictionary<string, IAdvertisedTopic> publishersByTopic = new();
        readonly Dictionary<string, IAdvertisedService> servicesByTopic = new();
        readonly Dictionary<string, ISubscribedTopic> subscribersByTopic = new();
        readonly List<(string hostname, string address)> hostAliases = new();

        IReadOnlyCollection<string> cachedParameters = EmptyParameters;
        IReadOnlyCollection<BriefTopicInfo> cachedPublishedTopics = EmptyTopics;
        IReadOnlyCollection<BriefTopicInfo> cachedTopics = EmptyTopics;
        SystemState? cachedSystemState;
        RosClient? client;
        BagListener? bagListener;

        CancellationTokenSource connectionTs = new();

        Task? watchdogTask;
        Task? ntpTask;
        Uri? masterUri;
        string? myId;
        Uri? myUri;

        bool Connected => client != null;

        public RosClient Client => client ?? throw new InvalidOperationException("Client not connected");

        public Uri? MasterUri
        {
            get => masterUri;
            set
            {
                masterUri = value;
                Disconnect();
            }
        }

        public string? MyId
        {
            get => myId;
            set
            {
                myId = value;
                Disconnect();
            }
        }

        public Uri? MyUri
        {
            get => myUri;
            set
            {
                myUri = value;
                Disconnect();
            }
        }

        public BagListener? BagListener
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
                        await bagListener.DisposeAsync();
                    }

                    bagListener = value;
                    foreach (var subscriber in subscribersByTopic.Values)
                    {
                        subscriber.BagListener = value;
                    }
                });
            }
        }

        public void SetHostAliases(IEnumerable<(string hostname, string address)> newHostAliases)
        {
            hostAliases.Clear();
            hostAliases.AddRange(newHostAliases);
        }

        async ValueTask DisposeClientAsync()
        {
            if (!Connected)
            {
                return;
            }

            connectionTs.Cancel();
            connectionTs = new CancellationTokenSource();
            await Client.CloseAsync(connectionTs.Token).AwaitNoThrow(this);
            client = null;
        }

        protected override async ValueTask<bool> ConnectAsync()
        {
            if (MasterUri == null ||
                MasterUri.Scheme != "http")
            {
                RosLogger.Internal("Connection request failed. Invalid master uri.");
                return false;
            }

            if (MyId == null)
            {
                RosLogger.Internal("Connection request failed. Invalid id.");
                return false;
            }

            if (MyUri == null ||
                MyUri.Scheme != "http")
            {
                RosLogger.Internal("Connection request failed. Invalid own uri.");
                return false;
            }

            if (Connected)
            {
                Debug.LogWarning("Warning: New client requested, but old client still running?!");
                await DisposeClientAsync();
            }

            try
            {
                const int rpcTimeoutInMs = 3000;


                //Tools.Logger.LogDebug = RosLogger.Debug;
                Tools.Logger.LogError = RosLogger.Error;
                Tools.Logger.Log = RosLogger.Info;


                RosLogger.Internal("Connecting...");

                connectionTs.Cancel();
                connectionTs = new CancellationTokenSource();

                var token = connectionTs.Token;

                RosClient newClient = await RosClient.CreateAsync(MasterUri, MyId, MyUri, token: token);
                client = newClient;

                await Client.CheckOwnUriAsync(token);

                Client.RosMasterClient.TimeoutInMs = rpcTimeoutInMs;

                AddTask(async () =>
                {
                    RosLogger.Internal("Resubscribing and readvertising...");
                    token.ThrowIfCancellationRequested();

                    (bool success, XmlRpcValue hosts) =
                        await Client.Parameters.GetParameterAsync("/iviz/hosts", token);
                    if (success)
                    {
                        AddHostsParamFromArg(hosts);
                    }

                    AddConfigHostAliases();

                    RosLogger.Debug("--- Advertising services...");
                    token.ThrowIfCancellationRequested();
                    await servicesByTopic.Values
                        .Select(topic => ReAdvertiseService(topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    RosLogger.Debug("+++ Done advertising services");

                    RosLogger.Debug("--- Readvertising...");
                    token.ThrowIfCancellationRequested();
                    await publishersByTopic.Values
                        .Select(topic => ReAdvertise(topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    RosLogger.Debug("+++ Done readvertising");

                    RosLogger.Debug("--- Resubscribing...");
                    token.ThrowIfCancellationRequested();
                    await subscribersByTopic.Values
                        .Select(topic => ReSubscribe(topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    RosLogger.Debug("+++ Done resubscribing");

                    RosLogger.Debug("--- Requesting topics...");
                    token.ThrowIfCancellationRequested();
                    cachedPublishedTopics = await Client.GetSystemPublishedTopicsAsync(token);
                    RosLogger.Debug("+++ Done requesting topics");

                    cachedSystemState = null;
                    cachedTopics = EmptyTopics;

                    RosLogger.Internal("Finished resubscribing and readvertising!");

                    watchdogTask = WatchdogTask(Client.RosMasterClient, token);
                    ntpTask = NtpCheckerTask(Client.MasterUri.Host, token);
                });

                RosLogger.Debug($"{this}: Connected!");
                RosLogger.Internal("<b>Connected!</b>");

                LogConnectionCheck(token);

                return true;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case RosUnreachableUriException _:
                        RosLogger.Internal($"<b>Connection failed:</b> Cannot reach my own URI. Reason: {e.Message}");
                        break;
                    case RosUriBindingException _:
                        RosLogger.Internal(
                            $"<b>Error:</b> Port {MyUri?.Port.ToString()} is already being used by another application. " +
                            $"Maybe another iviz instance is running? Try another port!");
                        break;
                    case RoslibException _:
                    case TimeoutException _:
                    case XmlRpcException _:
                    {
                        RosLogger.Internal("<b>Connection failed:</b>", e);
                        if (RosServerManager.IsActive && RosServerManager.MasterUri == MasterUri)
                        {
                            RosLogger.Internal("Note: This appears to be a local ROS master. " +
                                               "Make sure that <b>My Caller URI</b> is a reachable address and that " +
                                               "you are in the right network. Then stop and restart the master.");
                        }

                        break;
                    }
                    case OperationCanceledException _:
                        RosLogger.Info($"{this}: Connection cancelled!");
                        break;
                    default:
                        RosLogger.Error($"{this}: Exception during {nameof(ConnectAsync)}(): ", e);
                        break;
                }
            }

            await DisconnectImpl();
            return false;
        }

        static void AddHostsParamFromArg(XmlRpcValue hostsObj)
        {
            if (hostsObj.IsEmpty)
            {
                return;
            }

            if (!hostsObj.TryGetArray(out XmlRpcValue[] array))
            {
                RosLogger.Error($"{nameof(RoslibConnection)}: Error reading /iviz/hosts. " +
                                $"Expected array of string pairs.");
                return;
            }

            var hosts = new Dictionary<string, string>();
            foreach (var entry in array)
            {
                if (!entry.TryGetArray(out XmlRpcValue[] pair) ||
                    pair.Length != 2 ||
                    !pair[0].TryGetString(out string hostname) ||
                    !pair[1].TryGetString(out string address))
                {
                    RosLogger.Error(
                        $"{nameof(RoslibConnection)}: Error reading /iviz/hosts entry '{entry.ToString()}'. " +
                        $"Expected a pair of strings.");
                    return;
                }

                hosts[hostname] = address;
            }

            ConnectionUtils.GlobalResolver.Clear();
            foreach (var (key, value) in hosts)
            {
                RosLogger.Info($"{nameof(RoslibConnection)}: Adding custom host {key} -> {value}");
                ConnectionUtils.GlobalResolver[key] = value;
            }
        }

        void AddConfigHostAliases()
        {
            foreach (var (hostname, address) in hostAliases)
            {
                ConnectionUtils.GlobalResolver[hostname] = address;
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
                RosLogger.Internal("<b>Warning:</b> Our logger has no subscriptions yet. " +
                                   "Maybe /rosout hasn't seen us yet. " +
                                   "But it also may be that outside nodes cannot connect to us, for example due to a firewall.");
            }
        }

        static async Task WatchdogTask(
            RosMasterClient masterApi,
            CancellationToken token)
        {
            const int maxTimeMasterUnseenInMs = 10000;
            const int delayBetweenPingsInMs = 5000;
            DateTime lastMasterAccess = GameThread.Now;
            bool warningSet = false;
            Uri? lastRosOutUri = null;
            var connection = ConnectionManager.Connection;


            SetConnectionWarningState(false);
            try
            {
                for (; !token.IsCancellationRequested; await Task.Delay(delayBetweenPingsInMs, token))
                {
                    var now = GameThread.Now;
                    LookupNodeResponse response;
                    try
                    {
                        response = await masterApi.LookupNodeAsync("/rosout", token);
                    }
                    catch (Exception)
                    {
                        if (!warningSet)
                        {
                            RosLogger.Internal("<b>Warning:</b> The master is not responding. It was last seen at" +
                                               $" [{lastMasterAccess:HH:mm:ss}].");
                            SetConnectionWarningState(true);
                            warningSet = true;
                        }

                        continue;
                    }

                    var timeSinceLastAccess = now - lastMasterAccess;
                    lastMasterAccess = now;
                    if (warningSet)
                    {
                        RosLogger.Internal("The master is visible again, but we may be out of sync. Restarting!");
                        connection.Disconnect();
                        break;
                    }

                    if (timeSinceLastAccess.TotalMilliseconds > maxTimeMasterUnseenInMs)
                    {
                        // we haven't seen the master in a while, but no error has been thrown
                        // by the routine that checks every 5 seconds. maybe the app was suspended?
                        RosLogger.Internal(
                            "Haven't seen the master in a while. We may be out of sync. Restarting!");
                        connection.Disconnect();
                        break;
                    }

                    if (!response.IsValid)
                    {
                        continue;
                    }

                    if (lastRosOutUri == null)
                    {
                        lastRosOutUri = response.Uri;
                    }
                    else if (lastRosOutUri != response.Uri)
                    {
                        RosLogger.Internal("<b>Warning:</b> The master appears to have changed. Restarting!");
                        connection.Disconnect();
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }

            SetConnectionWarningState(false);
        }

        static async Task NtpCheckerTask(string hostname, CancellationToken token)
        {
            RosLogger.Debug("[NtpChecker]: Starting NTP task.");
            time.GlobalTimeOffset = TimeSpan.Zero;

            TimeSpan offset;
            try
            {
                offset = await NtpQuery.GetNetworkTimeOffsetAsync(hostname, token);
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException or IOException)
                {
                    RosLogger.Debug("[NtpChecker]: Master does not appear to have an NTP clock running.");
                }
                else
                {
                    RosLogger.Error("[NtpChecker]: Failed to read NTP clock from the master.", e);
                }

                return;
            }

            const int minOffsetInMs = 2;
            if (Math.Abs(offset.TotalMilliseconds) < minOffsetInMs)
            {
                RosLogger.Info("[NtpChecker]: No significant time offset detected from master clock.");
            }
            else
            {
                time.GlobalTimeOffset = offset;
                string offsetStr = Math.Abs(offset.TotalSeconds) >= 1
                    ? $"{offset.TotalSeconds:#,0.###} sec"
                    : $"{offset.TotalMilliseconds:#,0.###} ms";
                RosLogger.Info($"[NtpChecker]: Master clock appears to have a time offset of {offsetStr}. " +
                               "Local published messages will use this offset.");
            }
        }

        static Task RandomDelay(CancellationToken token) => Task.Delay(Random.Next(0, 100), token);

        async ValueTask ReAdvertise(IAdvertisedTopic topic, CancellationToken token)
        {
            await RandomDelay(token);
            await topic.AdvertiseAsync(Connected ? Client : null, token);
            topic.Id = publishers.Count;
            publishers[topic.Id] = topic.Publisher;
        }

        async ValueTask ReSubscribe(ISubscribedTopic topic, CancellationToken token)
        {
            await RandomDelay(token);
            await topic.SubscribeAsync(Connected ? Client : null, token: token);
        }

        async ValueTask ReAdvertiseService(IAdvertisedService service, CancellationToken token)
        {
            await RandomDelay(token);
            await service.AdvertiseAsync(Connected ? Client : null, token);
        }

        public override void Disconnect()
        {
            connectionTs.Cancel();

            if (!Connected)
            {
                Signal();
                return;
            }

            AddTask(DisconnectImpl);
        }

        async ValueTask DisconnectImpl()
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

            RosLogger.Internal("Disconnecting...");
            await DisposeClientAsync();
            RosLogger.Internal("<b>Disconnected.</b>");
            if (watchdogTask != null)
            {
                await watchdogTask.AwaitNoThrow(this);
                watchdogTask = null;
            }

            if (ntpTask != null)
            {
                await ntpTask.AwaitNoThrow(this);
                ntpTask = null;
            }

            base.Disconnect();
        }

        internal void Advertise<T>(Sender<T> advertiser) where T : IMessage
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

            advertiser.Id = InvalidId;
            CancellationToken token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await AdvertiseImpl(advertiser, token);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(Advertise)}()", e);
                }
            });
        }


        async ValueTask AdvertiseImpl<T>(Sender<T> advertiser, CancellationToken token) where T : IMessage
        {
            if (publishersByTopic.TryGetValue(advertiser.Topic, out var advertisedTopic))
            {
                advertisedTopic.Add(advertiser);
                advertiser.Id = advertisedTopic.Id;
                return;
            }

            var newAdvertisedTopic = new AdvertisedTopic<T>(advertiser.Topic);

            int id;
            if (Connected)
            {
                id = publishers.Where(pair => pair.Value == null).TryGetFirst(out var freePair)
                    ? freePair.Key
                    : publishers.Count;

                await newAdvertisedTopic.AdvertiseAsync(Client, token);
                var publisher = newAdvertisedTopic.Publisher;
                if (publisher == null)
                {
                    RosLogger.Error($"{this}: Failed to advertise topic '{advertiser.Topic}'");
                    return;
                }

                publishers[id] = publisher;
            }

            else
            {
                id = InvalidId;
            }

            newAdvertisedTopic.Id = id;
            publishersByTopic.Add(advertiser.Topic, newAdvertisedTopic);
            newAdvertisedTopic.Add(advertiser);
            advertiser.Id = newAdvertisedTopic.Id;
        }

        public void AdvertiseService<T>(string service, Action<T> callback) where T : IService, new()
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
                return default;
            });
        }

        public void AdvertiseService<T>(string service, Func<T, ValueTask> callback) where T : IService, new()
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
                    RosLogger.Error($"{this}: Exception during {nameof(AdvertiseService)}()", e);
                }
            });
        }

        async ValueTask AdvertiseServiceImpl<T>(string serviceName, Func<T, ValueTask> callback,
            CancellationToken token)
            where T : IService, new()
        {
            if (servicesByTopic.TryGetValue(serviceName, out var baseService)
                && baseService.TrySetCallback(callback))
            {
                return;
            }

            RosLogger.Info($"{this}: Advertising service <b>{serviceName}</b><i>[{BuiltIns.GetServiceType<T>()}]</i>.");

            var newAdvertisedService = new AdvertisedService<T>(serviceName, callback);
            servicesByTopic.Add(serviceName, newAdvertisedService);
            if (Connected)
            {
                await newAdvertisedService.AdvertiseAsync(Client, token);
            }
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

            if (!Connected || connectionTs.IsCancellationRequested)
            {
                return false;
            }

            token.ThrowIfCancellationRequested();

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token);
            tokenSource.CancelAfter(timeoutInMs);
            try
            {
                await Client.CallServiceAsync(service, srv, true, tokenSource.Token);
                return true;
            }
            catch (OperationCanceledException e) when (!token.IsCancellationRequested &&
                                                       !connectionTs.IsCancellationRequested)
            {
                throw new TimeoutException($"Service call to '{service}' timed out", e);
            }
        }

        internal void Publish<T>(Sender<T> advertiser, in T msg) where T : IMessage
        {
            if (advertiser.Id == InvalidId || connectionTs.IsCancellationRequested)
            {
                return;
            }

            try
            {
                msg.RosValidate();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Rejecting invalid message", e);
                return;
            }

            try
            {
                if (publishers.TryGetValue(advertiser.Id, out var basePublisher)
                    && basePublisher is { NumSubscribers: > 0 } and IRosPublisher<T> publisher)
                {
                    publisher.Publish(msg);
                }
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{this}: Exception during {nameof(Publish)}()", e);
            }
        }

        internal void Subscribe<T>(Listener<T> listener) where T : IMessage, IDeserializable<T>, new()
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            var token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await SubscribeImpl<T>(listener, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(Subscribe)}()", e);
                }
            });
        }

        async ValueTask SubscribeImpl<T>(IListener listener, CancellationToken token)
            where T : IMessage, IDeserializable<T>, new()
        {
            if (subscribersByTopic.TryGetValue(listener.Topic, out var subscribedTopic))
            {
                subscribedTopic.Add(listener);
                return;
            }

            var newSubscribedTopic = new SubscribedTopic<T>(listener.Topic, listener.TransportHint);
            subscribersByTopic.Add(listener.Topic, newSubscribedTopic);
            await newSubscribedTopic.SubscribeAsync(Connected ? Client : null, listener, token);
        }

        internal void SetPause(IListener listener, bool value)
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

                return default;
            });
        }

        internal void Unadvertise(ISender advertiser)
        {
            if (advertiser == null)
            {
                throw new ArgumentNullException(nameof(advertiser));
            }

            var token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await UnadvertiseImpl(advertiser, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(Unadvertise)}()", e);
                }
            });
        }

        ValueTask UnadvertiseImpl(ISender advertiser, CancellationToken token)
        {
            if (!publishersByTopic.TryGetValue(advertiser.Topic, out var advertisedTopic))
            {
                return default;
            }

            advertisedTopic.Remove(advertiser);
            if (advertisedTopic.Count != 0)
            {
                return default;
            }

            publishersByTopic.Remove(advertiser.Topic);
            if (advertiser.Id != InvalidId)
            {
                publishers[advertiser.Id] = null;
            }

            return Connected
                ? advertisedTopic.UnadvertiseAsync(token)
                : default;
        }

        internal void Unsubscribe(IListener subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            var token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await UnsubscribeImpl(subscriber, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(Unsubscribe)}()", e);
                }
            });
        }

        ValueTask UnsubscribeImpl(IListener subscriber, CancellationToken token)
        {
            if (!subscribersByTopic.TryGetValue(subscriber.Topic, out var subscribedTopic))
            {
                return default;
            }

            subscribedTopic.Remove(subscriber);
            if (subscribedTopic.Count != 0)
            {
                return default;
            }

            subscribersByTopic.Remove(subscriber.Topic);
            return Connected
                ? subscribedTopic.UnsubscribeAsync(token)
                : default;
        }

        public void UnadvertiseService(string service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var token = connectionTs.Token;
            AddTask(async () =>
            {
                try
                {
                    await UnadvertiseServiceImpl(service, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(UnadvertiseService)}()", e);
                }
            });
        }

        async ValueTask UnadvertiseServiceImpl(string serviceName, CancellationToken token)
        {
            if (!servicesByTopic.TryGetValue(serviceName, out var service))
            {
                return;
            }

            RosLogger.Info($"{this}: Unadvertising service <b>{serviceName}</b>.");

            if (Connected)
            {
                await service.UnadvertiseAsync(Client, token);
            }

            servicesByTopic.Remove(serviceName);
        }

        public IEnumerable<BriefTopicInfo> GetSystemPublishedTopicTypes(
            RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedButRequestInBackground)
            {
                TaskUtils.Run(() => GetSystemPublishedTopicTypesAsync().AsTask(), connectionTs.Token);
            }

            return cachedPublishedTopics;
        }

        public async ValueTask<IEnumerable<BriefTopicInfo>> GetSystemPublishedTopicTypesAsync(int timeoutInMs = 2000,
            CancellationToken token = default)
        {
            if (!Connected || token.IsCancellationRequested || connectionTs.Token.IsCancellationRequested)
            {
                cachedPublishedTopics = EmptyTopics;
                return EmptyTopics;
            }

            try
            {
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                cachedPublishedTopics = await Client.GetSystemPublishedTopicsAsync(tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during {nameof(GetSystemPublishedTopicTypesAsync)}()", e);
            }

            return cachedPublishedTopics;
        }

        public IEnumerable<BriefTopicInfo> GetSystemTopicTypes(
            RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedButRequestInBackground)
            {
                TaskUtils.Run(() => GetSystemTopicTypesAsync().AsTask(), connectionTs.Token);
            }

            return cachedTopics;
        }

        async ValueTask GetSystemTopicTypesAsync(int timeoutInMs = 2000, CancellationToken token = default)
        {
            if (!Connected || token.IsCancellationRequested || connectionTs.Token.IsCancellationRequested)
            {
                cachedTopics = EmptyTopics;
                return;
            }

            try
            {
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                cachedTopics = await Client.GetSystemTopicTypesAsync(tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during {nameof(GetSystemTopicTypesAsync)}()", e);
            }
        }

        public IEnumerable<string> GetSystemParameterList(CancellationToken token = default)
        {
            var internalToken = connectionTs.Token;
            TaskUtils.Run(async () =>
            {
                if (!Connected || token.IsCancellationRequested || internalToken.IsCancellationRequested)
                {
                    cachedParameters = EmptyParameters;
                    return;
                }

                try
                {
                    using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, internalToken);
                    cachedParameters = await Client.Parameters.GetParameterNamesAsync(tokenSource.Token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(GetSystemParameterList)}()", e);
                }
            }, internalToken);

            return cachedParameters;
        }

        public async ValueTask<(XmlRpcValue result, string? errorMsg)> GetParameterAsync(string parameter,
            int timeoutInMs, CancellationToken token = default)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (token.IsCancellationRequested || connectionTs.Token.IsCancellationRequested)
            {
                return (default, "Cancellation requested");
            }

            if (!Connected)
            {
                return (default, "Not connected");
            }

            try
            {
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, connectionTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                (bool success, XmlRpcValue param) =
                    await Client.Parameters.GetParameterAsync(parameter, tokenSource.Token);
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
                RosLogger.Error($"{this}: Exception during {nameof(GetParameterAsync)}()", e);
                return (default, "Unknown error");
            }
        }

        public SystemState? GetSystemState(RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedButRequestInBackground)
            {
                TaskUtils.Run(() => GetSystemStateAsync().AsTask(), connectionTs.Token);
            }

            return cachedSystemState;
        }

        async ValueTask GetSystemStateAsync()
        {
            if (!Connected)
            {
                cachedSystemState = null;
                return;
            }

            const int timeoutInMs = 2000;
            try
            {
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(connectionTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                cachedSystemState = await Client.GetSystemStateAsync(tokenSource.Token);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{this}: Exception during {nameof(GetSystemStateAsync)}()", e);
            }
        }

        public (int active, int total) GetNumPublishers(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            subscribersByTopic.TryGetValue(topic, out var subscribedTopic);
            var subscriber = subscribedTopic?.Subscriber;
            return subscriber != null ? (subscriber.NumActivePublishers, subscriber.NumPublishers) : (-1, -1);
        }

        internal int GetNumSubscribers(ISender sender)
        {
            return sender.Id != InvalidId
                   && publishers.TryGetValue(sender.Id, out var basePublisher)
                   && basePublisher != null
                ? basePublisher.NumSubscribers
                : 0;
        }

        internal override void Dispose()
        {
            Disconnect();
            AddTask(RosServerManager.DisposeAsync);
            base.Dispose();
        }
    }
}