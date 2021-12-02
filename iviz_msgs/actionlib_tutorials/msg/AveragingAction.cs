/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingAction : IDeserializable<AveragingAction>,
		IAction<AveragingActionGoal, AveragingActionFeedback, AveragingActionResult>
    {
        [DataMember (Name = "action_goal")] public AveragingActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public AveragingActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public AveragingActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public AveragingAction()
        {
            ActionGoal = new AveragingActionGoal();
            ActionResult = new AveragingActionResult();
            ActionFeedback = new AveragingActionFeedback();
        }
        
        /// Explicit constructor.
        public AveragingAction(AveragingActionGoal ActionGoal, AveragingActionResult ActionResult, AveragingActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal AveragingAction(ref Buffer b)
        {
            ActionGoal = new AveragingActionGoal(ref b);
            ActionResult = new AveragingActionResult(ref b);
            ActionFeedback = new AveragingActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AveragingAction(ref b);
        
        AveragingAction IDeserializable<AveragingAction>.RosDeserialize(ref Buffer b) => new AveragingAction(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVX21IbRxB936+YKh4MqYATOxeHKh4UEA4pbFOg5JUa7bR2J5mdUeaC0N/n9OxFEohC" +
                "lTJBhZEWzZw+fTvdHt2Rl5W21aiM2tmPThoh88fbCp+L0eb31xSSif0Jn58enjknUlNZ/t2fmnXPxclX" +
                "fhWfbj4ed1aMnt7GFJ3X0oS3W7wqfiOpyIs6vxWrW02owls+cXEm2OVbrVYe5XjkQLwM+RBVS6BlV+yJ" +
                "myitkl6JhqJUMkoxc2Ctq5r8oaE7Mrgkmzkpkb+NyzmFI1yc1DoI/FRkQd6YpUgBh6ITpWuaZHUpI4mo" +
                "G9q4j5vaCinm0kddJiM9zjuvtOXjMy8bYnT8BPonkS1JXJwd44wNVKaoQWgJhNKTDAgYvhRF0ja+f8cX" +
                "ir3Jwh3ikSrEfjAuYi0jk6X7OYqIecpwDBvftM4dARvBIVhRQeznv93iMRwIGAEFmruyFvtgfrWMtbMA" +
                "JHEnkf2pIQYuEQGgvuFLbw7WkJn2sbDSuh6+RVzZ2AXWDrjs02GNnBn2PqQKAcTBuXd3WuHodJlBSqPJ" +
                "RoGC89IvC77Vmiz2zjnGOIRbOSN4lyG4UiMBSix0rIsQPaPnbHB9vngrrTVFLq2OrAi1S0bhwXmm3NaT" +
                "QC4XtUZCshPcLmIhg/BcMAFOcAFd5HznkkRIpO2MIckezYb7ZIWOAo5S4KJFXVAzh9QYg9uMGdqqWRBM" +
                "D9BiSugPUBAl+SiROWa0Ht+Ov1Z9ThBe0ENa3CrOohcpMFO40SobejAEWVFOgghzKvVMl62DHYNw1KFz" +
                "g7QHQKpJIYKZQNfh1FGfP87ca6hg1r+ibUpSFYViZpzkJy+VTuH1lLmdJ89rM1QxJrRqflvJczeOujn0" +
                "0l48YFM8GBqseh96iu3D1fjz2cXnj6J/nYjv8LstyFxFNdpkSSh7x+WCAi1bNexUY6NHOszR6eTiz7FY" +
                "w/x+E5NlKnkPuYEyT4kLbyfgq+vx+NPVZHw2AL/bBPZUEvQeWg0dhGYOTSDkLCJ9aF9477kr6T4PB1sV" +
                "K6KPX3v4h37LUWhVGKNqbogRdAw9CojuT8g3GEmG52Okg47yzR+np+Px2Rrl95uUWYZkWWvMTahWKjkK" +
                "s8TDcVsgnjIz+vXL9SoubOaHLWamLruuUu71FfetllSiZ0PDVREchGwmtUkQuSfoXY9/H5+u8TsRPz6m" +
                "5+kvKrNgbqPDAudSfFgu3z7PcUqlhKpnzMFYwvLAYpzHJtYXbe+kgQI/4UBXeUOnnIif/ofKG0rPupib" +
                "cFV8Q/KGCJ+OLi9XnXwift6VYDeftjHcJbrIyeNsbZK2M+0b3vR4Og5pyMsKMyG14cR6mXz4Ck7sFmYu" +
                "io32aw3wLvVETVx+uZmsQ52IXzLgaNgcupUKSEIhawxCbRDkEAJG4QmNj93qwnGb7tB7gbEdR5tDutBw" +
                "f8vegu1iZIxb5CWdD6IV/OZmIRGzrAh5iVgbZnxF0TRVPMn6FSHSfXydJaEbxcNqwEu7187fSlsZGv4s" +
                "5w5+Nq+3Mpz3/538r0tDDzDsfK/hy+BFUfwLz/IpJoMPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
