/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public BoundingBox2D()
        {
            Center = new GeometryMsgs.Pose2D();
        }
        
        /// Explicit constructor.
        public BoundingBox2D(GeometryMsgs.Pose2D Center, double SizeX, double SizeY)
        {
            this.Center = Center;
            this.SizeX = SizeX;
            this.SizeY = SizeY;
        }
        
        /// Constructor with buffer.
        internal BoundingBox2D(ref Buffer b)
        {
            Center = new GeometryMsgs.Pose2D(ref b);
            SizeX = b.Deserialize<double>();
            SizeY = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new BoundingBox2D(ref b);
        
        BoundingBox2D IDeserializable<BoundingBox2D>.RosDeserialize(ref Buffer b) => new BoundingBox2D(ref b);
    
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
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 40;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/BoundingBox2D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9ab41e2a4c8627735e5091a9abd68b02";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1UTWsUQRC9768oyCEKyQoqHgQPkYDkIATUc6iZrtlt7elu+2N3x1/vq57MJIs5OjDQ" +
                "n1Wv3nvVF3RDb2+pC9Ub63cYnKjsuVDPnjqhFAoXMcQ4UciWTL34Imm7uaAb58jYUXy2wWfiJGQ9RXsS" +
                "l6+ow/kkMUnWC4Zq1viDC1wwuI7B+oIgRAd2VTKVQOxcOFKu3XULQrjcWw2+pbuBAEhO3Jc5A/UpRLIZ" +
                "OX5Xm8S0WENIxCvo51VdAfxSVM+ur46fUH1uKPc8XmZy1kuLxW4Xki37cbvB/PtelKgYsi1ARK/WUl8D" +
                "mSEcRZ3c9sIADuWc1IW2nYRRSpoexrzLb+5DFkSdN5c02f6Rs/gvxcs1pWWum6H7KeAmCeqyh7kCUKpb" +
                "wIz94Uy9psOH9y3Xw+l8Om02n/7zt/n67ctHeql2AL1Vl/QqByb3ThhwK37FPlSY7F3jXZoOdx5hvCR2" +
                "FGpCvX0YYUEzMw8/oGi9zOvV1YSrOHKQNJW9UqfSqWkgQo5w22B74hidBZ7m6pF/zUiwCsclC5wwZlC2" +
                "2wHY+JFmx15aMMxsWl3WTmk3hFjsCI9PdLSA1lClwxwdOK3H3XEGaWpSdMjTS1aLzrUPdBTaM+5EBgNO" +
                "2yBEi+5BVSgBLHCZIuatRPpxN3sTGRLcFEXNPWcHZVQjeZHZQmGOayqIRUmSBkZqXY8ulGeUbekGV6dQ" +
                "z+Ic0SjkcbCTRmvnZFECpZUQXF541rUeGjeQkKyZe7rE6wEpo5MiYAiPgjVXxFkzXZoZHAImsXoGiutr" +
                "NKc1dhgktRUfK0zeaq5Fx3g5CppakYGyYlWRhPh7TmYtTgWYext9AuEAo0JlXZ74SIJOS4uwpLalEarw" +
                "DqZAJiUXI/1dEjbTY7m652yXGC8DiFweVaNugU81HmqDsK3tsSIntYQSw0/vzD9vC5YU7MjeDsEZ3F+a" +
                "96mNp3UEcIU3m7/fiGCy5wUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
