using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib;

[DataContract]
public class SenderState : JsonToString
{
    [DataMember] public string RemoteId { get; set; } = "";
    [DataMember] public long NumSent { get; set; }
    [DataMember] public long BytesSent { get; set; }
}

[DataContract]
public class PublisherState : JsonToString
{
    [DataMember] public string Topic { get; }
    [DataMember] public string Type { get; }
    [DataMember] public string[] AdvertiserIds { get; }
    [DataMember] public SenderState[] Senders { get; }

    public PublisherState(string topic, string type,
        IEnumerable<string> advertiserIds,
        IEnumerable<SenderState> senders) =>
        (Topic, Type, AdvertiserIds, Senders) = (topic, type, advertiserIds.ToArray(), senders.ToArray());
}
