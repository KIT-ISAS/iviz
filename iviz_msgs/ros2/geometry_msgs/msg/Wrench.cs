/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class Wrench : IDeserializableRos2<Wrench>, IMessageRos2
    {
        // This represents force in free space, separated into its linear and angular parts.
        [DataMember (Name = "force")] public Vector3 Force;
        [DataMember (Name = "torque")] public Vector3 Torque;
    
        /// Constructor for empty message.
        public Wrench()
        {
        }
        
        /// Explicit constructor.
        public Wrench(in Vector3 Force, in Vector3 Torque)
        {
            this.Force = Force;
            this.Torque = Torque;
        }
        
        /// Constructor with buffer.
        public Wrench(ref ReadBuffer2 b)
        {
            b.Deserialize(out Force);
            b.Deserialize(out Torque);
        }
        
        public Wrench RosDeserialize(ref ReadBuffer2 b) => new Wrench(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in Force);
            b.Serialize(in Torque);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Force);
            WriteBuffer2.AddLength(ref c, Torque);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Wrench";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
