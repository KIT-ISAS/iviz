using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int16")]
    public sealed class Int16 : IMessage
    {
        [DataMember (Name = "data")] public short Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Int16()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int16(short Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int16(Buffer b)
        {
            Data = b.Deserialize<short>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Int16(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 2;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8524586e34fbd7cb1c08c5f5f1ca0e57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE0U0hJLEnk4gIAJDs+BgwAAAA=";
                
    }
}
