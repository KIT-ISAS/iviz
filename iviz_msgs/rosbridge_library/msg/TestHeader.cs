/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestHeader : IDeserializable<TestHeader>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
    
        /// Constructor for empty message.
        public TestHeader()
        {
        }
        
        /// Explicit constructor.
        public TestHeader(in StdMsgs.Header Header)
        {
            this.Header = Header;
        }
        
        /// Constructor with buffer.
        public TestHeader(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestHeader(ref b);
        
        public TestHeader RosDeserialize(ref ReadBuffer b) => new TestHeader(ref b);
    
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
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeader";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62RQWscMQyF7/4Vgj0kKWwKzW2ht9Kmh0IhuS9aW5kReOyJpdl0/n2eZ2na3HroYDAe" +
                "v/c9WTJPx8kG+3gvnKTRuG3h83/+wo+Hbwey92FhRw/OJXFLNIlzYmd6qihCh1HaPstZMkw8zZJou/V1" +
                "FruF8XFUI6xBijTOeaXFIPJKsU7TUjSyC7lO8s4PpxZimrm5xiVzg762pKXLnxpP0ulYJs+LlCj0/csB" +
                "mmISF1cUtIIQm7BpGXBJYdHid5+6IeweX+oeRxnQyrdw8pG9Fyu/5ibW62Q7IOPD5XG3YKM5gpRkdL39" +
                "O+JoN4QQlCBzjSNdo/Kfq4+1ACh05qZ8ytLBER0A9aqbrm7+IpcNXbjU3/gL8U/Gv2DLG7e/aT9iZrm/" +
                "3pYBDYRwbvWsCdLTukFiVilOWU+N2xq66xIZdl97jyGCa5sIdjarUTGARC/qYzBvnb5N46gphFe/0Y83" +
                "pQIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
