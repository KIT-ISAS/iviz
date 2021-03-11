/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/RecognizedObject")]
    public sealed class RecognizedObject : IDeserializable<RecognizedObject>, IMessage
    {
        //#################################################### HEADER ###########################################################
        // The header frame corresponds to the pose frame, NOT the point_cloud frame.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        //################################################# OBJECT INFO #########################################################
        // Contains information about the type and the position of a found object
        // Some of those fields might not be filled because the used techniques do not fill them or because the user does not
        // request them
        // The type of the found object
        [DataMember (Name = "type")] public ObjectRecognitionMsgs.ObjectType Type { get; set; }
        //confidence: how sure you are it is that object and not another one.
        // It is between 0 and 1 and the closer to one it is the better
        [DataMember (Name = "confidence")] public float Confidence { get; set; }
        //############################################### OBJECT CLUSTERS #######################################################
        // Sometimes you can extract the 3d points that belong to the object, in the frames of the original sensors
        // (it is an array as you might have several sensors)
        [DataMember (Name = "point_clouds")] public SensorMsgs.PointCloud2[] PointClouds { get; set; }
        // Sometimes, you can only provide a bounding box/shape, even in 3d
        // This is in the pose frame
        [DataMember (Name = "bounding_mesh")] public ShapeMsgs.Mesh BoundingMesh { get; set; }
        // Sometimes, you only have 2d input so you can't really give a pose, you just get a contour, or a box
        // The last point will be linked to the first one automatically
        [DataMember (Name = "bounding_contours")] public GeometryMsgs.Point[] BoundingContours { get; set; }
        //################################################### POSE INFO #########################################################
        // This is the result that everybody expects : the pose in some frame given with the input. The units are radian/meters
        // as usual
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovarianceStamped Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RecognizedObject()
        {
            Type = new ObjectRecognitionMsgs.ObjectType();
            PointClouds = System.Array.Empty<SensorMsgs.PointCloud2>();
            BoundingMesh = new ShapeMsgs.Mesh();
            BoundingContours = System.Array.Empty<GeometryMsgs.Point>();
            Pose = new GeometryMsgs.PoseWithCovarianceStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public RecognizedObject(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RecognizedObject(ref b);
        }
        
        RecognizedObject IDeserializable<RecognizedObject>.RosDeserialize(ref Buffer b)
        {
            return new RecognizedObject(ref b);
        }
    
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
        
        public void Dispose()
        {
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
                foreach (var i in PointClouds)
                {
                    size += i.RosMessageLength;
                }
                size += BoundingMesh.RosMessageLength;
                size += 24 * BoundingContours.Length;
                size += Pose.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/RecognizedObject";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f92c4cb29ba11f26c5f7219de97e900d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Ya28bNxb9PkD+AxF/kF3IcmMn3qwWRuH40aRI4jR2sdsNAoMzQ0mMR6RCciwpi/3v" +
                "ey7vcDS21e3WW1to4xnO5X2e+yA3Nu7xE69PDo9PPor77G1+WbYhLiZKTJQslRMjJ6dKFNY55WfWlF4E" +
                "KwK+z6xX/LUv3p9dNGvahMuisnXJnwbZa2bD3MD7j5t09uqnk6ML8eb96Zn4v6w6siZIbbzQZmTdVAZt" +
                "jZC5rUNUPixnSkhTJut0/G5HQoqRrbFu8y+qCGB0buESfAiT6AOtKrhlqseTIIwNIqe1qlIlngpZg4Q4" +
                "4i9Yq2Ji9NdaeVHaSEyU9H0qrLtN70AESpBBqFO0Lao6TTGKKkdF1E0d+c+lU4Udm2jI5dSP/c5ZXL+g" +
                "bbQXfAprRrpUplBDMbFz4WunxNLWQuKvDkIj4BMZGsbRP6S2xD8TKGgNYrwh3kTCXIW5UkZ8H8metc4E" +
                "IMgYAAfkLVNF9AGgGFVWhr1dsVLlj+MkoeTo7S/nFycfz++LlKwJb9BTeJ78UEgj1CI4CetJ6b2SYd74" +
                "JVeVNeOUFOylPhDGMaEc8ClC1umxNrISXhlvnYeoTXYGREjn5FJIlslYmshrBdpr5VZ7tjJ+4HB+IEWO" +
                "KN12P33uZp+/YUe/NcSaailmzl7Dz8A1sG9KDfVzu9jxEzlDLkOeIf33yggyaEf/mVs5n0Vq1uKd8pOW" +
                "1SXkTdZIj5KjRbsl2M2QdN4mvXoB8JYVKMYaFDIK4n1famB+rIA4wkewtetTppDuiyYLKgmSaLyYUzYh" +
                "/yptrijdOCoj7UBB2JN1sJT4BQnLxop0dMuOM+HG1pJGHvnyHj/x4ez85E+oWikEZAhqcF0FBh7hYpnb" +
                "cgl0zgA6L4arGCFenooUV29yqoFvwiRSRO8PoutqFAcfU93JUkuzA3+oiEwgsfa1rO44yau/g9GRvZYO" +
                "Gwp1HuR0Bl+T3OxJdvAn/55k785/HAofSlaA+8kTAlhAfZGuFFBPljJQmUajQeoot13BPUiaRrf4lQqe" +
                "H3Q8OlaGUguo49psgbDpFC4pZEB9JOx292Mn3ApsSgcA1ZV0oLcOUCHypt1tEJmnUg3XiDfHQ0KtV0Ud" +
                "EANI0qYA0j3l3JtjkdWAHCofNmQbF3O7jVc1plKZhHOsoSyCjOiTntIPIeM7Nm4A3vAO6jy15s24dolX" +
                "vyUgBCqomS0mqDNGfFiiX3Eex9jlFaDgkX6xVfVoU2+rw5nUHgqDSp/YM8eVjP+FrWn5kk3bE8QMyTlG" +
                "nxnDgSBsqhGa5bJpFlohkyudO+mWGe1ikdnGaURzoPDFiFDl9N4WGgEoI74zHxxxj9G41OXDAfL3Oyww" +
                "ev9h5/jhR507kw2VBCoeYW6BKNQFUADskmsC8gNlmHo+MgT4BphLNdJUVA0kNoNHU5Kv1HLdXMIVivcL" +
                "avVBY3oCDBg/XKfKPEURXBLDMu+q4oN1MRNgwU/nZ+93kLgpPX49fPdWMIOBODRpcNFlO0yJqbyixgpo" +
                "RtAkr6yGXOyFXMJYLr0aiJPBeBCb+t2oYznE1lpZewXUXmGOevqvHnm4N+wd2bqYHL/q9UXPWRuwMglh" +
                "NtzZqSwSBN4OvX8/ZRNp3CL1qGDADcQbbZ+jFysbT5sdL9B4pUMPmzTSEuXrSqmmYIwqtdC5rnRYcsVT" +
                "6wALgxU7MZZCXYjjV4yNyISsQrEqb8y5BK4afkKKqgWyslKxHJ1CwcZYehWRzVC0Dohr5AKs3XbB8MVf" +
                "Xz5nisKiahSkHOjuatxrJJ3//BbDKqr2xFZlG6cbgs+/Vq8TBfOOokRvPvZ7+7wysw4rL57v7cZXmriJ" +
                "ADOEnTcUqC9z1PhbywYhIEOSgEtW1fPXqS3rir5DrUoFO+slQAPaD9ch18+FT1LDQ0PxcoxzHfSlkK9c" +
                "TZn6frtEoTWeUccDbl/MJxrNYyqXYFJwSRGyLGMsQNYtLh5xJiwZWqmwl1oZ+IUlRspQxHEDXHhMS9W7" +
                "m8c5+qhbiryyOeWUx1iH+Q+0yhdO522DaFQBANvB+ilD8ykP0YNUM1gWn0ajRBhCw6F1Y2n0N3DEKLqp" +
                "p/DKNiXuFg2Wz2js3axhBsYMVW4NxIcVG9/ZC6VpkqXdPnFGLyvrIqpKagIlTsKAGQavZn5v/eSRwgoH" +
                "Iu7123a0Papo6mftqenBON7Eysvia635UNpfnaxuDSDUODapWMTpncO4dfcQLnaPqUTWRaDTXuPFjrsG" +
                "4g2vsfMQqdYhfXCJpxNNpYDPeHNdwsJmRq2UGeNtDdM07TCD9BY3k07HTaCZTYFJwaiKqxwWtEuAaJpF" +
                "g5foGwLNIIuBOiUoYIpvqlWWW1shJ6HdZa5R5mjKFTiw+s4c0X74ISnVnKUQpBn2boi3rVGyMQla5Mug" +
                "fLvD2Xmiv70Dn27Sv4SCUXL6bYhDxCJlXvzWx7j1LfaEzcT6O/bcVmMVTEILRWVumVw46qvR81TZ8T/6" +
                "iTbXskL7YzQ8YgGKofiN+kMR5MyepRJE5zM2H7mNyHKcsb1TzVouXHgYUi9xyrp4SR44EM+alV+apQOx" +
                "u6J5th9X9jo0tHQgnq9oKJRYedGhoaUDsd+snL49O6SlA/GX7sr+c6y8zFKlpw6RovKenmFghGTCix2N" +
                "MGMxwRk/j5yd0qjrQpy8ois4SxtBERexP2PTcXpWpqY6w4XBK8Q9t9cqySkwfkUx2PIaOJxKgxNjpaax" +
                "gKZ7iqjZwyHj5l0BQeKYRsfODVu6Nag0ndSBYDpcjjFc/K05spZqgfNFRTdnTo34Nild0kU7cBzGsUz5" +
                "T58zEnLRMECmtbxIAHGTnGtpB087cSisaX5SrM3624G06dG8lQxZ57VkGWbAVi+O+6c9VlUtLuG7h1V4" +
                "jaParG+mBr/mUrWtoyMH0PqZxL1fvApEKi3ap2X79O3xLPiNO47WqnQYjzd3OKhjCqATaLx6idcskkEa" +
                "LwHu9OjY0dIBP7vVnO8Kf+Cbld+zvTXaKTI6lg2+oLsZOza8xg5HEaeDRzSG1QeTj3a+PZVfMJy0nHh0" +
                "bEaF/cU+fNVaDZc6na74cHXakndOQHRXjjOsXqhyWy66OkbSeDMN/jS59BmBndOTiweGzQXuGfviWx9d" +
                "OnTPyf8QxPHO8q/rl/8Zl7cSWD/t7X/uGPOY0aN4Ha5x8d2I9em+i5bL5jsnJ8Gz4++B4MFKJILs5xpY" +
                "dibyXdE9lo0r6WuReUOnWwjF29eV7jRCAKP/veSkpznM+w9skRi5khsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
