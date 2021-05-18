using System;
using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    public enum FeedbackType
    {
        Expired,
        ButtonClick,
        MenuEntryClick,
        PositionChanged,
        OrientationChanged,
        ScaleChanged,
        TrajectoryChanged,
    }

    [DataContract]
    public sealed class Trajectory : RosMessageWrapper<Trajectory>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Trajectory";

        [DataMember, NotNull] public Pose[] Poses { get; set; } = Array.Empty<Pose>();
        [DataMember, NotNull] public time[] Timestamps { get; set; } = Array.Empty<time>();
    }


    [DataContract]
    public sealed class Feedback : RosMessageWrapper<Feedback>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Feedback";

        [DataMember] public Header Header { get; set; }
        [DataMember, NotNull] public string VizId { get; set; } = "";
        [DataMember, NotNull] public string Id { get; set; } = "";
        [DataMember] public FeedbackType FeedbackType { get; set; }
        [DataMember] public int EntryId { get; set; }
        [DataMember] public Point Position { get; set; }
        [DataMember] public Quaternion Orientation { get; set; }
        [DataMember] public Vector3 Scale { get; set; }
        [DataMember, NotNull] public Trajectory Trajectory { get; set; } = new Trajectory();
    }
}