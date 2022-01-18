/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestHeaderTwo : IDeserializable<TestHeaderTwo>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
    
        /// Constructor for empty message.
        public TestHeaderTwo()
        {
        }
        
        /// Explicit constructor.
        public TestHeaderTwo(in StdMsgs.Header Header)
        {
            this.Header = Header;
        }
        
        /// Constructor with buffer.
        public TestHeaderTwo(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestHeaderTwo(ref b);
        
        public TestHeaderTwo RosDeserialize(ref ReadBuffer b) => new TestHeaderTwo(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderTwo";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62RQWscMQyF7/4Vgj0kKWwC6W2ht9Kmh0IhuS9aW5kReOyppdlk/n2eZ2na3nroYDAe" +
                "v/c9WXoQTtJo3Lbw6T9/4fvj1wOZp+Nkg909XFJ29OhcErdEkzgndqbniiJ0GKXts5wlw8TTLIm2W19n" +
                "sVsYn0Y1whqkSOOcV1oMIq8U6zQtRSO7kOskf/nh1EJMMzfXuGRu0NeWtHT5c+NJOh3L5OciJQp9+3yA" +
                "ppjExRUFrSDEJmxaBlxSWLT4x/tuCLunl7rHUQa08j2cfGTvxcrr3MR6nWwHZHy4PO4WbDRHkJKMrrd/" +
                "RxzthhCCEmSucaRrVP5j9bEWAIXO3JRPWTo4ogOgXnXT1c0f5LKhC5f6C38h/s74F2x55/Y37UfMLPfX" +
                "2zKggRDOrZ41QXpaN0jMKsUp66lxW0N3XSLD7kvvMURwbRPBzmY1KgaQ6EV9DOat07dpHDWF8AaoFDiD" +
                "nAIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
