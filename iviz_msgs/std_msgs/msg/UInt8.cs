/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/UInt8")]
    [StructLayout(LayoutKind.Sequential)]
    public struct UInt8 : IMessage, System.IEquatable<UInt8>, IDeserializable<UInt8>
    {
        [DataMember (Name = "data")] public byte Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public UInt8(byte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal UInt8(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt8(ref b);
        }
        
        readonly UInt8 IDeserializable<UInt8>.RosDeserialize(ref Buffer b)
        {
            return new UInt8(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is UInt8 s && Equals(s);
        
        public readonly bool Equals(UInt8 o) => (Data) == (o.Data);
        
        public static bool operator==(in UInt8 a, in UInt8 b) => a.Equals(b);
        
        public static bool operator!=(in UInt8 a, in UInt8 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 1;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7c8164229e7d2c17eb95e9231617fdee";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
    }
}
