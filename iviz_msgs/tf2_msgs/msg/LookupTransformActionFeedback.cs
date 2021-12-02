/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LookupTransformActionFeedback : IDeserializable<LookupTransformActionFeedback>, IActionFeedback<LookupTransformFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public LookupTransformFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public LookupTransformActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public LookupTransformActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal LookupTransformActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = LookupTransformFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LookupTransformActionFeedback(ref b);
        
        LookupTransformActionFeedback IDeserializable<LookupTransformActionFeedback>.RosDeserialize(ref Buffer b) => new LookupTransformActionFeedback(ref b);
    
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
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgxofYndptnX6kntFBlRRHHSfx2GqvHpBYkWhIgAVAyfr3fQuKFFVL" +
                "Ex2SaGTrC3j78PbtYt+RVOREEV8SmQVtTanTp8rn/odbK8vHIEPjhY8vyZ21n5p64aTxS+uqt0Qqldkn" +
                "sdy+SUZf+JG8f7y9QXTVMnrX8jwToGWUdEpUFKSSQQrwEYXOC3KXJa2oZMpVTUrEX8OmJn+FjYtCe4Fn" +
                "ToacLMuNaDwWBSsyW1WN0ZkMJIKuaG8/dmojpKilCzprSumw3jqlDS9fOlkRo+Pp6d+GTEZiPr3BGuMp" +
                "a4IGoQ0QMkfSa5PjR5E02oTX17whOVus7SU+Uo5k9MFFKGRgsvRcO/LMU/obxPiuPdwVsCEOIYry4jx+" +
                "94SP/kIgCChQbbNCnIP5/SYU1gCQxEo6LdOSGDiDAkB9xZteXQyQmfaNMNLYDr5F3MU4Bdb0uHymywI5" +
                "K/n0vskhIBbWzq60wtJ0E0GyUpMJAg500m0S3tWGTM7essZYhF0xI3iV3ttMIwFKrHUoEh8co8dsPGmV" +
                "fCU3Hq2ShN8iszleOD4n+E1XOu2H+9mH6fzDregeI/Ej/rMtKW4ThfRiQ4ENmRLrk7WJ3wrUxkbO3Qp1" +
                "0GKOJ4v53zMxwPxpH5Mz0jgHZWHClFijk4DvH2az9/eL2bQHvt4HdpQRrA1bIuWwB38D9/sg5DLAyTrw" +
                "6R0niJ5jHZg82RF9+TjDH0wSVWgNh6qsS2IEHXyHAqLnC3IVqq/kVhDoYkv58a/JZDabDii/3qe8BrLM" +
                "Co0WoeDDjFVYNtwHDglxLMz4j48PO104zM8HwqQ2Hl010ZY77gcjqYY+Kw27wluUwVLqsnF0jN7D7M/Z" +
                "ZMBvJH55Sc/RP5Qxv4N0uKBsE/5vl+8/zzGlTKKnRsw+WIM+GSSYcodAp9ZmJUutjh1g67y+Ukbi12/g" +
                "vN56xoZYhDvz9cnrFZ6M7+52lTwSv51KMCVcVXSQ4SnqIicvs7VP2iy1q/hS4+ujT0Psy8yE1N4hhjZ5" +
                "8wUOcZrMbIq98msD8LVxxBN3Hx8XQ6iR+D0Cjk0nxvb2AJJQyBqDUCuC7CVglKt2CvAweKmibukJtecZ" +
                "27LaLOla4/ioHMTab53J2bgs7TrOI7wQpYA3dndZgcz2ouIaE4Mhi7coSps8Zxm3iwI9h+QbXmXzaZyS" +
                "tvduJ5IPnG4+T7yTIem60Jgt4n08aCnRHaR4FprH0SVOVwd0wn4y7B+ckjwLhBGHqhq5KkvsZkzfJm9N" +
                "CN1Dd9aDJclxS4mMhqPClr9W3XiBVgx66HLDLHQjK7sROzBfNWXAOOm9zDm9SI2vKdNLnXXFEBl4dg+j" +
                "86zXLgCpqolFgT6nseqqSx4PIV8pdWF53SbtyEyeJP8Btl0ZbdwLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
