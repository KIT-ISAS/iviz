/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingActionGoal : IDeserializable<AveragingActionGoal>, IMessage, IActionGoal<AveragingGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public AveragingGoal Goal { get; set; }
    
        public AveragingActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new AveragingGoal();
        }
        
        public AveragingActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, AveragingGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public AveragingActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        public AveragingActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        public AveragingActionGoal RosDeserialize(ref ReadBuffer b) => new AveragingActionGoal(ref b);
        
        public AveragingActionGoal RosDeserialize(ref ReadBuffer2 b) => new AveragingActionGoal(ref b);
    
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
    
        public int RosMessageLength => 8 + Header.RosMessageLength + GoalId.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = GoalId.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 8; // Goal
            return c;
        }
    
        public const string MessageType = "actionlib_tutorials/AveragingActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwWobMRC96ysGfEhSiEPbW6CHQmjiQ6E0uZUSxtJ4V1QrbTVau/v3fdI6bgI99NCY" +
                "hUXamTdv3pvxnbCTTH17GbbFpxj89nHQTq9uE4fNDXV4PXpnPu4lc+djV+/brfnwn3/m8/3tNWlxC4G7" +
                "hdaK7gtHx9nRIIUdF6ZdAmvf9ZIvg+wlIImHURy1r2UeRddIfOi9Ep5OIsiHMNOkCCqJbBqGKXrLRaj4" +
                "QV7kI9NHYho5F2+nwBnxKTsfa/gu8yAVHY/Kz0miFdrcXCMmqtipeBCagWCzsEIwfCQz+Vjev6sJtKJv" +
                "X5O+/W5WD4d0iXvpYMKJBZWeS2Utv8YsWgmzXqPYm6XLNYpAJUE5p3Te7h5x1AtCNXCRMdmeztHCl7n0" +
                "KQJQaM/Z8zZIBbaQAqhnNens4hlybNCRY3qCXxD/1PgX2HjCrT1d9jAvVBl06qAkAsec9t4hdDs3EBu8" +
                "xEKYvMx5NjVrKWlWn6rYCEJWswZvVk3WwwlHB196oyVX9GZLHdRXGsu/bkebsSNZ0j5NweGQsrS+WiPw" +
                "8tB7GNKaqHtDB1bKdXIUTdRJ2jS/22xCEo7HYjA5Y+uQL5F8ITQqWqcXcyHDWAiCI7ti6jI1B0HpEzRt" +
                "ZVe5MFnJheFcZfRc3yN/7548gbygN9ciJ51pJ+K2bH+AmUMGhnIKBcuoyp00E0hHsX7n7dLgkYGuj+h1" +
                "U5YAkBomLWBGWD9ErZ/8q869unVlgjkecl29+DszZtlOcZ2o2YXE9ZTZ+UnNb3rWZD4lBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
