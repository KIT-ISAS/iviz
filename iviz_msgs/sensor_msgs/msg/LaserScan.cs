
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/LaserScan";
    
        public IMessage Create() => new LaserScan();
    
        public int GetLength()
        {
            int size = 36;
            size += header.GetLength();
            size += 4 * ranges.Length;
            size += 4 * intensities.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public LaserScan()
        {
            header = new std_msgs.Header();
            ranges = new float[0];
            intensities = new float[0];
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
