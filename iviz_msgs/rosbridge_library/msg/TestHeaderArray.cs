
namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestHeaderArray : IMessage
    {
        public std_msgs.Header[] header;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestHeaderArray";
    
        public IMessage Create() => new TestHeaderArray();
    
        public int GetLength()
        {
            int size = 4;
            for (int i = 0; i < header.Length; i++)
            {
                size += header[i].GetLength();
            }
            return size;
        }
    
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq2RT2scMQzF74b9DoI95A9sCu1tobeQpIdAILmVsmhtZSzw2FNJs+l8+8qzlDS3HjoY" +
                "Zjx+7/dkSS0dRh300wNhIvn+A/L6sQlf//OzCY/P93vQj4GbsIVnw5pQEoxkmNAQXptA5iGT7AqdqLgL" +
                "x4kSrKe2TKQ3bnzJrOBroEqCpSwwq4usQWzjOFeOaATGI33wu5MrIEwoxnEuKK5vkrh2+avgSJ3uS+nn" +
                "TDUSfLvdu6YqxdnYC1qcEIVQuQ5+CGHmal8+d0PYvry1nW9pIHkPB8tovVj6NQlprxN17xnX58vdONu7" +
                "Q56SFC7Xfwff6hV4SCSgqcUMl17502K5VQcSnFAYj4U6OHoHnHrRTRdXf5F72XuoWNsf/Jn4nvEv2E45" +
                "c/uddtlnVvrtdR68gS6cpJ04ufS4rJBYmKpB4aOgLKG7zpFhe9d77CJ3rRPxN6q2yD6ABG9sOahJp6/T" +
                "OHAKm/AbCTy5n6sCAAA=";
                
    }
}
