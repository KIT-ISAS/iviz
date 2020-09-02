/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestHeaderTwo")]
    public sealed class TestHeaderTwo : IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderTwo()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeaderTwo(StdMsgs.Header Header)
        {
            this.Header = Header;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestHeaderTwo(Buffer b)
        {
            Header = new StdMsgs.Header(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TestHeaderTwo(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Header.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderTwo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62RQWscMQyF7/4Vgj0kKWwC6W2ht9Kmh0IhuS9aW5kReOyppdlk/n2eZ2na3nroYDAe" +
                "v/c9WXoQTtJo3Lbw6T9/4fvj1wOZp+Nkg909XFJ29OhcErdEkzgndqbniiJ0GKXts5wlw8TTLIm2W19n" +
                "sVsYn0Y1whqkSOOcV1oMIq8U6zQtRSO7kOskf/nh1EJMMzfXuGRu0NeWtHT5c+NJOh3L5OciJQp9+3yA" +
                "ppjExRUFrSDEJmxaBlxSWLT4x/tuCLunl7rHUQa08j2cfGTvxcrr3MR6nWwHZHy4PO4WbDRHkJKMrrd/" +
                "RxzthhCCEmSucaRrVP5j9bEWAIXO3JRPWTo4ogOgXnXT1c0f5LKhC5f6C38h/s74F2x55/Y37UfMLPfX" +
                "2zKggRDOrZ41QXpaN0jMKsUp66lxW0N3XSLD7kvvMURwbRPBzmY1KgaQ6EV9DOat07dpHDWF8AaoFDiD" +
                "nAIAAA==";
                
    }
}
