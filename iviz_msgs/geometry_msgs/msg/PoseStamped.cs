/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
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
        public PoseStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PoseStamped(ref b);
        
        public PoseStamped RosDeserialize(ref ReadBuffer b) => new PoseStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 56 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "d3812c3cbc69362b77dc0b19b345f8f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTY/TMBC9+1eM1MPuIlokQBwqcVgJ8XFAWrR7r6bxJLGU2Fl70m749Ty7JKXiAAfY" +
                "KErseObNe/ORFd3SXUhCR6ctRakliq+EqhCidZ5VqI7cC7G3pK6XpNwP5rOwlUhteZkCMOBhzPt/fJmv" +
                "95+2lNTu+tSkV6e4ZkX3CkIcLfWibFmZ6gA+rmklrjs5SEeFqVgqpzoNkjZwfGhdItyNeIncdRONCUYa" +
                "ILnvR++qrHlROvvD03liGjiqq8aO428pyui4kzyOJYVfPmxh45NUozoQmoBQReHkfINDMqPz+uZ1djCr" +
                "h2NYYysNsroEJ21ZM1l5GqKkzJPTFjFenMRtgI3kCKLYRNfl2w7bdEMIAgoyhKqlazC/m7QNHoBCB46O" +
                "951k4AoZAOpVdrq6+QXZF2jPPszwJ8RzjL+B9Qtu1rRuUbMuq09jgwTCcIjh4CxM91MBqTonXqlz+8hx" +
                "MtnrFNKsPpY21Fy+UhG8OaVQORTAlvY1SWNGL9XYOfu/urGRgK6L06klc/ND4C2GJxcJ9FkdchLqMhK5" +
                "beookDFwJS9zl+XP9ue5K7Z5uEJ0s++GMFLohsXAfBuhMvqCe7Z7LoGgMk8OekHZ+VSqtfCHFoxGoXwh" +
                "19RdYH33lp6W1bSsvj8P/XPqZg1LodBBF/m8JJ93j+e84//Sb8wfFM2rozE/AC1FA+BaBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
