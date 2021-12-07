/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3f : IMessage, IDeserializable<Vector3f>
    {
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
        [DataMember (Name = "z")] public float Z;
    
        /// Explicit constructor.
        public Vector3f(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Vector3f(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Vector3f(ref b);
        
        public readonly Vector3f RosDeserialize(ref ReadBuffer b) => new Vector3f(ref b);
        
        public override readonly int GetHashCode() => (X, Y, Z).GetHashCode();
        public override readonly bool Equals(object? o) => o is Vector3f s && Equals(s);
        public readonly bool Equals(Vector3f o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        public static bool operator==(in Vector3f a, in Vector3f b) => a.Equals(b);
        public static bool operator!=(in Vector3f a, in Vector3f b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Vector3f";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSqOBKg7Iq4awqLi4A6Ofahh8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Vector3f Zero = new Vector3f(0, 0, 0);
        public static readonly Vector3f One = new Vector3f(1, 1, 1);
        public static readonly Vector3f UnitX = new Vector3f(1, 0, 0);
        public static readonly Vector3f UnitY = new Vector3f(0, 1, 0);
        public static readonly Vector3f UnitZ = new Vector3f(0, 0, 1);
        public static implicit operator GeometryMsgs.Point(in Vector3f p) => new GeometryMsgs.Point(p.X, p.Y, p.Z);
        public static implicit operator GeometryMsgs.Vector3(in Vector3f p) => new GeometryMsgs.Vector3(p.X, p.Y, p.Z);
        public static Vector3f operator +(in Vector3f v, in Vector3f w) => new Vector3f(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Vector3f operator -(in Vector3f v, in Vector3f w) => new Vector3f(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Vector3f operator *(float f, in Vector3f v) => new Vector3f(f * v.X, f * v.Y, f * v.Z);
        public static Vector3f operator *(in Vector3f v, float f) => new Vector3f(f * v.X, f * v.Y, f * v.Z);
        public static Vector3f operator /(in Vector3f v, float f) => new Vector3f(v.X / f, v.Y / f, v.Z / f);
        public static Vector3f operator -(in Vector3f v) => new Vector3f(-v.X, -v.Y, -v.Z);
        public readonly float Dot(in Vector3f v) => X * v.X + Y * v.Y + Z * v.Z;
        public readonly float SquaredNorm => Dot(this);
        public readonly float Norm => (float)System.Math.Sqrt(SquaredNorm);
        public readonly Vector3f Normalized => this / Norm;
        public readonly Vector3f Cross(in Vector3f v) => new Vector3f(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
        public static implicit operator Vector3f(in (float X, float Y, float Z) p) => new Vector3f(p.X, p.Y, p.Z);
    }
}
