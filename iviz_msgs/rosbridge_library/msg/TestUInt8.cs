namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestUInt8 : IMessage
    {
        public byte[] data;
    
        /// <summary> Constructor for empty message. </summary>
        public TestUInt8()
        {
            data = System.Array.Empty<byte>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 4;
            size += 1 * data.Length;
            return size;
        }
    
        public IMessage Create() => new TestUInt8();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "rosbridge_library/TestUInt8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "f43a8e1b362b75baa741461b46adc7e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxiI5VSEksSeQCANR1vBgNAAAA";
                
    }
}
