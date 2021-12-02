/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestRequestAction : IDeserializable<TestRequestAction>,
		IAction<TestRequestActionGoal, TestRequestActionFeedback, TestRequestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestRequestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestRequestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestRequestActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public TestRequestAction()
        {
            ActionGoal = new TestRequestActionGoal();
            ActionResult = new TestRequestActionResult();
            ActionFeedback = new TestRequestActionFeedback();
        }
        
        /// Explicit constructor.
        public TestRequestAction(TestRequestActionGoal ActionGoal, TestRequestActionResult ActionResult, TestRequestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal TestRequestAction(ref Buffer b)
        {
            ActionGoal = new TestRequestActionGoal(ref b);
            ActionResult = new TestRequestActionResult(ref b);
            ActionFeedback = new TestRequestActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestRequestAction(ref b);
        
        TestRequestAction IDeserializable<TestRequestAction>.RosDeserialize(ref Buffer b) => new TestRequestAction(ref b);
    
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
        [Preserve] public const string RosMessageType = "actionlib/TestRequestAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "dc44b1f4045dbf0d1db54423b3b86b30";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXW2/iRhR+968YKQ+bVE12N9ltt5F4oMSbssoFAa36hgb7YE9re+jMOIR/3++ML5hA" +
                "FKRuUkRi7DnzzXfux1Oybkz/lLj0I6d0ca1lJqT/OUvwO5g+lRiTLTPXyBh/tyv1lSiey+jvRm5R3we9" +
                "7/wJbifXl/UpmZq/36tR8BvJmIxI/SVopWe5Tex7lhheCVZ3puKuLt4a3gyvQ9u6uKJQ8QuOxMTJIpYm" +
                "Fjk5GUsnxUKDt0pSMqcZPVCGTTJfUiz8qlsvyZ5h4zRVVuCbUEFGZtlalBZCTotI53lZqEg6Ek7ltLUf" +
                "O1UhpFhK41RUZtJAXptYFSy+MDInRsfXskmKiMTw6hIyhaWodAqE1kCIDEmrigSLIihV4S7OeUNwNF3p" +
                "U9xSAuu3hwuXSsdk6XGJAGKe0l7ijB8q5c6ADeMQTomtOPbPZri1JwKHgAItdZSKYzAfrV2qCwCSeJBG" +
                "yXlGDBzBAkB9x5venXSQmfalKGShG/gKcXPGIbBFi8s6nabwWcba2zKBASG4NPpBxRCdrz1IlCkqnEDI" +
                "GWnWAe+qjgyOvrKNIYRd3iO4Smt1pOCAWKyUSwPrDKN7b3CEvnYSddPCh1ZNVthUl1mMG22YchVPAr5c" +
                "pQoO8UpwuoiVtMJUOUQxB9DQ+9uHJEwii/owONk8IDRWKRVCOQFFyXLQIi4oX6LMZBl2M6atomZFOLqF" +
                "FnNCfoCCiMg4Cc8xo659a/4qbnwC84Ie3KI3dhZNeQKzGDuqqoYctFYm5J0g7JIitVBRpWDNwJ7V6Jwg" +
                "lQBI5aV1YCaQdZA6a/zHnnvL+ucrX5WL03B8O7zrT8PZ5PfBIJxMeh92Vvq/3o+n4VXv487KOPwWDnjp" +
                "fGfp5n4S9i52Hl+N70e9TzuPwz8H4Wg6vL/rfa7XHJnc15oZ3OVKG8y1zoRKCnh1FkmkZtaYr3LKzNGj" +
                "azan1DSgapudWZUvM+z0+RbEpZE+zGLK5Homo4iWO09bCpuFpUSkNoze0GPdDvtyz5p4fqKm2YGpW3Rt" +
                "mlfm/5RP8KSdcjf40pCsbkbh3dXw7lo0n574gP9VovrsSlE+1oRyoDmNkLhR1SXqarpVO2rM/mA6/CMU" +
                "HcyP25hcvktjEBboWHPiiDoIeDQOw9sRor8FPt8GNhQR+iB6mKgCtikOQi4QW1zWoL3hakWPvmkWSbAh" +
                "uvs5wh/qkLdC1Z3QwhHVjKCcbVBA9HhaxW7Gc4Ojk5qyz/HwqkP5Ypsyl2cZpQrzBKp5ibSwdlHy0LDP" +
                "EM8dUxeMjsk/7Tlmrr3qyC02+Yb73pPikl40DUeF1SjwC6myEsX/GXpN1drQ+7xLz9BfFPlGso8OF35d" +
                "uqfh8uPLHOcUcQ3xmO1hJaoMNyk/TmCsU8WDzNCZnlGgjrw2U3ripzeIvDb0Cu18Em6Cr3Vea+FB/+Zm" +
                "k8k98fOhBOu+vY/hIdaFT3a9tU26WCiT8wTMU0PrBj/EMROKt5TohsmX76DEYWbmoNhKv+oAnjGfiQn0" +
                "22kXqid+8YD9dqKqR00gob9VvY0qI8jWBIzCkwt+1iMd221+QO5ZxtZsbTbpSkH9PfMcpq5+lumVf3lh" +
                "QaSC2Z64JGzmK4IfrjrtjLfENC+ThM1YC/mm/6bDU92EXxo0an3fkFh/6/36P0wJ7Sv6//Fu3vIP/gUF" +
                "kx8bgxAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
