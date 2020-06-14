using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestChar")]
    public sealed class TestChar : IMessage
    {
        [DataMember (Name = "data")] public sbyte[] Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestChar()
        {
            Data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestChar(sbyte[] Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestChar(Buffer b)
        {
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TestChar(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException();
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
        [Preserve] public const string RosMessageType = "rosbridge_library/TestChar";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7b8d15902c8b049d5a32b4cb73fa86f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCyKjlVISSxJ5AIAudt/QwwAAAA=";
                
    }
}
