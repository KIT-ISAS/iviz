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
        internal TestHeaderTwo(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestHeaderTwo(ref b);
        
        TestHeaderTwo IDeserializable<TestHeaderTwo>.RosDeserialize(ref Buffer b) => new TestHeaderTwo(ref b);
    
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
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderTwo";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2RT2vcMBDF7/oUA3tIUtgU2ttCbyVND4VCcl9mpYk9IEuuZrypv32fbPrv1kONQMh6" +
                "7/dGM4/CSRqN2xY+/OcvfHn6dCLzdJ5ssLePe8qBnpxL4pZoEufEzvRSUYQOo7RjlqtkmHiaJdF26+ss" +
                "dg/j86hGWIMUaZzzSotB5JVinaalaGQXcp3kLz+cWohp5uYal8wN+tqSli5/aTxJp2OZfFukRKHPH0/Q" +
                "FJO4uKKgFYTYhE3LgEsKixZ//64bwuH5tR5xlAGt/BVOPrL3YuX73MR6nWwnZLzZH3cPNpojSElGt9u/" +
                "M452RwhBCTLXONItKv+6+lgLgEJXbsqXLB0c0QFQb7rp5u4Pci/7RIVL/Ynfib8z/gXbKTu3v+k4Yma5" +
                "v96WAQ2EcG71qgnSy7pBYlYpTlkvjdsaumuPDIeH3mOI4Nomgp3NalQMINGr+hjMW6dv0zhrCuEHqBQ4" +
                "g5wCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
