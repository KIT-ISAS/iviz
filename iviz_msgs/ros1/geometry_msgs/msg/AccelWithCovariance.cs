/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class AccelWithCovariance : IDeserializable<AccelWithCovariance>, IMessage
    {
        // This expresses acceleration in free space with uncertainty.
        [DataMember (Name = "accel")] public Accel Accel;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        public AccelWithCovariance()
        {
            Accel = new Accel();
            Covariance = new double[36];
        }
        
        public AccelWithCovariance(Accel Accel, double[] Covariance)
        {
            this.Accel = Accel;
            this.Covariance = Covariance;
        }
        
        public AccelWithCovariance(ref ReadBuffer b)
        {
            Accel = new Accel(ref b);
            b.DeserializeStructArray(36, out Covariance);
        }
        
        public AccelWithCovariance(ref ReadBuffer2 b)
        {
            Accel = new Accel(ref b);
            b.DeserializeStructArray(36, out Covariance);
        }
        
        public AccelWithCovariance RosDeserialize(ref ReadBuffer b) => new AccelWithCovariance(ref b);
        
        public AccelWithCovariance RosDeserialize(ref ReadBuffer2 b) => new AccelWithCovariance(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Accel.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Accel.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Accel is null) BuiltIns.ThrowNullReference();
            Accel.RosValidate();
            if (Covariance is null) BuiltIns.ThrowNullReference();
            if (Covariance.Length != 36) BuiltIns.ThrowInvalidSizeForFixedArray(Covariance.Length, 36);
        }
    
        public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Accel.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Covariance, 36);
        }
    
        public const string MessageType = "geometry_msgs/AccelWithCovariance";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ad5a718d699c6be72a02b8d6a139f334";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
