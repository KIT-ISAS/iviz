using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    public sealed class Int16 : IMessage
    {
        [DataMember] public short data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Int16()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int16(short data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int16(Buffer b)
        {
            this.data = b.Deserialize<short>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Int16(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 2;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8524586e34fbd7cb1c08c5f5f1ca0e57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE0U0hJLEnk4gIAJDs+BgwAAAA=";
                
    }
}
