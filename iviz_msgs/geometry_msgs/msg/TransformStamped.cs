/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/TransformStamped")]
    public sealed class TransformStamped : IDeserializable<TransformStamped>, IMessage
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id
        //
        // This message is mostly used by the 
        // <a href="http://wiki.ros.org/tf">tf</a> package. 
        // See its documentation for more information.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "child_frame_id")] public string ChildFrameId { get; set; } // the frame id of the child frame
        [DataMember (Name = "transform")] public Transform Transform { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TransformStamped()
        {
            Header = new StdMsgs.Header();
            ChildFrameId = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TransformStamped(StdMsgs.Header Header, string ChildFrameId, in Transform Transform)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Transform = Transform;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TransformStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            ChildFrameId = b.DeserializeString();
            Transform = new Transform(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TransformStamped(ref b);
        }
        
        TransformStamped IDeserializable<TransformStamped>.RosDeserialize(ref Buffer b)
        {
            return new TransformStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            Transform.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (ChildFrameId is null) throw new System.NullReferenceException(nameof(ChildFrameId));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 60;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(ChildFrameId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TransformStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b5764a33bfeb3588febc2682852579b0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTU/cMBC9R8p/GMGhUEFWolUPq5YTasuhEhWoVzQkk8QisVPbYdn++r5x2GRbeuih" +
                "ZcVq7WTmzcd7MxzSTWsCyePgJQQJxBQ921A731PtXU+lc74ylqPgzr1QK1yJL9Ll1lR5dkjRUWzluWnZ" +
                "mq663bNU4xSwRzRuhPToQuy2NAap6G6bgNTsPVPrpf5w0MY4rFerjbk3hXehcL5ZxfrgPNbvV3xOA5f3" +
                "QCqS07UAMgaqXDn2YiNH4yyhGETxeGW1rvSwyLM8+5xKeaooz0L0xja/ZU2HKaWpIFxdPdWqRtPTPLuZ" +
                "WzY3T+E//ONPnn25/rSmEKvbPjRhNaWf6o5sK/YV+hq54sip5tY0rfjTTh6kgxf3A1qc3sbtIKGY2cBf" +
                "I1Y8dzsiwGjp+n60plQ6owFh+wDqaizUMrCPphw79s/oT/j6DfJ9FFsKXV6sYWWDlGM0SGoLjNILB237" +
                "5QWMR2PjmzP1gOPNxp3iLg04mjNA9znSnmYr4rDWMK+nGgvAo0mCQFWgo/TsFtdwTIiDLGRwZUtHSP9q" +
                "G1voQ/l8YG/4rkuKLNEHwL5Sp1fH+9Ca+posW7fDnyCXIH+DaxdgLeu0BXmdtiCMDfoIy8G7B1MtA1F2" +
                "Bmqmztx59ts8U7cpKEA+JmlGJTJxg18OwZUGTFS0MbGdpb2M4n9TZyMOIvTbSaLzZMxa86K0oZqQKlu2" +
                "zZ3EjQi6tnHPpASJYow9pjtg3lVZefZNyuj8mwmhS0OdZ19H+HirU+/dNP4vVutTQn+qlOkhvfytjDQd" +
                "l0nNzmIaemGwjNmbXeFZGQ9fXVmAFaxEbLITbDksOfTFuqggPd8DVKAsdedhABrvt0Yfw+dIiqY4oU2L" +
                "RicrVcU0zmkDmJK8abDmZl5mb6anAk8o1mfQVddNWU/RQKai7Lp+XNBlTVs30kZrwsE/rR4HpufM0mhE" +
                "50507ewwfm3rlcMOWP5h2BCx9pIC6s5xfPeWHpcjZmN3/PFCtC+S+yPzlpzX4Z36+Av/evu+CFa7/Xd1" +
                "7Y4btf4JQZsMHcMHAAA=";
                
    }
}
