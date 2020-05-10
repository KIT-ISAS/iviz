using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Int32 : IMessage
    {
        public int data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Int32()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int32(int data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int32(Buffer b)
        {
            this.data = b.Deserialize<int>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Int32(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 4;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "std_msgs/Int32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UkhJLEnkAgAHaI4xCwAAAA==";
                
    }
}
