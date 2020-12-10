/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/UInt64")]
    [StructLayout(LayoutKind.Sequential)]
    public struct UInt64 : IMessage, System.IEquatable<UInt64>, IDeserializable<UInt64>
    {
        [DataMember (Name = "data")] public ulong Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public UInt64(ulong Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt64(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt64(ref b);
        }
        
        readonly UInt64 IDeserializable<UInt64>.RosDeserialize(ref Buffer b)
        {
            return new UInt64(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is UInt64 s && Equals(s);
        
        public readonly bool Equals(UInt64 o) => (Data) == (o.Data);
        
        public static bool operator==(in UInt64 a, in UInt64 b) => a.Equals(b);
        
        public static bool operator!=(in UInt64 a, in UInt64 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 8;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1b2a79973e8bf53d7b53acb71299cb57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxM1FISSxJ5AIAPtIFtgwAAAA=";
                
    }
}
