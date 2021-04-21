using System;
using System.Runtime.Serialization;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class DetectedARMarkerArray : RosMessageWrapper<DetectedARMarkerArray>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/DetectedARMarkerArray";

        [DataMember, NotNull] public DetectedARMarker[] Markers { get; set; } = Array.Empty<DetectedARMarker>();
    }
}