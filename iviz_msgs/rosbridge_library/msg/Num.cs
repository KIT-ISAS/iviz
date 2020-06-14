using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/Num")]
    public sealed class Num : IMessage
    {
        [DataMember (Name = "num")] public long Num_ { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Num()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Num(long Num_)
        {
            this.Num_ = Num_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Num(Buffer b)
        {
            Num_ = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Num(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Num_);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 8;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/Num";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "57d3c40ec3ac3754af76a83e6e73127a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUcgrzeXiAgCjoYsaCwAAAA==";
                
    }
}
