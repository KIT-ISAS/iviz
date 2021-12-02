/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestActionGoal : IDeserializable<TestActionGoal>, IActionGoal<TestGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TestGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public TestActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestGoal();
        }
        
        /// Explicit constructor.
        public TestActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal TestActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestActionGoal(ref b);
        
        TestActionGoal IDeserializable<TestActionGoal>.RosDeserialize(ref Buffer b) => new TestActionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "348369c5b403676156094e8c159720bf";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwW7bMAy9+ysI9NB2QFJguwXYrWibw4ABzT1gJMYWJkueKMfz3+9JTtIO2GGH1TAg" +
                "SCYfH/me/CJsJVFXl4ZNdjF4d9j32urDc2S/faQWy97ZZieay1E9aL7+56f59vq8Ic12qf2yMLqh18zB" +
                "crLUS2bLmekYQdi1naSVl5N4JHE/iKX6Nc+D6BqJu84p4W0lSGLvZxoVQTmSiX0/Bmc4C2XXyx/5yHSB" +
                "mAZO2ZnRc0J8TNaFEn5M3EtBx6vyc5RghLaPG8QEFTNmB0IzEEwSVhdafKRmdCF/+VwSmpvdFFfYSoux" +
                "X4tT7jgXsvJrSKKFJ+sGNT4tza2BjeEIqlilu3q2x1bvCUVAQYZoOroD8+9z7mIAoNCJk+ODlwJsMAGg" +
                "3pak2/t3yIX2hgKHeIFfEN9q/AtsuOKWnlYdNPOlex1bDBCBQ4onZxF6mCuI8U5CJngtcZqbkrWUbG6e" +
                "yowRhKyqCFZWjcZBAEuTy12jORX0qkax5ge58a/3oVrrTJa0i6O32MRUKC9+Img5dQ6C1CbKdaGJlVIx" +
                "jKKJYqBt1btaEiPhcC4GkdMJ1pg6CeQyoVHRYlr4QvohEwaO7IKpi2smQekrNB0E9wMUyEjKDOUKo/fz" +
                "bRb+zl40wXhBD7LEtznTUcQe2PwAM4sMmHL0GXdQlVupIpAOYtzRmaXBMwNdn9GRdA4AqX7UDGaEW4eo" +
                "9UW/otxHS/dw+W01yyWsP6/fkbT/RPcEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
