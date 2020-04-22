
namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestHeaderTwo : IMessage
    {
        public std_msgs.Header header;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestHeaderTwo";
    
        public IMessage Create() => new TestHeaderTwo();
    
        public int GetLength()
        {
            int size = 0;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderTwo()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq2RT0sDMRDF74F+h4EebIUq6K3gTbQeBMHeyzSZ7g5kkzUzW91v72Tr35sHl0DI5r3f" +
                "m8xsCAMVaKdt5m7++Zu5x+f7NYiGXSeNXG4+cubwrJgClgAdKQZUhEO2OrhpqawiHSmaC7ueAky3OvYk" +
                "F2bctixgq6FEBWMcYRATaQafu25I7FEJlDv65TcnJ0DosSj7IWIxfS6BU5UfCnZU6baEXgZKnuDhdm2a" +
                "JOQHZStoNIIvhMKpsUtwAye9vqoGN9++5pUdqbFufoWDtqi1WHrrC0mtE2VtGeenx10Y27pDlhIEFtO/" +
                "nR1lCRbiCajPvoWFVf40apuTAQmOWBj3kSrYWweMelZNZ8sf5Fr2GhKm/Ik/Eb8z/oKtlBO3vmnV2sxi" +
                "fb0MjTXQhH3JRw4m3Y8TxEempBB5X7CMrrpOkW5+V3tsInNNE7EdRbJnG0CAV9bWiZZKn6ax4+Bm7h1N" +
                "tjVCoAIAAA==";
                
    }
}
