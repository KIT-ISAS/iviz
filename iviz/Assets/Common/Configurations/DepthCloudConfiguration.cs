using System;
using System.Runtime.Serialization;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class DepthCloudConfiguration : IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.DepthCloud;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string ColorTopic { get; set; } = "";
        [DataMember] public string DepthTopic { get; set; } = "";
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.bone;
        [DataMember] public float MinIntensity { get; set; }
        [DataMember] public float MaxIntensity { get; set; } = 1.0f;
        [DataMember] public bool FlipMinMax { get; set; }
        [DataMember] public bool OverrideMinMax { get; set; }
    }
}