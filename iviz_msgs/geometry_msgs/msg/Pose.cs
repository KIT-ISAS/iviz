using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Pose : IMessage
    {
        // A representation of pose in free space, composed of position and orientation. 
        [DataMember] public Point position { get; }
        [DataMember] public Quaternion orientation { get; }
    
        /// <summary> Explicit constructor. </summary>
        public Pose(Point position, Quaternion orientation)
        {
            this.position = position;
            this.orientation = orientation;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Pose(Buffer b)
        {
            this = b.Deserialize<Pose>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Pose(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 56;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Pose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e45d45a5a1ce597b249e23fb30fc871f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71RsQrCMBTc31c8cJW6iIPg4OQkKLpLqC9twOTVvIjWrzctNrGTi5jpLrm83F0muEZP" +
                "jSchF1Qw7JA1NiyExqH2RCiNKmmKJdtu+/w+N71Wuci9Ge4WCDs2LiQB7G8qkHf93KwDWP14wfawWWJF" +
                "bCn49mSlkllvBSZ4rI1E+/Ft4wRDTdl/zKIi6yyP4oK+sAqLOT4SahN6/sd+rm7IkD5KYvGffY7Nd+ya" +
                "e9fsbQFfEg3oDvACaqg09xMCAAA=";
                
    }
}
