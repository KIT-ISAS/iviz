using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    public sealed class Char : IMessage
    {
        [DataMember] public sbyte data { get; set; }
    
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
            this.data = b.Deserialize<sbyte>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Char(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 1;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Char";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1bf77f25acecdedba0e224b162199717";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCxSSEksSeQCADeiGH4KAAAA";
                
    }
}
