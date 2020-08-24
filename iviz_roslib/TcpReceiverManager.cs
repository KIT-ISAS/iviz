using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal class TcpReceiverManager
    {
        static readonly string[][] SupportedProtocols = { new[] { "TCPROS" } };

        readonly Dictionary<Uri, TcpReceiver> connectionsByUri = new Dictionary<Uri, TcpReceiver>();

        internal RosSubscriber Subscriber { private get; set; }
        
        public TopicInfo TopicInfo { get; }
        public string Topic => TopicInfo.Topic;
        public string CallerId => TopicInfo.CallerId;
        public string TopicType => TopicInfo.Type;
        public int NumConnections => connectionsByUri.Count;
        public bool RequestNoDelay { get; }
        public int TimeoutInMs { get; set; } = 5000;

        public TcpReceiverManager(TopicInfo topicInfo, bool requestNoDelay)
        {
            this.TopicInfo = topicInfo;
            RequestNoDelay = requestNoDelay;
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
            if (!response.IsValid)
            {
                Logger.LogDebug($"{this}: Topic request to {response.Protocol.Hostname}:{response.Protocol.Port} failed");
                return false;
            }
            if (response.Protocol.Type == null)
            {
                Logger.LogDebug($"{this}: {response.Protocol.Hostname}:{response.Protocol.Port} has no suitable protocols");
                return false;
            }

            TcpReceiver connection = new TcpReceiver(
                remoteUri,
                new Endpoint(response.Protocol.Hostname, response.Protocol.Port), 
                TopicInfo, Subscriber.MessageCallback,
                RequestNoDelay);

            lock (connectionsByUri)
            {
                connectionsByUri[remoteUri] = connection;
            }
            connection.Start(TimeoutInMs);
            return true;
        }

        public bool PublisherUpdateRpc(RosClient caller, IList<Uri> publisherUris)
        {
            Uri[] toAdd;
            bool publishersChanged = false;
            lock (connectionsByUri)
            {
                toAdd = publisherUris.Where(uri => uri != null && !connectionsByUri.ContainsKey(uri)).ToArray();

                HashSet<Uri> toDelete = new HashSet<Uri>(connectionsByUri.Keys);
                publisherUris.ForEach(uri => toDelete.Remove(uri));

                connectionsByUri.Where(x => !x.Value.IsAlive).ForEach(x => toDelete.Add(x.Key));

                foreach (Uri uri in toDelete)
                {
                    Logger.Log($"{this}: Removing connection with '{uri}' - dead x_x");
                    connectionsByUri[uri].Stop();
                    connectionsByUri.Remove(uri);
                }
                publishersChanged = toDelete.Any();
                
            }
            foreach (Uri uri in toAdd)
            {
                publishersChanged |= AddPublisher(caller.CreateTalker(uri));
            }

            return publishersChanged;
        }

        public bool Cleanup()
        {
            bool publishersChanged = false;
            lock (connectionsByUri)
            {
                Uri[] toDelete = connectionsByUri.
                    Where(x => !x.Value.IsAlive).
                    Select(x => x.Key).
                    ToArray();
                foreach (Uri uri in toDelete)
                {
                    Logger.Log($"{this}: Removing connection with '{uri}' - dead x_x");
                    connectionsByUri[uri].Stop();
                    connectionsByUri.Remove(uri);
                }
                publishersChanged = toDelete.Length != 0;
            }

            return publishersChanged;
        }

        public void Stop()
        {
            lock (connectionsByUri)
            {
                connectionsByUri.Values.ForEach(x => x.Stop());
                connectionsByUri.Clear();
            }
        }

        public ReadOnlyDictionary<Uri, TcpReceiver> GetConnections()
        {
            lock (connectionsByUri)
            {
                return new ReadOnlyDictionary<Uri, TcpReceiver>(connectionsByUri);
            }
        }

        public ReadOnlyCollection<SubscriberReceiverState> GetStates()
        {
            lock (connectionsByUri)
            {
                return connectionsByUri.Values.Select(x => x.State).ToList().AsReadOnly();
            }
        }

        public override string ToString()
        {
            return $"[SubscriberConnectionManager '{Topic}']";
        }
    }
}
