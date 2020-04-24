namespace Iviz.Msgs.rosbridge_library
{
    public sealed class Num : IMessage
    {
        public long num;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out num, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(num, ref ptr, end);
        }
    
        public int GetLength() => 8;
    
        public IMessage Create() => new Num();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/Num";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "57d3c40ec3ac3754af76a83e6e73127a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUcgrzeXiAgCjoYsaCwAAAA==";
                
    }
}
