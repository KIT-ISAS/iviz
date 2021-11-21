/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/BoundingBox2D")]
    public sealed class BoundingBox2D : IDeserializable<BoundingBox2D>, IMessage
    {
        // A 2D bounding box that can be rotated about its center.
        // All dimensions are in pixels, but represented using floating-point
        //   values to allow sub-pixel precision. If an exact pixel crop is required
        //   for a rotated bounding box, it can be calculated using Bresenham's line
        //   algorithm.
        // The 2D position (in pixels) and orientation of the bounding box center.
        [DataMember (Name = "center")] public GeometryMsgs.Pose2D Center;
        // The size (in pixels) of the bounding box surrounding the object relative
        //   to the pose of its center.
        [DataMember (Name = "size_x")] public double SizeX;
        [DataMember (Name = "size_y")] public double SizeY;
    
        /// <summary> Constructor for empty message. </summary>
        public BoundingBox2D()
        {
            Center = new GeometryMsgs.Pose2D();
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoundingBox2D(GeometryMsgs.Pose2D Center, double SizeX, double SizeY)
        {
            this.Center = Center;
            this.SizeX = SizeX;
            this.SizeY = SizeY;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal BoundingBox2D(ref Buffer b)
        {
            Center = new GeometryMsgs.Pose2D(ref b);
            SizeX = b.Deserialize<double>();
            SizeY = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BoundingBox2D(ref b);
        }
        
        BoundingBox2D IDeserializable<BoundingBox2D>.RosDeserialize(ref Buffer b)
        {
            return new BoundingBox2D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Center.RosSerialize(ref b);
            b.Serialize(SizeX);
            b.Serialize(SizeY);
        }
        
        public void RosValidate()
        {
            if (Center is null) throw new System.NullReferenceException(nameof(Center));
            Center.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 40;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/BoundingBox2D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9ab41e2a4c8627735e5091a9abd68b02";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1UTWsUQRC9L+Q/FOQQhWQFFQ+Ch0hAchAC6jnUTNfstvZ0t/2R3fHX+6onM8lijg4M" +
                "9GfVq/de9Tld09sb6kL1xvodBkcqey7Us6dOKIXCRQwxThSyJVMvvkjabs7p2jkydhSfbfCZOAlZT9Ee" +
                "xeVL6nA+SUyS9YKhmjX+4AIXDK5isL4gCNEDuyqZSiB2Lhwo1+6qBSFc7q0G39LtQAAkR+7LnIH6FCLZ" +
                "jBy/q01iWqwhJOIV9POqLgF+Kapn11fHT6g+N5R7Hi8yOeulxWK3C8mW/bjdYP59L0pUDNkWIKJXa6mv" +
                "gcwQjqJObnthAIdySupC207CKCVN92Pe5Td3IQuizptLmmz/yEn8l+LlmtIy183Q/RRwkwR12Ye5AlCq" +
                "W8CM/eFEvabDh/ct1/3xdDptzjaf/vN3tvn67ctHeqn6M2C9UaP0qggmd04YiCt+hT9U+Oxdo16aFLce" +
                "cbwkdhRqQsl9GOFCM5MPS6Buvczr1dWHqz7yIGkqe2VP1VPfQIccYbjB9sQxOgs8zdgj/5qRYBWmSxY4" +
                "4c2ghLcDcPIj0469tGCY2bQarZ3Shgix2BE2n+hgAa2hSg9zdOC0HnfHGaSpSdEhTy9ZXTrXPtBBaM+4" +
                "ExkMOO2EEC0aCFWhBLDAZYqYtxLpx+1sT2RIMFQU9fecHZRRjeRFZheFOa6pIBYlSRoYqXU9ulCeUbal" +
                "a1ydQj2Jc0CvkMfBThqtnZNFCZRWQnB54VnXemjcQEKy5u/pAg8IpIxOioAhvAvWXBJnzXRhZnAImMTq" +
                "GSiuD9Kc1thhkNRWfKzweau5Fh3j8Sjoa0UGyopVRRLi7zmZtTgVYG5vtAqEA4wKlXV54gMJmi0twpL6" +
                "lkaowjuYApmUXIz0d0nYTI/l6p6zXWI8DiByeVeNugU+1XioDcK2zseKHNUSSgw/PTX/PC9YUrAjezsE" +
                "Z3B/6d+nTp7WEcAVRkv/BdEjSHXrBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
