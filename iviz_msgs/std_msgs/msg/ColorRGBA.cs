using System.Runtime.InteropServices;

namespace Iviz.Msgs.std_msgs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorRGBA : IMessage
    {
        public float r;
        public float g;
        public float b;
        public float a;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        public int GetLength() => 16;
    
        public IMessage Create() => new ColorRGBA();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/ColorRGBA";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a29a96539573343b1310c73607334b00";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSKOJKg7LS4awkOCuRiwsAZHVNWikAAAA=";
                
    }
}
