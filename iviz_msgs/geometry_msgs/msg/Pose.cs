/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Pose : IMessage, IDeserializable<Pose>, System.IEquatable<Pose>
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
        public Pose(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Pose(ref b);
        
        public readonly Pose RosDeserialize(ref ReadBuffer b) => new Pose(ref b);
        
        public readonly override int GetHashCode() => (Position, Orientation).GetHashCode();
        public readonly override bool Equals(object? o) => o is Pose s && Equals(s);
        public readonly bool Equals(Pose o) => (Position, Orientation) == (o.Position, o.Orientation);
        public static bool operator==(in Pose a, in Pose b) => a.Equals(b);
        public static bool operator!=(in Pose a, in Pose b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Pose";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e45d45a5a1ce597b249e23fb30fc871f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71RsQrCMBTc31c8cJW6iIPg4OQkKLpLqC9twOTVvIjWrzctNrGTi5jpLrm83F0muEZP" +
                "jSchF1Qw7JA1NiyExqH2RCiNKmmKJdtu+/w+N71Wuci9Ge4WCDs2LiQB7G8qkHf93KwDWP14wfawWWJF" +
                "bCn49mSlkllvBSZ4rI1E+/Ft4wRDTdl/zKIi6yyP4oK+sAqLOT4SahN6/sd+rm7IkD5KYvGffY7Nd+ya" +
                "e9fsbQFfEg3oDvACaqg09xMCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Pose Identity = (Point.Zero, Quaternion.Identity);
        public static implicit operator Transform(in Pose p) => new Transform(p.Position, p.Orientation);
        public static implicit operator Pose(in (Point position, Quaternion orientation) p) => new Pose(p.position, p.orientation);
    }
}
