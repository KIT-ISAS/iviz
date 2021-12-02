/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public LaserScan()
        {
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new LaserScan(ref b);
        
        LaserScan IDeserializable<LaserScan>.RosDeserialize(ref Buffer b) => new LaserScan(ref b);
    
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
            b.SerializeStructArray(Ranges);
            b.SerializeStructArray(Intensities);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/LaserScan";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1VwY7jNgy9+ysI5LBJMUmB7i1oeyra7qFFgdlTB8GAkWlbWNnySHIy7tf3UXYS76Cb" +
                "3UODIIEt8lF8JB9X9Gi72glFwx1VwbfE1DvuOJDjKIECd7VsK9uVEopVsaIPFY1+oIZPQtz51MxGwKFS" +
                "TtYInW1qqLRVJUG6REeBsfWB1rKrdwgQPfABxSHwuHlAQEEs0iAEMxOEE8AXEK3EyLU8ULQdAnDfO2s4" +
                "Wd9F4Jytc9TyJ0CwDW6crr6NvRhbWUMc49D22Zr46IdEqbGRSk5cFL8LIzVqpr/FZ0XJIm7itifbwUUu" +
                "RvDVJzYvg41WcbMp+YqKJQK9gYNPZUNM4Gu8QCrxuzte9wABUQVG4Pz7bMsHVATVRJZBwBnHIUiJBz+A" +
                "2a9drfeaC6r6N/ErUlwbuCUJxnnz6Wwj2LcVDnE09Jt7aLn+/0jw03XQANoclQ9nDriO83jSiK850JeR" +
                "isp5Tu9/mGCeW+R7jYG6hDTjg/cLlfQUuDy8deTXm6OAim9zQ6sFabX94IZXg8NUlBaRtQePks4i3YVm" +
                "tYszzBVHu2IJM/fUF3yjGN+V8UBbJRpDFvLdOgzevSaI1PoTCH6YujoPw1FoiCg9GLNaw947TAton4rs" +
                "u3uI4OV9CUt4xmsqepPnfPlb0yxT0fNFDjcOsoB8Xjs82HZopyM6sRuEntrDW5dl1fDwny4Xn6fDdBQX" +
                "eUy2OuVqSus/fZL95Bvpx8XFoDk/L4LGxg+uVA5RbIOWlXKziKOEdsriFGx1fTHOsSYVvMnP0NkUDzua" +
                "pfNuNWcFLT3AIa7UB9S2lGXQq17i9yRfm+qssSSQv3FXFD/9z5/ij8ff9pjF8rmNdfx+0lIo8iNmpNRh" +
                "byVxZgXTT42tsS22Tk7idIDbHi2aT9PYS9zB8aO2ML61oO3ZQctzHydPxrctmITqy02YL/7wRB2xuiAK" +
                "1uRBNd6H0nZqnuVR0fGN8jKIzu+HX/aw6dCwg6oeIuVBZWyYGodUDOBcG19eitXHs99qCWqo/20rpIaT" +
                "XlZe+4AFpVIb94jx3ZTcDtggZ5oIWud3z3iMm3mNSe9NQ2vc/K8xNbpFULATB8tHyBOADRgA6jt1erdZ" +
                "IOu199Rh/17gJ8RbjG+BVZQJV3PaNqiZ0+zjUINAGM7dh1kYM4hxVnXM2WPgMBZZAHLIYvVrXkTXvare" +
                "WLveWBSgzBuhiCnkPTAvq6L4F6uTjed+CAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
