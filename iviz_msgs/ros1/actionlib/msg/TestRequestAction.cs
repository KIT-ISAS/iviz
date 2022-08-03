/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestAction : IDeserializable<TestRequestAction>, IMessage,
		IAction<TestRequestActionGoal, TestRequestActionFeedback, TestRequestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestRequestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestRequestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestRequestActionFeedback ActionFeedback { get; set; }
    
        public TestRequestAction()
        {
            ActionGoal = new TestRequestActionGoal();
            ActionResult = new TestRequestActionResult();
            ActionFeedback = new TestRequestActionFeedback();
        }
        
        public TestRequestAction(TestRequestActionGoal ActionGoal, TestRequestActionResult ActionResult, TestRequestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public TestRequestAction(ref ReadBuffer b)
        {
            ActionGoal = new TestRequestActionGoal(ref b);
            ActionResult = new TestRequestActionResult(ref b);
            ActionFeedback = new TestRequestActionFeedback(ref b);
        }
        
        public TestRequestAction(ref ReadBuffer2 b)
        {
            ActionGoal = new TestRequestActionGoal(ref b);
            ActionResult = new TestRequestActionResult(ref b);
            ActionFeedback = new TestRequestActionFeedback(ref b);
        }
        
        public TestRequestAction RosDeserialize(ref ReadBuffer b) => new TestRequestAction(ref b);
        
        public TestRequestAction RosDeserialize(ref ReadBuffer2 b) => new TestRequestAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) BuiltIns.ThrowNullReference();
            ActionGoal.RosValidate();
            if (ActionResult is null) BuiltIns.ThrowNullReference();
            ActionResult.RosValidate();
            if (ActionFeedback is null) BuiltIns.ThrowNullReference();
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
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            ActionGoal.AddRos2MessageLength(ref c);
            ActionResult.AddRos2MessageLength(ref c);
            ActionFeedback.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "actionlib/TestRequestAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "dc44b1f4045dbf0d1db54423b3b86b30";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VX227jNhB911cQyMMmRZPdJLvtNoAfXEebusjFsN2iQFEYNDWW2FKkS1Jx/PedoShZ" +
                "jpPGQDep4USWODw8cx9Nwfkx/F3hpS+8NPrKcMV4+DnL8XcyfSwxBlcp38jYcLcr9QUgm3PxVyO3iPdJ" +
                "7yt/kpvJ1UU8Rcn5+x0mpFHyE/AMLCvCJWmlZ6XL3XuSGF4yUncms64uwRrBDK9D2/msplDzSw7YxHOd" +
                "cZuxEjzPuOdsYZC3zAuwxwruQeEmXi4hY2HVr5fgTnDjtJCO4TcHDZYrtWaVQyFvmDBlWWkpuAfmZQlb" +
                "+3Gn1IyzJbdeikpxi/LGZlKT+MLyEggdv45MogWw4eUFymgHovISCa0RQVjgTuocF1lSSe3Pz2gDO2C/" +
                "j407/SM5mK7MMT6HHN3QsmC+4J5Yw8MSI4kIc3eBh31Ta3mCh6CVAI/LHDsMz2Z4644YnoZcYGlEwQ5R" +
                "hdHaF0YjILB7biWfKyBggaZA1He06d1RB1kHaM21aeBrxM0Z+8DqFpd0Oi7QeYrM4KocLYmCS2vuZYai" +
                "83UAEUqC9gxjz3K7TmhXfWRy8IWMjUK4K7gGr9w5IyR6ImMr6YvEeUvowS0Uqq+dTd38CDEWyTJXmEpl" +
                "eGMsBL2CIujLVSHRIUEJyhu24o7ZOpkgo0gaBn+H2ESTcB0PQyfbewyNVQGaSc9QUXAUvRgXUC6x3iiF" +
                "uwnT1VGzAjy6hWZzWBAXzgRYz9FzxKhr38hfZo1P0LxIb02HtHZmi7Zu6Qx31OUNk9E5nkNwAnNLEHIh" +
                "Ra1gZOBOIjplSi2ApMrKeWTGMP1Q6qTxH3nuLQthKIF1Uk7T8c3wtj9NZ5NfBoN0Mul92Fnp/3g3nqaX" +
                "vdOdlXH6czqgpbOdpeu7Sdo733l8Ob4b9T7uPE5/G6Sj6fDutvcprnmwZSg6M3SXr1wyN0YxmWv06kxw" +
                "TE3VmK92yszDg282F9B0onqbmzlZLhXuDPmWZJXlIcwyUHw940LAcudpS2GzsOQYqQ2jN/RYt9W+3Lwm" +
                "gR+LNDswsVdH07wy/8d8kkd9ldrC54ZkfTNKby+Ht1es+fTYB/xfJ2rIrgLLxxo85SimESauqLtErKZb" +
                "tSNi9gfT4a8p62CebmNS+a6sxbDA1jUHiqi9gEfjNL0ZYfS3wGfbwBYEYEPMqAiFgG2KA+MLjC0qa6i9" +
                "pWoFD6F76jxh//I5wD+sQ8EKdXfCXo5RTQjSuwYFiR5O69hVNEB4OIqUQ46nlx3K59uUqTxzUUgg2q4S" +
                "ZIVFRdPDU4Z47phYMDom//jEMXMTVMfcIpNvuD95UlbBi6ahqHAGC/yCS1Vh8X+GXlO1NvQ+7dKz8CcI" +
                "/0wEhMJvKv84XL59meMcBNWQgNkeVmGVoSYVxgmc76S+5wo70zMKxMhrM6XHvnuDyGtDTxsfknATfK3z" +
                "WgsP+tfXm0zuse/3JRj79lMM97Eu+mTXW9uk9ULakkZhmhp8twoEJpBtKdENk89fQYn9zExBsZV+9QE0" +
                "Yz4TE9hvp12oHvshAPbbiSqOmoiE/a3ubVAbgbcmIJST+t0hjnRkt/keuecI25C1yaQrieo/Mc/h1NVX" +
                "yqzCWwwJYirY7YmLs9jWw3DVaWe0JYN5ledkxigUmv6bDk+xCb80aER935DY9ov2f5gS2nf1/+MlveWf" +
                "/AN7lD8DjBAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
