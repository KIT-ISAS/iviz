/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/Imu")]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation { get; set; }
        [DataMember (Name = "orientation_covariance")] public double[/*9*/] OrientationCovariance { get; set; } // Row major about x, y, z axes
        [DataMember (Name = "angular_velocity")] public GeometryMsgs.Vector3 AngularVelocity { get; set; }
        [DataMember (Name = "angular_velocity_covariance")] public double[/*9*/] AngularVelocityCovariance { get; set; } // Row major about x, y, z axes
        [DataMember (Name = "linear_acceleration")] public GeometryMsgs.Vector3 LinearAcceleration { get; set; }
        [DataMember (Name = "linear_acceleration_covariance")] public double[/*9*/] LinearAccelerationCovariance { get; set; } // Row major x, y z 
    
        /// <summary> Constructor for empty message. </summary>
        public Imu()
        {
            Header = new StdMsgs.Header();
            OrientationCovariance = new double[9];
            AngularVelocityCovariance = new double[9];
            LinearAccelerationCovariance = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
        public Imu(StdMsgs.Header Header, in GeometryMsgs.Quaternion Orientation, double[] OrientationCovariance, in GeometryMsgs.Vector3 AngularVelocity, double[] AngularVelocityCovariance, in GeometryMsgs.Vector3 LinearAcceleration, double[] LinearAccelerationCovariance)
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
        public Imu(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Orientation = new GeometryMsgs.Quaternion(ref b);
            OrientationCovariance = b.DeserializeStructArray<double>(9);
            AngularVelocity = new GeometryMsgs.Vector3(ref b);
            AngularVelocityCovariance = b.DeserializeStructArray<double>(9);
            LinearAcceleration = new GeometryMsgs.Vector3(ref b);
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
            Orientation.RosSerialize(ref b);
            b.SerializeStructArray(OrientationCovariance, 9);
            AngularVelocity.RosSerialize(ref b);
            b.SerializeStructArray(AngularVelocityCovariance, 9);
            LinearAcceleration.RosSerialize(ref b);
            b.SerializeStructArray(LinearAccelerationCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (OrientationCovariance is null) throw new System.NullReferenceException(nameof(OrientationCovariance));
            if (OrientationCovariance.Length != 9) throw new System.IndexOutOfRangeException();
            if (AngularVelocityCovariance is null) throw new System.NullReferenceException(nameof(AngularVelocityCovariance));
            if (AngularVelocityCovariance.Length != 9) throw new System.IndexOutOfRangeException();
            if (LinearAccelerationCovariance is null) throw new System.NullReferenceException(nameof(LinearAccelerationCovariance));
            if (LinearAccelerationCovariance.Length != 9) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 296;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Imu";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a62c6daae103f4ff57a132d6f95cec2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bRwy9C9B/IKKDrUKSG6coUAM9BAja6mAgbZJeilagdqndiXdnNjOzluVf38cZ" +
                "fawcB8ghrSHA+0E+ko98nJ3Q+9oEwo+plRC4EoqOateUVHJk2njXElta3n6gy6UVHw03dCscei+t2Egf" +
                "rInT8WiCH70uCmnEczTOBgq164GzFjKW2qvwzzVdWhf1rroI0xlwS/IuJnOg3kvjChN3546ey6sgxT7C" +
                "ckOxFircPXvDthBy+Uk7SAnl3Fm3tTMycQC2MU0jpWJemg1x09DO9clSPRREIwyBhYt6iDwjWVSLTIra" +
                "K0WhFsGLj32I1PURz10QoDtbZRvDlZanHNHrYeYtR28eNI7m8ijeBdoix1x4FN95iUiYA70YuPU2Ffci" +
                "84du9YiHSIqfesbDIAmw5vvUVwBzCH0LUOepcjGKzdUE16JioHhc9r6QE99KUgKwjiREg7TBJfydPbKf" +
                "4qL1SlKgy8QS/Hyam9JJsBdgx7uyR0oYJ+cNLFPjE+kHXAxF14BvoSDxAEjfH8IgeUwI7MpneER985eD" +
                "nNkPaDSpG2jzfsqPcYpairtUDqP1TZ9Kmr/UKdGIG+PDKZHDSGiUzxLI/ShN8FKxL58mfChxMR6NR78J" +
                "l6C6Tv/0QSVoQPS7VRuqcPV7D0NvlZ0BU+PRpnEcf/zhr5/+Hj5fDVKZ0B8Y55Y/akFrh3lEXrsZPRI/" +
                "SPg80p9SROdfIfWqb9ivDho8i/X05bcI2BgrgOTBxjiL+cz7L4XVgIinsX7+xn/j0e27X28oxDJnn/um" +
                "7X8X0W1tM2rjvCqRSm0qSGjeCKiCF7cdOp/exl0nAb2fHDduJVinUP5OBZyEXLi27a0pVGAYFjkDUFcM" +
                "JVPHWMKFNgQOzpfGJkF6bnW2JskwyKdelKflmxtYWSzQPhoktQNG4TH5KojlGxj30Mira/WA4/utm6tm" +
                "KgznMQNMMqelKg+QUtBkOdxomO9yjQvAgyRBoBLqT89WuA1TQhxkIZ2Dai6R/tsd9mOWVurluoFIAxXg" +
                "AbAX6nQxHUJr6jdk2boDfoY8BfkaXHsC1rLmNZqHCaso9NCqWmI53ZsStutdPmEa1RfGcO3ZQw7qloMC" +
                "5BclO++T1Bs9P09K35pYj0cBOwEBUl9WpvwPp/NLu+M4bF60b2k1P9m9YG7jBYV1DE5x9+m0eTDPbVpW" +
                "e1XSw+nytB/o8XS5/d+K3G+RZyvEd4S+PK9tkWSxTGPsLGSAUx3theiOrvAsjYcvil8AVryAAkkfEXqE" +
                "4fyLCtLynR5PGCl1564DGvTt2YYms4rH8Emn4Iy2Nc7YZKXjkHWcpG8K8qYyUL66KttHb6Z9gTOKm+t8" +
                "hqesc7T9YX/4dJouDkfeVmtK527eOenEP2SWNBGdmw0/GM5pfesg/uOHoLEhYt993Qz8C2xZqz9KCgAA";
                
    }
}
