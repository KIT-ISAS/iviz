using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Msgs;

namespace Iviz.RoslibSharp
{
    class TcpReceiverManager
    {
        static readonly string[][] SupportedProtocols = { new[] { "TCPROS" } };

        readonly Dictionary<Uri, TcpReceiver> connectionsByUri = new Dictionary<Uri, TcpReceiver>();

        internal Action<IMessage> Callback { private get; set; }

        public readonly TopicInfo TopicInfo;
        public string Topic => TopicInfo.Topic;
        public string CallerId => TopicInfo.CallerId;
        public string TopicType => TopicInfo.Type;
        public int NumConnections => connectionsByUri.Count;
        public readonly bool RequestNoDelay;

        public TcpReceiverManager(TopicInfo topicInfo, bool requestNoDelay)
        {
            this.TopicInfo = topicInfo;
            RequestNoDelay = requestNoDelay;
        }

        void AddPublisher(XmlRpc.NodeClient talker, Uri remoteUri)
        {
            talker.Uri = remoteUri;
            XmlRpc.NodeClient.RequestTopicResponse response;
            try
            {
                response = talker.RequestTopic(Topic, SupportedProtocols);
            }
            catch (Exception e)
            {
                Logger.LogError($"{this}: Failed to add publisher {remoteUri}: {e.Message}");
                Logger.LogError($"{this}: " + e.StackTrace);
                return;
            }
            if (response.protocol.type == null)
            {
                Logger.Log($"{this}: {response.protocol.hostname}:{response.protocol.port} has no suitable protocols");
                return;
            }

            TcpReceiver connection = new TcpReceiver(
                remoteUri,
                response.protocol.hostname,
                response.protocol.port,
                TopicInfo, Callback,
                RequestNoDelay);

            lock (connectionsByUri)
            {
                connectionsByUri[remoteUri] = connection;
            }
            connection.Start();
        }

        public void PublisherUpdateRpc(XmlRpc.NodeClient talker, Uri[] publisherUris)
        {
            Uri[] toAdd;
            lock (connectionsByUri)
            {
                toAdd = publisherUris.Where(uri => !connectionsByUri.ContainsKey(uri)).ToArray();

                HashSet<Uri> toDelete = new HashSet<Uri>(connectionsByUri.Keys);
                publisherUris.ForEach(uri => toDelete.Remove(uri));

                connectionsByUri.Where(x => !x.Value.IsAlive).ForEach(x => toDelete.Add(x.Key));
                foreach (Uri uri in toDelete)
                {
                    connectionsByUri[uri].Stop();
                    connectionsByUri.Remove(uri);
                }
            }
            foreach (Uri uri in toAdd)
            {
                AddPublisher(talker, uri);
            }
        }

        public void Cleanup()
        {
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
            }
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
                return new ReadOnlyCollection<SubscriberReceiverState>(
                    connectionsByUri.Values.Select(x => x.State).ToList()
                    );
            }
        }

        public override string ToString()
        {
            return $"[SubscriberConnectionManager '{Topic}']";
        }
    }
}
