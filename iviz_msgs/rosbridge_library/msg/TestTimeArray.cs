namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestTimeArray : IMessage
    {
        public time[] times;
    
        /// <summary> Constructor for empty message. </summary>
        public TestTimeArray()
        {
            times = System.Array.Empty<time>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out times, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(times, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 4;
            size += 8 * times.Length;
            return size;
        }
    
        public IMessage Create() => new TestTimeArray();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestTimeArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "237b97d24fd33588beee4cd8978b149d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAEyvJzE2NjlUoAVLFXAD3rdP6DQAAAA==";
                
    }
}
