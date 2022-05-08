/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        public RobotTrajectory(ref ReadBuffer b)
        {
            JointTrajectory = new TrajectoryMsgs.JointTrajectory(ref b);
            MultiDofJointTrajectory = new TrajectoryMsgs.MultiDOFJointTrajectory(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new RobotTrajectory(ref b);
        
        public RobotTrajectory RosDeserialize(ref ReadBuffer b) => new RobotTrajectory(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            JointTrajectory.RosSerialize(ref b);
            MultiDofJointTrajectory.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (JointTrajectory is null) BuiltIns.ThrowNullReference();
            JointTrajectory.RosValidate();
            if (MultiDofJointTrajectory is null) BuiltIns.ThrowNullReference();
            MultiDofJointTrajectory.RosValidate();
        }
    
        public int RosMessageLength => 0 + JointTrajectory.RosMessageLength + MultiDofJointTrajectory.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/RobotTrajectory";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "dfa9556423d709a3729bcef664bddf67";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VXTY/bNhC961cQ2EPswusFkiKHBXookKbdAkFTZNFLsDBoaSQxoUiFpNbr/vq+IfVh" +
                "aXfRAGlcw4Alcr7ezJshHZz8RHmw7rhrfOWvfrfKhNtxUXzi910YF7KwUHjX6aDe/PF2qdjw+q6w5e6R" +
                "ieyn//iTvfvw67VYRraIKPuNZEFO1PEn88EpU3286xEa2ZDPFirv+RUiLf/67xW3D0UKOAWYXYgPQZpC" +
                "ukI0FGQhgxSlReCqqsldaronDSXZtFSIuBuOLfktFG9r5QW+FRlyUuuj6DyEghW5bZrOqFwGEkEB66k+" +
                "NJURUrTSBZV3WjrIW1cow+KlQ27YOr6evnRkchI3b64hYzzlXVAI6AgLuSPpkVRsiqxDyl69ZIXs4vZg" +
                "L/FKFdI/OhehloGDpYfWkec4pb+Gjx8SuC1sIzkEL4UXq7i2w6tfCzhBCNTavBYrRP7+GGprYJDEvXRK" +
                "7jWx4RwZgNUXrPRifWLZRNNGGjuYTxYnH19j1ox2GdNljZppRu+7CgmEYOvsvSoguj9GI7lWZILQau8k" +
                "dxK0ksvs4i3nGELQihXBr/Te5goFKMRBhbonbKrGThX/UxfFlgDkXyRyP8mmFhG+pVyVilBUhIxqt9ar" +
                "oMCTjxsBlgBQwC5eZJ6TBkfj5t0dLNq5NJXgfLiLzOfknfgCnfco/wNzjwpm5s9aj74LFEt3CEE6Ylqz" +
                "sufsgtCISPq4ErtexK5noQXKLaBnWamtDK9/jAOgD+xkbYJzsjiDdbKe0GRFl7YiY3als80OBMDGmYr5" +
                "zLCOk4P6ycjcG6ZGymnP3sVEEOB73HBUkotDITL6iYINsP1iBnPloM7tj8ZImbElBlE8PC5xePR1OrG1" +
                "ImZeoht3Ce8ZD6dN1F8P3EwSUlv0zCKcgwJd0PC6KxgFiOLkkR2PZb6ains1L+lFGlt1T6NILE2mCvXA" +
                "rNHaCcU2MVkzpcTGQYzbXUTjc2pGY9vs6dPqmWqe59T6SmrN5sWyrDmSP1DspJBi1bXMvtcC9tZZRRbn" +
                "4ODndpACwlHDZ4nC6Hi2iekgh/48ngyGgZrWqUpFqk35Xro5KB/mXf7IhZm1+7f5mbPs7IPimRwPV4qx" +
                "R/3QS32l9hQORAjzYB8NiDhYS0cgfCtz3CCyvyInXiV9HQFmf3ZQcIaxOptmwHlA9sE8AZG5w3uL+Lk/" +
                "b+LQsQb3nYYkTyY7aUKxUA6qwLBNXEGSaCNUEIVFPozlVmjkZ5gkXB9YW7atHtmv+6JbVlnRttpuxKFG" +
                "fqMUH//xshavdyoXTK9iMQCjTdGD24hQvkzzLsacnKGEMDJke70VN6U42k4cGBAeXH+rjMfsEFe8/QRr" +
                "N3w49CbmCY2tjrR4Lys+eX3AlN+Ox6h4GJ+O49PfZyn1xLGnqm24T8fzZ1ZzfvsyEZST/K+AhqfDmXqV" +
                "B8gAa7hK+2n6zfHsnf1MDDJSzOMuagiXVT6dpKnizZ//BODPxNCrvcj03stl2T+LsLnSOw4AAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
