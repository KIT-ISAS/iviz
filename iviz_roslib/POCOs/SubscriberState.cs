namespace Iviz.RoslibSharp
{
    public class SubscriberReceiverState
    {
        public bool Alive { get; }
        public bool RequestNoDelay { get; }
        public string Hostname { get; }
        public int Port { get; }
        public string RemoteUri { get; }
        public string RemoteHostname { get; }
        public int RemotePort { get; }
        public int NumReceived { get; }
        public int BytesReceived { get; }

        internal SubscriberReceiverState(bool alive, bool requestNoDelay,
            string hostname, int port,
            string remoteUri, string remoteHostname, int remotePort,
            int numReceived, int bytesReceived)
        {
            Alive = alive;
            RequestNoDelay = requestNoDelay;
            Hostname = hostname;
            Port = port;
            RemoteUri = remoteUri;
            RemoteHostname = remoteHostname;
            RemotePort = remotePort;
            NumReceived = numReceived;
            BytesReceived = bytesReceived;
        }

    }

    public class SubscriberTopicState
    {
        public string Topic { get; }
        public string Type { get; }
        public string[] TopicIds { get; }
        public SubscriberReceiverState[] Receivers { get; }

        public SubscriberTopicState(string topic, string type, string[] topicIds, SubscriberReceiverState[] receivers)
        {
            Topic = topic;
            Type = type;
            TopicIds = topicIds;
            Receivers = receivers;
        }
    }

    public class SubscriberState
    {
        public SubscriberTopicState[] Topics { get; }

        internal SubscriberState(SubscriberTopicState[] topics)
        {
            Topics = topics;
        }
    }
}
