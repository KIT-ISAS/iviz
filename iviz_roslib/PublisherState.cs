namespace Iviz.RoslibSharp
{
    public class PublisherState : JsonToString
    {
        public class SenderState
        {
            public readonly bool alive;
            public bool latching;
            public readonly SenderStatus status;
            public readonly string hostname;
            public readonly int port;
            public readonly string remoteId;
            public readonly string remoteHostname;
            public readonly int remotePort;
            public readonly int currentQueueSize;
            public readonly int maxQueueSize;
            public readonly int numSent;
            public readonly int bytesSent;
            public readonly int numDropped;

            public SenderState(bool alive, bool latching, SenderStatus status, string hostname,
                int port, string remoteId, string remoteHostname, int remotePort,
                int currentQueueSize, int maxQueueSize, int numSent, int bytesSent, int numDropped)
            {
                this.alive = alive;
                this.latching = latching;
                this.status = status;
                this.hostname = hostname;
                this.port = port;
                this.remoteId = remoteId;
                this.remoteHostname = remoteHostname;
                this.remotePort = remotePort;
                this.currentQueueSize = currentQueueSize;
                this.maxQueueSize = maxQueueSize;
                this.numSent = numSent;
                this.bytesSent = bytesSent;
                this.numDropped = numDropped;
            }
        }

        public readonly struct TopicState
        {
            public readonly string topic;
            public readonly string type;
            public readonly string[] topicIds;
            public readonly SenderState[] senders;

            public TopicState(string topic, string type, string[] topicIds, SenderState[] senders)
            {
                this.topic = topic;
                this.type = type;
                this.topicIds = topicIds;
                this.senders = senders;
            }
        }

        public readonly TopicState[] topics;

        public PublisherState(TopicState[] topics)
        {
            this.topics = topics;
        }
    }
}
