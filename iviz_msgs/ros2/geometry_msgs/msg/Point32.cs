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
    public struct Point32 : IMessageRos2, IDeserializable<Point32>
    {
        // This contains the position of a point in free space(with 32 bits of precision).
        // It is recommended to use Point wherever possible instead of Point32.
        //
        // This recommendation is to promote interoperability.
        //
        // This message is designed to take up less space when sending
        // lots of points at once, as in the case of a PointCloud.
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
        [DataMember (Name = "z")] public float Z;
    
        /// Explicit constructor.
        public Point32(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point32(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Point32 RosDeserialize(ref ReadBuffer2 b) => new Point32(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 12;
        
        public readonly void GetRosMessageLength(ref int c) => WriteBuffer2.Advance(ref c, this);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Point32";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
