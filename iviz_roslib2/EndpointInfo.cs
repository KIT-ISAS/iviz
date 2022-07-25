using System.Runtime.Serialization;

namespace Iviz.Roslib2;

[DataContract]
internal class EndpointInfo
{
    readonly Guid guid;
    readonly QosProfile profile;

    [DataMember] public NodeName NodeName { get; }
    [DataMember] public string TopicType { get; }
    [DataMember] public ref readonly Guid Guid => ref guid;
    [DataMember] public ref readonly QosProfile Profile => ref profile;
    
    public EndpointInfo(string nodeName, string nodeNamespace, string topicType, in Guid guid, in QosProfile profile)
    {
        NodeName = new NodeName(nodeName, nodeNamespace);
        TopicType = topicType;
        this.guid = guid;
        this.profile = profile;
    }
}