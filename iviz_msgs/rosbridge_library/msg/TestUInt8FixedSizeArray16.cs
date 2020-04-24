namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestUInt8FixedSizeArray16 : IMessage
    {
        public byte[/*16*/] data;
    
        /// <summary> Constructor for empty message. </summary>
        public TestUInt8FixedSizeArray16()
        {
            data = new byte[16];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end, 16);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end, 16);
        }
    
        public int GetLength() => 16;
    
        public IMessage Create() => new TestUInt8FixedSizeArray16();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "rosbridge_library/TestUInt8FixedSizeArray16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "a4e84d0a73514dfe9696b4796e8755e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxiDY0i1VISSxJ5OICANuquFIQAAAA";
                
    }
}
