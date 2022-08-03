#nullable enable

using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class GuiWidgetConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.GuiWidget;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public bool Interactable { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
    }
}