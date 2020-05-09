using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Imu : IMessage
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
        
        public std_msgs.Header header { get; set; }
        
        public geometry_msgs.Quaternion orientation { get; set; }
        public double[/*9*/] orientation_covariance { get; set; } // Row major about x, y, z axes
        
        public geometry_msgs.Vector3 angular_velocity { get; set; }
        public double[/*9*/] angular_velocity_covariance { get; set; } // Row major about x, y, z axes
        
        public geometry_msgs.Vector3 linear_acceleration { get; set; }
        public double[/*9*/] linear_acceleration_covariance { get; set; } // Row major x, y z 
    
        /// <summary> Constructor for empty message. </summary>
        public Imu()
        {
            header = new std_msgs.Header();
            orientation_covariance = new double[9];
            angular_velocity_covariance = new double[9];
            linear_acceleration_covariance = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
        public Imu(std_msgs.Header header, geometry_msgs.Quaternion orientation, double[] orientation_covariance, geometry_msgs.Vector3 angular_velocity, double[] angular_velocity_covariance, geometry_msgs.Vector3 linear_acceleration, double[] linear_acceleration_covariance)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.orientation = orientation;
            BuiltIns.AssertSize(orientation_covariance, 9);
            this.orientation_covariance = orientation_covariance;
            this.angular_velocity = angular_velocity;
            BuiltIns.AssertSize(angular_velocity_covariance, 9);
            this.angular_velocity_covariance = angular_velocity_covariance;
            this.linear_acceleration = linear_acceleration;
            BuiltIns.AssertSize(linear_acceleration_covariance, 9);
            this.linear_acceleration_covariance = linear_acceleration_covariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Imu(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.orientation = new geometry_msgs.Quaternion(b);
            this.orientation_covariance = BuiltIns.DeserializeStructArray<double>(b, 9);
            this.angular_velocity = new geometry_msgs.Vector3(b);
            this.angular_velocity_covariance = BuiltIns.DeserializeStructArray<double>(b, 9);
            this.linear_acceleration = new geometry_msgs.Vector3(b);
            this.linear_acceleration_covariance = BuiltIns.DeserializeStructArray<double>(b, 9);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Imu(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            this.orientation.Serialize(b);
            BuiltIns.Serialize(this.orientation_covariance, b, 9);
            this.angular_velocity.Serialize(b);
            BuiltIns.Serialize(this.angular_velocity_covariance, b, 9);
            this.linear_acceleration.Serialize(b);
            BuiltIns.Serialize(this.linear_acceleration_covariance, b, 9);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            BuiltIns.AssertSize(orientation_covariance, 9);
            BuiltIns.AssertSize(angular_velocity_covariance, 9);
            BuiltIns.AssertSize(linear_acceleration_covariance, 9);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 296;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/Imu";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "6a62c6daae103f4ff57a132d6f95cec2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTW/bRhC981cMooOlQpIbpwhQAz0UCNrqYCBtkl6KVBiRI3JjclfZXVqWf33e7IoS" +
                "7SRogX4IAkSRO+/NvPnihN42JhC+TJ2EwLVQdNS4tqKKI9PWu47Y0urmHU1XVnw03NKNcOi9dGIjvbMm" +
                "zopJMaEfy1Ja8RyNs4FC43qgbISMpe4y/HlFU+ui/qsvwmwO1Iq8i+k4MO+kdaWJh8eGnqvLIGXCX20p" +
                "NkKlu2Nv2JZCLt/pRu4glFvr9nZOJo6gtqZtpVLEqdkSty0dXJ9OqoWCgGCMK1w2Y+A5ybJeZj30uKoT" +
                "GhE8+NCHSLs+4r4LAnBn63zGcK2xzVScsd8dR2/ulUY9eRDvAu3hYQ46it95iXCXAz0bmfU2hfYsa4c8" +
                "9aADEeBTsnjMkfAavksJBS6H0HfAdJ5qF6PYHEtwHeIFiMdl70sZpFZ9krl1JCEa+AwZYe3sSfjEipyr" +
                "QIGmSSHY+VQulZNgL6CMd1UPh1BFzhucTBlXvQdYFMOuhdRCQeKAR98OLPAclcEqyOcaIrjF87PH7EcK" +
                "mpQH5PdY2ieaspHyNgXDSHrbp4AWz7U8lHBrfDj7MRQDSD6jz5moTPBSs6+eujsEuCyKX4QraNykn6Ko" +
                "BbpHf1h3oQ6Xv/Y45K3KMpKo2LaO48vv/vj+/fj2euTFhH5DBXf8QUPZONQgXDrM6YH4XsJTmt+ljM6/" +
                "gM9137JfDy03Jnr67J+ztcYKAHk0HcaEX3j8NU5lA1lR/PAvf4qbNz9fU4hV9jznCvl+E5FezSvC4jwP" +
                "4UZjarTLohVoBCPudnKclvGwk7CE4TBVa8HIRI8ftFVTy5au63prSu0mFIc8soclSpBpx5izpeYB552v" +
                "jE3N57kTRcc3yMdeVJ/Vq2ucsZiRfTRw6ACE0qPKtfhXr6jo0Q4vrtSgmLzdu4V2R41KPJGjaDnNTblH" +
                "04SQ5s41OL7JwS2BDXEELBWaPN1b42+YEUjgguwc2mMKz18fMAJzD6UEblpR4JLT8L1Qo4vZCNkmaMvW" +
                "DfAZ8czxd2DtCVdjWjTIWavRhx49qQcxge5MhaObQ94grTYTKm/j2R8KtcqUxeQn1TgPjZQR3Yznft6b" +
                "2BQBjQ/0lI21qf6ravzahBiqy4tmKw3eJ5PV6GgXhLTjMi3Rj+fxgvrtMI2O/Uf3p6vTEKCH09X+/4nt" +
                "OCi+FBjeCvTZ45CW2gCrVLLOouCxpZFN9NbJEoaV8TBFyEugihcELumdQNcSdloERse3unJsSDuSdzuA" +
                "oYk929BmKXEbJmmxzWnfYGmmU5r/1K2pv01J3tSmypaq8MmY6RjcnOL2Ku/k5HMmy7t7eAmaLYcttteA" +
                "0iLNYyUt8MGvVP7Ruflo/T8W9LVDk5/e54wNEQPtL7P+CaR+DzoMCgAA";
                
    }
}
