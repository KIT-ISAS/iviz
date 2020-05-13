using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    public sealed class Int64 : IMessage
    {
        [DataMember] public long data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Int64()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int64(long data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int64(Buffer b)
        {
            this.data = b.Deserialize<long>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Int64(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 8;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "34add168574510e6e17f5d23ecc077ef";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUUhJLEnkAgBZU74aCwAAAA==";
                
    }
}
