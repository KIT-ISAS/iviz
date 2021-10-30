using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;
using Iviz.Tools;

namespace Iviz.Roslib
{
    /// <summary>
    /// The type of transport protocol being used
    /// </summary>
    public enum TransportType
    {
        Tcp, 
        Udp
    }
    
    [DataContract]
    public sealed class PublisherSenderState : JsonToString
    {
        [DataMember] public bool IsAlive { get; internal set; }
        [DataMember] public TransportType TransportType { get; internal set; }
        [DataMember] public Endpoint Endpoint { get; internal set; }
        [DataMember] public string RemoteId { get; internal set; } = "";
        [DataMember] public Endpoint RemoteEndpoint { get; internal set; }
        [DataMember] public int CurrentQueueSize { get; internal set; }
        [DataMember] public int MaxQueueSize { get; internal set; }
        [DataMember] public long NumSent { get; internal set; }
        [DataMember] public long BytesSent { get; internal set; }
        [DataMember] public long NumDropped { get; internal set; }
        [DataMember] public long BytesDropped { get; internal set; }
    }

    [DataContract]
    public sealed class PublisherTopicState : JsonToString
    {
        [DataMember] public string Topic { get; }
        [DataMember] public string Type { get; }
        [DataMember] public ReadOnlyCollection<string> AdvertiserIds { get; }
        [DataMember] public ReadOnlyCollection<PublisherSenderState> Senders { get; }

        internal PublisherTopicState(string topic, string type,
            IList<string> advertiserIds,
            IList<PublisherSenderState> senders) =>
            (Topic, Type, AdvertiserIds, Senders) = (topic, type, advertiserIds.AsReadOnly(), senders.AsReadOnly());

        public void Deconstruct(out string topic, out string type, out ReadOnlyCollection<string> advertiserIds,
            out ReadOnlyCollection<PublisherSenderState> senders)
            => (topic, type, advertiserIds, senders) = (Topic, Type, AdvertiserIds, Senders);
    }

    [DataContract]
    public sealed class PublisherState : JsonToString
    {
        [DataMember] public ReadOnlyCollection<PublisherTopicState> Topics { get; }

        internal PublisherState(IList<PublisherTopicState> topics) => Topics = topics.AsReadOnly();
    }
}