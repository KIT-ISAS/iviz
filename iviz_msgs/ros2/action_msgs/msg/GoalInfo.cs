/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.ActionMsgs
{
    [DataContract]
    public sealed class GoalInfo : IDeserializable<GoalInfo>, IMessageRos2
    {
        // Goal ID
        [DataMember (Name = "goal_id")] public UniqueIdentifierMsgs.UUID GoalId;
        // Time when the goal was accepted
        [DataMember (Name = "stamp")] public time Stamp;
    
        /// Constructor for empty message.
        public GoalInfo()
        {
            GoalId = new UniqueIdentifierMsgs.UUID();
        }
        
        /// Explicit constructor.
        public GoalInfo(UniqueIdentifierMsgs.UUID GoalId, time Stamp)
        {
            this.GoalId = GoalId;
            this.Stamp = Stamp;
        }
        
        /// Constructor with buffer.
        public GoalInfo(ref ReadBuffer2 b)
        {
            GoalId = new UniqueIdentifierMsgs.UUID(ref b);
            b.Deserialize(out Stamp);
        }
        
        public GoalInfo RosDeserialize(ref ReadBuffer2 b) => new GoalInfo(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            GoalId.RosSerialize(ref b);
            b.Serialize(Stamp);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference();
            GoalId.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 24;
        
        public void GetRosMessageLength(ref int c)
        {
            GoalId.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, Stamp);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "action_msgs/GoalInfo";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
