/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LaserScan : IDeserializable<LaserScan>, IMessage
    {
        // Single scan from a planar laser range-finder
        //
        // If you have another ranging device with different behavior (e.g. a sonar
        // array), please find or create a different message, since applications
        // will make fairly laser-specific assumptions about this data
        /// timestamp in the header is the acquisition time of
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // the first ray in the scan.
        //
        // in frame frame_id, angles are measured around 
        // the positive Z axis (counterclockwise, if Z is up)
        // with zero angle being forward along the x axis
        /// start angle of the scan [rad]
        [DataMember (Name = "angle_min")] public float AngleMin;
        /// end angle of the scan [rad]
        [DataMember (Name = "angle_max")] public float AngleMax;
        /// angular distance between measurements [rad]
        [DataMember (Name = "angle_increment")] public float AngleIncrement;
        /// time between measurements [seconds] - if your scanner
        [DataMember (Name = "time_increment")] public float TimeIncrement;
        // is moving, this will be used in interpolating position
        // of 3d points
        /// time between scans [seconds]
        [DataMember (Name = "scan_time")] public float ScanTime;
        /// minimum range value [m]
        [DataMember (Name = "range_min")] public float RangeMin;
        /// maximum range value [m]
        [DataMember (Name = "range_max")] public float RangeMax;
        /// range data [m] (Note: values < range_min or > range_max should be discarded)
        [DataMember (Name = "ranges")] public float[] Ranges;
        /// intensity data [device-specific units].  If your
        [DataMember (Name = "intensities")] public float[] Intensities;
        // device does not provide intensities, please leave
        // the array empty.
    
        /// Constructor for empty message.
        public LaserScan()
        {
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public LaserScan(in StdMsgs.Header Header, float AngleMin, float AngleMax, float AngleIncrement, float TimeIncrement, float ScanTime, float RangeMin, float RangeMax, float[] Ranges, float[] Intensities)
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
        
        /// Constructor with buffer.
        internal LaserScan(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LaserScan(ref b);
        
        public LaserScan RosDeserialize(ref ReadBuffer b) => new LaserScan(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(AngleMin);
            b.Serialize(AngleMax);
            b.Serialize(AngleIncrement);
            b.Serialize(TimeIncrement);
            b.Serialize(ScanTime);
            b.Serialize(RangeMin);
            b.Serialize(RangeMax);
            b.SerializeStructArray(Ranges);
            b.SerializeStructArray(Intensities);
        }
        
        public void RosValidate()
        {
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/LaserScan";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
