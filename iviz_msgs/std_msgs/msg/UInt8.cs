/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal UInt8(ref Buffer b)
        {
            Data = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new UInt8(ref b);
        
        UInt8 IDeserializable<UInt8>.RosDeserialize(ref Buffer b) => new UInt8(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/UInt8";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7c8164229e7d2c17eb95e9231617fdee";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACivNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
