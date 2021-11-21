/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/RobotTrajectory")]
    public sealed class RobotTrajectory : IDeserializable<RobotTrajectory>, IMessage
    {
        [DataMember (Name = "joint_trajectory")] public TrajectoryMsgs.JointTrajectory JointTrajectory;
        [DataMember (Name = "multi_dof_joint_trajectory")] public TrajectoryMsgs.MultiDOFJointTrajectory MultiDofJointTrajectory;
    
        /// <summary> Constructor for empty message. </summary>
        public RobotTrajectory()
        {
            JointTrajectory = new TrajectoryMsgs.JointTrajectory();
            MultiDofJointTrajectory = new TrajectoryMsgs.MultiDOFJointTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public RobotTrajectory(TrajectoryMsgs.JointTrajectory JointTrajectory, TrajectoryMsgs.MultiDOFJointTrajectory MultiDofJointTrajectory)
        {
            this.JointTrajectory = JointTrajectory;
            this.MultiDofJointTrajectory = MultiDofJointTrajectory;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal RobotTrajectory(ref Buffer b)
        {
            JointTrajectory = new TrajectoryMsgs.JointTrajectory(ref b);
            MultiDofJointTrajectory = new TrajectoryMsgs.MultiDOFJointTrajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RobotTrajectory(ref b);
        }
        
        RobotTrajectory IDeserializable<RobotTrajectory>.RosDeserialize(ref Buffer b)
        {
            return new RobotTrajectory(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/RobotTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dfa9556423d709a3729bcef664bddf67";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXXYsbNxR9H9j/INiH2MXrhaTkYaEPhTTtFkJTYvISFiPP3JlRMiNNJM163V/fc6X5" +
                "3l0aaOsYg2ek+33OvZK9lZ8p9cae9rUr3PXvRmm/GxbFZ37f+2EhWSq8ayuv3vzxdqlY8/o+M/n+kYmL" +
                "5Kf/+HORvPvw641YxraI6SL5jWRGVpThJ3HeKl18uuuS1LImlyx03vMrRBr+df9f6M5nMeYY4kVyKT54" +
                "qTNpM1GTl5n0UuQGsauiJHtV0T1V0JJ1Q5kIu/7UkNtCcVcqJ/AtSJOVVXUSrYOQNyI1dd1qlUpPwiuk" +
                "O9WHptJCikZar9K2khbyxmZKs3huUR62jq+jry3plMTtmxvIaEdp6xUCOsFCakk61BWbImlRtVcvWSG5" +
                "3B3NFV6pAAKDc+FL6TlYemgsOY5Tuhv4+CEmt4VtVIfgJXNiFdb2eHVrAScIgRqTlmKFyN+ffGk0DJK4" +
                "l1bJQ0VsOEUFYPUFK71YTyxz2DdCS21689Hi6ONbzOrBLud0VQKzirN3bYECQrCx5l5lED2cgpG0UqS9" +
                "qNTBSu4naEWXyeVbrjGEoBUQwa90zqQKAGTiqHzZcTaisVfZd+ul0BfM0l8kyj8Kx0YRrqFU5YqAK6IG" +
                "4I1xyitQ5dNGgCjIyWMXLzJNqQJNw+bdHSyauTTloL2/C+Tn+k18gdEHMOCB6UcZk/Pnqhp8Z8CrahGC" +
                "tEAsIui4wOA0IpIurITeBwvARxZapLlF7kmSV0b61z+GMdAFNlkb05ksztKarMdskqyNW4E0+9yaeg8O" +
                "YONseD4ztxnSHcoSRyQzsJ8dsawdhxdzQYD1YcNSTjaMhsDrJzDrM3eLYczgQZ2HANojFsfkGEfhILnC" +
                "QdJBNbG1IiZfZBz3Cu9pB6d10F/39IwSsjLonEU4RwXGoO2rNuMswBUrT+x4QPp6xPd6juplHF5lx6TA" +
                "rYp04cueXIO1Ccs2oVgzpUjIXoybnqfskp3B2DZ5+th6Bs5zHV/fyK751Fgim6L+PcsmWIpVi2PCiNcC" +
                "BtdJQQYHYu9o10shyUHDJZHF6Hu2iRkh+y49TcZDz05jVYHWRxxjyZdujsr5ea8/cgHyTOjx7/zMifYd" +
                "xsUzVY7jAb02dGqk8QjWgfyRCJEezaMxESZsbgm0b2SK20TyMfDiVdSvQo7Jny0UrOZ0rYmT4Fx5duE8" +
                "lSUziDcXKXCj3obpYzSuPzVJHlFm1IRipixUkcY2MgZ1oo1QXmQGJdHGw0Ytv8Ak4TbB2rJpqqEHYll4" +
                "GSor2hbbjTiWKHGQ4ttAuLuF255KBZMsW0zCYFN02W2Ez1/GwRdijs6AIoz0BV9vxW0uTqYVR04ID7a7" +
                "ZIYjt48rXIa8MRs+JToT84qGjkdZnJMFn8LOY9wD+O5EFA/D02l4+utMaI9EexJwdKrlq1qs4Ax2fvs6" +
                "0pTr/E85Jf3T8WxNy7NkyKy/X7txEs5TOljzBaQCXEw0hwuqJtxg+bCSugh/B/ifAf5h9E3biYzvnRwS" +
                "/Bsh3v0QVw4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
