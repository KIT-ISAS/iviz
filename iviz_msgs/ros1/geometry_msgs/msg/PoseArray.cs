/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PoseArray : IDeserializable<PoseArray>, IMessage
    {
        // An array of poses with a header for global reference.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public Pose[] Poses;
    
        public PoseArray()
        {
            Poses = System.Array.Empty<Pose>();
        }
        
        public PoseArray(in StdMsgs.Header Header, Pose[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        public PoseArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Poses);
        }
        
        public PoseArray(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeStructArray(out Poses);
        }
        
        public PoseArray RosDeserialize(ref ReadBuffer b) => new PoseArray(ref b);
        
        public PoseArray RosDeserialize(ref ReadBuffer2 b) => new PoseArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Poses);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Poses);
        }
        
        public void RosValidate()
        {
            if (Poses is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 56 * Poses.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Poses length
            c = WriteBuffer2.Align8(c);
            c += 56 * Poses.Length;
            return c;
        }
    
        public const string MessageType = "geometry_msgs/PoseArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "916c28c5764443f268b296bb671b9d97";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71Uy27bMBC88ysW8CFJ0bjoAz0Y6CFA0cehQNrkFgTGWlxLBChSWVJ21K/vUIrl+lL0" +
                "0MYQYIqcnZ2dXWpBV4FYlQeKW+pikkR7lxtiaoStKG2jUu3jhj2pbEUlVLI05st0OoGMuUbk3f1EYMyH" +
                "f/wz324+ryhlu25TnV5Nuc2CbjIHy2qplcyWM49qG1c3opdeduIRxG0nlsbTPHSSlgi8bVwiPLUEUfZ+" +
                "oD4BlCNVsW374CrOQtm1chKPSAe7qGPNruo9K/BRrQsFvlVupbDjSfLQF6fo68cVMCFJ1WcHQQMYKhVO" +
                "LtQ4JNO7kN++KQG0oLsfMb2+N4vbfbzEvtSweFZBueFcVMtjp5KKYE4rJHsxVblEErgkSGcTnY97a7ym" +
                "C0I2aJEuVg2do4TrITcxgFBox+p446UQV7ACrGcl6OziN+YwUgcO8UA/MR5z/A1tmHlLTZcNmueLDamv" +
                "4SSAncads4BuhpGk8k5CJu82yjqYEjWlNItPxWyAEDW2Bv+cUqwcOmHHETYpa2Ef27J29n+NZS0R46fD" +
                "NJvlIqDAK1yW0iTI5+zgydPtKvOzVUEZHVfysoxb2bZP527EwheK6g6xS8L1wjTMAPO9R5UaRt4j7rkK" +
                "hJTDFcIsZHYhjd2a9aMW3JFR8km5Zusj5/fv6HFeDfPq5/PIP1p3qGFuFCboxM9T8eXt4eg7PjQtPoN/" +
                "ruiw2hvzCzR+fYtpBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
