using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.MsgsWrapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PointCloudType
    {
        Points,
        Cubes,
        Spheres
    }
    
    [DataContract]
    public sealed class PointCloudConfiguration : RosMessageWrapper<PointCloudConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/PointCloudConfiguration";
        
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string IntensityChannel { get; set; } = "z";
        [DataMember] public float PointSize { get; set; } = 0.3f;
        [DataMember] public int SizeMultiplier { get; set; } = -1;
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