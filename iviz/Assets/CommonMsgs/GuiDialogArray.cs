using System.Runtime.Serialization;
using Iviz.MsgsWrapper;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class GuiDialogArray : RosMessageWrapper<GuiDialogArray>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiDialogArray";
        
        [DataMember] public GuiDialog[] Dialogs { get; set; }
    }
}