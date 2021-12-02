/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Odometry : IDeserializable<Odometry>, IMessage
    {
        // This represents an estimate of a position and velocity in free space.  
        // The pose in this message should be specified in the coordinate frame given by header.frame_id.
        // The twist in this message should be specified in the coordinate frame given by the child_frame_id
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "child_frame_id")] public string ChildFrameId;
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovariance Pose;
        [DataMember (Name = "twist")] public GeometryMsgs.TwistWithCovariance Twist;
    
        /// Constructor for empty message.
        public Odometry()
        {
            ChildFrameId = string.Empty;
            Pose = new GeometryMsgs.PoseWithCovariance();
            Twist = new GeometryMsgs.TwistWithCovariance();
        }
        
        /// Explicit constructor.
        public Odometry(in StdMsgs.Header Header, string ChildFrameId, GeometryMsgs.PoseWithCovariance Pose, GeometryMsgs.TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Pose = Pose;
            this.Twist = Twist;
        }
        
        /// Constructor with buffer.
        internal Odometry(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            ChildFrameId = b.DeserializeString();
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
            Twist = new GeometryMsgs.TwistWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Odometry(ref b);
        
        Odometry IDeserializable<Odometry>.RosDeserialize(ref Buffer b) => new Odometry(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            Pose.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ChildFrameId is null) throw new System.NullReferenceException(nameof(ChildFrameId));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            Twist.RosValidate();
        }
    
        public int RosMessageLength => 684 + Header.RosMessageLength + BuiltIns.GetStringSize(ChildFrameId);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/Odometry";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cd5e73d190d741a2f92e81eda573aca7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1WS2/bRhC+81cM4EPsQlKBuPDBQA9FgrY+FEgbo0lbFMaIHJHbkLvK7lIS8+vzzfIh" +
                "WVaaHFKdKhAQuTvv+eZxQfeVCeRl7SWIjYHYkoRoGo5CbkVMaxdMNM7ipqCN1C43sSNjaeVFKKw5lwVR" +
                "dgFJosSid1GlNhICl6CpXFsXtFRqyc3KSNHTCOXO+cJYVbby3AiVZiOWlh1VwoX4RTp9MMViUBC3JsSv" +
                "oyHdVqYuHkYl2c9J6aA7C9EbWx7TlOIaib57aEIZvn0Fh9+YWL1wG/aGbd7H4IjqXq0+IkueZNn3X/mX" +
                "/fL6p1sKseg19x4heK8j8se+QMwiFxyZVg6emrISP68FiQUTN2tELt3Gbi2hjzoCjacUK57ruqM2gCg6" +
                "hLZpWmtyjS0QI4/4wYkMAD7so8nbmv2TVKh0PEHet6IRuXt5CxobJG8jkgRNxuZeOGgW7l5S1hobr58r" +
                "Q3Zxv3VzfEqJfE3KkVMGOgLJTgGtdnK4hY5veucWkI3gCLQUgS7T2QM+wxVBCUyQtcsruoTlr7pYAfQK" +
                "kpSyZQ1gB8oRAUh9pkzPrg4kq9m3ZNm6UXwvca/jS8TaSa76NK+Qs1q9D22JAIJw7d3GFCAdAVwblC3V" +
                "ZunZd5ly9Sqzix8T3FOdpIzgn0NA+SIBBW2BxhHhE7b/IzR+rmSyE21oaiX7NpNsphYMPjJy3y2yTIX1" +
                "FQchv7ntvOF/gOtJEqfWhUamwbrZ3QBgUwWix3mzGxqL82YiB2YRkCg+KNZhy8rspJjz7tDGRKoQvoN8" +
                "jyKbJR0HvOxFsXe5m1E3ow8z8m5QwEvXRnpLKvHJ8R+nj/9Mx1fZqnYcb7776/rm7wNnzpc6ePTDifg+" +
                "TddMG4QeF8P9fowcBHtByCGSORFkv7YAqLdJ7p7uXA7ClBGOKGPFWejzOto/DEU1+ZG7Y2JoN71109uH" +
                "85i/D92pknoUz6PSwtf7fdwxGhoU1797NL5tz+PbiRk6Ojl2+/CJ/eRU40jixiH8f+s4T/q+OGFL795h" +
                "S0OuHBlgF0NQMAC1d7At0zahiwUWlN8lj85f00Cy/x7ozgPOQevJQbZJd8cbsw6OtKo4i0WnEUY/gbMT" +
                "JxgL48GaeiQmlBdUJZqqiVQ4RM46DWfD7yBSsDcQuHm9hjAsb55tqPs6TxGkS1mUixltK0Q1UencT1ta" +
                "2utMTt6UBmudcmr5T8xMg3OYb6vnKKW67m3ulaE3Qsg4q64WdLeizrW0VYfw4od10ul2PtqV1p7o3CzN" +
                "117EiUY87fdowhGL7Gdb0kfxFemazwwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
