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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACr1XUW/bNhB+F5D/QCAPTYol2dpu6wL4wUvcNEPaBom314CmzhI3ifRIyo7//b4jJdlu" +
                "HNQYGhtObFnk3Xd3331HjcmHoQramisrKyHj14cC37Nxf+uOfFOF7qaLV2u3PxDlE6n+6RZM2+uDbPCd" +
                "XwfZp/ur89ZPpSdnm/APso8kc3KijB9Zv+6h9oU/4yXXl4KDe9B5DCAGzT+8GFYf8uQ9QTvIDsV9kCaX" +
                "Lhc1BZnLIMXUArMuSnInFc2pwi5ZzygX8W5YzsifYuO41F7gXZAhJ6tqKRqPRcEKZeu6MVrJQCLomjb2" +
                "Y6c2QoqZdEGrppIO663LteHlUydrYut4e/q3IaNIXF+eY43xpJqgAWgJC8qR9NoUuCmyRpvw9g1vyA7H" +
                "C3uCSyqQ+d65CKUMDJYeZ2AM45T+HD5ep+BOYRvZIXjJvTiKvz3g0h8LOAEEmllViiMgv12G0hoYJDGX" +
                "TstJRWxYIQOw+oo3vTpes8ywz4WRxnbmk8WVj13Mmt4ux3RSomYVR++bAgnEwpmzc51j6WQZjahKkwkC" +
                "dHPSLTPelVxmhx84x1iEXbEi+JTeW6VRgFwsdCgzHxxbj9Vgdr5886w3BdNyjBhS6XxpmyrHhXWMOlFK" +
                "oJyLUqMmMQ5uGrGQXjjmjEcczKHrWPLISmRFmtYb6uzmYMeiJCN0EIiVPPMW1KB6BmmpKuxmmz4RZ0Fw" +
                "3ZsWE0KLAIJQ5IJE8RjReopb/DrvyoIMAx4qY1epFp0yAVmOHUnJ0Ibey4JiHYSfkdJTrVKALQJ/2lrn" +
                "HkkLAKpufAAygcbDqtOuhFiV7Un6kuilVnxRFdumuGkq7KC50LvQoAnjR5TddqCk/O+T6QnKQfbVKGA1" +
                "e98BTBe3o8+X15+vRPcaiB/xP7EsUqME95cELlvmAFinksq1arBB/Nbm8GJ8/ddIrNn8adMmy0/jHGQE" +
                "ijshZtNOhm/vRqNPt+PRZW/4zaZhR4qg49Bg6Bu0sGe2kNOA4qEnEb3jVqPHKPqmyFZAn74O8YcmillI" +
                "6ooRNKuILejgOysAejQmV2PUVDz3Ah23kO//vLgYjS7XIL/dhMzaIlWpMQ8hRY3iLEwbHnrbEvGcm+Hv" +
                "X+5WeWE377a4mdgYet7EBl5h3+opb+ibqWFWeAt1mkpdNVCuZ+Ddjf4YXazhG4ifn8Jz9DepqILb4LBq" +
                "2SZ8TZcfvo1xQkpCqqPN3lmDQwErbByHOJZoM5cVZPWZAFrm9Z0yEL/sgXk99YwNsQlX5OuL12f4Ynhz" +
                "s+rkgfh1V4Dt0NmGcJfsoiZPq7UJ2ky1q/kExyOvL0M8hDASyjeCWKfJ++8QxG5pZlJstF9ywGekZzhx" +
                "8+V+vG5qIH6LBof9caA9KsGSyFE1NkIpCbJPAVvhsYuv7XmE8zbZofc827acbU7pQiP8LYcRHBmGVWUX" +
                "8fDNC9EKbvO4IJGzqAjxZLA2ynhLTpOmKDiN7aJAj2Fvk78bvmn272uQnj196Ptf879/Ytzzo+IKdMpb" +
                "5x5V+w+WbrDYCQ8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
