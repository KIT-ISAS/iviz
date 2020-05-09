using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Bool : IMessage
    {
        public bool data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Bool()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Bool(bool data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Bool(Buffer b)
        {
            this.data = BuiltIns.DeserializeStruct<bool>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Bool(b);
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
        public int RosMessageLength => 1;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "std_msgs/Bool";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "8b94c1b53db61fb6aed406028ad6332a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vKz89RSEksSeQCAGFR0NcKAAAA";
                
    }
}
