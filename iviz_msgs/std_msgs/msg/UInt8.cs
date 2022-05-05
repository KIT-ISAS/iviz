/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt8 : IDeserializable<UInt8>, IMessage
    {
        [DataMember (Name = "data")] public byte Data;
    
        /// Constructor for empty message.
        public UInt8()
        {
        }
        
        /// Explicit constructor.
        public UInt8(byte Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt8(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UInt8(ref b);
        
        public UInt8 RosDeserialize(ref ReadBuffer b) => new UInt8(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt8";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "7c8164229e7d2c17eb95e9231617fdee";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
