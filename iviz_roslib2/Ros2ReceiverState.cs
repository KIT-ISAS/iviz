using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Roslib2;

[DataContract]
public sealed class Ros2ReceiverState : ReceiverState
{
    [DataMember] public string? TopicType { get; set; }
    [DataMember] public Guid Guid { get; }
    [DataMember] public QosProfile Profile { get; }

    public Ros2ReceiverState()
    {
        Profile = QosProfile.SystemDefault;
    }

    public Ros2ReceiverState(in Guid guid, QosProfile profile)
    {
        Guid = guid;
        Profile = profile;
    }
}

[DataContract]
public sealed class Ros2SubscriberState : SubscriberState
{
    [DataMember] public QosProfile Profile { get;}

    public Ros2SubscriberState(string topic, string type,
        IReadOnlyList<string> advertiserIds,
        IReadOnlyList<ReceiverState> receivers,
        QosProfile profile) : base(topic, type, advertiserIds, receivers) => Profile = profile;
}