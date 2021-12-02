/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class OrientedBoundingBox : IDeserializable<OrientedBoundingBox>, IMessage
    {
        // the pose of the box
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // the extents of the box, assuming the center is at the origin
        [DataMember (Name = "extents")] public GeometryMsgs.Point32 Extents;
    
        /// Constructor for empty message.
        public OrientedBoundingBox()
        {
        }
        
        /// Explicit constructor.
        public OrientedBoundingBox(in GeometryMsgs.Pose Pose, in GeometryMsgs.Point32 Extents)
        {
            this.Pose = Pose;
            this.Extents = Extents;
        }
        
        /// Constructor with buffer.
        internal OrientedBoundingBox(ref Buffer b)
        {
            b.Deserialize(out Pose);
            b.Deserialize(out Extents);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new OrientedBoundingBox(ref b);
        
        OrientedBoundingBox IDeserializable<OrientedBoundingBox>.RosDeserialize(ref Buffer b) => new OrientedBoundingBox(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref Pose);
            b.Serialize(ref Extents);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 68;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/OrientedBoundingBox";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "da3bd98e7cb14efa4141367a9d886ee7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWsbMRC961cM5NJCcMAuOQRyCD2UHAop7b3Iu+P10F3NVqOt7fz6PGl37ZgcAqFY" +
                "p5E0H++9GemK0papV2PSTbHXuncNa8cpHn531tjNU77NLs5dFRfeJw7JXkVckzcbOglNOalwzZHEyKdy" +
                "oFEaCW/ySkir5ZzOufv/vNz3n9/u6C0Z0HigyH1kQ2GfREPmUlSQQJvITNb7iq+p0i4f19O9FF8fsI8y" +
                "xy7IFSpHB/dj8OAfSt6T36UIAgoY/tpC/kpRW4LNXR7xg4vHLkM+o+s2rfp0+4X2R+twtJ4vA/8k3czh" +
                "2ChM05me5+Dz7u9J943GbuHeYTRbuwu2ZrX8WHM+7SRtCc9lLePTgyqVGEI+L5DxEe5ZKwxsxzUGNikN" +
                "mOenkmm35cj/8CRRxmTdZrUssS9zXVxWywUR8pxUL5lCPUkNlIqS2mnKwZBZe45+La2kQwmdIzs28w2c" +
                "jGo2acIIJvk/TENPLa6njgFVIHS2xr+B6FYnYhlP+To05DfoLStR/hU//lN+xPy11aHOtcdGQpupzbCm" +
                "NsN6di/1kU6A5QQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
