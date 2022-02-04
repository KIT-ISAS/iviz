/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2f : IMessage, IDeserializable<Vector2f>, System.IEquatable<Vector2f>
    {
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
    
        /// Explicit constructor.
        public Vector2f(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2f(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Vector2f(ref b);
        
        public readonly Vector2f RosDeserialize(ref ReadBuffer b) => new Vector2f(ref b);
        
        public readonly override int GetHashCode() => (X, Y).GetHashCode();
        public readonly override bool Equals(object? o) => o is Vector2f s && Equals(s);
        public readonly bool Equals(Vector2f o) => (X, Y) == (o.X, o.Y);
        public static bool operator==(in Vector2f a, in Vector2f b) => a.Equals(b);
        public static bool operator!=(in Vector2f a, in Vector2f b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Vector2f";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ff8d7d66dd3e4b731ef14a45d38888b6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSqOBKg7IqubgAEeFgKBUAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Vector2f Zero = new(0, 0);
        public static readonly Vector2f One = new(1, 1);
        public static readonly Vector2f UnitX = new(1, 0);
        public static readonly Vector2f UnitY = new(0, 1);
        public static Vector2f operator +(in Vector2f v, in Vector2f w) => new(v.X + w.X, v.Y + w.Y);
        public static Vector2f operator -(in Vector2f v, in Vector2f w) => new(v.X - w.X, v.Y - w.Y);
        public static Vector2f operator *(float f, in Vector2f v) => new(f * v.X, f * v.Y);
        public static Vector2f operator *(in Vector2f v, float f) => new(f * v.X, f * v.Y);
        public static Vector2f operator /(in Vector2f v, float f) => new(v.X / f, v.Y / f);
        public static Vector2f operator -(in Vector2f v) => new(-v.X, -v.Y);
        public readonly float Dot(in Vector2f v) => X * v.X + Y * v.Y;
        public readonly float SquaredNorm => Dot(this);
        public readonly float Norm => (float)System.Math.Sqrt(SquaredNorm);
        public readonly Vector2f Normalized => this / Norm;
        public static implicit operator Vector2f(in (float X, float Y) p) => new(p.X, p.Y);
    }
}
