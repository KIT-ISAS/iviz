using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Iviz.Roslib
{
    public class PublisherSenderState : JsonToString
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
            TopicIds = new ReadOnlyCollection<string>(topicIds);
            Senders = new ReadOnlyCollection<PublisherSenderState>(senders);
        }
    }

    public class PublisherState : JsonToString
    {
        public ReadOnlyCollection<PublisherTopicState> Topics { get; }

        internal PublisherState(IList<PublisherTopicState> topics)
        {
            Topics = new ReadOnlyCollection<PublisherTopicState>(topics);
        }
    }
}
