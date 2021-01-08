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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // A list of class probabilities. This list need not provide a probability for
        //   every possible class, just ones that are nonzero, or above some
        //   user-defined threshold.
        [DataMember (Name = "results")] public ObjectHypothesis[] Results { get; set; }
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Classification2D()
        {
            Header = new StdMsgs.Header();
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceImg = new SensorMsgs.Image();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Classification2D(StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.Image SourceImg)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceImg = SourceImg;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Classification2D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
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
                "H4sIAAAAAAAACrVWTW/cNhC9G/B/GMCHxMmukqZFERgo+uWm8SFtgQS9BIFBSbMSE4pUSWrX8q/vG1LS" +
                "7jpxmkOzcLIrivM4H28e54wueaMtB1L07JIqo0LQG12pqJ0lz2EwsTg9OcMfvWl1mJaodjCxLlLlbFTa" +
                "krIj9S7oZKftxvkuYRR0FQl2NQfdWK4JbwSMlrPYhxXtWl21FHTXG+B4t9U15x3yVKpSG0DjzEZvGadR" +
                "cIOvmHSnGoaDpycvWdXsqU1fsnBGP5PRIZLbfAqpyPGkHZbhmEQzn6wO9o57l3nLPoUZdGkm/1b0fpBD" +
                "JIexVZGUZ2DZW/ZuRc6TKt2W4W/HGWQI7Nd1ynoNCyS0daZGDH+W77mKL8fexRbZCm/fTdkOOZw3LUuN" +
                "ahVVPqlhy17FBMOB5930UBdc4KmRWiAQOKwMVfjRY68bJCXZF9jlFJ4X9Afi9/zPoH2uEiljxFmqVGCE" +
                "GRzpSJ0aqWTiro8jfA5sg/PXXWjCkysBmgpzrbvm9OSH//lzevLq9e8XFGKdT8w1l1BeR2Vr5WvqOKqU" +
                "IYmg1U2LZBvUzcBKdRJ/zt/YgwILq/GXk2lAP8SMjDpQu+sGK73AFHXHRwBiKrSnXvmoq8EoDwPna21l" +
                "/8arTnh5ljYGpJUt6Hp1eSEdE7gaIoiMw7StPKugbYOX2DxoG799JhYwfLNzazxzA2IvHuTaw2O+6VFx" +
                "cVaFCznmUY6xADySxDioBhnS2jUewzkaTLzg3qHZHsL9v8bYgiNCg63yWgmrgVwhD4B9IEYPzg+hxfUL" +
                "ssq6GT9D7g/5Ely7B5aw1i2KZyQFYWiQR+ycGrGmckwoldFsI7q19MqPpydilg8FyAtJNraJzEht8I3G" +
                "dJVOvbHTsQVRo5cDUl2udS0d9ZXYudUBfZcJerelkypZcmmZ2mU913TSUhHWT2vpXgfAS1BKKAN1kwRN" +
                "kEmSIG0OfIYU1XUCQfcfwIgiDXEWABx+dYnuHkAJhU6AfrRDp+watKxT4bJ4WiRuleRSGgV1b91gaurZ" +
                "CzCpjGec+zD0uTOWVpT/SohIQa+Zj/Lzd/p9Bd8KPKee7Rz0s4apNsVStlywHPqhMmM/krYBU4SEW2WQ" +
                "kykhgEBCRO9SZgr6ZZS9uD0kB6uD8LPZFA94Jt4LglcWevb26fqbd/BkY5yK339HoYKDX0/a7urpIlHo" +
                "/iD6upBEWbAAGjWLgJ63P3y6oqfnqQsidKyH/m2EXB51k+zMGz+6NZGQ9DmjaXmvOVN2oPyqwhUxcTN1" +
                "4R5xtj/6LGBz6x1guR7SCXKmV+mexrdX9yI5rxsUBxuzxcdIFcqbo/wPqMc3C4MdJFb0PtUcN0acCfD5" +
                "sB6Pxwi129kvtLw9tsQ/nI6eN8ou/P08xFXetOQU1xxKY+vj1Qnq15QK6bJ78WZ23dHNvSepz4yu4r0Q" +
                "srXkVm01uhLkG+w05AjPpmut5ZTfvVHGzsur+WZbkR26MpfRu11YzHe6hk8fmaflT1pXzgydPZifDDdg" +
                "Ser4kOQGwuFwZzdpbEP1NhqKF3z1JEFfz69DUfVy1UjiRzfQTmXOhGnu0LcyNFre0XzRJLXFcIgKi513" +
                "YS2TX/hJJDQUeUzCLoyvVqQatUPz4y63GK4gftPgllxLwLMr6RaYTlm8nxPy27yA4Ht9wybQek0VLliL" +
                "EahjZfFSxlJ0ZPoV4Pk9JSXUVH3AtL3xrkvlncfpfDouCyF8ZYaanxwK193U4frNJXyO8lyXGqNWrRFn" +
                "LqFcfvPNvbz7cT8JRe6PfHoxYC4FLVBL24AOcKIcI08seY6pOUEdWKgqDqg66uH1TXqb45azMR4B/1Hi" +
                "GQaS05N/AahbPekSDQAA";
                
    }
}
