/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/PoseWithCovarianceStamped")]
    public sealed class PoseWithCovarianceStamped : IDeserializable<PoseWithCovarianceStamped>, IMessage
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "pose")] public PoseWithCovariance Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseWithCovarianceStamped()
        {
            Header = new StdMsgs.Header();
            Pose = new PoseWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseWithCovarianceStamped(StdMsgs.Header Header, PoseWithCovariance Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseWithCovarianceStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Pose = new PoseWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PoseWithCovarianceStamped(ref b);
        }
        
        PoseWithCovarianceStamped IDeserializable<PoseWithCovarianceStamped>.RosDeserialize(ref Buffer b)
        {
            return new PoseWithCovarianceStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 344;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "953b798c0f514ff060a53a3498ce6246";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
    }
}
