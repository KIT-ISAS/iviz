/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryActionGoal : IDeserializable<ExecuteTrajectoryActionGoal>, IActionGoal<ExecuteTrajectoryGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ExecuteTrajectoryGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public ExecuteTrajectoryActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ExecuteTrajectoryGoal();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ExecuteTrajectoryGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        public ExecuteTrajectoryActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ExecuteTrajectoryGoal(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ExecuteTrajectoryActionGoal(ref b);
        
        public ExecuteTrajectoryActionGoal RosDeserialize(ref ReadBuffer b) => new ExecuteTrajectoryActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) BuiltIns.ThrowNullReference(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "36f350977c67bc94e8cd408452bad0f0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VXS2/bRhC+81cs4EPsQpaBpMjBQA8FnMQuEDRtjFwCQ1iRQ2pjcpfZXVpRf32/mSUp" +
                "UZabAq0Vw4BI7rznm8deky7Iq5X8ZDqPxtnaLBdNqMLFO6frmytV4WdhiuzNN8q7SLdef6E8Or/hcznN" +
                "fvmf/7L3H99dqhCLZMh1Mu9EfYzaFtoXqqGoCx21Kh2sN9WK/HlND1SDSTctFUpO46alMAfj7coEhf+K" +
                "LHld1xvVBRBFp3LXNJ01uY6komlowg9OY5VWrfbR5F2tPeidL4xl8tLrhlg6/gN97cjmpG6uLkFjA8fK" +
                "wKANJOSedDC2wqHKOmPjq5fMkJ3crt05XqlCDkblKq50ZGPpW+spsJ06XELHT8m5OWQjOAQtRVCn8m2B" +
                "13CmoAQmUOvylTqF5R82ceUsBJJ60N7oZU0sOEcEIPUFM70425FsRbTV1g3ik8Stjn8j1o5y2afzFXJW" +
                "s/ehqxBAELbePZgCpMuNCMlrQzYqAM9rv8mYK6nMTt5yjEEELskIfnUILjdIQKHWJq6yED1Ll2wwTp8J" +
                "jQeLQ6DVG6vCynV1gRfnSfwSR5DL9cogIeIEl4ta66A8AybACQbQjeRbIImQaNsrQ5L9A6CxXpFVJio4" +
                "SoFBC1xQ00aFgIObZYaEmjVB9ShaLalkW7TKyUeNzLFFu/Ht7TfFkBOEF+ZtWMkYZ1USFUud38OyAhwA" +
                "ZVdH1GAIuiJJggot5aY0eXKwtyDMe+lcIIkARjVdiLBMoepANR/yx5l7ptQ17oFMTHk72MWy7E+3dHH7" +
                "EaEeHo9h1J72bKs9nf/m0CV2rPvC74v4NMN7JMhc/f52n7Hh74vClYtHIp7Jze+4kl1PJlDCwue73kOL" +
                "kg7ZHssHfgVJy7/hB9ktRgDWbzRX9jbAYtQIdvRwFAfca10wXNHh80xhKKCuIk7xovOcaowkOby7g0Q3" +
                "paYSJRzvZNBJT9nBqOMyogRo6SO/1vVOoT3ouuOCRgcwqVsHbqaYX7BIB/kicVYSZyba83IO17OsrJ2O" +
                "r3+WkPeG7XzburPzceLWzvfkTVZ06Uj60aL0rlmgH+HgSMl8ojz6bpiwOHZkhDnFtB9WewsAN0U58FSi" +
                "+/K4lAZ7IGGD22EP9Zw5sPO0xxxMkXElOrGU6znKtc/TjqxTYuQluPFQ5DMboLQR/rMBm4lC1w4tds+c" +
                "tQFcMN/rriAZOt6j80PZmOaLbXIvpik9SfNm1cNIgFWTrTAK+k+jtB2IzSRYE6aExoGMp7sS4VNoirB5" +
                "drg/PJHNH9Mn/smYISf7ac0R/AFiO4lUp13L6HutIO8sq8hh7R303A5U8HDkCP1AR8WzzCWP/j6Fm53G" +
                "MEDTeVMZgdo23vtq1ibEaZU/UmEn5f7f9ExRdvRG8USMhxvEWKNhqKU+U0uKa8KSFtfuUYOQxlpiY0Nk" +
                "dI6FJ/skmHiV+GtxMPujA4O37Kt3qQccx8nemAMuMnb4bM9+NS6sWIexUpDmzuS2nGAsjCdZYecJK7yG" +
                "zniFLRziYR2XQqPvIZJwW5Bts23rEf11n3THLKc0r+aztAQLFW+LcjeT2xx2ToZXsdcARabqnZupWL5M" +
                "/U5sTsqQQt5m+2ifzdVNqTauw/4LH/Dg+0ukjNnBLrnsROdmPBx6EdOASqmP27Gx2MQ1pvMwBdW38Wkz" +
                "Pv11lFRvMXYo25brdJw/k5zz29ctQDnI33VoeFofqVa5gQxuDTfnsO1+U3+W3t3zncoKxAKunpZwN+Xp" +
                "pG0lF32+84f5WKs9yfa9p8uyvwEgek0kRBEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
