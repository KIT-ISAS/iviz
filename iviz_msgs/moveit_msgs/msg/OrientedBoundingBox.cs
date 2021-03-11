/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/OrientedBoundingBox")]
    public sealed class OrientedBoundingBox : IDeserializable<OrientedBoundingBox>, IMessage
    {
        // the pose of the box
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        // the extents of the box, assuming the center is at the origin
        [DataMember (Name = "extents")] public GeometryMsgs.Point32 Extents { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public OrientedBoundingBox()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public OrientedBoundingBox(in GeometryMsgs.Pose Pose, in GeometryMsgs.Point32 Extents)
        {
            this.Pose = Pose;
            this.Extents = Extents;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public OrientedBoundingBox(ref Buffer b)
        {
            Pose = new GeometryMsgs.Pose(ref b);
            Extents = new GeometryMsgs.Point32(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new OrientedBoundingBox(ref b);
        }
        
        OrientedBoundingBox IDeserializable<OrientedBoundingBox>.RosDeserialize(ref Buffer b)
        {
            return new OrientedBoundingBox(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Pose.RosSerialize(ref b);
            Extents.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 68;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/OrientedBoundingBox";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "da3bd98e7cb14efa4141367a9d886ee7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UPYsbMRDtBf4PA9ckcDhghxSBFCHFcUXgQtIHeXe8HrKr2Wi0sX2/Pk/aD8dcEQjB" +
                "qkbSfL33RrqjdGDq1Zh0X+ydnlzD2nGK5++dNfbmKd9mF+fuigufEodkf0TckzcbOglNOalwzZHEyKdy" +
                "oFEaCS/ySkjbzZzOrdyH/7xW7vPXh/f0Es4KSD5S5D6yobZPoiHDKURIoH1kJut9xfdUaZeP6+leiq8P" +
                "2EeZY9fkCprFwX0ZPCgIJe/F73YY0UwG+e0AESpFeQk2az1CAByPXe76CrHbt+rTu7d0WqzzYj3fCsGF" +
                "vwXGIhfG6orV6/7z7ueF/b3Gbu3+Amq2jjcVaLv5R4leHSUdCE9nJ+MzBDGVGEJer5HxEe6ZLkxuxzUm" +
                "NykNGOynkul44Mi/8DxRxmTXZsIssS8DXly2mzUR8lyIL5lCPbGNLhUltdOUg8G09hz9TlpJ5xI6R3Zs" +
                "5hs4GdVs0oSxmeR/MA09tbieRENXgSBujT8E0a1OwHI/5RvRkB+jt8xE+WP8+Gf5sedPrQ51rj1qCW4m" +
                "pWFNSsN6Xrnf2KGCAfIEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
