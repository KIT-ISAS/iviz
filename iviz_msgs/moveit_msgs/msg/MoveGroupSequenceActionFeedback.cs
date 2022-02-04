/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceActionFeedback : IDeserializable<MoveGroupSequenceActionFeedback>, IActionFeedback<MoveGroupSequenceFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public MoveGroupSequenceFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupSequenceActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new MoveGroupSequenceFeedback();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupSequenceFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public MoveGroupSequenceActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new MoveGroupSequenceFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveGroupSequenceActionFeedback(ref b);
        
        public MoveGroupSequenceActionFeedback RosDeserialize(ref ReadBuffer b) => new MoveGroupSequenceActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgxofYnVptkzZJPaODKimOMnbisdRePSC5ItGCoIoPyfr3fQtSFOVY" +
                "jQ5JNLL1Bbx9ePt2se9J5mRFGV8SmXlVG63Sh8oV7qfrWuq5lz444eJLcluv6drWYTWnfwOZjN4R5anM" +
                "/hHL9k0y/MqP5HZ+fYX4ecPpfcP0TICYyaXNRUVe5tJLsaxxEFWUZC81rUkz6WpFuYi/+u2K3AAbF6Vy" +
                "As+CDFmp9VYEh0W+FlldVcGoTHoSXlV0sB87lRFSrKT1KgtaWqyvba4ML19aWRGj4+labcRscoU1xlEW" +
                "vAKhLRAyS9IpU+BHkQRl/KuXvCE5W2zqS3ykAunoggtfSs9k6XFlyTFP6a4Q44fmcANgQxxClNyJ8/jd" +
                "Az66C4EgoECrOivFOZjfbX1ZGwCSWEurZKqJgTMoANQXvOnFRQ/ZRGgjTb2DbxD3MU6BNR0un+myRM40" +
                "n96FAgJi4crWa5VjabqNIJlWZLyAB62024R3NSGTs3esMRZhV8wIXqVzdaaQgFxslC8T5y2jx2w8qDz5" +
                "Rm48WicJv0VmC7xwfE7w213xNB/uph8ns4/XYvcYip/xn21JcZsopRNb8mzIlFifrEl8K1ATGzm3a9RB" +
                "gzkaL2Z/TUUP85dDTM5IsBbKwoQpsUYnAd/dT6e3d4vppAN+eQhsKSNYG7ZEymEP/gbud17IpYeTlefT" +
                "W04QPcY6MEUi/udxhj+YJKrQGA5VudLECMq7HQqIni/IVqg+za3A00VLef7neDydTnqUXx1S3gBZZqUi" +
                "pu1CxiosA/eB54Q4Fmb0x6f7vS4c5tdnwqR1PHoeoi333J+NlAf6ojTsClejDJZS6WDpGL376YfpuMdv" +
                "KH77nJ6lvynzRxwQC6oO/qldfvwyx5QyiZ4aMbtgAX3SSzDlDoFOrcxaapUfO0DrvK5ShuL1d3BeZz1T" +
                "+1iEe/N1yesUHo9ubvaVPBRvTiWYEq4qepbhKeoiJ59n65C0WSpb8aXG14fvd4HIhPKDQ/Rt8vYrHOI0" +
                "mdkUB+XXBOBr44gnbj7NF32oofg9Ao7MToz29gCSyJE1BqFGBNlJwCiDZgpwMLjOo27pCbXnGLtmtVnS" +
                "jcLxUTnSPGmdydlI63oT5xFeiFKwXLfdZQUy7UXFNSZ6YxZvySkNRcEytos8PfrkO15ls0nSOKAZQVqR" +
                "nOd083ninQxJN6XCbBHv415Lie6gnGehWRxdQnvHPNUJ+8mwf3BKciwQRhyqVsiV1tjNmK5J3oYQuoPe" +
                "WQ+WJMstJTLqjwotf3SXdrxAKwa97WEWdiMruxE7MF8F7TFOOicLalLjVpSppcp2xRAZuEGLzrNeswCk" +
                "qhCLAn1OYdVglzweQr5R6ipYUfkmb0cHc0RvmfAUQsl/If79lvELAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
