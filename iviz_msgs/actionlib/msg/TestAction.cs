/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestAction")]
    public sealed class TestAction : IDeserializable<TestAction>,
		IAction<TestActionGoal, TestActionFeedback, TestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestAction()
        {
            ActionGoal = new TestActionGoal();
            ActionResult = new TestActionResult();
            ActionFeedback = new TestActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestAction(TestActionGoal ActionGoal, TestActionResult ActionResult, TestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestAction(ref Buffer b)
        {
            ActionGoal = new TestActionGoal(ref b);
            ActionResult = new TestActionResult(ref b);
            ActionFeedback = new TestActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestAction(ref b);
        }
        
        TestAction IDeserializable<TestAction>.RosDeserialize(ref Buffer b)
        {
            return new TestAction(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib/TestAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "991e87a72802262dfbe5d1b3cf6efc9a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71X31PjNhB+91+xMzwcdAq0d/1xZSYPKeSudLg7BtK+Moq0sdXacirJhPz3/VZ2jAOh" +
                "ZDoHmUDiWPr2291vd+UphzjW0dbuY61KUunrTY7v2bS/dcWhKeP6pk9Xg9sfmM1M6b/XC+bddTb6yq/s" +
                "0/XHk85KaWfH0w3y2W+sDHsq0kfWL7upQh6OZcX5GYlnN9Yk9snj5OrLEA3RtLZbYtkeXUfljPKGKo7K" +
                "qKhoXoOwzQv2hyXfcolNqlqwoXQ3rhYcjrBxWthAeOfs2KuyXFETsCjWpOuqapzVKjJFW/HGfuy0jhQt" +
                "lI9WN6XyWF97Y50sn3tVsaDjHfifhp1mOj87wRoXWDfRgtAKCNqzCtbluElZY11891Y2ZHvTZX2IS84R" +
                "9t44xUJFIct3C2hFeKpwAhvftM4dARvBYVgxgfbTbze4DAcEI6DAi1oXtA/ml6tY1A6ATLfKWzUrWYA1" +
                "IgDUN7LpzcEA2SVop1y9hm8R723sAut6XPHpsEDOSvE+NDkCiIULX99ag6WzVQLRpWUXCVrzyq8y2dWa" +
                "zPY+SIyxCLtSRvCpQqi1RQIMLW0sshC9oKdsiDRfumyG9ZCk1ZGlUNRNaXBRe05+JUeQy2VhkZDkhJQL" +
                "LVUgL4IJcEIEdJ7ynSSJkCjXGUOS/S2ksSzYkY0ERzmIaKELrhboKGWJ3YIZWtUsGaZ7aJrxXLgo0uyj" +
                "QuaE0TC+HX9r1jlBeEFvJUb6ONO8b1DOYEfbwFCDIaicUxIoLFjbudWtgx2DcNShS4G0C0CqakIEM0LV" +
                "YdXROn+SuVfpeKnXtUX4gs1rW5dt58DzfRZtLjZBUoSP1Gq7CdKNjhdm/JBI9qD3Swd7v2bXXlxOPp+d" +
                "f/5I69eIvsP/VlxJEQUkv+IoukLqITbddrauA2zovcMcn07P/5zQAPP7TUxpOY33aB3osjMWEe0EfHk1" +
                "mXy6nE7OeuC3m8CeNaN3Gykchf7XC5rUPCJzKEV476XC+C41epdn9B+vPfyhdlIU2o6KsbMoWRBsDGsU" +
                "EN2fsq8wXkqZdZEPOsrXf5yeTiZnA8rvNilLS1G6sCy0Q6MlCvNGBt22QDxlZvzrl6v7uIiZH7aYmdXJ" +
                "ddOkur3nvtWSafjZ0IgqQo2mNFe2bNCwnqB3Nfl9cjrgN6IfH9Pz/Bfr+IQCUrOqm/hQLt8+z3HGWqFD" +
                "J8zeWIODgDTWNAJxFLHuVpXopk840Cmvr5QR/fQKyuul5+qYivBefH3y+gifji8u7it5RD/vSrCbNdsY" +
                "7hJd5ORxtjZJu7n1lZzaZNLFYRdITNhsODGUyfuv4MRuYRZRbJRfa0DORU9o4uLL9XQINaJfEuC4PwV0" +
                "xyMgkUHWBITbIKg+BIJy1B5zu2OIxG22Q+0Fwa4l2hLSpYX7W84gOCmMy7JepgO3LEQp+M1TgqJukqcD" +
                "wWCOyRbDsybPJYzdosh38ZUGfjd225H/OiP0+PHT3f8Z+/2T4as+EvaM24j1trN/AT6fEl3tDgAA";
                
    }
}
