/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IMessage, IDeserializable<Vector3>
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
        internal Vector3(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Vector3(ref b);
        
        public readonly Vector3 RosDeserialize(ref ReadBuffer b) => new Vector3(ref b);
        
        public readonly bool Equals(in Vector3 o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Vector3 a, in Vector3 b) => a.Equals(b);
        
        public static bool operator!=(in Vector3 a, in Vector3 b) => !a.Equals(b);
    
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
        [Preserve] public const string RosMessageType = "geometry_msgs/Vector3";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0WQQWrEMAxF9znFh9m0EFJoS+8wuy7KbIsmUTymjhVkTdP09JUTSHcfo/f05RM+brFA" +
                "eVYunK2A8M29iSJmjMqMMlPPHZoTzgaflZxWTEzZYPJPOjhEdTRK7tzKyqMot4iGQbggi7ljoi9Xci5c" +
                "aZpnlxFMKZdEla3PjjxwF7oWy43zPhVz8EE3BM6ssYfGEIed9EXTARMu2wEvLWx8xhJT2jvvy+zGLlGx" +
                "DXjscB6xyh1LPciDYiCjKrry0YuuqfaVFvdafFMElolN18+phPL0LtH5iUuhwP53xZiGrmnGJGRvr/g5" +
                "0nqk3+YP1MrAiH8BAAA=";
                
        public override string ToString() => Extensions.ToString(this);
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
        public static implicit operator Vector3(in (double X, double Y, double Z) p) => new Vector3(p.X, p.Y, p.Z);
    }
}
