using System.Runtime.Serialization;

namespace Iviz.Roslib2;

[DataContract]
internal sealed class EndpointInfo
{
    [DataMember] public NodeName NodeName { get; }
    [DataMember] public string TopicType { get; }
    [DataMember] public Guid Guid { get; }
    [DataMember] public QosProfile Profile { get; }
    
    public EndpointInfo(string nodeName, string nodeNamespace, string topicType, in Guid guid, QosProfile profile)
    {
        NodeName = new NodeName(nodeName, nodeNamespace);
        TopicType = topicType;
        Guid = guid;
        Profile = profile;
    }
}