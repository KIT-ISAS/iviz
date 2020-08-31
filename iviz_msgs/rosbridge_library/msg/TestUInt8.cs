using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestUInt8")]
    public sealed class TestUInt8 : IMessage
    {
        [DataMember (Name = "data")] public byte[] Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestUInt8()
        {
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestUInt8(byte[] Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestUInt8(Buffer b)
        {
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TestUInt8(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestUInt8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f43a8e1b362b75baa741461b46adc7e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxiI5VSEksSeQCANR1vBgNAAAA";
                
    }
}
