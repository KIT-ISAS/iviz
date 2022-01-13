/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class UInt64 : IDeserializable<UInt64>, IMessage
    {
        [DataMember (Name = "data")] public ulong Data;
    
        /// Constructor for empty message.
        public UInt64()
        {
        }
        
        /// Explicit constructor.
        public UInt64(ulong Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt64(ref ReadBuffer b)
        {
            Data = b.Deserialize<ulong>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UInt64(ref b);
        
        public UInt64 RosDeserialize(ref ReadBuffer b) => new UInt64(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/UInt64";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1b2a79973e8bf53d7b53acb71299cb57";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxM1FISSxJ5AIAPtIFtgwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
