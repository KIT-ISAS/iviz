using System.Collections.Generic;
using System.Linq;
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
    [DataMember] public string[] SubscriberIds { get; }
    [DataMember] public ReceiverState[] Receivers { get; }

    public SubscriberState(string topic, string type, IEnumerable<string> topicIds, IEnumerable<ReceiverState> receivers)
    {
        Topic = topic;
        Type = type;
        SubscriberIds = topicIds.ToArray();
        Receivers = receivers.ToArray();
    }
}
