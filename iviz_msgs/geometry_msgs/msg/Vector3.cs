/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IMessage, IDeserializable<Vector3>, System.IEquatable<Vector3>
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
    
        /// Explicit constructor.
        public Vector3(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Vector3(ref b);
        
        public readonly Vector3 RosDeserialize(ref ReadBuffer b) => new Vector3(ref b);
        
        public readonly override int GetHashCode() => (X, Y, Z).GetHashCode();
        public readonly override bool Equals(object? o) => o is Vector3 s && Equals(s);
        public readonly bool Equals(Vector3 o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        public static bool operator==(in Vector3 a, in Vector3 b) => a.Equals(b);
        public static bool operator!=(in Vector3 a, in Vector3 b) => !a.Equals(b);
    
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
        public const string MessageType = "geometry_msgs/Vector3";
    
        public readonly string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public readonly string RosMd5Sum => "4a842b65f413084dc2b10fb484ea7f17";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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
        public static implicit operator Point(in Vector3 p) => new(p.X, p.Y, p.Z);
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
    }
}
