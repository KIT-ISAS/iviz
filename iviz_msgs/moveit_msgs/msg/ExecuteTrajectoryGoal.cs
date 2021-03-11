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
                "H4sIAAAAAAAACsVXTW/cNhC9E/B/IOBD7GK9BpIiBwM9BEjTukDQtFn0EhgLrjSSmEikQlJeb39935D6" +
                "WtlGAqRxFgusKM7Xm3kz5Iq/7c6GjVMfKQvWHWQYH8WJ+OV//pyIt+9/u5KNvSUdto0v/eXC/4mYAkgC" +
                "f1ht5gF+5PV2FuZS4W1XB/36zzdLxYbfb3NbbO+Z+G5IvwDmRPxOKicnq/gjfHDalB9uepBGNeTFQucd" +
                "LyHS8q//fqH7kKeYU4gn4lS+D8rkyuWyoaByFZQsLGLXZUXuoqZbqqGlmpZyGXfDoSW/huKm0l7iW5Ih" +
                "p+r6IDsPoWBlZpumMzpTgWTQgDvXh6Y2UslWuaCzrlYO8tbl2rB44ZAeto6vp88dmYzk9esryBhPWRc0" +
                "AjrAQuZIeeQVm1J0yNqL56wgTjd7e4EllajA6FyGSgUOlu5aR57jVP4KPn5K4NawjewQvORensV3Wyz9" +
                "uYQThECtzSp5hsjfHUJlDQySvFVOq11NbDhDBmD1GSs9O59Z5rCvpFHGDuaTxcnH15g1o13GdFGhZjWj" +
                "912JBEKwdfZW5xDdHaKRrNZkgqz1zinuJ2gll+L0DecYQtCKFcGv8t5mGgXI5V6HqudsqsZW5z+sl2Jf" +
                "MEt/VUj/JJwaRfqWMl1oQl0RNQreWq+DBlU+rCSIAkwBu1ioLKMaNI2bNzewaI+lqQDtw00kP+dv5guM" +
                "3oEBd0w/ypmcr+p69J2jXnWHEJRDxVIFPScYnEZEysc3sffBAvCRhRYw18AuRFFbFV7+HMdAH9js3QRn" +
                "9vII1ux9QiPyLm1F0mwLZ5stOICNJ6vnI3ObS7pBWtKIZAYOsyOltefwYi5IsD5uOCrIxdEQef1AzQbk" +
                "fjGMuXhQ5yGA9kjJsQXGUTxILnCQ9KWa2TojJl9iHPcK7xkPp03UPx/omSRUbdE5i3D2GoxB29ddzijA" +
                "FacO7His9OVU38vjqp6m4VX1TIrcqsmUoRrINVqbsWwVk3WklAg5iHHT85RdsjMaW4uHj61HyvlUx9dX" +
                "sut4aiwrmyH/A8tmtZRnHY4JK19KGDwXJVkciIOjzSAFkKOGF4nF6Hu2iRmhhi49zMbDwE7rdInWRxxT" +
                "ypdu9tqH416/5wLkmdHj2/wcE+0HjItHspzGA3pt7NRE46lYOwp7IkS6t/fGRJywhSPQvlUZbhPin8iL" +
                "F0m/jhjFXx0UnGG4zqZJ8FQ4+3AeQskM4s0FBG7U6zh9rMH1pyHFI8pOmlDMtYMqYKwTY5AnWkkdZG6R" +
                "EmMDbDTqE0wSbhOsrdq2HnsgpYVfQ+WM1uV6JfcVUhyl+DYQ727xtqczySTLF5Mw2pQ9upUMxfM0+GLM" +
                "yRmqCCNDws/X8rqQB9vJPQPCg+svmfHIHeKKl6Fg7YpPid7EcUZjxyMt3quST2EfMO5R+P5ElHfj02F8" +
                "+veJqj0R7cGCo1MdX9VSBo/KzqvPE005z1/CJIan/ZM1Lc+SEdlwv/bTJDyGtHP2E0iFcjHRPC6ohnCD" +
                "5cNKmTL+HeB/BviHMTRtLzKtezkA/A/jmQ1p6Q4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
