/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.ActionMsgs
{
    [DataContract]
    public sealed class GoalStatus : IDeserializableRos2<GoalStatus>, IMessageRos2
    {
        // An action goal can be in one of these states after it is accepted by an action
        // server.
        //
        // For more information, see http://design.ros2.org/articles/actions.html
        // Indicates status has not been properly set.
        public const sbyte STATUS_UNKNOWN = 0;
        // The goal has been accepted and is awaiting execution.
        public const sbyte STATUS_ACCEPTED = 1;
        // The goal is currently being executed by the action server.
        public const sbyte STATUS_EXECUTING = 2;
        // The client has requested that the goal be canceled and the action server has
        // accepted the cancel request.
        public const sbyte STATUS_CANCELING = 3;
        // The goal was achieved successfully by the action server.
        public const sbyte STATUS_SUCCEEDED = 4;
        // The goal was canceled after an external request from an action client.
        public const sbyte STATUS_CANCELED = 5;
        // The goal was terminated by the action server without an external request.
        public const sbyte STATUS_ABORTED = 6;
        // Goal info (contains ID and timestamp).
        [DataMember (Name = "goal_info")] public GoalInfo GoalInfo;
        // Action goal state-machine status.
        [DataMember (Name = "status")] public sbyte Status;
    
        /// Constructor for empty message.
        public GoalStatus()
        {
            GoalInfo = new GoalInfo();
        }
        
        /// Explicit constructor.
        public GoalStatus(GoalInfo GoalInfo, sbyte Status)
        {
            this.GoalInfo = GoalInfo;
            this.Status = Status;
        }
        
        /// Constructor with buffer.
        public GoalStatus(ref ReadBuffer2 b)
        {
            GoalInfo = new GoalInfo(ref b);
            b.Deserialize(out Status);
        }
        
        public GoalStatus RosDeserialize(ref ReadBuffer2 b) => new GoalStatus(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            GoalInfo.RosSerialize(ref b);
            b.Serialize(Status);
        }
        
        public void RosValidate()
        {
            if (GoalInfo is null) BuiltIns.ThrowNullReference();
            GoalInfo.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 25;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public void AddRosMessageLength(ref int c)
        {
            GoalInfo.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Status);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "action_msgs/GoalStatus";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
