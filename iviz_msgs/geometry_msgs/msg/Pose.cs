using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Pose : IMessage
    {
        // A representation of pose in free space, composed of position and orientation. 
        public Point position;
        public Quaternion orientation;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 56;
    
        public IMessage Create() => new Pose();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "geometry_msgs/Pose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "e45d45a5a1ce597b249e23fb30fc871f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71RsQrCMBTc31c8cJW6iIPg4OQkKLpLqC9twOTVvIjWrzctNrGTi5jpLrm83F0muEZP" +
                "jSchF1Qw7JA1NiyExqH2RCiNKmmKJdtu+/w+N71Wuci9Ge4WCDs2LiQB7G8qkHf93KwDWP14wfawWWJF" +
                "bCn49mSlkllvBSZ4rI1E+/Ft4wRDTdl/zKIi6yyP4oK+sAqLOT4SahN6/sd+rm7IkD5KYvGffY7Nd+ya" +
                "e9fsbQFfEg3oDvACaqg09xMCAAA=";
                
    }
}
