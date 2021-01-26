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
        [DataMember (Name = "center")] public GeometryMsgs.Pose2D Center { get; set; }
        // The size (in pixels) of the bounding box surrounding the object relative
        //   to the pose of its center.
        [DataMember (Name = "size_x")] public double SizeX { get; set; }
        [DataMember (Name = "size_y")] public double SizeY { get; set; }
    
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
        public BoundingBox2D(ref Buffer b)
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
                "H4sIAAAAAAAAE61UTWsUQRC9L+Q/FOQQhWQFFQ+Ch0hAchAC6llqp2t2W3u62/7I7vjrfdWdnWQ1RxcW" +
                "pj+q6tV7r/qcrun1DW1C9cb6LT4OVHZcaGBPG6EUChcxxLhRyJZMg/giaX22Oqdr58jYSXy2wWfiJGQ9" +
                "RXsQly9pg4AkMUnWCEM1a4HRBS74uIrB+qJZiO7ZVclUArFzYU+5bq5aFkL0YDX7mm5HAiQ58FB6CRpS" +
                "iGQzivyqNonpycaQiBfcTxu7BP5jXwO7oTp+xPWx4dzxdJHJWS89GbttSLbsJvSrO193onTFkG0BKnqx" +
                "9PsS6AzhMprldhZGMCmn1C7kbSVMUtL8fcrb/OouZEHafvpYKdvfclLiuZS5pnRc62HY/JBBmUd39v6h" +
                "D1CrZ8AtmuREx6bIu7et2vfDX+v5bLX68J9/q89fPr2n5xgA1ht1zKDCYHHnhIG44q/wxwrDvWn0y3qF" +
                "81uPNF4SOwo1oechTLCj6QLYZikN5iV0MeSikdxLmstO6VMF1T6QIkcYb7QDcYzODu12pol/diTYhfmS" +
                "BU54NCjj7QIs/cC0Yy8tGVY2LX5rt3QyQix2gt1n2lvXjJ4l3ffswGk9YqcO0tSk6FBnkKxm7b2PtBfa" +
                "MWIigwGnExGixSChK7QAFrjMEevWIn277RZFhQRHRVGb9+qgjGokL9JtFHpeU0GsVZuMPPQBjS6UJ5St" +
                "6Rqhc6gnefaYGPK4uJFG68bJUQm0VkJw+ciz7g3QuIGEZM3g8wVeEkgZnRQBQ3gfrLkkzlrpwnRwSJjE" +
                "6h0ork9TL2vsOEpqOz5W+Lz1XIt+4xEpGG9FBsqKVUUS8u84maW5sow4RiU1GLV0XWbek2Da0lFYUtvS" +
                "BFV4K62SkivtJWSXhM380K6eObtJnFSg5YU16hbb2UBvELaNPnbkoJZQYvjxufnnicGWgp3Y2zE4g/jj" +
                "9B6Wr3n5ArjCq9Uf7+Mjj/QFAAA=";
                
    }
}
