/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorRGBA : IMessageRos1, IMessageRos2, IDeserializableRos1<ColorRGBA>, IDeserializableRos2<ColorRGBA>
    {
        [DataMember (Name = "r")] public float R;
        [DataMember (Name = "g")] public float G;
        [DataMember (Name = "b")] public float B;
        [DataMember (Name = "a")] public float A;
    
        /// Explicit constructor.
        public ColorRGBA(float R, float G, float B, float A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorRGBA(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorRGBA(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new ColorRGBA(ref b);
        
        public readonly ColorRGBA RosDeserialize(ref ReadBuffer b) => new ColorRGBA(ref b);
        
        public readonly ColorRGBA RosDeserialize(ref ReadBuffer2 b) => new ColorRGBA(ref b);
    
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 16;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 16;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly void AddRos2MessageLength(ref int c) => WriteBuffer2.AddLength(ref c, this);
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/ColorRGBA";
    
        public readonly string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a29a96539573343b1310c73607334b00";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTZSKOJKg7LS4awkOCuRiwsAZHVNWikAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static ColorRGBA White => new(1, 1, 1, 1);
        public static ColorRGBA Black => new(0, 0, 0, 1);
        public static ColorRGBA Red => new(1, 0, 0, 1);
        public static ColorRGBA Green => new(0, 1, 0, 1);
        public static ColorRGBA Blue => new(0, 0, 1, 1);
        public static ColorRGBA Yellow => new(1, 1, 0, 1);
        public static ColorRGBA Cyan => new(0, 1, 1, 1);
        public static ColorRGBA Magenta => new(1, 0, 1, 1);
        public static ColorRGBA Grey => new(0.5f, 0.5f, 0.5f, 1);
        public static ColorRGBA operator *(in ColorRGBA v, in ColorRGBA w) => new(v.R * w.R, v.G * w.G, v.B * w.B, v.A * w.A);
        public static implicit operator ColorRGBA(in (float R, float G, float B, float A) p) => new(p.R, p.G, p.B, p.A);
        public static implicit operator ColorRGBA(in ((float R, float G, float B) p, float A) q) => new(q.p.R, q.p.G, q.p.B, q.A);
        public static implicit operator ColorRGBA(in (float R, float G, float B) p) => new(p.R, p.G, p.B, 1);
        public (float R, float G, float B) RGB { readonly get => (R, G, B); set => (R, G, B) = value; }
    }
}
