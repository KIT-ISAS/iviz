using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    [DataContract]
    public sealed class TestUInt8FixedSizeArray16 : IMessage
    {
        [DataMember] public byte[/*16*/] data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestUInt8FixedSizeArray16()
        {
            data = new byte[16];
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestUInt8FixedSizeArray16(byte[] data)
        {
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
            if (this.data.Length != 16) throw new System.ArgumentException("Invalid size", nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestUInt8FixedSizeArray16(Buffer b)
        {
            this.data = b.DeserializeStructArray<byte>(16);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestUInt8FixedSizeArray16(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.data, 16);
        }
        
        public void Validate()
        {
            if (data is null) throw new System.NullReferenceException();
            if (data.Length != 16) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength => 16;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestUInt8FixedSizeArray16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a4e84d0a73514dfe9696b4796e8755e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxiDY0i1VISSxJ5OICANuquFIQAAAA";
                
    }
}
