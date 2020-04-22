
namespace Iviz.Msgs.std_msgs
{
    public sealed class Empty : IMessage
    {
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Empty";
    
        public IMessage Create() => new Empty();
    
        public int GetLength() => 0;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACuPlAgCshaIUAgAAAA==";
                
    }
}
