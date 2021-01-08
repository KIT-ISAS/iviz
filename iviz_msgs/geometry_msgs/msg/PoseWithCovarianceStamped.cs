/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PoseWithCovarianceStamped")]
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
        public PoseWithCovarianceStamped(ref Buffer b)
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
                "H4sIAAAAAAAACr1VTW/TQBC9W/J/GKmHFpQUiaIcInFAIKAHpEIr8SVUTexJvMjedXfXbdxfz5t146Sk" +
                "EhygUaLY65k3b968SQ7oojKBZN16CUECsSUJ0TQcpaTWBaEbEyti8rIUL7YQKpzzpbGIoKXnRpBTElKQ" +
                "x02bZ3n2XrgUT1X6yrMzwHwGymt3zd6wYiiyRr78x688+3D+bk4hlpdNWIVnA5M8O6DzCJrsS2okcsmR" +
                "aelA0awq8dNarqWmxB9tp6exbyUca2ZSCO+VWPFc1z11AVHRQYmm6awpVIpRgA2AphoL5Vr20RRdzX5P" +
                "uoSvnyBXXRL39M0cUTZI0UUDUj0wCi8cjF3hIYI7Y+PJc81A4sWNm+JeVpB7ZECx4kg7Yy2Jw1zLPB16" +
                "PAY8RBIUKgMdpbNL3IYnhDpgIa0rKjoC/bM+Vs4CUSgNb1GLIhfQAbCHmnT4ZBdaqc/JsnUb/AFyW+Rv" +
                "cO0WWNuaVhherRKEbgUdEdl6d21KxC76hFLURmyk2iw8+z7PNG0oCpC3yadRB5lmg28OwRUm2Vwdnmch" +
                "ei2Q5nJpyv/ozpU4mND3g0X3t2M0nRedH9oC3WEZodzSCxprubhbzQ4ZPjJc0MNOw7aN+3VAn9zNtOGf" +
                "8PqIxtFAerdMus3WMxhuXEwsvjfrgYGQ82aMh42hTBQf1P8gtDRrKae83iWaQpOrT1HBY/cmqcpOMntJ" +
                "XjxaT6if0O2EvLsrwQvXRfpCirl3/PXh42/pGE5Z1o7j7MX3k9mPnYYedYza1qsHdN4f3UR/O/S4vHtu" +
                "hpbwS7qj+THW/cxhsmNEnn3s4FlvE/I28hHbBJ3RoFhwdV4YZrzpAh2pXZX3vabHGREMtrnEqm4ubx+t" +
                "i62ID+7aPWl/2zncXW1HgP+QJm3dHzvbXN5o9C9Qqv27dwcAAA==";
                
    }
}
