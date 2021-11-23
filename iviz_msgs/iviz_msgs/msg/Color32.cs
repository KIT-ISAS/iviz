/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Color32 : IMessage, System.IEquatable<Color32>, IDeserializable<Color32>
    {
        [DataMember (Name = "r")] public byte R;
        [DataMember (Name = "g")] public byte G;
        [DataMember (Name = "b")] public byte B;
        [DataMember (Name = "a")] public byte A;
    
        /// Explicit constructor.
        public Color32(byte R, byte G, byte B, byte A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Color32(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b) => new Color32(ref b);
        
        readonly Color32 IDeserializable<Color32>.RosDeserialize(ref Buffer b) => new Color32(ref b);
        
        public override readonly int GetHashCode() => (R, G, B, A).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Color32 s && Equals(s);
        
        public readonly bool Equals(Color32 o) => (R, G, B, A) == (o.R, o.G, o.B, o.A);
        
        public static bool operator==(in Color32 a, in Color32 b) => a.Equals(b);
        
        public static bool operator!=(in Color32 a, in Color32 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Color32";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3a89b17adab5bedef0b554f03235d9b3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxUCjiKgXT6VA6CUoncnEBACHBa7shAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
