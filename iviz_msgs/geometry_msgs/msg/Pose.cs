
namespace Iviz.Msgs.geometry_msgs
{
    public struct Pose : IMessage
    {
        // A representation of pose in free space, composed of position and orientation. 
        public Point position;
        public Quaternion orientation;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Pose";
    
        public IMessage Create() => new Pose();
    
        public int GetLength() => 56;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "e45d45a5a1ce597b249e23fb30fc871f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr2RMQvCMBCF94P+h4Ouoos4CA5OToKiu4R6aQM2V3MRrb/etNjETi5ipnvJ5fK+lxzX" +
                "6KhxJGS98oYtssaGhdBY1I4IpVEFTbDguts+v89N36ts0M4Md6cIOzbWxwbY35QnZ/u5qQ8yWP14ZbA9" +
                "bJZYEtfkXXuqpZRZbyaDHI+VkUAQnjdW0FeUEAKOCqpzPSIGfWHlF3N8xKqN1fNfBCm/iBG/S0L8n6mO" +
                "/XfqmtLX7OopfIEaqnvAewEjTA5+GgIAAA==";
                
    }
}
