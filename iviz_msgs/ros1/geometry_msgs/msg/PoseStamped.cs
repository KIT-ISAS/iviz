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
    
        public PoseStamped()
        {
        }
        
        public PoseStamped(in StdMsgs.Header Header, in Pose Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        public PoseStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
        }
        
        public PoseStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
        }
        
        public PoseStamped RosDeserialize(ref ReadBuffer b) => new PoseStamped(ref b);
        
        public PoseStamped RosDeserialize(ref ReadBuffer2 b) => new PoseStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 56 + Header.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Pose);
        }
    
        public const string MessageType = "geometry_msgs/PoseStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d3812c3cbc69362b77dc0b19b345f8f5";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTY/TMBC9+1eM1MPuIlrEhzhU4rAS4uOAtLB7Q6ia2pPEUmJnbafd8Ot5dkhKxQEO" +
                "sFGUOPbMm/fmIyu6phsfhY42NRSkkiBOC2nvg7GOk1AVuBNiZyjZTmLirlcfhI0EaspLFYAeD6Xe/ONL" +
                "fbp9v6WYzK6LdXw2xVUruk0gxMFQJ4kNJ6bKg4+tGwnrVg7SUmEqhsppGnuJGzjeNTYS7lqcBG7bkYYI" +
                "o+QhuesGZ3XWvCid/eFpHTH1HJLVQ8vhtxRldNxR7oeSwo9vt7BxUfSQLAiNQNBBOFpX45DUYF16+SI7" +
                "0Iq+fvHx+Te1ujv6NfalRnoXFpQaTpm1PPRBYibMcYtgTyaVGwRBlgThTKTLsrfDZ7wiRAMX6b1u6BIS" +
                "bsbUeAdAoQMHy/tWMrBGKoB6kZ0urn5BdgXasfMz/IR4ivE3sG7BzZrWDYrX5jTEoUYmYdgHf7AGpvux" +
                "gOjWikvU2n3gMKrsNYVUq3elH1OuYykN3hyj1xaVMKWPVUwho5ey7Kz5X21Zi0f7hXHqzTwFEHiNKcpF" +
                "An1OFjnxVZmN3D9VEMjoWcvT3G552/w8t8U2T5kPdvbdEGYL3bAYqM8DVAZXcE92jyUQVOYRQi8kti6W" +
                "ai38oQUzUiifyVVV6zm9fkUPy2pcVt8fh/4pdbOGpVDooLN8npPPX/envONH023UHxTNq6NSPwD4OOVH" +
                "YwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
