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
                "H4sIAAAAAAAAE71YbXMbtxH+nPsVGPMDpQxFxZKtuupoOrIkx87YlmMp06Yejwa8A0lYR4AGcCLpTv97" +
                "n10Ax5PENo0bmZNYd7jFvj77AvR6X/ETL8+OT8/ei6/Zm35F0ROXUyWmSlbKibGTMyVK65zyc2sqL4IV" +
                "Ad/n1qv4dSDenl+mNW3CVVnbpoqfhsXLyCZyA+/fb9L585/OTi7Fq7cvzr/eLrLqxJogtfFCm7F1Mxm0" +
                "NUKObBNY+bCaKyFNla3T/N2OhRRj22Ddjj6pMoDRhYVL8CFM2Qda1XDLTE+mQRgbxIjW6lpVeCplAxLi" +
                "iL9grcqp0Z8b5UVlmZgo6ftMWHeX3oEIlCCDUKdoG6s6yzFilVkRdVvH+OfKqdJODBtyNfMTv3vO65e0" +
                "jfaCT2nNWFfKlOpQTO1C+MYpsbKNkPirg9AI+FSGxJj9Q2pL/DOFgtYgxj3xiglHKiyUMuIHJnvcOhOA" +
                "IGMAHJC3TBXRB4BiXFsZ9vfEWpXfj5OMkpPXv1xcnr2/+FqkFCm8Qc/gefJDKY1Qy+BkGWGyX0WYJ7+M" +
                "VG3NJCdF9NIACIsxoRzwOULW6Yk2shZeGW+dh6it6AyIkM7JlZBRZsTSVN4o0N4ot96zXcSHGM53pMgJ" +
                "pdveh4/d7PO37Bi0hlhTr8Tc2Rv4GbgeEWY01B/Z5a6fyjlyGfIM6b9fMcigHf1n7uR8wdRRizfKT1tW" +
                "V5A33SCdJbNFexXYzZF03ma9+gHwljUoJvqGFCNBcd+nBpifKCCO8BFs4waUKaT7MmVBLUHCxosFZRPy" +
                "r9bmmtItRmWsHSgIe7IJlhK/JGHFRJGObtVxJtzYWpLk+a+pWkDku/OLsz+gauUQkCGowU0dIvAIF6uR" +
                "rVZA5xyg8+JwHSPEy1ORitWbnGrgmzBlCvb+kF3XoDh4TnUnKy3NLvyhGJlAYuMbWd9zkld/A6MTeyMd" +
                "NpTqIsjZXFUstyiO/uBf8ebix0PhQxXFx25C6AooLtJVArrJSgaq0egyyBvldmr4BhmTFOOvVO38sOPO" +
                "iTKUV4BcLMwW8JrN4I9SBhRHAm53P3bCpwCmdEBPU0sHeuuAEyJPva5HZJ7qNPwiXp0eEmS9KpuAAECS" +
                "NiVg7inhXp2KogHeUPawoehdLuwOXtWE6mQWHgMNZRFhhJ70lP4QMr6Pxg3BG85Bkae+vMVrV3j12wJC" +
                "oIKa23KKImPEuxWaVUxiDtyoVsSY8gBc+7Spv93hbJi1QZnP7CPHtYz/ha1p+ZJNO1PErCbrfTOBA0GY" +
                "ShE65Sp1Cq2QxrUeOelWBe2KIoveC4ZyoPBxRKhsem9LjQBUDO7CB0fcORpXunooNP52c/1/xpzThx9y" +
                "7s00VAyobISFBZwCdywgXcZqgORAAaZu3/DsAiRXaqypnBpITCNHKsbXarVpIom1Ke4X1OSDxtzkcmOJ" +
                "Faoa5RCCS2ZYjbqq+GAdpwEs+Oni/O0usjbnxq/Hb16LyGAojk0eWXTVjlFiJq8Vt1LFiMleWY+32Au5" +
                "BLCR9GoozoaTIbfz+0Ef0CRDTbW29hqQvcYE9eifffJw/7B/Yptyevq8PxB9Z23AyjSE+eHubm2RHfB2" +
                "6P/rUTTRcc4YqkDmhjxjaUiI0eOyFufMjhdosNKhj00aOYnada1UqhbjWi31SNc6rIbJgxvwCoNVdCLX" +
                "QV2K0+cRG8yErEKlqm5NuASuBn5CfqolUrJWXIteQMFkLL0KZnMoWgfwGrkAa3ddcPj0z8+eRIrSomSU" +
                "pBzo7mvcT5Iufn6NMRUle2rrqo3TLcEXn+uXmSLyZlGiv5j4/YO4MrcOK0+f7O/xK83aRIDpwS4SBYrL" +
                "AgX+zrJBCMiQLOAqqurj15mtmpq+B6qEwc77GdCA9kP1xs3zYG51aCVeTnCcs3RIkR0/U5q+3alQYo2P" +
                "kItz7UAsphptYyZXYFLGeiJkVemEzG5l8QgyAcnQSu0pVQLxCytMkqHkKQNc4nSW63Y3iUfooG4lRrUd" +
                "UUJ5THMrSstK+dLpUdsakirKhHaefhRx+SjOzsNcMKKseAhliTCEZkLrJtLoL+CICXRLz+CVHcrabZon" +
                "H9O0u9XADMwXqtoeindrNr6zF0rTAEu7feaMLlY1JatKagIiTsKAOeatNLa3fvLIX2VJIndEO94Z1zTs" +
                "R+11PFfGTVF5WX5utE8Fpz1Q3Rk9qGtsUaXgoT2Gcfv+2VvsnVJ9bMpAh7zkxY67huLVOB/Y4DxEqnXI" +
                "AFz4UKKpDsSj3UJXsDCNprUyE7xtYJrnnMggv/Fm0uk0BTqyKTEjGFX7bKp2GRCpUyS8sG8INMOCA/WC" +
                "oIDhPZWqYmRtjYSEdlcjjRpHw63AOdV3Joj2w1+zUukIhSDNsbcnXrdGyWQStBitgvLtDmcXmf7uDnxa" +
                "0/dowzNoyKLzryeOEYycevxtgEnrC3eErcz7++i67aLL47vE4MN75MTHZDHMrbi7tfwvHTVcjgqVfPyP" +
                "RqPNjax1Psh+s8rEQdpcmCi0MeXnuTbReS26BQa6VQIAtneqXMslVqSItWc4dV0+I/OPxOO08ktaOhJ7" +
                "a5rHB7yy36GhpSPxZE1DMcbK0w4NLR2Jg7Ty4vX5MS0diT91Vw6eYOVZkes/9Y0ckrcypjljNQPJjseY" +
                "vCLBeXweOzuj6dcFnsfYFTF9kyDGAXdtbDrNz8o0VIBixfBK0fR3o7KcEkNZSIq8BEBn0uAEWasZV9Z8" +
                "b8GaPRQsbt8ccAnANNm5bstXCLX2bHqgk+YE88Zf0vm1UkucN2q6RnNqHK+W8o0dG4GzMY5pyn/4WJCM" +
                "y8QAqdPyIgHETcYEzDviAMRzYjNnAtZm81VB3vSNXJXN2OCybBZmwlapGPEP+1FPtbyC4x5S2w0+ysle" +
                "5lPJ/bvVtq6OHbDq57JU8UYQGbRsn1bt05dvpf5/uOjIJuUzOd/e4byOkSCka5B41SIjNvku4F7D5vaW" +
                "z/nFnU59X/aD3q78luHZYqfIYq4Tsr1mWkctWt1gg6NY0/mDLUm698R7u9iZyU8YU1pOMuOAcHGwPICj" +
                "WpPhT6fzHZ91uiXvHIToshxHWb1U1Y5cdnVkUr6aBn+aYQYRe51DlONzw9ZyIDCsfhmgX4fucfnvgjje" +
                "W/518/I/eHk7w/TD/sHHjjHfLnSw6HiDf++Ha0AXXrRcpe8xJwmYHWcPRZyvWoLi5wYodob5rum+jYFr" +
                "2ZsweUuhO9jE2+e14jQtAJ3/vczkp0VR/BusDwPVjBsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
