/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/LaserScan")]
    public sealed class LaserScan : IDeserializable<LaserScan>, IMessage
    {
        // Single scan from a planar laser range-finder
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
        [DataMember (Name = "ranges")] public float[] Ranges { get; set; } // range data [m] (Note: values < range_min or > range_max should be discarded)
        [DataMember (Name = "intensities")] public float[] Intensities { get; set; } // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// <summary> Constructor for empty message. </summary>
        public LaserScan()
        {
            Header = new StdMsgs.Header();
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LaserScan(StdMsgs.Header Header, float AngleMin, float AngleMax, float AngleIncrement, float TimeIncrement, float ScanTime, float RangeMin, float RangeMax, float[] Ranges, float[] Intensities)
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
        public LaserScan(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACq1VwY7bRgy9G/A/EPAh3mLtAs3NSHMq2ubQosDm1IVh0CNKGmSk0c6M7FW/vo8j2Vab" +
                "1skhhtcLachHziP5uKIn21ZOKBpuqQy+IabOccuBHEcJFLitZFPatpCwXKzwpQ8lDb6nmk9C3PpUT2ZA" +
                "okJO1gidbaqpsGUpQdpER4Gx9YHWsq22CBE9IigWh8DDwyNiCsKRxiHYmSCcgD7DaCRGruSRom0RgbvO" +
                "WcPJ+jYq0Nk6Rw1/Agbb4IYx/U3sxNjSGuIY+6bL5sRH3ydKtY1UcOLlYrn4VRgXpHr8N/usKFmETtx0" +
                "ZFs4ycUI3vrE5qW30SpyNiVf0nIxh6B/4cGptCEmkDZcMJX/7T23u5AAKQMjdv492OIRhUFZcdUgYI5j" +
                "H6TAg+/B7xez67zeB9X9k/gV11wb+CUJxnnz6WwjimBLHOKo7x7uwuVG+EuCHxNCJ2iXlD6cOSAh5/Gk" +
                "IV9zpDtQy0XpPKe3P4xAhwZ3vkZBeUKaIoD+C6H0HLjYf+bJrzdPAR9f6Ye2C9JoK8IPr3qHISksYms/" +
                "HiWdRdoL2WoXLzg3JG2QOdDUXv/jHcX4toh72ijfmLmQ02t1Eu91Q6TGn8Dz49jjeTSOQn1ED4A2q7Xs" +
                "vMPwgP2x2L69Cwly3hYwhWu83UaTOeT8b+0zv42ez64xJyLLyj+LiAfb9M14RCd2vdBzs//MZ14+PPy3" +
                "z9XreT8extltRmsdfTWm9e8+yW70jvRulhuU6P0sbKx97wqlEmU3aGApHuaBlNhW2Ryjra4vhinYqI43" +
                "Uepbm+J+S5Ok3i/rJK2FBzpUl7qAIhcyj3rVUfye5ItznsWXBLI4QHuWix+/8We5+O3plx2mszg0sYrf" +
                "jyKrav2EoSlUARpJnLmBJFBtK+ySjZOTOJ3ppkPD5tM0dBKR44o+akfjWwnGgB2EPrd18mR804BQ7AS5" +
                "afYFQF1RUCw3KIU1eXaN96Gwrdpn3cz4+hflpRcd6g8/7WDVooF71UMEy7PLWEEVDmHcg32dBHmB48ez" +
                "32g1KmyH29ZINSfNWF67gB2mOhx3Gua78Y5bwIOkcUhond8d8BgfplUnnTc1rZH+H0Oqdc2gdicOlo/Q" +
                "LSAb8ADYN+r0Bh15g9bUd9RiS1/wR8hbkK/BVZQJWK+1qVE8pxTEvgKPsJx6EbMxZBTjrOqbs8fAYVgu" +
                "sizkoAD5Oa+q6/pVf2xnbywqUeSNsVzEFPKimPaZduffnWBkwawIAAA=";
                
    }
}
