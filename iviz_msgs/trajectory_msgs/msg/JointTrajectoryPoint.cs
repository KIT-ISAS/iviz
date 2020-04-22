
namespace Iviz.Msgs.trajectory_msgs
{
    public sealed class JointTrajectoryPoint : IMessage
    {
        // Each trajectory point specifies either positions[, velocities[, accelerations]]
        // or positions[, effort] for the trajectory to be executed.
        // All specified values are in the same order as the joint names in JointTrajectory.msg
        
        public double[] positions;
        public double[] velocities;
        public double[] accelerations;
        public double[] effort;
        public duration time_from_start;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "trajectory_msgs/JointTrajectoryPoint";
    
        public IMessage Create() => new JointTrajectoryPoint();
    
        public int GetLength()
        {
            int size = 24;
            size += 8 * positions.Length;
            size += 8 * velocities.Length;
            size += 8 * accelerations.Length;
            size += 8 * effort.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public JointTrajectoryPoint()
        {
            positions = new double[0];
            velocities = new double[0];
            accelerations = new double[0];
            effort = new double[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out positions, ref ptr, end, 0);
            BuiltIns.Deserialize(out velocities, ref ptr, end, 0);
            BuiltIns.Deserialize(out accelerations, ref ptr, end, 0);
            BuiltIns.Deserialize(out effort, ref ptr, end, 0);
            BuiltIns.Deserialize(out time_from_start, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(positions, ref ptr, end, 0);
            BuiltIns.Serialize(velocities, ref ptr, end, 0);
            BuiltIns.Serialize(accelerations, ref ptr, end, 0);
            BuiltIns.Serialize(effort, ref ptr, end, 0);
            BuiltIns.Serialize(time_from_start, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "f3cd1e1c4d320c79d6985c904ae5dcd3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAClWPsY7CQAxE+5X4B0u0iOp0/RXXUF+HImQ2s7BoE6O1g46/xwlSEpqV5o29M97SL8cr" +
                "WeUbokl90l1yb6R3xJwylJDtiupYs2Xp9bijB4pEVxgFx4iCypPZNGFL8jmNlKRaQ/6Sf7XOMqEzCP+I" +
                "g6Hd++5PKXN2Sw8ug1fgCsr9tKzcwQNab8Q6kdvUt3eu49BhlH9zxL7TSwipCNv317FZiq3Ycs4Kfpy1" +
                "4u9rQju8LbLc4ZSqdCc1dmMTXu0FwIBTAQAA";
                
    }
}
