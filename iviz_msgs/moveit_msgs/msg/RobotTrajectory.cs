/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/RobotTrajectory")]
    public sealed class RobotTrajectory : IDeserializable<RobotTrajectory>, IMessage
    {
        [DataMember (Name = "joint_trajectory")] public TrajectoryMsgs.JointTrajectory JointTrajectory { get; set; }
        [DataMember (Name = "multi_dof_joint_trajectory")] public TrajectoryMsgs.MultiDOFJointTrajectory MultiDofJointTrajectory { get; set; }
    
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
        public RobotTrajectory(ref Buffer b)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (JointTrajectory is null) throw new System.NullReferenceException(nameof(JointTrajectory));
            JointTrajectory.RosValidate();
            if (MultiDofJointTrajectory is null) throw new System.NullReferenceException(nameof(MultiDofJointTrajectory));
            MultiDofJointTrajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += JointTrajectory.RosMessageLength;
                size += MultiDofJointTrajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/RobotTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dfa9556423d709a3729bcef664bddf67";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VX32/bNhB+F7D/gUAeag+OA7RDHwLsYUDXLQOKdWjQlyIwaOkksaVIlaTiaH/9viMl" +
                "2VISrMA2zzBgibxf3913Rzo4+ZnyYF2/a3zlr36zyoTbaVF85vddmBa+y8JC412ng3rz+9ulZsPru8KW" +
                "u8c2sh//5U/27sMv12IZ2iKk7FeSBTlRx5/MB6dM9eluwGhkQz5bqLznV4i0/Ov/q7h9KFLAKcDsQnwI" +
                "0hTSFaKhIAsZpCgtAldVTe5S0z1pKMmmpULE3dC35LdQvK2VF/hWZMhJrXvReQgFK3LbNJ1RuQwkggLW" +
                "U31oKiOkaKULKu+0dJC3rlCGxUuH3LB1fD197cjkJG7eXEPGeMq7oBBQDwu5I+mRVGyKrEPKXr1khezi" +
                "9mAv8UoV0j85F6GWgYOlh9aR5zilv4aP7xO4LWwjOQQvhReruLbDq18LOEEI1Nq8FitE/r4PtTUwSOJe" +
                "OiX3mthwjgzA6gtWerE+sWyiaSONHc0ni0cf32LWTHYZ02WNmmlG77sKCYRg6+y9KiC676ORXCsyQWi1" +
                "dxJ8ZK3kMrt4yzmGELRiRfArvbe5QgEKcVChHgibqrFTxf/URbElAPlnidwfZVOLCN9SrkpFKCpCRrVb" +
                "61VQ4MmnjQBLAChgFy8yz0mDo3Hz7g4W7VyaSnA+3EXmc/JOfIHOe5T/gblHBTPzJ60n3wWKpTuEIB0x" +
                "rVnZc3ZBaEQkfVyJXS9i17PQAuUW0LOs1FaG1z/EATAEdrJ2hHOyOIN1sp7QZEWXtiJjdqWzzQ4EwMaZ" +
                "ivnMtI6Tg4bJyNwbp0bK6cDexUQQ4HvccFSSi0MhMvqJgo2w/WIGc+Wgzu2PxkiZsSUGUTw9LnF6DHU6" +
                "sbUiZl6iG3cJ7xkPp03UX4/cTBJSW/TMIpyDAl3Q8LorGAWI4mTPjqcyXx2LezUv6UUaW/VAo0gsTaYK" +
                "9cisydoJxTYxWTOlxMZRjNtdRONzakZj2+zp0+qZap7n1PpGas3mxbKsOZI/UuykkGLVtcy+1wL21llF" +
                "Fufg6Od2lALCScNnicLoeLaJ6SDH/uxPBsNITetUpSLVjvleujkoH+Zd/siFmbX7P/MzZ9nZB8UzOR6v" +
                "FFOP+rGXhkrtKRyIEObBPhoQcbCWjkD4Vua4QWQfIydeJX0dAWZ/dFBwhrE6m2bAeUAOwTwBkbnDe4v4" +
                "uT9v4tCxBvedhiRPJnvUhGKhHFSBYZu4giTRRqggCot8GMut0MgvMEm4PrC2bFs9sV8PRbessqJttd2I" +
                "Q438Rik+/uNlLV7vVC6YXsViAEabYgC3EaF8meZdjDk5QwlhZMz2eituStHbThwYEB7ccKuMx+wYV7z9" +
                "BGs3fDgMJuYJja2OtHgvKz55fcCU307HqHiYnvrp6c+zlPrIsaeqbbhPp/NnVnN++3okKCf5bwGNT4cz" +
                "9SoPkBHWeJX2x+k3x7N39gsxyEgxj7uoIVxW+XSSpoo3f/4TgD8TY68OIsf3QS7L/gLAmXaiPQ4AAA==";
                
    }
}
