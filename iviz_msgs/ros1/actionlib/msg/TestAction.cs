/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestAction : IDeserializable<TestAction>, IMessage,
		IAction<TestActionGoal, TestActionFeedback, TestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestActionFeedback ActionFeedback { get; set; }
    
        public TestAction()
        {
            ActionGoal = new TestActionGoal();
            ActionResult = new TestActionResult();
            ActionFeedback = new TestActionFeedback();
        }
        
        public TestAction(TestActionGoal ActionGoal, TestActionResult ActionResult, TestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public TestAction(ref ReadBuffer b)
        {
            ActionGoal = new TestActionGoal(ref b);
            ActionResult = new TestActionResult(ref b);
            ActionFeedback = new TestActionFeedback(ref b);
        }
        
        public TestAction(ref ReadBuffer2 b)
        {
            ActionGoal = new TestActionGoal(ref b);
            ActionResult = new TestActionResult(ref b);
            ActionFeedback = new TestActionFeedback(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TestAction(ref b);
        
        public TestAction RosDeserialize(ref ReadBuffer b) => new TestAction(ref b);
        
        public TestAction RosDeserialize(ref ReadBuffer2 b) => new TestAction(ref b);
    
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
    
        public const string MessageType = "actionlib/TestAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "991e87a72802262dfbe5d1b3cf6efc9a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XUVPbRhB+16/YGR4CnQJN0iYpM35wwaFkSMIYty+dDnM+raVrpZN7d8L43/fbkyxk" +
                "sIunE/AYbFl33367++3uacI+DHUwlT2vVEEqfr3J8D2ZdLfG7OsirG66eNW7/ZE5nSr992rBrL1OBt/4" +
                "lXy+Pj9prRRmejxZI5/8yiplR3n8SLplN6XP/LGsuDgj8ezGpJF99Di6+jxEfUgb2w2xZI+ug7KpcimV" +
                "HFSqgqJZBcImy9kdFnzLBTapcs4pxbthOWd/hI2T3HjCO2PLThXFkmqPRaEiXZVlbY1WgSmYktf2Y6ex" +
                "pGiuXDC6LpTD+sqlxsrymVMlCzrenv+p2Wqmi7MTrLGedR0MCC2BoB0rb2yGm5TUxoa3b2QD7dEf48q/" +
                "/jPZmyyqQ/zOGeLfsaCQqyCs+W4O0Qhh5U9g7LvGyyMYQZQY5lJP+/G3G1z6A4I1cOF5pXPahwtXy5BX" +
                "FoBMt8oZNS1YgDVCAdRXsunVQQ/ZRmirbLWCbxDvbewCaztc8ekwR/IKCYOvM0QSC+euujUplk6XEUQX" +
                "hm0giM4pt0xkV2My2fsowcYi7IqpwafyvtIGmUhpYUKe+OAEPaZFNPrc9dMvjKixliz5vKqLFBeV4+hX" +
                "dAS5XOQGCYlOSN3QQnlyohwPJ0RJFzHfUZsIibKtMSTZ3UIai5wtmUBwlL2oF7rgco7WUhTYLZi+Uc2C" +
                "YbqDpinPhIsizS4oZE4Y9ePb8jfpKicIL+gtxUgXZ5p1ncqm2NF0MhSj9yrjmATyc9ZmZnTjYMvAH7Xo" +
                "UinNApAqax/AjFB+WHW0yp9k7kVaX2x6TTU+Yxfb1G6bgfB0w0W/C7WXFOEj9tx2lLQz5JkZPySSPBgC" +
                "0so+rNg1F1ejL2cXX85p9RrQD/jfiCsqIofklxxEV0g9xKabztZ2gDW9t5jD08nF7yPqYb5ex5SWUzuH" +
                "1oF2O2UR0U7AV+PR6PPVZHTWAb9ZB3asGU08lcJR6H+doEnNAjKHUoT3TiqM72LHt1lC//Hawx9qJ0ah" +
                "6aiYP/OCBcEEv0IB0f0JuxJzppChF/igpXz92+npaHTWo/x2nbK0FKVzw0Lb11qiMKtl4m0KxDYzw1++" +
                "ju/jImZ+3GBmWkXX0zrW7T33jZbSmp8MjajCV2hKM2WKGg1rC73x6NPotMdvQD89puf4L9ZhiwJis6rq" +
                "8FAu3z/NccpaoUNHzM5YjROBNNY4AnEmMfZWFeimWxxolddVyoDevYDyOunZKsQivBdfl7wuwqfDy8v7" +
                "Sh7Q+10JtrNmE8NdooucPM7WOmk7M66U45tMutDvApEJp2tO9GXy4Rs4sVuYRRRr5dcYkHPRFk1cfr2e" +
                "9KEG9HMEHHangPZ4BCRKkTUB4SYIqguBoBw15932GCJxm+5Qe16wK4m2hHRh4P6GMwhOCsOiqBbx5C0L" +
                "UQpu/ZSgqJ3k8UDQm2OyJeVpnWUSxnZR4LvwQgO/HbvNyH+ZEXr8+DHv/4z97hHxRZ8NO8ZNxDrbyb/Z" +
                "mxYz9g4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
