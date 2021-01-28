/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Vector3")]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vector3 : IMessage, System.IEquatable<Vector3>, IDeserializable<Vector3>
    {
        // This represents a vector in free space. 
        // It is only meant to represent a direction. Therefore, it does not
        // make sense to apply a translation to it (e.g., when applying a 
        // generic rigid transformation to a Vector3, tf2 will only apply the
        // rotation). If you want your data to be translatable too, use the
        // geometry_msgs/Point message instead.
        [DataMember (Name = "x")] public double X { get; }
        [DataMember (Name = "y")] public double Y { get; }
        [DataMember (Name = "z")] public double Z { get; }
    
        /// <summary> Explicit constructor. </summary>
        public Vector3(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Vector3(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Vector3(ref b);
        }
        
        readonly Vector3 IDeserializable<Vector3>.RosDeserialize(ref Buffer b)
        {
            return new Vector3(ref b);
        }
        
        public override readonly int GetHashCode() => (X, Y, Z).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Vector3 s && Equals(s);
        
        public readonly bool Equals(Vector3 o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Vector3 a, in Vector3 b) => a.Equals(b);
        
        public static bool operator!=(in Vector3 a, in Vector3 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 24;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Vector3";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA0WQQWrEMAxF9znFh9m0EFJoS+8wuy7KbIsmUTymjhVkTdP09JUTSHcfo/f05RM+brFA" +
                "eVYunK2A8M29iSJmjMqMMlPPHZoTzgaflZxWTEzZYPJPOjhEdTRK7tzKyqMot4iGQbggi7ljoi9Xci5c" +
                "aZpnlxFMKZdEla3PjjxwF7oWy43zPhVz8EE3BM6ssYfGEIed9EXTARMu2wEvLWx8xhJT2jvvy+zGLlGx" +
                "DXjscB6xyh1LPciDYiCjKrry0YuuqfaVFvdafFMElolN18+phPL0LtH5iUuhwP53xZiGrmnGJGRvr/g5" +
                "0nqk3+YP1MrAiH8BAAA=";
                
        /// Custom iviz code
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);
        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);
        public static implicit operator Point(in Vector3 p) => new Point(p.X, p.Y, p.Z);
        public static Vector3 operator +(in Vector3 v, in Vector3 w) => new Vector3(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Vector3 operator -(in Vector3 v, in Vector3 w) => new Vector3(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Vector3 operator *(double f, in Vector3 v) => new Vector3(f * v.X, f * v.Y, f * v.Z);
        public static Vector3 operator *(in Vector3 v, double f) => new Vector3(f * v.X, f * v.Y, f * v.Z);
        public static Vector3 operator /(in Vector3 v, double f) => new Vector3(v.X / f, v.Y / f, v.Z / f);
        public static Vector3 operator -(in Vector3 v) => new Vector3(-v.X, -v.Y, -v.Z);
        public readonly double Dot(in Vector3 v) => X * v.X + Y * v.Y + Z * v.Z;
        public readonly double SquaredNorm => Dot(this);
        public readonly double Norm => System.Math.Sqrt(SquaredNorm);
        public readonly Vector3 Normalized => this / Norm;
        public readonly Vector3 Cross(in Vector3 v) => new Vector3(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
        public static implicit operator Vector3((double X, double Y, double Z) p) => new Vector3(p.X, p.Y, p.Z);
    }
}
