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
                "H4sIAAAAAAAAE8VYbVMbuxX+vjP8B034YLhjTAMJpXSYDuHlhjtJ4AbutLeZDKPdlW2FteRIWmyn0//e" +
                "5xyt1gu4b/SGeki81h6d1+e8SOvrT/iIt6dHJ6cfxVP2Np+1bC1bF9djJcZKlsqJoZMTJQrrnPJTa0ov" +
                "ghUB76fWq/i2Lz5cXDdr2oSborJ1GV8N1rK3kU9kx+z/e7su3vx0enwtzj+cXTzduGjasTVBauOFNkPr" +
                "JjJoa4TMbR3YgrCYKiFNmUzU/N4OhRRDW2Pd5l9UEYjTlYVj8CaM2RNaVXDORI/GQRgbRE5rVaVKPBWy" +
                "BgmxxDd4q2Js9NdaeVFaJiZKej8R1j2kdyACJchIqlO0j5WdLIPFarMu6oGe8fvGqcKODFtzM/Ejv33B" +
                "69e0jzYzq8KaoS6VKdSBGNuZ8LVTYmFrIfGtg9AI/liGhje7iZSX+G8MNa2heK+Lc6bMVZgpZcTvmO5l" +
                "61Sgg2wCikDfclVEHwggw8rKsLsjlso8BTQJMsfvfrm6Pv149VTYRBdTqIOeIArkjUIaoebBySJiZreM" +
                "wG+8k6vKmlFKk+irPuAWg0NZ4VOorNMjbWQlvDLeOk+yNqJLIEM6JxdCRqERWGN5p0B8p9xy0+ZaFp9i" +
                "YC9JlWNKwZ1Pn7sZ6R/Y0m+NsaZaiKmzd/A3gJ4TgDRMyO1824/lFBkOkYZs2C0j5KAh/ZkHpQCqEH3U" +
                "5L3y45bZDSSOV2rA0tmwnRIcp8hEb5NuvQDEywoUI31HypGsuO9LjTQYKeCPsBJs7fqUPaT/POVFJUHD" +
                "PhAzSjEkZaXNLeVgDM9QO1AQEmUdLJWDgqStZSNFWrpFx6lwZ2tNI9E/raABn5cXV6e/SUFLsSBrUKPr" +
                "KkQYEkgWuS0XwOoUEPTiYBksBM5T+YrVnVxr4KAwZgqOwYD9V6NkeE5/J0stzTZ8oiJOgcva17J67Cqv" +
                "/gxWx/ZOOmwp1FWQk6kqWfJalh3+xp/s/dWPB8KHMsqP/YZgFlBypCsFlJOlDFTB0YeQR8ptVXAPMqjR" +
                "jN9SGfSDjkdHylCeAXuxalvgbDKBSwoZUDUJwd392Am3AqHSAUV1JR3orQNciDy2Q9Dgz1MNh2PE+ckB" +
                "Yderog6IASRpUwDvnrLv/ERkNWCHWogN2fr1zG7hpxpR9UzCY6yhLIKM6JOe0h9Axg/RuAF4wzmo/tS6" +
                "N3jtBj/9poAQqKCmthij6BhxuUAniwnNkcsrRYwpH8C1R5t6mx3OhlkbVP/EPnJcyvhP2JqWL9m0NUbM" +
                "KrLe1yM4EIRNXUIbXTT9Qyukc6VzJ90io11RZLZ+xmgOFD6OCJVR722hEYCS8Z354Ig7R+NGl98Ljf++" +
                "6/4vY9DJcwxBj2YeqghUO8LMAlCBmxiwLmNJQHqgFtMYUPNoAyyXaqipsBoSmeaRVJlv1WLVwBJrVGQh" +
                "qP0HjcnKpU4TK1WZr6VAgs2SZ5l3FfLBOk4H2PHT1cWHbWRvypFfj96/E5HFQByZNNHosp21xETeKm6x" +
                "ipGTfLOchLGXBBPScunVQJwORgNu9I+j36dBh1ptZe0tsHuLCevF33rk6N5B79jWxfjkTa8ves7agJVx" +
                "CNOD7e3KIk3g9ND7+4vGSMfZY6gWmTtyj6X5IUaRC1wcRzt+oMFLhx42aWQnqtitUk3dGFZqrnNd6bAY" +
                "JCeugC5sVtGPXBJ1IU7eRJAwF7ILRau8NwkzzGr4Crmq5kjPSvkDWjyDjo3B/FswpwPReiEukiOw+NAR" +
                "B6//sP+qISksSkhBGoLwsdq9JO3q53cYZ1HDx7Yq23jdF371tXqbSBr2LE70ZiO/u9csTa3D0utXuzvx" +
                "N03nRILRws4SDUrODGX/4bpBPMigJOUmauyb1xNb1hURBKqQwU57LcYJ7t+ra64eHFMTRJPxcoSzoKWz" +
                "jex4nHL3w1aJ4mt8hGAcgftiNtZoKBO5AJMi1hkhy1I3SO1WHI+AE64MrVSecicQv7DAwBkKHkHAJc5v" +
                "qaJ30zpHb3ULkVc2pwzzmPcWlKil8oXTeds0GlWUCe3o/SLC9EWcsgdZg/4oK55gWSIMoanRupE0+hs4" +
                "Ykjd0BN4ZYvSeJNGzpclze01zMDkocrNgbhcsvGdvVCaZlza7RNn9LeyLlhVUhMocRIGTDGMNQN+6yeP" +
                "fFaWJHKvtMOtYUXHgqi9jsfRuCkqL4uvtfZNBWoPYA+GEuonG1Q5dsvkbL85uH9uJwE7J1Qx6yLQqbDx" +
                "YsddA3E+TAc8OA+Rah3SBxc+vmgPPvEoONMlLGzm1kqZEX6tYJomoMgg/eLNpNNJE+jIpsD0YFTlk6na" +
                "JUA07aPBC/uGQDPIOFBnBAVM9xESWZZbWyElod1NrlHyaPIVONf6zmzRvvhTUqo5ayFIU+xdF+9ao2Rj" +
                "ErTIF0H5doezs0T/cAde3affh4IsOX3WxRFikTKP3/Uxgn3jBrGRWP8QPbfZWAWTSu5pLZNrR52WPU9V" +
                "Hv/QXLS5k5VO59pnqz4ciNXFh8IX03qa6g+d2qLtSGy3aIKM7Z1K1nKJVSfiaR8Hr+t9Mv9QvGxWfmmW" +
                "DsXOkublHq/sdmho6VC8WtJQHLHyukNDS4dir1k5e3dxREuH4vfdlb1XWNnPUpGn9pBC8kHGVGY8JrDY" +
                "4RBTVyS4iM9DZyc0+7rAsxi7IqZoI4hBwY0am07SszI1FZlYFbxSNPndqSSnwDQWGkXeAoQTaXCErNSE" +
                "q2e6xmDNvhcs7l8hcJpjkuxcxdFdAqV/pT2bHuigOcKE8cfmAFuqOU4bFd2wOTWM103pNo+NwOEYhzTl" +
                "P33OSMZ1wwA51vIiAcRNxixLO+LMw9NhPWUC1mb1fUHa9EyuSmascFkyC3Ngq1SM+KfdqKea38Bx31Pb" +
                "FT5KyV6kE8nje9e2dg4dsOqnslDxkhAZNG+fFu3Tt+dS/5/ccyST0omc7/JwWkfbD80tSLxrkRGbfBPw" +
                "qClzC0un/OxBN34sm9n+vwxPFjtFFnOdkO090zJq0eoaGxzFOtDUdcmHFdZ9XXy0s62J/IJRpOUkEw4I" +
                "F3vzPTiqNRn+dHqeji1Ot+Sdww/do+MYq+eq3JLzro5MShcy5+BPc0o/Yq9zcHKKblI25n2BgfRbHz05" +
                "dI/KfxHE8dHyr6uX/8rLmwmmn3b3PneMeb7QwaKjFf59HK4+XXfRctm8jzlJwOw4eyDiDNUSZD/XQLEz" +
                "zHdJ9zwGLmWvwuQ9hR5gE7++LhWnaQHo/NdlJj3NsuwfsmxcCa8bAAA=";
                
    }
}
