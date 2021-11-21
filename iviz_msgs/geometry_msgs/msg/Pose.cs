/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Pose")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Pose : IMessage, System.IEquatable<Pose>, IDeserializable<Pose>
    {
        // A representation of pose in free space, composed of position and orientation. 
        [DataMember (Name = "position")] public Point Position;
        [DataMember (Name = "orientation")] public Quaternion Orientation;
    
        /// <summary> Explicit constructor. </summary>
        public Pose(in Point Position, in Quaternion Orientation)
        {
            this.Position = Position;
            this.Orientation = Orientation;
        }
        
        /// <summary> Constructor with buffer. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Pose(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Pose(ref b);
        }
        
        readonly Pose IDeserializable<Pose>.RosDeserialize(ref Buffer b)
        {
            return new Pose(ref b);
        }
        
        public override readonly int GetHashCode() => (Position, Orientation).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Pose s && Equals(s);
        
        public readonly bool Equals(Pose o) => (Position, Orientation) == (o.Position, o.Orientation);
        
        public static bool operator==(in Pose a, in Pose b) => a.Equals(b);
        
        public static bool operator!=(in Pose a, in Pose b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Pose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e45d45a5a1ce597b249e23fb30fc871f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2RMQvCMBCF94P+h4Ouoos4CA5OToKiu4R6aQM2V3MRrb/etNjETi5ipnvJ5fK+lxzX" +
                "6KhxJGS98oYtssaGhdBY1I4IpVEFTbDguts+v89N36ts0M4Md6cIOzbWxwbY35QnZ/u5qQ8yWP14ZbA9" +
                "bJZYEtfkXXuqpZRZbyaDHI+VkUAQnjdW0FeUEAKOCqpzPSIGfWHlF3N8xKqN1fNfBCm/iBG/S0L8n6mO" +
                "/XfqmtLX7OopfIEaqnvAewEjTA5+GgIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Pose Identity = (Point.Zero, Quaternion.Identity);
        public static implicit operator Transform(in Pose p) => new Transform(p.Position, p.Orientation);
        public static implicit operator Pose(in (Point position, Quaternion orientation) p) => new Pose(p.position, p.orientation);
    }
}
