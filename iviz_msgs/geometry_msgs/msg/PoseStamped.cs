/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PoseStamped : IDeserializable<PoseStamped>, IMessage
    {
        // A Pose with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "pose")] public Pose Pose;
    
        /// Constructor for empty message.
        public PoseStamped()
        {
        }
        
        /// Explicit constructor.
        public PoseStamped(in StdMsgs.Header Header, in Pose Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        internal PoseStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PoseStamped(ref b);
        
        PoseStamped IDeserializable<PoseStamped>.RosDeserialize(ref Buffer b) => new PoseStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 56 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d3812c3cbc69362b77dc0b19b345f8f5";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTY/TMBC9+1eM1MPuIlokQBwqcVgJ8XFAWrR7r6bxJLGU2Fl70m749Ty7JKXiAAfY" +
                "KErseObNe/ORFd3SXUhCR6ctRakliq+EqhCidZ5VqI7cC7G3pK6XpNwP5rOwlUhteZkCMOBhzPt/fJmv" +
                "95+2lNTu+tSkV6e4ZkX3CkIcLfWibFmZ6gA+rmklrjs5SEeFqVgqpzoNkjZwfGhdItyNeIncdRONCUYa" +
                "ILnvR++qrHlROvvD03liGjiqq8aO428pyui4kzyOJYVfPmxh45NUozoQmoBQReHkfINDMqPz+uZ1djCr" +
                "h2NYYysNsroEJ21ZM1l5GqKkzJPTFjFenMRtgI3kCKLYRNfl2w7bdEMIAgoyhKqlazC/m7QNHoBCB46O" +
                "951k4AoZAOpVdrq6+QU5096SZx9m+BPiOcbfwPoFN2tat6hZl9WnsUECYTjEcHAWpvupgFSdE6/UuX3k" +
                "OJnsdQppVh9LG2ouX6kI3pxSqBwKYEv7mqQxo5dq7Jz9X93YSEDXxenUkrn5IfAWw5OLBPqsDjkJdRmJ" +
                "3DZ1FMgYuJKXucvyZ/vz3BXbPFwhutl3QxgpdMNiYL6NUBl9wT3bPZdAUJknB72g7Hwq1Vr4QwtGo1C+" +
                "kGvqLrC+e0tPy2paVt+fh/45dbOGpVDooIt8XpLPu8dz3vF/6TfmD4rm1dGYHy1FA+BaBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
