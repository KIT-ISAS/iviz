using System;
using System.Runtime.Serialization;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class RobotConfiguration : RosMessageWrapper<RobotConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/RobotConfiguration";

        [DataMember,NotNull] public string SourceParameter { get; set; } = "";
        [DataMember,NotNull] public string SavedRobotName { get; set; } = "";
        [DataMember,NotNull] public string FramePrefix { get; set; } = "";
        [DataMember,NotNull] public string FrameSuffix { get; set; } = "";
        [DataMember] public bool AttachedToTf { get; set; }
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
        [DataMember] public float Metallic { get; set; } = 0.5f;
        [DataMember] public float Smoothness { get; set; } = 0.5f;
        [DataMember,NotNull] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Robot;
        [DataMember] public bool Visible { get; set; } = true;
    }
}