using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int32")]
    public sealed class Int32 : IMessage
    {
        [DataMember (Name = "data")] public int Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Int32()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int32(int Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int32(Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Int32(b ?? throw new System.ArgumentNullException(nameof(b)));
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
        [Preserve] public const string RosMessageType = "std_msgs/Int32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UkhJLEnkAgAHaI4xCwAAAA==";
                
    }
}
