/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/ByteMultiArray")]
    public sealed class ByteMultiArray : IDeserializable<ByteMultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public byte[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public ByteMultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ByteMultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ByteMultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ByteMultiArray(ref b);
        }
        
        ByteMultiArray IDeserializable<ByteMultiArray>.RosDeserialize(ref Buffer b)
        {
            return new ByteMultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/ByteMultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "70ea476cbcfd65ac2f68f3cda1e891fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
