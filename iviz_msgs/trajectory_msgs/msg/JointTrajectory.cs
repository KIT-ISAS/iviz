/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = "trajectory_msgs/JointTrajectory")]
    public sealed class JointTrajectory : IDeserializable<JointTrajectory>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "points")] public JointTrajectoryPoint[] Points;
    
        /// <summary> Constructor for empty message. </summary>
        public JointTrajectory()
        {
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<JointTrajectoryPoint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointTrajectory(in StdMsgs.Header Header, string[] JointNames, JointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        /// <summary> Constructor with buffer. </summary>
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JointTrajectory(ref b);
        }
        
        JointTrajectory IDeserializable<JointTrajectory>.RosDeserialize(ref Buffer b)
        {
            return new JointTrajectory(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames, 0);
            b.SerializeArray(Points, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "trajectory_msgs/JointTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTYvbMBC9C/IfBnLY3dKk0JYeAj0U+rEtFBY2tyWYiTSOtciSK8nZ9b/vk72buKWH" +
                "HlpjkKWZefNm5snXwkYiNeOiUo7WH+52dB+sz5XnVpL6Vr63ke9F5xCHm7KFS1fWpBbq/T9+Fur77ZcN" +
                "pWyqNh3Sq+uR20It6TazNxwNtZLZcGaqA7jbQyNx5eQoDlHcdmJotOahk7RG4LaxifAexEtk5wbqE5xy" +
                "IB3atvdWcxbKFuXO4xFpPTF1HLPVveMI/xCN9cW9jmhPQceb5EcvXgt9/biBj0+i+2xBaACCjsIJfYWR" +
                "VI+uvXldAtRy+xBW2MoBEzglp9xwLmTlsYuSCk9OG+R4MRW3Bja6I8hiEl2OZxW26YqQBBSkC7qhSzC/" +
                "GXITPACFjhwt750UYI0OAPWiBF1czZAL7Q159uEZfkI85/gbWH/CLTWtGszMlepTf0AD4djFcLQGrvth" +
                "BNHOis/k7D5yHFSJmlKq5efSYzghapwIVk4paIsBGHqwuXnS7DSNypr/J8h8ugKTLv90L4pKPzHaf3ae" +
                "LgqlTrStrWCuYI2BdyHZbCGVu5cEoaCmDCs2rLU4yHQ07nZAhMjn3lJD9nk3ir/0b5YLit5DAY9FfmKK" +
                "OD84XIqn3Abzcj0ocMTEpgmm0mBoGow4jSfj3YcKoMfi9FuZa9SuVO0C53dvx9/AE7HZ2bmc2eEvZc3O" +
                "p2qU6SfTKJqqjqGtoAEYFuoneXdohKQEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
