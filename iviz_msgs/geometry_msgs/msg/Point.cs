using System.Runtime.InteropServices;

namespace Iviz.Msgs.geometry_msgs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IMessage
    {
        // This contains the position of a point in free space
        public double x;
        public double y;
        public double z;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        public int GetLength() => 24;
    
        public IMessage Create() => new Point();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "geometry_msgs/Point";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAEz3HwQmAMAwF0Hum+OAK4iQuEEpCA5KUJgd1ej319t6Gs1uihRebJ6oLRqSVhSMU/M+8" +
                "YA6dIsjBTUiv4Dp23EvP0kv0AQQdt/JVAAAA";
                
    }
}
