/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class BoundingBox3D : IDeserializable<BoundingBox3D>, IMessage
    {
        // A 3D bounding box that can be positioned and rotated about its center (6 DOF)
        // Dimensions of this box are in meters, and as such, it may be migrated to
        //   another package, such as geometry_msgs, in the future.
        // The 3D position and orientation of the bounding box center
        [DataMember (Name = "center")] public GeometryMsgs.Pose Center;
        // The size of the bounding box, in meters, surrounding the object's center
        //   pose.
        [DataMember (Name = "size")] public GeometryMsgs.Vector3 Size;
    
        /// Constructor for empty message.
        public BoundingBox3D()
        {
        }
        
        /// Explicit constructor.
        public BoundingBox3D(in GeometryMsgs.Pose Center, in GeometryMsgs.Vector3 Size)
        {
            this.Center = Center;
            this.Size = Size;
        }
        
        /// Constructor with buffer.
        internal BoundingBox3D(ref Buffer b)
        {
            b.Deserialize(out Center);
            b.Deserialize(out Size);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new BoundingBox3D(ref b);
        
        BoundingBox3D IDeserializable<BoundingBox3D>.RosDeserialize(ref Buffer b) => new BoundingBox3D(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref Center);
            b.Serialize(ref Size);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/BoundingBox3D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "727c83f2b037373b8e968433d9c84ecb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7UQAy95yss7YFWihaJoh6QOFRagXpAFIG4Im/iZIcm48UzYbv9et7MbrKNuoIL" +
                "ak6ejP38nu3xgm7oakVrHXztfAvjgeKGI1XsaS201eCiUy81sa/JNHJMNgIiuRioEh/F6OKaVp8/XBYL" +
                "WrlefEBIIG2A5UIGZRNynnqBdygzGAcKQ7UpgUM971O63rWWE0QFFMFN4wbwW67uuZUyB6TAVhRQtv/R" +
                "hxZwQIYfNUMcTJYFYr/hCGEj/5xQzYEt53PmJnPhBy3FDPv1nQYZb464wT3KOYDyqcIwmI13yVHXP6WK" +
                "r8aKZXlgB7bzfN/hpXaVkxTF+//8FZ++fnxHzxWCzg2ZbE3C0xIlgklUYwLZ6AJaUGmfftfH+7PlXVJx" +
                "p87HyaH4MqCv5jPuye+lBIJK7h2GsVLkdpjO1JSJP7QwTonyTG7RdMrx+i09TNZ+sh5fhv6pdKOGqVEB" +
                "hZ+N9Yx8Ov061b1R6/E2/q5otHYvo+047eeE0e98N5eEwVrQLZqE7eK7PV4bo2VRT5EIrJ0hNI8hnqsJ" +
                "hEveMrVKIKwUYPR8D0hsKknRvN0CjCka+9AdSonfCLmQZbssabcRDHnySu+ZE4tWvJiryFzrsLBSZKrw" +
                "FMx0FFdSbN7QznXdgfMhGcYPIHmhIuBySbcN7XWgXRIEw6jmCEaa9uLIi9dd4qslDYl4hjgz6yhLCNiX" +
                "qF2IwvU/u/4HB+XEawcGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
