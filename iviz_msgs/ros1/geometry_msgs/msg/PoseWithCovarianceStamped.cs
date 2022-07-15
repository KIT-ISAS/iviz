/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PoseWithCovarianceStamped : IDeserializable<PoseWithCovarianceStamped>, IMessageRos1
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "pose")] public PoseWithCovariance Pose;
    
        /// Constructor for empty message.
        public PoseWithCovarianceStamped()
        {
            Pose = new PoseWithCovariance();
        }
        
        /// Explicit constructor.
        public PoseWithCovarianceStamped(in StdMsgs.Header Header, PoseWithCovariance Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        public PoseWithCovarianceStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Pose = new PoseWithCovariance(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PoseWithCovarianceStamped(ref b);
        
        public PoseWithCovarianceStamped RosDeserialize(ref ReadBuffer b) => new PoseWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "953b798c0f514ff060a53a3498ce6246";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VwW7TQBC971eMlENblASJohwqcUAgoAekQitRQKia2GN7kb3r7q6buF/P203spKRC" +
                "HKBRpNjrmTcz771xJnRVaU+ybp14L57YkPigGw6SU2u90EqHipicFOLEZEKZtS7XBhFUOG4EOTkhBXnc" +
                "tEp9EM7FUZV+1AUwvgDijb1jpzkCRFilXv3jj/p4+f6MfMhvGl/655su1IQuA/pjl1MjgXMOTIVFd7qs" +
                "xM1quZOaUuOYNz0NfSt+jsTEDL6lGHFc1z11HkHBgoGm6YzOIgXj4EM+MrUBYS27oLOuZnfAWETH18tt" +
                "lxg9f3uGGOMl64JGQz0QMifstSnxkFSnTTh9ERPU5GplZ7iVEhyPxSlUHGhPyZzYn6HGs81wc2CDHEGV" +
                "3NNxOrvBrT8hFEEL0tqsomN0ftGHyhoACiXFlrVE4AwMAPUoJh2d7CGbBG3Y2AF+g7ir8TewZsSNM80q" +
                "aFbH6X1XgkAEts7e6Ryhyz6BZLUWE6jWS8euVzFrU1JN3iVXhihfUgS/7L3NdDJ19LPywUX0pMaNzv+X" +
                "G0uxcJ3rN5Y8XIXBZU6iapjHR+PErQNjhROM1HK23cEOCS4wtO/nKu3VdpMm9NmuZg3/hK9HJA4adNsi" +
                "kbVYL2CwcQGx3U6vU3Eh6/QYDs+CkCDOR6+jl0KvJZ/xer/HFBotfA58hyWbphp7uewkeu94PaV+SvdT" +
                "cnZbgJe2C3RNEfHg+Ovjx9/S8Ykqasth8fL76eLH3jBPJx0mev0Iv4dyTeMLIh7n2+d6Mw1ek3tkzwka" +
                "QswxQH3qYFBnEu4u7qkGRCuDHbHG0Wd+o+vQP2aJ5owtPxh3EIbW41U/Xt0/Tfs76h5bqQd8/rZauLvd" +
                "8Y6/hgbL9eeJhquVUr8AuY0YXkAHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
