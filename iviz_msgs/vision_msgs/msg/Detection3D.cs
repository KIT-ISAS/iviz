/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Detection3D : IDeserializable<Detection3D>, IMessage
    {
        // Defines a 3D detection result.
        //
        // This extends a basic 3D classification by including position information,
        //   allowing a classification result for a specific position in an image to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Class probabilities. Does not have to include hypotheses for all possible
        //   object ids, the scores for any ids not listed are assumed to be 0.
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results;
        // 3D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox3D Bbox;
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). This information is not required for all detectors, so it may
        //   be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud;
        // If this message was tracking result, this field set true.
        [DataMember (Name = "is_tracking")] public bool IsTracking;
        // ID used for consistency across multiple detection messages. This value will
        //   likely differ from the id field set in each individual ObjectHypothesis.
        // If you set this field, be sure to also set is_tracking to True.
        [DataMember (Name = "tracking_id")] public string TrackingId;
    
        /// Constructor for empty message.
        public Detection3D()
        {
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox3D();
            SourceCloud = new SensorMsgs.PointCloud2();
            TrackingId = "";
        }
        
        /// Constructor with buffer.
        public Detection3D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Results = b.DeserializeArray<ObjectHypothesisWithPose>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesisWithPose(ref b);
            }
            Bbox = new BoundingBox3D(ref b);
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
            b.Deserialize(out IsTracking);
            TrackingId = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Detection3D(ref b);
        
        public Detection3D RosDeserialize(ref ReadBuffer b) => new Detection3D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
            Bbox.RosSerialize(ref b);
            SourceCloud.RosSerialize(ref b);
            b.Serialize(IsTracking);
            b.Serialize(TrackingId);
        }
        
        public void RosValidate()
        {
            if (Results is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) BuiltIns.ThrowNullReference($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (Bbox is null) BuiltIns.ThrowNullReference();
            Bbox.RosValidate();
            if (SourceCloud is null) BuiltIns.ThrowNullReference();
            SourceCloud.RosValidate();
            if (TrackingId is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 89;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Results);
                size += SourceCloud.RosMessageLength;
                size += BuiltIns.GetStringSize(TrackingId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d570bbfcd5dea29f64da78e043da65ae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71YbW/cuBH+XP0K4vwh9mGtXuyrG6QwiiSLNAauSXpJry9GYHAlapcXSVRIyvbm1/eZ" +
                "GVIrO5tePzQ2EluihsN5fWaGB2ppGtuboLQ6XaraRFNF63rlTRjbWBYHxYF6v7FBmdto+proVjrYiqir" +
                "VodgG1tp3rLaKttX7Vjbfq0GFyyv2r5xvmOKBXgppdvW3RCJvs9AzlSgx7cwmIo+zTkpjd+dXhsVHfOK" +
                "Tq2Mah0YmJoI4gav2q+NF8KyKF4ZXeN1w38KbHtBp6rBu5Ve2Ra8TSjV0sEGvYtqo6+JfVLFqM12cOAa" +
                "8JkFa1uSKNhVa1gEt/oVJlO2Dgs+PVTOZ9p+S+vMtrWBRNTeKJw+dngW4X8oizfM4lU6yIZ/2Lh564K5" +
                "/JBMEkhsGHzlxp6tu3K3Koze53c6WAQpi+dp8bm7pS0gLdiHhh2sowa1jmpteuPZbKxdPkkd2tKUeFuT" +
                "yWEkKKtbVeFhAK0bo3KN2B4c2cZHpUTIzNPKitbefBqtx75sOgkw52GsABtH1ektc4MlTDfEbVkE0wfn" +
                "r7qwDr9/62wfX7RurE9AP/rKXFX0RhpdNBAB53QmBAqJGx1U9Lr6SAYRbRZC0VjT1iqYiO8jQmLlXAsJ" +
                "rzI1c1uqMSRBK9cHcldfbZWG5oiWDtzs0JpZhqRzQ9L+WrcjhLBty/q09qNpt6q2TYPga7zrxGD1TBrE" +
                "q9HVBn9re23rEXa+HwqlKLp1o8g/qbMgiyEEOFZ1C2Myx51StP6e1Q3R83v6cGVhvvP/80/x13d/eapC" +
                "rMVvknOQ/V3Ufa19DWtFzcFHBt7Y9cb449ZcmxabdEehJaG5HQwrLREVUpi2MCW7B0pVruvGnjADult4" +
                "Yb4fOwkm1KB9tNUIKAC988gHIm+87gxxx7+A0ISHDTz/lD1uqjHaa3IaUt8bgByMhrAoRsTg6QltKA7e" +
                "37hjvBoCmOlwSSjGyAGBR3Lq8BRnfC/KleAN4xicAjQ45LUrvIYjhUMgghkcwuAQkr/dxo0TGLvW3mqg" +
                "DDGuYAFwfUSbHh3NOPfMute9y+yF4+6M/4VtP/ElnY438FlL2odxDQNaBksEKEiB8cSkaq3pCdRWXvtt" +
                "QbvkyOLgJdlYApU9gr8APFdZxpobQFsOSPbGN4zGaxuQpxKQX4NYqPyszxi+mT6LR2HQqG0f9hazMqMq" +
                "YhGRRJHimhkQS3EDODjEcFS6rpkFcnyOkxqQHhOc4tiLJXBxRCwAyyzAeDN2uj9GMNbsMmYJb3dmIQUF" +
                "uQGPb9wIRBmMJ75KC/4493EcJBmm3KNfqN7A93fG3LHPL/x8AclKvHOSdqhjBHfathOG2DprvSufWyWQ" +
                "2SBAKPQECJMtBC6pcEh1Us+3RHuN6JkagriDz6QKoisXc697YPvlD8ePP5RF0zodz36UGpslOVuSf8w9" +
                "6+98mfCZaRL/lZTumlsfSlbembkEhwBu7C2+eAP0Zq0GqkQq5ZCckV01VcO1wU4YqlIV9ANCJJHulG0Y" +
                "izNooujIp0I5aygIpV47gjiKxDhpkAprAr0/SRLdCakKTdLKTMqtpL4CECljp6JFgU9Vo5/OkFJFRBxm" +
                "hrLe1Ja91ySpgxw2oAvggEL15oZJzDkx2G1MzsF5yTjbXNiDoRx84RiPxMJIx28EBb91eC443hCAQwXq" +
                "c8XePUyMZAmDrgzDFxK+Mp6AgbqVgpgl2Q/Uz+7muNO/wloTJ3FKioSz2zOE/6QyPObtbYpj5+1EDm/B" +
                "0pGyG2UPsnA4HuvbuYwJhtAggL9HvZUGdLYXPqcydHi7UNuF+rxQ3sUZ7qh/KuL4xfK/9i//m5ePchZe" +
                "np59mCnzcK6j0N1j3y/dtaBegZbr9F1AHNVtbuxSFdxmTgTF30bUKt8z3x3dQykIUXI4TgUowZPNuuqE" +
                "R3fUneDxdnraTk+fH0b8nen2pdQde95LLbx92tmd4AzJ9d81yk83D9FB3BmrOATvD2TSMwj6ZmdRdUG4" +
                "cSbRMycSVfaE/4dnavnmJXVeS/RQPR2XigFMR0xpYLQ0bFBCL5gZyhUVnkUan+i4zq7TLCdzMfpBhIwH" +
                "FlQfAfe7puKOtxa5yDZjxCRRzubEryXL3pImuuxB+Pwl8Q32s9nHYDHXcP9Y+yhbTIoZeN+vKL/wXHnK" +
                "hzxMrKcT99aOa/52N8RLGjsueFBwPcaMzmikMCaaaSc21hiXK4ElmAz9Bzod9nSdLijAo9MfwRLRIsPf" +
                "MICZpvmuD614KfJsfWjKdblQNxvTC5XcuoADT1XoU7xdYyLlnbsGgniqpBxKSnPCU63ILIdJq5LLA+b/" +
                "NKLekEJ48GmY4wuOLBd3sNG5BZc0YbEH+6Z5HrgX0fj+Jgp8G1d/5RYiOzsLuXEt34lVDpNUlTPk9XGd" +
                "s1m3AtWB/GCRhHLhkZD9a2NBTteeVlpO1Ej8Isq4iRXHBrhIEchjVoDDpJvVaoWB1wMaWrdaMN60ekvQ" +
                "U5tQebuaJrkkCkdtyszv+H4hfAfo8Xo7gYKcxbcvu95vRW3LWvfIuVqd1Jg16ULomC4/jqjTfVxT+zH2" +
                "3J6YGpHydscmzPZCaGzn3SFzxohRj1VuYSs0NR75YQa0YOKdMNkJkeKN496aBljXHDetXW+iSE/TKfX1" +
                "vEmE19Wn0QrECahyT37vpoBmukNqfU/rbOxwVH55oXiyhAB+rAhEsxVn5irlqsok48FTk0EW4EJyYhF8" +
                "HrMkN7aGhlaKfmv6Nd72MM3XEsIgv/FmkmmZHC1sKoz0vWlDVtX6HBCpAqR4YdtQ0JTSE72kULj8IFdO" +
                "oZCbM/zYcLWyAJEarZ8CqIXZwD99+HMWSuS+gpMG7D1QP01KzfqY1TaaMO3w7ibT39+BT3fpn0BAOvl3" +
                "ifjyZ4TzB/UM/sjZx98XUoAg52Fm/71Y76iY7gRrhtX0c8A3aMqy9enmFv97uhrGtArclIj4Zpdp9xGI" +
                "nbEfgGTUJo8PGYPQeyTdYQ2/TY7G9vmdauYiyCMx9URdvH7/hNQ/V4/Tyt/T0rk62dE8PuOV0xkNLZ2r" +
                "H3c05Eus/GFGQ0vn6iytvPzpzTNaOld/nK8A2c/VkyLfO9CVR3bJay3pzDGZA8Y1Dd1/MsEbeeY7V8zJ" +
                "Pkr3T6aQNE0HcVDQhSNtWuZn048ENIIMAbUbPRsG3HROhdYkJkFeIRA7uuM3rekYQXM7xZIV/wFtMklb" +
                "XBkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
