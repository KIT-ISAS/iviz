/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class JointState : IDeserializableRos2<JointState>, IMessageRos2
    {
        // This is a message that holds data to describe the state of a set of torque controlled joints.
        //
        // The state of each joint (revolute or prismatic) is defined by:
        //  * the position of the joint (rad or m),
        //  * the velocity of the joint (rad/s or m/s) and
        //  * the effort that is applied in the joint (Nm or N).
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state.
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // effort associated with them, you can leave the effort array empty.
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "name")] public string[] Name;
        [DataMember (Name = "position")] public double[] Position;
        [DataMember (Name = "velocity")] public double[] Velocity;
        [DataMember (Name = "effort")] public double[] Effort;
    
        /// Constructor for empty message.
        public JointState()
        {
            Name = System.Array.Empty<string>();
            Position = System.Array.Empty<double>();
            Velocity = System.Array.Empty<double>();
            Effort = System.Array.Empty<double>();
        }
        
        /// Constructor with buffer.
        public JointState(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out Name);
            b.DeserializeStructArray(out Position);
            b.DeserializeStructArray(out Velocity);
            b.DeserializeStructArray(out Effort);
        }
        
        public JointState RosDeserialize(ref ReadBuffer2 b) => new JointState(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Name);
            b.SerializeStructArray(Position);
            b.SerializeStructArray(Velocity);
            b.SerializeStructArray(Effort);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] is null) BuiltIns.ThrowNullReference(nameof(Name), i);
            }
            if (Position is null) BuiltIns.ThrowNullReference();
            if (Velocity is null) BuiltIns.ThrowNullReference();
            if (Effort is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Name);
            WriteBuffer2.AddLength(ref c, Position);
            WriteBuffer2.AddLength(ref c, Velocity);
            WriteBuffer2.AddLength(ref c, Effort);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/JointState";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
