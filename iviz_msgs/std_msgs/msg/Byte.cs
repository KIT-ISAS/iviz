using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Byte : IMessage
    {
        public byte data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Byte()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Byte(byte data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Byte(Buffer b)
        {
            this.data = BuiltIns.DeserializeStruct<byte>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Byte(b);
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
        public const string RosMessageType = "std_msgs/Byte";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "ad736a2e8818154c487bb80fe42ce43b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0uqLElVSEksSeTiAgAksd8TCwAAAA==";
                
    }
}
