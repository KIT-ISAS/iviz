/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/TwistWithCovarianceStamped")]
    public sealed class TwistWithCovarianceStamped : IDeserializable<TwistWithCovarianceStamped>, IMessage
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "twist")] public TwistWithCovariance Twist { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovarianceStamped()
        {
            Header = new StdMsgs.Header();
            Twist = new TwistWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwistWithCovarianceStamped(StdMsgs.Header Header, TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwistWithCovarianceStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Twist = new TwistWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovarianceStamped(ref b);
        }
        
        TwistWithCovarianceStamped IDeserializable<TwistWithCovarianceStamped>.RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovarianceStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            Twist.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 336;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/TQBC9W/J/GKkHWpQEqUU5VOIEAnpAqmjFp1A1sSf2UnvX7K6bmF/Pm7WTpioH" +
                "DkCUyM565s3MmzfjI7quTSAvnZcgNgZiSxKiaTlKSXFjQqSNiTVM1uLFFkKFc740Fga09twKXGBpWrhx" +
                "2y3y7K1wKZ7qdMmzawX5CIyX7o69YcVIwHmWZy/+8ifP3l29OacQy5s2VOHZmEueHdFVRJ7sS2olcsmR" +
                "ae2QpKlq8fNG7qShVADKTk/j0ElANUcjRfhWYsVz0wzUByXHgYq27a0plIs9AzsAdTWWmDr20RR9w/4R" +
                "dwlff0F+9Indi1fnsLJBij4aJDUAo/DCwdgKD2HcGxvPTtUDjtcbN8d/qUD4PgOKNUfNWLbaV02Ww7mG" +
                "eTrWuAA8SBIEKgMdp7Mb/A0nhDjIQjpX1HSM9C+HWDsLRKHUvVUjilyAB8A+UacnJ4fQmvo5WbZuhz9C" +
                "3gf5E1x7D6xlzWs0r1EKQl+BR1h23t2ZErarIaEUjYGAqTErz37IM3UbgwLkdRJq1Eam3uDKIbjCJJmr" +
                "wPMsRK8BUl9uTPkP1VmJgwj9MEr0N/OxV92ugYEgBaQbVQ5IUVBax2A1zWYPFx8ZOhggqGni7mfsiN67" +
                "zbzl79D7ftI5GtDv1om75XYJ0e2nE8PvzXZMQsh5s7eHlMFOFB90BqDttdlKOeft4RJJpknZF4jgMX+z" +
                "FOXAmb0kPR5vZzTM6OeMvJtC8Mr1kT6RYj46/vz74y/pGGpZN47j8vnXs+W3g4L+byv/vHkr727F4hCr" +
                "xGD3QuACces+ZVulhaG7Q7fQBymi82c02RwcTJb/rcYp8L7Kw3cHCtWHD8tcpAV3kRaSs1horTAGFTXv" +
                "XeFZGg9fFY6KDm8b52UGVqh0INC6RGvLtwAVLAd1564DGja1ZxuaUQ+JSTqWRbWY0aYGu8lKB3vcyGmJ" +
                "m4K8qQx2uLoiFBQ/eTNNBUKz61PMV9OMWY/RoGNF2envZEEXaxpcTxutCTd+ens4WiHJKbO03aJzszQ1" +
                "E8ZDWi8dZABqQuAKm9CGiDdXmuZJ04SB3N1ive1uf+bZL9imaCDFBwAA";
                
    }
}
