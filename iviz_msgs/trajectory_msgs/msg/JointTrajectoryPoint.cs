using System.Runtime.Serialization;

namespace Iviz.Msgs.trajectory_msgs
{
    public sealed class JointTrajectoryPoint : IMessage
    {
        // Each trajectory point specifies either positions[, velocities[, accelerations]]
        // or positions[, effort] for the trajectory to be executed.
        // All specified values are in the same order as the joint names in JointTrajectory.msg
        
        public double[] positions { get; set; }
        public double[] velocities { get; set; }
        public double[] accelerations { get; set; }
        public double[] effort { get; set; }
        public duration time_from_start { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JointTrajectoryPoint()
        {
            positions = System.Array.Empty<double>();
            velocities = System.Array.Empty<double>();
            accelerations = System.Array.Empty<double>();
            effort = System.Array.Empty<double>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointTrajectoryPoint(double[] positions, double[] velocities, double[] accelerations, double[] effort, duration time_from_start)
        {
            this.positions = positions ?? throw new System.ArgumentNullException(nameof(positions));
            this.velocities = velocities ?? throw new System.ArgumentNullException(nameof(velocities));
            this.accelerations = accelerations ?? throw new System.ArgumentNullException(nameof(accelerations));
            this.effort = effort ?? throw new System.ArgumentNullException(nameof(effort));
            this.time_from_start = time_from_start;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal JointTrajectoryPoint(Buffer b)
        {
            this.positions = BuiltIns.DeserializeStructArray<double>(b, 0);
            this.velocities = BuiltIns.DeserializeStructArray<double>(b, 0);
            this.accelerations = BuiltIns.DeserializeStructArray<double>(b, 0);
            this.effort = BuiltIns.DeserializeStructArray<double>(b, 0);
            this.time_from_start = BuiltIns.DeserializeStruct<duration>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new JointTrajectoryPoint(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.positions, b, 0);
            BuiltIns.Serialize(this.velocities, b, 0);
            BuiltIns.Serialize(this.accelerations, b, 0);
            BuiltIns.Serialize(this.effort, b, 0);
            BuiltIns.Serialize(this.time_from_start, b);
        }
        
        public void Validate()
        {
            if (positions is null) throw new System.NullReferenceException();
            if (velocities is null) throw new System.NullReferenceException();
            if (accelerations is null) throw new System.NullReferenceException();
            if (effort is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += 8 * positions.Length;
                size += 8 * velocities.Length;
                size += 8 * accelerations.Length;
                size += 8 * effort.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "trajectory_msgs/JointTrajectoryPoint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "f3cd1e1c4d320c79d6985c904ae5dcd3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1WPsQ7CMAxE93yFJVbEhNgZWJjZUIVMeoGgtEaxi+DvcYsEZYl07+zceUE7jleyyjdE" +
                "k/qiu+TeSO+IOWUoIdsV1bFmy9LrcUkPFImuMAqOEQWVJ7NpwoLkfxopSbWG/CX/ap5lQmcQnoiDoV35" +
                "7raUb3ZLDy6DV+AKyv20rNzBA1pvxDqR29S3d67j0H6Uh2/EqtNLCKkI22Z9bH7FZux3zgz+nTXjn2tC" +
                "O3wsstzhlKp0JzV2I7wBGsNsNFIBAAA=";
                
    }
}
