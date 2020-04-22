
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Twist : IMessage
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        public Vector3 linear;
        public Vector3 angular;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Twist";
    
        public IMessage Create() => new Twist();
    
        public int GetLength() => 48;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            linear.Deserialize(ref ptr, end);
            angular.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            linear.Serialize(ref ptr, end);
            angular.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1RwUrEMBC9B/oPA3tRKBVUPAieZQ+CoHiV2XaaDZtmymTW3fr1Tral4t1CYZK+9+a9" +
                "1w2870MGOo9COVOGL4rcBp0gJOiFCPKILcFO+EDJLpUhaIYYEqEAps5ef4w2jyiaG/dBrbLcwQL5PS84" +
                "V7mnf34q9/L2/AieeCCV6XPIPt8seyu3mSMKlYiUzDxayvLxb8YGDLpVMCynOMFAmBQs78o0YhfEqIFT" +
                "Y6ok1LNQbY1Ax1ZeYjWNAQ8mSSlTYeM4mhiCCqYcsXDLtVGuqPFNDae9FXtBheQNaAqeEkloQYIP3cy0" +
                "RcNKRljS1aD9LZxCjLPneZnuyUSE9UK4bmDbw8RHOJVANgh0qOaIYWcWF1+4i8Uv13Asxi8Sfxt9Zfv9" +
                "VkvO6Mm6y0rYNc71kVEf7uG8TtM6fVfuBy/yawBjAgAA";
                
    }
}
