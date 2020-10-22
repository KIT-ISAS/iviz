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

        readonly ConcurrentDictionary<Uri, TcpReceiverAsync> connectionsByUri =
            new ConcurrentDictionary<Uri, TcpReceiverAsync>();

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
            catch (Exception e) when (e is TimeoutException)
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
            catch (Exception e) when (e is TimeoutException || e is AggregateException)
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
            HashSet<Uri> keys = new HashSet<Uri>(connectionsByUri.Keys);
            Uri[] toAdd = publisherUris.Where(uri => uri != null && !keys.Contains(uri)).ToArray();
            bool[] results = await Task.WhenAll(toAdd.Select(uri => AddPublisherAsync(caller.CreateTalker(uri)))).Caf();

            if (results.Any())
            {
                NumConnectionsChanged?.Invoke();
            }

            Cleanup();
        }

        public void PublisherUpdateRpc(RosClient caller, IEnumerable<Uri> publisherUris)
        {
            HashSet<Uri> keys = new HashSet<Uri>(connectionsByUri.Keys);
            IEnumerable<Uri> toAdd = publisherUris.Where(uri => uri != null && !keys.Contains(uri));
            IEnumerable<bool> results = toAdd.Select(uri => AddPublisher(caller.CreateTalker(uri)));

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
                Logger.Log($"{this}: Removing connection with '{receiver.RemoteUri}' - dead x_x");
                connectionsByUri.TryRemove(receiver.RemoteUri, out _);
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