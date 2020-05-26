using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Point")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IMessage
    {
        // This contains the position of a point in free space
        [DataMember (Name = "x")] public double X { get; set; }
        [DataMember (Name = "y")] public double Y { get; set; }
        [DataMember (Name = "z")] public double Z { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Point(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Point(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Point(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 24;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Point";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3HwQmAMAwF0Hum+OAK4iQuEEpCA5KUJgd1ej319t6Gs1uihRebJ6oLRqSVhSMU/M+8" +
                "YA6dIsjBTUiv4Dp23EvP0kv0AQQdt/JVAAAA";
                
    }
}
