namespace Iviz.Msgs.sensor_msgs
{
    public sealed class LaserScan : IMessage
    {
        // Single scan from a planar laser range-finder
        //
        // If you have another ranging device with different behavior (e.g. a sonar
        // array), please find or create a different message, since applications
        // will make fairly laser-specific assumptions about this data
        
        public std_msgs.Header header; // timestamp in the header is the acquisition time of 
        // the first ray in the scan.
        //
        // in frame frame_id, angles are measured around 
        // the positive Z axis (counterclockwise, if Z is up)
        // with zero angle being forward along the x axis
        
        public float angle_min; // start angle of the scan [rad]
        public float angle_max; // end angle of the scan [rad]
        public float angle_increment; // angular distance between measurements [rad]
        
        public float time_increment; // time between measurements [seconds] - if your scanner
        // is moving, this will be used in interpolating position
        // of 3d points
        public float scan_time; // time between scans [seconds]
        
        public float range_min; // minimum range value [m]
        public float range_max; // maximum range value [m]
        
        public float[] ranges; // range data [m] (Note: values < range_min or > range_max should be discarded)
        public float[] intensities; // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Constructor for empty message. </summary>
        public LaserScan()
        {
            header = new std_msgs.Header();
            ranges = System.Array.Empty<float>();
            intensities = System.Array.Empty<float>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out angle_min, ref ptr, end);
            BuiltIns.Deserialize(out angle_max, ref ptr, end);
            BuiltIns.Deserialize(out angle_increment, ref ptr, end);
            BuiltIns.Deserialize(out time_increment, ref ptr, end);
            BuiltIns.Deserialize(out scan_time, ref ptr, end);
            BuiltIns.Deserialize(out range_min, ref ptr, end);
            BuiltIns.Deserialize(out range_max, ref ptr, end);
            BuiltIns.Deserialize(out ranges, ref ptr, end, 0);
            BuiltIns.Deserialize(out intensities, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(angle_min, ref ptr, end);
            BuiltIns.Serialize(angle_max, ref ptr, end);
            BuiltIns.Serialize(angle_increment, ref ptr, end);
            BuiltIns.Serialize(time_increment, ref ptr, end);
            BuiltIns.Serialize(scan_time, ref ptr, end);
            BuiltIns.Serialize(range_min, ref ptr, end);
            BuiltIns.Serialize(range_max, ref ptr, end);
            BuiltIns.Serialize(ranges, ref ptr, end, 0);
            BuiltIns.Serialize(intensities, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 36;
            size += header.GetLength();
            size += 4 * ranges.Length;
            size += 4 * intensities.Length;
            return size;
        }
    
        public IMessage Create() => new LaserScan();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/LaserScan";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
