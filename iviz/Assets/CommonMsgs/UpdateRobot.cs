using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace CommonMsgs
{
    public enum OperationType
    {
        AddOrUpdate,
        Remove
    } 
    
    [DataContract]
    public sealed class UpdateRobotRequest : RosRequestWrapper<UpdateRobot, UpdateRobotRequest, UpdateRobotResponse>
    {
        [DataMember] public OperationType Operation { get; set; }
        [DataMember, NotNull] public string Id { get; set; } = "";
        [DataMember, NotNull] public RobotConfiguration Configuration { get; set; } = new RobotConfiguration();
        [DataMember, NotNull] public string[] ValidFields { get; set; } = Array.Empty<string>();
    }

    [DataContract]
    public sealed class UpdateRobotResponse : RosResponseWrapper<UpdateRobot, UpdateRobotRequest, UpdateRobotResponse>
    {
        [DataMember] public bool Success { get; set; }
        [DataMember, NotNull] public string Message { get; set; } = "";
    }

    [DataContract]
    public sealed class UpdateRobot : RosServiceWrapper<UpdateRobot, UpdateRobotRequest, UpdateRobotResponse>
    {
        [Preserve] public const string RosServiceType = "iviz_msgs/UpdateRobot";
    }
}