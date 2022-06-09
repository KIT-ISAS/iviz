using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class GridMapConfiguration : JsonToString,IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.GridMap;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string IntensityChannel { get; set; } = "";
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.jet;
        [DataMember] public bool ForceMinMax { get; set; }
        [DataMember] public float MinIntensity { get; set; }
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; }
        [DataMember] public float Smoothness { get; set; } = 0.5f;
        [DataMember] public float Metallic { get; set; } = 0.5f;
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
    }
}