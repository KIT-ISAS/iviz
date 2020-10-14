using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Iviz.Roslib
{
    public class PublisherSenderState : JsonToString
    {
        public bool IsAlive { get; }
        public bool Latching { get; }
        public SenderStatus Status { get; }
        public Endpoint Endpoint { get; }
        public string RemoteId { get; }
        public Endpoint RemoteEndpoint { get; }
        public int CurrentQueueSize { get; }
        public int MaxQueueSize { get; }
        public int NumSent { get; }
        public int BytesSent { get; }
        public int NumDropped { get; }
        public int BytesDropped { get; }

        internal PublisherSenderState(bool isAlive,
                           bool latching,
                           SenderStatus status,
                           Endpoint endpoint,
                           string remoteId,
                           Endpoint remoteEndpoint,
                           int currentQueueSize,
                           int maxQueueSize,
                           int numSent,
                           int bytesSent,
                           int numDropped, 
                           int bytesDropped)
        {
            IsAlive = isAlive;
            Latching = latching;
            Status = status;
            Endpoint = endpoint;
            RemoteId = remoteId;
            RemoteEndpoint = remoteEndpoint;
            CurrentQueueSize = currentQueueSize;
            MaxQueueSize = maxQueueSize;
            NumSent = numSent;
            BytesSent = bytesSent;
            NumDropped = numDropped;
            BytesDropped = bytesDropped;
        }
    }

    public class PublisherTopicState : JsonToString
    {
        public string Topic { get; }
        public string Type { get; }
        public ReadOnlyCollection<string> TopicIds { get; }
        public ReadOnlyCollection<PublisherSenderState> Senders { get; }

        internal PublisherTopicState(string topic, string type, IList<string> topicIds, IList<PublisherSenderState> senders)
        {
            Topic = topic;
            Type = type;
            TopicIds = topicIds.AsReadOnly();
            Senders = senders.AsReadOnly();
        }
    }

    public class PublisherState : JsonToString
    {
        public ReadOnlyCollection<PublisherTopicState> Topics { get; }

        internal PublisherState(IList<PublisherTopicState> topics)
        {
            Topics = topics.AsReadOnly();
        }
    }
}
