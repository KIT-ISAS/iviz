using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    public enum ARMarkerType : byte
    {
        Aruco,
        QrCode,
    }

    [DataContract]
    public sealed class DetectedARMarker : RosMessageWrapper<DetectedARMarker>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/DetectedARMarker";

        [DataMember] public Header Header { get; set; }
        [DataMember] public ARMarkerType MarkerType { get; set; }
        [DataMember] public uint ArucoId { get; set; }
        [DataMember, NotNull] public string QrCode { get; set; } = "";

        [DataMember, FixedSizeArray(4), NotNull]
        public Vector2f[] Corners { get; set; } = new Vector2f[4];

        [DataMember, NotNull] public Intrinsic Intrinsic { get; set; } = new Intrinsic();
        [DataMember] public Pose CameraPose { get; set; }

        [DataMember] public bool HasExtrinsicPose { get; set; }
        [DataMember] public Pose PoseRelativeToCamera { get; set; }
        [DataMember] public Pose ExtrinsicPose { get; set; }
    }
}