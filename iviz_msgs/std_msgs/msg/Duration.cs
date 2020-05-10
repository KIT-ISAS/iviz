using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Duration : IMessage
    {
        public duration data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Duration()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Duration(duration data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Duration(Buffer b)
        {
            this.data = b.Deserialize<duration>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Duration(b);
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
        public int RosMessageLength => 8;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "std_msgs/Duration";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "3e286caf4241d664e55f3ad380e2ae46";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPU0hJLEnk4gIAtVhIcg8AAAA=";
                
    }
}
