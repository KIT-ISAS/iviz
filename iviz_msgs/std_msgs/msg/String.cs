
namespace Iviz.Msgs.std_msgs
{
    public sealed class String : IMessage
    {
        public string data;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/String";
    
        public IMessage Create() => new String();
    
        public int GetLength()
        {
            int size = 4;
            size += data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public String()
        {
            data = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "992ce8a1687cec8c8bd883ec73ca41d1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1dISSxJ5OLlAgCAhD+7DgAAAA==";
                
    }
}
