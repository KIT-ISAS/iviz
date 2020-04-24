namespace Iviz.Msgs.std_msgs
{
    public sealed class Byte : IMessage
    {
        public byte data;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        public int GetLength() => 1;
    
        public IMessage Create() => new Byte();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Byte";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ad736a2e8818154c487bb80fe42ce43b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE0uqLElVSEksSeTiAgAksd8TCwAAAA==";
                
    }
}
