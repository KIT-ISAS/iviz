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
        internal Detection2D(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Detection2D(ref b);
        
        Detection2D IDeserializable<Detection2D>.RosDeserialize(ref Buffer b) => new Detection2D(ref b);
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACr1ZW3PcthV+56/AjB5kJbu0G2c0GWc6bW01jR7aurWTXjweDUiCu7BBggFI7dK/vt85" +
                "uCxXWjl5qLVjeXkBDs71O5c9E1eq1b3yQopvrkSjRlWP2vbCKT+ZsSzOijPxdqu9wD+vO22kE6MNq2sj" +
                "vdetriVtWYlqGoXuazM1oDdYr5mS7lvrurAExISQxtid7jcgckwhHiqwHu/8oGp6JWpnB4FHupMbBbq6" +
                "H8ECkwInlRLGYr9qcJIYt7iVbqPi8rIoflSywe2Wvwpse0WHisHZSlbagEnli79XHyD4j/NgQcFr/y89" +
                "bl9br969j0x52gmZKzv1DTFf2b3wk3Ppnk62TKUsXsaHL+2etmAp7X6LFaRjOUqslqPYqF455pwOVekk" +
                "8USXqsTdhnQCPqFKaVgNA9ZaaNm2QXxQZDEvSvE3O2LLL5N2WMMaNEZMoFpLr/xKeCv0KDo5k8ZUN4xz" +
                "WXjVe+tuOr/xT69Zu95OrlY3utsQx9etGN2kVjgI5u+U97Smtv0ode+juFgi64+kgoWly6Ky1sBpbtJb" +
                "pndFHAX2QAV6HlVfz0JCNpikg/R6MGrhhvFMXwYnvJVmUmKnjWH5jf6ozCwa3bawcOtsF1SCE7QyjfCK" +
                "HFIoWW/x3ehb3UzQ5F1jl0HU2U68g4VlAnBpqGRyil3eQIVM8SAUPX8LDUGVo+P7+OJGN0Xx+//zp/jr" +
                "m7+8EH5sgsWCY4P3N6PsG+kaaGuU7F6k4K3ebJVbG3WrDDbJjpwnON88QKOLwA6OaKBKNg+Eqm3XTT3F" +
                "JWTXsMJyP3ZCq1IM0o26nggSamsdPJ6Wt052UMgZLfNwSFhYwfIv2OKqnkYNhmYCCqekJ6XBLYoJUf38" +
                "G9pQnL3d2TVuFUVxPjyEDJhV+wGBQnxK/wJnfBWEK0EbylE4pUEI8bMb3PoL4BaxoAYLN3gCzl/P4xa+" +
                "Ra5yK52WFVwOhGtoAFTPadP5xYIysf1C9LK3iXygeDjjt5DtM12Sab2FzQxJ76cNFIiFiHQ4KJZWMxOp" +
                "jVaAOqMrJ91c0K5wZHH2A+k4OCpbBN/ANFtrRpMdwCs5JFvjC3rjrfaI0+CQD4EoRP5Tn9Bim18Hi2Y0" +
                "OZUwyoSb8EV4EnmKbRdQGxIIwMHCh0chm4ZJIMYXRIQEaI8RMHHs9RXQcIIvSDg/4HY7dbJfwxkbNhmT" +
                "hLU74J5hhFIOFt/aCYgyKEd0hQz4Y+3HaQjBkGOP/quAuaV4o9SRfn7m62twVuKeg7SzjuFOapMxhIwV" +
                "pD7kqJkSIFTVwkHI9QIQRl0EuKTUEPKPeDnT2lt4T066LHnYFUWBd6WM6WQPXH/3bP2792XRGivHy2+F" +
                "r8Fb4uTyiuyTT7xny4jPvCbSr8juAsxRfUHByjsTFW/hwK3e441TQG+WKuT2GEPhjGSqnO82CjuhKNQF" +
                "kA8IEVk6SsxQFkdQXtGRTcNKppQy9RnlTRU8kVXE/OG7RzqNoPd9CKIjl6plT7khCVfNTBWASBGbkxY5" +
                "PmWNPp8RUhUtYjejOgn5WrP12si1D4cNyPPsUJSzt/I2qjMTOGyMxsF5UTlz8DeKPYrBV5bxKGgY4fiF" +
                "oODXDk8JxykCcIhARWfQdw8VI1j8IMEjwRcCvlaOgIHqlIKIRd7PxD/tbt3JD9BWphSMEj3hcn8J988i" +
                "w2JO76MfW6fzclgLmoaDeK6TZHDHtdwveYwwhAIB9B3yLRVDarkXNqc09GS/EvNKfFoJZ8cF7oh/C6J4" +
                "7/F/Tj/+Lz++SFH47vnl+4Uwj2c6ct0T+r1vrhXVCvS4ie8DiCO7LZVdCtiQgjstKP4xIVe5nuke1j2W" +
                "gGAluWNOQBGeAv+QhZyTWD4SN8PjPl/N+erT47B/UN2pkDrS553Qwt0vB70TnCG4Pi9Ruto9RgVx1Dix" +
                "C95tuULNENCXQ4dSC0cOpfIA+AF3DbCyQ4MD4hykJPyACDc+dKpZZ6AwcR3KouJizXZnuOWM6UP5j64V" +
                "yahaMxGC35o5L6l3AENqL5ESw0tuWdkuoSFjWqGtTUwvpVpRaxaFQrlIJfWBq5fM5VZ25x45uw9pQJoN" +
                "rDxuyX65s8zO+ySLenE3EE+my6S2+zBAXT6/TMd4/Ukd0T9F73RfDG1ALlT/qXVf1gNL6+UKBGfdHLyS" +
                "b+fHg0D2wCvyEh4v4Oa1QceCYhR/xHs7wcmeh+TLdkCOiK0UunRKT9RGKfRmIRTZj2KqiVvvIyzaNYdG" +
                "gucjMB05DYyQhyFyGEwcl6Bhlh8DJ3gKj0OOQJ2BK9I2L4AbRzUb2atUYWiXvYxXUTTYAf0Ft4DUXZN3" +
                "e+VuA3Xwuax9monLVJxTo9DBZZC9FTvFdQonR/Q8CAM7YLhCUsW5B3eeQUTx03XwTZzg4E2DIucOp0Nl" +
                "AnV1r1DhhCab6XLzTo2hawFmrM3BoE47qKxEo9FwI7+kwwUFFXSVYrVSlR8tAdFGDCp80nMemBBRmIyd" +
                "ez4HelCWM8j40BBAQWM2gIoWJ503gTkQdErTGlic0Cgce6jXuKIjfOYRDl0DOUYENXF2aKVBf0u9fBJu" +
                "zLFN1QuzMXE1qcQsd0Ih0lwuHblOSnMaOomUiyv6M9ThzFFcehcaSzJQBtXGxukH6EE2GDZlmNR1x5rt" +
                "ZJKnyp0Hg+indGtN8yvJBcyN8ouNSe7OtpIk98ZYkBvVJvQaxwo8UaOK7tlKPLvg3poGjgNGKS31qw4R" +
                "zpAV1t2ZMALa+HMm4uPDBCM3Rhh4IS9EJXJfn8ml7UefTCt18wtSFLiI5tijgE6NbycfIgRzbYDeWBc2" +
                "3Cd0aJo+T+nrfW6K40g29JIYO3HNlIeTDxKYjwk0dtf/to2fjjcGlLMR5GJC+iwFGmwSgCd1higJ8XJ4" +
                "Gim9YjVQ2/4QuRxxxzOYAx/cugO3x4cocApVABJNg240JH1sLYs0GQNsk2IPWwLh8HiVpmMr0U9dFczn" +
                "7M6n3TvdgJ97u/nxyc21NVPX+5T2jdrANWI1ROiAft0yOMfKqtUAVe/qp0z4Jr32ZT0Mh/HqTgZH8XFi" +
                "ScWEBM6jsIrjKs4yK/EBhsU2jIXXgGTn/0iTGF+G8TQWYbLf08AHJkOgN1x/dRiixHF5+HWB6CZGAEXx" +
                "iMx5UsWf0wPqX7iqEeu1qDGg65HDOgUw6zer0P3xFRUipw1JlkROppo7zqKJb5538OGUkdNPJE+XCHVX" +
                "a9tg9+9o2FxpFBUN+r9oOcLrNPjL7/6QJ6ijGo4Y+oFKDfgCbNhv4ATgoJpH/ObBB+AHDia02IAylhJt" +
                "6Jv5bRCZTsZcFeS/Yt+6KIr/AdoULBI/GgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
