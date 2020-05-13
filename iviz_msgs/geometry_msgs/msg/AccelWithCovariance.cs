using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [DataContract]
    public sealed class AccelWithCovariance : IMessage
    {
        // This expresses acceleration in free space with uncertainty.
        
        [DataMember] public Accel accel { get; set; }
        
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember] public double[/*36*/] covariance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovariance()
        {
            accel = new Accel();
            covariance = new double[36];
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelWithCovariance(Accel accel, double[] covariance)
        {
            this.accel = accel ?? throw new System.ArgumentNullException(nameof(accel));
            this.covariance = covariance ?? throw new System.ArgumentNullException(nameof(covariance));
            if (this.covariance.Length != 36) throw new System.ArgumentException("Invalid size", nameof(covariance));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AccelWithCovariance(Buffer b)
        {
            this.accel = new Accel(b);
            this.covariance = b.DeserializeStructArray<double>(36);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new AccelWithCovariance(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.accel);
            b.SerializeStructArray(this.covariance, 36);
        }
        
        public void Validate()
        {
            if (accel is null) throw new System.NullReferenceException();
            accel.Validate();
            if (covariance is null) throw new System.NullReferenceException();
            if (covariance.Length != 36) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength => 336;
    
        string IMessage.RosType => RosMessageType;
    
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
