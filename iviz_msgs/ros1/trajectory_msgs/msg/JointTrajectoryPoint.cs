/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract]
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
    
        public JointTrajectoryPoint()
        {
            Positions = System.Array.Empty<double>();
            Velocities = System.Array.Empty<double>();
            Accelerations = System.Array.Empty<double>();
            Effort = System.Array.Empty<double>();
        }
        
        public JointTrajectoryPoint(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Positions);
            b.DeserializeStructArray(out Velocities);
            b.DeserializeStructArray(out Accelerations);
            b.DeserializeStructArray(out Effort);
            b.Deserialize(out TimeFromStart);
        }
        
        public JointTrajectoryPoint(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Positions);
            b.DeserializeStructArray(out Velocities);
            b.DeserializeStructArray(out Accelerations);
            b.DeserializeStructArray(out Effort);
            b.Deserialize(out TimeFromStart);
        }
        
        public JointTrajectoryPoint RosDeserialize(ref ReadBuffer b) => new JointTrajectoryPoint(ref b);
        
        public JointTrajectoryPoint RosDeserialize(ref ReadBuffer2 b) => new JointTrajectoryPoint(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Positions);
            b.SerializeStructArray(Velocities);
            b.SerializeStructArray(Accelerations);
            b.SerializeStructArray(Effort);
            b.Serialize(TimeFromStart);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Positions);
            b.SerializeStructArray(Velocities);
            b.SerializeStructArray(Accelerations);
            b.SerializeStructArray(Effort);
            b.Serialize(TimeFromStart);
        }
        
        public void RosValidate()
        {
            if (Positions is null) BuiltIns.ThrowNullReference();
            if (Velocities is null) BuiltIns.ThrowNullReference();
            if (Accelerations is null) BuiltIns.ThrowNullReference();
            if (Effort is null) BuiltIns.ThrowNullReference();
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.Align4(c);
            c += 4;  // Positions length
            c = WriteBuffer2.Align8(c);
            c += 8 * Positions.Length;
            c += 4;  // Velocities length
            c = WriteBuffer2.Align8(c);
            c += 8 * Velocities.Length;
            c += 4;  // Accelerations length
            c = WriteBuffer2.Align8(c);
            c += 8 * Accelerations.Length;
            c += 4;  // Effort length
            c = WriteBuffer2.Align8(c);
            c += 8 * Effort.Length;
            c += 8;  // TimeFromStart
            return c;
        }
    
        public const string MessageType = "trajectory_msgs/JointTrajectoryPoint";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f3cd1e1c4d320c79d6985c904ae5dcd3";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1WPsQ7CMAxE93yFJVbEhNgZWJjZUIVMeoGgtEaxi+DvcYsEZYl07+zceUE7jleyyjdE" +
                "k/qiu+TeSO+IOWUoIdsV1bFmy9LrcUkPFImuMAqOEQWVJ7NpwoLkfxopSbWG/CX/ap5lQmcQnoiDoV35" +
                "7raUb3ZLDy6DV+AKyv20rNzBA1pvxDqR29S3d67j0H6Uh2/EqtNLCKkI22Z9bH7FZux3zgz+nTXjn2tC" +
                "O3wsstzhlKp0JzV2I7wBGsNsNFIBAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
