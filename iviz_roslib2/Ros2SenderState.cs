using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Roslib2;

[DataContract]
public sealed class Ros2SenderState : SenderState
{
    [DataMember] public string? TopicType { get; set; }
    [DataMember] public Guid Guid { get; }
    [DataMember] public QosProfile Profile { get; }

    public Ros2SenderState(in Guid guid, QosProfile profile)
    {
        Guid = guid;
        Profile = profile;
    }
}

[DataContract]
public sealed class Ros2PublisherState : PublisherState
{
    [DataMember] public QosProfile Profile { get; }

    public Ros2PublisherState(string topic, string type,
        IReadOnlyList<string> advertiserIds,
        IReadOnlyList<SenderState> senders,
        QosProfile profile) : base(topic, type, advertiserIds, senders) => Profile = profile;
}