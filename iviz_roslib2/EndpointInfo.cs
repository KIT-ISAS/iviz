using System.Runtime.Serialization;

namespace Iviz.Roslib2;

[DataContract]
internal readonly struct EndpointInfo
{
    [DataMember] public readonly NodeName NodeName;
    [DataMember] public readonly string TopicType;
    [DataMember] public readonly Guid Guid;
    
    public EndpointInfo(in Guid guid, in NodeName nodeName, string topicType)
    {
        Guid = guid;
        NodeName = nodeName;
        TopicType = topicType;
    }
    
    public void Deconstruct(out NodeName nodeName, out string topicType, out Guid guid)
    {
        nodeName = NodeName;
        topicType = TopicType;
        guid = Guid;
    }
}