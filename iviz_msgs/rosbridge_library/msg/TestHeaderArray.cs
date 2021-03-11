/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = "rosbridge_library/TestHeaderArray")]
    public sealed class TestHeaderArray : IDeserializable<TestHeaderArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header[] Header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderArray()
        {
            Header = System.Array.Empty<StdMsgs.Header>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeaderArray(StdMsgs.Header[] Header)
        {
            this.Header = Header;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestHeaderArray(ref Buffer b)
        {
            Header = b.DeserializeArray<StdMsgs.Header>();
            for (int i = 0; i < Header.Length; i++)
            {
                Header[i] = new StdMsgs.Header(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestHeaderArray(ref b);
        }
        
        TestHeaderArray IDeserializable<TestHeaderArray>.RosDeserialize(ref Buffer b)
        {
            return new TestHeaderArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Header, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Header)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2RT2scMQzF74b9DoI95A9sCu1tobeQpIdAILmVsmhtZSzw2FNJs+l8+8qzlDS3HjoY" +
                "Zjx+7/dkSS0dRh300wNhIvn+A/L6sQlf//OzCY/P93vQj4GbsIVnw5pQEoxkmNAQXptA5iGT7AqdqLgL" +
                "x4kSrKe2TKQ3bnzJrOBroEqCpSwwq4usQWzjOFeOaATGI33wu5MrIEwoxnEuKK5vkrh2+avgSJ3uS+nn" +
                "TDUSfLvdu6YqxdnYC1qcEIVQuQ5+CGHmal8+d0PYvry1nW9pIHkPB8tovVj6NQlprxN17xnX58vdONu7" +
                "Q56SFC7Xfwff6hV4SCSgqcUMl17502K5VQcSnFAYj4U6OHoHnHrRTRdXf5F72XuoWNsf/Jn4nvEv2E45" +
                "c/uddtlnVvrtdR68gS6cpJ04ufS4rJBYmKpB4aOgLKG7zpFhe9d77CJ3rRPxN6q2yD6ABG9sOahJp6/T" +
                "OHAKm/AbCTy5n6sCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
