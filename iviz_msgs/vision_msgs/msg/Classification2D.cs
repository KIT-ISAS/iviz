/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public Classification2D()
        {
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceImg = new SensorMsgs.Image();
        }
        
        /// Explicit constructor.
        public Classification2D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.Image SourceImg)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceImg = SourceImg;
        }
        
        /// Constructor with buffer.
        public Classification2D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Classification2D(ref b);
        
        public Classification2D RosDeserialize(ref ReadBuffer b) => new Classification2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
            SourceImg.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Results is null) BuiltIns.ThrowNullReference(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) BuiltIns.ThrowNullReference($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (SourceImg is null) BuiltIns.ThrowNullReference(nameof(SourceImg));
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
                "H4sIAAAAAAAAE7VWTW/cNhC961cM4EPsZFdJ06IIDBT9ctP40A8gQS9BYHDFWYkJRaoktevNr+8bUpLX" +
                "jh3k0Cyc7IriPM7Mm3mcE7rgrXEcSdHzC2qsitFsTaOS8Y4Cx9GmujqpTuhNZ+K0QNrDwPlEjXdJGUfK" +
                "HWjw0WQr47Y+9BmhpstEsNMcTetYE94Ai5aDOMQV7TvTdBRNP1jABL8zmssOedqojbFAxpGt2TEOo+jH" +
                "0DCZXrVcV9UrVpoDdfmrAv7PZE1M5Lf3odQllLzDMXySQOZT1dHew+It7zjkAKPZ2Mm1Fb0f5QzJXepU" +
                "IhUYUO4jB78iH0ht/I7has8ZY4wc1jonW8MAmey81XX11+Y9N+nVYfCpQ5bi23dTlmOVs87Ci1ZJlVNa" +
                "dhxUyhgced5Lp6bmGk+tMIAY4Kyy1ODHgL1+lGxkP2BWEndW05+IPPC/owmFGlLWiqPUqMiIMHoyiXp1" +
                "oA0T90M61FVkF3246mMbn14KzsTGlenb6of/+VP98fr3c4pJl/MK0QjjdVJOq6Cp56RycsT7zrQdkmxB" +
                "l4WR6iX0krrDAOLnKsZfSaNFvSFc5NKjlPt+dFL5TMn0fMsellLlNKiQTDNaFbDfB22cbN8G1bOg4y8i" +
                "nexQnJcX59IekZsxoWxxknFNYBWNa/GSqtG49O1zMahO3uz9Go/cooyXwwvhcJavB9Asfqp4jjMel+Bq" +
                "YCM5jFM0CiCvXeExnqGXxAUePPrqFJ7/fUgd6kK436lglFQxgBtkAKiPxOjR2RGyy9BOOT/DF8SbM74E" +
                "1i24EtO6A2dWoo9jiwSaOHedps0hgzTWsEtozU1Q4VCJVTmyOnkpOcYmERNhBN9oQt+Y3At7k7oqpiDo" +
                "mY0ro6uvVI07E9FjpSDvNq9ojyOfV6lblguTk1iKct4vlnPDowxRRFIkUDDJywSYdQfy5VG+0ButMwTa" +
                "/AhEZGdMU6fj5MsLtPGIMlCoe+hEN/bKrVGHWi1SBqJ7XmVFlLYA2Z0fraaBg+CSynDW+w/jUPpgaTv5" +
                "bwOxqOk1863U/JN/X8KzGs+5P3sPidQwNbae2RKeStTHwuulv9wWtSFVt1MW6ZhyAXvkQjQtJ6WmXw6y" +
                "FxeDhL+6ibxYTaGgsMRzAQjKQbTePlt/866uttar9P13FBv49rXk665izjqEPo8ioEthKAfuIURzu5tp" +
                "9+mzFT07yzWfoFUDJG4r9RTAluRl2nfnJqTpc0LT8o2yTFmBqqsG8j/VYu63BY7u+yxYc5cdQfkB2ohq" +
                "zK/y5YvvoB4C8sG0oAT7isGnQA04LQF+HunJ9VKxHioqap6JxnWQZtY/G9OTw20A7ffuyww/3jbEPy/H" +
                "D1a5pWI/i3BZ9izpxAUGUpy+vToh/ZrTID31ENxcUne08caP3FfWNOkhBNm54U7tjM8aPbppaKnmG6vj" +
                "nNgbkwJcllfzrbUiN/abQl/w+zhb742GP59Y5+V7jRtvx94t05DlFqWRmztmVYFEeC1SIgMYSNsa6FoM" +
                "zdMMfDW/jnUz4B6RhB/8SHtVCiVOk4T5KMOf4z3N10hWVEx5IBZmwce1jHDxJ5HJWJehB5swgTpRY1CG" +
                "RtfSw9xD4aYhLDuWcWdHIPPTEYvncyp+mxcQ9mCu2UZar6nBxekw0fSsHF7KcIkOzL8i3L6fSGFSfcC0" +
                "vA2+z6TOI3E5PEqqcKfbUfPTY4W6m7Wu8P4CrFxtDOYmbRBiYS4eXcjLux+XySbxcMuhlyPGS9QCOHQt" +
                "igAebA6JS2m8wOCbgY4MVJNGkA0egrnOb0vIcvJphn+ca+usqv4DnWz6OckMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
