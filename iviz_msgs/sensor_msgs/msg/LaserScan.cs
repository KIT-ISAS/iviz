using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class LaserScan : IMessage
    {
        // Single scan from a planar laser range-finder
        //
        // If you have another ranging device with different behavior (e.g. a sonar
        // array), please find or create a different message, since applications
        // will make fairly laser-specific assumptions about this data
        
        public std_msgs.Header header { get; set; } // timestamp in the header is the acquisition time of 
        // the first ray in the scan.
        //
        // in frame frame_id, angles are measured around 
        // the positive Z axis (counterclockwise, if Z is up)
        // with zero angle being forward along the x axis
        
        public float angle_min { get; set; } // start angle of the scan [rad]
        public float angle_max { get; set; } // end angle of the scan [rad]
        public float angle_increment { get; set; } // angular distance between measurements [rad]
        
        public float time_increment { get; set; } // time between measurements [seconds] - if your scanner
        // is moving, this will be used in interpolating position
        // of 3d points
        public float scan_time { get; set; } // time between scans [seconds]
        
        public float range_min { get; set; } // minimum range value [m]
        public float range_max { get; set; } // maximum range value [m]
        
        public float[] ranges { get; set; } // range data [m] (Note: values < range_min or > range_max should be discarded)
        public float[] intensities { get; set; } // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Constructor for empty message. </summary>
        public LaserScan()
        {
            header = new std_msgs.Header();
            ranges = System.Array.Empty<float>();
            intensities = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LaserScan(std_msgs.Header header, float angle_min, float angle_max, float angle_increment, float time_increment, float scan_time, float range_min, float range_max, float[] ranges, float[] intensities)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.angle_min = angle_min;
            this.angle_max = angle_max;
            this.angle_increment = angle_increment;
            this.time_increment = time_increment;
            this.scan_time = scan_time;
            this.range_min = range_min;
            this.range_max = range_max;
            this.ranges = ranges ?? throw new System.ArgumentNullException(nameof(ranges));
            this.intensities = intensities ?? throw new System.ArgumentNullException(nameof(intensities));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LaserScan(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.angle_min = b.Deserialize<float>();
            this.angle_max = b.Deserialize<float>();
            this.angle_increment = b.Deserialize<float>();
            this.time_increment = b.Deserialize<float>();
            this.scan_time = b.Deserialize<float>();
            this.range_min = b.Deserialize<float>();
            this.range_max = b.Deserialize<float>();
            this.ranges = b.DeserializeStructArray<float>(0);
            this.intensities = b.DeserializeStructArray<float>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new LaserScan(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.Serialize(this.angle_min);
            b.Serialize(this.angle_max);
            b.Serialize(this.angle_increment);
            b.Serialize(this.time_increment);
            b.Serialize(this.scan_time);
            b.Serialize(this.range_min);
            b.Serialize(this.range_max);
            b.SerializeStructArray(this.ranges, 0);
            b.SerializeStructArray(this.intensities, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (ranges is null) throw new System.NullReferenceException();
            if (intensities is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += header.RosMessageLength;
                size += 4 * ranges.Length;
                size += 4 * intensities.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/LaserScan";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61VTY/jNgy9+1cQyGGTYpIC3dug7Wmx7RxaFJg9dRAMGJmOhZUljyQn4/76Psr5cBed" +
                "7B7WCBIoJh/FR/JxQY/W751QMuypiaEjpt6x50iOk0SK7PeybqyvJVaLakEPDY1hoJYPQuxDbk9GwKFa" +
                "DtYIHW1uqbZNI1F8pp3A2IZIS9nsNwiQAvABxTHyuLpDQEEs0iAEMxOFM8BnEJ2kxHu5o2Q9AnDfO2s4" +
                "2+ATcI7WOer4MyDYRjdOV1+nXoxtrCFOaej6Yk28C0Om3NpENWeuqt+FkRq108/sWVC2iJu568l6uMjZ" +
                "CL56YvMy2GQVt5hSaKiit55F8WlsTBl8jWdIJX5zw+sWoNWSMQKX72db36EiqCayjALOOA1RahzCAGa/" +
                "drU+aC6o6t/Er0hxaeCWJRoXzOejTWDfNniJV0O/uoVW6v+PxDBdBw2gzdGEeOSI67iAk0Z8LYHeRqoa" +
                "Fzi//2mCee6Q7yUG6hLzCR+8n6mkp8j19ktHfr06Cqj4Nje0WpRO2w9u+GtwmIraIrL24E7yUcSfaVa7" +
                "dIK54GhXzGFOPfWGbxITfJ22tFaiMWSx3M1j8G41QaIuHEDw3dTVZRh2QkNC6cGY1Rr2wWFaQPtU5OBv" +
                "IYKX9zUs4ZkuqehNnsvlr00zT0Xfz3K4clAE5L+1w8F2Qze9ogO7Qeip237pMq8aDv/rcvZ52k6v0iyP" +
                "yVanXE1p+WfIcj/5Jvp5djFozq+zoKkNg6uVQxTboGWlXs3iKKFeWZyCLS5/jKdYkwpe5WfwNqfthk7S" +
                "ebOaJwWtA8AhrtRH1LaWedCLXuL7IF+b6qKxJJC/cVNVv3znp/rj8bd7zGL93KV9+nHSUijyI2ak1mHv" +
                "JHNhBdNPrd1jW6ydHMRREVa0aHmbx17SBo6ftIXx2Qvanh20vPRxDmRC14FJo6vhIsxn/6qIIVYXRMGa" +
                "MqgmhFhbr+ZFHhUdnyQvg+j8Pny4h41Hww6qek4VWTdP0jF5+EDVAM618eWlWnw6hrWWYA/1v26F3HLW" +
                "y8prH7GgVGrTPWL8MCW3ATbImSaCluW/ZxzT6rTGpA+mpSVu/teY2zAthANHyzvIE4ANGADqO3V6t5oh" +
                "+wLtsX/P8BPiNca3wPoLrua0blEzp9mnYc9ly526D7MwFhDjrOqYs7vIcayKAJSQ1eJjWUSXvareWLvB" +
                "WBSgLhuhSjmWPXBaVlX1L6uTjed+CAAA";
                
    }
}
