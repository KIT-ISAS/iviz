
namespace Iviz.Msgs.std_msgs
{
    public struct ColorRGBA : IMessage
    {
        public float r;
        public float g;
        public float b;
        public float a;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/ColorRGBA";
    
        public IMessage Create() => new ColorRGBA();
    
        public int GetLength() => 16;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a29a96539573343b1310c73607334b00";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACkvLyU8sMTZSKOJKg7LS4awkOCuRi5cLAGB4EzcqAAAA";
                
    }
}
