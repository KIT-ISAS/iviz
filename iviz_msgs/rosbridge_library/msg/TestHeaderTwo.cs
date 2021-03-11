/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = "rosbridge_library/TestHeaderTwo")]
    public sealed class TestHeaderTwo : IDeserializable<TestHeaderTwo>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderTwo()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeaderTwo(in StdMsgs.Header Header)
        {
            this.Header = Header;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestHeaderTwo(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestHeaderTwo(ref b);
        }
        
        TestHeaderTwo IDeserializable<TestHeaderTwo>.RosDeserialize(ref Buffer b)
        {
            return new TestHeaderTwo(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAACq2RT0sDMRDF74F+h4EebIUq6K3gTbQeBMHeyzSZ7g5kkzUzW91v72Tr35sHl0DI5r3f" +
                "m8xsCAMVaKdt5m7++Zu5x+f7NYiGXSeNXG4+cubwrJgClgAdKQZUhEO2OrhpqawiHSmaC7ueAky3OvYk" +
                "F2bctixgq6FEBWMcYRATaQafu25I7FEJlDv65TcnJ0DosSj7IWIxfS6BU5UfCnZU6baEXgZKnuDhdm2a" +
                "JOQHZStoNIIvhMKpsUtwAye9vqoGN9++5pUdqbFufoWDtqi1WHrrC0mtE2VtGeenx10Y27pDlhIEFtO/" +
                "nR1lCRbiCajPvoWFVf40apuTAQmOWBj3kSrYWweMelZNZ8sf5Fr2GhKm/Ik/Eb8z/oKtlBO3vmnV2sxi" +
                "fb0MjTXQhH3JRw4m3Y8TxEempBB5X7CMrrpOkW5+V3tsInNNE7EdRbJnG0CAV9bWiZZKn6ax4+Bm7h1N" +
                "tjVCoAIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
