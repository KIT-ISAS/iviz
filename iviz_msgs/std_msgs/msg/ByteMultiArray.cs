
namespace Iviz.Msgs.std_msgs
{
    public sealed class ByteMultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public byte[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/ByteMultiArray";
    
        public IMessage Create() => new ByteMultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 1 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public ByteMultiArray()
        {
            layout = new MultiArrayLayout();
            data = System.Array.Empty<0>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            layout.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            layout.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "70ea476cbcfd65ac2f68f3cda1e891fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhcEGY4RQVOs6VmJLQZKbZb9+R/5O+zom" +
                "DLbv5zlHVxrRt4yFY8qMOZDw5FOm5yLzam2tOD+Jsyk85eyc2DFJTpRWXhlNibHRiKSJi5y1F6UNj8gy" +
                "ykO6COluGkUfilFWv6s1InfkWCUqroskJIUXdVT0eva82TbBYZXedo2o7NSkRdEgWv3jNYiev399IOfl" +
                "S+527vN7RgMI8QOydbwhVJwJy44E7VizVXHlvZEKcjnwFFkHXKDAUViv4gJZFUF/PvKU6LGJRynLZKxk" +
                "y5ISa3JCa7aUG+eR7w0prev/C9nbEpAR7aHYulWscdHRmiMDAbuoUNovFyWKF5MkjntbdRRSKr0jzjhs" +
                "O0D5gEX7Tn+Uj2PMi7GOXGqKTNL66ef613d6ZTpZ5T1rQCVgz90lCOetkowKQstmKkC25HkTePViE2UD" +
                "zxHh6YS/UpP95HBNqxLMps/hU0h+qVps5tuxurQstuM9LIdtNAoUgAUghJUTWt7EqYC0Gd3dzn7ffpmR" +
                "ysNhOCmfggiw4QS9AWdsMmOpDnaocirZg3bHRbiHsgE6b2bbaSZeURdwhymrXeqHncupPwzNV4SOPWuJ" +
                "FtblGGjGAc2K7hfzu9mM6Eobz3VkLSYpR/sCypXloHaJ/bouOO8jOCnp02HnaQGgUc96AQDv+f2icS/6" +
                "5Wodhp2vLbjs2dpypSwfd9JywpgkjHe4mYLk1pwmtMcH9C5yPSmn5RD+q47T/3oFPDYTOYgCF5yNWgKc" +
                "luoLou/UG4a+Hd7miNWChCuw3p13gXQVDgpuAipw7brrNrFSLSRWXx9TB9FfG58dY9sFAAA=";
                
    }
}
