/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MultiDOFJointTrajectory : IDeserializable<MultiDOFJointTrajectory>, IMessage
    {
        // The header is used to specify the coordinate frame and the reference time for the trajectory durations
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A representation of a multi-dof joint trajectory (each point is a transformation)
        // Each point along the trajectory will include an array of positions/velocities/accelerations
        // that has the same length as the array of joint names, and has the same order of joints as 
        // the joint names array.
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "points")] public MultiDOFJointTrajectoryPoint[] Points;
    
        /// Constructor for empty message.
        public MultiDOFJointTrajectory()
        {
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<MultiDOFJointTrajectoryPoint>();
        }
        
        /// Explicit constructor.
        public MultiDOFJointTrajectory(in StdMsgs.Header Header, string[] JointNames, MultiDOFJointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        /// Constructor with buffer.
        internal MultiDOFJointTrajectory(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            JointNames = b.DeserializeStringArray();
            Points = b.DeserializeArray<MultiDOFJointTrajectoryPoint>();
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new MultiDOFJointTrajectoryPoint(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MultiDOFJointTrajectory(ref b);
        
        MultiDOFJointTrajectory IDeserializable<MultiDOFJointTrajectory>.RosDeserialize(ref Buffer b) => new MultiDOFJointTrajectory(ref b);
    
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
        [Preserve] public const string RosMessageType = "trajectory_msgs/MultiDOFJointTrajectory";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ef145a45a5f47b77b7f5cdde4b16c942";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WS4/bNhC+81cM4EPswusFkiKHBXookKbdAkFTxMilKAxaGklMJFIhqXXUX99vqJft" +
                "TR9Am13sgSLnPd984xXtK6aKdc6eTKAucE7RUWg5M0VPEa+Zcz43VkemwuuGSds8PXgu2LPNmKLBdeF8" +
                "uo5ef+AsOt9T3nkdjbNB/TS4GDwptaLvod56DmxjEiFXkKamq6O5yXH+4IyN57bWrLOK2nSNSLW82QCn" +
                "TdLfwOYPi4SunS2vwzmZuiZjs7rLJQvS3uteHLcumBTn7QPXLsOZw63OMq55SmAFYzpSpUOyGqQQNdsy" +
                "VjRezdaG0C0kwjYV60IJxUQhJjEkEigZ53O9wdhOqRC9seVvvw+Ph/So3kiVXv3y+me528/5vZVPiKYK" +
                "BKW++5//1Jt3P95RiPmhCWW4HVqK2N9FJKl9Tg1HneuoExQqU1bsb2pGSaGkmxbQSq+xbznsoLiv0Ej8" +
                "l2xR57ruZ/xlrmk6azIBnYDrQh+aBs2jVvtosq7W/hFGxTr+A3/qEkDvX91BxgbOumgQUC8w8KwDiotH" +
                "Uh1K9uK5KKjV/uRu8Mkl+jQ7H9qPYPmzwFbi1OEOPr4ZktvBNorD8JIHWqe7Az7DhuAEIXDrgM41In/b" +
                "xwqAl5Y/aG/0sWYxnKECsPpMlJ5tzixL2HfAhXWT+cHi4uPfmLWzXcnppkLPask+dCUKCMHWuweTQ/Q4" +
                "zn1tMJxUm6PXvldpxpNLtXqdeCBK+1JHZB5DwOCgATnGLFYjcIduHEz+tdC4TPcAyr8bjYkhrkkmAxVM" +
                "hHdGK7Tu0HVHLwn2NqpkB3xPfvaTFOZt1sDMCahBisnmEZxAI6H0oweD+kxE6bwp0TfEsUz/tZuTCTLS" +
                "Cy09doFROGOq/+bnkvPUxN8JMofCu+YABPj4tbr5FzWeqGLeGAOdLp06cjwxI8yTe0QFYBiLEwO8rc7A" +
                "DOp9wsSLQb9OCapfOyh4K7l6N2ykp0lyDOYLKQp25O0qfuG1+8REzoLHGtayJ92iCcXceKgih92AFRSJ" +
                "t2Qi5Q71sE5GodEfYZJBC6Kt27ae0T/URK6hsuZdudvSqUJ9k5SMdSLhRNsmI4FXfrWOk00ak9tSLJ4P" +
                "2zfFPDhDC2FkqvZmR/cF9a6jkySEgx+3hROUT3ElVovObWVVjCYuC5pGHWUJQZcgQBsi9hS6XtROx5ff" +
                "0uf51M+nP56k1QvGvtRtDKgXwh3Kd9Fz+fq0AFSK/I8JFePp9ESzKgQypTWtyLCw32U+R+8+Ak5olEAs" +
                "YMdYxhKS30ralmmjy3LHj4RpVkeR5XuUU+pPINHGMsMKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
