/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt32 : IDeserializable<UInt32>, IMessage
    {
        [DataMember (Name = "data")] public uint Data;
    
        /// Constructor for empty message.
        public UInt32()
        {
        }
        
        /// Explicit constructor.
        public UInt32(uint Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt32(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UInt32(ref b);
        
        public UInt32 RosDeserialize(ref ReadBuffer b) => new UInt32(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt32";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "304a39449588c7f8ce2df6e8001c5fce";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNlJISSxJ5AIAYOk1nQwAAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
