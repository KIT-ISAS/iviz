
namespace Iviz.Msgs.tf2_msgs
{
    public sealed class TF2Error : IMessage
    {
        public const byte NO_ERROR = 0;
        public const byte LOOKUP_ERROR = 1;
        public const byte CONNECTIVITY_ERROR = 2;
        public const byte EXTRAPOLATION_ERROR = 3;
        public const byte INVALID_ARGUMENT_ERROR = 4;
        public const byte TIMEOUT_ERROR = 5;
        public const byte TRANSFORM_ERROR = 6;
        
        public byte error;
        public string error_string;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "tf2_msgs/TF2Error";
    
        public IMessage Create() => new TF2Error();
    
        public int GetLength()
        {
            int size = 5;
            size += error_string.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TF2Error()
        {
            error_string = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out error, ref ptr, end);
            BuiltIns.Deserialize(out error_string, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(error, ref ptr, end);
            BuiltIns.Serialize(error_string, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "bc6848fd6fd750c92e38575618a4917d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACkXKywrCMBCF4X3Ad8gjeMeNi1BjGUxmyjgpdpWVSDctxPb9BRvj8vzfmfthOmmkaJmJ" +
                "9Vmv1fxNjugWmpI3OVeEaCuBFqQruM1oH8KmIWcECIvusgK2xsElGq6DtyjlsM8HAW8p/Pvh19ng/Urs" +
                "ixxVpmdKY1LvKfXDaxlxGWqlPpexWmnbAAAA";
                
    }
}
