/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.ActionMsgs
{
    [DataContract]
    public sealed class GoalStatusArray : IDeserializableRos2<GoalStatusArray>, IMessageRos2
    {
        // An array of goal statuses.
        [DataMember (Name = "status_list")] public GoalStatus[] StatusList;
    
        /// Constructor for empty message.
        public GoalStatusArray()
        {
            StatusList = System.Array.Empty<GoalStatus>();
        }
        
        /// Explicit constructor.
        public GoalStatusArray(GoalStatus[] StatusList)
        {
            this.StatusList = StatusList;
        }
        
        /// Constructor with buffer.
        public GoalStatusArray(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out StatusList);
            for (int i = 0; i < StatusList.Length; i++)
            {
                StatusList[i] = new GoalStatus(ref b);
            }
        }
        
        public GoalStatusArray RosDeserialize(ref ReadBuffer2 b) => new GoalStatusArray(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(StatusList);
        }
        
        public void RosValidate()
        {
            if (StatusList is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < StatusList.Length; i++)
            {
                if (StatusList[i] is null) BuiltIns.ThrowNullReference(nameof(StatusList), i);
                StatusList[i].RosValidate();
            }
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, StatusList);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "action_msgs/GoalStatusArray";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
