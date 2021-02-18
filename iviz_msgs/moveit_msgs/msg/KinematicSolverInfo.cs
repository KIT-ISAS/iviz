/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/KinematicSolverInfo")]
    public sealed class KinematicSolverInfo : IDeserializable<KinematicSolverInfo>, IMessage
    {
        // A list of joints in the kinematic tree
        [DataMember (Name = "joint_names")] public string[] JointNames { get; set; }
        // A list of joint limits corresponding to the joint names
        [DataMember (Name = "limits")] public MoveitMsgs.JointLimits[] Limits { get; set; }
        // A list of links that the kinematics node provides solutions for
        [DataMember (Name = "link_names")] public string[] LinkNames { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public KinematicSolverInfo()
        {
            JointNames = System.Array.Empty<string>();
            Limits = System.Array.Empty<MoveitMsgs.JointLimits>();
            LinkNames = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public KinematicSolverInfo(string[] JointNames, MoveitMsgs.JointLimits[] Limits, string[] LinkNames)
        {
            this.JointNames = JointNames;
            this.Limits = Limits;
            this.LinkNames = LinkNames;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public KinematicSolverInfo(ref Buffer b)
        {
            JointNames = b.DeserializeStringArray();
            Limits = b.DeserializeArray<MoveitMsgs.JointLimits>();
            for (int i = 0; i < Limits.Length; i++)
            {
                Limits[i] = new MoveitMsgs.JointLimits(ref b);
            }
            LinkNames = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new KinematicSolverInfo(ref b);
        }
        
        KinematicSolverInfo IDeserializable<KinematicSolverInfo>.RosDeserialize(ref Buffer b)
        {
            return new KinematicSolverInfo(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(JointNames, 0);
            b.SerializeArray(Limits, 0);
            b.SerializeArray(LinkNames, 0);
        }
        
        public void Dispose()
        {
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
                size += 4 * JointNames.Length;
                foreach (string s in JointNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                foreach (var i in Limits)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * LinkNames.Length;
                foreach (string s in LinkNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/KinematicSolverInfo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cc048557c0f9795c392dd80f8bb00489";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62SMU/DQAyF90j9D5a6wIBYEAMSAxMSggk2hCI3cVvT3Dk6X6ry7/GlvZKjZSPb3fOz" +
                "P7/cHB6gY40gS/gU9lGBPcQ1wYY9OYzcQAxEs0pjYL96/9iX1R4d6aya//bbybF1aSQE0l58ay6IMvbc" +
                "VxysTrbEsXa60uunJDyPTpuwb1E279hv1JpgLOkUvLQEfZAtt6Sg0g2RxSssJUyokz9DV/f//FUvr493" +
                "8MdCtsbbmhVstOKKLBkfkX0K2hDTEuIBFzIcs7N1EXoMtt7QYTjEdiFh9AbpoGVHXs14mVec/JZZlaKL" +
                "YSDg5ST3NSr0ojwOzBkvxNqZUmelzkpq4uwxoG/B4e7UuuwE4+1NKjq6J7e4m9xOiX5ottRJw/HrlCYr" +
                "JY1RlJZyXNYO5MczWPqoOjhq00tcEFyV5efpsGmoo4Dn85qq9c+LTYynxpJzqmfWwnOet7RV3wbsJSa7" +
                "AwAA";
                
    }
}
