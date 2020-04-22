
namespace Iviz.Msgs.geometry_msgs
{
    public struct Quaternion : IMessage
    {
        // This represents an orientation in free space in quaternion form.
        
        public double x;
        public double y;
        public double z;
        public double w;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Quaternion";
    
        public IMessage Create() => new Quaternion();
    
        public int GetLength() => 32;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a779879fadf0160734f906b8c19c7004";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACj3JPQqAMAxA4T3gHQLuTuJJvECQFAOa1DTiz+m1S7fv8XqcVynonJ0LaxQkRXP5SSGm" +
                "KIrJmbFkWrjWcVKwa33JfB8A0mYU04h309P0Nl3QwQc5jmFPbgAAAA==";
                
    }
}
