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
            b.SerializeArray(JointNames);
            b.SerializeArray(Limits);
            b.SerializeArray(LinkNames);
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
                "H4sIAAAAAAAACq2SP0/DQAzF93wKS11gQCyIAYmBCQnBBBtCkZs4rendOTpfqvLtcf41SVs2st09v+ef" +
                "fVnBEzjWBFLBt3BIChwgbQl2HMhj4gJSJMo0RQ6bz6++Kg/oSbPVqdtOni2jkBhJawmlmSBJl9hX9E4v" +
                "e+KUe93o7Ut7/9oZLb9PWEQ7Dju1CExLMoUgJUEdZc8lKai4JrEEhUriRNzaB+Ds8Z+/7O39+QH+mMaG" +
                "+NiygnVW3JBtJSRkw+NggO0IEgDX0hz3ZsMi1BhtuMZhHFZ2JbHzRnFQsqegZrweBpw9SGYNU2wIuJot" +
                "fIsKtSh33YbtrsWiTMhHIR8ES/D2A2AowePhzFc5wXR/19YcrdMlHqbLGcqEsScnBaefM4xRmGPg4aR+" +
                "0WiUeuDjEWzdqNp4Ktvfbk1ws6i+iIVFQY5i/x6naHNxxOvpzm0Lwrk8UC4cl0kXruwXqZinDp8DAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
