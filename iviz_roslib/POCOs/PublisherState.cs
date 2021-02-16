using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Iviz.Roslib
{
    public sealed class PublisherSenderState : JsonToString
    {
        public bool IsAlive { get; internal set; }
        public bool Latching { get; internal set; }
        public SenderStatus Status { get; internal set; }
        public Endpoint? Endpoint { get; internal set; }
        public string RemoteId { get; internal set; } = "";
        public Endpoint? RemoteEndpoint { get; internal set; }
        public int CurrentQueueSize { get; internal set; }
        public int MaxQueueSize { get; internal set; }
        public long NumSent { get; internal set; }
        public long BytesSent { get; internal set; }
        public long NumDropped { get; internal set; }
        public long BytesDropped { get; internal set; }
    }

    public sealed class PublisherTopicState : JsonToString
    {
        public string Topic { get; }
        public string Type { get; }
        public ReadOnlyCollection<string> TopicIds { get; }
        public ReadOnlyCollection<PublisherSenderState> Senders { get; }

        internal PublisherTopicState(string topic, string type, IList<string> topicIds,
            IList<PublisherSenderState> senders)
        {
            Topic = topic;
            Type = type;
            TopicIds = topicIds.AsReadOnly();
            Senders = senders.AsReadOnly();
        }
    }

    public sealed class PublisherState : JsonToString
    {
        public ReadOnlyCollection<PublisherTopicState> Topics { get; }

        internal PublisherState(IList<PublisherTopicState> topics)
        {
            Topics = topics.AsReadOnly();
        }
    }
}