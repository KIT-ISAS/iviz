using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    [DataContract]
    public sealed class TestChar : IMessage
    {
        [DataMember] public sbyte[] data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestChar()
        {
            data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestChar(sbyte[] data)
        {
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestChar(Buffer b)
        {
            this.data = b.DeserializeStructArray<sbyte>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestChar(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.data, 0);
        }
        
        public void Validate()
        {
            if (data is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 1 * data.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestChar";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7b8d15902c8b049d5a32b4cb73fa86f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCyKjlVISSxJ5AIAudt/QwwAAAA=";
                
    }
}
