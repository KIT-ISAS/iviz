/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryGoal")]
    public sealed class ExecuteTrajectoryGoal : IDeserializable<ExecuteTrajectoryGoal>, IGoal<ExecuteTrajectoryActionGoal>
    {
        // The trajectory to execute
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryGoal()
        {
            Trajectory = new RobotTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryGoal(RobotTrajectory Trajectory)
        {
            this.Trajectory = Trajectory;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryGoal(ref Buffer b)
        {
            Trajectory = new RobotTrajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryGoal(ref b);
        }
        
        ExecuteTrajectoryGoal IDeserializable<ExecuteTrajectoryGoal>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Trajectory.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            Trajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "054c09e62210d7faad2f9fffdad07b57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VXXWscNxR9F/Q/CPwQu6zXkJQ8GPpQSNO6EJo2pi/BLNqZOzNKZqSJpPFm++t7rjTf" +
                "tkmgzXZZ2NHofp17z73Sij/t3oZbpz5QFqw7yjA+CvHjf/wRb979ci0be0867Bpf+quVdzF5T/u/WW3m" +
                "0X3g9W6S+u6BxpuuDvrV76/Xmg2/3+W22D208Y2AfgGM+JVUTk5W8Uf44LQp39/1GI1qyIuVylteQqTl" +
                "X/+t4vYhTwGnAMWZfBeUyZXLZUNB5SooWVgErsuK3GVN91RDSTUt5TLuhmNLfgvF20p7iW9Jhpyq66Ps" +
                "PISClZltms7oTAWSQQPrXB+a2kglW+WCzrpaOchbl2vD4oVDbtg6vp4+dWQykjevriFjPGVd0AjoCAuZ" +
                "I+WRVGxK0SFlL56zgji7PdhLLKlE+kfnMlQqcLD0uXXkOU7lr+Hj+wRuC9tIDsFL7uV5fLfD0l9IOEEI" +
                "1NqskueI/O0xVNbAIMl75bTa18SGM2QAVp+x0rOLmWUTTRtl7GA+WZx8fI1ZM9plTJcValYzet+VSCAE" +
                "W2fvdQ7R/TEayWpNJsha753i5oNWcinOXnOOIQStWBH8Ku9tplGAXB50qHrCpmrsdP4/dVFsCUD+WSH3" +
                "k2xqEelbynShCUVFyKh2a70OGjx5v5FgCQAF7GKhsoxqcDRu3t3Bol1KUwHOh7vIfE7ezBfovEf5PzP3" +
                "KGdm/lTXo+8cxao7hKAcMa1Z2XN2QWhEpHx8E7texq5noRXKLaALUdRWhZc/xAHQBzZ7N8GZvVzAmr1P" +
                "aETepa3ImF3hbLMDAbBxomI+Ma3j5KB+MjL3hqmRctqzdzURJPgeNxwV5OJQiIx+pGADbL+awVw5qHP7" +
                "ozFSZmyBQRRPj0ucHn2dZrbOiZmX6MZdwnvGw2kT9S8GbiYJVVv0zCqcgwZd0PB1lzMKEMWpIzsey3w1" +
                "FfdqWdKzNLaqnkaRWDWZMlQDs0ZrM4ptYrIWSomNgxi3u4zGl9SMxrbi8dPqiWqe5tT6Smot5sW6rBmS" +
                "P1BsVkh53rXMvpcS9i5ESRbn4ODndpACwlHDi0RhdDzbxHRQQ38eZ4NhoKZ1utSRalO+124O2odllz9w" +
                "YRbt/u/8LFl28kHxRI6HK8XYo37opb5SewoHIoR5sA8GRByshSMQvlUZbhDir8iJF0m/jgDFHx0UnGGs" +
                "zqYZcBqQfTCPQGTu8N4qfu7Pmzh0rMF9pyHFk8lOmlDMtYMqMGwTV5Ak2kgdZG6RD2O5FRr1ESYJ1wfW" +
                "Vm1bj+yv+6JbVjmnbbndyEOF/EYpPv7jZS1e73QmmV75agBGm7IHt5GheJ7mXYw5OUMJYWTI9sVW3hTy" +
                "aDt5YEB4cP2tMh6zQ1zx9hOs3fDh0JtYJjS2OtLivSr55PUBU347HqPy8/h0HJ/+PkmpJ449Vm3DfTqe" +
                "P4ua8+rTRFBO8hcBDU+HE/UqD5AB1nCV9tP0W+LZO/uRGGSkmMdd1BAuq3w6KVPGmz//CcCfiaFXe5Fp" +
                "3csJ8Q+1AixyzA4AAA==";
                
    }
}
