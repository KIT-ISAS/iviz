/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryGoal : IDeserializable<ExecuteTrajectoryGoal>, IGoal<ExecuteTrajectoryActionGoal>
    {
        // The trajectory to execute
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory;
    
        /// Constructor for empty message.
        public ExecuteTrajectoryGoal()
        {
            Trajectory = new RobotTrajectory();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryGoal(RobotTrajectory Trajectory)
        {
            this.Trajectory = Trajectory;
        }
        
        /// Constructor with buffer.
        internal ExecuteTrajectoryGoal(ref Buffer b)
        {
            Trajectory = new RobotTrajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryGoal(ref b);
        
        ExecuteTrajectoryGoal IDeserializable<ExecuteTrajectoryGoal>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Trajectory.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            Trajectory.RosValidate();
        }
    
        public int RosMessageLength => 0 + Trajectory.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "054c09e62210d7faad2f9fffdad07b57";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VXTW/cNhC981cQ8CF2sV4DSZGDgR4KpGldIGjaGLkExoIrjSQmEqmQlDfbX983pL5t" +
                "IwHabBcLrCjO15t5M+SKv+zehlunPlIWrDvKMD4K8dN//BFv3v16LRt7TzrsGl/6q5V3MXlP+79bbebR" +
                "feT1Ljyt8Karg371x+u1YsPvd7ktdg9MfCeYX4EifiOVk5NV/BE+OG3KD3c9QqMa8mKl8paXEGn513+v" +
                "uH3IU8ApQHEm3wVlcuVy2VBQuQpKFhaB67Iid1nTPdVQUk1LuYy74diS30LxttJe4luSIafq+ig7D6Fg" +
                "ZWabpjM6U4Fk0MA614emNlLJVrmgs65WDvLW5dqweOGQG7aOr6fPHZmM5M2ra8gYT1kXNAI6wkLmSHkk" +
                "FZtSdEjZi+esIM5uD/YSSyqR/tG5DJUKHCx9aR15jlP5a/j4IYHbwjaSQ/CSe3ke3+2w9BcSThACtTar" +
                "5Dkif3sMlTUwSPJeOa32NbHhDBmA1Wes9OxiZtlE00YZO5hPFicf32LWjHYZ02WFmtWM3nclEgjB1tl7" +
                "nUN0f4xGslqTCbLWe6e4k6CVXIqz15xjCEErVgS/ynubaRQglwcdqp6wqRo7nf9PXRRbApB/Ucj9JJta" +
                "RPqWMl1oQlERMqrdWq+DBk8+bCRYAkABu1ioLKMaHI2bd3ewaJfSVIDz4S4yn5M38wU671H+L8w9ypmZ" +
                "P9f16DtHseoOIShHTGtW9pxdEBoRKR/fxK6XsetZaIVyC+hCFLVV4eWPcQD0gc3eTXBmLxewZu8TGpF3" +
                "aSsyZlc42+xAAGycqJhPDOs4OaifjMy9YWqknPbsXU0ECb7HDUcFuTgUIqMfKdgA269mMFcO6tz+aIyU" +
                "GVtgEMXD4xKHR1+nma1zYuYlunGX8J7xcNpE/YuBm0lC1RY9swrnoEEXNHzd5YwCRHHqyI7HMl9Nxb1a" +
                "lvQsja2qp1EkVk2mDNXArNHajGKbmKyFUmLjIMbtLqPxJTWjsa14/LR6opqnObW+kVqLebEua4bkDxSb" +
                "FVKedy2z76WEvQtRksU5OPi5HaSAcNTwIlEYHc82MR3U0J/H2WAYqGmdLnWk2pTvtZuD9mHZ5Q9cmEW7" +
                "/zs/S5adfFA8kePhSjH2qB96qa/UnsKBCGEe7IMBEQdr4QiEb1WGG4R4HznxIunXEaD4s4OCM4zV2TQD" +
                "TgOyD+YRiMwd3lvFz/15E4eONbjvNKR4MtlJE4q5dlAFhm3iCpJEG6mDzC3yYSy3QqM+wSTh+sDaqm3r" +
                "kf11X3TLKue0LbcbeaiQ3yjFx3+8rMXrnc4k0ytfDcBoU/bgNjIUz9O8izEnZyghjAzZvtjKm0IebScP" +
                "DAgPrr9VxmN2iCvefoK1Gz4cehPLhMZWR1q8VyWfvD5gym/HY1R+GZ+O49PfJyn1xLHHqm24T8fzZ1Fz" +
                "Xn2eCMpJ/iqg4elwol7lATLAGq7Sfpp+Szx7Zz8Rg4wU87iLGsJllU8nZcp48+c/AfgzMfRqLzKtezkh" +
                "/gHqm2rqyg4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
