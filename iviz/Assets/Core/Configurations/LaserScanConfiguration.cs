using System;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class LaserScanConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.LaserScan;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public float PointSize { get; set; } = 0.03f;
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.jet;
        [DataMember] public bool UseIntensity { get; set; } = false;
        [DataMember] public bool UseLines { get; set; } = false;
        [DataMember] public bool ForceMinMax { get; set; } = false;
        [DataMember] public float MinIntensity { get; set; } = 0;
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; } = false;
    }
}