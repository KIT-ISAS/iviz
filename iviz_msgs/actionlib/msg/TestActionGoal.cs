/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract (Name = "actionlib/TestActionGoal")]
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
                
    }
}
