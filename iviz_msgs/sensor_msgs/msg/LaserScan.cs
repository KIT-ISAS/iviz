/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/LaserScan")]
    public sealed class LaserScan : IDeserializable<LaserScan>, IMessage
    {
        // Single scan from a planar laser range-finder
        //
        // If you have another ranging device with different behavior (e.g. a sonar
        // array), please find or create a different message, since applications
        // will make fairly laser-specific assumptions about this data
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp in the header is the acquisition time of 
        // the first ray in the scan.
        //
        // in frame frame_id, angles are measured around 
        // the positive Z axis (counterclockwise, if Z is up)
        // with zero angle being forward along the x axis
        [DataMember (Name = "angle_min")] public float AngleMin; // start angle of the scan [rad]
        [DataMember (Name = "angle_max")] public float AngleMax; // end angle of the scan [rad]
        [DataMember (Name = "angle_increment")] public float AngleIncrement; // angular distance between measurements [rad]
        [DataMember (Name = "time_increment")] public float TimeIncrement; // time between measurements [seconds] - if your scanner
        // is moving, this will be used in interpolating position
        // of 3d points
        [DataMember (Name = "scan_time")] public float ScanTime; // time between scans [seconds]
        [DataMember (Name = "range_min")] public float RangeMin; // minimum range value [m]
        [DataMember (Name = "range_max")] public float RangeMax; // maximum range value [m]
        [DataMember (Name = "ranges")] public float[] Ranges; // range data [m] (Note: values < range_min or > range_max should be discarded)
        [DataMember (Name = "intensities")] public float[] Intensities; // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Constructor for empty message. </summary>
        public LaserScan()
        {
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        internal LaserScan(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LaserScan(ref b);
        }
        
        LaserScan IDeserializable<LaserScan>.RosDeserialize(ref Buffer b)
        {
            return new LaserScan(ref b);
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
            b.SerializeStructArray(Ranges, 0);
            b.SerializeStructArray(Intensities, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/LaserScan";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1VTY/jNgy9G5j/QCCHTYpJCnRvwbanou0eWhSYPXUQBIxM28LKlkeSk3F/fR/lfLiD" +
                "bnYPDYIEtshH8ZF8XNCT7WonFA13VAXfElPvuONAjqMECtzVsq5sV0ooFsWCPlY0+oEaPgpx51NzNgIO" +
                "lXK0RuhkU0OlrSoJ0iU6CIytD7SUTb1BgOiBDygOgcfVIwIKYpEGIZiZIJwAPoNoJUau5ZGi7RCA+95Z" +
                "w8n6LgLnZJ2jlj8Dgm1w43T1dezF2Moa4hiHts/WxAc/JEqNjVRy4qL4TRipUTP9zT4LShZxE7c92Q4u" +
                "cjGCrz6xeRlstIqbTclXVMwR6A0cfCobYgJf4wVSid/c8boHCIgqMALn370tH1ERVBNZBgFnHIcgJR78" +
                "AGa/drXeay6o6l/Er0hxaeCWJBjnzeeTjWDfVjjE0dCv7qHl+v8twU/XQQNoc1Q+nDjgOs7jSSO+5kBf" +
                "Rioq5zm9/2GC2bfI9xoDdQnpjA/eL1TSc+By99aRX2+OAiq+zQ2tFqTV9oMbXg0OU1FaRNYePEg6iXQX" +
                "mtUunmGuONoVc5hzT33BN4rxXRl3tFaiMWQh363D4N1rgkitP4Lgx6mr8zAchIaI0oMxqzXsvcO0gPap" +
                "yL67hwhe3pewhGe8pqI32efL35pmnoqez3K4cZAF5N+1w4Nth3Y6oiO7Qei53b11mVcND//pcvF53k1H" +
                "cZbHZKtTrqa0/MMn2U6+kT7MLgbN+WkWNDZ+cKVyiGIbtKyUq1kcJbRTFqdgi+uL8RxrUsGb/AydTXG3" +
                "obN03q3mWUFLD3CIK/UBtS1lHvSql/g9ytemOmssCeRv3BQPxY//8+eh+P3p1y2msdy3sY7fT2r6AFF+" +
                "wpiUOu+tJM7EQACosTUWxtrJUZzOcNujS/NpGnuJGzh+0i7GtxZ0PjvIeW7l5Mn4tgWZEH65afPFH54o" +
                "JbYXdMGaPKvG+1DaTs2zQio6vlFeBtER/vjzFjYdenZQ4UOkPKuMJVPjkIoBtGvvy0ux+HTya61CjQVw" +
                "Wwyp4aSXldc+YEep2sYtYnw3JbcBNtiZhoKW+d0ej3F13mTSe9PQEjf/c0yNLhLU7MjB8gEKBWADBoD6" +
                "Tp3erWbIeu0tdVjBF/gJ8RbjW2AVZcLVnNYNauY0+zjUIBCG5wbEOIwZxDirUubsIXAYi6wBOWSx+CXv" +
                "outqVW9sXm8sClDmpVDEFPIqOO8rNOQ/GuuXZ4IIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
