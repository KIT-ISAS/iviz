using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Char : IMessage
    {
        public sbyte data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Char()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Char(sbyte data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Char(Buffer b)
        {
            this.data = BuiltIns.DeserializeStruct<sbyte>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Char(b);
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
        public const string RosMessageType = "std_msgs/Char";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "1bf77f25acecdedba0e224b162199717";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCxSSEksSeQCADeiGH4KAAAA";
                
    }
}
