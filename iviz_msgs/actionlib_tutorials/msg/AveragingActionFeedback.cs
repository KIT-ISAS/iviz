/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingActionFeedback : IDeserializable<AveragingActionFeedback>, IActionFeedback<AveragingFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public AveragingFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public AveragingActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = AveragingFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public AveragingActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal AveragingActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = AveragingFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AveragingActionFeedback(ref b);
        
        AveragingActionFeedback IDeserializable<AveragingActionFeedback>.RosDeserialize(ref Buffer b) => new AveragingActionFeedback(ref b);
    
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgxofYndhpkzZNPaODKimOOk7isdVePSCxItGCIAuAkvXv+xYUKaqW" +
                "Jjok0cjWF/D24e3bxX4gqciJIr4kMgu6skanj6XP/aubSpqHIEPjhY8vyXhFTuba5u+JVCqzf8Ry+yYZ" +
                "feVH8vHh5hpxVcvlQ8vwTICQVdIpUVKQSgYplhUOoPOC3KWhFRkmW9akRPw1bGryV9i4KLQXeOZkcQhj" +
                "NqLxWBQqkVVl2VidyUAi6JL29mOntkKKWrqgs8ZIh/WVU9ry8qWTJTE6np7+bchmJObTa6yxnrImaBDa" +
                "ACFzJD2Ew48iabQNb17zhuRssa4u8ZFypKEPLkIhA5Olp9qRZ57SXyPGD+3hroANcQhRlBfn8btHfPQX" +
                "AkFAgeoqK8Q5mN9tQlFZAJJYSadlaoiBMygA1Be86cXFAJlpXwsrbdXBt4i7GKfA2h6Xz3RZIGeGT++b" +
                "HAJiYe2qlVZYmm4iSGY02SDgPSfdJuFdbcjk7D1rjEXYFTOCV+l9lWkkQIm1DkXig2P0mI1HrZJv5Maj" +
                "9ZHwW2Q2xwvH5wS/64qm/XA3+zSdf7oR3WMkfsR/tiXFbaKQXmwosCFTYn2yNvFbgdrYyLlDDW4xx5PF" +
                "/K+ZGGD+tI/JGWmcg7IwYUqs0UnAd/ez2ce7xWzaA7/eB3aUEawNWyLlsAd/A/f7IOQywMk68OkdJ4ie" +
                "Yh3YPNkRff44wx9MElVoDYeqrA0xgg6+QwHR8wW5EtVnuBUEuthSfvhzMpnNpgPKb/Ypr4Ess0KjRSj4" +
                "MGMVlg33gUNCHAsz/v3z/U4XDvPzgTBpFY+ummjLHfeDkVRDX5SGXeErlMFSatM4OkbvfvbHbDLgNxK/" +
                "PKfn6G/KmN9BOlxQVRP+b5eXX+aYUibRUyNmH6xBnwwSTLlDoFNru5JGq2MH2Dqvr5SRePsdnNdbz1Yh" +
                "FuHOfH3yeoUn49vbXSWPxK+nEkwJVxUdZHiKusjJ82ztk7ZL7Uq+1Pj66NMQ+zIzIbV3iKFN3n2FQ5wm" +
                "M5tir/zaAHxtHPHE7eeHxRBqJH6LgGPbibG9PYAkFLLGINSKIHsJGOWqnQI8DG5U1C09ofY8Y1esNku6" +
                "1jg+Kgex9ltncjY2plrHeYQXohTwptpdViCzvai4xsRgvOItitIm59mqu80CPYXkO15l82mckrb3bieS" +
                "D5xuPk+8kyHputCYLeJ9PGgp0R2keBaax9ElTlcHdMJ+suwfnJI8C4QRh8oauTIGuxnTt8lbE0L30J31" +
                "YEly3FIio+GosOWP7lK34wVaMeihyw2z0I2s7EbswHzVmIBx0nuZc3qRGl9Tppc664ohMvDsHkbnWa9d" +
                "AFJlE4sCfU5j1VWXPB5CvnnqQoPkaMj16tlgjuj/AfyH9cHcCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
