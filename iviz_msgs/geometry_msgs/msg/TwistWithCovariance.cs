/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwistWithCovariance : IDeserializable<TwistWithCovariance>, IMessage
    {
        // This expresses velocity in free space with uncertainty.
        [DataMember (Name = "twist")] public Twist Twist;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// Constructor for empty message.
        public TwistWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// Explicit constructor.
        public TwistWithCovariance(in Twist Twist, double[] Covariance)
        {
            this.Twist = Twist;
            this.Covariance = Covariance;
        }
        
        /// Constructor with buffer.
        internal TwistWithCovariance(ref Buffer b)
        {
            b.Deserialize(out Twist);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwistWithCovariance(ref b);
        
        TwistWithCovariance IDeserializable<TwistWithCovariance>.RosDeserialize(ref Buffer b) => new TwistWithCovariance(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref Twist);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new RosInvalidSizeForFixedArrayException(nameof(Covariance), Covariance.Length, 36);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistWithCovariance";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1fe8a28e6890a4cc3ae4c3ca5c7d82e6";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TTWvcQAy9z694kEsCjgtN2UOg55JDobSh9INStLbsncYeLRpt1s6vr+x1NlmaQw+l" +
                "BpvxjN6T9PTmDLebmMHDVjlnzrjnTqpoI2JCo8zIW6oY+2gb7FLFahSTjWUIt/uYDTZ9QzjDR9lf9vRL" +
                "FMoTGScji5IgDWzDWA0rVHJPGslp0JNpHBx362ei8Ri+JaWejTVjlxmEJg5cX9LgZZ4yl46+cX6tWYs5" +
                "xzMsKV/7+flQYCzwUEBlSUBr2Rm+YGL8Y/vry9vf5u2L0HRCtnrz/Wr141kzIbz9x094/+ndNVoW70bH" +
                "n31u86tZ7/C3A1ur3HHyTRNEy+hiYlJQqv1td52vXS3LZfjMlYleYQl5+l/i/k93S9bH/o6T9kl6i9PZ" +
                "aYMlpukbPFZSN6JnSu5GeUI6sI7q0Mkqk82UG1EuXA7U4solmeTs6c4pObnXHE3brZMRTCnl7mCBWUGc" +
                "c9mWBfYbV3WOiqn1QGdoObHGChrbWB+QnsgNvoAJS3Nu0ua1X6WuO9R8SObGdZJHw12UuGkwyg77qSFf" +
                "KGoyr0iw9hKXumjdTfVKMV+SA8WpoB/EZ++y5Ewtu3bZmGq/touFMRxX43H1EH4DKG1jSRAEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
