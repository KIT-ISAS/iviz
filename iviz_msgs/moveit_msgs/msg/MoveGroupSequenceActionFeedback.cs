/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceActionFeedback")]
    public sealed class MoveGroupSequenceActionFeedback : IDeserializable<MoveGroupSequenceActionFeedback>, IActionFeedback<MoveGroupSequenceFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public MoveGroupSequenceFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new MoveGroupSequenceFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupSequenceFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MoveGroupSequenceActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new MoveGroupSequenceFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionFeedback(ref b);
        }
        
        MoveGroupSequenceActionFeedback IDeserializable<MoveGroupSequenceActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionFeedback(ref b);
        }
    
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
                "H4sIAAAAAAAACr1W224bNxB9X0D/QEAPsYvabZNeUgN6UCXFUWAnhqX21eAuR7tsd7lbkitZf98z3Iuk" +
                "WkL0kESQrRt55vDMmeG8J6nIiiy8RDLxujS5jp8Kl7ofbkuZL7z0tRMuvET35ZpubVlXC/q3JpPQOyIV" +
                "y+QfsWrfDKLRF34MovvF7Q0YqIbV+8B1EA0FuBklrRIFeamkl2JV4iw6zche5bSmnHkXFSkRfvXbitw1" +
                "Ni4z7QSeKRmyMs+3onZY5EuRlEVRG51IT8Lrgg72Y6c2QopKWq+TOpcW60urtOHlKysLYnQ8XSuPmE9v" +
                "sMY4SmqvQWgLhMSSdNqk+FFEtTb+zWveEA2Xm/IKHylFRvrgwmfSM1l6riw55indDWJ81xzuGthQhxBF" +
                "OXERvnvCR3cpEAQUqCqTTFyA+cPWZ6UBIIm1tFrGOTFwAgWA+oo3vbrcQ2baN8JIU3bwDeIuxjmwpsfl" +
                "M11lyFnOp3d1CgGxsLLlWissjbcBJMk1GS9gQyvtNuJdTcho+I41xiLsChnBq3SuTDQSoMRG+yxy3jJ6" +
                "yMaTVtFXM+TJYhlE/B7JTfHCFDjHb7sSaj48zD5O5x9vRfcYiR/xn51JYZvIpBNb8uzJmFiipMl9q1ET" +
                "HGm3a9RtgzmeLOd/zcQe5k+HmJyU2lqICx/GxDKdBfzwOJvdPyxn0x749SGwpYTgbjgTWYdD+BsUgPNC" +
                "rjzMrD2f3nKO6DmUgkmjHdGXjyH+4JOgQuM5FGaVEyNo7zoUEL1Yki1QgDl3A0+XLeXFn5PJbDbdo/zm" +
                "kPIGyDLJNLqEghUTVmFVcys4JsSpMOM/Pj3udOEwPx8JE5fh6KoOztxxPxpJ1fRZadgVrkQlrKTOa0un" +
                "6D3OPswme/xG4peX9Cz9TQnzO0qHa6qs/f/t8v3nOcaUSLTVgNkHq9EqvQRTbhJo1tqsZa7VqQO0zusr" +
                "ZSR+/QbO661nSh+KcGe+Pnm9wpPx3d2ukkfit3MJxoTbio4yPEdd5ORltg5Jm5W2Bd9rfIP0aQitmZmQ" +
                "OjjEvk3efoFDnCczm+Kg/JoAfHOc8MTdp8VyH2okfg+AY9OJ0V4gQBIKWWMQakSQvQSMct0MAg4Gz1XQ" +
                "LT6j9hxjYw7CHQ19NhrHR+Ug1mHrjIbjPC83YSThhSgFvCl39xXItHcV15jYG7Z4i6K4TlOWsV3k6dlH" +
                "3/Q2m095yGITNINIq5PznHE+UriZoeom05gwwq2811WCQUjxRDQPA0yYsY5Ihf1k2EI4KDnWCIMOFRXS" +
                "lefYzZiuyd+GELqH7twHV5LlrhIY7Q8MLX80mHbIQDcGve1hIlbdEAtDYgemrDr3GCqdkylnGNlxFSV6" +
                "pZOuHgIDxwZidJ74mgUgVdShLtDqNFZdd/nDqq+XvQJ+1L5J3ckZfRBFHRmeRWgQ/QcRa1yE/gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
