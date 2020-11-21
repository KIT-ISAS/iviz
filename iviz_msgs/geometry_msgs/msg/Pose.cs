/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Pose")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Pose : IMessage, System.IEquatable<Pose>, IDeserializable<Pose>
    {
        // A representation of pose in free space, composed of position and orientation. 
        [DataMember (Name = "position")] public Point Position { get; set; }
        [DataMember (Name = "orientation")] public Quaternion Orientation { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Pose(in Point Position, in Quaternion Orientation)
        {
            this.Position = Position;
            this.Orientation = Orientation;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Pose(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Pose(ref b);
        }
        
        readonly Pose IDeserializable<Pose>.RosDeserialize(ref Buffer b)
        {
            return new Pose(ref b);
        }
        
        public override readonly int GetHashCode() => (Position, Orientation).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Pose s && Equals(s);
        
        public readonly bool Equals(Pose o) => (Position, Orientation) == (o.Position, o.Orientation);
        
        public static bool operator==(in Pose a, in Pose b) => a.Equals(b);
        
        public static bool operator!=(in Pose a, in Pose b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 56;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Pose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e45d45a5a1ce597b249e23fb30fc871f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2RMQvCMBCF90D+w0FX0UUcBAcnJ0HRXUK9tAGTq7mI1l9vUmxjJxcx073kJXnfXQFr" +
                "8Nh4ZHRBBUMOSENDjGAcaI8I3KgSJ1CSTdvn97npvMpF7U1/dwpS7Mi4MDik2N9UQO+6l7NTCilWP15S" +
                "bA+bJVRIFoNvT5YrnnVxpCjgWBuOEPF/4xhCjZkiEqmoUu4RtBT6Qios5vDIZZvL598ochMHlGFqHKfw" +
                "2doxQ1LXPAJN3k5T7K9kfXlP7hfm4ljNJwIAAA==";
                
        /// Custom iviz code
        public static readonly Pose Identity = new Pose(Point.Zero, Quaternion.Identity);
        public static implicit operator Transform(in Pose p) => new Transform(p.Position, p.Orientation);
    }
}
