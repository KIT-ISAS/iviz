/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class MagneticField : IDeserializableRos1<MagneticField>, IDeserializableRos2<MagneticField>, IMessageRos1, IMessageRos2
    {
        // Measurement of the Magnetic Field vector at a specific location.
        // If the covariance of the measurement is known, it should be filled in
        // (if all you know is the variance of each measurement, e.g. from the datasheet,
        //just put those along the diagonal)
        // A covariance matrix of all zeros will be interpreted as "covariance unknown",
        // and to use the data a covariance will have to be assumed or gotten from some
        // other source
        /// <summary> Timestamp is the time the </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // field was measured
        // frame_id is the location and orientation
        // of the field measurement
        /// <summary> X, y, and z components of the </summary>
        [DataMember (Name = "magnetic_field")] public GeometryMsgs.Vector3 MagneticField_;
        // field vector in Tesla
        // If your sensor does not output 3 axes,
        // put NaNs in the components not reported.
        /// <summary> Row major about x, y, z axes </summary>
        [DataMember (Name = "magnetic_field_covariance")] public double[/*9*/] MagneticFieldCovariance;
        // 0 is interpreted as variance unknown
    
        /// Constructor for empty message.
        public MagneticField()
        {
            MagneticFieldCovariance = new double[9];
        }
        
        /// Explicit constructor.
        public MagneticField(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MagneticField_, double[] MagneticFieldCovariance)
        {
            this.Header = Header;
            this.MagneticField_ = MagneticField_;
            this.MagneticFieldCovariance = MagneticFieldCovariance;
        }
        
        /// Constructor with buffer.
        public MagneticField(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out MagneticField_);
            b.DeserializeStructArray(9, out MagneticFieldCovariance);
        }
        
        /// Constructor with buffer.
        public MagneticField(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out MagneticField_);
            b.DeserializeStructArray(9, out MagneticFieldCovariance);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MagneticField(ref b);
        
        public MagneticField RosDeserialize(ref ReadBuffer b) => new MagneticField(ref b);
        
        public MagneticField RosDeserialize(ref ReadBuffer2 b) => new MagneticField(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in MagneticField_);
            b.SerializeStructArray(MagneticFieldCovariance, 9);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in MagneticField_);
            b.SerializeStructArray(MagneticFieldCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (MagneticFieldCovariance is null) BuiltIns.ThrowNullReference();
            if (MagneticFieldCovariance.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(MagneticFieldCovariance.Length, 9);
        }
    
        public int RosMessageLength => 96 + Header.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, MagneticField_);
            WriteBuffer2.AddLength(ref c, MagneticFieldCovariance, 9);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/MagneticField";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2f3b0b43eed0c9501de0fa3ff89a45aa";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VVXYvbRhR916+4RA/ZDVq3ZEugC30ohLT7sCE0S19KMdfSlTSJNKPOjGxrf33PHVle" +
                "sTTgQmsMtuV7z/2Yc85QTg/CYfTSi43kaoqt0AM3VqIp6YORrqK9lNF54khMYZDS1PircyVH4+wmyyin" +
                "+zmxdHv2hm0pC1S/QjeBvlp3sAWZSKF1I7B3QrXpOqnIWAW6MjVx19HkxhSsSYqzxhUu2zVwQbJpNlR7" +
                "16fYiiOHViQWQPwyhkjDGPGPCwJsZ5s5ynDjLHfXWvbndes9R2+OWkk7eRLvAh3QpDZrbBQ/eInomAO9" +
                "WqWNNk33SqsS24qioxEll5awvVV0Amx5LxoGYA5h7AGKRTcuRrHzPMH1ongOKB6/Rl9KhpX/KlzhQTt/" +
                "fOOVUzS9hMj9sOxRH+iX7Fs5LxHqRIEDZj1tvLo81XMvW1MttRfKpOU4b3B06ffFgCdOzS2tCIB9NIJF" +
                "RT9t+9CE735PjL3FSc5E3s4pOR0LmopU/wmH0Q/OIj2cgP/lTk6yMJYeJXR8cTa0AnbjMMUG5FdOAlkH" +
                "8Y1ReXpLfJRQXAynOR/5Y9BGZg2ex1JUL4PzYKvqtO4cx3c//PHjny82s10RM6ffILuev6jkd2jqtLSn" +
                "1NfFbX2vp/5CLS+lkv30H7+yh8+/3FGI1UyDWSRZTp8jjpy9kiZy0mKN6VrTQFQ3neylo6QStJn+jdMg" +
                "YYPEx1anCKCXFQ83mFTRSdlYcz9aA0bLs8qWfGTiNJgG9tjx2LFHvPOVsRqedKHoeAf5axRdyf37O8TY" +
                "IOUYDRqagFB6cNzAr+7fUzZimbdvNSHLHw/uRnfbQPvPEo8tJ5OVI1YeQtr5HWq8mYfbABvLEVSpAl2l" +
                "Z1v8DNeEImgBTIGxXqHzTxPc0j4b764TBS45OfVrTXp9vUK2CdqydQv8jPhc4xJYe8bVmW5anFmn04ex" +
                "wQIROHi3NxVCd9NM9U49hDqz8+ynLHlbKpnlH3THCEJWOhF8wmFdaVi5eDCxzQJcHuiLS2X/Exv/0ZkW" +
                "akGdOKokVl4ZSu0FkwxcykZJcp+O1VmQAq6HicG/cyYSK+ORqpcxUMULyC3pkl28BRg9f5VkOenC4WEA" +
                "GIju2cK7kivjMVKu9C4t6NDiBkpRuqPE6KQB3PzeNPD0lIlC/TmZ6TRcQbF+O19wqee5mBpsTt7Nnn+9" +
                "OfkgrhbMkAxxll66DZe+EkWic8Vyk6Y+1gv95CAErCUEbvR6DhGih9mdvI6O52/T+dtT9jfL5/4J+AgA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
