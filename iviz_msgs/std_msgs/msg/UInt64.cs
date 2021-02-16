/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt64")]
    public sealed class UInt64 : IDeserializable<UInt64>, IMessage
    {
        [DataMember (Name = "data")] public ulong Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt64()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt64(ulong Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt64(ref Buffer b)
        {
            Data = b.Deserialize<ulong>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt64(ref b);
        }
        
        UInt64 IDeserializable<UInt64>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1b2a79973e8bf53d7b53acb71299cb57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxM1FISSxJ5AIAPtIFtgwAAAA=";
                
    }
}
