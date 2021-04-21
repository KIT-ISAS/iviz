using System;
using System.Runtime.Serialization;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class GuiDialogArray : RosMessageWrapper<GuiDialogArray>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiDialogArray";

        [DataMember, NotNull] public GuiDialog[] Dialogs { get; set; } = Array.Empty<GuiDialog>();
    }
}