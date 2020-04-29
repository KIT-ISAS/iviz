using System.Runtime.Serialization;

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
    
        [IgnoreDataMember]
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
    
        public IMessage Create() => new MultiEchoLaserScan();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/MultiEchoLaserScan";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "6fefb0c6da89d7c8abe4b339f5c2f8fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
