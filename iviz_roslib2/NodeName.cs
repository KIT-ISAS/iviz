using System.Runtime.Serialization;

namespace Iviz.Roslib2;

[DataContract]
internal readonly struct NodeName
{
    [DataMember] public readonly string Namespace;
    [DataMember] public readonly string Name;

    public NodeName(string nodeName, string nodeNamespace)
    {
        Name = nodeName;
        Namespace = nodeNamespace;
    }

    public override string ToString()
    {
        return Namespace + Name;
    }
}