using System.Runtime.Serialization;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Iviz.Msgs.IvizCommonMsg
{
    public enum FeedbackType
    {
        ButtonClick,
        MenuEntryClick,
    } 
    
    [DataContract]
    public sealed class GuiDialogFeedback : RosMessageWrapper<GuiDialogFeedback>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiDialogFeedback";
        
        [DataMember, NotNull] public string EngineId { get; set; } = "";
        [DataMember] public string DialogId { get; set; } = "";
        [DataMember] public FeedbackType FeedbackType { get; set; }
        [DataMember] public int EntryId { get; set; }
    }
}