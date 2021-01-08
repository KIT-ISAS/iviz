/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/Odometry")]
    public sealed class Odometry : IDeserializable<Odometry>, IMessage
    {
        // This represents an estimate of a position and velocity in free space.  
        // The pose in this message should be specified in the coordinate frame given by header.frame_id.
        // The twist in this message should be specified in the coordinate frame given by the child_frame_id
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "child_frame_id")] public string ChildFrameId { get; set; }
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovariance Pose { get; set; }
        [DataMember (Name = "twist")] public GeometryMsgs.TwistWithCovariance Twist { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Odometry()
        {
            Header = new StdMsgs.Header();
            ChildFrameId = "";
            Pose = new GeometryMsgs.PoseWithCovariance();
            Twist = new GeometryMsgs.TwistWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Odometry(StdMsgs.Header Header, string ChildFrameId, GeometryMsgs.PoseWithCovariance Pose, GeometryMsgs.TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Pose = Pose;
            this.Twist = Twist;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Odometry(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            ChildFrameId = b.DeserializeString();
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
            Twist = new GeometryMsgs.TwistWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Odometry(ref b);
        }
        
        Odometry IDeserializable<Odometry>.RosDeserialize(ref Buffer b)
        {
            return new Odometry(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            Pose.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (ChildFrameId is null) throw new System.NullReferenceException(nameof(ChildFrameId));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            Twist.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 684;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(ChildFrameId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/Odometry";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cd5e73d190d741a2f92e81eda573aca7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1XS2/bRhC+E+B/GCCH2IWkAnHhg4EeigZtfSjgNkafKIwVOSS3IXeZ3aUk5tf3m+VL" +
                "tlXEh1SnChZE7s57vnn4Fd1X2pPj1rFnEzwpQ+yDblRgsgUpaq3XQVuDm5x2XNtMh560ocIxk29Vxhui" +
                "NHkFUSzULJdBxDbsvSpBVNmuzmkr5JzpQnM+0DBl1rpcG9FWONUwlXrHhrY9Vaxydpt4+qDzzaQh7LUP" +
                "n0dFvK10nT9MWtLkh6h21J4mPjhtymdUJduGg+sfGl/6L+/g9K86VN/anXJamWyIw1Oye7H8CV30Jk3S" +
                "5OvP/EmTH999f0M+5IPywS8J4ruATCqXI3ZB5SooKiwc1mXFbl0zUgwu1bSIYLwNfct+DD8ijr+SDTtV" +
                "1z11HlTBIsZN0xmdSZABHn4kQFiRC0BJuaCzrlbuWVKifPl6/tCxROb27Q2ojOesC0gYlGmTOVZe8nH7" +
                "FsSdNuHqjXCA8X5v13jnEsmbLUCGFbDiiQ8CcDFW+RtR88Xg4wbiESSGotzTRTx7wKu/JOiBFdzarKIL" +
                "mH/XhwpVIJiJ2dvWALqnDHGA2NfC9PryWLSYfkNGGTvJH0QuSl4i1yyCxa11heTVEgLflYgjKFtndzoH" +
                "7YToWqOSqdZbp1yfJsI2KIWQ72IFxNKJucGv8h41jUzktAc4Z8wvaP/P0PmpMppBd9yg5h6zNKBoOHXg" +
                "cEEBBT3glCYibyxEEfSz3a8b9TewPktTsbGhzUncrg/XANxcmeiATh+mrmOdnukBY0QmsPOCfxhU6APn" +
                "a3U4NjSSRlTfQoND7a2iliNm5Thi8eKwon5FH1fk7KhCbW0X6DcSmc+Ofz99/Ec8BlKK2qpw/dWfV9d/" +
                "HTl01jSKW9+ciPPz1K2kd8hxPt4vw+Yo5huU+51FZmeKNPmpA2adiZIXyjO6CXNmgKLABXl+yPHkxThA" +
                "xe5HTs85IgBsekSpTo8fz+bFEsSTtfYotE9qDm8flhRghjSx6j7p2fS4P5uTJwbv7O00Gfy/7DanW0uU" +
                "uAzv/5vLWVP58uRtnX2PXQ95s6QBaExOxtSU7qJMGTcRWUpkvfmFs2DdFY00Rwcj5dl8HBWfHn67ePl0" +
                "/45zJm461mBTalih5cDnmRWcuXbgjc0UE80xKhbdVwfKLQJobAxro95DKGPrEHbVtpCGFdAp4+uhCcRI" +
                "0gVvys2K9hWiG6lkYxhWvbgd6oycLjWWQ2GV5jBzKxodxEAs3qC+6nqwetCGBipSpuF2uaHbgnrb0V58" +
                "woMb11Ir2/5kWVybgrWrOJJHGSca9vwPA3p1wEr8spb1DwyGxhwnDQAA";
                
    }
}
