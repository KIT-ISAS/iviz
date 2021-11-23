/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AccelWithCovariance : IDeserializable<AccelWithCovariance>, IMessage
    {
        // This expresses acceleration in free space with uncertainty.
        [DataMember (Name = "accel")] public Accel Accel;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// Constructor for empty message.
        public AccelWithCovariance()
        {
            Accel = new Accel();
            Covariance = new double[36];
        }
        
        /// Explicit constructor.
        public AccelWithCovariance(Accel Accel, double[] Covariance)
        {
            this.Accel = Accel;
            this.Covariance = Covariance;
        }
        
        /// Constructor with buffer.
        internal AccelWithCovariance(ref Buffer b)
        {
            Accel = new Accel(ref b);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AccelWithCovariance(ref b);
        
        AccelWithCovariance IDeserializable<AccelWithCovariance>.RosDeserialize(ref Buffer b) => new AccelWithCovariance(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Accel.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Accel is null) throw new System.NullReferenceException(nameof(Accel));
            Accel.RosValidate();
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new RosInvalidSizeForFixedArrayException(nameof(Covariance), Covariance.Length, 36);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelWithCovariance";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ad5a718d699c6be72a02b8d6a139f334";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71TTWvcQAy9+1c8yCUBx4Wm7CHQQ08lh0JpQ+kHpWht2TuNPTKacdbOr6/Gdpws7aGH" +
                "0oUFWaP3pPdGc4bbgwvgsVcOgQOoLLllpejEw3nUyozQU8k4unjA4EvWSM7HqciyN6l6wWTZGT7I8bKj" +
                "n6JQToTs40IkNeKBsRt3KOWe1JHRoKOobjTcrZ2Juq28J6WOI2vAEBiE2o1cXdJoo54yF4a+MX6tWPO5" +
                "xzMsKV/b+fmYY8rxkENlbUB7GSI+IzH+lv7y5/TXOX2R1a1Q3L36drX7/kxMlr3+x7/s3ce312hYTI1O" +
                "P7rQhBez37Nhf3tpe5U7TskocDGgdZ5JQb6yfzO0FptjMRTZJy6j6BXWkqfvte7/KFy7PmrcbttE4n4+" +
                "OxVYIG1AhNWKbyd0TD7CxG5IA1ZODZrWJa2aci3KudmBSsw9L9E4OrozSva2b4amvjcyQlTyoV2MnR3E" +
                "ORdNkeN4MFfnKucbKzSGhj2rK6GucdWCtEbdBias4mxR65f2nNp2mXlpZstrJI9Ld1HgpsYkA45JkAWK" +
                "iiIloj1vc9G+TfNKPj+UheLU0Pdid2+2hEANm3chMlX2dNc1xrhF0xY9ZL8AxPBx3BgEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
