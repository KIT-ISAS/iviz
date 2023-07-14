/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PoseStamped : IHasSerializer<PoseStamped>, IMessage
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
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Pose);
        }
        
        public PoseStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align8();
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
            b.Align8();
            b.Serialize(in Pose);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 56;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 56; // Pose
            return size;
        }
    
        public const string MessageType = "geometry_msgs/PoseStamped";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d3812c3cbc69362b77dc0b19b345f8f5";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
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
    
        public Serializer<PoseStamped> CreateSerializer() => new Serializer();
        public Deserializer<PoseStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PoseStamped>
        {
            public override void RosSerialize(PoseStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PoseStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PoseStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(PoseStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(PoseStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<PoseStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PoseStamped msg) => msg = new PoseStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PoseStamped msg) => msg = new PoseStamped(ref b);
        }
    }
}
