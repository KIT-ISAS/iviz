/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Imu : IDeserializable<Imu>, IMessage
    {
        // This is a message to hold data from an IMU (Inertial Measurement Unit)
        //
        // Accelerations should be in m/s^2 (not in g's), and rotational velocity should be in rad/sec
        //
        // If the covariance of the measurement is known, it should be filled in (if all you know is the 
        // variance of each measurement, e.g. from the datasheet, just put those along the diagonal)
        // A covariance matrix of all zeros will be interpreted as "covariance unknown", and to use the
        // data a covariance will have to be assumed or gotten from some other source
        //
        // If you have no estimate for one of the data elements (e.g. your IMU doesn't produce an orientation 
        // estimate), please set element 0 of the associated covariance matrix to -1
        // If you are interpreting this message, please check for a value of -1 in the first element of each 
        // covariance matrix, and disregard the associated estimate.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        [DataMember (Name = "orientation_covariance")] public double[/*9*/] OrientationCovariance; // Row major about x, y, z axes
        [DataMember (Name = "angular_velocity")] public GeometryMsgs.Vector3 AngularVelocity;
        [DataMember (Name = "angular_velocity_covariance")] public double[/*9*/] AngularVelocityCovariance; // Row major about x, y, z axes
        [DataMember (Name = "linear_acceleration")] public GeometryMsgs.Vector3 LinearAcceleration;
        [DataMember (Name = "linear_acceleration_covariance")] public double[/*9*/] LinearAccelerationCovariance; // Row major x, y z 
    
        /// Constructor for empty message.
        public Imu()
        {
            OrientationCovariance = new double[9];
            AngularVelocityCovariance = new double[9];
            LinearAccelerationCovariance = new double[9];
        }
        
        /// Explicit constructor.
        public Imu(in StdMsgs.Header Header, in GeometryMsgs.Quaternion Orientation, double[] OrientationCovariance, in GeometryMsgs.Vector3 AngularVelocity, double[] AngularVelocityCovariance, in GeometryMsgs.Vector3 LinearAcceleration, double[] LinearAccelerationCovariance)
        {
            this.Header = Header;
            this.Orientation = Orientation;
            this.OrientationCovariance = OrientationCovariance;
            this.AngularVelocity = AngularVelocity;
            this.AngularVelocityCovariance = AngularVelocityCovariance;
            this.LinearAcceleration = LinearAcceleration;
            this.LinearAccelerationCovariance = LinearAccelerationCovariance;
        }
        
        /// Constructor with buffer.
        internal Imu(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Orientation);
            OrientationCovariance = b.DeserializeStructArray<double>(9);
            b.Deserialize(out AngularVelocity);
            AngularVelocityCovariance = b.DeserializeStructArray<double>(9);
            b.Deserialize(out LinearAcceleration);
            LinearAccelerationCovariance = b.DeserializeStructArray<double>(9);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Imu(ref b);
        
        Imu IDeserializable<Imu>.RosDeserialize(ref Buffer b) => new Imu(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Orientation);
            b.SerializeStructArray(OrientationCovariance, 9);
            b.Serialize(ref AngularVelocity);
            b.SerializeStructArray(AngularVelocityCovariance, 9);
            b.Serialize(ref LinearAcceleration);
            b.SerializeStructArray(LinearAccelerationCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (OrientationCovariance is null) throw new System.NullReferenceException(nameof(OrientationCovariance));
            if (OrientationCovariance.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(OrientationCovariance), OrientationCovariance.Length, 9);
            if (AngularVelocityCovariance is null) throw new System.NullReferenceException(nameof(AngularVelocityCovariance));
            if (AngularVelocityCovariance.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(AngularVelocityCovariance), AngularVelocityCovariance.Length, 9);
            if (LinearAccelerationCovariance is null) throw new System.NullReferenceException(nameof(LinearAccelerationCovariance));
            if (LinearAccelerationCovariance.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(LinearAccelerationCovariance), LinearAccelerationCovariance.Length, 9);
        }
    
        public int RosMessageLength => 296 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/Imu";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6a62c6daae103f4ff57a132d6f95cec2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW/bRhC981cMooPlQpIbpyhQAz0ECNrqYCBtkl6KVhiRI3JjclfZXVqmf33e7JoS" +
                "7TpogX4YAsyPnTczb+bNcEbvGxMIP6ZOQuBaKDpqXFtRxZFp511HbGl9/YHmays+Gm7pWjj0XjqxkT5Y" +
                "E8+LWTGj12UprXiOxtlAoXE9ULZCxlJ3Ef64pLl1Ue/qs3C+AGpF3sV0HJi30rrSxOGxoefqIkiZ8Nc7" +
                "io1Q6W7ZG7alkMtPukk4SOXGuoNdkIkTqJ1pW6kUcW52xG1Lg+vTSbVQEDiY4gqXzRR4QbKqV5kPPa7s" +
                "hEYELz72IdK+j3juggDc2TqfMVxrbqCHXk/j7jh6c6duNJJ78S7QARHmpKP4vZeIcDnQi4lZb1NqLzJ3" +
                "qFMPd3AE+FQsnvpIeA3fpoICl0PoO2A6T7WLUWzOJbgO+QLE47L3pYxUKz/J3DqSEA1iBo2wdvZIfPKK" +
                "mitBgeaJIdj51C6Vk2DPwIx3VY+A0EXOG5xMFVe+R1g0w74F1UJB4ohHX49eEDk6A+eqZzhEcsuXp4jZ" +
                "Txg0qQ6o70NrH92UjZQ3KRlG0ds+JbR8qe2hDnfGh1McYzPAyZ/c50pUJnip2VdPwx0TXBXFT8IVOG7S" +
                "v6KoBbxHP2y6UIeLn3sc8lZpmVBU7FrH8dtvfvvu9+njzSSKGf2CDu74o6aydehBhDQs6J74TsJTN79K" +
                "GZ1/hZjrvmW/GSU3dfT03T/31horAOTJdJg6fOb1l3yqNzgriu//5b/i+t2PVxRilSPPtUK930WUV+uK" +
                "tDjPQ4TRmBpyWbYCjmDE3R6lTm/jsJewKk5TtRaMTGh8UKkmyZau63prSlUTmkMe2cMSLci0Z8zZUuuA" +
                "885Xxibxee7QSjM9FuRTL8rP+s0VzljMyD4aBDQAofTocm3+9Rsqesjh1aUaFLP3B7dUddToxKNzNC2n" +
                "uSl3EE3QODlcwcdXObkVsEGOwEsFkadnG9yGc4IThCB7B3nMEfnbASMwaygVcNtCjYFKMADUMzU6O58g" +
                "a9hXZNm6ET4jnnz8HVh7xNWclg1qhqaqKfTQpB7EBLo1FY5uh7xBWhUTOm/r2Q+FWmWXxewH5TgPjVQR" +
                "3YwnPR9MbIoA4QM9VWNjqv+qG780Icbu8qLVSoP3yWQFYzsvSGnP4BJ3n07jBf3bYRo96I/ujlfHIUD3" +
                "x6vD/5Pbw6B4LjF8Fei7xymtVADr1LLOouGxpVFNaOtoCcPKeJgi5RVQxQsSl/RNoGsJOy0Co+MbXTno" +
                "H4I17/cAg4g929BmKvEYJmmxLejQYGmmU1r/pNakb1OSN7WBvNVSGT4aMz0kt6C4u8w7OcWcneXdPX4E" +
                "na/GLXbQhNIizWMlLfAxrtT+0bnFZP0/JvStg8iP33PGhoiB9pdV/wykfg86DAoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
