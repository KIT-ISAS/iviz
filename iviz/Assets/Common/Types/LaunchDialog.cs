using System.Runtime.Serialization;
using Iviz.Msgs.IvizMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class LaunchDialogRequest : RosRequestWrapper<LaunchDialog, LaunchDialogRequest, LaunchDialogResponse>
    {
        [DataMember, NotNull] public Dialog Dialog { get; set; } = new Dialog();
    }

    [DataContract]
    public sealed class LaunchDialogResponse : RosResponseWrapper<LaunchDialog, LaunchDialogRequest, LaunchDialogResponse>
    {
        [DataMember] public bool Success { get; set; }
        [DataMember, NotNull] public string Message { get; set; } = "";
        [DataMember, NotNull] public Feedback Feedback { get; set; } = new Feedback();
    }

    [DataContract]
    public sealed class LaunchDialog : RosServiceWrapper<LaunchDialog, LaunchDialogRequest, LaunchDialogResponse>
    {
        [Preserve] public const string RosServiceType = "iviz_msgs/LaunchDialog";
    }
}