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
        internal TestHeader(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestHeader(ref b);
        
        TestHeader IDeserializable<TestHeader>.RosDeserialize(ref Buffer b) => new TestHeader(ref b);
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACq2RT2scMQzF7/4Ugj0kKWwK6W2ht5Kkh0IhuS9aW5kReOyppdl0vn2eZ+mf3HroYDAe" +
                "v/d7smSejpMN9vFROEmjcdvC5//8hW9PDwey92FhR0/OJXFLNIlzYmd6qShCh1HaPstZMkw8zZJou/V1" +
                "FruF8XlUI6xBijTOeaXFIPJKsU7TUjSyC7lO8s4PpxZimrm5xiVzg762pKXLXxpP0ulYJj8WKVHo65cD" +
                "NMUkLq4oaAUhNmHTMuCSwqLFP911Q9g9v9Y9jjKglb/DyUf2Xqz8nJtYr5PtgIwPl8fdgo3mCFKS0fX2" +
                "74ij3RBCUILMNY50jcq/rz7WAqDQmZvyKUsHR3QA1Ktuurr5i9zLPlDhUn/hL8Q/Gf+C7ZQLt79pP2Jm" +
                "ub/elgENhHBu9awJ0tO6QWJWKU5ZT43bGrrrEhl2973HEMG1TQQ7m9WoGECiV/UxmLdO36Zx1BTCG7/R" +
                "jzelAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
