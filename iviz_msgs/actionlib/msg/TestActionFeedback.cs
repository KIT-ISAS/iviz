/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestActionFeedback : IDeserializable<TestActionFeedback>, IActionFeedback<TestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public TestActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new TestFeedback();
        }
        
        /// Explicit constructor.
        public TestActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal TestActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new TestFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestActionFeedback(ref b);
        
        TestActionFeedback IDeserializable<TestActionFeedback>.RosDeserialize(ref Buffer b) => new TestActionFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6d3d0bf7fb3dda24779c010a9f3eb7cb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgxofYndhpkzZNPaODKimOOk7isdVePSCxItGCoAqAkvXv+xb8EFVL" +
                "Ex2SaGTrC3j78PbtYj+QVOREEV8SmQVdWaPTx9Ln/tVNJc1DkKH2wseXZEE+vCdSqcz+Ecv2TTL6yo/k" +
                "48PNNUKqhsaHhtyZABerpFOipCCVDFIsK3DXeUHu0tCaDPMsV6RE/DVsV+SvsHFRaC/wzMmSk8ZsRe2x" +
                "KFQiq8qytjqTgUTQJe3tx05thRQr6YLOaiMd1ldOacvLl06WxOh4evq3JpuRmE+vscZ6yuqgQWgLhMyR" +
                "9Nrm+FEktbbhzWvekJwtNtUlPlKODPTBRShkYLL0tHLkmaf014jxQ3O4K2BDHEIU5cV5/O4RH/2FQBBQ" +
                "oFWVFeIczO+2oagsAEmspdMyNcTAGRQA6gve9OJigMy0r4WVturgG8RdjFNgbY/LZ7oskDPDp/d1DgGx" +
                "cOWqtVZYmm4jSGY02SBgOyfdNuFdTcjk7D1rjEXYFTOCV+l9lWkkQImNDkXig2P0mI1HrZJv5MajpZHw" +
                "W2Q2xwvH5wS/6+ql+XA3+zSdf7oR3WMkfsR/tiXFbaKQXmwpsCFTYn2yJvGtQE1s5NytUQcN5niymP81" +
                "EwPMn/YxOSO1c1AWJkyJNToJ+O5+Nvt4t5hNe+DX+8COMoK1YUukHPbgb+B+H4RcBjhZBz694wTRU6wD" +
                "myc7os8fZ/iDSaIKjeFQlStDjKCD71BA9HxBrkT1GW4FgS5ayg9/Tiaz2XRA+c0+5Q2QZVZotAgFH2as" +
                "wrLmPnBIiGNhxr9/vt/pwmF+PhAmreLRVR1tueN+MJKq6YvSsCt8hTJYSm1qR8fo3c/+mE0G/Ebil+f0" +
                "HP1NGfM7SIcLqqrD/+3y8sscU8okemrE7IPV6JNBgil3CHRqbdfSaHXsAK3z+koZibffwXm99WwVYhHu" +
                "zNcnr1d4Mr693VXySPx6KsGUcFXRQYanqIucPM/WPmm71K7kS42vjz4NsS8zE1J7hxja5N1XOMRpMrMp" +
                "9sqvCcDXxhFP3H5+WAyhRuK3CDi2nRjt7QEkoZA1BqFGBNlLwChXzRTgYXCjom7pCbXnGbtitVnSjcbx" +
                "UTmItd86k7OxMdUmziO8EKWAN9XusgKZ9qLiGhODyYq3KErrPGcZ20WBnkLyHa+y+TROSe2924nkA6eb" +
                "zxPvZEi6KTRmi3gfD1pKdAcpnoXmcXSJ09UBnbCfLPsHpyTPAmHEoXKFXBmD3Yzpm+RtCKF76M56sCQ5" +
                "bimR0XBUaPmju7TjBVox6KHLDbPQjazsRuzAfFWbgHHSe5lzepEav6JML3XWFUNk4Nk9jM6zXrMApMo6" +
                "FgX6nMaqqy55PIR869S9Gk7iSTNT9vN48h81MDSS1gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
