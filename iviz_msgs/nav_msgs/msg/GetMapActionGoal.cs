/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GetMapActionGoal : IDeserializable<GetMapActionGoal>, IActionGoal<GetMapGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public GetMapGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public GetMapActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = GetMapGoal.Singleton;
        }
        
        /// Explicit constructor.
        public GetMapActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, GetMapGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal GetMapActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = GetMapGoal.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMapActionGoal(ref b);
        
        GetMapActionGoal IDeserializable<GetMapActionGoal>.RosDeserialize(ref Buffer b) => new GetMapActionGoal(ref b);
    
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
    
        public int RosMessageLength => 0 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4b30be6cd12b9e72826df56b481f40e0";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwW7bMAy9+ysI9NB2QDJguwXYrWiaQ4EB7T1gJMYWJkueKMfz3+9JTtIO2GGH1TAg" +
                "SCYfH/me/CRsJVFXl4ZNdjF4d9j32urnbWS/e6AWy97ZZiv5mYdyWI+ab//5aZ5fthvSbJfqTwunG3rJ" +
                "HCwnS71ktpyZjhGUXdtJWnk5iUcS94NYql/zPIiukfjaOSW8rQRJ7P1MoyIoRzKx78fgDGeh7Hr5Ix+Z" +
                "LhDTwCk7M3pOiI/JulDCj4l7Keh4VX6OEozQ7mGDmKBixuxAaAaCScLqQouP1Iwu5K9fSkJz8zrFFbbS" +
                "YvDX4pQ7zoWs/BqSaOHJukGNT0tza2BjOIIqVumunu2x1XtCEVCQIZqO7sD8+5y7GAAodOLk+OClABtM" +
                "AKi3Jen2/h1yob2hwCFe4BfEtxr/AhuuuKWnVQfNfOlexxYDROCQ4slZhB7mCmK8k5AJbkuc5qZkLSWb" +
                "m8cyYwQhqyqClVWjcRDA0uRy12hOBb2qUcz5QW78642o1jqTJe3i6C02MRXKi58IWk6dgyC1iXJdaGKl" +
                "VAyjaKIYaFf1rpbESDici0HkdII1pk4CuUxoVLSYFr6QfsiEgSO7YOrimklQ+gpNB8H9AAUykjJDucLo" +
                "/Xybhb+zF00wXtCDLPFtznQUsQc2P8DMIgOmHH3GHVTlVqoIpIMYd3RmafDMQNdndCSdA0CqHzWDGeHW" +
                "IWp90a8o90HSBT6dRbv+t5rmN4oowiPwBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
