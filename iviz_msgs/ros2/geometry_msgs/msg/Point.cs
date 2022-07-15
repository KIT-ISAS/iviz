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
    public struct Point : IMessageRos2, IDeserializable<Point>
    {
        // This contains the position of a point in free space
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "z")] public double Z;
    
        /// Explicit constructor.
        public Point(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Point RosDeserialize(ref ReadBuffer2 b) => new Point(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 24;
        
        public readonly void GetRosMessageLength(ref int c) => WriteBuffer2.Advance(ref c, this);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Point";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
