#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class RobotConfiguration : IConfiguration
    {
        [DataMember] public string SourceParameter { get; set; } = "";
        [DataMember] public string SavedRobotName { get; set; } = "";
        [DataMember] public string FramePrefix { get; set; } = "";
        [DataMember] public string FrameSuffix { get; set; } = "";
        [DataMember] public bool AttachedToTf { get; set; }
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
        [DataMember] public float Metallic { get; set; } = 0.5f;
        [DataMember] public float Smoothness { get; set; } = 0.5f;
        [DataMember] public bool EnableColliders { get; set; } = true;
        [DataMember] public bool KeepMeshMaterials { get; set; } = false;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Robot;
        [DataMember] public bool Visible { get; set; } = true;
    }
}