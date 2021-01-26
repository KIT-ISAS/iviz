/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Classification3D")]
    public sealed class Classification3D : IDeserializable<Classification3D>, IMessage
    {
        // Defines a 3D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesis[] Results { get; set; }
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all detectors, so it may be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Classification3D()
        {
            Header = new StdMsgs.Header();
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Classification3D(StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceCloud = SourceCloud;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Classification3D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
        
        Classification3D IDeserializable<Classification3D>.RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            SourceCloud.RosSerialize(ref b);
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
            if (SourceCloud is null) throw new System.NullReferenceException(nameof(SourceCloud));
            SourceCloud.RosValidate();
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
                size += SourceCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Classification3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fcf55de4cff8870324fd6e7873a6f904";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VXTW/cNhC9C8h/GMSH2IF3W9tpagQwijRGagOpEyBuL0FgUOLsig1FqiRlV/31fUNK" +
                "8jr1oYfGWHslivM4H2/eyHt0zhvjOJKik3NqrIrRbEyjkvGOAsfBpvWTag8fum5NnJZIe5g4n6jxLinj" +
                "SLmReh9NtjNu40OXMdZ0mQh2mqPZOtaEJwJGy1kc4iHdtaZpKZqut8AJ/tZolu9a1cYCFKdtzS3jHIp+" +
                "CA2T6dSW4dqT6oKV5kBt/pKFPXoj2A/tn1Tv6z+4SRdj71MLb+Knz1M0sRhdtyw50CopSq1KtGXHQSU4" +
                "LQY876Z9s+Y17rYSKw5B3MpSg4see/2QyG9KjLArjh6s6QrZCvznYELJAilrkZYEn7ykIHoyiTo1Us3E" +
                "XZ9GRBfZRR9uuriN333wxqU31g/6eErCTSN31dn//FP9+vGXVxSTLueW/CKej0k5rYKmjpPKaZIwWrNt" +
                "Oaws37KFkeokCSWJY89xPTMHn5JQixIPUbLqQZ+uG5zwjSmZjh/Yw1KYRb0KyTSDVQH7fdDGyfZNUB0L" +
                "Oj4ReWUHVlyevxJKRm6GBL7gJOOawCoat8VDqgbk8ORYDKq96zu/wi1vwZ7l8FJ6OMt/9Si4+KniK5zx" +
                "vAS3BjaSwzhFgwp57Qa38QD0FRe496DyPjz/MKYWDBES3KpgVG1ZgBtkAKjPxOjZwQ6yy9BOOT/DF8T7" +
                "M/4LrFtwJaZVi5pZiT4OWyTQxLm9NNVjBmmsYZfImjqoMFZiVY6s9t5KjrFJGlgqgm80lm9M7oo7k9oq" +
                "piDouRo3RlffiI23JqLbCiG/bmSE+tqRz6vULsulkpNAiVo9LlD3zQ8igkZCE7/JmZkgs1Kt6dqDwImU" +
                "1hkELb8DQ6pG489dj8Mvz9HSA5igQH2IRjt0yq1ARZ0LljFR644PkfmYpDNQ79YPVlPPQYBJFTzr/Zeh" +
                "L72wtJ78qVWEEH1kfpCe3/P1JXxb4z73aOcDi9YoY0VUSsmMvg/9XipH8tJmbgOKCPlulUVOpoQUuRKR" +
                "y5lZ08+j7IUwSw4Od8IvZlM8IJh4LwhBuS3Tp+9XR5/hycZ6lV6+oNjAwW+lY49L6CxLaPsIgabWWy1T" +
                "sPFooyaXFEFfrTTawcVS717Ml2kFqQbIMgAfp8VMAScrFrYiOMBL4yFxataSfaBk5KXHIiZC1h34U0Pt" +
                "AmaC9fVhJpJVo4wYDNQmmHpp48kVVCLO1XqK4YqgnpIKQY3raq51PiuPjnLiNHN82Cpn/gbisYbQyNha" +
                "WfOFD4QRRxrW+wPCwCxgjXn24R4m7tjCaZhn6zgjg116aLKr4iZIH0Bg7lM7VScueUInBPZyYlYvv1lt" +
                "LAZMKt6LNCG4YlScVw1GamnrQ7yF6CJpX40J6el9aYQTPSc7HqwfvjnIAcfncCAMTRrCwvmddOFlZjNJ" +
                "piQPlVoScggU8ZOyHB1lT+6MRoRZiJgsuy3uHgGdZ1IBmO+ysfh0PhW6wDTQc8c2zqGaMBNiarCJLzk3" +
                "Qpp1lQv1VqiAN55CiaqqvbfoVXh3UxvMZW2Uoz26jDtqvzz4aXaq+H2DIvWw3aN3S1BqCgle1GPiuFgE" +
                "fzfv/9oCjx7uP4WD+eT5B8KOWsydl59BVcEzyer+DP28ZO5gigohQbrwvraAXAdokcmZR10VfjEMjING" +
                "GT2x4VvNrX+pTy7E4+JTFFaq3c/6493MFTR2GKciw3z3ZXBGKapT+HRKl1fXpxL+GR1NK79NS2d0fL/n" +
                "6GVeOdnZI0tn9OJ+j9QRKz/s7JGlM3o5rbx99/61LJ3Rj7srUPYzOq3mgSPTbi7JlSqtnPk4k8VvNhET" +
                "Nm94X643wXfyNhLkpXpKRWnR6aBMCnnTFKPz+ZrdICJTVCFiQGI83/J8TuMHlyZHLkDCTv57YctdVs+p" +
                "kYpn1T9tAEWdIg0AAA==";
                
    }
}
