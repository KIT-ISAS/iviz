using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Iviz.Msgs;

namespace Iviz.Roslib2;

/// ROS MiddleWare quality of service profile
[DataContract, StructLayout(LayoutKind.Sequential)]
public readonly struct QosProfile
{
    /// Default size of the rmw queue when history is set to KeepLast,
    /// 0 indicates it is currently not set.
    public const int DepthSystemDefault = 0;

    [DataMember] public readonly HistoryPolicy History;

    /// Size of the message queue.
    [DataMember] public readonly long Depth;

    /// Reliability QoS policy setting.
    [DataMember] public readonly ReliabilityPolicy Reliability;

    /// Durability QoS policy setting.
    [DataMember] public readonly DurabilityPolicy Durability;

    /// The period at which messages are expected to be sent/received.
    [DataMember] public readonly QosProfileTime Deadline;

    /// The age at which messages are considered expired and no longer valid.
    [DataMember] public readonly QosProfileTime Lifespan;

    /// Liveliness QoS policy setting.
    [DataMember] public readonly LivelinessPolicy Liveliness;

    /// The time within which the RMW node or publisher must show that it is alive.
    [DataMember] public readonly QosProfileTime LivelinessLeaseDuration;

    /// If true, any ROS specific namespacing conventions will be circumvented.
    [DataMember] public readonly bool AvoidRosNamespaceConventions;

    public QosProfile(ReliabilityPolicy reliability)
    {
        this = default;
        Reliability = reliability;
    }
}

[DataContract, StructLayout(LayoutKind.Sequential)]
public readonly struct QosProfileTime
{
    [DataMember] public readonly ulong Sec;
    [DataMember] public readonly ulong Nsecs;

    public time AsTime() => new((uint)Sec, (uint)Nsecs);
}

/// QoS history enumerations describing how samples endure
public enum ReliabilityPolicy
{
    /// Implementation specific default.
    SystemDefault,

    /// Guarantee that samples are delivered, may retry multiple times.
    Reliable,

    /// Attempt to deliver samples, but some may be lost if the network is not robust.
    BestEffort,
    Unknown
};

/// QoS history enumerations describing how samples endure.
public enum HistoryPolicy
{
    /// Implementation specific default.
    SystemDefault,

    /// Only store up to a maximum number of samples, dropping oldest once max is exceeded.
    KeepLast,

    /// Store all samples, subject to resource limits.
    KeepAll,
    Unknown
};

/// QoS durability enumerations describing how samples persist.
public enum DurabilityPolicy
{
    /// Implementation specific default.
    SystemDefault,

    /// The rmw publisher is responsible for persisting samples for “late-joining” subscribers.
    TransientLocal,

    /// Samples are not persistent.
    Volatile,
    Unknown
};

/// QoS liveliness enumerations that describe a publisher's reporting policy for its alive status.
/// For a subscriber, these are its requirements for its topic's publishers.
public enum LivelinessPolicy
{
    /// Implementation specific default.
    SystemDefault = 0,

    /// The signal that establishes a Topic is alive comes from the ROS rmw layer.
    Automatic = 1,

    /// Explicitly asserting node is required in this case.
    /// This option is deprecated, use ManualByTopic if your application
    /// requires to assert manually.
    [Obsolete] ManualByNode = 2,

    /// The signal that establishes a Topic is alive is at the Topic level.
    /// Only publishing a message on the Topic or an explicit signal from the application to assert  on the
    /// Topic will mark the Topic as being alive.
    ManualByTopic = 3,
    Unknown = 4
};