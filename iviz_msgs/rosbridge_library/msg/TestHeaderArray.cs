namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestHeaderArray : IMessage
    {
        public std_msgs.Header[] header;
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderArray()
        {
            header = System.Array.Empty<std_msgs.Header>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out header, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(header, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 4;
            for (int i = 0; i < header.Length; i++)
            {
                size += header[i].GetLength();
            }
            return size;
        }
    
        public IMessage Create() => new TestHeaderArray();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestHeaderArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE62RTWvkMAyG7/4Vgjn0A6YL29tAb2U/DoWF9raUQWOrscCxU0mZbv79yhl22956aDAk" +
                "jt/3eWVJLe1HHfTLD8JE8vsR8voRbj75CXf333eg7+PCBu4Na0JJMJJhQkN4agKZh0yyLXSk4iYcJ0qw" +
                "ntoykV658SGzgq+BKgmWssCsLrIGsY3jXDmiERiP9M7vTq6AMKEYx7mguL5J4trlT4IjdbovpeeZaiT4" +
                "ebtzTVWKs7EXtDghCqFyHfwQwszVrr92Q9g8vLStb2kgeQ0Hy2i9WPozCWmvE3XnGZeny10525tDnpIU" +
                "ztd/e9/qBXiIl0BTixnOvfJfi+VWHUhwRGE8FOrg6B1w6lk3nV28IdcVXbG2f/gT8TXjI9j6n9vvtM0+" +
                "s9Jvr/PgDXThJO3IyaWHZYXEwlQNCh8EZQnddYoMm2+9xy5y1zoRf6Nqi+wDSPDCloOadPo6jT2nEP4C" +
                "0StuzqcCAAA=";
                
    }
}
