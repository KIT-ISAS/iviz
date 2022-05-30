#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class InteractiveMarkerConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool DescriptionsVisible { get; set; }
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.InteractiveMarker;
        [DataMember] public bool TriangleListFlipWinding { get; set; } = true;
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
        [DataMember] public float Alpha { get; set; } = 1;
        [DataMember] public float Smoothness { get; set; } = 0.5f;
        [DataMember] public float Metallic { get; set; } = 0.5f;
        [DataMember] public bool Visible { get; set; } = true;
    }
}