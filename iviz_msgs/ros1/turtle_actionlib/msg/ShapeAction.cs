/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeAction : IDeserializable<ShapeAction>, IMessage,
		IAction<ShapeActionGoal, ShapeActionFeedback, ShapeActionResult>
    {
        [DataMember (Name = "action_goal")] public ShapeActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ShapeActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ShapeActionFeedback ActionFeedback { get; set; }
    
        public ShapeAction()
        {
            ActionGoal = new ShapeActionGoal();
            ActionResult = new ShapeActionResult();
            ActionFeedback = new ShapeActionFeedback();
        }
        
        public ShapeAction(ShapeActionGoal ActionGoal, ShapeActionResult ActionResult, ShapeActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public ShapeAction(ref ReadBuffer b)
        {
            ActionGoal = new ShapeActionGoal(ref b);
            ActionResult = new ShapeActionResult(ref b);
            ActionFeedback = new ShapeActionFeedback(ref b);
        }
        
        public ShapeAction(ref ReadBuffer2 b)
        {
            ActionGoal = new ShapeActionGoal(ref b);
            ActionResult = new ShapeActionResult(ref b);
            ActionFeedback = new ShapeActionFeedback(ref b);
        }
        
        public ShapeAction RosDeserialize(ref ReadBuffer b) => new ShapeAction(ref b);
        
        public ShapeAction RosDeserialize(ref ReadBuffer2 b) => new ShapeAction(ref b);
    
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = ActionGoal.AddRos2MessageLength(c);
            c = ActionResult.AddRos2MessageLength(c);
            c = ActionFeedback.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "turtle_actionlib/ShapeAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VX227bRhB951cM4IfYRe00SS+pAT2otuIqcBLDUvtSFMKKOyS3Xe6ye7Gsv+/MkqKk" +
                "RKqFIrYF2RLJ2TNn7qNJJRoc5kFZc2WFBpG+zkr6nk3Wz27RRx1WT1262nz+DlHORf73SqLorrPBV35l" +
                "HyZX5xCiCxpnrTKt5i8n22Zkv6KQ6KBKH1kvN6t96V+yxPgS2MaZkq0Zyfhk9eMw9kG2yltm2RFMgjBS" +
                "OAk1BiFFEFBYYqzKCt2pxjvUdEjUDUpIT8OyQX9GB6eV8kDvEg06ofUSoiehYCG3dR2NykVACKrGrfN0" +
                "UhkQ0AgXVB61cCRvnVSGxQsnamR0env8J6LJEcaX5yRjPOYxKCK0JITcofDKlPQQsqhMePOaD8AR/HFr" +
                "/as/s6Ppwp7SfSwpAD0LCJUIzBrvG0ofJiz8OSn7prXyjJSQl5DUSQ/H6d6MLv0JkDbigo3NKzgmE26W" +
                "obKGABHuhFNirpGBc3IFob7gQy9ONpBNgjbC2BV8i7jWcQis6XHZptOKgqfZDT6W5EkSbJy9U5JE58sE" +
                "kmuFJgBlnRNumfGpVmV29I6dTUJ0KoWGPoX3NlcUCQkLFarMB8foKSycpI+UljsrI+VYRxZ8ZaOWdGEd" +
                "JruSIRTLRaUoIMkIrhtYCA+OM8eTEZxJ4xTvlJvkEmE6ZRRkd0epsajQgApAhqLn7KW8wLqhJqM1nWZM" +
                "32bNAkl1Dw1zLJiLgBxdEBQ5ZrTp346/kquYkHuJ3pKV9H6Gom9ZRtKJtqdRMXovSkxBAN9grgqVtwZ2" +
                "DPxZh86V0goQqTr6QMyAyo+kzlbx48g9aQ9M3S9ryxJliT4rtBV85YRU0T9DQ25nx8MtmRpiiJ5jSB9t" +
                "V+7GTjdvHof6XibZZ3OCm93bFb324mb08XL88QpWrwF8R//b9Es5U1FRLDFw5lFyUDrmbe/resRWRXSY" +
                "w4vp+PcRbGC+2sbkphSdo+ZCDXmOnGYHAd/cjkYfbqajyx749TawwxypzUsuLUEdsk95EEWg0FGxkvWO" +
                "axDv00wwZQb/8TqiP6qu5IW259KEajQyggp+hUJEj6foappEmsdiwJOO8uS3i4vR6HKD8pttytx0RF4p" +
                "ZNo+5uyFIvJM3OWIfWqGv3y6XfuF1Xy/Q83cJtNlTJW95r5Tk4z4oGs4K7yltlUIpSO1tD30bkfvRxcb" +
                "/Abww5f0HP6FediTAamd2Rg+T5dvH+Y4x1xQD0+YvbJIOwO33jQkaWtR5k5o6rd7DOgyr6+UAfz4BJnX" +
                "p56xIRXhOvn64PUevhheX68reQA/HUqwm0a7GB7iXYrJl9HaJm0K5Wpe8HgWhs0ukJig3DJiM03efgUj" +
                "DnMzJ8VW+bUKeHPakxPXnybTTagB/JwAh/2e0C1QhASSosYg2DpB9C5glLN2I+4WFfbb/IDa84xt2dvs" +
                "0oUi83dsKbRLDLW2i7SbsyCVgtveIwR0sz6tDBuDjI9InMeyZDd2QgHvwxOvBN387RcB3s2dsm4mTKmx" +
                "vy0aSwbWz7AgrH48/q8Vof/l+Sw/OXvqWfYvZSNPw0oPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
