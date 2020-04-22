
namespace Iviz.Msgs.trajectory_msgs
{
    public sealed class JointTrajectory : IMessage
    {
        public std_msgs.Header header;
        public string[] joint_names;
        public JointTrajectoryPoint[] points;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "trajectory_msgs/JointTrajectory";
    
        public IMessage Create() => new JointTrajectory();
    
        public int GetLength()
        {
            int size = 12;
            size += header.GetLength();
            for (int i = 0; i < joint_names.Length; i++)
            {
                size += joint_names[i].Length;
            }
            for (int i = 0; i < points.Length; i++)
            {
                size += points[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public JointTrajectory()
        {
            header = new std_msgs.Header();
            joint_names = new string[0];
            points = new JointTrajectoryPoint[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out joint_names, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out points, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(joint_names, ref ptr, end, 0);
            BuiltIns.SerializeArray(points, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
