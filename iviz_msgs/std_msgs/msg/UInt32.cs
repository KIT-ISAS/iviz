/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/UInt32")]
    [StructLayout(LayoutKind.Sequential)]
    public struct UInt32 : IMessage, System.IEquatable<UInt32>, IDeserializable<UInt32>
    {
        [DataMember (Name = "data")] public uint Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public UInt32(uint Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal UInt32(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt32(ref b);
        }
        
        readonly UInt32 IDeserializable<UInt32>.RosDeserialize(ref Buffer b)
        {
            return new UInt32(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object o) => o is UInt32 s && Equals(s);
        
        public readonly bool Equals(UInt32 o) => (Data) == (o.Data);
        
        public static bool operator==(in UInt32 a, in UInt32 b) => a.Equals(b);
        
        public static bool operator!=(in UInt32 a, in UInt32 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 4;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "304a39449588c7f8ce2df6e8001c5fce";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlJISSxJ5AIAYOk1nQwAAAA=";
                
    }
}
