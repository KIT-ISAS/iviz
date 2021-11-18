using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class GuiDialogConfiguration : RosMessageWrapper<GuiDialogConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiDialogConfiguration";

        [DataMember, NotNull] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.GuiDialog;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember, NotNull] public string Topic { get; set; } = "";
        [DataMember, NotNull] public string Type { get; set; } = "";
    }
}