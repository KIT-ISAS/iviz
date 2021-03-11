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
                "H4sIAAAAAAAACq2SP0/DQAzF90j5Dpa6wIBYEAMSAxMSggk2hCI3cVrT3Dk6X6ry7XH+NUlbNrLdPb/n" +
                "n31ZwRNUrBGkhG9hHxXYQ9wS7NiTw8g5xECUaAzsN59ffVXm0ZEmq1O3nRxbRi4hkNbiCzNBlC6xr+id" +
                "TvbEMXO60duX9v61M1p+n7CIrtjv1CIwLskUvBQEdZA9F6SgUjWRxSuUEibi1j4Ap8njP39p8vb+/AB/" +
                "zJPaHB9bVrDmihuyxfiIbITsjbGdQjzgWprj6mxehBqDzddUGIatXUnovEEqKNiRVzNeDzPO3iSxhjE0" +
                "BFzOdr5FhVqUu27DgtdiUSZko5ANgiU4+wfQF+DwcOYrK8F4f9fWHK3TJR6myxnKhLGnSnKOP2cYozDH" +
                "wMNJ/aLRKPXAxyPYulG1cVS0f96a4GZRfREL85wqCv17nKLNxRGvpzu3LQjn8kC5cFwmXbjS5Bd/C9qD" +
                "owMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
