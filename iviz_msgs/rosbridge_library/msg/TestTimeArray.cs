
namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestTimeArray : IMessage
    {
        public time[] times;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestTimeArray";
    
        public IMessage Create() => new TestTimeArray();
    
        public int GetLength()
        {
            int size = 4;
            size += 8 * times.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TestTimeArray()
        {
            times = new time[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out times, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(times, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "237b97d24fd33588beee4cd8978b149d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACivJzE2NjlUoAVLFvFwARiS1Xg4AAAA=";
                
    }
}
