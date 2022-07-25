using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Roslib2;

[DataContract]
public sealed class Ros2ReceiverState : ReceiverState
{
    readonly Guid guid;
    readonly QosProfile profile;

    [DataMember] public string? TopicType { get; set; }
    [DataMember] public ref readonly Guid Guid => ref guid;
    [DataMember] public ref readonly QosProfile Profile => ref profile;

    public Ros2ReceiverState()
    {
    }

    public Ros2ReceiverState(in Guid guid, in QosProfile profile)
    {
        this.guid = guid;
        this.profile = profile;
    }
}