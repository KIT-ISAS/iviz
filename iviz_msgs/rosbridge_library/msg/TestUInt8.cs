
namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestUInt8 : IMessage
    {
        public byte[] data;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestUInt8";
    
        public IMessage Create() => new TestUInt8();
    
        public int GetLength()
        {
            int size = 4;
            size += 1 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TestUInt8()
        {
            data = System.Array.Empty<0>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "f43a8e1b362b75baa741461b46adc7e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACivNzCuxiI5VSEksSeTlAgDsOjD8DgAAAA==";
                
    }
}
