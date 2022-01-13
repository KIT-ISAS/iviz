/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            TrackingId = string.Empty;
        }
        
        /// Explicit constructor.
        public Detection2D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox2D Bbox, SensorMsgs.Image SourceImg, bool IsTracking, string TrackingId)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceImg = SourceImg;
            this.IsTracking = IsTracking;
            this.TrackingId = TrackingId;
        }
        
        /// Constructor with buffer.
        public Detection2D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
                size += BuiltIns.GetArraySize(Results);
                size += SourceImg.RosMessageLength;
                size += BuiltIns.GetStringSize(TrackingId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e38d1866e74825fff6b9ec7ca5865dc2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZW3PbuBV+Ln8FZvxge1di0s1OppNOp23i7q4f2k036TXj8UAkJCEBAS5AWmJ+fb9z" +
                "DkhKtpLdh8YejyWCwMG5fufiM3Vl1tabpLT65krVpjNVZ4NX0aTedWVxVpypt1ubFH6TbazTUXVBdldO" +
                "p2TXttJ0ZKFWfaesr1xfg14bkmVK1q9DbGQLiCmlnQs76zcgckwhX6qwH+9Sayp6paoYWoUl2+iNAV3r" +
                "O7DApMDJyigXcN7UuEl1WzzquDF5e1kUPxhd43HLHwWOvaJLVRvDSq+sA5MmFT+u3kPwH4Y2gEKy6V+2" +
                "274Oyby7yUwlOgmZV6H3NTG/CnuV+hjHZ7o5MJWyeJkXX4Y9HcHWgtVoWMe609itO7Ux3kTmnC41403q" +
                "wpamxNOGdAI+oUrtWA0t9gZoOaxFfFBkMS9L9bfQ4cjPvY3Ywxp0TvWgWulk0kKloGynGj2QxkzTdkNZ" +
                "JONTiLdN2qQn16zdFPpYmVvbbIjj67XqYm8WuAjmb0xKtKcKvtPWpywutujqA6ngwNJlsQrBwWlux7dM" +
                "74o4EvZABXrujK8GpSEbTNJAets6c+CG+c5UihPeadcbtbPOsfzOfjBuULVdr2HhdQyNqAQ3WONqlQw5" +
                "pDK62uKztne27qHJ+8YuRdQh9HyChWUCC9IVjGzY5R1UyBRnoWj9LTQEVXaRn/OLW1sXxR/+zz/FX998" +
                "/0KlrhaLiWOD9zed9rWONbTVaXYvUvDWbrYmLp25Mw6HdEPOI843tIaFHgNbHNFBlWweCFWFpuk9xSVk" +
                "t7DC4XmchFa1anXsbNUTJFQhRHg8bV9H3Riijt8Eh4SFDSz/gi1uqr6zd2Q0AEU0OpHS4BZFj6h+9g0d" +
                "KM7e7sISj4aieLpcQgbMmn2LQCE+dXqBO74S4UrQhnIMbqkRQrx2i8d0CdwiFkwb4AYX4Pz10G2DYMWd" +
                "jlav4HIgXEEDoHpOh84vDyh7Ju21DyN5oTjf8WvI+okuybTcwmaOpE/9Bgq0jEhwUGxdDUykctYA6pxd" +
                "RR2Hgk7JlcXZd6RjcVS2CD6BaaGyjCY7gNfokGyNL+iNdzYhTsUhPwWiEPnPfkSL7fRaLDqhyamEUY64" +
                "CV+EJ5GnhPUB1EoCATgE+HCndF0zCcT4ARGlAdpdBkxce30FNOzhCxrOD7jd9o32SzhjzSZjkrB2A9xz" +
                "jFAmwuLb0ANRWhOJrtKCPyF86FsJhin26M8KmFuqN8Yc6eef/P0anJV45iBtQmS409ZNGELGEqnnHDUo" +
                "gcw1HIRcT4Aw60LgklKD5B/1cqC9d/CeKel2M3xmUeBdY8aM2gPX3z1d/vamLNYu6O75typV4G3k5PkV" +
                "2cfc0/5sy4zPvCfTXxm+uOb6goKVT45UUoADr+0eb6IBerNUkttzDMkdo6mmfLcxOAlFoS6AfECIzNJR" +
                "YoayOIKmHQ3ZVHYypTFTn1HeNOKJ3SQBPj3SaQa930sQHblUpT3lhlG41cBUAYgUsVPSIsenrOGnOyRV" +
                "0SZ2M6qTkK8tW2+duU5yWYs8zw5FOXur77I6JwLzwWwc3JeVM4i/UexRDL4KjEeiYYTjF4KCX7p8TDjR" +
                "EIBDBCo6Rd8eKkawpFZXhuELAV+ZSMBAdUpBxDLvZ+qnsFs2+j20NVESo2RPeL5/DvefRIbFot1nPw7R" +
                "TtthLWi6o+imOkmLOy71/pDHDEMoEEA/It8uxI3ns7A5paGL/UINC/VxoWLoDnBH/VsRxQfL/zm9/F9e" +
                "vhyj8N2z5zcHwjye6ch1T+j3obkWVCvQcp3fC4gjux0qu1SwIQX3uKH4e49cFT3Tnfc9loBgZXTHKQFl" +
                "eLKjrDrj0ZG4Ezzup2/D9O3j47A/q+5USB3p815o4ennWe8EZwiuz0s0fts9RgVx1DixC95vuaRmEPTl" +
                "0KHUwpFDqVwAX3DXASsbNDggzkFKwreIcJekU510Bgo916EsKr4s2+wfSjJmkvIfXSuS0WrJRAh+K+a8" +
                "pN4BDJm9RkqUl9yysl2kIWNa0taOTB9KtaDWLAuFcpFK6pmrl8zlVjfnCTnbm9xDb2DlbtuUB53l5LwX" +
                "k6iX9wPxZLoc1fYQBqjL55fjNcl+NEf0T9E73RdDG5DL5kQGlR7WA4fWmyoQ3HW7P34cHg8C2QOvyEt4" +
                "vICH1w4di+FsQbyvezjZM0m+bAfkiNxKoUun9ERtlEFvJqHIfpRTTT76EGHRrkU0Ejwf8VIUwAjTMES3" +
                "rcvjEjTM+oNwglV4XKTyn4rG99I9o7z1o5qd9masMGycvIx3UTSEFv0Ft4DUXSvmKt4JdfB5WPvUPZep" +
                "uKdCoYOvZZ4V7AzXKZwc0fMgDEJrDdddee7BnaeIqP5xLb6JGyK8qTXk3HI7VKZQV3tj6txkM11u3qkx" +
                "jGuAGWuzdajTZpWVaDRqbuQP6XBBQQXdyrBaqcrPloBoXQgujXqeBiZEFCZj5x7Oo+Es55DxoSGAgq0X" +
                "VNHipvNamAPBaCztaag6Q0DztXO9xhVdEpn7jr4DObpzKTXnVtpRsRfrSbhuim0jPUCDo2KXQe+UQaTF" +
                "qXTkOmmc09BNpFzD6KcddThDFpfeSWNJBppAtQ55+gF6kA2GHTPM2HXnmu1kkqfKnQeD6KfsOrj6F5IL" +
                "mOv0FxuT3J9tjZI8GGNBblSb0GseK9i8++LpQj295N6aBo7t0pk19avRS0+R992bMKr8c6by8jzBmBoj" +
                "pSvkhaxE7usncurUz0Rr7OYPSFHgIppzjwI6FT6j/hQhmGtjGWjkwENCc9P0eUpf76emOI9kpZe0m203" +
                "Npaflenr4ZhAHXb+1x38eHxQUC5kkMsJ6bMUrmXPpE6JEomXeTVTesVqoLb9U+SmiDuewcx8cOsO3O4+" +
                "RYFTqAGQ2MCzICRPaS2LcTIG2CbFzkeEsCwvxunYQvm+WYn5Ytil8fTO1uDnwWlePnm4Cq5vfBrTvjMb" +
                "uEauhggd0K8HBudcWa0tQDXF6gkTvh1fp7Jq23m8utPiKClPLKmY0MB5FFZ5XMVZZqHew7A4FkNaApJj" +
                "+hNNYlIp42ls2pjS08DH05AXf6j+arR1eVwu/10guiMjgKJ8xcT5qIq/jAvUv3BVo5ZLVW2198hhjQGY" +
                "+c1Cuj/+RoXIaUOSJZGT/TyLJr553sGXU0Ye/0Xy5BCh7mttK3b/HQ2bVxZFRY3+L1suHQz+pnd/nCao" +
                "nWmPGPqOSg34AmzoNx0NwtVq6EwqzviGdzdC6eAE6ljKtNI481uRma6+YPpfsXNdFocUfjMef/cTEOSm" +
                "KP4HxXCf/1waAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
