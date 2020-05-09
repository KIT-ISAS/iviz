using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PoseStamped : IMessage
    {
        // A Pose with reference coordinate frame and timestamp
        public std_msgs.Header header { get; set; }
        public Pose pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseStamped()
        {
            header = new std_msgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseStamped(std_msgs.Header header, Pose pose)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.pose = pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.pose = new Pose(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new PoseStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            this.pose.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 56;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/PoseStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "d3812c3cbc69362b77dc0b19b345f8f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTY/TMBC9+1eM1MPuIlokQBwqcVgJ8XFAWrR7r6bxJLGU2Fl70m749Ty7JKXiAAfY" +
                "KErseObNe/ORFd3SXUhCR6ctRakliq+EqhCidZ5VqI7cC7G3pK6XpNwP5rOwlUhteZkCMOBhzPt/fJmv" +
                "95+2lNTu+tSkV6e4ZkX3CkIcLfWibFmZ6gA+rmklrjs5SEeFqVgqpzoNkjZwfGhdItyNeIncdRONCUYa" +
                "ILnvR++qrHlROvvD03liGjiqq8aO428pyui4kzyOJYVfPmxh45NUozoQmoBQReHkfINDMqPz+uZ1djCr" +
                "h2NYYysNsroEJ21ZM1l5GqKkzJPTFjFenMRtgI3kCKLYRNfl2w7bdEMIAgoyhKqlazC/m7QNHoBCB46O" +
                "951k4AoZAOpVdrq6+QXZF2jPPszwJ8RzjL+B9Qtu1rRuUbMuq09jgwTCcIjh4CxM91MBqTonXqlz+8hx" +
                "MtnrFNKsPpY21Fy+UhG8OaVQORTAlvY1SWNGL9XYOfu/urGRgK6L06klc/ND4C2GJxcJ9FkdchLqMhK5" +
                "beookDFwJS9zl+XP9ue5K7Z5uEJ0s++GMFLohsXAfBuhMvqCe7Z7LoGgMk8OekHZ+VSqtfCHFoxGoXwh" +
                "19RdYH33lp6W1bSsvj8P/XPqZg1LodBBF/m8JJ93j+e84//Sb8wfFM2rozE/AC1FA+BaBQAA";
                
    }
}
