/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class RobotTrajectory : IDeserializable<RobotTrajectory>, IMessage
    {
        [DataMember (Name = "joint_trajectory")] public TrajectoryMsgs.JointTrajectory JointTrajectory;
        [DataMember (Name = "multi_dof_joint_trajectory")] public TrajectoryMsgs.MultiDOFJointTrajectory MultiDofJointTrajectory;
    
        /// Constructor for empty message.
        public RobotTrajectory()
        {
            JointTrajectory = new TrajectoryMsgs.JointTrajectory();
            MultiDofJointTrajectory = new TrajectoryMsgs.MultiDOFJointTrajectory();
        }
        
        /// Explicit constructor.
        public RobotTrajectory(TrajectoryMsgs.JointTrajectory JointTrajectory, TrajectoryMsgs.MultiDOFJointTrajectory MultiDofJointTrajectory)
        {
            this.JointTrajectory = JointTrajectory;
            this.MultiDofJointTrajectory = MultiDofJointTrajectory;
        }
        
        /// Constructor with buffer.
        internal RobotTrajectory(ref Buffer b)
        {
            JointTrajectory = new TrajectoryMsgs.JointTrajectory(ref b);
            MultiDofJointTrajectory = new TrajectoryMsgs.MultiDOFJointTrajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new RobotTrajectory(ref b);
        
        RobotTrajectory IDeserializable<RobotTrajectory>.RosDeserialize(ref Buffer b) => new RobotTrajectory(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            JointTrajectory.RosSerialize(ref b);
            MultiDofJointTrajectory.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (JointTrajectory is null) throw new System.NullReferenceException(nameof(JointTrajectory));
            JointTrajectory.RosValidate();
            if (MultiDofJointTrajectory is null) throw new System.NullReferenceException(nameof(MultiDofJointTrajectory));
            MultiDofJointTrajectory.RosValidate();
        }
    
        public int RosMessageLength => 0 + JointTrajectory.RosMessageLength + MultiDofJointTrajectory.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/RobotTrajectory";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "dfa9556423d709a3729bcef664bddf67";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXXW/bNhR9168gkIfag+MA7dCHAHsY0HXLgGIdGuylCAxaupLYSqRKUnG0X79zSX0n" +
                "wQp09QwDlsj7fc69pL2Vnyj1xnaH2hXu6nejtL8dF8Unfj/4cSFZK7xrK6/e/PF2rVjz+iEz+eGRieSn" +
                "//iTvPvw67VYR7aKKPmNZEZWlOEncd4qXXy86zPUsiaXrFTe8ytEGv513ytu57MYcAwwuRAfvNSZtJmo" +
                "yctMeilyg8BVUZK9rOieKijJuqFMhF3fNeT2ULwtlRP4FqTJyqrqROsg5I1ITV23WqXSk/AKuc71oam0" +
                "kKKR1qu0raSFvLGZ0iyeW9SGrePr6EtLOiVx8+YaMtpR2nqFgDpYSC1Jh6JiUyQtSvbqJSskF7cnc4lX" +
                "KlD+0bnwpfQcLD00lhzHKd01fPwQk9vDNopD8JI5sQlrB7y6rYAThECNSUuxQeTvO18aDYMk7qVV8lgR" +
                "G05RAVh9wUovtjPLHPa10FKbwXy0OPn4GrN6tMs5XZbArOLsXVuggBBsrLlXGUSPXTCSVoq0F5U6Wsmd" +
                "BK3oMrl4yzWGELQCIviVzplUAYBMnJQve8JGNA4q+5+6KLQEUv5FovaTbGwR4RpKVa4IoCJkoN0Yp7wC" +
                "Tz7uBFiChDx28SLTlCpwNGze3cGiWUpTDs77u8B8Lt7MF+h8BPwPzD3KmJk/V9XoOwNYVYsQpAVcET7H" +
                "1QWhEZF0YSV0PSgAMrLQKss9Uk+SvDLSv/4xDIA+sNnalM5scZHWbD1mk2Rt3AqMOeTW1AcQABtnAvOZ" +
                "YR0mB/WTkbk3TI1Y0569q4kgwPewYSknG4ZCYPQTgA1pu9UMZuSgzu2PxoiVMTkGUTg8LnF49DjNbG2I" +
                "mRfpxl3Ce9rBaR30twM3o4SsDHpmFc5JgS5o+KrNOAsQxcqOHY8wX03gXi0hvYhjq+xpFIhVkS58OTBr" +
                "tDaj2C4Ua6EU2TiIcbvzfF1TMxjbJ0+fVs+geZ5T6yuptZgXa1hTFH+g2AxIsWlxOhjxWsDeNinI4Bwc" +
                "/NwOUshw1ECWgcLoeLaJ6SCH/uxmg2GgprGqQNMjjqneazcn5fyyyx+5AHNm3Pg2P0uWnX1QPFPj4Uox" +
                "9mgk8ITUkfyJCGGezKMBEQZrbgmEb2SKG0TyV+DEq6hfhQSTP1soWM25WhNnwHmS7IN5IkXmDu+t4uf+" +
                "vAlDx2jcd2qSPJnMpAnFTFmoIod95AqKRDuhvMgM6qENt0ItP8Mk4frA2rJpqpH9sSa8DJUN7Yv9TpxK" +
                "1DdI8fEfLmvheqdSwfTKVgMw2BR9cjvh85dx3oWYozNACCNDtbd7cZOLzrTixAnhwfa3ynDMDnGF2483" +
                "ZseHQ29iWdDQ6iiLc7Lgk9d5THmg3p+C4mF86sanv88C9cSxp9BGg1q+mMXyLTDnty8TQbnI/5pQ3j+d" +
                "ztSrPECGtIartJum3zKfozWfQScAxRRzuItqwmWVTyepi3Dz5z8B+DMx9GovMr33cknyD4uwudI7DgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
