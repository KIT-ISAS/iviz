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
        
        /// Explicit constructor.
        public Detection3D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox3D Bbox, SensorMsgs.PointCloud2 SourceCloud, bool IsTracking, string TrackingId)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceCloud = SourceCloud;
            this.IsTracking = IsTracking;
            this.TrackingId = TrackingId;
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
            IsTracking = b.Deserialize<bool>();
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
            if (Results is null) BuiltIns.ThrowNullReference(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) BuiltIns.ThrowNullReference($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (Bbox is null) BuiltIns.ThrowNullReference(nameof(Bbox));
            Bbox.RosValidate();
            if (SourceCloud is null) BuiltIns.ThrowNullReference(nameof(SourceCloud));
            SourceCloud.RosValidate();
            if (TrackingId is null) BuiltIns.ThrowNullReference(nameof(TrackingId));
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
                "H4sIAAAAAAAAE71YbW/cuBH+XP0K4vwh9mGtXuyrL0hhFJcs0hi4JrlLen0xAoMrUbu8SKJCUrY3v77P" +
                "zJBa2dn0+qGxkdgSNRzO6zMzPFBL09jeBKXV6VLVJpoqWtcrb8LYxrI4KA7Uu40NytxG09dEt9LBVkRd" +
                "tToE29hK85bVVtm+asfa9ms1uGB51faN8x1TLMBLKd227oZI9H0GcqYCPb6FwVT0ac5Jafzu9Nqo6JhX" +
                "dGplVOvAwNREEDd41X5tvBCWRfHS6BqvG/5TYNtzOlUN3q30yrbgbUKplg426F1UG31N7JMqRm22gwPX" +
                "gM8sWNuSRMGuWsMiuNVvMJmydVjw6aFyPtP2W1pntq0NJKL2RuH0scOzCP9dWbxmFi/TQTb8w8bNGxfM" +
                "5ftkkkBiw+ArN/Zs3ZW7VWH0Pr/TwSJIWTxLi8/cLW0BacE+NOxgHTWodVRr0xvPZmPt8knq0JamxNua" +
                "TA4jQVndqgoPA2jdGJVrxPbgyDY+KiVCZp5WVrT25uNoPfZl00mAOQ9jBdg4qk5vmRssYbohbssimD44" +
                "f9WFdfjjG2f7+Lx1Y30C+tFX5qqiN9LoooEIOKczIVBI3OigotfVBzKIaLMQisaatlbBRHwfERIr51pI" +
                "eJWpmdtSjSEJWrk+kLv6aqs0NEe0dOBmh9bMMiSdG5L217odIYRtW9antR9Mu1W1bRoEX+NdJwarZ9Ig" +
                "Xo2uNvhb22tbj7Dz/VAoRdGtG0X+SZ0FWQwhwLGqWxiTOe6UovV3rG6Int/ThysL853/n3+Kv73961MV" +
                "Yi1+k5yD7G+j7mvta1grag4+MvDGrjfGH7fm2rTYpDsKLQnN7WBYaYmokMK0hSnZPVCqcl039oQZ0N3C" +
                "C/P92EkwoQbto61GQAHonUc+EHnjdWeIO/4FhCY8bOD5p+xxU43RXpPTkPreAORgNIRFMSIGT09oQ3Hw" +
                "7sYd49UQwEyHS0IxRg4IPJJTh6c441tRrgRvGMfgFKDBIa9d4TUcKRwCEczgEAaHkPzNNm6cwNi19lYD" +
                "ZYhxBQuA6yPa9Ohoxrln1r3uXWYvHHdn/C9s+4kv6XS8gc9a0j6MaxjQMlgiQEEKjCcmVWtNT6C28tpv" +
                "C9olRxYHL8jGEqjsEfwF4LnKMtbcANpyQLI3vmI0XtuAPJWA/BLEQuUf+4zhm+mzeBQGjdr2YW8xKzOq" +
                "IhYRSRQprpkBsRQ3gINDDEel65pZIMfnOKkB6THBKY69WAIXR8QCsMwCjDdjp/tjBGPNLmOW8HZnFlJQ" +
                "kBvw+MaNQJTBeOKrtOCPcx/GQZJhyj36heoNfH9rzB37/MrPF5CsxDsnaYc6RnCnbTthiK2z1rvyuVUC" +
                "mQ0ChEJPgDDZQuCSCodUJ/VsS7TXiJ6pIYg7+EyqILpyMfe6B7Zffnf8+H1ZNK3T8ex7qbFZkrMl+cfc" +
                "s/7OlwmfmSbxX0nprrn1oWTlnZlLcAjgxt7iizdAb9ZqoEqkUg7JGdlVUzVcG+yEoSpVQT8gRBLpTtmG" +
                "sTiDJoqOfCqUs4aCUOqVI4ijSIyTBqmwJtD7syTRnZCq0CStzKTcSuorAJEydipaFPhUNfrpDClVRMRh" +
                "ZijrTW3Ze02SOshhA7oADihUb26YxJwTg93G5Bycl4yzzYU9GMrB547xSCyMdPxKUPB7h+eC4w0BOFSg" +
                "Plfs3cPESJYw6MowfCHhK+MJGKhbKYhZkv1A/eJujjv9G6w1cRKnpEg4uz1D+E8qw2Pe3qY4dt5O5PAW" +
                "LB0pu1H2IAuH47G+ncuYYAgNAvh71FtpQGd74XMqQ4e3C7VdqE8L5V2c4Y76pyKOny3/a//yv3n5KGfh" +
                "5enZ+5kyD+c6Ct099v3cXQvqFWi5Tt8FxFHd5sYuVcFt5kRQ/DyiVvme+e7oHkpBiJLDcSpACZ5s1lUn" +
                "PLqj7gSPt9PTdnr69DDi70y3L6Xu2PNeauHt487uBGdIrv+uUX66eYgO4s5YxSF4fyCTnkHQNzuLqgvC" +
                "jTOJnjmRqLIn/D88U8vXL6jzWqKH6um4VAxgOmJKA6OlYYMSesHMUK6o8CzS+ETHdXadZjmZi9EPImQ8" +
                "sKD6ALjfNRV3vLXIRbYZIyaJcjYnfilZ9pY00WUPwucviW+wn8w+Bou5hvvH2kfZYlLMwPt+RfmV58pT" +
                "PuRhYj2duLd2XPO3uyFe0thxwYOC6zFmdEYjhTHRTDuxsca4XAkswWToP9DpsKfrdEEBHp3+AJaIFhn+" +
                "hgHMNM13fWjFS5Fn60NTrsuFutmYXqjk1gUceKpCn+LtGhMp79w1EMRTJeVQUpoTnmpFZjlMWpVcHjD/" +
                "pxH1hhTCg0/DHF9wZLm4g43OLbikCYs92DfN88C9iMb3d1Hg67j6C7cQ2dlZyI1r+U6scpikqpwhr47r" +
                "nM26FagO5AeLJJQLj4TsXxoLcrr2tNJyokbiF1HGTaw4NsBFikAeswIcJt2sVisMvB7Q0LrVgvGm1VuC" +
                "ntqEytvVNMklUThqU2Z+w/cL4RtAj9fbCRTkLL592fV+K2pb1rpHztXqpMasSRdCx3T5cUSd7uOa2o+x" +
                "5/bE1IiUNzs2YbYXQmM77w6ZM0aMeqxyC1uhqfHIDzOgBRPvhMlOiBRvHPfWNMC65rhp7XoTRXqaTqmv" +
                "500ivK4+jlYgTkCVe/J7NwU00x1S63taZ2OHo/LzC8WTJQTwY0Ugmq04M1cpV1UmGQ+emgyyABeSE4vg" +
                "85glubE1NLRS9FvTr/G2h2m+lhAG+Y03k0zL5GhhU2Gk700bsqrW54BIFSDFC9uGgqaUnugFhcLle7ly" +
                "CoXcnOHHhquVBYjUaP0UQC3MBv7pw1+yUCL3FZw0YO+B+mlSatbHrLbRhGmHdzeZ/v4OfNrRH9CGJ5CQ" +
                "j84/qM5wRk49/raQ6gMhDzPvb8V0R8Wcxx8Sg8tfkBPvi+musGa4nfjTzZqy7BW60cX/nq6MMcUCTyVS" +
                "vtol231kYiftByYZwSkShoxN6EmSWaCg36YAwPb5XWvmIogksfZEXbx694TUP1eP08rf09K5OtnRPD7j" +
                "ldMZDS2dq+93NORjrPxpRkNL5+osrbz46fWPtHSufpivAPHP1ZMi30fQVUh2ySstac6xmgPJNQ3dizLB" +
                "a3nmu1jMzz7KVECmkPRNB3Ec0EUkbVrmZ9OPBECCGAE1Hb0cBt90ToWWJSZBXiJAO7r7N63pGFlzm8WS" +
                "Ff8B+pjR83QZAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
