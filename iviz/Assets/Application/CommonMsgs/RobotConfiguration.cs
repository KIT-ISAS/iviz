using System;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using Iviz.Resources;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class RobotConfiguration : RosMessageWrapper<RobotConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/RobotConfiguration";

        [DataMember] public string SourceParameter { get; set; } = "";
        [DataMember] public string SavedRobotName { get; set; } = "";
        [DataMember] public string FramePrefix { get; set; } = "";
        [DataMember] public string FrameSuffix { get; set; } = "";
        [DataMember] public bool AttachedToTf { get; set; }
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public ColorRGBA Tint { get; set; } = ColorRGBA.White;
        [DataMember] public float Metallic { get; set; } = 0.5f;
        [DataMember] public float Smoothness { get; set; } = Settings.IsHololens ? 0.25f : 0.5f;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.Robot;
        [DataMember] public bool Visible { get; set; } = true;
    }
}