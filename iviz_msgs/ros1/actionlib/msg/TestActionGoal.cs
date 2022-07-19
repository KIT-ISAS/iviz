/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestActionGoal : IDeserializableRos1<TestActionGoal>, IDeserializableRos2<TestActionGoal>, IMessageRos1, IMessageRos2, IActionGoal<TestGoal>
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
        public TestActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestGoal(ref b);
        }
        
        /// Constructor with buffer.
        public TestActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestGoal(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TestActionGoal(ref b);
        
        public TestActionGoal RosDeserialize(ref ReadBuffer b) => new TestActionGoal(ref b);
        
        public TestActionGoal RosDeserialize(ref ReadBuffer2 b) => new TestActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference();
            GoalId.RosValidate();
            if (Goal is null) BuiltIns.ThrowNullReference();
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + GoalId.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            GoalId.AddRos2MessageLength(ref c);
            Goal.AddRos2MessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "348369c5b403676156094e8c159720bf";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwYrbMBC9+ysGctjdQrLQ3gK9Ld3NoVDY3MNEmtiisuRq5Lj++z7JSbqFHnroGoOQ" +
                "PPPmzbwnvwhbSdTVpWGTXQzeHQ+9tvr4HNnvnqjFcnC22YvmclQPms//+Wm+vj5vSbNdar8sjFb0mjlY" +
                "TpZ6yWw5M50iCLu2k7T2chaPJO4HsVS/5nkQ3SBx3zklvK0ESez9TKMiKEcyse/H4Axnoex6+SMfmS4Q" +
                "08ApOzN6ToiPybpQwk+JeynoeFV+jBKM0O5pi5igYsbsQGgGgknC6kKLj9SMLuRPH0tCs9pPcY2ttBj7" +
                "rTjljnMhKz+HJFp4sm5R48PS3AbYGI6gilW6r2cHbPWBUAQUZIimo3sw/zbnLgYACp05OT56KcAGEwDq" +
                "XUm6e3iDHCp04BCv8Avi7xr/AhtuuKWndQfNfOlexxYDROCQ4tlZhB7nCmK8k5AJXkuc5qZkLSWb1Zcy" +
                "YwQhqyqClVWjcRDA0uRy12hOBb2qUaz5Tm78632o1rqQJe3i6C02MUntqzYCLafOQZDaRLkuNLFSKoZR" +
                "NFEMtKt6V0tiJBwuxSByOsMaUyeBXCY0KlpMC19IP2TCwJFdMHVxzSQofYOmo5wKFyYjKTOUK4zezvfC" +
                "39mrJhgv6M2lyG3OdBKxRzbfwcwiA6YcfcYdVOVWqgikgxh3cmZp8MJANxf0ckGWAJDqR81gRrh1iNpc" +
                "9SvKvbd0j9ffVrNcwvrz+gWRtP9E9wQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
