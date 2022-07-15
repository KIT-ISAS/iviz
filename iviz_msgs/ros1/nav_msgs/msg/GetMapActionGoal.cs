/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapActionGoal : IDeserializable<GetMapActionGoal>, IMessageRos1, IActionGoal<GetMapGoal>
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
        public GetMapActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = GetMapGoal.Singleton;
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMapActionGoal(ref b);
        
        public GetMapActionGoal RosDeserialize(ref ReadBuffer b) => new GetMapActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public int RosMessageLength => 0 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/GetMapActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4b30be6cd12b9e72826df56b481f40e0";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwYrbMBC9+ysGctjdQlJob4HelmZzWCjs3sNEmtiisuRq5Lj++z7JSbqFHnroGoOQ" +
                "PPPmzbwnPwlbSdTVpWGTXQzeHQ+9tvpxF9nvH6nFcnC22Ul+5qEc1qPmy39+mueX3ZY026X608JpRS+Z" +
                "g+VkqZfMljPTKYKyaztJay9n8UjifhBL9WueB9ENEl87p4S3lSCJvZ9pVATlSCb2/Ric4SyUXS9/5CPT" +
                "BWIaOGVnRs8J8TFZF0r4KXEvBR2vyo9RghHaP24RE1TMmB0IzUAwSVhdaPGRmtGF/PlTSWhWr1NcYyst" +
                "Bn8rTrnjXMjKzyGJFp6sW9T4sDS3ATaGI6hile7r2QFbfSAUAQUZounoHsy/zbmLAYBCZ06Oj14KsMEE" +
                "gHpXku4e3iCHCh04xCv8gvi7xr/Ahhtu6WndQTNfutexxQAROKR4dhahx7mCGO8kZILbEqe5KVlLyWb1" +
                "tcwYQciqimBl1WgcBLA0udw1mlNBr2oUc76TG/96I6q1LmRJuzh6i01MUvuqjUDLqXMQpDZRrgtNrJSK" +
                "YRRNFAPtq97VkhgJh0sxiJzOsMbUSSCXCY2KFtPCF9IPmTBwZBdMXVwzCUrfoOkop8KFyUjKDOUKo7fz" +
                "vfB39qoJxgt6cylymzOdROyRzXcws8iAKUefcQdVuZUqAukgxp2cWRq8MNDNBb1ckCUApPpRM5gRbh2i" +
                "Nlf9inLvJF3g80W023+raX4BiijCI/AEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
