/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
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
        public UInt16(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UInt16(ref b);
        
        public UInt16 RosDeserialize(ref ReadBuffer b) => new UInt16(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 2;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt16";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "1df79edf208b629fe6b81923a544552d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNFNISSxJ5OICAF50RNUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
