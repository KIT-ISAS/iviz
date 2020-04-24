namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt8 : IMessage
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
    
        public IMessage Create() => new UInt8();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "std_msgs/UInt8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "7c8164229e7d2c17eb95e9231617fdee";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
    }
}
