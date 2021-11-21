using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Common
{
    public sealed class Widget : RosMessageWrapper<Widget>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Widget";
        
        [DataMember] public Header Header { get; set; }

        [DataMember] public ActionType Action { get; set; }
        [DataMember, NotNull] public string Id { get; set; } = "";

        [DataMember] public WidgetType Type { get; set; }
        [DataMember] public ColorRGBA MainColor { get; set; } = ColorRGBA.Cyan;
        [DataMember] public ColorRGBA SecondaryColor { get; set; } = ColorRGBA.Blue;
        [DataMember] public double Scale { get; set; } = 1;
        [DataMember] public Pose Pose { get; set; } = Pose.Identity;
        [DataMember] public string Caption { get; set; } = "";
    }
}