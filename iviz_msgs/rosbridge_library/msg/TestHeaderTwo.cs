using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestHeaderTwo : IMessage
    {
        public std_msgs.Header header;
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderTwo()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        public IMessage Create() => new TestHeaderTwo();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosbridge_library/TestHeaderTwo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62RQWscMQyF7/4Vgj0kKWwC6W2ht9Kmh0IhuS9aW5kReOyppdlk/n2eZ2na3nroYDAe" +
                "v/c9WXoQTtJo3Lbw6T9/4fvj1wOZp+Nkg909XFJ29OhcErdEkzgndqbniiJ0GKXts5wlw8TTLIm2W19n" +
                "sVsYn0Y1whqkSOOcV1oMIq8U6zQtRSO7kOskf/nh1EJMMzfXuGRu0NeWtHT5c+NJOh3L5OciJQp9+3yA" +
                "ppjExRUFrSDEJmxaBlxSWLT4x/tuCLunl7rHUQa08j2cfGTvxcrr3MR6nWwHZHy4PO4WbDRHkJKMrrd/" +
                "RxzthhCCEmSucaRrVP5j9bEWAIXO3JRPWTo4ogOgXnXT1c0f5LKhC5f6C38h/s74F2x55/Y37UfMLPfX" +
                "2zKggRDOrZ41QXpaN0jMKsUp66lxW0N3XSLD7kvvMURwbRPBzmY1KgaQ6EV9DOat07dpHDWF8AaoFDiD" +
                "nAIAAA==";
                
    }
}
