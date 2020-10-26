/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Quaternion")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IMessage, System.IEquatable<Quaternion>, IDeserializable<Quaternion>
    {
        // This represents an orientation in free space in quaternion form.
        [DataMember (Name = "x")] public double X { get; set; }
        [DataMember (Name = "y")] public double Y { get; set; }
        [DataMember (Name = "z")] public double Z { get; set; }
        [DataMember (Name = "w")] public double W { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Quaternion(double X, double Y, double Z, double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Quaternion(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Quaternion(ref b);
        }
        
        readonly Quaternion IDeserializable<Quaternion>.RosDeserialize(ref Buffer b)
        {
            return new Quaternion(ref b);
        }
        
        public override readonly int GetHashCode() => (X, Y, Z, W).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Quaternion s && Equals(s);
        
        public readonly bool Equals(Quaternion o) => (X, Y, Z, W) == (o.X, o.Y, o.Z, o.W);
        
        public static bool operator==(in Quaternion a, in Quaternion b) => a.Equals(b);
        
        public static bool operator!=(in Quaternion a, in Quaternion b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 32;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Quaternion";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a779879fadf0160734f906b8c19c7004";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3JTQqAQAhA4b2nENq3ik7SBSQcEkonNfo5fbWZ3fd4HU6LBDpX52DNQFI0l4+UYoqi" +
                "WJwZo9LMf+0HJbv+r5hvPUBZjXIc8Gq6m56mE+AFLI5yL20AAAA=";
                
        /// Custom iviz code
        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);
        public static Quaternion operator *(in Quaternion a, in Quaternion b) =>
            new Quaternion(
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X,
                a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W,
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z
            );
    }
}
