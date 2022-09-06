/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Pose : IMessage, IDeserializable<Pose>, IHasSerializer<Pose>
    {
        // A representation of pose in free space, composed of position and orientation. 
        [DataMember (Name = "position")] public Point Position;
        [DataMember (Name = "orientation")] public Quaternion Orientation;
    
        public Pose(in Point Position, in Quaternion Orientation)
        {
            this.Position = Position;
            this.Orientation = Orientation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pose(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pose(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Pose RosDeserialize(ref ReadBuffer b) => new Pose(ref b);
        
        public readonly Pose RosDeserialize(ref ReadBuffer2 b) => new Pose(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 56;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 56;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Pose";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "e45d45a5a1ce597b249e23fb30fc871f";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71RsQrCMBTc31c8cJW6iIPg4OQkKLpLqC9twOTVvIjWrzctNrGTi5jpLrm83F0muEZP" +
                "jSchF1Qw7JA1NiyExqH2RCiNKmmKJdtu+/w+N71Wuci9Ge4WCDs2LiQB7G8qkHf93KwDWP14wfawWWJF" +
                "bCn49mSlkllvBSZ4rI1E+/Ft4wRDTdl/zKIi6yyP4oK+sAqLOT4SahN6/sd+rm7IkD5KYvGffY7Nd+ya" +
                "e9fsbQFfEg3oDvACaqg09xMCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static Pose Identity => new(Point.Zero, Quaternion.Identity);
        public static implicit operator Transform(in Pose p) => p.AsTransform();
        public static implicit operator Pose(in (Point position, Quaternion orientation) p) => new(p.position, p.orientation);
    
        public Serializer<Pose> CreateSerializer() => new Serializer();
        public Deserializer<Pose> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Pose>
        {
            public override void RosSerialize(Pose msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Pose msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Pose _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Pose _) => Ros2FixedMessageLength;
        }
        sealed class Deserializer : Deserializer<Pose>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Pose msg) => msg = new Pose(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Pose msg) => msg = new Pose(ref b);
        }
    }
}
