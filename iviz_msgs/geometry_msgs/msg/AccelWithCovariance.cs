/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/AccelWithCovariance")]
    public sealed class AccelWithCovariance : IMessage, IDeserializable<AccelWithCovariance>
    {
        // This expresses acceleration in free space with uncertainty.
        [DataMember (Name = "accel")] public Accel Accel { get; set; }
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelWithCovariance(in Accel Accel, double[] Covariance)
        {
            this.Accel = Accel;
            this.Covariance = Covariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AccelWithCovariance(ref Buffer b)
        {
            Accel = new Accel(ref b);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovariance(ref b);
        }
        
        AccelWithCovariance IDeserializable<AccelWithCovariance>.RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovariance(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Accel.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength => 336;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ad5a718d699c6be72a02b8d6a139f334";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71TTWvcQAy9+1c8yCUBx4Wm7CHQQ08lh0JpQ+kHpWht2TuNPTKacdbOr6/Gdpws7aGH" +
                "0oUFWaP3pPdGc4bbgwvgsVcOgQOoLLllpejEw3nUyozQU8k4unjA4EvWSM7HqciyN6l6wWTZGT7I8bKj" +
                "n6JQToTs40IkNeKBsRt3KOWe1JHRoKOobjTcrZ2Juq28J6WOI2vAEBiE2o1cXdJoo54yF4a+MX6tWPO5" +
                "xzMsKV/b+fmYY8rxkENlbUB7GSI+IzH+lv7y5/TXOX2R1a1Q3L36drX7/kxMlr3+x7/s3ce312hYTI1O" +
                "P7rQhBez37Nhf3tpe5U7TskocDGgdZ5JQb6yfzO0FptjMRTZJy6j6BXWkqfvte7/KFy7PmrcbttE4n4+" +
                "OxVYIG1AhNWKbyd0TD7CxG5IA1ZODZrWJa2aci3KudmBSsw9L9E4OrozSva2b4amvjcyQlTyoV2MnR3E" +
                "ORdNkeN4MFfnKucbKzSGhj2rK6GucdWCtEbdBias4mxR65f2nNp2mXlpZstrJI9Ld1HgpsYkA45JkAWK" +
                "iiIloj1vc9G+TfNKPj+UheLU0Pdid2+2hEANm3chMlX2dNc1xrhF0xY9ZL8AxPBx3BgEAAA=";
                
    }
}
