using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.Roslib.XmlRpc;
using Iviz.XmlRpc;
using Nito.AsyncEx;

namespace Iviz.Roslib
{
    internal sealed class TcpReceiverManager<T> where T : IMessage
    {
        const int DefaultTimeoutInMs = 5000;

        readonly AsyncLock mutex = new();
        readonly ConcurrentDictionary<Uri, TcpReceiverAsync<T>> connectionsByUri = new();
        readonly RosClient client;
        readonly RosSubscriber<T> subscriber;
        readonly TopicInfo<T> topicInfo;

        bool isPaused;

        public bool IsPaused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                foreach (var receiver in connectionsByUri.Values)
                {
                    receiver.IsPaused = value;
                }
            }
        }

        public TcpReceiverManager(RosSubscriber<T> subscriber, RosClient client, TopicInfo<T> topicInfo,
            bool requestNoDelay)
        {
            this.subscriber = subscriber;
            this.client = client;
            this.topicInfo = topicInfo;
            RequestNoDelay = requestNoDelay;
        }

        public string Topic => topicInfo.Topic;
        public string TopicType => topicInfo.Type;

        HashSet<Uri> allPublisherUris = new();

        public int NumConnections => connectionsByUri.Count;

        public int NumActiveConnections => connectionsByUri.Count(pair => pair.Value.IsConnected);

        public bool RequestNoDelay { get; }
        public int TimeoutInMs { get; set; } = DefaultTimeoutInMs;

        internal async ValueTask<Endpoint?> RequestConnectionFromPublisherAsync(Uri remoteUri, CancellationToken token)
        {
            RosNodeClient.RequestTopicResponse response;
            try
            {
                response = await client.CreateTalker(remoteUri).RequestTopicAsync(Topic, token);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case OperationCanceledException:
                        return null;
                    case TimeoutException:
                    case XmlRpcException:
                    case SocketException:
                    case IOException:
                        Logger.LogDebugFormat("{0}: Connection request to publisher {1} failed: {2}",
                            this, remoteUri, e);
                        return null;
                    default:
                        Logger.LogErrorFormat("{0}: Connection request to publisher {1} failed: {2}",
                            this, remoteUri, e);
                        return null;
                }
            }

            if (!response.IsValid || response.Protocol == null)
            {
                Logger.LogDebugFormat("{0}: Connection request to publisher {1} failed: {2}",
                    this, remoteUri, response.StatusMessage);
                return null;
            }

            if (string.IsNullOrEmpty(response.Protocol.Hostname) || response.Protocol.Port == 0)
            {
                Logger.LogErrorFormat("{0}: Connection request to publisher {1} returned an uninitialized address!",
                    this, remoteUri);
                return null;
            }

            return new Endpoint(response.Protocol.Hostname, response.Protocol.Port);
        }

        internal void MessageCallback(in T msg, IRosTcpReceiver receiver)
        {
            subscriber.MessageCallback(msg, receiver);
        }
        
        void CreateConnection(Uri remoteUri, Endpoint? remoteEndpointHint = null)
        {
            connectionsByUri[remoteUri] =
                new TcpReceiverAsync<T>(this, remoteUri, remoteEndpointHint, topicInfo, RequestNoDelay, TimeoutInMs)
                    {IsPaused = IsPaused};
        }

        public async Task PublisherUpdateRpcAsync(IEnumerable<Uri> publisherUris, CancellationToken token)
        {
            bool numConnectionsHasChanged = false;

            using (await mutex.LockAsync(token))
            {
                var newPublishers = new HashSet<Uri>(publisherUris);
                allPublisherUris = newPublishers;

                if (connectionsByUri.Keys.Any(key => !newPublishers.Contains(key)))
                {
                    TcpReceiverAsync<T>[] toDelete = connectionsByUri
                        .Where(pair => !newPublishers.Contains(pair.Key))
                        .Select(pair => pair.Value)
                        .ToArray();

                    var deleteTasks = toDelete.Select(receiver => receiver.DisposeAsync(token));
                    await deleteTasks.WhenAll().AwaitNoThrow(this);
                }

                if (newPublishers.Any(uri => !connectionsByUri.ContainsKey(uri)))
                {
                    IEnumerable<Uri> toAdd =
                        newPublishers.Where(uri => uri != null && !connectionsByUri.ContainsKey(uri));
                    foreach (Uri remoteUri in toAdd)
                    {
                        CreateConnection(remoteUri);
                    }
                }

                numConnectionsHasChanged |= await CleanupAsync(token);
            }

            if (numConnectionsHasChanged)
            {
                subscriber.RaiseNumPublishersChanged();
            }
        }

        bool NeedsCleanup()
        {
            return connectionsByUri.Values.Any(receiver => !receiver.IsAlive);
        }

        async ValueTask<bool> CleanupAsync(CancellationToken token)
        {
            if (connectionsByUri.Values.All(receiver => receiver.IsAlive))
            {
                return false;
            }

            TcpReceiverAsync<T>[] toDelete = connectionsByUri.Values.Where(receiver => !receiver.IsAlive).ToArray();
            var tasks = toDelete.Select(receiver =>
            {
                connectionsByUri.TryRemove(receiver.RemoteUri, out _);
                Logger.LogDebugFormat("{0}: Removing connection with '{1}' - dead x_x", this, receiver.RemoteUri);
                return receiver.DisposeAsync(token);
            });

            await tasks.WhenAll().AwaitNoThrow(this);

            return true;
        }

        public void Stop()
        {
            Task.Run(() => StopAsync(default)).WaitNoThrow(this);
        }

        public async Task StopAsync(CancellationToken token)
        {
            TcpReceiverAsync<T>[] receivers;
            using (await mutex.LockAsync(token))
            {
                receivers = connectionsByUri.Values.ToArray();
                connectionsByUri.Clear();
            }

            await receivers.Select(receiver => receiver.DisposeAsync(token)).WhenAll();
            subscriber.RaiseNumPublishersChanged();
        }

        public ReadOnlyCollection<SubscriberReceiverState> GetStates()
        {
            var publisherUris = allPublisherUris;
            var alivePublishers = connectionsByUri.Values.Select(receiver => receiver.State);
            var missingPublishers = publisherUris
                .Where(uri => !connectionsByUri.ContainsKey(uri))
                .Select(uri => new SubscriberReceiverState(uri));

            return alivePublishers.Concat(missingPublishers).ToList().AsReadOnly();
        }

        public override string ToString()
        {
            return $"[TcpReceiverManager '{Topic}']";
        }
    }
}