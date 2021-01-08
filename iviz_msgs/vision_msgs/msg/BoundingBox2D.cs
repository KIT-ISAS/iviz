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
                "H4sIAAAAAAAACq1US2sUQRC+C/kPBTlEIYmg4kHwEAlIDkJAPYea6Zrd1p7uth+7O/56v+rZmWQ1RxcW" +
                "Znq6q75X9Tnd0Jtb6kL1xvoNHg5UtlyoZ0+dUAqFixhi7ChkS6ZefJF0ffbinG6cI2NH8dkGn4mTkPUU" +
                "7UFcvqQOB5LEJFlPGKpZGwwucMHDVQzWF61CtGNXJVMJxM6FPeXaXbUqhNO91erXdDcQIMmB+zK3oD6F" +
                "SDajya9qk5i52BAS8Yr7KbFL4F949ez66vgR16eGc8vjRSZnvczF2G1CsmU7gq+ufNuKyhVDtgWo6OXK" +
                "9xXQGcJmkOX2LQxQUk6lXcXbSBilpOlhzJv8+j5kQdn562OnbH/LSYvnSuaa0vKuH0P3QyBRErCzuyMP" +
                "SKvfgBsbhlMfmyPv37VuD4e/3idF8/E//85efPn6+QM9p4HCvdXQ9OqNvt07YaCu+CuFoSJ0b5sFcjTl" +
                "zqOUl8SOQk1g3ocRoTSzDQgI2OtxXg+vsVydkp2kqWxVRPVRQwRDckT8BtsTx+gsILWcj/xzxoJVRDBZ" +
                "QEVSg+reNiDYR70de2nF8GbTmrq2S+cjxGJHhH6ivQW0hirt5urAaT3OjjNIU5OiQ59eskZ2YT/QXmjL" +
                "OBUZGjidjBAtBgq8QAI6cJki3htJ+n43RxU9EpIVReM+94doVCN5kTlOYa5rKqQFKUkDo7muRxfKE9Gu" +
                "6QZHp1BP6uwxOeSxsZMmbOdk8QLkSgguL0rrWg+fG0iY1oI+XeBGgZnRSRFohHvCmkvirJ0uzAwOBZNY" +
                "3QPP9Yqa2xo7DJLaio8VeW+ca9FnXCYFY67IIqdi1ZOE+ltOZiWnFsyjjpGBdYBR4bMuT7wnwdSlxVrS" +
                "8NIIX3iDWKCTiosn/bskbKYjXf3mbJcYFwWEXG5ao3lBUrUeuDVr51sAi3LQXKg2/Hjz/HPbYEnxjuzt" +
                "EJxpJZZRfjLVGOjlERgL67Y/Ms4lkgUGAAA=";
                
    }
}
