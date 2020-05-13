using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Point : IMessage
    {
        // This contains the position of a point in free space
        [DataMember] public double x { get; }
        [DataMember] public double y { get; }
        [DataMember] public double z { get; }
    
        /// <summary> Explicit constructor. </summary>
        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Point(Buffer b)
        {
            this = b.Deserialize<Point>();
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
