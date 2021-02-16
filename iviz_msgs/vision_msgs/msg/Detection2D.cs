/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection2D")]
    public sealed class Detection2D : IDeserializable<Detection2D>, IMessage
    {
        // Defines a 2D detection result.
        //
        // This is similar to a 2D classification, but includes position information,
        //   allowing a classification result for a specific crop or image point to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results { get; set; }
        // 2D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox2D Bbox { get; set; }
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg { get; set; }
        // If true, this message contains object tracking information.
        [DataMember (Name = "is_tracking")] public bool IsTracking { get; set; }
        // ID used for consistency across multiple detection messages. This value will
        //   likely differ from the id field set in each individual ObjectHypothesis.
        // If you set this field, be sure to also set is_tracking to True.
        [DataMember (Name = "tracking_id")] public string TrackingId { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Detection2D()
        {
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox2D();
            SourceImg = new SensorMsgs.Image();
            TrackingId = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection2D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox2D Bbox, SensorMsgs.Image SourceImg, bool IsTracking, string TrackingId)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceImg = SourceImg;
            this.IsTracking = IsTracking;
            this.TrackingId = TrackingId;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Detection2D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesisWithPose>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesisWithPose(ref b);
            }
            Bbox = new BoundingBox2D(ref b);
            SourceImg = new SensorMsgs.Image(ref b);
            IsTracking = b.Deserialize<bool>();
            TrackingId = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection2D(ref b);
        }
        
        Detection2D IDeserializable<Detection2D>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            Bbox.RosSerialize(ref b);
            SourceImg.RosSerialize(ref b);
            b.Serialize(IsTracking);
            b.Serialize(TrackingId);
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (Bbox is null) throw new System.NullReferenceException(nameof(Bbox));
            Bbox.RosValidate();
            if (SourceImg is null) throw new System.NullReferenceException(nameof(SourceImg));
            SourceImg.RosValidate();
            if (TrackingId is null) throw new System.NullReferenceException(nameof(TrackingId));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 49;
                size += Header.RosMessageLength;
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
                size += SourceImg.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(TrackingId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e38d1866e74825fff6b9ec7ca5865dc2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZW3PcthV+56/AjB4kJbu0G3c0GXcybW01jR7aurXbJPV4NFgSuwsbBBiA1C796/ud" +
                "c0ByV1o7eail0WiXIHBwrt+56Exdm7X1JimtvrlWtelM1dngVTSpd11ZnBVn6s3WJoXfZBvrdFRdkN2V" +
                "0ynZta00HVmoVd8p6yvX16DXhmSZkvXrEBvZAmJKaefCzvoNiBxTyJcq7Me71JqKXqkqhlZhyTZ6Y0DX" +
                "+g4sMClwsjLKBZw3NW5S3RaPOm5M3l4WxQ9G13jc8keBYy/pUtXGsNIr68CkScU/Vu8h+A9DG0Ah2fSj" +
                "7bavQjJv32WmEp2EzKvQ+5qYX4W9Sn2M4zPdHJhKWbzIiy/Cno5ga8FqNKxj3Wns1p3aGG8ic06XmvEm" +
                "dWFLU+JpQzoBn1CldqyGFnsDtBzWIj4ospiXpfp76HDkl95G7GENOqd6UK10MmmhUlC2U40eSGOmabuh" +
                "LJLxKcTbJm3SkxvWbgp9rMytbTbE8c1adbE3C1wE8zcmJdpTBd9p61MWF1t09YFUcGDpsliF4OA0t+Nb" +
                "pndNHAl7oAI9d8ZXg9KQDSZpIL1tnTlww3xnKsUJ77TrjdpZ51h+Zz8YN6jartew8DqGRlSCG6xxtUqG" +
                "HFIZXW3xWds7W/fQ5H1jlyLqEHo+wcIygQXpCkY27PIOKmSKs1C0/gYagiq7yM/5xa2ti+K7//NP8bfX" +
                "f32uUleLxcSxwfvrTvtaxxra6jS7Fyl4azdbE5fO3BmHQ7oh5xHnG1rDQo+BLY7ooEo2D4SqQtP0nuIS" +
                "sltY4fA8TkKrWrU6drbqCRKqECI8nravo24MUcdvgkPCwgaWf84WN1Xf2TsyGoAiGp1IaXCLokdUP/uG" +
                "DhRnb3ZhiUdDUTxdLiEDZs2+RaAQnzo9xx1fiXAlaEM5BrfUCCFeu8VjugRuEQumDXCDC3D+aui2QbDi" +
                "TkerV3A5EK6gAVA9p0PnlweUPZP22oeRvFCc7/gtZP1El2RabmEzR9KnfgMFWkYkOCi2rgYmUjlrAHXO" +
                "rqKOQ0Gn5Mri7HvSsTgqWwSfwLRQWUaTHcBrdEi2xhf0xjubEKfikJ8CUYj8Zz+ixXZ6LRad0ORUwihH" +
                "3IQvwpPIU8L6AGolgQAcAny4U7qumQRi/ICI0gDtLgMmrr25Bhr28AUN5wfcbvtG+yWcsWaTMUlYuwHu" +
                "OUYoE2HxbeiBKK2JRFdpwZ8QPvStBMMUe/RnBcwt1WtjjvTzH/5+A85KPHOQNiEy3GnrJgwhY4nUc44a" +
                "lEDmGg5CridAmHUhcEmpQfKPejHQ3jt4z5R0uxk+syjwrjFjRu2B62+fLn/3rizWLuju6vcqVeBt5OTq" +
                "muxj7ml/tmXGZ96T6a8MX1xzfUHByidHKinAgdd2jzfRAL1ZKsntOYbkjtFUU77bGJyEolAXQD4gRGbp" +
                "KDFDWRxB046GbCo7mdKYqc8obxrxxG6SAJ8e6TSD3h8kiI5cqtKecsMo3GpgqgBEitgpaZHjU9bw0x2S" +
                "qmgTuxnVScjXlq23zlwnuaxFnmeHopy91XdZnROB+WA2Du7LyhnE3yj2KAZfBsYj0TDC8QtBwa9dPiac" +
                "aAjAIQIVnaJvDxUjWFKrK8PwhYCvTCRgoDqlIGKZ9zP1r7BbNvo9tDVREqNkT7jaX8H9J5FhsWj32Y9D" +
                "tNN2WAua7ii6qU7S4o5LvT/kMcMQCgTQj8i3C3Hj+SxsTmnoYr9Qw0J9XKgYugPcUT8povhg+efTy//l" +
                "5csxCt8+u3p3IMzjmY5c94R+H5prQbUCLdf5vYA4stuhsksFG1JwjxuKf/bIVdEz3XnfYwkIVkZ3nBJQ" +
                "hic7yqozHh2JO8Hjfvo2TN8+Pg77s+pOhdSRPu+FFp5+mfVOcIbg+rxE47fdY1QQR40Tu+D9lktqBkFf" +
                "Dh1KLRw5lMoF8AV3HbCyQYMD4hykJHyLCHdJOtVJZ6DQcx3KouLLss3+oSRjJin/0bUiGa2WTITgt2LO" +
                "S+odwJDZa6REecktK9tFGjKmJW3tyPShVAtqzbJQKBeppJ65esFcbnVznpCzvck99AZW7rZNedBZTs57" +
                "MYl6eT8QT6bLUW0PYYC6fH45XpPsR3NE/xS9030xtAG5bE5kUOlhPXBovakCwV23++PH4fEgkD3wmryE" +
                "xwt4eOXQsRjOFsT7uoeTPZPky3ZAjsitFLp0Sk/URhn0ZhKK7Ec51eSjDxEW7VpEI8HzES9FAYwwDUN0" +
                "27o8LkHDrD8IJ1iFx0Uq/6lofC/dM8pbP6rZaW/GCsPGyct4F0VDaNFfcAtI3bViruKdUAefh7VP3XOZ" +
                "insqFDr4WuZZwc5wncLJET0PwiC01nDdlece3HmKiOrfN+KbuCHCm1pDzi23Q2UKdbU3ps5NNtPl5p0a" +
                "w7gGmLE2W4c6bVZZiUaj5kb+kA4XFFTQrQyrlar8bAmI1oXg0qjnaWBCRGEydu7hPBrOcg4ZHxoCKNh6" +
                "QRUtbjqvhTkQjMbSnoaqMwQ0XzvXa1zRJZG57+g7kKM7l1JzbqUdFXuxnoTrptg20gM0OCp2GfROGURa" +
                "nEpHrpPGOQ3dRMo1jH7aUYczZHHpnTSWZKAJVOuQpx+gB9lg2DHDjF13rtlOJnmq3HkwiH7KroOrfyW5" +
                "gLlOf7Exyf3Z1ijJgzEW5Ea1Cb3msYLNuy+eLtTTS+6taeDYLp1ZU78avfQUed+9CaPKP2cqL88TjKkx" +
                "UrpCXshK5L5+IqdO/Uy0xm7+gBQFLqI59yigU+Ez6k8Rgrk2loFGDjwkNDdNn6f09X5qivNIVnpJu9l2" +
                "Y2P5WZm+Ho4J1GHnf9vBj8cHBeVCBrmckD5L4Ub2TOqUKJF4mVczpZesBmrbP0VuirjjGczMB7fuwO3u" +
                "UxQ4hRoAiQ08C0LylNayGCdjgG1S7HxECMvyYpyOLZTvm5WYL4ZdGk/vbA1+Hpzm5ZOHq+D6xqcx7Tuz" +
                "gWvkaojQAf16YHDOldXaAlRTrJ4w4dvxdSqrtp3HqzstjpLyxJKKCQ2cR2GVx1WcZRbqPQyLYzGkJSA5" +
                "pj/RJCaVMp7Gpo0pPQ18PA158Yfqr0Zbl8fl8t8FojsyAijKV0ycj6r4y7hA/QtXNWq5VNVWe48c1hiA" +
                "md8spPvjb1SInDYkWRI52c+zaOKb5x18OWXk8V8kTw4R6r7WtmL3b2nYvLIoKmr0f9ly6WDwN7374zRB" +
                "7Ux7xND3VGrAF2BDv+loEK5WQ2fENb59+04IHRxAGUuJVvpmfisi080XTP4r9q3Lovgf2hQsEj8aAAA=";
                
    }
}
