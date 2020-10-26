/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/LaserScan")]
    public sealed class LaserScan : IMessage, IDeserializable<LaserScan>
    {
        // Single scan from a planar laser range-finder
        //
        // If you have another ranging device with different behavior (e.g. a sonar
        // array), please find or create a different message, since applications
        // will make fairly laser-specific assumptions about this data
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp in the header is the acquisition time of 
        // the first ray in the scan.
        //
        // in frame frame_id, angles are measured around 
        // the positive Z axis (counterclockwise, if Z is up)
        // with zero angle being forward along the x axis
        [DataMember (Name = "angle_min")] public float AngleMin { get; set; } // start angle of the scan [rad]
        [DataMember (Name = "angle_max")] public float AngleMax { get; set; } // end angle of the scan [rad]
        [DataMember (Name = "angle_increment")] public float AngleIncrement { get; set; } // angular distance between measurements [rad]
        [DataMember (Name = "time_increment")] public float TimeIncrement { get; set; } // time between measurements [seconds] - if your scanner
        // is moving, this will be used in interpolating position
        // of 3d points
        [DataMember (Name = "scan_time")] public float ScanTime { get; set; } // time between scans [seconds]
        [DataMember (Name = "range_min")] public float RangeMin { get; set; } // minimum range value [m]
        [DataMember (Name = "range_max")] public float RangeMax { get; set; } // maximum range value [m]
        [DataMember (Name = "ranges")] public float[] Ranges { get; set; } // range data [m] (Note: values < range_min or > range_max should be discarded)
        [DataMember (Name = "intensities")] public float[] Intensities { get; set; } // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Constructor for empty message. </summary>
        public LaserScan()
        {
            Header = new StdMsgs.Header();
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LaserScan(StdMsgs.Header Header, float AngleMin, float AngleMax, float AngleIncrement, float TimeIncrement, float ScanTime, float RangeMin, float RangeMax, float[] Ranges, float[] Intensities)
        {
            this.Header = Header;
            this.AngleMin = AngleMin;
            this.AngleMax = AngleMax;
            this.AngleIncrement = AngleIncrement;
            this.TimeIncrement = TimeIncrement;
            this.ScanTime = ScanTime;
            this.RangeMin = RangeMin;
            this.RangeMax = RangeMax;
            this.Ranges = Ranges;
            this.Intensities = Intensities;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LaserScan(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            AngleMin = b.Deserialize<float>();
            AngleMax = b.Deserialize<float>();
            AngleIncrement = b.Deserialize<float>();
            TimeIncrement = b.Deserialize<float>();
            ScanTime = b.Deserialize<float>();
            RangeMin = b.Deserialize<float>();
            RangeMax = b.Deserialize<float>();
            Ranges = b.DeserializeStructArray<float>();
            Intensities = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LaserScan(ref b);
        }
        
        LaserScan IDeserializable<LaserScan>.RosDeserialize(ref Buffer b)
        {
            return new LaserScan(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(AngleMin);
            b.Serialize(AngleMax);
            b.Serialize(AngleIncrement);
            b.Serialize(TimeIncrement);
            b.Serialize(ScanTime);
            b.Serialize(RangeMin);
            b.Serialize(RangeMax);
            b.SerializeStructArray(Ranges, 0);
            b.SerializeStructArray(Intensities, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Ranges is null) throw new System.NullReferenceException(nameof(Ranges));
            if (Intensities is null) throw new System.NullReferenceException(nameof(Intensities));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += Header.RosMessageLength;
                size += 4 * Ranges.Length;
                size += 4 * Intensities.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/LaserScan";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
