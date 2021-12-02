/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class JointTrajectory : IDeserializable<JointTrajectory>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "points")] public JointTrajectoryPoint[] Points;
    
        /// Constructor for empty message.
        public JointTrajectory()
        {
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<JointTrajectoryPoint>();
        }
        
        /// Explicit constructor.
        public JointTrajectory(in StdMsgs.Header Header, string[] JointNames, JointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        /// Constructor with buffer.
        internal JointTrajectory(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            JointNames = b.DeserializeStringArray();
            Points = b.DeserializeArray<JointTrajectoryPoint>();
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new JointTrajectoryPoint(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new JointTrajectory(ref b);
        
        JointTrajectory IDeserializable<JointTrajectory>.RosDeserialize(ref Buffer b) => new JointTrajectory(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeArray(Points);
        }
        
        public void RosValidate()
        {
            if (JointNames is null) throw new System.NullReferenceException(nameof(JointNames));
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) throw new System.NullReferenceException($"{nameof(JointNames)}[{i}]");
            }
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i] is null) throw new System.NullReferenceException($"{nameof(Points)}[{i}]");
                Points[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(JointNames);
                size += BuiltIns.GetArraySize(Points);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "trajectory_msgs/JointTrajectory";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTWvcMBC961cM5JCkdLfQlh4Weij0Iy0UAtlbWMysNLYVZMmV5E387/tkb3ed0kMP" +
                "rTHI0sy8eTPz5BthI5HaaVEpR+ub+x09BOtz5bmTpL6V723kB9E5xPG2bOHSlzUp9f4fP+r73ZcNpWyq" +
                "LjXp1c3M7ILuMnvD0VAnmQ1npjqAuG1aiSsnB3EI4q4XQ5M1j72kNQK3rU2EtxEvkZ0baUhwyoF06LrB" +
                "W81ZKFvUuoxHpPXE1HPMVg+OI/xDNNYX9zqiNwUdb5Ifg3gt9PXjBj4+iR6yBaERCDoKJzQVRlIDWvbm" +
                "dQlQF9vHsMJWGrT/lJxyy7mQlac+Sio8OW2Q48Vc3BrYaI4gi0l0NZ1V2KZrQhJQkD7olq7A/HbMbfAA" +
                "FDpwtLx3UoA1OgDUyxJ0eb1ALrQ35NmHX/Az4jnH38D6E26padViZq5Un4YGDYRjH8PBGrjuxwlEOys+" +
                "k7P7yHFUJWpOqS4+lx7DCVHTRLBySkFbDMDQo83tUbDzNCpr/pca80n9syj/dCVQ8idG78++8xWh1Iu2" +
                "tRUMFZQx7T4kmy10cv+SoBIUlGHFhrUWB41Oxt0OiFD40ltqaD7vJuWX5i1yQc57jP+paE9MUeYHhxtx" +
                "zG0wLDeAAkeMax5fKt2FoMGI03Qy3XpIAGIsTr9VuUbpStUucH73dvoBHIktzs7lLA6flbU4n6tRZphN" +
                "k2KqOoauggBgUD8BWSTt+p0EAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
