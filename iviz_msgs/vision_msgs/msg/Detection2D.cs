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
            TrackingId = string.Empty;
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
            return new Detection2D(ref b);
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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACr1ZW2/byBV+F5D/MIAfHG8lJt0sjCJF0TZxt+uHtmmTXoPAGJFDaZIhhztDWmJ+/X7n" +
                "nOGIsuXsPjQW4oiXmTPn+p2LztSVqW1rotLq2ytVmd6UvfWtCiYOri8WZ4sz9W5ro8K/aBvrdFC9l9Wl" +
                "0zHa2paatizVeuiVbUs3VKDX+WiZkm1rHxpZAmJKaef8zrYbEDmmkA5VWI93sTMlvVJl8J3CI9vojQFd" +
                "2/ZggUmBk7VRzmO/qXCS6re41WFj0vJisfjB6Aq3W/5aYNtrOlR1wa/12jowaeLib+uPEPyHsfOgEG38" +
                "t+23b3w07z8kpiLthMxrP7QVMb/2exWHEKZ7OtkzlWLxKj185fe0BUtp9zusIB3rXmO17tXGtCYw53So" +
                "mU5ST21hCtxtSCfgE6rUjtXQYa2Hln0t4oMii3lRqL/6Hlt+HGzAGtagc2oA1VJHE5cqemV71eiRNGaa" +
                "rh+LRTRt9OGmiZv47Jq1G/0QSnNjmw1xfF2rPgxmiYNg/sbESGtK3/batjGJiyW6/EQqmFm6WKy9d3Ca" +
                "m+kt07sijoQ9UIGee9OWo9KQDSZpIL3tnJm5YTozFuKEt9oNRu2scyy/s5+MG1Vl6xoWroNvRCU4wRpX" +
                "qWjIIZXR5Rbflb211QBN3jV2IaKOfuAdLCwTgEtDJUMw7PIOKmSKB6Ho+TtoCKrsA9+nFze2WjxZ/O7/" +
                "/Hmy+MvbP79Usa/EZuLaT8D+2163lQ4VFNZr9jDS8dZutiasnLk1Drt0Q/4j/jd2UOostsUXHbTJFoJc" +
                "pW+aoaXQhPgWhpjvx04oVqtOh96WA6FC6X2A09PyOugGOjmjZRE+CSMbGP8lG92UQ2/B0EhYEYyOpDd4" +
                "xmJAYL/4ljYszt7t/Aq3hgI5Hy5RA2bNvkOsEJ86vsQZ34hwBWhDOwanVIgifnaD23gB6CIWTOfhCU/B" +
                "+Zux38K9yFtudbB6Da8D4RIaANVz2nR+MaNMbL9UrW79RF4oHs74JWTbTJdkWm1hM0fSx2EDBWIhgh0+" +
                "iqXrkYmUzhqgnbProMO4oF1y5OLse9Kx+CpbBN+ANV9aBpQd8GvySbbGV3XIWxsRrOKTDyEpeekf2wkz" +
                "tvm9GDVjyqm0UUzoCXeEM5Gz+HoGuJJGABEebtwrXVVMApE+I6I0oLtPsIljr6+AiQPcQcP/AbrbodHt" +
                "Cv5YsdWYJAzeAP0c45QJMPrWD8CVzgSiq7SgkPefhk7iIYcf/bcG8hbqrTFHCvoXX1+DswL3HKeNDwx6" +
                "2rqMJLBXkvqQqUZKg1BVDR8h7xM4TLoQ0KQEIVlIvRpp7S0cKKdellx2JVHgYFPeDLoFur9/vvr1h2JR" +
                "O6/7y+9ULMHbxMnlFdknn3jPlgmleU2ivzZ8cMVVBsUr75yoRA8fru0eb4IBhrNUkuFTGMkZk6ly1tsY" +
                "7ISiUB1APoBEYukoPUNZHER5RUM2lZVMacrXZ5Q9jXgiq4j5w3eLpJpw77cSR0cuVeqWMsQk3HpkqsBE" +
                "CtqcusjxKXe0+QxJWLSI3YyqJWRty9arE9dRDuuQ7dmhKHNv9W1SZyZw2JiMg/OSckbxNwo+CsLXniFJ" +
                "NBzN10ODnzuekID9JBiCcUhB1aeovIWWES+x02CTQAwxX5pA2EAFy4KoCfsg8g+/WzX6IxSWKYldkjNc" +
                "7i8RAVlqGC3YfXJlH2xeDoNB2fCRyAWTFo9c6f2cx4REqBRAPyDtUlVk5nthdkpGT/dLNS7V56UKvp9B" +
                "j/qPIor3Hv/39OP/8eOLKRDfv7j8MBPmMa3HyH1CxfcttqSigR5X6b1AOdLcXN+FghkpxKcFi78PSFqh" +
                "ZbqHdY8nI5jJTpkzUcIpEQHikIsS10cSZ5zc56sxX31+LAkO+jsZW0davRNjuPvxoH2CNkTZl4WarnaP" +
                "U1AcNVPii3f7MCkhBIw5jCjTcBRRZhf8Fxh2gM4GXQ+oc8CS/B2i3UVpX7PaQGHgypSlxcWKrc/oywk0" +
                "Sk+AVha5ab1iIoTGJbNeUEMBhsxeI0PKS+5j2TTSpTEt6XUnpudSLalfS0KhgKQi+8DVK+Zyq5vziBTe" +
                "SlbQbgND91syYW43sws/zaJe3I3Ik9lzUtt9QKDWn19Ox0T72RzRP0XvdLMMbUAu9ANTPz8vD+bWywUJ" +
                "zro5OCbfjo8Jh+KEV+QoPHbAzRuHNgblKf6I/XqAn72QdMymQMpI/RW6d8pW1FsZNGwSkOxKKfOkrffR" +
                "Fj1cQHfBcxNYj/wGdshDEt11Lo1R0EjrT8IJnsLpkDJQeeCKFM4L4MlJ0063Zqo5bMiOxqsoIHyHpoP7" +
                "Quq6ycGjCbdCHXzOq6Fq4MIV55QofXApstdqZ7hy4VyJRgiR4DsMXUiqNA/hdlREVP+8FvfECQEO1Rny" +
                "bzkdKlOotFuDmkeab6bLTT11i6EGpLE2O4fK7aCyAq1HxQ3+nA7XF1TirQ2rler+ZAmI1mOAESc950EK" +
                "EYXJ2L/HcwAIZTyHAgAaAi5YzAxQ4+Kk80qYA8FgLK2BxQmQ5NhDBcc1HqE0j3boGuDRI66Js0N/Dfpb" +
                "avAn4foc3lTMMBsD15dGjXqnDIIt5GKSy6ZpfkMnkXJxRX+Oep4xiUvvpNskA2VcrXyaioAeZINhpzwz" +
                "teKphDuZ8KmW54EhOixbe1f9TIoBc73+iuOTu1OvnDTvTbggOupPqDaNG3jYRjXe86V6fsE9N80iO4xY" +
                "ampiA4KcgUvW3Rk+AuD4c6bS48NkI3dLmIUhOyQ9cr+fyU3bjz6Z1tTlz0hR7CKgU+MCOiW+g36IECy2" +
                "AYZjnWy4T+jQSX2Z0q/2uVNO01ppMDGO4vopzy0fJDAeE6j8rv1lGz8fbxSg8wnnUlr6IgWaeRKGT+qU" +
                "QJGQOTxNlF6zGqiXf4hcDrrj2cyBD+7nAd39QxQ4kRpgiaUZOFqUNvWbi2liBuQmxR62CGF5vJymZkvV" +
                "Ds1azBf8Lk67d7YCP/d28+OTm0vvhqaNU/J3ZgPXSDURAQSaeM/4nOqr2gJXYyifMeGb6XUsyq47TF53" +
                "WhwlpkkmlRQaUI/yKo2xONEs1UcYFtswMV4BlUP8A41nYiGTayzC0L+lKRBMhkivuAprMFlJk3T54YHo" +
                "TowAjdIRmfNJFX+aHlA7w7WNWq1UicFdizTWGOBZu1lKP8hXVI6cNqSCJZGWqfhOY2rim4cgfDgl5enX" +
                "k2dziLqrta3Y/Tc0h15b1BUVOsJkOYLsaSCY3/0+T1Z70x0x9D1VG/AF2LDdwAnAwXrs8XMIH4DfPpjQ" +
                "bAOKWcq10knzWxGZTsa8FeS/Yd+6AHr/BAXq0XpbGgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
