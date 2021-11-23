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
            b.SerializeArray(PointClouds, 0);
            BoundingMesh.RosSerialize(ref b);
            b.SerializeStructArray(BoundingContours, 0);
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
                "H4sIAAAAAAAAE71Ya28buRX9Pr+CiD/IXshyYyeu68IoHD+aLBI7G3vRboPAoGYoifGIVEiOJaXof++5" +
                "l+RobKvdbrqOsBvPcC7v89wHubHxDT/x+uz49OyD+Ja96VcUG+J6osREyUo5MXJyqkRpnVN+Zk3lRbAi" +
                "4PvMehW/9sXF5XVa0ybclLVtqvhpULyObCI38P7tJl2++vHs5Fq8uTi//Ha7yKoTa4LUxgttRtZNZdDW" +
                "CDm0TWDlw3KmhDRVtk7zdzsSUoxsg3U7/KzKAEZXFi7BhzBhH2hVwy1TPZ4EYWwQQ1qra1XhqZQNSIgj" +
                "/oK1KidGf2mUF5VlYqKk71Nh3UN6ByJQggxCnaJtrOo0x4hVZkXUfR3jnxunSjs2bMjN1I/9ziWvX9M2" +
                "2gs+pTUjXSlTqkMxsXPhG6fE0jZC4q8OQiPgExkSY/YPqS3xzwQKWoMYb4g3TDhUYa6UEX9gsuetMwEI" +
                "MgbAAXnLVBF9AChGtZVhb1esVPntOMkoOXn789X12Yerb0VKkcIb9BSeJz+U0gi1CE6WESZ7VYR58stQ" +
                "1daMc1JEL/WBsBgTygGfI2SdHmsja+GV8dZ5iNqMzoAI6ZxcChllRixN5J0C7Z1yqz1bRXyI4XxPipxQ" +
                "uu1+/NTNPn/Pjn5riDX1UsycvYOfgeshYUZD/aFd7PiJnCGXIc+Q/nsVgwza0X/mQc4XTB21eKf8pGV1" +
                "A3mTNdJZMlu0W4HdDEnnbdarFwBvWYNirO9IMRIU931ugPmxAuIIH8E2rk+ZQrovUhbUEiRsvJhTNiH/" +
                "am1uKd1iVEbagYKwJ5tgKfFLElaMFenolh1nwo2tJUme/5aqBUS+v7w6+x2qVg4BGYIa3NQhAo9wsRza" +
                "agl0zgA6Lw5XMUK8PBWpWL3JqQa+CROmYO8P2HUNioPnVHey0tLswB+KkQkkNr6R9SMnefU3MDqxd9Jh" +
                "Q6mugpzOVMVyi+Lod/4V767+eih8qKL42E0IXQHFRbpKQDdZyUA1Gl0GeaPcdg3fIGOSYvyVqp0fdNw5" +
                "VobyCpCLhdkCXtMp/FHKgOJIwO3ux074FMCUDuhpaulAbx1wQuSp120Qmac6Db+IN6eHBFmvyiYgAJCk" +
                "TQmYe0q4N6eiaIA3lD1sKDau53Ybr2pMdTILj4GGsogwQk96Sn8IGT9E4wbgDeegyFNf3uS1G7z6LQEh" +
                "UEHNbDlBkTHi/RLNKiYxB25YK2JMeQCuPdrU2+pwNszaoMxn9pHjSsb/wta0fMmm7QliVpP1vhnDgSBM" +
                "pQidcpk6hVZI41oPnXTLgnZFkcXGOUM5UPg4IlQ2vbelRgAqBnfhgyPuHI0bXT0VGn+9uf4/Y87p0w85" +
                "j2YaKgZUNsLcAk6BOxaQLmM1QHKgAFO3b3h2AZIrNdJUTg0kppEjFeNbtVw3kcTaFPcLavJBY25yubHE" +
                "ClUNcwjBJTOshl1VfLCO0wAW/Hh1ebGDrM258cvxu7ciMhiIY5NHFl21Y5SYylvFrVQxYrJXVuMt9kIu" +
                "AWwovRqIs8F4wO38cdD7NMlQU62tvQVkbzFBPftnjzzcO+yd2KacnL7q9UXPWRuwMglhdrizU1tkB7wd" +
                "ev96Fk10nDOGKpC5I89YGhJi9LisxTmz4wUarHToYZNGTqJ23SqVqsWoVgs91LUOy0Hy4Bq8wmAVnch1" +
                "UJfi9FXEBjMhq1CpqnsTLoGrgZ+Qn2qBlKwV16JzKJiMpVfBbA5F6wBeIxdg7aELDl/+6eBFpCgtSkZJ" +
                "yoHusca9JOnqp7cYU1GyJ7au2jjdE3z1pX6dKSJvFiV687Hf248rM+uw8vLF3i6/0qxNBJge7DxRoLjM" +
                "UeAfLBuEgAzJAm6iqj5+ndqqqel7oEoY7KyXAQ1oP1VvXD8P5laHVuLlGMc5S4cU2fEzpenFdoUSa3yE" +
                "XJxr+2I+0WgbU7kEkzLWEyGrSidkdiuLR5AJSIZWak+pEohfWGKSDCVPGeASp7Nct7tJPEQHdUsxrO2Q" +
                "EspjmltSWlbKl04P29aQVFEmtPP0s4jLZ3F2HuSCEWXFQyhLhCE0E1o3lkZ/BUdMoJt6Cq9sU9Zu0Tz5" +
                "nKbdzQZmYL5Q1dZAvF+x8Z29UJoGWNrtM2d0saopWVVSExBxEgbMMG+lsb31k0f+KksSuSPa0faopmE/" +
                "aq/juTJuisrL8kujfSo47YHqwehBXWOTKgUP7TGMW4/P3mL3lOpjUwY65CUvdtw1EG9G+cAG5yFSrUP6" +
                "4MKHEk11IB7t5rqChWk0rZUZ420N0zznRAb5jTeTTqcp0JFNiRnBqNpnU7XLgEidIuGFfUOgGRQcqHOC" +
                "Aob3VKqKobU1EhLa3Qw1ahwNtwLnVN+ZINoPf8lKpSMUgjTD3g3xtjVKJpOgxXAZlG93ODvP9A934NN9" +
                "+gMoyJLzb0McIxY58/hbH4PWV24Im5n1D9FzW8kqmFRxB2uZXDtqqux5Kuv4H81EmztZ63xY/W7VhwOx" +
                "vvhQ+GJaz3L9oTNZtB2J7ZYpyNjeqWQtl1h1Ip4OcLK6PiDzj8TztPJzWjoSuyua5/u8stehoaUj8WJF" +
                "Q3HEyssODS0dif20cv728piWjsQfuyv7L7ByUOQaT70hh+RCxlRmPGaw2NEI01UkuIzPI2enNOG6wDMX" +
                "uyKmaBLEoODOjE2n+VmZhopMrApeKZrw7lSWU2LwCkmR1wDhVBqcEms15eqZ7yZYs6eCxf3bAU5zTIyd" +
                "K7V8TVBrz6YHOk2OMVP8OZ1RK7XAmaKmqzKnRvH6KN/KsRE4/+IopvzHTwXJuE4MkGMtLxJA3GTMsrwj" +
                "Djk8CzYzJmBt1l8H5E3fyVXZjDUuy2Zh7muVihH/uBf1VIsbOO4ptV3jo5zsZT55PL4/bWvnyAGrfiZL" +
                "FW/9kEGL9mnZPn39Xur/h8uMbFI+d/MNHc7kaPshXXXE6xQZscnn/UdNmVtYPssXD7rxY9lPeoPya4Zn" +
                "i50ii7lOyPYqaRW1aHWDDY5iTWcMtiTpviE+2Pn2VH7GKNJykhkHhIv9xT4c1ZoMfzqd7/Gs0y1557BD" +
                "F+I4ruqFqrbloqsjk/L1M/jTnNKP2OsclByfDTYXfYGB9GsfPTl0j8R/F8Tx0fIv65f/wctbGaYf9/Y/" +
                "dYz5fqGDRcdr/Ps4XH261KLlKn2POUnA7Dh7IOIM1RIUPzVAsTPMd0X3fQxcyV6HyXsKPcAm3r6sFKdp" +
                "Aej872UmP82L4t/dCMVScBsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
