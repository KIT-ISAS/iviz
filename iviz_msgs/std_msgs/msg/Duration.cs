
namespace Iviz.Msgs.std_msgs
{
    public sealed class Duration : IMessage
    {
        public duration data;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Duration";
    
        public IMessage Create() => new Duration();
    
        public int GetLength() => 8;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "3e286caf4241d664e55f3ad380e2ae46";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACkspLUosyczPU0hJLEnk4uUCAA+f78YQAAAA";
                
    }
}
