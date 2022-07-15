/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class MultiDOFJointState : IDeserializable<MultiDOFJointState>, IMessageRos2
    {
        // Representation of state for joints with multiple degrees of freedom,
        // following the structure of JointState which can only represent a single degree of freedom.
        //
        // It is assumed that a joint in a system corresponds to a transform that gets applied
        // along the kinematic chain. For example, a planar joint (as in URDF) is 3DOF (x, y, yaw)
        // and those 3DOF can be expressed as a transformation matrix, and that transformation
        // matrix can be converted back to (x, y, yaw)
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state.
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // wrench associated with them, you can leave the wrench array empty.
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms;
        [DataMember (Name = "twist")] public GeometryMsgs.Twist[] Twist;
        [DataMember (Name = "wrench")] public GeometryMsgs.Wrench[] Wrench;
    
        /// Constructor for empty message.
        public MultiDOFJointState()
        {
            JointNames = System.Array.Empty<string>();
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Twist = System.Array.Empty<GeometryMsgs.Twist>();
            Wrench = System.Array.Empty<GeometryMsgs.Wrench>();
        }
        
        /// Constructor with buffer.
        public MultiDOFJointState(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out JointNames);
            b.DeserializeStructArray(out Transforms);
            b.DeserializeArray(out Twist);
            for (int i = 0; i < Twist.Length; i++)
            {
                Twist[i] = new GeometryMsgs.Twist(ref b);
            }
            b.DeserializeArray(out Wrench);
            for (int i = 0; i < Wrench.Length; i++)
            {
                Wrench[i] = new GeometryMsgs.Wrench(ref b);
            }
        }
        
        public MultiDOFJointState RosDeserialize(ref ReadBuffer2 b) => new MultiDOFJointState(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(Transforms);
            b.SerializeArray(Twist);
            b.SerializeArray(Wrench);
        }
        
        public void RosValidate()
        {
            if (JointNames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) BuiltIns.ThrowNullReference(nameof(JointNames), i);
            }
            if (Transforms is null) BuiltIns.ThrowNullReference();
            if (Twist is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Twist.Length; i++)
            {
                if (Twist[i] is null) BuiltIns.ThrowNullReference(nameof(Twist), i);
                Twist[i].RosValidate();
            }
            if (Wrench is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Wrench.Length; i++)
            {
                if (Wrench[i] is null) BuiltIns.ThrowNullReference(nameof(Wrench), i);
                Wrench[i].RosValidate();
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, JointNames);
            WriteBuffer2.Advance(ref c, Transforms);
            WriteBuffer2.Advance(ref c, Twist);
            WriteBuffer2.Advance(ref c, Wrench);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/MultiDOFJointState";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
