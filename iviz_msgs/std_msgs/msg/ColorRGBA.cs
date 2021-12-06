/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorRGBA : IMessage, IDeserializable<ColorRGBA>
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
        internal ColorRGBA(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ColorRGBA(ref b);
        
        public readonly ColorRGBA RosDeserialize(ref ReadBuffer b) => new ColorRGBA(ref b);
        
        public readonly bool Equals(in ColorRGBA o) => (R, G, B, A) == (o.R, o.G, o.B, o.A);
        
        public static bool operator==(in ColorRGBA a, in ColorRGBA b) => a.Equals(b);
        
        public static bool operator!=(in ColorRGBA a, in ColorRGBA b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 16;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/ColorRGBA";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a29a96539573343b1310c73607334b00";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSKOJKg7LS4awkOCuRiwsAZHVNWikAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly ColorRGBA White = (1, 1, 1, 1);
        public static readonly ColorRGBA Black = (0, 0, 0, 1);
        public static readonly ColorRGBA Red = (1, 0, 0, 1);
        public static readonly ColorRGBA Green = (0, 1, 0, 1);
        public static readonly ColorRGBA Blue = (0, 0, 1, 1);
        public static readonly ColorRGBA Yellow = (1, 1, 0, 1);
        public static readonly ColorRGBA Cyan = (0, 1, 1, 1);
        public static readonly ColorRGBA Magenta = (1, 0, 1, 1);
        public static readonly ColorRGBA Grey = (0.5f, 0.5f, 0.5f, 1);
        public static ColorRGBA operator *(in ColorRGBA v, in ColorRGBA w) => new ColorRGBA(v.R * w.R, v.G * w.G, v.B * w.B, v.A * w.A);
        public static implicit operator ColorRGBA(in (float R, float G, float B, float A) p) => new ColorRGBA(p.R, p.G, p.B, p.A);
        public static implicit operator ColorRGBA(in ((float R, float G, float B) p, float A) q) => new ColorRGBA(q.p.R, q.p.G, q.p.B, q.A);
        public static implicit operator ColorRGBA(in (float R, float G, float B) p) => new ColorRGBA(p.R, p.G, p.B, 1);
        public (float R, float G, float B) RGB { readonly get => (R, G, B); set => (R, G, B) = value; }
    }
}
