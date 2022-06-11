using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class JointStateConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.JointState;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string RobotName { get; set; } = "";
        [DataMember] public string MsgJointPrefix { get; set; } = "";
        [DataMember] public string MsgJointSuffix { get; set; } = "";
        [DataMember] public int MsgTrimFromEnd { get; set; } = 0;
    }
}