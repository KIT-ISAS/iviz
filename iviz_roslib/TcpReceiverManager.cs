using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.XmlRpc;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    internal sealed class TcpReceiverManager
    {
        const int DefaultTimeoutInMs = 5000;

        static readonly string[][] SupportedProtocols = {new[] {"TCPROS"}};

        readonly AsyncLock mutex = new AsyncLock();
        readonly Dictionary<Uri, TcpReceiverAsync> connectionsByUri = new Dictionary<Uri, TcpReceiverAsync>();

        public TcpReceiverManager(TopicInfo topicInfo, bool requestNoDelay)
        {
            TopicInfo = topicInfo;
            RequestNoDelay = requestNoDelay;
        }

        internal RosSubscriber Subscriber { private get; set; }
        public TopicInfo TopicInfo { get; }
        public string Topic => TopicInfo.Topic;
        public string CallerId => TopicInfo.CallerId;
        public string TopicType => TopicInfo.Type;

        public int NumConnections
        {
            get
            {
                using IDisposable @lock = mutex.Lock();
                Cleanup();
                return connectionsByUri.Count;
            }
        }

        public bool RequestNoDelay { get; }
        public int TimeoutInMs { get; set; } = DefaultTimeoutInMs;

        public event Action NumConnectionsChanged;

        async Task<bool> AddPublisherAsync(NodeClient talker)
        {
            Uri remoteUri = talker.Uri;
            NodeClient.RequestTopicResponse response;
            try
            {
                response = await talker.RequestTopicAsync(Topic, SupportedProtocols).Caf();
            }
            catch (Exception e) when (e is TimeoutException || e is XmlRpcException)
            {
                Logger.LogDebug($"{this}: Failed to add publisher {remoteUri}: {e}");
                return false;
            }
            catch (Exception e)
            {
                Logger.LogError($"{this}: Failed to add publisher {remoteUri}: {e}");
                return false;
            }

            if (!response.IsValid || response.Protocol.Type == null)
            {
                Logger.LogDebug(
                    $"{this}: Topic request to {response.Protocol.Hostname}:{response.Protocol.Port} has failed");
                return false;
            }

            CreateConnection(response, remoteUri);
            return true;
        }

        bool AddPublisher(NodeClient talker)
        {
            Uri remoteUri = talker.Uri;
            NodeClient.RequestTopicResponse response;
            try
            {
                response = talker.RequestTopic(Topic, SupportedProtocols);
            }
            catch (Exception e) when (e is TimeoutException || e is AggregateException || e is XmlRpcException)
            {
                Logger.LogDebug($"{this}: Failed to add publisher {remoteUri}: {e}");
                return false;
            }
            catch (Exception e)
            {
                Logger.LogError($"{this}: Failed to add publisher {remoteUri}: {e}");
                return false;
            }

            if (!response.IsValid || response.Protocol.Type == null)
            {
                Logger.LogDebug(
                    $"{this}: Topic request to {response.Protocol.Hostname}:{response.Protocol.Port} has failed");
                return false;
            }

            CreateConnection(response, remoteUri);

            return true;
        }

        void CreateConnection(NodeClient.RequestTopicResponse response, Uri remoteUri)
        {
            Endpoint remoteEndpoint = new Endpoint(response.Protocol.Hostname, response.Protocol.Port);
            TcpReceiverAsync connection = new TcpReceiverAsync(remoteUri, remoteEndpoint, TopicInfo,
                Subscriber.MessageCallback, RequestNoDelay);

            connectionsByUri[remoteUri] = connection;

            connection.Start(TimeoutInMs);
        }

        public async Task PublisherUpdateRpcAsync(RosClient caller, IEnumerable<Uri> publisherUris)
        {
            using IDisposable @lock = await mutex.LockAsync();
            
            HashSet<Uri> newPublishers = new HashSet<Uri>(publisherUris);
            IEnumerable<Uri> toAdd = newPublishers.Where(uri => uri != null && !connectionsByUri.ContainsKey(uri));

            // if an uri is not registered as a publisher anymore,
            // we kill existing receivers only if they are still trying to reconnect
            // existing sessions should continue
            IEnumerable<TcpReceiverAsync> toDelete = connectionsByUri
                .Where(pair => !newPublishers.Contains(pair.Key) /*&& !pair.Value.IsConnected*/)
                .Select(pair => pair.Value).ToArray();

            //Logger.Log(this + " old: " + string.Join(",", connectionsByUri.Keys) + " new: " +
            //           string.Join(",", newPublishers) + " todie: " + string.Join(",", toDelete));

            foreach (TcpReceiverAsync receiver in toDelete)
            {
                //Logger.Log(this + " disposing: " + receiver);
                receiver.Dispose();
            }

            bool[] results = await Task.WhenAll(toAdd.Select(uri => AddPublisherAsync(caller.CreateTalker(uri)))).Caf();

            if (results.Any())
            {
                NumConnectionsChanged?.Invoke();
            }

            //Logger.Log(this + " calling cleanup!");
            Cleanup();
        }

        public void PublisherUpdateRpc(RosClient caller, IEnumerable<Uri> publisherUris)
        {
            using IDisposable @lock = mutex.Lock();
            
            HashSet<Uri> newPublishers = new HashSet<Uri>(publisherUris);
            IEnumerable<Uri> toAdd = newPublishers.Where(uri => uri != null && !connectionsByUri.ContainsKey(uri));

            // if an uri is not registered as a publisher anymore,
            // we kill existing receivers only if they are still trying to reconnect
            // existing sessions should continue
            IEnumerable<TcpReceiverAsync> toDelete = connectionsByUri
                .Where(pair => !newPublishers.Contains(pair.Key)/* && !pair.Value.IsConnected*/)
                .Select(pair => pair.Value);

            //Logger.Log(this + " old: " + string.Join(",", connectionsByUri.Keys) + " new: " +
            //           string.Join(",", newPublishers) + " todie: " + string.Join(",", toDelete));
            
            
            foreach (TcpReceiverAsync receiver in toDelete)
            {
                receiver.Dispose();
            }

            IEnumerable<bool> results = toAdd.Select(uri => AddPublisher(caller.CreateTalker(uri))).ToArray();

            if (results.Any())
            {
                NumConnectionsChanged?.Invoke();
            }

            Cleanup();
        }

        void Cleanup()
        {
            bool publishersChanged;
            TcpReceiverAsync[] toDelete = connectionsByUri.Values.Where(receiver => !receiver.IsAlive).ToArray();
            foreach (TcpReceiverAsync receiver in toDelete)
            {
                connectionsByUri.Remove(receiver.RemoteUri);
                Logger.Log($"{this}: Removing connection with '{receiver.RemoteUri}' - dead x_x");
                receiver.Dispose();
            }

            publishersChanged = toDelete.Length != 0;
            if (publishersChanged)
            {
                NumConnectionsChanged?.Invoke();
            }
        }

        public void Stop()
        {
            using IDisposable @lock = mutex.Lock();
            connectionsByUri.Values.ForEach(x => x.Dispose());
            connectionsByUri.Clear();
            NumConnectionsChanged = null;
        }

        public ReadOnlyCollection<SubscriberReceiverState> GetStates()
        {
            return connectionsByUri.Values.Select(x => x.State).ToList().AsReadOnly();
        }

        public override string ToString()
        {
            return $"[TcpReceiverManager '{Topic}']";
        }
    }
}