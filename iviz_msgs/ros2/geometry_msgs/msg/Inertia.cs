/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class Inertia : IDeserializableRos2<Inertia>, IMessageRos2
    {
        // Mass [kg]
        [DataMember (Name = "m")] public double M;
        // Center of mass [m]
        [DataMember (Name = "com")] public GeometryMsgs.Vector3 Com;
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        [DataMember (Name = "ixx")] public double Ixx;
        [DataMember (Name = "ixy")] public double Ixy;
        [DataMember (Name = "ixz")] public double Ixz;
        [DataMember (Name = "iyy")] public double Iyy;
        [DataMember (Name = "iyz")] public double Iyz;
        [DataMember (Name = "izz")] public double Izz;
    
        /// Constructor for empty message.
        public Inertia()
        {
        }
        
        /// Constructor with buffer.
        public Inertia(ref ReadBuffer2 b)
        {
            b.Deserialize(out M);
            b.Deserialize(out Com);
            b.Deserialize(out Ixx);
            b.Deserialize(out Ixy);
            b.Deserialize(out Ixz);
            b.Deserialize(out Iyy);
            b.Deserialize(out Iyz);
            b.Deserialize(out Izz);
        }
        
        public Inertia RosDeserialize(ref ReadBuffer2 b) => new Inertia(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(M);
            b.Serialize(in Com);
            b.Serialize(Ixx);
            b.Serialize(Ixy);
            b.Serialize(Ixz);
            b.Serialize(Iyy);
            b.Serialize(Iyz);
            b.Serialize(Izz);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, M);
            WriteBuffer2.AddLength(ref c, Com);
            WriteBuffer2.AddLength(ref c, Ixx);
            WriteBuffer2.AddLength(ref c, Ixy);
            WriteBuffer2.AddLength(ref c, Ixz);
            WriteBuffer2.AddLength(ref c, Iyy);
            WriteBuffer2.AddLength(ref c, Iyz);
            WriteBuffer2.AddLength(ref c, Izz);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Inertia";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
