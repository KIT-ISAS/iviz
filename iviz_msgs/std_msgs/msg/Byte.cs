/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Byte")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Byte : IMessage, System.IEquatable<Byte>, IDeserializable<Byte>
    {
        [DataMember (Name = "data")] public byte Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Byte(byte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Byte(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Byte(ref b);
        }
        
        readonly Byte IDeserializable<Byte>.RosDeserialize(ref Buffer b)
        {
            return new Byte(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Byte s && Equals(s);
        
        public readonly bool Equals(Byte o) => (Data) == (o.Data);
        
        public static bool operator==(in Byte a, in Byte b) => a.Equals(b);
        
        public static bool operator!=(in Byte a, in Byte b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 1;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Byte";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ad736a2e8818154c487bb80fe42ce43b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0uqLElVSEksSeTiAgAksd8TCwAAAA==";
                
    }
}
