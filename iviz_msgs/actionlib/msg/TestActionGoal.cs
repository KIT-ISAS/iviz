/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestActionGoal")]
    public sealed class TestActionGoal : IDeserializable<TestActionGoal>, IActionGoal<TestGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TestGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestActionGoal(ref b);
        }
        
        TestActionGoal IDeserializable<TestActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new TestActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "348369c5b403676156094e8c159720bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrbQBC9C/wPAz4kKdiB9mboLTTxoVCI72a8O5aWrnbVnZVV/33fruwkhR56aCoE" +
                "YqWZN2/mvdGTsJVEXX00bLKLwbvDvtdW7x8j++0DtXjsnW12orm8qi8Wzed/fC2ar8+PG9Js5+pPldOi" +
                "WdJz5mA5Weols+XMdIzg7NpO0srLSTyyuB/EUv2az4PoGom7zinhbiVIYu/PNCqCciQT+34MznAWyq6X" +
                "3/KR6QIxDZyyM6PnhPiYrAsl/Ji4l4KOW+XHKMEIbR82iAkqZswOhM5AMElYXWjxkZrRhfzpY0lolrsp" +
                "rnCUFpN/KU6541zIys8hiRaerBvU+DA3twY2piOoYpVu67s9jnpHKAIKMkTT0S2YfzvnLgYACp04OT54" +
                "KcAGEwDqTUm6uXuDXGhvKHCIV/gZ8bXG38CGF9zS06qDZr50r2OLASJwSPHkLEIP5wpivJOQCXZLnM5N" +
                "yZpLNssvZcYIQlZVBE9WjcZBAEuTy12jORX0qkZx57sZ8o9LUWy5Qw+zdNrF0VscYiqsZ0sR5Jw6B01q" +
                "H2VpaGKlVDyj6KN4aFslr67EVDhcqkHndII7pk4CuUzoVbT4FtaQfsiEmSO7YOpsnElQ+gWaDoIVAQUy" +
                "kjJDvMLo7Ygv/J29yoIJgx6Uia+jpqOIPbD5DmYWGfDl6DPWUJVbqTqQDmLc0Zm5wQsDXV/Qy47MASDV" +
                "j5rBjLB4iFpfJUTUf1Dv/vr7WjTzKs5/sV9wHr3AAQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
