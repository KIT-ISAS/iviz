/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class RecognizedObject : IDeserializable<RecognizedObject>, IMessage
    {
        //#################################################### HEADER ###########################################################
        // The header frame corresponds to the pose frame, NOT the point_cloud frame.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        //################################################# OBJECT INFO #########################################################
        // Contains information about the type and the position of a found object
        // Some of those fields might not be filled because the used techniques do not fill them or because the user does not
        // request them
        // The type of the found object
        [DataMember (Name = "type")] public ObjectRecognitionMsgs.ObjectType Type;
        //confidence: how sure you are it is that object and not another one.
        // It is between 0 and 1 and the closer to one it is the better
        [DataMember (Name = "confidence")] public float Confidence;
        //############################################### OBJECT CLUSTERS #######################################################
        // Sometimes you can extract the 3d points that belong to the object, in the frames of the original sensors
        // (it is an array as you might have several sensors)
        [DataMember (Name = "point_clouds")] public SensorMsgs.PointCloud2[] PointClouds;
        // Sometimes, you can only provide a bounding box/shape, even in 3d
        // This is in the pose frame
        [DataMember (Name = "bounding_mesh")] public ShapeMsgs.Mesh BoundingMesh;
        // Sometimes, you only have 2d input so you can't really give a pose, you just get a contour, or a box
        // The last point will be linked to the first one automatically
        [DataMember (Name = "bounding_contours")] public GeometryMsgs.Point[] BoundingContours;
        //################################################### POSE INFO #########################################################
        // This is the result that everybody expects : the pose in some frame given with the input. The units are radian/meters
        // as usual
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovarianceStamped Pose;
    
        /// Constructor for empty message.
        public RecognizedObject()
        {
            Type = new ObjectRecognitionMsgs.ObjectType();
            PointClouds = System.Array.Empty<SensorMsgs.PointCloud2>();
            BoundingMesh = new ShapeMsgs.Mesh();
            BoundingContours = System.Array.Empty<GeometryMsgs.Point>();
            Pose = new GeometryMsgs.PoseWithCovarianceStamped();
        }
        
        /// Explicit constructor.
        public RecognizedObject(in StdMsgs.Header Header, ObjectRecognitionMsgs.ObjectType Type, float Confidence, SensorMsgs.PointCloud2[] PointClouds, ShapeMsgs.Mesh BoundingMesh, GeometryMsgs.Point[] BoundingContours, GeometryMsgs.PoseWithCovarianceStamped Pose)
        {
            this.Header = Header;
            this.Type = Type;
            this.Confidence = Confidence;
            this.PointClouds = PointClouds;
            this.BoundingMesh = BoundingMesh;
            this.BoundingContours = BoundingContours;
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        internal RecognizedObject(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Type = new ObjectRecognitionMsgs.ObjectType(ref b);
            Confidence = b.Deserialize<float>();
            PointClouds = b.DeserializeArray<SensorMsgs.PointCloud2>();
            for (int i = 0; i < PointClouds.Length; i++)
            {
                PointClouds[i] = new SensorMsgs.PointCloud2(ref b);
            }
            BoundingMesh = new ShapeMsgs.Mesh(ref b);
            BoundingContours = b.DeserializeStructArray<GeometryMsgs.Point>();
            Pose = new GeometryMsgs.PoseWithCovarianceStamped(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new RecognizedObject(ref b);
        
        RecognizedObject IDeserializable<RecognizedObject>.RosDeserialize(ref Buffer b) => new RecognizedObject(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Type.RosSerialize(ref b);
            b.Serialize(Confidence);
            b.SerializeArray(PointClouds);
            BoundingMesh.RosSerialize(ref b);
            b.SerializeStructArray(BoundingContours);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
            Type.RosValidate();
            if (PointClouds is null) throw new System.NullReferenceException(nameof(PointClouds));
            for (int i = 0; i < PointClouds.Length; i++)
            {
                if (PointClouds[i] is null) throw new System.NullReferenceException($"{nameof(PointClouds)}[{i}]");
                PointClouds[i].RosValidate();
            }
            if (BoundingMesh is null) throw new System.NullReferenceException(nameof(BoundingMesh));
            BoundingMesh.RosValidate();
            if (BoundingContours is null) throw new System.NullReferenceException(nameof(BoundingContours));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += Header.RosMessageLength;
                size += Type.RosMessageLength;
                size += BuiltIns.GetArraySize(PointClouds);
                size += BoundingMesh.RosMessageLength;
                size += 24 * BoundingContours.Length;
                size += Pose.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/RecognizedObject";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "f92c4cb29ba11f26c5f7219de97e900d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Ya1McNxb93r9CZT4MpIZhDTbLskVtYR4xKQyOIbWbdbkodbdmRqZHGktqZsZb+9/3" +
                "XN1WTwOTZOMEphLTrb66z3Mf0traN/zE25PD45MP4lv2Nr8sWxPXYyXGSpbKiaGTEyUK65zyU2tKL4IV" +
                "Ad+n1iv+2hcXl9fNmjbhpqhsXfKnQfaW2TA38P79Jl2++eHk6FqcXZxeij9k1ZE1QWrjhTZD6yYyaGuE" +
                "zG0dovJhMVVCmjJZp+N3OxRSDG2NdZt/VkUAoysLl+BDGEcfaFXBLRM9GgdhbBA5rVWVKvFUyBokxBF/" +
                "wVoVY6O/1MqL0kZioqTvE2HdQ3oHIlCCDEKdom1R1UmKUVQ5KqLu68h/bpwq7MhEQ24mfuS3LuP6NW2j" +
                "veBTWDPUpTKF2hdjOxO+dkosbC0k/uogNAI+lqFhHP1Dakv8M4aC1iDGa+IsEuYqzJQy4i+R7GXrTACC" +
                "jAFwQN4yVUQfAIphZWXY2RZLVX4/ThJKjs5/uro++XD1rUjJmvAGPYHnyQ+FNELNg5OwnpTeKRnmjV9y" +
                "VVkzSknBXuoDYRwTygGfImSdHmkjK+GV8dZ5iFpnZ0CEdE4uhGSZjKWxvFOgvVNuuWcj4wcO53tS5IjS" +
                "bfvjp272+Xt29FtDrKkWYursHfwMXAP7ptRQP7fzLT+WU+Qy5BnSf6eMIIN29J95kPNZpGYt3ik/blnd" +
                "QN54hfQoOVq0XYLdFEnnbdKrFwBvWYFipEEhoyDe97kG5kcKiCN8BFu7PmUK6T5vsqCSIInGixllE/Kv" +
                "0uaW0o2jMtQOFIQ9WQdLiV+QsGykSEe36DgTbmwtaeSRL7/hJ95fXp38CVUrhYAMQQ2uq8DAI1wsclsu" +
                "gM4pQOfF/jJGiJenIsXVm5xq4JswjhTR+4PouhrFwcdUd7LU0mzBHyoiE0isfS2rR07y6p9gdGTvpMOG" +
                "Ql0FOZnC1yQ3yw7+5F/27ur7feFDyeK5mxC6AoqLdKWAbrKUgWo0ugzyRrnNCr5BxjSKxa9U7fyg486R" +
                "MpRXgBwXZgt4TSbwRyEDiiMBt7sfO+FTAFM6oKeupAO9dcAJkTe9bo3IPNVp+EWcHe8TZL0q6oAAQJI2" +
                "BWDuKeHOjkVWA28oe9iQrV3P7CZe1YjqZBLOgYayiDBCT3pKvw8Z37FxA/CGc1DkqS+vx7UbvPoNASFQ" +
                "QU1tMUaRMeL9As2KkzgGLq+AA4/ci32qR5t6Gx3OpPa+MCjziT1zXMr4f9iali/ZtDlGzJCZIzSZERwI" +
                "wqYUoVMumk6hFdK40rmTbpHRLhaZrZ1GKAcKX4wIlU3vbaERgDKCO/PBEfcYjRtdPhUaf7u5/pEx5/jp" +
                "h5xHMw0VAyobYWYBJ1QEUADpkqsBkgMFmLo90gPgBpJLNdRUTg0kNiNHU4xv1WLVRMK1ifcLavJBY24C" +
                "Bhg8XKHKPIUQXBLDMu+q4oN1MQ1gwQ9XlxdbyNqUGz8fvjsXzGAgDk0aWXTZjlFiIm+ppQKXETHJK8vx" +
                "FnshlwCWS68G4mQwGsR2/jjoWA6xqVbW3gKyt5igXvynRx7u7feObF2Mj9/0+qLnrA1YGYcw3d/aqiyy" +
                "A94Ovf++YBNp0CL1qFrADcQbDZ+jF8saz5kdL9BgpUMPmzRyErXrVqmmWgwrNde5rnRYcLlTq/AKgxU7" +
                "MdZBXYjjN4yNyISsQqUq7024BK4afkJ+qjlSslKxFp1CwcZYehWRzb5oHRDXyAVYe+iC/dd/23vFFIVF" +
                "yShIOdA91rjXSLr68RxjKkr22FZlG6d7gq++VG8TBfOOokRvNvI7u7wytQ4rr1/tbMdXmrWJANODnTUU" +
                "KC4zFPgHywYhIEOSgBtW1fPXiS3rir5DrUoFO+0lQAPaT9UbV8+DqdWhlXg5wnEOylK8l36mNL3YLFFi" +
                "jWfI8VzbF7OxRtuYyAWYFFxPhCzLGAiQdSuLR5AJSIZWKuylJgZ+YYFJMhRxygAXns5S3e4mcY4O6hYi" +
                "r2xOCeUxzWHsA63yhdN52xoaVYC+dp5+wbh8wbPzIBUMlsWH0CgRhtBMaN1IGv0VHDGBrusJvLJJWbtB" +
                "8+RLmnbXa5iB+UKVGwPxfsnGd/ZCaRpgabdPnNHFyrqIqpKagIiTMGCKeasZ21s/eeSvwjmIu/ymHW4O" +
                "Kxr2WXtqdzCON7HysvhSaz6L9pcHqgejB3WNdaoUcWjnMG48PnuL7WOqj3UR6JDXeLHjroE44zV2HiLV" +
                "OqQPLvFQoqkO8NFupktY2IymlTIjvK1gmuYcZpDe4mbS6bgJNLMpMCMYVXGJw4J2CRBNp2jwEn1DoBlk" +
                "MVCnBAUM702pynJrKyQktLvJNWocDbcC51TfmSDaD/9ISjVHKARpir1r4rw1SjYmQYt8EZRvdzg7S/QP" +
                "d+DTffo9KBglp9+aOEQsUubFb30MWl9jQ1hPrL9jz200VsEk9E+U5ZbJtaOmGj1PZR3/o5locycr9D5G" +
                "w7NVnxiI1cWHwsdpPU31h85kbDsSG2HlIGN7p5K1XLjqMJ72cLK63iPzD8TLZuWnZulAbC9pXu7GlZ0O" +
                "DS0diFdLGoojVl53aGjpQOw2K6fnl4e0dCD+2l3ZfYWVvSzVeOoNKSQX9AwDIx4TWOxwiOmKCS75eejs" +
                "hCZcF+LMFV3BKdoIiqCInRmbjtOzMjUVGa4KXiHoub1TSU6BwSuKwZa3AOFEGpwSKzWJ1TPdTUTNngoW" +
                "928HYppjYuxcqaVrgkrT0RzYpdPkCDPF35szaqnmOFNUdFXm1JCvj9KtXDQC518cxZT/+CkjGdcNA+RY" +
                "y4sEEDfJWZZ28JATZ8GaxibF2qy+DkibnslVyYwVLktmYe5rleKIf9xhPdX8Bo57Sm1X+CglezMp+BX3" +
                "p23tHDpg1U8lrvjirR8yaN4+Ldqnr8+l/i9cZiST0rk73tDhTI62T4fNeMUSr1MkYzOe9x815djC0lk+" +
                "e9CNH8t+0huU3zI8WewUWRzrBN/C3Y8aW11jg6NY0xkjWtLoviY+2NnmRH7GKNJy4kGxGQx257twVGsy" +
                "/Ol0usfD/WhL3jns0IU4jqt6rspNOe/qGEnj9TP405zSZ+x1Dkoung3W57hM7IuvffTk0D0S/0sQx0fL" +
                "P69e/ndc3kgw/biz+6ljzPOFDhYdrvDv43D16VKLlsvmO+ckAbPj7IHgGaolyH6sgWJnIt8l3fMYuJS9" +
                "CpP3FHqATbx9WSpO0wLQ+etlJj3Nsux/3QjFUnAbAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
