/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class JointTrajectoryPoint : IDeserializable<JointTrajectoryPoint>, IMessage
    {
        // Each trajectory point specifies either positions[, velocities[, accelerations]]
        // or positions[, effort] for the trajectory to be executed.
        // All specified values are in the same order as the joint names in JointTrajectory.msg
        [DataMember (Name = "positions")] public double[] Positions;
        [DataMember (Name = "velocities")] public double[] Velocities;
        [DataMember (Name = "accelerations")] public double[] Accelerations;
        [DataMember (Name = "effort")] public double[] Effort;
        [DataMember (Name = "time_from_start")] public duration TimeFromStart;
    
        /// Constructor for empty message.
        public JointTrajectoryPoint()
        {
            Positions = System.Array.Empty<double>();
            Velocities = System.Array.Empty<double>();
            Accelerations = System.Array.Empty<double>();
            Effort = System.Array.Empty<double>();
        }
        
        /// Explicit constructor.
        public JointTrajectoryPoint(double[] Positions, double[] Velocities, double[] Accelerations, double[] Effort, duration TimeFromStart)
        {
            this.Positions = Positions;
            this.Velocities = Velocities;
            this.Accelerations = Accelerations;
            this.Effort = Effort;
            this.TimeFromStart = TimeFromStart;
        }
        
        /// Constructor with buffer.
        public JointTrajectoryPoint(ref ReadBuffer b)
        {
            Positions = b.DeserializeStructArray<double>();
            Velocities = b.DeserializeStructArray<double>();
            Accelerations = b.DeserializeStructArray<double>();
            Effort = b.DeserializeStructArray<double>();
            TimeFromStart = b.Deserialize<duration>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new JointTrajectoryPoint(ref b);
        
        public JointTrajectoryPoint RosDeserialize(ref ReadBuffer b) => new JointTrajectoryPoint(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Positions);
            b.SerializeStructArray(Velocities);
            b.SerializeStructArray(Accelerations);
            b.SerializeStructArray(Effort);
            b.Serialize(TimeFromStart);
        }
        
        public void RosValidate()
        {
            if (Positions is null) throw new System.NullReferenceException(nameof(Positions));
            if (Velocities is null) throw new System.NullReferenceException(nameof(Velocities));
            if (Accelerations is null) throw new System.NullReferenceException(nameof(Accelerations));
            if (Effort is null) throw new System.NullReferenceException(nameof(Effort));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += 8 * Positions.Length;
                size += 8 * Velocities.Length;
                size += 8 * Accelerations.Length;
                size += 8 * Effort.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "trajectory_msgs/JointTrajectoryPoint";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "f3cd1e1c4d320c79d6985c904ae5dcd3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1WPsQ7CMAxE93yFJVbEhNgZWJjZUIVMeoGgtEaxi+DvcYsEZYl07+zceUE7jleyyjdE" +
                "k/qiu+TeSO+IOWUoIdsV1bFmy9LrcUkPFImuMAqOEQWVJ7NpwoLkfxopSbWG/CX/ap5lQmcQnoiDoV35" +
                "7raUb3ZLDy6DV+AKyv20rNzBA1pvxDqR29S3d67j0H6Uh2/EqtNLCKkI22Z9bH7FZux3zgz+nTXjn2tC" +
                "O3wsstzhlKp0JzV2I7wBGsNsNFIBAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
