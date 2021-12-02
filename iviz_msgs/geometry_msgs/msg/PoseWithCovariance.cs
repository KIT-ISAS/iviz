/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PoseWithCovariance : IDeserializable<PoseWithCovariance>, IMessage
    {
        // This represents a pose in free space with uncertainty.
        [DataMember (Name = "pose")] public Pose Pose;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// Constructor for empty message.
        public PoseWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// Explicit constructor.
        public PoseWithCovariance(in Pose Pose, double[] Covariance)
        {
            this.Pose = Pose;
            this.Covariance = Covariance;
        }
        
        /// Constructor with buffer.
        internal PoseWithCovariance(ref Buffer b)
        {
            b.Deserialize(out Pose);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PoseWithCovariance(ref b);
        
        PoseWithCovariance IDeserializable<PoseWithCovariance>.RosDeserialize(ref Buffer b) => new PoseWithCovariance(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref Pose);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new RosInvalidSizeForFixedArrayException(nameof(Covariance), Covariance.Length, 36);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 344;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseWithCovariance";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c23e848cf1b7533a8d7c259073a97e6f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TO0/DQAze71dY6gJSWgZQhkoMTIgBiUcHHkLIJE57iNwF34Um/fX40jRJH2JCzeT4" +
                "8dnf5/MIZgvtgKlgcmS8A4TCOgJtIGMicAUmBEvtF1CahNijNr6eKHUXskKqUiN4sMtxjp+WeyT02hqw" +
                "GfgFQVzFkNgfZI0CAjl61pXUzSRmWXfpBTLm5IkdlAKPkOmK0jFWwxmb1IlU3wg+p8RR02NQi0xTiZ9U" +
                "EdQRrCJg2zbAD1t6eIKAuOd+Pux+adynKvuy6OOL1/P4bUBGqct//tTt4/UU5mSFDdfvuZu7s6C2MLo6" +
                "oO/+uiIZLw/utI3rNRuTDsWegOxQltklqPsSRT7T4PZ5xyIoozQvQladWOmtjVvvdTO/cAmPM4y8RVe1" +
                "i4Gqs+rOWh1n/F66DYfhSW3puXNa8vfd655ZzuW4/ma0sZZK/QLCDfdMwAMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
