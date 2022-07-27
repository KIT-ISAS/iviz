#nullable enable

#define LOG_ENABLED

using System;
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
using Iviz.Roslib2;
using Iviz.XmlRpc;
using Nito.AsyncEx;
using Iviz.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Random = System.Random;

namespace Iviz.Ros
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RosVersion
    {
        ROS1,
        ROS2
    }

    public sealed class RoslibConnection : RosConnection, Iviz.Displays.IServiceProvider
    {
        static TopicNameType[] EmptyTopics => Array.Empty<TopicNameType>();
        static string[] EmptyParameters => Array.Empty<string>();
        static readonly Random Random = Defaults.Random;

        readonly IRosPublisher?[] publishers = new IRosPublisher[256];
        readonly Dictionary<string, IAdvertisedTopic> publishersByTopic = new();
        readonly Dictionary<string, IAdvertisedService> servicesByTopic = new();
        readonly Dictionary<string, ISubscribedTopic> subscribersByTopic = new();
        readonly List<(string hostname, string address)> hostAliases = new();

        string[] cachedParameters = EmptyParameters;
        TopicNameType[] cachedPublishedTopics = EmptyTopics;
        TopicNameType[] cachedTopics = EmptyTopics;
        SystemState? cachedSystemState;
        IRosClient? client;
        BagListener? bagListener;

        float? modelServiceBlacklistTime;
        readonly AsyncLock modelServiceLock = new();

        CancellationTokenSource runningTs = new();

        Task? watchdogTask;
        Task? ntpTask;
        Uri? masterUri;
        string? myId;
        Uri? myUri;
        RosVersion version = RosVersion.ROS1;

        bool Connected => client != null;

        public static event Action<RosVersion>? RosVersionChanged;

        public RosVersion RosVersion
        {
            get => version;
            set
            {
                if (version == value)
                {
                    return;
                }

                version = value;
                RosVersionChanged?.Invoke(version);
                RosLogger.Internal("ROS version changed to " + (version == RosVersion.ROS1 ? "ROS1" : "ROS2"));

                Disconnect();
            }
        }

        public IRosClient Client => client ?? throw new InvalidOperationException("Client not connected");

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
                Post(async () =>
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

            runningTs.Cancel();
            runningTs = new CancellationTokenSource();
            await Client.DisposeAsync(runningTs.Token).AwaitNoThrow(this);
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
                var currentVersion = version;
                const int rpcTimeoutInMs = 3000;

                //Tools.Logger.LogDebugCallback = RosLogger.Debug;
                if (Settings.IsStandalone)
                {
                    Tools.Logger.LogErrorCallback = RosLogger.Debug;
                    Tools.Logger.LogCallback = RosLogger.Debug;
                }
                else
                {
                    Tools.Logger.LogErrorCallback = RosLogger.Error;
                    Tools.Logger.LogCallback = RosLogger.Info;
                }

                RosLogger.Internal("Connecting...");

                runningTs.Cancel();
                runningTs = new CancellationTokenSource();

                var token = runningTs.Token;

                if (currentVersion == RosVersion.ROS1)
                {
                    var newClient = await RosClient.CreateAsync(MasterUri, MyId, MyUri, token: token);
                    newClient.ShutdownAction = OnShutdown;
                    await newClient.CheckOwnUriAsync(token);
                    newClient.RosMasterClient.TimeoutInMs = rpcTimeoutInMs;
                    client = newClient;
                }
                else
                {
                    client = new Ros2Client(MyId);
                }

                var currentClient = client;

                Post(async () =>
                {
                    RosLogger.Internal("Resubscribing and readvertising...");
                    token.ThrowIfCancellationRequested();

                    (bool success, XmlRpcValue hosts) = await currentClient.GetParameterAsync("/iviz/hosts", token);
                    if (success)
                    {
                        AddHostsParamFromArg(hosts);
                    }

                    AddConfigHostAliases();

                    //RosLogger.Debug("--- Advertising services...");
                    token.ThrowIfCancellationRequested();
                    await servicesByTopic.Values
                        .Select(topic => ReAdvertiseService(currentClient, topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    //RosLogger.Debug("+++ Done advertising services");

                    //RosLogger.Debug("--- Readvertising...");
                    token.ThrowIfCancellationRequested();
                    await publishersByTopic.Values
                        .Select(topic => ReAdvertise(currentClient, topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    //RosLogger.Debug("+++ Done readvertising");

                    //RosLogger.Debug("--- Resubscribing...");
                    token.ThrowIfCancellationRequested();
                    await subscribersByTopic.Values
                        .Select(topic => ReSubscribe(currentClient, topic, token).AwaitNoThrow(this))
                        .WhenAll();
                    //RosLogger.Debug("+++ Done resubscribing");

                    //RosLogger.Debug("--- Requesting topics...");
                    token.ThrowIfCancellationRequested();
                    cachedPublishedTopics = await currentClient.GetSystemPublishedTopicsAsync(token);
                    //RosLogger.Debug("+++ Done requesting topics");

                    cachedSystemState = null;
                    cachedTopics = EmptyTopics;

                    RosLogger.Internal("Finished resubscribing and readvertising!");

                    if (currentVersion == RosVersion.ROS1)
                    {
                        watchdogTask = WatchdogTask(((RosClient)currentClient).RosMasterClient, token);
                        ntpTask = NtpCheckerTask(MasterUri.Host, token);
                    }
                });

                RosLogger.Debug($"{this}: Connected!");
                RosLogger.Internal("<b>Connected!</b>");

                if (currentVersion == RosVersion.ROS1)
                {
                    LogConnectionCheck(token);
                }

                return true;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case RosUnreachableUriException:
                        RosLogger.Internal($"<b>Connection failed:</b> Cannot reach my own URI. Reason: {e.Message}");
                        break;
                    case RosUriBindingException:
                        RosLogger.Internal(
                            $"<b>Error:</b> Port {MyUri?.Port.ToString()} is already being used by another application. " +
                            $"Maybe another iviz instance is running? Try another port!");
                        break;
                    case RoslibException:
                    case TimeoutException:
                    case XmlRpcException:
                    {
                        RosLogger.Internal("<b>Connection failed:</b>", e);
                        break;
                    }
                    case OperationCanceledException:
                        RosLogger.Info($"{this}: Connection cancelled!");
                        break;
                    default:
                        RosLogger.Error($"{this}: Exception during {nameof(ConnectAsync)}(): ", e);
                        break;
                }
            }

            await DisconnectCore();
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

        void OnShutdown(string id, string reason)
        {
            Post(() =>
            {
                RosLogger.Internal($"Received <b>kill signal</b> from node '{id}'. Reason: {reason}");
                KeepReconnecting = false;
                Disconnect();
                return default;
            });
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

            if (RosManager.Logger.Sender.NumSubscribers == 0)
            {
                RosLogger.Internal("<b>Warning:</b> Our logger has no subscriptions yet. " +
                                   "Maybe /rosout hasn't seen us yet. " +
                                   "But it also may be that outside nodes cannot connect to us, for example due to a firewall.");
            }
            else
            {
                return;
            }

            try
            {
                await Task.Delay(5000, token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            if (RosManager.Logger.Sender.NumSubscribers != 0)
            {
                RosLogger.Internal("Never mind, /rosout just saw us.");
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
            var connection = RosManager.Connection;


            SetConnectionWarningState(false);
            try
            {
                for (; !token.IsCancellationRequested; await Task.Delay(delayBetweenPingsInMs, token))
                {
                    var now = GameThread.Now;
                    LookupNodeResponse response;
                    try
                    {
                        // check if the master is responding to requests.
                        // the node name does not matter. 
                        response = await masterApi.LookupNodeAsync("/rosout", token);
                    }
                    catch
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
            if (Mathf.Abs((float)offset.TotalMilliseconds) < minOffsetInMs)
            {
                RosLogger.Info("[NtpChecker]: No significant time offset detected from master clock.");
            }
            else
            {
                time.GlobalTimeOffset = offset;
                string offsetStr = Mathf.Abs((float)offset.TotalSeconds) >= 1
                    ? $"{offset.TotalSeconds:#,0.###} sec"
                    : $"{offset.TotalMilliseconds:#,0.###} ms";
                RosLogger.Info($"[NtpChecker]: Master clock appears to have a time offset of {offsetStr}. " +
                               "Local published messages will use this offset.");
            }
        }

        int GetFreeId()
        {
            for (int i = 0; i < publishers.Length; i++)
            {
                if (publishers[i] == null)
                {
                    return i;
                }
            }

            throw new InvalidOperationException("Ran out of publishers!"); // NYI!
        }

        static Task RandomDelay(CancellationToken token) => Task.Delay(Random.Next(0, 100), token);

        async ValueTask ReAdvertise(IRosClient newClient, IAdvertisedTopic topic, CancellationToken token)
        {
            await RandomDelay(token);
            await topic.AdvertiseAsync(newClient, token);
            int id = GetFreeId();
            topic.Id = id;
            publishers[id] = topic.Publisher;
        }

        static async ValueTask ReSubscribe(IRosClient newClient, ISubscribedTopic topic, CancellationToken token)
        {
            await RandomDelay(token);
            await topic.SubscribeAsync(newClient, token: token);
        }

        static async ValueTask ReAdvertiseService(IRosClient newClient, IAdvertisedService service,
            CancellationToken token)
        {
            await RandomDelay(token);
            await service.AdvertiseAsync(newClient, token);
        }

        public override void Disconnect()
        {
            runningTs.Cancel();

            if (!Connected)
            {
                Signal();
                return;
            }

            Post(DisconnectCore);
        }

        async ValueTask DisconnectCore()
        {
            if (!Connected)
            {
                return;
            }

            foreach (var entry in publishersByTopic.Values)
            {
                entry.Invalidate();
            }

            foreach (var entry in subscribersByTopic.Values)
            {
                entry.Invalidate();
            }

            publishers.AsSpan().Fill(null);

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

        internal void Advertise<T>(Sender<T> advertiser) where T : IMessage, new()
        {
            ThrowHelper.ThrowIfNull(advertiser, nameof(advertiser));
            advertiser.Id = null;
            CancellationToken token = runningTs.Token;
            Post(async () =>
            {
                try
                {
                    await AdvertiseCore(advertiser, token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(Advertise)}()", e);
                }
            });
        }

        async ValueTask AdvertiseCore<T>(Sender<T> advertiser, CancellationToken token) where T : IMessage, new()
        {
            if (publishersByTopic.TryGetValue(advertiser.Topic, out var advertisedTopic))
            {
                advertisedTopic.Add(advertiser);
                advertiser.Id = advertisedTopic.Id;
                return;
            }

            RosLogger.Info($"{this}: Advertising {advertiser.Topic} [{advertiser.Type}].");

            var newAdvertisedTopic = new AdvertisedTopic<T>(advertiser.Topic);

            int? id;
            if (Connected)
            {
                int newId = GetFreeId();

                await newAdvertisedTopic.AdvertiseAsync(Client, token);
                var publisher = newAdvertisedTopic.Publisher;
                if (publisher == null)
                {
                    RosLogger.Error($"{this}: Failed to advertise topic '{advertiser.Topic}'");
                    return;
                }

                publishers[newId] = publisher;
                id = newId;
            }

            else
            {
                id = null;
            }

            newAdvertisedTopic.Id = id;
            publishersByTopic.Add(advertiser.Topic, newAdvertisedTopic);
            newAdvertisedTopic.Add(advertiser);
            advertiser.Id = newAdvertisedTopic.Id;
        }

        public void AdvertiseService<T>(string service, Action<T> callback) where T : IService, new()
        {
            ThrowHelper.ThrowIfNull(service, nameof(service));
            ThrowHelper.ThrowIfNull(callback, nameof(callback));

            AdvertiseService(service, (T t) =>
            {
                callback(t);
                return default;
            });
        }

        public void AdvertiseService<T>(string service, Func<T, ValueTask> callback) where T : IService, new()
        {
            ThrowHelper.ThrowIfNull(service, nameof(service));
            ThrowHelper.ThrowIfNull(callback, nameof(callback));

            var token = runningTs.Token;
            Post(async () =>
            {
                try
                {
                    await AdvertiseServiceCore(service, callback, token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(AdvertiseService)}()", e);
                }
            });
        }

        async ValueTask AdvertiseServiceCore<T>(string serviceName, Func<T, ValueTask> callback,
            CancellationToken token)
            where T : IService, new()
        {
            if (servicesByTopic.TryGetValue(serviceName, out var baseService)
                && baseService.TrySetCallback(callback))
            {
                return;
            }

            RosLogger.Info($"{this}: Advertising service {serviceName} [{BuiltIns.GetServiceType<T>()}].");

            var newAdvertisedService = new AdvertisedService<T>(serviceName, callback);
            servicesByTopic.Add(serviceName, newAdvertisedService);
            if (Connected)
            {
                await newAdvertisedService.AdvertiseAsync(Client, token);
            }
        }

        public async ValueTask<bool> CallModelServiceAsync<T>(string service, T srv, int timeoutInMs,
            CancellationToken token) where T : IService, new()
        {
            using var myLock = await modelServiceLock.LockAsync();

            float currentTime = GameThread.GameTime;
            if (modelServiceBlacklistTime is { } blacklistTime && currentTime < blacklistTime)
            {
                return false;
            }

            modelServiceBlacklistTime = null;
            try
            {
                return await CallServiceAsync(service, srv, timeoutInMs, token);
            }
            catch (RoslibException e)
            {
                modelServiceBlacklistTime = currentTime + 5;
                throw new NoModelLoaderServiceException("Failed to reach the iviz model loader service", e);
            }
        }

        async ValueTask<bool> CallServiceAsync<T>(string service, T srv, int timeoutInMs,
            CancellationToken token) where T : IService, new()
        {
            ThrowHelper.ThrowIfNull(service, nameof(service));
            ThrowHelper.ThrowIfNull(srv, nameof(srv));

            if (!Connected || runningTs.IsCancellationRequested)
            {
                return false;
            }

            token.ThrowIfCancellationRequested();

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
            tokenSource.CancelAfter(timeoutInMs);
            try
            {
                await Client.CallServiceAsync(service, srv, true, tokenSource.Token);
                return true;
            }
            catch (OperationCanceledException e) when (!token.IsCancellationRequested &&
                                                       !runningTs.IsCancellationRequested)
            {
                throw new TimeoutException($"Service call to '{service}' timed out", e);
            }
        }

        internal void Publish<T>(Sender<T> advertiser, in T msg) where T : IMessage, new()
        {
            if (advertiser.Id is not { } id || runningTs.IsCancellationRequested)
            {
                return;
            }

            var basePublisher = publishers[id];
            if (basePublisher == null || basePublisher.NumSubscribers == 0)
            {
                return;
            }

            if (basePublisher is not IRosPublisher<T> publisher)
            {
                RosLogger.Error($"{this}: Publisher type does not match! Message is " + typeof(T).Name +
                                ", publisher is " + basePublisher.GetType().Name);
                return;
            }

            try
            {
                publisher.Publish(msg);
            }
            catch (OperationCanceledException)
            {
                // ignore
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during {nameof(Publish)}()", e);
            }
        }

        internal void Subscribe<T>(Listener<T> listener) where T : IMessage, IDeserializable<T>, new()
        {
            ThrowHelper.ThrowIfNull(listener, nameof(listener));

            var token = runningTs.Token;
            Post(async () =>
            {
                try
                {
                    await SubscribeImpl<T>(listener, token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception e)
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

            RosLogger.Info($"{this}: Subscribing to {listener.Topic} [{listener.Type}].");

            var newSubscribedTopic = new SubscribedTopic<T>(listener.Topic, listener.TransportHint);
            subscribersByTopic.Add(listener.Topic, newSubscribedTopic);
            await newSubscribedTopic.SubscribeAsync(Connected ? Client : null, listener, token);
        }

        internal void SetPause(IListener listener, bool value)
        {
            ThrowHelper.ThrowIfNull(listener, nameof(listener));

            Post(() =>
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
            ThrowHelper.ThrowIfNull(advertiser, nameof(advertiser));

            var token = runningTs.Token;
            Post(async () =>
            {
                try
                {
                    await UnadvertiseCore(advertiser, token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(Unadvertise)}()", e);
                }
            });
        }

        ValueTask UnadvertiseCore(ISender advertiser, CancellationToken token)
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
            if (advertiser.Id is { } id)
            {
                publishers[id] = null;
            }

            return Connected
                ? advertisedTopic.UnadvertiseAsync(token)
                : default;
        }

        internal void Unsubscribe(IListener subscriber)
        {
            ThrowHelper.ThrowIfNull(subscriber, nameof(subscriber));

            var token = runningTs.Token;
            Post(async () =>
            {
                try
                {
                    await UnsubscribeCore(subscriber, token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(Unsubscribe)}()", e);
                }
            });
        }

        ValueTask UnsubscribeCore(IListener subscriber, CancellationToken token)
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
            ThrowHelper.ThrowIfNull(service, nameof(service));

            var token = runningTs.Token;
            Post(async () =>
            {
                try
                {
                    await UnadvertiseServiceCore(service, token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(UnadvertiseService)}()", e);
                }
            });
        }

        async ValueTask UnadvertiseServiceCore(string serviceName, CancellationToken token)
        {
            if (!servicesByTopic.TryGetValue(serviceName, out var service))
            {
                return;
            }

            RosLogger.Info($"{this}: Unadvertising service {serviceName}.");

            if (Connected)
            {
                await service.UnadvertiseAsync(Client, token);
            }

            servicesByTopic.Remove(serviceName);
        }

        public TopicNameType[] GetSystemPublishedTopicTypes(
            RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedButRequestInBackground)
            {
                TaskUtils.Run(() => GetSystemPublishedTopicTypesAsync().AsTask(), runningTs.Token);
            }

            return cachedPublishedTopics;
        }

        public async ValueTask<TopicNameType[]> GetSystemPublishedTopicTypesAsync(int timeoutInMs = 2000,
            CancellationToken token = default)
        {
            if (!Connected || token.IsCancellationRequested || runningTs.Token.IsCancellationRequested)
            {
                cachedPublishedTopics = EmptyTopics;
                return EmptyTopics;
            }

            try
            {
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                cachedPublishedTopics = await Client.GetSystemPublishedTopicsAsync(tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // ignore
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during {nameof(GetSystemPublishedTopicTypesAsync)}()", e);
            }

            return cachedPublishedTopics;
        }

        public TopicNameType[] GetSystemTopicTypes(RequestType type = RequestType.CachedButRequestInBackground)
        {
            if (type == RequestType.CachedButRequestInBackground)
            {
                TaskUtils.Run(() => GetSystemTopicTypesAsync().AsTask(), runningTs.Token);
            }

            return cachedTopics;
        }

        async ValueTask GetSystemTopicTypesAsync(int timeoutInMs = 2000, CancellationToken token = default)
        {
            if (!Connected || token.IsCancellationRequested || runningTs.Token.IsCancellationRequested)
            {
                cachedTopics = EmptyTopics;
                return;
            }

            try
            {
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                cachedTopics = await Client.GetSystemTopicsAsync(tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // ignore
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during {nameof(GetSystemTopicTypesAsync)}()", e);
            }
        }

        public string[] GetSystemParameterList(CancellationToken token = default)
        {
            var internalToken = runningTs.Token;
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
                    cachedParameters = await Client.GetParameterNamesAsync(tokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Exception during {nameof(GetSystemParameterList)}()", e);
                }
            }, internalToken);

            return cachedParameters;
        }

        public async ValueTask<(XmlRpcValue result, string? errorMsg)> GetParameterAsync(string parameter,
            int timeoutInMs, CancellationToken token = default)
        {
            ThrowHelper.ThrowIfNull(parameter, nameof(parameter));

            if (token.IsCancellationRequested || runningTs.Token.IsCancellationRequested)
            {
                return (default, "Cancellation requested");
            }

            if (!Connected)
            {
                return (default, "Not connected");
            }

            try
            {
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                (bool success, XmlRpcValue param) =
                    await Client.GetParameterAsync(parameter, tokenSource.Token);
                if (!success)
                {
                    return (default, $"'{parameter}' not found");
                }

                return (param, null);
            }
            catch (OperationCanceledException)
            {
                return token.IsCancellationRequested || runningTs.IsCancellationRequested
                    ? (default, "Operation cancelled")
                    : (default, "Operation timed out");
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
                TaskUtils.Run(() => GetSystemStateAsync().AsTask(), runningTs.Token);
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
                using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token);
                tokenSource.CancelAfter(timeoutInMs);
                cachedSystemState = await Client.GetSystemStateAsync(tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // ignore
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during {nameof(GetSystemStateAsync)}()", e);
            }
        }

        public int GetNumPublishers(string topic)
        {
            ThrowHelper.ThrowIfNull(topic, nameof(topic));

            subscribersByTopic.TryGetValue(topic, out var subscribedTopic);
            return subscribedTopic?.Subscriber?.NumPublishers ?? -1;
        }

        internal int GetNumSubscribers(ISender sender)
        {
            return sender.Id is { } id
                   && publishers[id] is { } basePublisher
                ? basePublisher.NumSubscribers
                : 0;
        }

        internal override void Dispose()
        {
            Disconnect();
            Post(RosManager.Server.DisposeAsync);
            base.Dispose();
        }
    }
}