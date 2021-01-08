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
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestGoal Goal)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACrVUwWrbQBC9C/QPAzkkKdiB9mboLTTxoVCI72a8O5aWSLvqzsqq/r5vV3bcQA89NEZm" +
                "kTTz5s17M3oWthKpLUfFJrngO3fY99row1PgbvtIDY69s9VONOVH5UFdff3Pv7r6/vK0IU12qf5cONXV" +
                "Db0k9pajpV4SW05MxwDOrmklrjo5SYcs7gexVN6meRBd58xd65RwNeIlctfNNCqiUiAT+n70znASSq6X" +
                "dwA51XliGjgmZ8aOIxJCtM7n+GPkXgp+/qv8HMUboe3jBlFexYzJgdQMDBOF1fkGLxE8Op++fM4ZSNxN" +
                "YYV7aaD/GwNKLafMWH4NUTSTZd3kMp+WHteAh0iCQlbprjzb41bvCXXAQoZgWroD/R9zaoMHotCJo+ND" +
                "JxnZQAfA3uak2/s/oTP1DXn24YK/QF6L/AuuvwLntlYtzOuyBDo20BGRQwwnZxF7mAuK6Zz4RJi7yHGu" +
                "q5y2FAXItyw2wpBXvMHJqsE4OGFpcqmtK00xFyi+YFLr6sOm868bskzamTJpG8bO4ibEzHsZL4KrU+vg" +
                "TOkkbxBNrBTz8Cg6KeO0LdaXEYU07M/lYHc8YUqmVjy5ROhWNA8xRkT6IRGUz+kZVZcJmgTF38DpINgY" +
                "kCAjMTE8zJzeC31pwtmLPxAaHGFRuCpORxF7YPMKdtD5BjV07BI2U5UbKX6QDmLc0ZmlzTMLXZ/hy84s" +
                "EWDWj5pAj7CLCIMKZy8XFz/ex4fLV62uluVcPm6/ATJ2X30YBQAA";
                
    }
}
