/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PoseWithCovarianceStamped : IDeserializable<PoseWithCovarianceStamped>, IHasSerializer<PoseWithCovarianceStamped>, IMessage
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "pose")] public PoseWithCovariance Pose;
    
        public PoseWithCovarianceStamped()
        {
            Pose = new PoseWithCovariance();
        }
        
        public PoseWithCovarianceStamped(in StdMsgs.Header Header, PoseWithCovariance Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        public PoseWithCovarianceStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Pose = new PoseWithCovariance(ref b);
        }
        
        public PoseWithCovarianceStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Pose = new PoseWithCovariance(ref b);
        }
        
        public PoseWithCovarianceStamped RosDeserialize(ref ReadBuffer b) => new PoseWithCovarianceStamped(ref b);
        
        public PoseWithCovarianceStamped RosDeserialize(ref ReadBuffer2 b) => new PoseWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Pose is null) BuiltIns.ThrowNullReference();
            Pose.RosValidate();
        }
    
        public int RosMessageLength => 344 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 344; // Pose
            return c;
        }
    
        public const string MessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "953b798c0f514ff060a53a3498ce6246";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTWvbQBC9768Y8CFJsV3aFB8CPZSWtjkU0iTQjxDCWBpJW6RdZXcVW/n1ebu2ZacO" +
                "pYc2xiBpNfNm3ps39oguK+1Jlq0T78UTGxIfdMNBcmqtF1roUBGTk0KcmEwos9bl2iCCCseNICcnpCCP" +
                "m1apz8K5OKrSRZ0B4xsg3ts7dpojQIRV6u0//qgvF59OyIf8pvGlf7nqQo3oIqA/djk1EjjnwFRYdKfL" +
                "StykljupKTUOvult6FvxUyQmZfAtxYjjuu6p8wgKFgo0TWd0FiUYiG/ykakNBGvZBZ11Nbs9xSI6vl5u" +
                "u6To6YcTxBgvWRc0GuqBkDlhr02Jl6Q6bcLx65hAI7o6t/7VtRpdLuwE51JC7KELChUH2hlpTuxPUOzF" +
                "iuUURaCSoFzu6TCd3eDRHxGqoRdpbVbRISic9aGyBoBCaXTzWiJwBimAehCTDo52kE2CNmzsBn6FuK3x" +
                "N7BmwI2cJhWGV0cZfFdCSQS2zt7pHKHzPoFktRYTqNZzx65XMWtVUo0+JnuGOMc0GlzZe5vp5O5obOWD" +
                "i+hpLDc6/1+2LMXCfq5feXN/JzZ2cxKnBj4+OiiuHxQrnIBSy9l6GTskuMCYfT9VacHWKzWic7uYNPwL" +
                "Bh+QOGjIbYsk1mw5g9OGTcSaO71MxYWs00M4zAtBgjgfTY9eCr2UfMLL3R5TaPTyKfAdtm2cauzkspPo" +
                "vcPlmPox3Y/J2XUBntsu0HeKiHvHP54+/pmOj1RRWw6zN1fHs+sdMs83OjB694S+++Max1+KeJyv3+sV" +
                "G/xe7og9JcwQwxwC1NcOBnUm4W7jnosgWtnYEWscfeZXc930Dy7RnLHlR3Q3g6HlcNcPd/fP0/5WuqdW" +
                "6pGev60Wnm63uuM/osFy/ZnR5m6h1ANfJQybSQcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<PoseWithCovarianceStamped> CreateSerializer() => new Serializer();
        public Deserializer<PoseWithCovarianceStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PoseWithCovarianceStamped>
        {
            public override void RosSerialize(PoseWithCovarianceStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PoseWithCovarianceStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PoseWithCovarianceStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(PoseWithCovarianceStamped msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<PoseWithCovarianceStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PoseWithCovarianceStamped msg) => msg = new PoseWithCovarianceStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PoseWithCovarianceStamped msg) => msg = new PoseWithCovarianceStamped(ref b);
        }
    }
}
