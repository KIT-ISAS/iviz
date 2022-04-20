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
                "H4sIAAAAAAAAE7VWTW/cNhC961cM4EPsZFdJ06IIAhT9ctP4kLZogl6CwOCKsxITilRJatfrX983pCSv" +
                "HSfIoVk42RXFeZyZN/M4J3TOW+M4kqKn59RYFaPZmkYl4x0FjqNNdXVSndCbzsRpgbSHgfOJGu+SMo6U" +
                "O9Dgo8lWxm196DNCTReJYKc5mtaxJrwBFi0HcYgr2nem6SiafrCACX5nNJcd8rRRG2OBjCNbs2McRtGP" +
                "oWEyvWq5rqqXrDQH6vJXBfyfyZqYyG/vQ6lLKHmHY/gkgcynqqO9h8Vb3nHIAUazsZNrK3o/yhmSu9Sp" +
                "RCowoNw1B78iH0ht/I7has8ZY4wc1jonW8MAmey81XX15+Y9N+nlYfCpQ5bi23dTlmOVs87Ci1ZJlVNa" +
                "dhxUyhgced5Lp6bmGk+tMIAY4Kyy1ODHgL1+lGxkP2BWEndW0x+IPPC/owmFGlLWiqPUqMiIMHoyiXp1" +
                "oA0T90M61FVkF3247GMbH18IzsTGpenb6of/+VO9ev37c4pJl/MK0QjjdVJOq6Cp56RycsT7zrQdkmxB" +
                "l4WR6iX0krrDAOLnKsZfSaNFvSFc5NKjlPt+dFL5TMn0fMsellLlNKiQTDNaFbDfB22cbN8G1bOg4y8i" +
                "nexQnBfnz6U9IjdjQtniJOOawCoa1+IlVaNx6dunYlCdvNn7NR65RRkvhxfC4SxfDaBZ/FTxOc54WIKr" +
                "gY3kME7RKIC8donHeIZeEhd48OirU3j+1yF1qAvhfqeCUVLFAG6QAaA+EKMHZ0fILkM75fwMXxBvzvgS" +
                "WLfgSkzrDpxZiT6OLRJo4tx1mjaHDNJYwy6hNTdBhUMlVuXI6uSF5BibREyEEXyjCX1jci/sTeqqmIKg" +
                "ZzYuja6+UjXuTESPlYK827yiPY58XqVuWS5MTmIpynm/WM4NjzJEEUmRQMEkLxNg1h3Il0f5Qm+0zhBo" +
                "8yMQkZ0xTZ2Oky/O0cYjykCh7qET3dgrt0YdarVIGYjueZUVUdoCZHd+tJoGDoJLKsNZ7z+MQ+mDpe3k" +
                "vw3EoqbXzLdS80/+fQHPajzn/uw9JFLD1Nh6Zkt4KlEfC6+X/nJb1IZU3U5ZpGPKBeyRC9G0nJSafjnI" +
                "XlwMEv7qJvJiNYWCwhLPBSAoB9F6+2T9zbu62lqv0vffUWzg29eSr7uKOesQ+jyKgC6FoRy4hxDN7W6m" +
                "3adPVvTkLNd8glYNkLit1FMAW5KXad+dm5CmzwlNyzfKMmUFqq4ayP9Ui7nfFji677NgzV12BOUHaCOq" +
                "Mb/Kly++g/oUkA+mBSXYVww+BmrAaQnw80iPrpaK9VBRUfNMNK6DNLP+2ZgeHW4DaL93X2Z4fdsQ/7wc" +
                "P1jllor9LMJF2bOkExcYSHH69uqE9GtOg/TUp+DmkrqjjTd+5L6ypkmfQpCdG+7Uzvis0aObhpZqvrE6" +
                "zom9MSnAZXk131orcmO/KfQFv4+z9d5o+PORdV6+17jxduzdMg1ZblEaubljVhVIhNciJTKAgbStga7F" +
                "0DzOwJfz61g3A+4RSfjBj7RXpVDiNEmYaxn+HO9pvkayomLKA7EwCz6uZYSLP4lMxroMPdiECdSJGoMy" +
                "NLqWHuYeCjcNYdmxjDs7Apmfjlg8n1Px27yAsAdzxTbSek0NLk6HiaZn5fBShkt0YP4V4fb9RAqT6gOm" +
                "5W3wfSZ1HonL4VFShTvdjpofHyvU3ax1hfdnYOVyYzA3aYMQC3Px6EJe3v24TDaJh1sOvRgxXqIWwKFr" +
                "UQTwYHNIXErjGQbfDHRk8PZvKMA7aFQawTnoCOYqbyqRiwOn+ZSHucTOquo/uCzaLdAMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
