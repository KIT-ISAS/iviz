using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    public enum WidgetType
    {
        RotationDisc,
        SpringDisc,
    }
    
    public sealed class Widget : RosMessageWrapper<Widget>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Widget";
        
        [DataMember] public Header Header { get; set; }

        [DataMember] public ActionType Action { get; set; }
        [DataMember, NotNull] public string Id { get; set; } = "";

        [DataMember] public WidgetType Type { get; set; }
        [DataMember] public double Scale { get; set; } = 1;
        [DataMember] public Pose Pose { get; set; } = Pose.Identity;
    }
}