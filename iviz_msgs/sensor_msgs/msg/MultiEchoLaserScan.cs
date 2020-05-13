using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
    public sealed class MultiEchoLaserScan : IMessage
    {
        // Single scan from a multi-echo planar laser range-finder
        //
        // If you have another ranging device with different behavior (e.g. a sonar
        // array), please find or create a different message, since applications
        // will make fairly laser-specific assumptions about this data
        
        [DataMember] public std_msgs.Header header { get; set; } // timestamp in the header is the acquisition time of 
        // the first ray in the scan.
        //
        // in frame frame_id, angles are measured around 
        // the positive Z axis (counterclockwise, if Z is up)
        // with zero angle being forward along the x axis
        
        [DataMember] public float angle_min { get; set; } // start angle of the scan [rad]
        [DataMember] public float angle_max { get; set; } // end angle of the scan [rad]
        [DataMember] public float angle_increment { get; set; } // angular distance between measurements [rad]
        
        [DataMember] public float time_increment { get; set; } // time between measurements [seconds] - if your scanner
        // is moving, this will be used in interpolating position
        // of 3d points
        [DataMember] public float scan_time { get; set; } // time between scans [seconds]
        
        [DataMember] public float range_min { get; set; } // minimum range value [m]
        [DataMember] public float range_max { get; set; } // maximum range value [m]
        
        [DataMember] public LaserEcho[] ranges { get; set; } // range data [m] (Note: NaNs, values < range_min or > range_max should be discarded)
        // +Inf measurements are out of range
        // -Inf measurements are too close to determine exact distance.
        [DataMember] public LaserEcho[] intensities { get; set; } // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Constructor for empty message. </summary>
        public MultiEchoLaserScan()
        {
            header = new std_msgs.Header();
            ranges = System.Array.Empty<LaserEcho>();
            intensities = System.Array.Empty<LaserEcho>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiEchoLaserScan(std_msgs.Header header, float angle_min, float angle_max, float angle_increment, float time_increment, float scan_time, float range_min, float range_max, LaserEcho[] ranges, LaserEcho[] intensities)
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
        internal MultiEchoLaserScan(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.angle_min = b.Deserialize<float>();
            this.angle_max = b.Deserialize<float>();
            this.angle_increment = b.Deserialize<float>();
            this.time_increment = b.Deserialize<float>();
            this.scan_time = b.Deserialize<float>();
            this.range_min = b.Deserialize<float>();
            this.range_max = b.Deserialize<float>();
            this.ranges = b.DeserializeArray<LaserEcho>();
            for (int i = 0; i < this.ranges.Length; i++)
            {
                this.ranges[i] = new LaserEcho(b);
            }
            this.intensities = b.DeserializeArray<LaserEcho>();
            for (int i = 0; i < this.intensities.Length; i++)
            {
                this.intensities[i] = new LaserEcho(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MultiEchoLaserScan(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.angle_min);
            b.Serialize(this.angle_max);
            b.Serialize(this.angle_increment);
            b.Serialize(this.time_increment);
            b.Serialize(this.scan_time);
            b.Serialize(this.range_min);
            b.Serialize(this.range_max);
            b.SerializeArray(this.ranges, 0);
            b.SerializeArray(this.intensities, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (ranges is null) throw new System.NullReferenceException();
            for (int i = 0; i < ranges.Length; i++)
            {
                if (ranges[i] is null) throw new System.NullReferenceException();
                ranges[i].Validate();
            }
            if (intensities is null) throw new System.NullReferenceException();
            for (int i = 0; i < intensities.Length; i++)
            {
                if (intensities[i] is null) throw new System.NullReferenceException();
                intensities[i].Validate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += header.RosMessageLength;
                for (int i = 0; i < ranges.Length; i++)
                {
                    size += ranges[i].RosMessageLength;
                }
                for (int i = 0; i < intensities.Length; i++)
                {
                    size += intensities[i].RosMessageLength;
                }
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
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
