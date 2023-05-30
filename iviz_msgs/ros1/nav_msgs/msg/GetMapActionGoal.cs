/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapActionGoal : IHasSerializer<GetMapActionGoal>, IMessage, IActionGoal<GetMapGoal>
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
            BuiltIns.ThrowIfNull(GoalId, nameof(GoalId));
            GoalId.RosValidate();
            BuiltIns.ThrowIfNull(Goal, nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = GoalId.AddRos2MessageLength(size);
            size = Goal.AddRos2MessageLength(size);
            return size;
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
    
        public Serializer<GetMapActionGoal> CreateSerializer() => new Serializer();
        public Deserializer<GetMapActionGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GetMapActionGoal>
        {
            public override void RosSerialize(GetMapActionGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GetMapActionGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GetMapActionGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GetMapActionGoal msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(GetMapActionGoal msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<GetMapActionGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GetMapActionGoal msg) => msg = new GetMapActionGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GetMapActionGoal msg) => msg = new GetMapActionGoal(ref b);
        }
    }
}
