/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RosgraphMsgs
{
    [DataContract]
    public sealed class Clock : IDeserializable<Clock>, IMessageRos2
    {
        // This message communicates the current time.
        //
        // For more information, see https://design.ros2.org/articles/clock_and_time.html.
        [DataMember (Name = "clock")] public time Clock_;
    
        /// Constructor for empty message.
        public Clock()
        {
        }
        
        /// Explicit constructor.
        public Clock(time Clock_)
        {
            this.Clock_ = Clock_;
        }
        
        /// Constructor with buffer.
        public Clock(ref ReadBuffer2 b)
        {
            b.Deserialize(out Clock_);
        }
        
        public Clock RosDeserialize(ref ReadBuffer2 b) => new Clock(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Clock_);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Clock_);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosgraph_msgs/Clock";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
