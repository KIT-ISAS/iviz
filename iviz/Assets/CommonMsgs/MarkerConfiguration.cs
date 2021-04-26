using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class MarkerConfiguration : RosMessageWrapper<MarkerConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/MarkerConfiguration";

        [DataMember, NotNull] public string Topic { get; set; } = "";
        [DataMember, NotNull] public string Type { get; set; } = "";
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public bool TriangleListFlipWinding { get; set; } = true;
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
        [DataMember, NotNull] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Marker;

        [DataMember]
        public bool[] VisibleMask { get; set; } = Enumerable.Repeat(true, Marker.TRIANGLE_LIST + 1).ToArray();

        [DataMember] public bool Visible { get; set; } = true;
    }
}