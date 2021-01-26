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
            Header = new StdMsgs.Header();
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox2D();
            SourceImg = new SensorMsgs.Image();
            TrackingId = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection2D(StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox2D Bbox, SensorMsgs.Image SourceImg, bool IsTracking, string TrackingId)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAAE71ZW28bNxZ+F5D/QMAPtltJyTaFUWSx2G3ibeuH3WY32WsQGNQMJTHhkFOSY2ny6/c7" +
                "53BmJFtJ+7CJYVgaDnl4rt+5+Exdm7X1JimtvrlWtcmmyjZ4FU3qXF4+mp3hV73e2qTwm2xjnY4qB9lf" +
                "OZ2SXdtK06G5WnVZWV+5rgbFNiTLtKxfh9jIFqKmlHYu7KzfgMoxiXKvwgG8S62p6JWqYmgVlmyjNwaE" +
                "rc/gQWiBl5VRLoCAqXGXyls86rgxZT+EeDT7yegaC1v+oIUz9YJuVm0MK72yDqya9Gj28+odNPBT3waQ" +
                "STb9y+bty5DMm7eFtSSHIfwqdL4mIVZhr1IX4/BMDASmg6ufl9XnYU9nsFcIvMYm0rjOGgd0VhvjTWQZ" +
                "6GYzXKcu7NIs8bQh9YBbqFU71kiLvQEaD+uiCZBkiS+X6q8h48wvnY3YxNp0TnUgW+lk0lyloGxWje5J" +
                "eaZpcw9ek/EpxNsmbdLjG1Z1Cl2szK1tNsL1zVrl2Jk57oI7NCYl2lUFn7X1qUiNLbp6T5o4sDzIr0Jw" +
                "8KLb4XUheU18CZMgBJ1n46teaYgI+zRQgm2dOfDNcm1ail/eadcZtbPOiRqcfW9cr2q7XsPi6xga0Qyu" +
                "sMbVKhlyUmV0tcVnbe9s3UGj9y2/LPL2oeMjLDFTmJPOYHDDceCgSiY5CUbrr6EmUmmOvFDe3Nr60Wz2" +
                "h//zz+wvr358plKuxXTi6+D+Vda+1rGGxrJmTyMlb+1ma+LCmTvjcEg35Efih30LrR7Eu/ikgzbZRBCr" +
                "Ck3TeYpWSG9hicPzOAnFatXqmG3VEVJUIUS4P21fR90Yoo7fBNeElQ2s/4ytbqou2zuyG/AjGp1Ia3CN" +
                "WYdYf/oNHZidvd6FBR4NhfZ4uUQPmDX7FjFDfOr0DHd8JcItQRvKMbilRjTx2i0e0yXgjFgwbYAnXIDz" +
                "l33eBgGQOx2tXsHtQLiCBkD1nA6dXx5Q9kzaax8G8kJxuuO3kPUjXZJpsYXNHEmfug0UaBmi4KPYuuqZ" +
                "SOWsAQA6u4o69jM6JVfOzn4gHYurskXwCZALlWVg2QHMBo9ka8AdP5c33tmEWBWH/BioQuTv/QAa2/G1" +
                "WHQElVN5ZDmBKLwRvkS+EtYH0CuJBRAR4MVZ6bpmIgj0AzJKA8TzgJ64+eYa0NjBHTT8H+C77RrtF/DH" +
                "mq3GNGHwBgjoGKhMhNG3oQOutCYSYaULDIXwvmslIMb4oz8rIPBSvTLmSEf/5O834G2JZw7UJkSGPW3d" +
                "hCQEIIPoU+7qlaDnGn5CHiiYWBQiyEnJQpKSet7T3js40ZSS8wSlRR542ZBOo/aA+TdPFr97C07WLuh8" +
                "9a1KFRicuLm6JlOZe2aYzFrgmveUK1ZGLq+5CKHA5aMDmRTgzGu7x5toAOYsmmT/Ek9yyWCzKQ1uDI5C" +
                "YSgdICXgojB1lLOhMg6ncUdD1pWdQmrM4meUUI04Zh6lwKdHni0Y+HuJqSP/qrSnZDHIt+qFLgCSInhM" +
                "ZLT6PSUSP94i6Yt2sdNRPYVUbtmO68J5kuta1ADsXZTOt/puUOpIYTpZbEQ3FhX14n4UjhSWLwJDlCg6" +
                "mc+WrH7t9iEJRUOgDiGoQhWle+gZwZNaXRmGNEBAZSKBBaqY2YyI8dYZiPw97BaNfgeFjZTEMsUhrvZX" +
                "iIVRZpgt2n1x6BDtuB0Wg7IzhTtVUVrccqH3hzwKNFHZAPoROXgu7jydhd0pNV3s56qfqw9zFUM+QCL1" +
                "b0UUHyz/5/Tyf3n5cgjIN0+v3h4I8+VMR957Qr8PzTWn+oGW6/JegB0Z71DZSwUbUpAPG2Z/65C/ome6" +
                "074vJSBYGdxxTEoFpuwgqy64dCTuiJT78Vs/fvvwZdifVHcqpI70eS+08PTLpHfCNATXpyUavu2+RFVx" +
                "1FmxC95vyqSOEAjm0KEUw5FDuV1wv2CvA1w2aH9AnaOUpG8R4i5JVzsqDSQ6Lk5ZVnxZsOEFcjl/JmkL" +
                "0OEiLa0WTIUguGLel9RTgCWz18iO8pLbW7aMNGxCTHrgge9DwebUuxW5UEVSpT3x9Zz53OrmPCGFezN0" +
                "3BtYOm+b5VH7ObrwxSjv5f1wPJk7R+U9RAOaC/Db6aZkP5ijK06RPN1CQymQzg5JDao9rBCO7DjWJbjt" +
                "dn/vuf+CqYzd8Zo8hocSeHjp0NIYTh3E/rqDwz2VXLycScIovRY6espV1GcZNG8Sl+xSJe+Uow/hFv1c" +
                "RKfBYxUvRQJMMc5QdNu6MmVBV63fCydYhfNF6g+onHwnLTaKXz9o2mlvhorDxtHfeBdFRmjRgHCPSC24" +
                "Yq7inVAHn4fVUN1xCYt7KlQ++Cqyr9XOcN3CmRJNESIitNZwLVZmJNyaiojqHzfiorghwqNaQ24ut0Nl" +
                "CkW3N6YufTjT5QafOse4BrKxNluHym1S2RKdSM29/iEdri6oxFsZViv1AMUSEC2H4NKg53G2QkRhMnbw" +
                "/jwaTnkO6R8aAj7Yek5lLm46r4U5EIzG0p6GijWENl87lW9c4SWRucv0HSCSz6X4nHptR8VfrEfh8hji" +
                "RrqDBkfFLr3eKYNoi2MpyUXTMM+hm0i5hpFQO+p/+iIuvZPOkww0ImwdyoAE9CAbDDukm6EtLwXcyYxP" +
                "5TwPFNFt2XVw9a9kGjCX9Webo9yfgg2SPBh3QW6UntBrmTvYsvviyVw9ueTmm+aU7cKZNTW00UufUfbN" +
                "jqeSqvycqbI8jTjGdknpCimiKJEb/5GcOvUz0hra/QNSFLiI5tK1gE6Fz6g/Rgjm2lgGGjnwkNDUSH2a" +
                "0tf7sWUuk1xpMu1mm4eO85Myfd0fE6jDzv+2gx+ODwrKhQJyJSl9ksKN7BnVKVEi8TKtFkovWA3U03+M" +
                "3Bhxx0OaiQ9u6oHb+WMUOI0aAIkNPCxCApVmczaMzgDbpNjpiBCW5fkwPpsr3zUrMV8MuzSc3tka/Dw4" +
                "zcsnD1fBdY1Ps5L6ndnANUphROiAJj4wOJcqa20BqilWj5nw7fA6Lau2nSawOy2OkspIkwoKDZxHjVXm" +
                "WZxl5uodDItjMaQFIDmmP9GcJi1lkI1NG7P0NA/yNAjGHyrFGm1dGa3LPyWI7sAIoKhcMXI+qOLPwwI1" +
                "M1zZqMVCVVvtPXJYYwBmfjOXVpC/USVy2pBkSeRkP82riW8egvDllJGHf608PkSo+1rbit2/o3n0yqKo" +
                "qNEMFsulg8ng+O6P44g1m/aIoR+o1IAvwIZ+k2lYrlZ9NuIa3715K4QODqCipUQrTTS/FZHp5gsm/xX7" +
                "1uVs9j/1beTneRoAAA==";
                
    }
}
