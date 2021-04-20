using Iviz.MsgsWrapper;
using UnityEngine.Scripting;

namespace Iviz.Msgs.IvizCommonMsg
{
    public sealed class GuiDialogFeedback : RosMessageWrapper<GuiDialogFeedback>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiDialogFeedback";
        
        public string Id { get; set; } = "";
    }
}