using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class OctomapConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.OccupancyGrid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public ColorRGBA Tint { get; set; } = new ColorRGBA(0.5f, 0.5f, 1, 1);
        [DataMember] public int MaxDepth { get; set; } = 16;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
    }
}