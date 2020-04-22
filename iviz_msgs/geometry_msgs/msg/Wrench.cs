
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Wrench : IMessage
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        public Vector3 force;
        public Vector3 torque;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Wrench";
    
        public IMessage Create() => new Wrench();
    
        public int GetLength() => 48;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            force.Deserialize(ref ptr, end);
            torque.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            force.Serialize(ref ptr, end);
            torque.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4f539cf138b23283b520fd271b567936";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1SwUrEMBC9B/oPD/aiUCKoeBA8yx4EQfEqs+00G0yTmkxd69c73S67LF4ttEyT9968" +
                "N8kKr1tfkHnIXDhKQZdyw/ARXWZGGajhGoUHyiTc6oYks4JXZPCRKYNiq68bg9aKkmLNGzeS8g0WsdOv" +
                "fj9HNpV5+OenMk8vj/dwnHqWPL33xZWrQ9vK/A1J+Npvnue0UOhaoNgUw4SeKYqaPjGV2PqsVJ+iVVXO" +
                "rBF1QF7QJi6ISVSjpw+V5Fh4ZtMwqBhBMsUSaObOy0q5YOtsjd2W44Ly0SlQFRxHzr5B9s63C1Mb9Ucy" +
                "4ZCuhnTX2PkQFs9LM9myiuQke8KlxbrDlEbs5kBaZLQk6ihhoxYPvmgTZr+pxjgb30ucT/Q56fHrWEoh" +
                "N9+RIkytNaYLieTuFt/HajpWP5X5BVTczFZlAgAA";
                
    }
}
