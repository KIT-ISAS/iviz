namespace Iviz.RoslibSharp
{
    public class PublisherSenderState
    {
        public bool Alive { get; }
        public bool Latching { get; }
        public SenderStatus Status { get; }
        public string Hostname { get; }
        public int Port { get; }
        public string RemoteId { get; }
        public string RemoteHostname { get; }
        public int RemotePort { get; }
        public int CurrentQueueSize { get; }
        public int MaxQueueSize { get; }
        public int NumSent { get; }
        public int BytesSent { get; }
        public int NumDropped { get; }

        internal PublisherSenderState(bool alive,
                           bool latching,
                           SenderStatus status,
                           string hostname,
                           int port,
                           string remoteId,
                           string remoteHostname,
                           int remotePort,
                           int currentQueueSize,
                           int maxQueueSize,
                           int numSent,
                           int bytesSent,
                           int numDropped)
        {
            Alive = alive;
            Latching = latching;
            Status = status;
            Hostname = hostname;
            Port = port;
            RemoteId = remoteId;
            RemoteHostname = remoteHostname;
            RemotePort = remotePort;
            CurrentQueueSize = currentQueueSize;
            MaxQueueSize = maxQueueSize;
            NumSent = numSent;
            BytesSent = bytesSent;
            NumDropped = numDropped;
        }
    }

    public class PublisherTopicState
    {
        public string Topic { get; }
        public string Type { get; }
        public string[] TopicIds { get; }
        public PublisherSenderState[] Senders { get; }

        internal PublisherTopicState(string topic, string type, string[] topicIds, PublisherSenderState[] senders)
        {
            Topic = topic;
            Type = type;
            TopicIds = topicIds;
            Senders = senders;
        }
    }

    public class PublisherState : JsonToString
    {
        public PublisherTopicState[] Topics { get; }

        internal PublisherState(PublisherTopicState[] topics)
        {
            Topics = topics;
        }
    }
}
