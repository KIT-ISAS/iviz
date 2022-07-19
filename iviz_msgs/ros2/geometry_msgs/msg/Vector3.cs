/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IMessageRos2, IDeserializableRos2<Vector3>
    {
        // This represents a vector in free space.
        // This is semantically different than a point.
        // A vector is always anchored at the origin.
        // When a transform is applied to a vector, only the rotational component is applied.
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "z")] public double Z;
    
        /// Explicit constructor.
        public Vector3(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Vector3 RosDeserialize(ref ReadBuffer2 b) => new Vector3(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 24;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public readonly void AddRosMessageLength(ref int c) => WriteBuffer2.AddLength(ref c, this);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Vector3";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
