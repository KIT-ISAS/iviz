/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Detection2D : IDeserializable<Detection2D>, IMessage
    {
        // Defines a 2D detection result.
        //
        // This is similar to a 2D classification, but includes position information,
        //   allowing a classification result for a specific crop or image point to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results;
        // 2D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox2D Bbox;
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg;
        // If true, this message contains object tracking information.
        [DataMember (Name = "is_tracking")] public bool IsTracking;
        // ID used for consistency across multiple detection messages. This value will
        //   likely differ from the id field set in each individual ObjectHypothesis.
        // If you set this field, be sure to also set is_tracking to True.
        [DataMember (Name = "tracking_id")] public string TrackingId;
    
        /// Constructor for empty message.
        public Detection2D()
        {
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox2D();
            SourceImg = new SensorMsgs.Image();
            TrackingId = "";
        }
        
        /// Constructor with buffer.
        public Detection2D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeArray(out Results);
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesisWithPose(ref b);
            }
            Bbox = new BoundingBox2D(ref b);
            SourceImg = new SensorMsgs.Image(ref b);
            b.Deserialize(out IsTracking);
            b.DeserializeString(out TrackingId);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Detection2D(ref b);
        
        public Detection2D RosDeserialize(ref ReadBuffer b) => new Detection2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
            Bbox.RosSerialize(ref b);
            SourceImg.RosSerialize(ref b);
            b.Serialize(IsTracking);
            b.Serialize(TrackingId);
        }
        
        public void RosValidate()
        {
            if (Results is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) BuiltIns.ThrowNullReference(nameof(Results), i);
                Results[i].RosValidate();
            }
            if (Bbox is null) BuiltIns.ThrowNullReference();
            Bbox.RosValidate();
            if (SourceImg is null) BuiltIns.ThrowNullReference();
            SourceImg.RosValidate();
            if (TrackingId is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 49;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Results);
                size += SourceImg.RosMessageLength;
                size += BuiltIns.GetStringSize(TrackingId);
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "vision_msgs/Detection2D";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "e38d1866e74825fff6b9ec7ca5865dc2";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71ZW3PcthV+56/AjB4kJbu0G3c8GXc6bW01jR7SOrHTm0ajwZLYXdggwQCgdulf3++c" +
                "A5K70trJQ22NRrsEgYNz/c5FZ+rKrG1rotLqmytVm2SqZH2rgom9S2VxVpypt1sbFX6jbazTQSUvuyun" +
                "Y7RrW2k6slCrPinbVq6vQa/z0TIl2659aGQLiCmlnfM7225A5JhCvlRhP97FzlT0SlXBdwpLttEbA7q2" +
                "TWCBSYGTlVHO47ypcZNKWzzqsDF5e1kU3xtd43HLHwWOvaJLVRf8Sq+sA5MmFv9YvYPg3w+dB4Vo479s" +
                "2r720dzcZqYinYTMK9+3NTG/8nsV+xDGZ7rZM5WyeJkXX/o9HcHWgtVoWMc6aezWSW1MawJzTpea8SZ1" +
                "YUtT4mlDOgGfUKV2rIYOez207NciPiiymJel+rtPOPJLbwP2sAadUz2oVjqauFDRK5tUowfSmGm6NJRF" +
                "NG304a6Jm/jkmrUbfR8qc2ebDXF8vVYp9GaBi2D+xsRIeyrfJm3bmMXFFl29JxUcWLosVt47OM3d+Jbp" +
                "XRFHwh6oQM/JtNWgNGSDSRpIbztnDtww3xlLccJ77XqjdtY5lt/Z98YNqrbrNSy8Dr4RleAGa1ytoiGH" +
                "VEZXW3zW9t7WPTT50NiliDr4nk+wsExgQbqCkQ27vIMKmeIsFK2/hYagyhT4Ob+4s3VR/PH//FP88OZv" +
                "L1RMtVhMHBu8v0m6rXWooa2k2b1IwVu72ZqwdObeOBzSDTmPON/QGRZ6DGxxRAdVsnkgVOWbpm8pLiG7" +
                "hRUOz+MktKpVp0OyVU+QUHkf4PG0fR10Y4g6fiMcEhY2sPwLtrip+mTvyWgAimB0JKXBLYoeUf3sGzpQ" +
                "nL3d+SUeDUXxdLmEDJg1+w6BQnzq+AJ3fCXClaAN5RjcUiOEeO0Oj/ESuEUsmM7DDS7A+eshbb1gxb0O" +
                "Vq/gciBcQQOgek6Hzi8PKLdMutWtH8kLxfmO30K2neiSTMstbOZI+thvoEDLiAQHxdbVwEQqZw2gztlV" +
                "0GEo6JRcWZx9RzoWR2WL4BOY5ivLaLIDeI0Oydb4jN54byPiVBzyYyAKkf/SjmixnV6LRSc0OZUwyhE3" +
                "4YvwJPIUvz6AWkkgAAcPH05K1zWTQIwfEFEaoJ0yYOLa6yugYQ9f0HB+wO22b3S7hDPWbDImCWs3wD3H" +
                "CGUCLL71PRClM4HoKi344/37vpNgmGKP/qyAuaV6Y8yRfv7J36/BWYlnDtLGB4Y7bd2EIWQskXrOUYMS" +
                "yFzDQcj1BAizLgQuKTVI/lEvB9p7D++Zkm6a4TOLAu8aM2bQLXD95unyd7dlsXZep+e/V7ECbyMnz6/I" +
                "PuaB9mdbZnzmPZn+yvDFNdcXFKx8cqQSPRx4bfd4EwzQm6WS3J5jSO4YTTXlu43BSSgKdQHkA0Jklo4S" +
                "M5TFETTtaMimspMpjZn6jPKmEU9MkwT4bJFOM+j9QYLoyKUq3VJuGIVbDUwVgEgROyUtcnzKGu10h6Qq" +
                "2sRuRnUS8rVl660z11Eu65Dn2aEoZ2/1fVbnRGA+mI2D+7JyBvE3ij2KwVee8Ug0jHD8TFDwa5ePCScY" +
                "AnCIQEWn6LuFihEssdOVYfhCwFcmEDBQnVIQscz7mfrJ75aNfgdtTZTEKNkTnu+fw/0nkWGxYPfZj32w" +
                "03ZYC5pOFN1UJ2lxx6XeH/KYYQgFAugH5NuFuPF8FjanNHSxX6hhoT4sVPDpAHfUvxVRfLT8n9PL/+Xl" +
                "yzEKb549vz0Q5suZjlz3hH4fm2tBtQIt1/m9gDiy26GySwUbUnCPG4ofe+Sq0DLded+XEhCsjO44JaAM" +
                "T3aUVWc8OhJ3gsf99G2Yvn34MuzPqjsVUkf6fBBaePpl1jvBGYLr0xKN33ZfooI4apzYBR+2XFIzCPpy" +
                "6FBq4cihVC6AL7jrgJUNGhwQ5yAl4TtEuIvSqU46A4We61AWFV+WXfYPJRkzSvmPrhXJaLVkIgS/FXNe" +
                "Uu8AhsxeIyXKS25Z2S7SkDEtaWtHpg+lWlBrloVCuUgl9czVS+Zyq5vziJzdmtxDb2DltG3Kg85yct6L" +
                "SdTLh4F4Ml2OansMA9Tl88vxmmg/mCP6p+id7ouhDchlcyKDSg/rgUPrTRUI7rrbHz8OXw4C2QOvyEt4" +
                "vICH1w4di+FsQbyvezjZM0m+bAfkiNxKoUun9ERtlEFvJqHIfpRTTT76GGHRrgU0EjwfaaUogBGmYYju" +
                "OpfHJWiY9XvhBKvwuEDlPxWN76R7Rnnbjmp2ujVjhWHD5GW8i6LBd+gvuAWk7loxV+FeqIPPw9qn7rlM" +
                "xT0VCh18LfOsYGe4TuHkiJ4HYeA7a7juynMP7jxFRPXztfgmbgjwps6Qc8vtUJlCXd0aU+cmm+ly806N" +
                "YVgDzFibnUOdNqusRKNRcyN/SIcLCiroVobVSlV+tgRES967OOp5GpgQUZiMnXs4D4aznEPGh4YACrZe" +
                "UEWLm85rYQ4Eg7G0p6HqDAHN1871Gld0UWTuE30HcqRzKTXnVtpRsRfqSbg0xbaRHqDBUbHLoHfKINLC" +
                "VDpynTTOaegmUq5h9NOOOpwhi0vvpLEkA02gWvs8/QA9yAbDjhlm7LpzzXYyyVPlzoNB9FN27V39K8kF" +
                "zCX92cYkD2dboySPxliQG9Um9JrHCjbvvni6UE8vubemgWO3dGZN/WpopafI+x5MGFX+OVN5eZ5gTI2R" +
                "0hXyQlYi9/UTOXXqZ6I1dvMHpChwEc25RwGdCp9Bf4wQzLWxDDRy4DGhuWn6NKWv91NTnEey0kvazTaN" +
                "jeUnZfp6OCZQ+1372w5+OD4oKOczyOWE9EkK17JnUqdEicTLvJopvWI1UNv+MXJTxB3PYGY+uHUHbqeP" +
                "UeAUagAk1vMsCMlTWstinIwBtkmx8xEhLMuLcTq2UG3frMR8we/ieHpna/Dz6DQvnzxcedc3bRzTvjMb" +
                "uEauhggd0K97BudcWa0tQDWG6gkTvhtfx7Lqunm8utPiKDFPLKmY0MB5FFZ5XMVZZqHewbA4FnxcApJD" +
                "/DNNYmIp42ls2piypYFPS0Ne/KH6q9HW5XG5/HeB6I6MAIryFRPnoyr+Oi5Q/8JVjVouVbXVbYsc1hiA" +
                "WbtZSPfH36gQOW1IsiRycjvPoolvnnfw5ZSRx3+RPDlEqIda24rdv6Vh88qiqKjR/2XLxYPB3/TuT9ME" +
                "NZnuiKHvqNSAL8CG7SbRIFythmTENb69uRVCBwdufgIC3AKjEuVbaZ95k0hODFzwLV+xi10Wxf8AJUqB" +
                "OkYaAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
