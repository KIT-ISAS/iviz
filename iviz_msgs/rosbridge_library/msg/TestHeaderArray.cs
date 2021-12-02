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
        internal TestHeaderArray(ref Buffer b)
        {
            Header = b.DeserializeArray<StdMsgs.Header>();
            for (int i = 0; i < Header.Length; i++)
            {
                StdMsgs.Header.Deserialize(ref b, out Header[i]);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestHeaderArray(ref b);
        
        TestHeaderArray IDeserializable<TestHeaderArray>.RosDeserialize(ref Buffer b) => new TestHeaderArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Header);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Header);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2RQWscMQyF7/4Vgj0kKWwK7W0ht5I2h0AguYWyaG3tWOCxJ5Jmk/n3lWcpaW49ZDDM" +
                "ePze92RJLe1HHfTrL8JE8vwb8voRbj75CfePP3egH+PCBh4Na0JJMJJhQkM4NoHMQybZFjpRcROOEyVY" +
                "T22ZSK/d+JRZwddAlQRLWWBWF1mD2MZxrhzRCIxH+uB3J1dAmFCM41xQXN8kce3yo+BIne5L6WWmGgnu" +
                "fuxcU5XibOwFLU6IQqhcBz+EMHO179+6IWyeXtvWtzSQvIeDZbReLL1NQtrrRN15xpfz5a6d7c0hT0kK" +
                "l+u/vW/1CjzES6CpxQyXXvnDYrlVBxKcUBgPhTo4egecetFNF1f/kHvZO6hY21/8mfie8T/YTjlz+522" +
                "2WdW+u11HryBLpyknTi59LCskFiYqkHhg6AsobvOkWFz23vsInetE/E3qrbIPoAEr2w5qEmnr9PYcwrh" +
                "D9Erbs6nAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
