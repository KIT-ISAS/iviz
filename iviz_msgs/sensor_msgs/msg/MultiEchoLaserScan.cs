/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/MultiEchoLaserScan")]
    public sealed class MultiEchoLaserScan : IDeserializable<MultiEchoLaserScan>, IMessage
    {
        // Single scan from a multi-echo planar laser range-finder
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
        [DataMember (Name = "ranges")] public LaserEcho[] Ranges { get; set; } // range data [m] (Note: NaNs, values < range_min or > range_max should be discarded)
        // +Inf measurements are out of range
        // -Inf measurements are too close to determine exact distance.
        [DataMember (Name = "intensities")] public LaserEcho[] Intensities { get; set; } // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Constructor for empty message. </summary>
        public MultiEchoLaserScan()
        {
            Ranges = System.Array.Empty<LaserEcho>();
            Intensities = System.Array.Empty<LaserEcho>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiEchoLaserScan(in StdMsgs.Header Header, float AngleMin, float AngleMax, float AngleIncrement, float TimeIncrement, float ScanTime, float RangeMin, float RangeMax, LaserEcho[] Ranges, LaserEcho[] Intensities)
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
        public MultiEchoLaserScan(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            AngleMin = b.Deserialize<float>();
            AngleMax = b.Deserialize<float>();
            AngleIncrement = b.Deserialize<float>();
            TimeIncrement = b.Deserialize<float>();
            ScanTime = b.Deserialize<float>();
            RangeMin = b.Deserialize<float>();
            RangeMax = b.Deserialize<float>();
            Ranges = b.DeserializeArray<LaserEcho>();
            for (int i = 0; i < Ranges.Length; i++)
            {
                Ranges[i] = new LaserEcho(ref b);
            }
            Intensities = b.DeserializeArray<LaserEcho>();
            for (int i = 0; i < Intensities.Length; i++)
            {
                Intensities[i] = new LaserEcho(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MultiEchoLaserScan(ref b);
        }
        
        MultiEchoLaserScan IDeserializable<MultiEchoLaserScan>.RosDeserialize(ref Buffer b)
        {
            return new MultiEchoLaserScan(ref b);
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
            b.SerializeArray(Ranges, 0);
            b.SerializeArray(Intensities, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Ranges is null) throw new System.NullReferenceException(nameof(Ranges));
            for (int i = 0; i < Ranges.Length; i++)
            {
                if (Ranges[i] is null) throw new System.NullReferenceException($"{nameof(Ranges)}[{i}]");
                Ranges[i].RosValidate();
            }
            if (Intensities is null) throw new System.NullReferenceException(nameof(Intensities));
            for (int i = 0; i < Intensities.Length; i++)
            {
                if (Intensities[i] is null) throw new System.NullReferenceException($"{nameof(Intensities)}[{i}]");
                Intensities[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += Header.RosMessageLength;
                foreach (var i in Ranges)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Intensities)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/MultiEchoLaserScan";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6fefb0c6da89d7c8abe4b339f5c2f8fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
