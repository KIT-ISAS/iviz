using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PoseWithCovarianceStamped : IMessage
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        
        public std_msgs.Header header { get; set; }
        public PoseWithCovariance pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseWithCovarianceStamped()
        {
            header = new std_msgs.Header();
            pose = new PoseWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseWithCovarianceStamped(std_msgs.Header header, PoseWithCovariance pose)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.pose = pose ?? throw new System.ArgumentNullException(nameof(pose));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseWithCovarianceStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.pose = new PoseWithCovariance(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new PoseWithCovarianceStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            this.pose.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (pose is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 344;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "953b798c0f514ff060a53a3498ce6246";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
