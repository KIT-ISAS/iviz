using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;
using Iviz.Tools;

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
    [DataMember] public IReadOnlyList<string> AdvertiserIds { get; }
    [DataMember] public IReadOnlyList<SenderState> Senders { get; }

    public PublisherState(string topic, string type,
        IReadOnlyList<string> advertiserIds,
        IReadOnlyList<SenderState> senders) =>
        (Topic, Type, AdvertiserIds, Senders) = (topic, type, advertiserIds, senders);

    public void Deconstruct(out string topic, out string type,
        out IReadOnlyList<string> advertiserIds,
        out IReadOnlyList<SenderState> senders) =>
        (topic, type, advertiserIds, senders) = (Topic, Type, AdvertiserIds, Senders);
}
