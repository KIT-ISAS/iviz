#nullable enable

using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Tools;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class MarkerConfiguration : IConfigurationWithType
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public bool TriangleListFlipWinding { get; set; } = true;
        [DataMember] public bool ShowDescriptions { get; set; }
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public bool PreferUdp { get; set; } = true;
        [DataMember] public ModuleType ModuleType => ModuleType.Marker;
        [DataMember] public float Smoothness { get; set; } = 0.5f;
        [DataMember] public float Metallic { get; set; } = 0.5f;

        [DataMember]
        public bool[] VisibleMask { get; set; } = Enumerable.Repeat(true, Marker.TRIANGLE_LIST + 1).ToArray();

        [DataMember] public bool Visible { get; set; } = true;
    }
}