namespace Iviz.Msgs.std_msgs
{
    public sealed class Duration : IMessage
    {
        public duration data;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        public int GetLength() => 8;
    
        public IMessage Create() => new Duration();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "std_msgs/Duration";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "3e286caf4241d664e55f3ad380e2ae46";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPU0hJLEnk4gIAtVhIcg8AAAA=";
                
    }
}
