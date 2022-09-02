/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IMessage, IDeserializable<Point>
    {
        // This contains the position of a point in free space
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "z")] public double Z;
    
        public Point(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Point RosDeserialize(ref ReadBuffer b) => new Point(ref b);
        
        public readonly Point RosDeserialize(ref ReadBuffer2 b) => new Point(ref b);
    
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
        
    
        public const string MessageType = "geometry_msgs/Point";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEz3HwQmAMAwF0Hum+OAK4iQuEEpCA5KUJgd1ej319t6Gs1uihRebJ6oLRqSVhSMU/M+8" +
                "YA6dIsjBTUiv4Dp23EvP0kv0AQQdt/JVAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static Point Zero => new();
        public static Point One => new(1, 1, 1);
        public static Point UnitX => new(1, 0, 0);
        public static Point UnitY => new(0, 1, 0);
        public static Point UnitZ => new(0, 0, 1);
        public static implicit operator Vector3(in Point p) => new(p.X, p.Y, p.Z);
        public static implicit operator Point(in Vector3 p) => new(p.X, p.Y, p.Z);
        public static bool operator !=(in Point a, in Point b) => !(a == b);
        public static bool operator ==(in Point a, in Point b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        public static Point operator +(in Point v, in Vector3 w) => new(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Point operator -(in Point v, in Vector3 w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Vector3 operator -(in Point v, in Point w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Point operator *(double f, in Point v) => new(f * v.X, f * v.Y, f * v.Z);
        public static Point operator *(in Point v, double f) => new(f * v.X, f * v.Y, f * v.Z);
        public static Point operator /(in Point v, double f) => new(v.X / f, v.Y / f, v.Z / f);
        public static Point operator -(in Point v) => new(-v.X, -v.Y, -v.Z);
        public static implicit operator Point(in (double X, double Y, double Z) p) => new(p.X, p.Y, p.Z);
        public readonly double SquaredNorm => X * X + Y * Y + Z * Z;
        public readonly double Norm => System.Math.Sqrt(SquaredNorm);
        public readonly Vector3 Normalized => this / Norm;
        public override bool Equals(object? b) => b is Point pb && this == pb;
        public override int GetHashCode() => System.HashCode.Combine(X, Y, Z);
    }
}
