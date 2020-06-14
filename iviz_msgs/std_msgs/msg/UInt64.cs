using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/UInt64")]
    public sealed class UInt64 : IMessage
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
        internal UInt64(Buffer b)
        {
            Data = b.Deserialize<ulong>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new UInt64(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 8;
    
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
