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
        
        /// Constructor with buffer.
        public RecognizedObject(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Type = new ObjectRecognitionMsgs.ObjectType(ref b);
            b.Deserialize(out Confidence);
            PointClouds = b.DeserializeArray<SensorMsgs.PointCloud2>();
            for (int i = 0; i < PointClouds.Length; i++)
            {
                PointClouds[i] = new SensorMsgs.PointCloud2(ref b);
            }
            BoundingMesh = new ShapeMsgs.Mesh(ref b);
            BoundingContours = b.DeserializeStructArray<GeometryMsgs.Point>();
            Pose = new GeometryMsgs.PoseWithCovarianceStamped(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new RecognizedObject(ref b);
        
        public RecognizedObject RosDeserialize(ref ReadBuffer b) => new RecognizedObject(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
            if (Type is null) BuiltIns.ThrowNullReference(nameof(Type));
            Type.RosValidate();
            if (PointClouds is null) BuiltIns.ThrowNullReference(nameof(PointClouds));
            for (int i = 0; i < PointClouds.Length; i++)
            {
                if (PointClouds[i] is null) BuiltIns.ThrowNullReference($"{nameof(PointClouds)}[{i}]");
                PointClouds[i].RosValidate();
            }
            if (BoundingMesh is null) BuiltIns.ThrowNullReference(nameof(BoundingMesh));
            BoundingMesh.RosValidate();
            if (BoundingContours is null) BuiltIns.ThrowNullReference(nameof(BoundingContours));
            if (Pose is null) BuiltIns.ThrowNullReference(nameof(Pose));
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/RecognizedObject";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f92c4cb29ba11f26c5f7219de97e900d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71Y/1MbuRX/+fav0IQfDDfGXCChKR2mQ4BcckMgF7hpr5kMI+/KtsJaciQtttPp/97P" +
                "e5LWC7i9XnrEcxd2tU/v6+d9kTY2vuInXp8enZy+F1+zN/2KYkNcTZSYKFkpJ0ZOTpUorXPKz6ypvAhW" +
                "BHyfWa/i1744v7hKa9qE67K2TRU/DYrXkU3kBt6/36SLlz+dHl+JN+evLr7eLrLq2JogtfFCm5F1Uxm0" +
                "NUIObRNY+bCcKSFNla3T/N2OhBQj22DdDj+pMoDRpYVL8CFM2Ada1XDLVI8nQRgbxJDW6lpVeCplAxLi" +
                "iL9grcqJ0Z8b5UVlmZgo6ftUWHef3oEIlCCDUKdoG6s6zTFilVkRdVfH+OfaqdKODRtyPfVjv3PB61e0" +
                "jfaCT2nNSFfKlOpATOxc+MYpsbSNkPirg9AI+ESGxJj9Q2pL/DOBgtYgxhviDRMOVZgrZcQPTPa0dSYA" +
                "QcYAOCBvmSqiDwDFqLYy7O2KlSq/HycZJcdnv1xenb6//FqkFCm8QU/hefJDKY1Qi+BkGWGyV0WYJ78M" +
                "VW3NOCdF9FIfCIsxoRzwOULW6bE2shZeGW+dh6jN6AyIkM7JpZBRZsTSRN4q0N4qt9qzVcSHGM53pMgx" +
                "pdvuh4/d7PN37Oi3hlhTL8XM2Vv4GbgeEmY01B/axY6fyBlyGfIM6b9XMcigHf1n7uV8wdRRi7fKT1pW" +
                "15A3WSOdJbNFuxXYzZB03ma9egHwljUoxvqWFCNBcd+nBpgfKyCO8BFs4/qUKaT7ImVBLUHCxos5ZRPy" +
                "r9bmhtItRmWkHSgIe7IJlhK/JGHFWJGObtlxJtzYWpLk+a+pWkDku4vL0z+gauUQkCGowU0dIvAIF8uh" +
                "rZZA5wyg8+JgFSPEy1ORitWbnGrgmzBhCvb+gF3XoDh4TnUnKy3NDvyhGJlAYuMbWT9wkld/A6Njeysd" +
                "NpTqMsjpTFUstygO/+Bf8fbyxwPhQxXFx25C6AooLtJVArrJSgaq0egyyBvltmv4BhmTFOOvVO38oOPO" +
                "sTKUV4BcLMwW8JpO4Y9SBhRHAm53P3bCpwCmdEBPU0sHeuuAEyJPvW6DyDzVafhFvDk5IMh6VTYBAYAk" +
                "bUrA3FPCvTkRRQO8oexhQ7FxNbfbeFVjqpNZeAw0lEWEEXrSU/oDyPg+GjcAbzgHRZ768iavXePVbwkI" +
                "gQpqZssJiowR75ZoVjGJOXDDWhFjygNw7dGm3laHs2HWBmU+s48cVzL+F7am5Us2bU8Qs5qs980YDgRh" +
                "KkXolMvUKbRCGtd66KRbFrQriiw2XjGUA4WPI0Jl03tbagSgYnAXPjjiztG41tVjofG3m+v/M+acPP6Q" +
                "82CmoWJAZSPMLeAUuGMB6TJWAyQHCjB1+4ZnFyC5UiNN5dRAYho5UjG+Uct1E0msTXG/oCYfNOYmlxtL" +
                "rFDVMIcQXDLDathVxQfrOA1gwU+XF+c7yNqcG78evT0TkcFAHJk8suiqHaPEVN4obqWKEZO9shpvsRdy" +
                "CWBD6dVAnA7GA27nD4Pep0mGmmpt7Q0ge4MJ6sk/e+Th3kHv2Dbl5ORlry96ztqAlUkIs4OdndoiO+Dt" +
                "0PvXk2ii45wxVIHMLXnG0pAQo8dlLc6ZHS/QYKVDD5s0chK160apVC1GtVrooa51WA6SB9fgFQar6ESu" +
                "g7oUJy8jNpgJWYVKVd2ZcAlcDfyE/FQLpGStuBa9goLJWHoVzOZAtA7gNXIB1u674OD5n188ixSlRcko" +
                "STnQPdS4lyRd/nyGMRUle2Lrqo3THcGXn+vXmSLyZlGiNx/7vf24MrMOK8+f7e3yK83aRIDpwc4TBYrL" +
                "HAX+3rJBCMiQLOA6qurj16mtmpq+B6qEwc56GdCA9mP1xvXzYG51aCVejnGcs3RIkR0/U5qeb1coscZH" +
                "yMW5ti/mE422MZVLMCljPRGyqnRCZreyeASZgGRopfaUKoH4hSUmyVDylAEucTrLdbubxEN0ULcUw9oO" +
                "KaE8prklpWWlfOn0sG0NSRVlQjtPP4m4fBJn50EuGFFWPISyRBhCM6F1Y2n0F3DEBLqpp/DKNmXtFs2T" +
                "T2na3WxgBuYLVW0NxLsVG9/ZC6VpgKXdPnNGF6uaklUlNQERJ2HADPNWGttbP3nkr7IkkTuiHW2Pahr2" +
                "o/Y6nivjpqi8LD832qeC0x6o7o0e1DU2qVLw0B7DuPXw7C12T6g+NmWgQ17yYsddA/FmlA9scB4i1Tqk" +
                "Dy58KNFUB+LRbq4rWJhG01qZMd7WMM1zTmSQ33gz6XSSAh3ZlJgRjKp9NlW7DIjUKRJe2DcEmkHBgXpF" +
                "UMDwnkpVMbS2RkJCu+uhRo2j4VbgnOo7E0T74a9ZqXSEQpBm2LshzlqjZDIJWgyXQfl2h7PzTH9/Bz7d" +
                "pX8BBUnyd4n4w3vA+aM4Qjxy9vH3PoatL9wUNjP776P3tpJlMKviLpZ+wI6jxsrep9KO/9FQtLmVtc4H" +
                "1m9WgTgY6wsQhTCm9izXIDqXRdvhDbdMgcb2TjVrucTKEzH1Aqerqxdk/qF4mlZ+SUuHYndF83SfV/Y6" +
                "NLR0KJ6taCiWWHneoaGlQ7GfVl6dXRzR0qH4U3dl/xlWXhS5zlN/yCE5lzGdGZMZMHY0woQVCS7i88jZ" +
                "KU25LvDcxa6IaZoEMSi4O2PTSX5WpqFCEyuDV4qmvFuV5ZQYvkJS5DWAOJUGJ8VaTbmC5vsJ1uyxYHH3" +
                "hoBTHVNj51otXxXU2rPpgU6UY8wVf0nn1EotcK6o6brMqVG8Qso3c2wEzsA4jin/4WNBMq4SA+RZy4sE" +
                "EDcZsyzviIMOz4PNjAlYm/VXAnnTN3JVNmONy7JZmP1apWLEP+xFPdXiGo57TG3X+Cgne5lPHw/vUNv6" +
                "OXLAqp/JUsWbP2TQon1atk9fvpX6/+FCI5uUz958S4dzOVp/SNcd8UpFRmzymf9BY+Y2ls/zxb2O/FD2" +
                "o96i/Jbh2WKnyGKuE7K9TlpFLVrdYIOjWNM5gy1Jum+I93a+PZWfMI60nGTGAeFif7EPR7Umw59O57s8" +
                "63RL3jnw0KU4jqx6oaptuejqyKR8BQ3+NKv0I/Y6hyXH54PNRV9gKP3SR18O3WPx3wVxfLD86/rlf/Dy" +
                "Vobph739jx1jvl3oYNHRGv8+DFefLrZouUrfY04SMDvOHog4R7UExc8NUOwM813RfRsDV7LXYfKOQvew" +
                "ibfPK8VpWgA6/3uZyU/zovg3KQ7WXnQbAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in PointClouds) e.Dispose();
        }
    }
}
