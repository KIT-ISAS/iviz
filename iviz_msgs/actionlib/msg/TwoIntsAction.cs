/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsAction")]
    public sealed class TwoIntsAction : IDeserializable<TwoIntsAction>,
		IAction<TwoIntsActionGoal, TwoIntsActionFeedback, TwoIntsActionResult>
    {
        [DataMember (Name = "action_goal")] public TwoIntsActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TwoIntsActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TwoIntsActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsAction()
        {
            ActionGoal = new TwoIntsActionGoal();
            ActionResult = new TwoIntsActionResult();
            ActionFeedback = new TwoIntsActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsAction(TwoIntsActionGoal ActionGoal, TwoIntsActionResult ActionResult, TwoIntsActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsAction(ref Buffer b)
        {
            ActionGoal = new TwoIntsActionGoal(ref b);
            ActionResult = new TwoIntsActionResult(ref b);
            ActionFeedback = new TwoIntsActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsAction(ref b);
        }
        
        TwoIntsAction IDeserializable<TwoIntsAction>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6d1aa538c4bd6183a2dfb7fcac41ee50";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXW/bNhR9F+D/QCAPTYYl3dqu6wL4wUuc1EPaBom314CSriVuEumRVBz/+51LSrK1" +
                "OJgxtDZiy7Iuzz33+2a+MjPt3STzyuhrIyshw9eHAt+T+fbTO3JN5bvnNtwNJa6I8lRmf3Uyi/Z+lIy/" +
                "8muUfLq/Pm/1VCp9/cyOUfKRZE5WlOGS9KIPtSvcaxaZXQq28kHlnRnBAfzbN2PsfB4JRHaj5Ejce6lz" +
                "aXNRk5e59FIsDGiroiR7WtEjVTgl6yXlIjz16yW5Mz45L5UT+CtIk5VVtRaNg5Q3IjN13WiVSU/Cq5oG" +
                "AHxUaSHFUlqvsqaSFgeMzZVm+YWVNQV8fjv6uyGdkZhdnkNKO8oar0BqDYzMknRKF3gI4UZp//YNn8BB" +
                "ePQU91QgBD0D4UvpmTE9LZFATFa6c1bzXbTxDPBwEkFR7sRx+O0Bt+5EQA9Y0NJkpTgG/du1L40GIolH" +
                "aZVMK2LkDH4A7Cs+9OpkG5qpnwsttenwI+RGyT64egPMZp2WCF7FLnBNAT9CcmnNo8ohm64DSlYp0l4g" +
                "9ay061HCx6JSgFyxsyGGcyE2uErnTKYQiVyslC9HifOWFYS4IFlHybevp+0iiZnWUhauNE2V48ZY5h3T" +
                "SyCqq1IhMsESriCxkk5YTh4HS0I6zULoQ4rCNVK36hBu+4gsWZWkhfIC1pLjJEaKUL1Ex6lQj0cB1cUM" +
                "WhGU9+AiJVQMSIiMrJeIIXMaOrozQuVdfOBocESIzMbjoutYYAc/H0FH6HlIXycLCvEQbkmZWqgsmtmy" +
                "cGctfKiZKAFmdeM86AnUIsTO+ljGKB6uL8aOmKAg378Tsr2mh+7LcYLs0ZnREn2D+gyXrjm38yeG5JAl" +
                "ENmMkuHMiP3uQ0uyu7udfr6cfb4W3WssfsBnzL2QLyXKYk3IcsN5gVzMYh9sm8WgJDrQycV89sdUbIH+" +
                "OATl/tRYizaDtpwSZ9h+yLd30+mn2/n0skd+M0S2lBG6Pfo0OiC6ZZ/wQi48Yoh6hQMsVyE9hdGgi1Gy" +
                "ofr8dYQ3qis4IjZgDKtlRQyhvOtgQPV4TrbGTKp4RHo66Ujf/35xMZ1ebpF+OyTNrUdmpcLsRKdqMnbE" +
                "ouH5uMsXL+qZ/PrlbuMa1vNuh57UBOvzJhT2hv1OVXlD/+0dzg1n0LsWUlUNGttLBO+mv00vthiOxU/P" +
                "CVr6k7LQJXcR4o5mGv/vpPl+D5YpZRLNPID22hqsENyCw9jEGqP0o6zQdF8yoU3AvmTG4v0hErDPQG18" +
                "KMdNDvYR3Hj5YnJzsynqsfh5X4rtYNrFcS8PIzDPQzakrRfK1rz08VzsQxEWFqZC+dCM7WT58BXM2NPV" +
                "nBqDQowaeKN6KTNuvtzPt7HG4peAOOn3hnaxApTIETpGoegH2XuBUXg042u7ubDr0n2q0DG4YY+zW1cK" +
                "HtixtoTVYlJVZhXWdhZFUdjhWiHhuNAewgKxNeH4SE5pUxTBl62Upyd/2P2gm8xxMXBNfejV4Kr/V/H/" +
                "LQfd+X6DO5wBG+qj5B+uIyTtTw8AAA==";
                
    }
}
