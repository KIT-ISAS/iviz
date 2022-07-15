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
    public struct Pose : IMessageRos2, IDeserializable<Pose>
    {
        // A representation of pose in free space, composed of position and orientation.
        [DataMember (Name = "position")] public Point Position;
        [DataMember (Name = "orientation")] public Quaternion Orientation;
    
        /// Explicit constructor.
        public Pose(in Point Position, in Quaternion Orientation)
        {
            this.Position = Position;
            this.Orientation = Orientation;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pose(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Pose RosDeserialize(ref ReadBuffer2 b) => new Pose(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 56;
        
        public readonly void GetRosMessageLength(ref int c) => WriteBuffer2.Advance(ref c, this);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Pose";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
