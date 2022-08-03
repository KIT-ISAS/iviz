/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2f : IMessage, IDeserializable<Vector2f>
    {
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
    
        public Vector2f(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2f(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2f(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Vector2f RosDeserialize(ref ReadBuffer b) => new Vector2f(ref b);
        
        public readonly Vector2f RosDeserialize(ref ReadBuffer2 b) => new Vector2f(ref b);
    
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
    
        public const int RosFixedMessageLength = 8;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly void AddRos2MessageLength(ref int c) => WriteBuffer2.AddLength(ref c, this);
        
    
        public const string MessageType = "iviz_msgs/Vector2f";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ff8d7d66dd3e4b731ef14a45d38888b6";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTZSqOBKg7IqubgAEeFgKBUAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static Vector2f Zero => new(0, 0);
        public static Vector2f One => new(1, 1);
        public static Vector2f UnitX => new(1, 0);
        public static Vector2f UnitY => new(0, 1);
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
