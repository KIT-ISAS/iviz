using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    [DataContract]
    public sealed class Num : IMessage
    {
        [DataMember] public long num { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Num()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Num(long num)
        {
            this.num = num;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Num(Buffer b)
        {
            this.num = b.Deserialize<long>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Num(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.num);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 8;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/Num";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "57d3c40ec3ac3754af76a83e6e73127a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUcgrzeXiAgCjoYsaCwAAAA==";
                
    }
}
