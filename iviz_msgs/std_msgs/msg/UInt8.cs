using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    public sealed class UInt8 : IMessage
    {
        [DataMember] public byte data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt8()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt8(byte data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal UInt8(Buffer b)
        {
            this.data = b.Deserialize<byte>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new UInt8(b ?? throw new System.ArgumentNullException(nameof(b)));
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
        [Preserve] public const string RosMessageType = "std_msgs/UInt8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7c8164229e7d2c17eb95e9231617fdee";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
    }
}
