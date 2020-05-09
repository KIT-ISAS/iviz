using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestUInt8FixedSizeArray16 : IMessage
    {
        public byte[/*16*/] data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestUInt8FixedSizeArray16()
        {
            data = new byte[16];
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestUInt8FixedSizeArray16(byte[] data)
        {
            BuiltIns.AssertSize(data, 16);
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestUInt8FixedSizeArray16(Buffer b)
        {
            this.data = BuiltIns.DeserializeStructArray<byte>(b, 16);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestUInt8FixedSizeArray16(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.data, b, 16);
        }
        
        public void Validate()
        {
            BuiltIns.AssertSize(data, 16);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 16;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosbridge_library/TestUInt8FixedSizeArray16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "a4e84d0a73514dfe9696b4796e8755e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxiDY0i1VISSxJ5OICANuquFIQAAAA";
                
    }
}
