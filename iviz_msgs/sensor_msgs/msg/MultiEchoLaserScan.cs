
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class MultiEchoLaserScan : IMessage
    {
        // Single scan from a multi-echo planar laser range-finder
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
        
        public LaserEcho[] ranges; // range data [m] (Note: NaNs, values < range_min or > range_max should be discarded)
        // +Inf measurements are out of range
        // -Inf measurements are too close to determine exact distance.
        public LaserEcho[] intensities; // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/MultiEchoLaserScan";
    
        public IMessage Create() => new MultiEchoLaserScan();
    
        public int GetLength()
        {
            int size = 36;
            size += header.GetLength();
            for (int i = 0; i < ranges.Length; i++)
            {
                size += ranges[i].GetLength();
            }
            for (int i = 0; i < intensities.Length; i++)
            {
                size += intensities[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiEchoLaserScan()
        {
            header = new std_msgs.Header();
            ranges = System.Array.Empty<LaserEcho>();
            intensities = System.Array.Empty<LaserEcho>();
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
            BuiltIns.DeserializeArray(out ranges, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out intensities, ref ptr, end, 0);
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
            BuiltIns.SerializeArray(ranges, ref ptr, end, 0);
            BuiltIns.SerializeArray(intensities, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6fefb0c6da89d7c8abe4b339f5c2f8fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACrVWwW7jNhC9C8g/DODDOq3tAt2b0fbUbRugGxTInjYIApoaWcRSpEJSdtSv7xtKlpUg" +
                "6+6hDQInNGfekG9m3nBBd8btLVPUylEVfEOKms4ms2Zde2qtciqQVZEDBeX2vK6MKzkUi2JBNxX1vqNa" +
                "HZiU86kejQBJJR+MZjqaVFNpqooDu0Q7hrHxgZa82W8QK3rgA0qFoPrrFQIyYpEEIZjpwCoBfAbRcIxq" +
                "zyuKxiGAaltrtErGuwico7GWGvUFEMoE2w9HX8eWtamMJhVj17TZmtTOd4lSbSKVKqmi+IMVrkb18Gf2" +
                "s6BkEDeppiXj4MInI/jKSumnzkQjuNmUfEXFHIFewcGnMiEm8NWfICUHmwtelwABUQWFwPnz0ZQrZASJ" +
                "xS0DgzMVu8AlFr4Ds/92tNbLXZDVz6SeccWlhlvioK3XX44mgn1TYRNbXXt9CS3n/28OfjgOCkCKo/Lh" +
                "qAKOYz1WEvE5B/o6UlFZr9L7HweYxwb3nWIgLyGN+OD9RCXdB1U+vHZUz2dHBhXf5oZSC9xI+cENX3UW" +
                "XVEaRJYa3HE6MrsTzWIXR5gJR6piDjPW1Fd8I2vvyvhAayEaTRby2Rwa71IRRGr8AQSvhqrOzbBj6iJS" +
                "D8aM5LD1Ft0C2ocke3cJEby8L2EJzzhdRU7ymA9/Lpr5VWR/doczB1lAXuYOC9N0zbBFB2U7pvvm4bXL" +
                "PGtYvOlS/Cmt/gGydf8wbMbJZ7CVLhdTWt76xFu6VbdxNSBE+ml2PCjPL7PQsfadLYVJpFyjcLm8WPTf" +
                "37jqZUKlCUVswGeGveS9ftM7eU9oP2hj8hBXJBIHZeJnpdNUiZsXHEi6neRYiFhMy37kYVDoszR2zqT4" +
                "sKFR1i9W2qjupQc0hJ/agLoreR5y0nJ8Hi5eOAuo6D8xpLnfXBU//8c/V8XHu9+3EIrysYn7+MMg9FeY" +
                "F3fgrRQpajipzAu0iWqzxyxbWz6wFXlpWjRQ3k19y3EDx0/SYPjdM5pSWUya3GVIjvZNAy4xk/g8Nk7+" +
                "8ER9KWohWUZnGdHeh9I4Mc/iLej4jfzUsajLza9b2Di0UyeajEhZRhTm3x6bVHRgXdqSn4rFp6NfSxL2" +
                "mE3nmZVqleSw/NwGjE8ZBHGLGN8Nl9sAG+wM/UrL/N0jlvF6HLLcel3TEif/q0+1zDik7KCCUTuIJ4A1" +
                "GADqO3F6dz1DlmNvyeF1cIIfEM8xvgXWTbhyp3WNnFm5fez2IBCGY/2hR/sMoq0RlbVmF1ToiyxPOWSx" +
                "+C2PyWnqizceBV4bJKDM86qIKeQpNY7S4v8rSDSLD0NNTp0rZZmra3zp5BPiqrvTGiLyUZ5oYpy97mRs" +
                "gROxlG7MXQg2hC8/DYDIKDpc0vabSZKhEvLOGwQig6JpT4p4Eiv8B5LPnf3WG2VBHxRKZGjjwFJmWbqG" +
                "lpJ3ZZ6vQv0wcadRiHb/B5WxdYiGCgAA";
                
    }
}
