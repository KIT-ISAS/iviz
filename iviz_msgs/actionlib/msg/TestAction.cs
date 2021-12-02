/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestAction : IDeserializable<TestAction>,
		IAction<TestActionGoal, TestActionFeedback, TestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public TestAction()
        {
            ActionGoal = new TestActionGoal();
            ActionResult = new TestActionResult();
            ActionFeedback = new TestActionFeedback();
        }
        
        /// Explicit constructor.
        public TestAction(TestActionGoal ActionGoal, TestActionResult ActionResult, TestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal TestAction(ref Buffer b)
        {
            ActionGoal = new TestActionGoal(ref b);
            ActionResult = new TestActionResult(ref b);
            ActionFeedback = new TestActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestAction(ref b);
        
        TestAction IDeserializable<TestAction>.RosDeserialize(ref Buffer b) => new TestAction(ref b);
    
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
        [Preserve] public const string RosMessageType = "actionlib/TestAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "991e87a72802262dfbe5d1b3cf6efc9a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXVPbRhR916+4MzwEOgXapE1SZvzggkPJkIQBt6/MenUtbSvtursrjP99z13Jsg1m" +
                "8HQCHoMta/fcc7/OXY05xKGOxtlzpypS6ettge/ZuL91zaGp4vKmT1drtz8x5xOl/1kumHbX2eA7v7Iv" +
                "N+cnnZXKTI43yWd/sMrZU5k+sn7ZbR2KcCwrLs5IPLs1eWKfPE6uvgzREPPWdkss26ObqGyufE41R5Wr" +
                "qGjqQNgUJfvDiu+4wiZVzzindDcuZhyOsHFcmkB4F2zZq6paUBOwKDrSrq4ba7SKTNHUvLEfO40lRTPl" +
                "o9FNpTzWO58bK8unXtUs6HgH/rdhq5kuzk6wxgbWTTQgtACC9qyCsQVuUtYYG9+9lQ3Z3njuDnHJBcLe" +
                "G6dYqihk+X6GWhGeKpzAxg+tc0fARnAYVvJA++m3W1yGA4IRUOCZ0yXtg/nVIpbOApDpTnmjJhULsEYE" +
                "gPpGNr05WEMW2idklXVL+BZxZWMXWNvjik+HJXJWifehKRBALJx5d2dyLJ0sEoiuDNtIqDWv/CKTXa3J" +
                "bO+TxBiLsCtlBJ8qBKcNEpDT3MQyC9ELesqGlOZLt816P6TS6shSKF1T5bhwXii39UTI5bw0SEhyQtqF" +
                "5iqQl4IJcEIK6CLlO5UkQqJsZwxJ9ncojXnJlkwkOMpBihZ1wfUMilJV2C2Yoa2aOcN0D00TRn+AAmn2" +
                "USFzwmg9vh1/ky9zgvCCHtLiVnGmpSCBWY4drYChB0NQBackUJixNlOjWwc7BuGoQ5cGaReAVN2ECGaE" +
                "rsOqo2X+JHOvonhJ69omfEHx2qay7Rx4Xmchc7FB76WPJLXdBOlGxwszfkgke6D9omAfl+zai6vR17OL" +
                "r+e0fA3oJ/xviytVRImSXzBK2EnqUWy6VbZOATbqvcMcno4v/hrRGubPm5giOY33kA6o7ISliHYCvroe" +
                "jb5cjUdnPfDbTWDPmqHd0F1oGvSvL2hS04jMoRXhvZcO4/sk9LbIVkQfv/bwh95JUWgVFWNnVrEgmBiW" +
                "KCC6P2ZfY7xUMusiH3SUb/48PR2NztYov9ukLJKidGkwA6FAjZYoTBsZdNsC8ZSZ4e/frldxETO/bDEz" +
                "ccn1vEl9u+K+1VLe8LOhkaoIDqI0VaZqIFhP0LsefR6drvEb0K+P6Xn+m3USv210RKxcEx+Wy4/Pc5yw" +
                "VlDohNkba3AQEGFNIxBHEWPvVAU1fcKBrvL6ThnQ+1eovL70rIupCVfF1yevj/Dp8PJy1ckD+rArwW7W" +
                "bGO4S3SRk8fZ2iRtp8bXcmqTSdenIR08hAnnG06sl8nH7+DEbmGWothov9aAnIueqInLbzfjdagB/ZYA" +
                "h/0poDseAYlyZE1AuA2C6kMgKDJt8bU7hkjcJjv0XhBsJ9GWkM4N3N9yBsFJYVhVbp4O3LIQreA3TwkK" +
                "MUuKkA4Ea3NMtuQ8aYpCwtgtinwfX2ngd2O3HfmvM0KPHz/d/Z+x3z8ZvuojYc+4jVhvO/sPPp8SXe0O" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
