
namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestChar : IMessage
    {
        public char[] data;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestChar";
    
        public IMessage Create() => new TestChar();
    
        public int GetLength()
        {
            int size = 4;
            size += 1 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TestChar()
        {
            data = new char[0];
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
        public const string Md5Sum = "7b8d15902c8b049d5a32b4cb73fa86f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACkvOSCyKjlVISSxJ5OUCAKfkaM8NAAAA";
                
    }
}
