/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class LaserScan : IDeserializable<LaserScan>, IMessage
    {
        // Single scan from a planar laser range-finder
        //
        // If you have another ranging device with different behavior (e.g. a sonar
        // array), please find or create a different message, since applications
        // will make fairly laser-specific assumptions about this data
        /// <summary> Timestamp in the header is the acquisition time of </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // the first ray in the scan.
        //
        // in frame frame_id, angles are measured around 
        // the positive Z axis (counterclockwise, if Z is up)
        // with zero angle being forward along the x axis
        /// <summary> Start angle of the scan [rad] </summary>
        [DataMember (Name = "angle_min")] public float AngleMin;
        /// <summary> End angle of the scan [rad] </summary>
        [DataMember (Name = "angle_max")] public float AngleMax;
        /// <summary> Angular distance between measurements [rad] </summary>
        [DataMember (Name = "angle_increment")] public float AngleIncrement;
        /// <summary> Time between measurements [seconds] - if your scanner </summary>
        [DataMember (Name = "time_increment")] public float TimeIncrement;
        // is moving, this will be used in interpolating position
        // of 3d points
        /// <summary> Time between scans [seconds] </summary>
        [DataMember (Name = "scan_time")] public float ScanTime;
        /// <summary> Minimum range value [m] </summary>
        [DataMember (Name = "range_min")] public float RangeMin;
        /// <summary> Maximum range value [m] </summary>
        [DataMember (Name = "range_max")] public float RangeMax;
        /// <summary> Range data [m] (Note: values < range_min or > range_max should be discarded) </summary>
        [DataMember (Name = "ranges")] public float[] Ranges;
        /// <summary> Intensity data [device-specific units].  If your </summary>
        [DataMember (Name = "intensities")] public float[] Intensities;
        // device does not provide intensities, please leave
        // the array empty.
    
        /// Constructor for empty message.
        public LaserScan()
        {
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        /// Constructor with buffer.
        public LaserScan(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out AngleMin);
            b.Deserialize(out AngleMax);
            b.Deserialize(out AngleIncrement);
            b.Deserialize(out TimeIncrement);
            b.Deserialize(out ScanTime);
            b.Deserialize(out RangeMin);
            b.Deserialize(out RangeMax);
            b.DeserializeStructArray(out Ranges);
            b.DeserializeStructArray(out Intensities);
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
            if (Ranges is null) BuiltIns.ThrowNullReference();
            if (Intensities is null) BuiltIns.ThrowNullReference();
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/LaserScan";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "90c7ef2dc6895d81024acba2ac42f369";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
