using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/UInt32")]
    public sealed class UInt32 : IMessage
    {
        [DataMember (Name = "data")] public uint Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt32()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt32(uint Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal UInt32(Buffer b)
        {
            Data = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new UInt32(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 4;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "304a39449588c7f8ce2df6e8001c5fce";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlJISSxJ5AIAYOk1nQwAAAA=";
                
    }
}
