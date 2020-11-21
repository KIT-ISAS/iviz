/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/MultiEchoLaserScan")]
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
            Header = new StdMsgs.Header();
            Ranges = System.Array.Empty<LaserEcho>();
            Intensities = System.Array.Empty<LaserEcho>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiEchoLaserScan(StdMsgs.Header Header, float AngleMin, float AngleMax, float AngleIncrement, float TimeIncrement, float ScanTime, float RangeMin, float RangeMax, LaserEcho[] Ranges, LaserEcho[] Intensities)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACrVWwW4bNxC9C9A/DKBD7NZSgeZmND01bQ00RgHnFMMwRtxZLREuuSa5ktWv7xuutFo3" +
                "rpJDK8iSqZ15Q76ZecMF3Vm/cULJsKc6hpaY2t5luxTTBOoce47kOEmkyH4jy9r6SuJ8tsCbbmrah54a" +
                "3gqxD7k5mAGUKtlaI7SzuaHK1rVE8ZnWAmMbIl3IarNCtBQQQbE4Rt5fXiGmIBxpHIKdicIZ6BOMVlLi" +
                "jVxRsh4RuOucNZxt8EmBdtY5avkzMNhGtx+2v0ydGFtbQ5xS33bFnHgd+ky5sYkqzjyfzWe/C+OA1Axf" +
                "k9eCskXozG1H1sNJjkbw1hWbp94mq8jFlEJN89kUgv6BB6faxpRB2v6IqalYnXM7CwmQOjJil89HW10h" +
                "McgwjhoFzHHqo1RYhB78fnV3XdDzILufiJ9xzAsDvyzRuGA+72xCEmyNh3jUd5dn4Uoh/CUxDBtCJWiV" +
                "1CHuOGJDLmClIZ9LpDNQ81ntAue3Pw5Ajy3OPEZBemI+RAD9R0LpPnL18IUnP588BXx8ox/KLkqrpQg/" +
                "/NQ7NEllEVvrcS15J+KPZKtdOuKckLRApkCH8voX7yQm+Co90FL5Rs/Fsj2vnXiuGhK1YQuer4YaL62x" +
                "FuoTagC0Wc1lFxyaB+wPyQ7+LCTIeVvBFK7pdBrdzGPZ/6l8pqfR55NjTIkosvIyiVjYtm+HR7Rl1wvd" +
                "tw9f+EzTh8XrPvPZH9r/76Fn9w/D4zR6Ddba+mpMF7chyzXd8m26GjAS/TTZIfTo50nw1ITeVUookm9Q" +
                "xlKd74Hvb3z9MrPalSpBoLXgnnVfvuqeQyD0IzQzB6guMoqtCskzmzxW5eolDZp4r9lWLhbjcn+gYtDu" +
                "k2T23ub0sKKD4J8vuoPwVwHYmAnURZRgJdOYo8rjc3v+zEVYdTSQQLT3OMe7//g1n324++0aylE9tmmT" +
                "fhgGgE6SO1BXqTq1krkwA7mixm4w55ZOtuJUb9oOzVSe5n0nCTtc0EftNrw3ghZlhyFUWg4JMqFtQSfm" +
                "lZzmyRFAXVFmTB1UzJqiKyaEWFmv9kXTC77+JXnqRQXn5pdrWHk0V69ajWBFVxjjcYOHMO7BvXapPMHx" +
                "4y4sNRcbTK7TRMsNZ92xPHcR81VnRLrWMN8NZ1wBHiQNDUwX5bdHLNPlYQxLF0xDF9j+n/vc6AhE5rYc" +
                "La+hqUA24AGwb9TpzeUUWrd+TR43iCP+AHkK8i24/gSsx1o2SJ5TClK/AY+wPFQiOnZfUIyzqr3OriPH" +
                "/XxWJKsEBcivZYyOVwP1x80hGItMVGWazWcpxzLEDrNWxeb/qk60TohDgY6NPFba4UZU9ojjro9rqMoH" +
                "vc2pcfG605kGXtRSe7P0JBgppIVxNCRBAeKcTvttlF3oht4KB8kouOjio0weBQz/getTq796lVnQe0ax" +
                "DH0dRSuuyNnQY3oNLQNYEzCM5HFQAu5v2F+w07UKAAA=";
                
    }
}
