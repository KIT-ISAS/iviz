using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    internal class TcpReceiverManager
    {
        static readonly string[][] SupportedProtocols = {new[] {"TCPROS"}};

        readonly ConcurrentDictionary<Uri, TcpReceiverAsync> connectionsByUri =
            new ConcurrentDictionary<Uri, TcpReceiverAsync>();

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
        public int TimeoutInMs { get; set; } = 5000;

        public TcpReceiverManager(TopicInfo topicInfo, bool requestNoDelay)
        {
            this.TopicInfo = topicInfo;
            RequestNoDelay = requestNoDelay;
        }

        async Task<bool> AddPublisherAsync(XmlRpc.NodeClient talker)
        {
            Uri remoteUri = talker.Uri;
            XmlRpc.NodeClient.RequestTopicResponse response;
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

        bool AddPublisher(XmlRpc.NodeClient talker)
        {
            Uri remoteUri = talker.Uri;
            XmlRpc.NodeClient.RequestTopicResponse response;
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

        void CreateConnection(XmlRpc.NodeClient.RequestTopicResponse response, Uri remoteUri)
        {
            Endpoint remoteEndpoint = new Endpoint(response.Protocol.Hostname, response.Protocol.Port);
            TcpReceiverAsync connection = new TcpReceiverAsync(remoteUri, remoteEndpoint, TopicInfo,
                Subscriber.MessageCallback, RequestNoDelay);

            connectionsByUri[remoteUri] = connection;

            connection.Start(TimeoutInMs);
        }

        public async Task<bool> PublisherUpdateRpcAsync(RosClient caller, IEnumerable<Uri> publisherUris)
        {
            HashSet<Uri> keys = new HashSet<Uri>(connectionsByUri.Keys);
            Uri[] toAdd = publisherUris.Where(uri => uri != null && !keys.Contains(uri)).ToArray();
            bool[] results = await Task.WhenAll(toAdd.Select(uri => AddPublisherAsync(caller.CreateTalker(uri)))).Caf();

            bool publishersChanged = Cleanup() || results.Any();

            return publishersChanged;
        }

        public bool PublisherUpdateRpc(RosClient caller, IEnumerable<Uri> publisherUris)
        {
            HashSet<Uri> keys = new HashSet<Uri>(connectionsByUri.Keys);
            IEnumerable<Uri> toAdd = publisherUris.Where(uri => uri != null && !keys.Contains(uri));
            IEnumerable<bool> results = toAdd.Select(uri => AddPublisher(caller.CreateTalker(uri)));

            bool publishersChanged = Cleanup() || results.Any();

            return publishersChanged;
        }

        bool Cleanup()
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

            return publishersChanged;
        }

        public void Stop()
        {
            connectionsByUri.Values.ForEach(x => x.Dispose());
            connectionsByUri.Clear();
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