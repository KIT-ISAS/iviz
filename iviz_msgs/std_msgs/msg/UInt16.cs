/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class UInt16 : IDeserializable<UInt16>, IMessage
    {
        [DataMember (Name = "data")] public ushort Data;
    
        /// Constructor for empty message.
        public UInt16()
        {
        }
        
        /// Explicit constructor.
        public UInt16(ushort Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal UInt16(ref Buffer b)
        {
            Data = b.Deserialize<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new UInt16(ref b);
        
        UInt16 IDeserializable<UInt16>.RosDeserialize(ref Buffer b) => new UInt16(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 2;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/UInt16";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1df79edf208b629fe6b81923a544552d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNFNISSxJ5OICAF50RNUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
