/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsActionFeedback : IDeserializable<TwoIntsActionFeedback>, IActionFeedback<TwoIntsFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TwoIntsFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public TwoIntsActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public TwoIntsActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal TwoIntsActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwoIntsActionFeedback(ref b);
        
        TwoIntsActionFeedback IDeserializable<TwoIntsActionFeedback>.RosDeserialize(ref Buffer b) => new TwoIntsActionFeedback(ref b);
    
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Wy3IbNxC871egSgdLqUhO7DwcVfHAkLRMl2yrJCZXFRYY7iLZxTIAlhT/Pj3YB5cW" +
                "WebBNosSX0BPo6dnMO9IanIijy+JVMFUtjDpY+kz//KmksVDkKH2wseXZLGp5jb4t0Q6lepfsWzfJKOv" +
                "/Eg+PNxcI6pumLxr+J0J0LFaOi1KClLLIMWyAn2T5eQuC1pTwVTLFWkRfw3bFfkrbFzkxgs8M7LkZFFs" +
                "Re2xKFRCVWVZW6NkIBFMSXv7sdNYIcVKumBUXUiH9ZXTxvLypZMlMTqenv6rySoS8+k11lhPqg4GhLZA" +
                "UI6kNzbDjyKpjQ2vX/GG5AyCXuIjZUhCH1yEXAYmS08rR555Sn+NGD80h7sCNsQhRNFenMfvHvHRXwgE" +
                "AQVaVSoX52B+tw15ZQFIYi2dkWlBDKygAFBf8KYXFwNkpn0trLRVB98g7mKcAmt7XD7TZY6cFXx6X2cQ" +
                "EAtXrlobjaXpNoKowpANAs5z0m0T3tWETM7essZYhF0xI3iV3lfKIAFabEzIEx8co8dsPBqdfCM3Hq2O" +
                "hN8isxleOD4n+E1XMs2Hu9nH6fzjjegeI/ET/rMtKW4TufRiS4ENmRLro5rEtwI1sZFzt0YdNJjjyWL+" +
                "90wMMH/ex+SM1M5BWZgwJdboJOC7+9nsw91iNu2BX+0DO1IEa8OWSDnswd/A/T4IuQxwsgl8escJoqdY" +
                "BzZLdkSfP87wB5NEFRrDoSpXBTGCCb5DAdHzBbkS1VdwKwh00VJ++Gsymc2mA8qv9ylvgCxVbtAiNHyo" +
                "WIVlzX3gkBDHwoz//HS/04XD/HIgTFrFo+s62nLH/WAkXdMXpWFX+AplsJSmqB0do3c/ez+bDPiNxK/P" +
                "6Tn6hxTzO0iHC6qqw+d2+fHLHFNSEj01YvbBavTJIMGUOwQ6tbFrWRh97ACt8/pKGYnfvoPzeuvZKsQi" +
                "3JmvT16v8GR8e7ur5JH4/VSCKeGqooMMT1EXOXmerX3SdmlcyZcaXx99GmJfZiak9w4xtMmbr3CI02Rm" +
                "U+yVXxOAr40jnrj99LAYQo3EHxFwbDsx2tsDSEIjawxCjQiyl4BRrpopwMPghY66pSfUnmfsitVmSTcG" +
                "x0flINZ+60zOxkVRbeI8wgtRCnhT7S4rkGkvKq4xMRiueIumtM4ylrFdFOgpJN/xKptP45TU3rudSD5w" +
                "uvk88U6GpJvcYLaI9/GgpUR3kOZZaB5HlzhdHdAJ+8myf3BK8iwQRhwqV8hVUWA3Y/omeRtC6B66sx4s" +
                "SY5bSmQ0HBWShj+6SzteoBWDHrrcMAvdyMpuxA7MV3URME56LzNOL1LjV6TM0qiuGCIDz+5hdGxqF4BU" +
                "WceiQJ8zWHXVJY+HkG+dupefDeNJ8j+WbStTzQsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
