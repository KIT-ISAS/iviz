using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Roslib2;

[DataContract]
public sealed class Ros2ReceiverState : ReceiverState
{
    [DataMember] public Guid Guid { get; set; }
    [DataMember] public string? TopicType { get; set; }
}