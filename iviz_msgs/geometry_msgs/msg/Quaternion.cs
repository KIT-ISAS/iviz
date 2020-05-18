using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Quaternion")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IMessage
    {
        // This represents an orientation in free space in quaternion form.
        [DataMember (Name = "x")] public double X { get; set; }
        [DataMember (Name = "y")] public double Y { get; set; }
        [DataMember (Name = "z")] public double Z { get; set; }
        [DataMember (Name = "w")] public double W { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Quaternion(double X, double Y, double Z, double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Quaternion(Buffer b)
        {
            this = b.Deserialize<Quaternion>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Quaternion(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 32;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Quaternion";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a779879fadf0160734f906b8c19c7004";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3JTQqAQAhA4b2nENq3ik7SBSQcEkonNfo5fbWZ3fd4HU6LBDpX52DNQFI0l4+UYoqi" +
                "WJwZo9LMf+0HJbv+r5hvPUBZjXIc8Gq6m56mE+AFLI5yL20AAAA=";
                
    }
}
