namespace Iviz.RoslibSharp
{
    public class SubscriberState
    {
        public class ReceiverState
        {
            public readonly bool alive;
            public readonly bool requestNoDelay;
            public readonly string hostname;
            public readonly int port;
            public readonly string remoteUri;
            public readonly string remoteHostname;
            public readonly int remotePort;
            public readonly int numReceived;
            public readonly int bytesReceived;

            public ReceiverState(bool alive, bool requestNoDelay,
                string hostname, int port,
                string remoteUri, string remoteHostname, int remotePort,
                int numReceived, int bytesReceived)
            {
                this.alive = alive;
                this.requestNoDelay = requestNoDelay;
                this.hostname = hostname;
                this.port = port;
                this.remoteUri = remoteUri;
                this.remoteHostname = remoteHostname;
                this.remotePort = remotePort;
                this.numReceived = numReceived;
                this.bytesReceived = bytesReceived;
            }
        }

        public readonly struct TopicState
        {
            public readonly string topic;
            public readonly string type;
            public readonly string[] topicIds;
            public readonly ReceiverState[] receivers;

            public TopicState(string topic, string type, string[] topicIds, ReceiverState[] receivers)
            {
                this.topic = topic;
                this.type = type;
                this.topicIds = topicIds;
                this.receivers = receivers;
            }
        }

        public readonly TopicState[] topics;

        public SubscriberState(TopicState[] topics)
        {
            this.topics = topics;
        }
    }
}
