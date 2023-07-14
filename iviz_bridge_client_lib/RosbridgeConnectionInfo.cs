using System.Runtime.Serialization;
using Iviz.Msgs;

namespace iviz_bridge_client;

[DataContract]
internal sealed class RosbridgeConnectionInfo : IRosConnection
{
    [DataMember] public string Topic { get; }
    [DataMember] public string Type { get; }

    public RosbridgeConnectionInfo(string topic, string type)
    {
        Topic = topic;
        Type = type;
    }
}