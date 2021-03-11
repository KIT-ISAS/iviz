/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Vector3f")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3f : IMessage, System.IEquatable<Vector3f>, IDeserializable<Vector3f>
    {
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
        [DataMember (Name = "z")] public float Z;
    
        /// <summary> Explicit constructor. </summary>
        public Vector3f(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Vector3f(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Vector3f(ref b);
        }
        
        readonly Vector3f IDeserializable<Vector3f>.RosDeserialize(ref Buffer b)
        {
            return new Vector3f(ref b);
        }
        
        public override readonly int GetHashCode() => (X, Y, Z).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Vector3f s && Equals(s);
        
        public readonly bool Equals(Vector3f o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Vector3f a, in Vector3f b) => a.Equals(b);
        
        public static bool operator!=(in Vector3f a, in Vector3f b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void Dispose()
        {
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Vector3f";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACkvLyU8sMTZSqOBKg7Iq4awqLl4uAPkgwdMgAAAA";
                
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
    }
}
