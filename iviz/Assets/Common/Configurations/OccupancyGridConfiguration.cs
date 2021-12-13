using System;
using System.Runtime.Serialization;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class OccupancyGridConfiguration : IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.OccupancyGrid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.bone;
        [DataMember] public bool FlipMinMax { get; set; } = true;
        [DataMember] public bool CubesVisible { get; set; } = false;
        [DataMember] public bool TextureVisible { get; set; } = true;
        [DataMember] public float ScaleZ { get; set; } = 0.5f;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
    }
}