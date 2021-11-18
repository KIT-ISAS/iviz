using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class OctomapConfiguration : RosMessageWrapper<OctomapConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/OctomapConfiguration";

        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.OccupancyGrid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public ColorRGBA Tint { get; set; } = new ColorRGBA(0.5f, 0.5f, 1, 1);
        [DataMember] public int MaxDepth { get; set; } = 16;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
    }
}