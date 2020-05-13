using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    [DataContract]
    public sealed class TestHeaderArray : IMessage
    {
        [DataMember] public std_msgs.Header[] header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderArray()
        {
            header = System.Array.Empty<std_msgs.Header>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeaderArray(std_msgs.Header[] header)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestHeaderArray(Buffer b)
        {
            this.header = b.DeserializeArray<std_msgs.Header>();
            for (int i = 0; i < this.header.Length; i++)
            {
                this.header[i] = new std_msgs.Header(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestHeaderArray(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.header, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            for (int i = 0; i < header.Length; i++)
            {
                if (header[i] is null) throw new System.NullReferenceException();
                header[i].Validate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                for (int i = 0; i < header.Length; i++)
                {
                    size += header[i].RosMessageLength;
                }
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62RTWvkMAyG7/4Vgjn0A6YL29tAb2U/DoWF9raUQWOrscCxU0mZbv79yhl22956aDAk" +
                "jt/3eWVJLe1HHfTLD8JE8vsR8voRbj75CXf333eg7+PCBu4Na0JJMJJhQkN4agKZh0yyLXSk4iYcJ0qw" +
                "ntoykV658SGzgq+BKgmWssCsLrIGsY3jXDmiERiP9M7vTq6AMKEYx7mguL5J4trlT4IjdbovpeeZaiT4" +
                "ebtzTVWKs7EXtDghCqFyHfwQwszVrr92Q9g8vLStb2kgeQ0Hy2i9WPozCWmvE3XnGZeny10525tDnpIU" +
                "ztd/e9/qBXiIl0BTixnOvfJfi+VWHUhwRGE8FOrg6B1w6lk3nV28IdcVXbG2f/gT8TXjI9j6n9vvtM0+" +
                "s9Jvr/PgDXThJO3IyaWHZYXEwlQNCh8EZQnddYoMm2+9xy5y1zoRf6Nqi+wDSPDCloOadPo6jT2nEP4C" +
                "0StuzqcCAAA=";
                
    }
}
