/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IMessage, IDeserializable<Point>, System.IEquatable<Point>
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
        public Point(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Point(ref b);
        
        public readonly Point RosDeserialize(ref ReadBuffer b) => new Point(ref b);
        
        public readonly override int GetHashCode() => (X, Y, Z).GetHashCode();
        public readonly override bool Equals(object? o) => o is Point s && Equals(s);
        public readonly bool Equals(Point o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        public static bool operator==(in Point a, in Point b) => a.Equals(b);
        public static bool operator!=(in Point a, in Point b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 24;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Point";
    
        public readonly string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public readonly string RosMd5Sum => "4a842b65f413084dc2b10fb484ea7f17";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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
        public static Point operator +(in Point v, in Vector3 w) => new(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Point operator -(in Point v, in Vector3 w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Point operator *(double f, in Point v) => new(f * v.X, f * v.Y, f * v.Z);
        public static Point operator *(in Point v, double f) => new(f * v.X, f * v.Y, f * v.Z);
        public static Point operator /(in Point v, double f) => new(v.X / f, v.Y / f, v.Z / f);
        public static Point operator -(in Point v) => new(-v.X, -v.Y, -v.Z);
        public static implicit operator Point(in (double X, double Y, double Z) p) => new(p.X, p.Y, p.Z);
        public readonly double SquaredNorm => X * X + Y * Y + Z * Z;
        public readonly double Norm => System.Math.Sqrt(SquaredNorm);
        public readonly Vector3 Normalized => this / Norm;
    }
}
