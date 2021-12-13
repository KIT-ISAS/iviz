using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Common.Configurations
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PointCloudType
    {
        Points,
        Cubes,
        Spheres
    }

    [DataContract]
    public sealed class PointCloudConfiguration : IConfigurationWithTopic
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string IntensityChannel { get; set; } = "z";
        [DataMember] public float PointSize { get; set; } = 0.03f;
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.hsv;
        [DataMember] public bool OverrideMinMax { get; set; }
        [DataMember] public float MinIntensity { get; set; }
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; }
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.PointCloud;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public PointCloudType PointCloudType { get; set; } = PointCloudType.Points;
    }
}