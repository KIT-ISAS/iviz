/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IMessage, IDeserializable<Point>
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
        internal Point(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Point(ref b);
        
        public readonly Point RosDeserialize(ref ReadBuffer b) => new Point(ref b);
        
        public readonly bool Equals(in Point o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Point a, in Point b) => a.Equals(b);
        
        public static bool operator!=(in Point a, in Point b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 24;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Point";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3HwQmAMAwF0Hum+OAK4iQuEEpCA5KUJgd1ej319t6Gs1uihRebJ6oLRqSVhSMU/M+8" +
                "YA6dIsjBTUiv4Dp23EvP0kv0AQQdt/JVAAAA";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Point Zero = new Point(0, 0, 0);
        public static readonly Point One = new Point(1, 1, 1);
        public static readonly Point UnitX = new Point(1, 0, 0);
        public static readonly Point UnitY = new Point(0, 1, 0);
        public static readonly Point UnitZ = new Point(0, 0, 1);
        public static implicit operator Vector3(in Point p) => new Vector3(p.X, p.Y, p.Z);
        public static Point operator +(in Point v, in Vector3 w) => new Point(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Point operator -(in Point v, in Vector3 w) => new Point(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Point operator *(double f, in Point v) => new Point(f * v.X, f * v.Y, f * v.Z);
        public static Point operator *(in Point v, double f) => new Point(f * v.X, f * v.Y, f * v.Z);
        public static Point operator /(in Point v, double f) => new Point(v.X / f, v.Y / f, v.Z / f);
        public static Point operator -(in Point v) => new Point(-v.X, -v.Y, -v.Z);
        public static implicit operator Point(in (double X, double Y, double Z) p) => new Point(p.X, p.Y, p.Z);
        public readonly double Dot(in Point v) => X * v.X + Y * v.Y + Z * v.Z;
        public readonly double SquaredNorm => Dot(this);
        public readonly double Norm => System.Math.Sqrt(SquaredNorm);
        public readonly Vector3 Normalized => this / Norm;
        public readonly Vector3 Cross(in Point v) => new Point(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
    }
}
