using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib;

[DataContract]
public class ReceiverState : JsonToString
{
    [DataMember] public string? RemoteId { get; set; }
    [DataMember] public long NumReceived { get; set; }
    [DataMember] public long BytesReceived { get; set; }
}

[DataContract]
public class SubscriberState : JsonToString
{
    [DataMember] public string Topic { get; }
    [DataMember] public string Type { get; }
    [DataMember] public IReadOnlyList<string> SubscriberIds { get; }
    [DataMember] public IReadOnlyList<ReceiverState> Receivers { get; }

    public SubscriberState(string topic, string type, IReadOnlyList<string> topicIds, IReadOnlyList<ReceiverState> receivers)
    {
        Topic = topic;
        Type = type;
        SubscriberIds = topicIds;
        Receivers = receivers;
    }

    public void Deconstruct(out string topic, out string type, out IReadOnlyList<string> subscriberIds,
        out IReadOnlyList<ReceiverState> receivers)
        => (topic, type, subscriberIds, receivers) = (Topic, Type, SubscriberIds, Receivers);
}
