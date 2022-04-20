/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MultiEchoLaserScan : IDeserializable<MultiEchoLaserScan>, IMessage
    {
        // Single scan from a multi-echo planar laser range-finder
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
        /// <summary> Range data [m] (Note: NaNs, values < range_min or > range_max should be discarded) </summary>
        [DataMember (Name = "ranges")] public LaserEcho[] Ranges;
        // +Inf measurements are out of range
        // -Inf measurements are too close to determine exact distance.
        /// <summary> Intensity data [device-specific units].  If your </summary>
        [DataMember (Name = "intensities")] public LaserEcho[] Intensities;
        // device does not provide intensities, please leave
        // the array empty.
    
        /// Constructor for empty message.
        public MultiEchoLaserScan()
        {
            Ranges = System.Array.Empty<LaserEcho>();
            Intensities = System.Array.Empty<LaserEcho>();
        }
        
        /// Constructor with buffer.
        public MultiEchoLaserScan(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out AngleMin);
            b.Deserialize(out AngleMax);
            b.Deserialize(out AngleIncrement);
            b.Deserialize(out TimeIncrement);
            b.Deserialize(out ScanTime);
            b.Deserialize(out RangeMin);
            b.Deserialize(out RangeMax);
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MultiEchoLaserScan(ref b);
        
        public MultiEchoLaserScan RosDeserialize(ref ReadBuffer b) => new MultiEchoLaserScan(ref b);
    
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
            b.SerializeArray(Ranges);
            b.SerializeArray(Intensities);
        }
        
        public void RosValidate()
        {
            if (Ranges is null) BuiltIns.ThrowNullReference(nameof(Ranges));
            for (int i = 0; i < Ranges.Length; i++)
            {
                if (Ranges[i] is null) BuiltIns.ThrowNullReference($"{nameof(Ranges)}[{i}]");
                Ranges[i].RosValidate();
            }
            if (Intensities is null) BuiltIns.ThrowNullReference(nameof(Intensities));
            for (int i = 0; i < Intensities.Length; i++)
            {
                if (Intensities[i] is null) BuiltIns.ThrowNullReference($"{nameof(Intensities)}[{i}]");
                Intensities[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Ranges);
                size += BuiltIns.GetArraySize(Intensities);
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
