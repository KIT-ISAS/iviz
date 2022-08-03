/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapActionGoal : IDeserializable<GetMapActionGoal>, IMessage, IActionGoal<GetMapGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public GetMapGoal Goal { get; set; }
    
        public GetMapActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = GetMapGoal.Singleton;
        }
        
        public GetMapActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, GetMapGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public GetMapActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = GetMapGoal.Singleton;
        }
        
        public GetMapActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = GetMapGoal.Singleton;
        }
        
        public GetMapActionGoal RosDeserialize(ref ReadBuffer b) => new GetMapActionGoal(ref b);
        
        public GetMapActionGoal RosDeserialize(ref ReadBuffer2 b) => new GetMapActionGoal(ref b);
    
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
    
        public int RosMessageLength => 0 + Header.RosMessageLength + GoalId.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            GoalId.AddRos2MessageLength(ref c);
            Goal.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "nav_msgs/GetMapActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4b30be6cd12b9e72826df56b481f40e0";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwYrbMBC9+ysGctjdQlLa3gK9Lc3msFC6eyslTKSJLWpLrkaO67/vk+ykW+ihh24w" +
                "CMszb97Me5MHYSuRmnJUbJILvnXHQ6e1vt0Fbvf3VOM4OFvtJD1yny/LVfXxP/+qx6fdljTZufrDzGlF" +
                "T4m95Wipk8SWE9MpgLKrG4nrVs7SIom7XiyVr2nqRTdIfG6cEp5avERu24kGRVAKZELXDd4ZTkLJdfJH" +
                "PjKdJ6aeY3JmaDkiPkTrfA4/Re4ko+NR+TGIN0L7+y1ivIoZkgOhCQgmCqvzNT5SNTifPrzPCbSir1+C" +
                "vvtWrZ7HsMa91FDgyoJSwymzlp99FM2EWbco9mbucoMimJKgnFW6LXcHvOodoRq4SB9MQ7do4fOUmuAB" +
                "KHTm6PjYSgY2GAVQb3LSzd0LZF+gPftwgZ8Rf9f4F1h/xc09rRuI1+Yx6FBjkgjsYzg7i9DjVEBM68Qn" +
                "gu0ix6nKWXPJavUpDxtByCrS4GTVYByUsDS61FSaYkYvsmSXvpIt/7oaxWMLWdImDK3FS4hS+iqNQMux" +
                "cRCkNJH3hkZWitk5iiayk/ZF7+JNjIT9UgwixzOsMTbiySVCo6LZvfCFdH0iDBzZGVNn14yC0ldoOsop" +
                "c2EyEhNDuczo5XwX/s5eNMF4QW/KRa5zppOIPbL5DmYWGTDl0CYsoyrXUkQg7cW4kzNzgwsD3SzoeVPm" +
                "AJDqBk1gRlg/RG0u+mXlXkk6z+dFtOsfWFX9Aty4sNv5BAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
