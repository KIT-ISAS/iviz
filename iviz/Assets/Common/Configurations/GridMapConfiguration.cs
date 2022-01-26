using System.Runtime.Serialization;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public class GridMapConfiguration : IConfigurationWithTopic
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
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
    }
}