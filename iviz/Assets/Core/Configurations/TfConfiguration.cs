#nullable enable

using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class TfConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public ModuleType ModuleType => ModuleType.TF;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Id { get; set; } = "";
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public bool PreferUdp { get; set; } = false;
        [DataMember] public float FrameSize { get; set; } = 0.125f;
        [DataMember] public bool FrameLabelsVisible { get; set; }
        [DataMember] public bool ParentConnectorVisible { get; set; }
        [DataMember] public bool KeepAllFrames { get; set; } = true;
        [DataMember] public string FixedFrameId { get; set; } = "";
        [DataMember] public bool Interactable { get; set; } = true;
        [DataMember] public bool FlipZ { get; set; }
    }
}