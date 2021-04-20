using System.Runtime.Serialization;
using Iviz.MsgsWrapper;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class DetectedARMarkerArray : RosMessageWrapper<DetectedARMarkerArray>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/DetectedARMarkerArray";

        [DataMember] public DetectedARMarker[] Markers { get; set; }
    }
}