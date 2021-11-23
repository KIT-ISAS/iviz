/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class KinematicSolverInfo : IDeserializable<KinematicSolverInfo>, IMessage
    {
        // A list of joints in the kinematic tree
        [DataMember (Name = "joint_names")] public string[] JointNames;
        // A list of joint limits corresponding to the joint names
        [DataMember (Name = "limits")] public MoveitMsgs.JointLimits[] Limits;
        // A list of links that the kinematics node provides solutions for
        [DataMember (Name = "link_names")] public string[] LinkNames;
    
        /// Constructor for empty message.
        public KinematicSolverInfo()
        {
            JointNames = System.Array.Empty<string>();
            Limits = System.Array.Empty<MoveitMsgs.JointLimits>();
            LinkNames = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public KinematicSolverInfo(string[] JointNames, MoveitMsgs.JointLimits[] Limits, string[] LinkNames)
        {
            this.JointNames = JointNames;
            this.Limits = Limits;
            this.LinkNames = LinkNames;
        }
        
        /// Constructor with buffer.
        internal KinematicSolverInfo(ref Buffer b)
        {
            JointNames = b.DeserializeStringArray();
            Limits = b.DeserializeArray<MoveitMsgs.JointLimits>();
            for (int i = 0; i < Limits.Length; i++)
            {
                Limits[i] = new MoveitMsgs.JointLimits(ref b);
            }
            LinkNames = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new KinematicSolverInfo(ref b);
        
        KinematicSolverInfo IDeserializable<KinematicSolverInfo>.RosDeserialize(ref Buffer b) => new KinematicSolverInfo(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(JointNames, 0);
            b.SerializeArray(Limits, 0);
            b.SerializeArray(LinkNames, 0);
        }
        
        public void RosValidate()
        {
            if (JointNames is null) throw new System.NullReferenceException(nameof(JointNames));
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) throw new System.NullReferenceException($"{nameof(JointNames)}[{i}]");
            }
            if (Limits is null) throw new System.NullReferenceException(nameof(Limits));
            for (int i = 0; i < Limits.Length; i++)
            {
                if (Limits[i] is null) throw new System.NullReferenceException($"{nameof(Limits)}[{i}]");
                Limits[i].RosValidate();
            }
            if (LinkNames is null) throw new System.NullReferenceException(nameof(LinkNames));
            for (int i = 0; i < LinkNames.Length; i++)
            {
                if (LinkNames[i] is null) throw new System.NullReferenceException($"{nameof(LinkNames)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.GetArraySize(JointNames);
                size += BuiltIns.GetArraySize(Limits);
                size += BuiltIns.GetArraySize(LinkNames);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/KinematicSolverInfo";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cc048557c0f9795c392dd80f8bb00489";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62SP0/DQAzF93wKSywwIBbEgMTAhIRggg2hyE3c1vTuHJ0vVfn2OH+a5GjZyHZnv+ef" +
                "X+4CHsGxJpA1fAmHpMAB0pZgx4E8Jq4gRaJCU+Sw+fgcusqAnrS4+K22k2fzqCRG0kZCbSJI0jsOHYPS" +
                "y544lV43evPc3b/0QvMfHDJrx2GnZoEpJ1MIUhM0UfZck4KKaxNLUFhLnIk7+QhcPPzzV7y+Pd3DH9vY" +
                "Eu9bVrDJihuyVEJCDl3EBtitIAFwJe2Umy2L0GC05VqHcYzsUmKvjeKgZk9BTXg1Lrj4IYUNTLEl4PUi" +
                "8C0qNKLcTxvTXYlZWaE8FsqxYA7eHgCGGjweTnRrJ5jubrueSTpf4mG+XKDMGHtyUnH6PsE4FpYYNj7v" +
                "zwYdSwPwdASLG1VbT3X37FYE11n3WSysKnIU8WxCy2I5Pc6O7lSWES7LI2WmOE+aqYofqZinDp8DAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
