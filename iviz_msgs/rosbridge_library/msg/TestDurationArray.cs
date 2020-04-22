
namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestDurationArray : IMessage
    {
        public duration[] durations;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestDurationArray";
    
        public IMessage Create() => new TestDurationArray();
    
        public int GetLength()
        {
            int size = 4;
            size += 8 * durations.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TestDurationArray()
        {
            durations = System.Array.Empty<0>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out durations, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(durations, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8b3bcadc803a7fcbc857c6a1dab53bcd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACkspLUosyczPi45VSIEyi3m5AKlp9poWAAAA";
                
    }
}
