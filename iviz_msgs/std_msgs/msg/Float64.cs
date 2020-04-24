namespace Iviz.Msgs.std_msgs
{
    public sealed class Float64 : IMessage
    {
        public double data;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        public int GetLength() => 8;
    
        public IMessage Create() => new Float64();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "std_msgs/Float64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "fdb28210bfa9d7c91146260178d9a584";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
    }
}
