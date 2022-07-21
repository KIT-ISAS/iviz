using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;
using Iviz.Tools;

namespace Iviz.Roslib;

/// <summary>
/// The type of transport protocol being used
/// </summary>
public enum TransportType
{
    Tcp,
    Udp
}

[DataContract]
public class SenderState : JsonToString
{
    [DataMember] public bool IsAlive { get; set; }
    [DataMember] public Endpoint Endpoint { get; set; }
    [DataMember] public string RemoteId { get; set; } = "";
    [DataMember] public Endpoint RemoteEndpoint { get; set; }
    [DataMember] public long NumSent { get; set; }
    [DataMember] public long BytesSent { get; set; }
}

[DataContract]
public sealed class Ros1SenderState : SenderState
{
    [DataMember] public TransportType TransportType { get; set; }
    [DataMember] public int CurrentQueueSize { get; set; }
    [DataMember] public long MaxQueueSizeInBytes { get; set; }
    [DataMember] public long NumDropped { get; set; }
    [DataMember] public long BytesDropped { get; set; }
}

[DataContract]
public sealed class PublisherState : JsonToString
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
