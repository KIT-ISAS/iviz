using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Byte")]
    public sealed class Byte : IMessage
    {
        [DataMember (Name = "data")] public byte Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Byte()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Byte(byte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Byte(Buffer b)
        {
            Data = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Byte(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 1;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Byte";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ad736a2e8818154c487bb80fe42ce43b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0uqLElVSEksSeTiAgAksd8TCwAAAA==";
                
    }
}
