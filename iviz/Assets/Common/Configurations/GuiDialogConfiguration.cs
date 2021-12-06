using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class GuiDialogConfiguration : IConfigurationWithType
    {
        [DataMember, NotNull] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.GuiDialog;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember, NotNull] public string Topic { get; set; } = "";
        [DataMember, NotNull] public string Type { get; set; } = "";
    }
}