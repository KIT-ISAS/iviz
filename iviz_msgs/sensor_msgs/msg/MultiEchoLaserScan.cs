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
                "H4sIAAAAAAAAE7VWwW7jNhC96ysG8GGTNnaB7i1oe9ptG6AbFMieGgQBTY0sYilSISk77tf3DSnJyiJx" +
                "99AKhhNaM2/INzNvuKI743aWKWrlqAm+I0XdYJNZs2499VY5FciqyIGCcjteN8bVHKpVtaKbho5+oFbt" +
                "mZTzqR2NAEk1741mOpjUUm2ahgO7RFuGsfGBLniz2yBW9MAHlApBHS+vEJARiyQIwUwHVgngC4iOY1Q7" +
                "vqJoHAKovrdGq2S8i8A5GGupU18AoUywx7L1dexZm8ZoUjEOXZ+tSW39kCi1JlKtkqqq31nhaNSWP4tn" +
                "RckgblJdT8bBhScj+MpK6afBRCO42ZR8QxW99ayyT2NCTODrOEFKDjZnvM4BGsmeQuD8/WjqK2QEicUp" +
                "A4MzFYfANRZ+ALP/trXey1mQ1b9IPeOIFxpuiYO2Xn85mAj2TYOXeDX0l+fQcv7/5uDLdlAAUhyNDwcV" +
                "sB3rsZKIzznQ20hVY71K738sMI8dzjvHQF5CGvHB+0Ql3QdVP3ztqJ5Pjgwqvs0NpRa4k/KDG34aLLqi" +
                "NogsNbjldGB2E81iF0eYGUeqYgkz1tQbvpG1d3V8oLUQjSYLeW8OjXeuCCJ1fg+Cr0pV52bYMg0RqQdj" +
                "RnLYe4tuAe0lyd6dQwQv72tYwjPOR5GdPObNn4pmeRR5vzjDiYMsIC9zh4Xphq68or2yA9N99/C1yzJr" +
                "WLzqUv0hrf4RsnX/UF7G2afYSpeLKV3c+sTXdKtu41VBiPTTYntQnl8WoWPrB1sLk0i5RuFyfbbov79x" +
                "zcuEShOK2IDPDHvOe/2qd/Ke0H5R/oO4IpHYKBM/K53mSty84EDS7STHQsRqXh5HHopCn6RxcCbFhw2N" +
                "sn620kZ1rz2gIfzUB9RdzcuQs5bje3/2wFlARf+JIc3HTfXzf/xUn+5+u4ZM1I9d3MUfisxjWNyBtFp0" +
                "qOOkMikQJmrNDoNsbXnPlrLmo3vy23TsOW7g+Fm6C58doyOVxZjJLYbMaN91IFLL1JpnxuRfZZ1W1EOv" +
                "jM4aor0PtXFinpVb0PGJ/DSwSMvNh2vYOPTSIIJsZVjIUIzSwTcfqBpAufQkP1Wrzwe/lgzsMJhOAyu1" +
                "Kslm+bkPmJ0yBeI1YnxXDrcBNsgpzUoX+bdHLOPlOGG597qlC+z8z2NqfZlVexWM2kI5AazBAFDfidO7" +
                "ywWyy9AOV4MJviCeYnwLrJtx5UzrFjmzcvo47FQewGPxoUGPGURbIxJrzTaocKyyNuWQ1erXPCPnkS/e" +
                "uBF4bZCAOg+rKqaQR9Q4R6v/qxrRJz6Ugpybdiqt8Y6Tt4dzbqc15OOTXM7ENjvdycACIWIpfZj7D1QI" +
                "WX6W/sioOJzQorcmZYU+yA2vSEMGRbtOWjjJVBQlXPT0a7eTFX1UqI/SwIGlxrJolX6SG2WerMJ7mbXz" +
                "ENxU/wBTMNhgfwoAAA==";
                
    }
}
