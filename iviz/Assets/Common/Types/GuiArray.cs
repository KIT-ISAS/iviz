using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    public sealed class GuiArray : RosMessageWrapper<GuiArray>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiArray";

        [DataMember, NotNull] public Dialog[] Dialogs { get; set; } = Array.Empty<Dialog>();
        [DataMember, NotNull] public Widget[] Widgets { get; set; } = Array.Empty<Widget>();
    }
}