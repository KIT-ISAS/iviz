/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Classification2D")]
    public sealed class Classification2D : IDeserializable<Classification2D>, IMessage
    {
        // Defines a 2D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide class probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of class probabilities. This list need not provide a probability for
        //   every possible class, just ones that are nonzero, or above some
        //   user-defined threshold.
        [DataMember (Name = "results")] public ObjectHypothesis[] Results;
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg;
    
        /// <summary> Constructor for empty message. </summary>
        public Classification2D()
        {
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceImg = new SensorMsgs.Image();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Classification2D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.Image SourceImg)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceImg = SourceImg;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Classification2D(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Classification2D(ref b);
        }
        
        Classification2D IDeserializable<Classification2D>.RosDeserialize(ref Buffer b)
        {
            return new Classification2D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            SourceImg.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (SourceImg is null) throw new System.NullReferenceException(nameof(SourceImg));
            SourceImg.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Results);
                size += SourceImg.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Classification2D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "90b6e63f97920f8c0f94c08bd35cb96d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWTY/bNhC9C9j/MIAPySa2kqZFESxQtGm3bfbQDyBBL0GwoKWxxIQiVZKyV/n1fUNK" +
                "WnuzCXJojGRtUZzH+XjzOCu65J22HEjRs0uqjApB73SlonaWPIfBxLJYFSt63eowLVDtYGBdpMrZqLQl" +
                "ZUfqXdDJStud811CKOkqEuxqDrqxXBPeAIuWg9iHNR1aXbUUdNcbwHi31zXnHfK0VVttgIwjG71nHEbB" +
                "Db5i0p1quCyKl6xq9tSmrwL4L8joEMnt7kMpcyhph2X4JIHMp6qjvePiLe/ZpwCD3prJtTW9G+QMyV1s" +
                "VSTlGVD2A3u3JudJbd2e4WrHCWMI7Dd1SnYNA2SydaYui7+277iKL8fexRZZCm/eTlkOEsnrlqUutYoq" +
                "n9KwZa9iwuDA8156qEsu8dRIBRADnFWGKvzosdcNko3kB8xy4s5L+hORe/530D6XhpQx4ihVKjAiDI50" +
                "pE6NtGXiro9jWQS2wfnrLjThyZXgTNW41l1zVvzwP3/Oij9e/X5BIdb5xFzqM0TyKipbK19Tx1Gl/EgA" +
                "rW5a5NmgYgZWqpPoc/bGHrWfiYx/OZMGlEPESKcDm7tusEJ+pqg7PrGHpRCdeuWjrgajPPY7X2sr23de" +
                "daDiSrYFZJQt+Hl1eSEdErgaIpiLk7StPKugbYOXVAzaxm+fiUGxen1wGzxyAyYvh+eaw1m+6VFp8VOF" +
                "C5zxKAdXAhvZYZxSgwNp7RqP4RztJC5w79BaD+H532NsQQ0p/155rYTIAK6QAaA+EKMH50fI4vYFWWXd" +
                "DJ8Rb8/4Eli74EpMmxY1MxJ9GBokEBunxqtpOyaQymi2Ed259cqPhVjlI4vVb5JjbBI9kYrgG33oKp3a" +
                "4aBjW4ToBT1V41rXxVcj5F4HNFrm5N0OFna+sOTSMrXLei7mJJmin/dL5tz2YCJ4JDyBjklqJsCkPhAx" +
                "BwZDdeo6QaDZj0BEfIY49TtOvrpEMw9gggL1oRbt0Cm7ARXrVLCskhZJWyddlM5AvVs3mJp69oJLKsEZ" +
                "594PfW6FpfPkzxaSUdIr5pPc/JN+X8GzEs+pRTsHoaxhqk05FwylmqI+ll9sRrZ2oIcQb68M0jHlAvbI" +
                "hShbSkpJP4+yF9eDhL++jTxbTaGAW+K5AHhlIV1vnm6+eVsWO+NU/P47ChV8+3oidlc5z2Y1QrcHUdKF" +
                "G8qi/JCjuemTYGP3w6drenqemB+hWD2EbieU8iiYpGbad+dKRC7SZ0XT8q2+TImBvKsK98BEx9R1C9xs" +
                "fvJZsOZeO4JyPRQShEyv0i2Mb68+BeS8blAV7MsGHwNVKGsO8PNIj28W0jpoqWh6qjUuhTgX/rMxPR5P" +
                "AWp3sF9m+OHUEP9xNjrcKLuQ9rMIV3nPkk5cYyiKrU9XJ6RfUhqkrT4FN1PqjkLe+pFay+gqfgpBdm65" +
                "VXuNPgTjBjtNL8V8b7WcEntrkoHz8nq+u9Zkh26by+fdIczWB13Dn4+s0/K9xpUzQ2eXschwA2qk/g5J" +
                "WKASDtdxkyYxFG2nIW3BV08S8PX8OpRVj9tEEj66gQ4qEyVM84T+IFOg5QPNl0kSVYx7KCzMvAsbmeXC" +
                "T6KUoczTDzZhFLUiyCgZOh0XtcXIBJGbprHkWMKdHYHST0csns+p+HVeQNi9vmETaLOhCtenxVzTsbJ4" +
                "KVMmOjD9CnD7/kISKqneY2zeedelos6zcT4cF4JQvDJDzU+OJepu1tpc9+eoyvVWY3qqNULMlZO7bb6W" +
                "l3c/LvNN5P7Eod8GzJngAmpoG5AAHmzHyJkazzEBJ6AjA1XFAcVGHby+SW9zyHIyph7AP0rcOseN/x9i" +
                "HED60wwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
