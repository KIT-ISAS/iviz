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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Classification2D(ref b);
        
        Classification2D IDeserializable<Classification2D>.RosDeserialize(ref Buffer b) => new Classification2D(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/Classification2D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "90b6e63f97920f8c0f94c08bd35cb96d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVW247bNhB911cM4IdkE1tJ06IIFih626bZh16ALPoSBAtaGktMKFIlKXuVr+8ZUtLa" +
                "m90gD42RrC2KcziXM4ezogveacuBFL24oMqoEPROVypqZ8lzGEwsi1WxoqtWh2mBagcD6yJVzkalLSk7" +
                "Uu+CTlba7pzvEkJJl5FgV3PQjeWa8AZYtBzEPqzp0OqqpaC73gDGu72uOe+Qp63aagNkHNnoPeMwCm7w" +
                "FZPuVMNlUbxmVbOnNn0VwP+ZjA6R3O4+lDKHknZYhk8SyHyqOto7Lt7ynn0KMOitmVxb0/tBzpDcxVZF" +
                "Up4BZT+yd2tyntTW7RmudpwwhsB+U6dk1zBAJltn6rL4a/ueq/h67F1skaXw9t2U5SCRXLUsdalVVPmU" +
                "hi17FRMGB5730mNdcomnRiqAGOCsMlThR4+9bpBsJD9glhN3VtKfiNzzv4P2uTSkjBFHqVKBEWFwpCN1" +
                "aqQtE3d9HMsisA3OX3ehCc8uBWeqxrXumuKH//lT/PHm93MKsc7n5UIjjDdR2Vr5mjqOKiVHvG910yLJ" +
                "BuUyMFKdhJ5TN/Yo/Mxi/MtpNOAbwkUuHajcdYMV5jNF3fGJPSyF5dQrH3U1GOWx3/laW9m+86oDD1ey" +
                "LSCdbEHOy4tzaY/A1RBBW5ykbeVZBW0bvKRi0DZ++0IMitXVwW3wyA1ovByeCw5n+aZHmcVPFc5xxpMc" +
                "XAlsJIdxSg0CpLVrPIYz9JK4wL1DXz2G53+PsQUvpPZ75bUSFgO4QgaA+kiMHp0dIYvb52SVdTN8Rrw9" +
                "40tg7YIrMW1a1MxI9GFokEBsnLqupu2YQCqj2Ua05tYrPxZilY8sVq8kx9gkYiIVwTea0FU69cJBx7YI" +
                "0Qt6qsa1rouvxMa9DuixTMi7zSvaY8mlVWqX5VzJSSxFOe8Xy7nhQUOQSEgCBZO8TIBJdyBfDvSF3tR1" +
                "gkCbH4GI7Axx6nScfHmBNh5AAwXeQyfaoVN2Ax7WqVpZHy0ytk6KKG2BYrduMDX17AWXVIIzzn0Y+twH" +
                "S9vJny3EoqQ3zCep+Sf9voRnJZ5Tf3YOElnDVJtyrpbUKUd9LLzYjGztwA1h3V4ZpGPKBeyRC9G0lJSS" +
                "fhllLy4GCX99G3m2mkIBscRzAfDKQrTePt98864sdsap+P13FCr49rXk665izjqEPg8ioAsxlEXtIURz" +
                "u+tp9+Pna3p+ljgfoVU9JG4nfPKoluRl2nfnJkQi0mdF0/KtskxZgaqrCvI/cTH12wI3m598Fqy5y46g" +
                "XA9tBBvTq3T54turh4Cc1w1Kgn3Z4FOgCjXNAX4e6enNwlgHFRU1T4XGdRDnqn82pqfjKUDtDvbLDD+e" +
                "GuI/zkZ7G2UXxn4W4TLvWdKJCwxFsfXp6oT0a0qD9NRDcDOl7mjjrR+pr4yu4kMIsnPLrdprNCEYN9hp" +
                "aCnmG6vllNhbkwycl9fzrbUmO3TbXD7vDmG2Puga/nxinZbvNa6cGTq7TEOGG1AjNXdIqgKJcLiImzSA" +
                "oWg7DV0LvnqWgK/n16GsetwjkvDRDXRQmShhmiT0Rxn+LB9ovkaSomLKQ2Fh5l3YyAgXfhKZDGUeerAJ" +
                "E6gVNUbJ0Oi4oi0mJSjcNIQlxxLu7Ahkfjpi8XxOxW/zAsLu9Q2bQJsNVbg4LSaajpXFSxku0YHpV4Db" +
                "9xdSKqk+YFreedelos4jcT4ct4FQvDJDzc+OFepu1tpc95eoyvVWY26qNULMlZOLbb6Ql3c/LpNN5P7E" +
                "oVcDxktwATW0DUgAD7Zj5EyNlxh8E9CRgarigGKjDl7fpLc5ZDkZ8w7gnyRunRXFf51s+jnJDAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
