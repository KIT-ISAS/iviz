using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/BoundingBox")]
    [StructLayout(LayoutKind.Sequential)]
    public struct BoundingBox : IMessage
    {
        [DataMember (Name = "minx")] public float Minx { get; set; }
        [DataMember (Name = "maxx")] public float Maxx { get; set; }
        [DataMember (Name = "miny")] public float Miny { get; set; }
        [DataMember (Name = "maxy")] public float Maxy { get; set; }
        [DataMember (Name = "minz")] public float Minz { get; set; }
        [DataMember (Name = "maxz")] public float Maxz { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public BoundingBox(float Minx, float Maxx, float Miny, float Maxy, float Minz, float Maxz)
        {
            this.Minx = Minx;
            this.Maxx = Maxx;
            this.Miny = Miny;
            this.Maxy = Maxy;
            this.Minz = Minz;
            this.Maxz = Maxz;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal BoundingBox(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(Buffer b)
        {
            return new BoundingBox(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public readonly void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 24;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/BoundingBox";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9073d3913b9d98666b3f836aaf76439f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSyM3Mq+BKg3ESK5A4mXmVyDKVyDJVyDJVXFwAF9ihhE8AAAA=";
                
    }
}
