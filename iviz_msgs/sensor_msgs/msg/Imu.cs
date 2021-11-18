/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Imu")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public Imu()
        {
            OrientationCovariance = new double[9];
            AngularVelocityCovariance = new double[9];
            LinearAccelerationCovariance = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Imu(ref b);
        }
        
        Imu IDeserializable<Imu>.RosDeserialize(ref Buffer b)
        {
            return new Imu(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Orientation);
            b.SerializeStructArray(OrientationCovariance, 9);
            b.Serialize(AngularVelocity);
            b.SerializeStructArray(AngularVelocityCovariance, 9);
            b.Serialize(LinearAcceleration);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Imu";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a62c6daae103f4ff57a132d6f95cec2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W227jRgx9F+B/INYPiQvb6WaLAg3QhwUWbf0QYLuXvhStQUu0NBtpxjsziuN8fQ9n" +
                "IltJs2iBXgID0YU8JA95OJrSh8YEwo+pkxC4FoqOGtdWVHFk2nrXEVtaXX+k85UVHw23dC0cei+d2Egf" +
                "rYmzYlpM6XVZSiueo3E2UGhcD5SNkLHUXYTfL+ncuqh39VmYzYFakXcxmQPzVlpXmnh47Oi5ughSJvzV" +
                "lmIjVLpb9oZtKeTyk26UDkq5sW5v52TiCGpr2lYqRTw3W+K2pYPrk6V6KAgCjHGFy2YMPCdZ1svMh5or" +
                "O6ERwYtPfYi06yOeuyAAd7bONoZrrQ300Otx3h1Hb+40jGZyL94F2iPDXHQUv/MSkS4HejFy620q7UXm" +
                "Dn3qEQ6BAJ+axeMYCa/h29RQ4HIIfQdM56l2MYrNtQTXoV6AeFz2vpSBauUnuVtHEqJBzqAR3s4eiU9R" +
                "0XMlKNB5Ygh+Po1L5STYMzDjXdUjIUyR8waWqePK9wCLYdi1oFooSBzw6OshCjLHZMCueoZDFLd4ecqY" +
                "/YhBk/qA/j6M9jFM2Uh5k4phNL3tU0GLlzoeGnBrfDjlMQwDgvwpfO5EZYKXmn31NN2hwGVR/CRcgeMm" +
                "/SuKWsB79Id1F+pw8XMPI2+VlhFFxbZ1HL/95tfvfhs/Xo+ymNI7THDHn7SUjcMMIqXDnO6J7yQ8DfOL" +
                "lNH5V8i57lv260Fy40BP3/3zaK2xAkAebYdxwGdefymmRkOwYlJ8/y//TYrr9z9eUYhVzj13a4KWv4/o" +
                "sLYWlXFeicikMTUUs2gFNMGLux26nd7Gw07Csjgt1lqwNSHzg6o1qbZ0XddbU6qgMB/yyB+emEKmHWPV" +
                "ltoK2DtfGZv057nDNE3VLMjnXpSi1Zsr2FisyT4aJHQAQukx6Dr/qzdU9FDEq0t1KKYf9m6hAqkxjMfg" +
                "mFtOq1PuoJugeXK4QoyvcnFLYIMdQZQKOk/P1rgNM0IQpCA7B4WcI/O3B2zBLKPUw00LQQYqwQBQz9Tp" +
                "bDZC1rSvyLJ1A3xGPMX4O7D2iKs1LRr0DHNVU+ghSzXEEro1FUw3h3yItKonDN/Gsz8U6pVDFtMflOO8" +
                "N1JH9HA8SXpvYlMEaB/oqRtrU/13A/mlNTEZBsyLNiyt3yf7FaRtvaCqHYNO3H0+LRmMcIed9KBCujte" +
                "HVcB3R+v9v9XeQ8L49na8HmgLx9XtVQZrNLgOouxx3GNnkJhR084VsbDFVUvgSpeULukjwM9n3C4RWB0" +
                "fKNnD6aI4M27HcAgZc82tJlNPIZLOuHmtG9weiYrnYKk2aRyU5I3tYHI1VNJPjozPVQ3p7i9zIdzyjkH" +
                "y4f48DU0Ww7H2V4LSidqXi7pJB/ySiKIzs1H3wGPGX3rIPXjh52xIWKv/VXjJ8UfzapddhYKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
