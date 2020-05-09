using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Float32 : IMessage
    {
        public float data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Float32()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float32(float data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float32(Buffer b)
        {
            this.data = BuiltIns.DeserializeStruct<float>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Float32(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.data, b);
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
        public const string RosMessageType = "std_msgs/Float32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "73fcbf46b49191e672908e50842a83d4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSSEksSeQCAK0qjc8NAAAA";
                
    }
}
