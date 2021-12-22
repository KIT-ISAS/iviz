#nullable enable

using System.Runtime.Serialization;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class GuiDialogConfiguration : IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.GuiDialog;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
    }
}