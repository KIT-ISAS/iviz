/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestHeaderArray : IDeserializable<TestHeaderArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header[] Header;
    
        /// Constructor for empty message.
        public TestHeaderArray()
        {
            Header = System.Array.Empty<StdMsgs.Header>();
        }
        
        /// Explicit constructor.
        public TestHeaderArray(StdMsgs.Header[] Header)
        {
            this.Header = Header;
        }
        
        /// Constructor with buffer.
        public TestHeaderArray(ref ReadBuffer b)
        {
            Header = b.DeserializeArray<StdMsgs.Header>();
            for (int i = 0; i < Header.Length; i++)
            {
                StdMsgs.Header.Deserialize(ref b, out Header[i]);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestHeaderArray(ref b);
        
        public TestHeaderArray RosDeserialize(ref ReadBuffer b) => new TestHeaderArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Header);
        }
        
        public void RosValidate()
        {
            if (Header is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Header);
    
        public string RosType => RosMessageType;
    
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
