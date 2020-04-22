
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
        
        public std_msgs.Header header;
        
        public geometry_msgs.Quaternion orientation;
        public double[/*9*/] orientation_covariance; // Row major about x, y, z axes
        
        public geometry_msgs.Vector3 angular_velocity;
        public double[/*9*/] angular_velocity_covariance; // Row major about x, y, z axes
        
        public geometry_msgs.Vector3 linear_acceleration;
        public double[/*9*/] linear_acceleration_covariance; // Row major x, y z 
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Imu";
    
        public IMessage Create() => new Imu();
    
        public int GetLength()
        {
            int size = 296;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Imu()
        {
            header = new std_msgs.Header();
            orientation_covariance = new double[9];
            angular_velocity_covariance = new double[9];
            linear_acceleration_covariance = new double[9];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            orientation.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out orientation_covariance, ref ptr, end, 9);
            angular_velocity.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out angular_velocity_covariance, ref ptr, end, 9);
            linear_acceleration.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out linear_acceleration_covariance, ref ptr, end, 9);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            orientation.Serialize(ref ptr, end);
            BuiltIns.Serialize(orientation_covariance, ref ptr, end, 9);
            angular_velocity.Serialize(ref ptr, end);
            BuiltIns.Serialize(angular_velocity_covariance, ref ptr, end, 9);
            linear_acceleration.Serialize(ref ptr, end);
            BuiltIns.Serialize(linear_acceleration_covariance, ref ptr, end, 9);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6a62c6daae103f4ff57a132d6f95cec2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
