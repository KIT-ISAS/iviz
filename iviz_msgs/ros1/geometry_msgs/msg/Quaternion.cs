/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IMessage, IDeserializable<Quaternion>, IHasSerializer<Quaternion>
    {
        // This represents an orientation in free space in quaternion form.
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "z")] public double Z;
        [DataMember (Name = "w")] public double W;
    
        public Quaternion(double X, double Y, double Z, double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Quaternion RosDeserialize(ref ReadBuffer b) => new Quaternion(ref b);
        
        public readonly Quaternion RosDeserialize(ref ReadBuffer2 b) => new Quaternion(ref b);
    
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
    
        public const int RosFixedMessageLength = 32;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 32;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Quaternion";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a779879fadf0160734f906b8c19c7004";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEz3JTQqAQAhA4b2nENq3ik7SBSQcEkonNfo5fbWZ3fd4HU6LBDpX52DNQFI0l4+UYoqi" +
                "WJwZo9LMf+0HJbv+r5hvPUBZjXIc8Gq6m56mE+AFLI5yL20AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public readonly Quaternion Inverse => new(-X, -Y, -Z, W);
        public static Quaternion Identity => new(0, 0, 0, 1);
        public static Quaternion operator *(in Quaternion a, in Quaternion b) => Extensions.Multiply(a, b).Normalized;
        public static Vector3 operator *(in Quaternion q, in Vector3 v) => Extensions.Multiply(q, v);
        public static Point operator *(in Quaternion q, in (double X, double Y, double Z) v) => q * (Vector3) v;
        public static Point operator *(in Quaternion q, in Point v) => q * (Vector3) v;
        public readonly Quaternion Normalized => Extensions.Normalize(this);
        public static implicit operator Quaternion(in (double X, double Y, double Z, double W) p) => new Quaternion(p.X, p.Y, p.Z, p.W);
        public static implicit operator Quaternion(in (Vector3 p, double W) q) => new Quaternion(q.p.X, q.p.Y, q.p.Z, q.W);
        public static Quaternion AngleAxis(double angleInRad, in Vector3 axis) => Extensions.AngleAxis(angleInRad, axis);
        public static Quaternion Rodrigues(in Vector3 rod) => Extensions.Rodrigues(rod);
        public static bool operator !=(in Quaternion a, in Quaternion b) => !(a == b);
        public static bool operator ==(in Quaternion a, in Quaternion b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        public override bool Equals(object? b) => b is Quaternion pb && this == pb;
        public override int GetHashCode() => System.HashCode.Combine(X, Y, Z, W);
    
        public Serializer<Quaternion> CreateSerializer() => new Serializer();
        public Deserializer<Quaternion> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Quaternion>
        {
            public override void RosSerialize(Quaternion msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Quaternion msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Quaternion msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Quaternion msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<Quaternion>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Quaternion msg) => msg = new Quaternion(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Quaternion msg) => msg = new Quaternion(ref b);
        }
    }
}
