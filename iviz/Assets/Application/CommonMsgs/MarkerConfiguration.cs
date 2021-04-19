using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using Iviz.Resources;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class MarkerConfiguration : RosMessageWrapper<MarkerConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/MarkerConfiguration";
        
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public bool TriangleListFlipWinding { get; set; } = true;
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.Marker;
        [DataMember] public bool Visible { get; set; } = true;
    }
}