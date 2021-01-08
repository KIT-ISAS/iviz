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
                "H4sIAAAAAAAACr1X227jNhB9F+B/IJCHTYomaXd72Qbwg+t4sy6yu0Hi9jWgqbHEVhJdkrLjv+8ZUpKt" +
                "xkGNYmMhji2LPHM4lzPjGTk/Ul6b6sbIQsjw8THD52TWPbonVxe+fWjD3c7jD0TpXKq/2gWL5n6QDL/y" +
                "NUg+PdxcNXYKPb/s0x8kH0mmZEUe3pJu3WPpMnfJS6bXgg/3qNNwgHBo/uLVuDqfRuuR2iA5EQ9eVqm0" +
                "qSjJy1R6KRYGnHWWkz0vaEUFdslySakIT/1mSe6Cd85y7QT+MqrIyqLYiNphlTdCmbKsK62kJ+F1ST0A" +
                "3qorIcVSWq9VXUiLDcamuuL1CytLCvj8cvR3TZUiMb2+wqrKkaq9BqkNMJQl6XSV4SEW17ry797yDmyc" +
                "rc057imD/zsGwufSM2N6WiJvmKx0V2zmm3jGC8DDSQRDqROn4btH3LozATtgQUujcnEK+ncbn5sKiCRW" +
                "0mo5L4iRFfwA2De86c3ZLjRTvxKVrEyLHyG3Rg7BrbbAfKzzHMEr2AWuzuBHrFxas9Ip1s43AUUVmiov" +
                "kHdW2s0g4W3RKEA+sLOxDPtCbPAunTNKIxKpWGufDxLnLRsIcUGmDpLXr6TdComZ1lAWLjd1keLGWOYd" +
                "00sgqutcIzLhJFxBYi2dsJw8DicJ6TQNoQ8pCtfIqjGHcNsVsmSdUyW0FzgtOU5ipAiVSwhNgXo8Cagu" +
                "ZtCaYLwDF3NCxYCEUGS9RAyZU9/R7SF02sYHjgZHhMhsPS5arQI7+PkENoLUIX2dzCjEQ7glKb3QKh6z" +
                "YeEuGvhQM3EFmJW186AnUItYdtHFMkbxSIoYtTAW56uK2z4hjs3iACmGDPoaNRnegho3fSbG4Jg5H6kM" +
                "kn6HiAL3vmHY3t1NPl9PP9+I9hqK7/A/JltIkBx1sCGkteFEQPKpKHyNOvRqoAUdjWfTPyZiB/T7PigL" +
                "Um0tdAU6PCdOqcOQ7+4nk093s8l1h/y2j2xJEeQdwgzJgzx2GS7kwiOAKFA4wHLZ0VPoBVU2SLZUn18n" +
                "eKGcgiOi4qI7LQtiCO1dCwOqpzOyJZpQwT3R01lL+uH38Xgyud4h/a5PmrVGqlyjWUKaasWOWNTcEPf5" +
                "4kU7o1+/3G9dw3Z+2GNnbsLp0zpU8pb9XlNpTf/tHc4NZyBWC6mLGkr2EsH7yW+T8Q7DofjxOUFLf5IK" +
                "sriPEEuYqf2/k+bbA1jOSUmodwDtrNWYGVhzQ5/E3KKrlSygsi8doUnArmSG4qdjJGCXgZXxoRy3OdhF" +
                "cOvl8ej2dlvUQ/HzoRSbTrSP40EeRmCeh6xPu1poW/KUx42wC0WYUJgKpf1j7CbL+69wjANdzanRK8Ro" +
                "gUeolzLj9svDbBdrKH4JiKNuUGgmKUCJFKFjFIp+kJ0XGIV7MT42owq7bn5IFToGN+xxdutawwN75pQw" +
                "S4yKwqzDnM5LURS2P0dIOC7IQ5gYdtobb0lpXmdZ8GWzytOTP+JA0PbkOBIcq79ePv+J+L/Ggu735ZF/" +
                "WG5JR7+15hG1fwCzoNIZNw8AAA==";
                
    }
}
