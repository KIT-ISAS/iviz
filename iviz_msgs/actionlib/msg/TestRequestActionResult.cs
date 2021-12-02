/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestRequestActionResult : IDeserializable<TestRequestActionResult>, IActionResult<TestRequestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestRequestResult Result { get; set; }
    
        /// Constructor for empty message.
        public TestRequestActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestRequestResult();
        }
        
        /// Explicit constructor.
        public TestRequestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal TestRequestActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestRequestResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestRequestActionResult(ref b);
        
        TestRequestActionResult IDeserializable<TestRequestActionResult>.RosDeserialize(ref Buffer b) => new TestRequestActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength => 5 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "0476d1fdf437a3a6e7d6d0e9f5561298";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W0XIaNxR936/QDA+xO7XTJm2SeoYHCtQh4yQem/aV0a4uu2q1EpW0YP6+52phgRom" +
                "PCRhsBds6dyjc8+9uu9JKvKiSo9MFlE7a3Q+q0MZXt46aR6jjE0QIT2yKYX4QP826REaE4VPj6z/lV/Z" +
                "x8fbG0RVLZP3Lb+eAB2rpFeipiiVjFLMHejrsiJ/ZWhJhqnWC1Ii/TeuFxSusXFa6SDwLsmSl8asRROw" +
                "KDpRuLpurC5kJBF1TQf7sVNbIcVC+qiLxkiP9c4rbXn53MuaGB3vwLLYgsRkdIM1NlDRRA1CayAUnmTQ" +
                "tsQ/RdZoG1+/4g1Zb7pyV/hKJZLQBRexkpHJ0tMC+jJPGW4Q44f2cNfAhjiEKCqIi/S3Gb6GS4EgoEAL" +
                "V1TiAszv17FyFoAkltJrmRti4AIKAPUFb3pxuYfMtG+EldZt4VvEXYxzYG2Hy2e6qpAzw6cPTQkBsXDh" +
                "3VIrLM3XCaQwmmwUcJ6Xfp3xrjZk1vuDNcYi7EoZwVOG4AqNBCix0rHKQvSMnrIx0yr7Rm48WR0Zf0Rm" +
                "Szw4Pif43bZk2i/340+jyadbsX31xU/4zbaktE1UMog1RTZkTqxP0SZ+I1AbGzn3S9RBizkYTid/jcUe" +
                "5s+HmJyRxnsoCxPmxBqdBXz/MB5/vJ+ORx3wq0NgTwXB2rAlUg578F9SUxByHuFkHfn0nhNET6kObJnt" +
                "iD5/9fADkyQVWsOhKheGGEHHsEUB0Ysp+RrVZ7gVRLrcUH78czgcj0d7lF8fUl4BWRaVRotQ8GHBKswb" +
                "7gPHhDgVZvD754edLhzmlyNhcpeOrppkyx33o5FUQ1+Uhl0RHMpgLrVpPJ2i9zD+MB7u8euLX5/T8/Q3" +
                "FczvKB0uKNfE/9vlxy9zzKmQ6KkJswvWoE9GCabcIdCptV1Ko9WpA2yc11VKX7z5Ds7rrGddTEW4M1+X" +
                "vE7h4eDublfJffH2XII54aqiowzPURc5eZ6tQ9J2rn3NlxpfH10aUl9mJqQODrFvk3df4RDnycymOCi/" +
                "NgBfGyc8cff5cboP1Re/JcCB3YqxuT2AJBSyxiDUiiA7CRjlup0CAgxuVNItP6P2AmM7VpslXWkcH5WD" +
                "WIetM+sNjHGrNI/wQpQCPrjdZQUym4uKa0zsDVe8RVHelCXLuFkU6Slm3/Eqm4zSlLS5d7cihcjp5vOk" +
                "OxmSriqN2SLdx3stJbmDFM9CkzS6pOnqiE7YT5b9g1NSYIEw4lC9QK6MwW7GDG3yVoTQHfTWerAkeW4p" +
                "idH+qLDhj+6yGS/QikFvfZiFOZHKZfEPuxE72vkV42QIsuT0IjVhQYWe62JbDIlBYPcwOs967QKQqptU" +
                "FOhzGquut8njIeRbp+7ls2E8awdLZGu2Gctz59j9s6C5oWF+S1b9D7uL/Dv1CwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
