using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Float64 : IMessage
    {
        public double data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Float64()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float64(double data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float64(Buffer b)
        {
            this.data = b.Deserialize<double>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Float64(b);
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
        public const string RosMessageType = "std_msgs/Float64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "fdb28210bfa9d7c91146260178d9a584";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
    }
}
