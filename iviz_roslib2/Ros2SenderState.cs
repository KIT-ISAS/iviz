using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Roslib2;

[DataContract]
public sealed class Ros2SenderState : SenderState
{
    readonly Guid guid;
    readonly QosProfile profile;

    [DataMember] public string? TopicType { get; set; }
    [DataMember] public ref readonly Guid Guid => ref guid;
    [DataMember] public ref readonly QosProfile Profile => ref profile;

    public Ros2SenderState(in Guid guid, in QosProfile profile)
    {
        this.guid = guid;
        this.profile = profile;
    }
}

[DataContract]
public sealed class Ros2PublisherState : PublisherState
{
    readonly QosProfile profile;
    [DataMember] public ref readonly QosProfile Profile => ref profile;

    public Ros2PublisherState(string topic, string type,
        IReadOnlyList<string> advertiserIds,
        IReadOnlyList<SenderState> senders,
        in QosProfile profile) : base(topic, type, advertiserIds, senders) =>
        this.profile = profile;
}
