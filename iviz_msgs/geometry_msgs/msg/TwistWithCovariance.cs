/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/TwistWithCovariance")]
    public sealed class TwistWithCovariance : IDeserializable<TwistWithCovariance>, IMessage
    {
        // This expresses velocity in free space with uncertainty.
        [DataMember (Name = "twist")] public Twist Twist { get; set; }
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwistWithCovariance(in Twist Twist, double[] Covariance)
        {
            this.Twist = Twist;
            this.Covariance = Covariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwistWithCovariance(ref Buffer b)
        {
            Twist = new Twist(ref b);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovariance(ref b);
        }
        
        TwistWithCovariance IDeserializable<TwistWithCovariance>.RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovariance(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Twist.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new System.IndexOutOfRangeException();
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1fe8a28e6890a4cc3ae4c3ca5c7d82e6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TTYvbQAy9B/IfHuSyC94UuiWHhZ7LHgqlDaUflKLYsjNdexQ0ysbeX1+N491N2B56" +
                "KDXYyBrpSXrztMB6GxK43ymnxAn33EoZbECIqJUZaUcl4xBsi30sWY1CtGE5n81n60NIBsvf/LvARzlc" +
                "dfRLFMoZkKORBYmQGrZlrPoVSrknDeRQ6Mg09Dlx7Yei4Sl+R0odG2vCPjEIdei5uqLeez2H9kYWuPUK" +
                "WrEWY5WTZFK+yQEXfYGhwEMBlakEbWRv+IKM+cL99c/ub6P7cj6rWyFbvfl+vfpxMlAm4e0/fuaz95/e" +
                "3aBh8ZF0+NmlJr0aiT/y9jeXt1G54+hOEwRLaENkUlCs/G32rdvOmSXn8jOXJnqNKebEMUX+txmnwk9T" +
                "Pl2736oPmg/Px1xilILBgyW2Azqm6PKU51TPrIJ6bhZOFp1yLcqFs4JKnMAoI60d3TkoR5eep9Nu52gE" +
                "U4qpPephZBIXvGyWBQ5bZ3eMCrHxwAzRcGQNJTQ0oTqmeilX/JRNmAZ0zdavfb/a9tj1sZrrOKM86u9y" +
                "idsag+xxyDO5oajIvCfBxpucOqNNmzuWYtyaCeOc1g/iMnBqUqKGncBkTNW4zZOm4Qv5aA7P5sN89hs7" +
                "GhRcLAQAAA==";
                
    }
}
