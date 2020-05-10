using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class String : IMessage
    {
        public string data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public String()
        {
            data = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public String(string data)
        {
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal String(Buffer b)
        {
            this.data = b.DeserializeString();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new String(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
            if (data is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(data);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "std_msgs/String";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "992ce8a1687cec8c8bd883ec73ca41d1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1dISSxJ5OICADpmzaUNAAAA";
                
    }
}
