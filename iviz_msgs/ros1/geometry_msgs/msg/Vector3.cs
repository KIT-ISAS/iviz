/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IMessage, IHasSerializer<Vector3>
    {
        // This represents a vector in free space. 
        // It is only meant to represent a direction. Therefore, it does not
        // make sense to apply a translation to it (e.g., when applying a 
        // generic rigid transformation to a Vector3, tf2 will only apply the
        // rotation). If you want your data to be translatable too, use the
        // geometry_msgs/Point message instead.
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "z")] public double Z;
    
        public Vector3(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Vector3 RosDeserialize(ref ReadBuffer b) => new Vector3(ref b);
        
        public readonly Vector3 RosDeserialize(ref ReadBuffer2 b) => new Vector3(ref b);
    
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
    
        public const int RosFixedMessageLength = 24;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 24;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Vector3";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0WQQWrEMAxF9znFh9m0EFJoS+8wuy7KbIsmUTymjhVkTdP09JUTSHcfo/f05RM+brFA" +
                "eVYunK2A8M29iSJmjMqMMlPPHZoTzgaflZxWTEzZYPJPOjhEdTRK7tzKyqMot4iGQbggi7ljoi9Xci5c" +
                "aZpnlxFMKZdEla3PjjxwF7oWy43zPhVz8EE3BM6ssYfGEIed9EXTARMu2wEvLWx8xhJT2jvvy+zGLlGx" +
                "DXjscB6xyh1LPciDYiCjKrry0YuuqfaVFvdafFMElolN18+phPL0LtH5iUuhwP53xZiGrmnGJGRvr/g5" +
                "0nqk3+YP1MrAiH8BAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static Vector3 Zero => new();
        public static Vector3 One => new(1, 1, 1);
        public static Vector3 UnitX => new(1, 0, 0);
        public static Vector3 UnitY => new(0, 1, 0);
        public static Vector3 UnitZ => new(0, 0, 1);
        public static bool operator !=(in Vector3 a, in Vector3 b) => !(a == b);
        public static bool operator ==(in Vector3 a, in Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        public static Vector3 operator +(in Vector3 v, in Vector3 w) => new(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Vector3 operator -(in Vector3 v, in Vector3 w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Vector3 operator *(double f, in Vector3 v) => new(f * v.X, f * v.Y, f * v.Z);
        public static Vector3 operator *(in Vector3 v, double f) => new(f * v.X, f * v.Y, f * v.Z);
        public static Vector3 operator /(in Vector3 v, double f) => new(v.X / f, v.Y / f, v.Z / f);
        public static Vector3 operator -(in Vector3 v) => new(-v.X, -v.Y, -v.Z);
        public readonly double SquaredNorm => X * X + Y * Y + Z * Z;
        public readonly double Norm => System.Math.Sqrt(SquaredNorm);
        public readonly Vector3 Normalized => this / Norm;
        public static implicit operator Vector3(in (double X, double Y, double Z) p) => new(p.X, p.Y, p.Z);
        public override bool Equals(object? b) => b is Vector3 pb && this == pb;
        public override int GetHashCode() => System.HashCode.Combine(X, Y, Z);
    
        public Serializer<Vector3> CreateSerializer() => new Serializer();
        public Deserializer<Vector3> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Vector3>
        {
            public override void RosSerialize(Vector3 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Vector3 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Vector3 _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Vector3 _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Vector3>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Vector3 msg) => msg = new Vector3(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Vector3 msg) => msg = new Vector3(ref b);
        }
    }
}
