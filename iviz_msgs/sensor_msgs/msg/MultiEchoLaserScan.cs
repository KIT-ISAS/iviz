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
        [DataMember (Name = "ranges")] public LaserEcho[] Ranges; // range data [m] (Note: NaNs, values < range_min or > range_max should be discarded)
        // +Inf measurements are out of range
        // -Inf measurements are too close to determine exact distance.
        [DataMember (Name = "intensities")] public LaserEcho[] Intensities; // intensity data [device-specific units].  If your
        // device does not provide intensities, please leave
        // the array empty.
    
        /// Constructor for empty message.
        public MultiEchoLaserScan()
        {
            Ranges = System.Array.Empty<LaserEcho>();
            Intensities = System.Array.Empty<LaserEcho>();
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal MultiEchoLaserScan(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new MultiEchoLaserScan(ref b);
        
        MultiEchoLaserScan IDeserializable<MultiEchoLaserScan>.RosDeserialize(ref Buffer b) => new MultiEchoLaserScan(ref b);
    
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
            b.SerializeArray(Ranges);
            b.SerializeArray(Intensities);
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
                size += BuiltIns.GetArraySize(Ranges);
                size += BuiltIns.GetArraySize(Intensities);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/MultiEchoLaserScan";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6fefb0c6da89d7c8abe4b339f5c2f8fb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWTW8jNwy9z68g4MMmbewC3Vuw7anbNkA3KJA9NQgCeobjEVYjTSSNHffX91Hz4UmQ" +
                "dffQBoETWeSj9Eg+akV3xu2sUCzZUR18S0xtb5NZS9l46iw7DmQ5SqDAbifr2rhKQrEqVnRT09H31PBe" +
                "iJ1PzWgESKpkb0qhg0kNVaauJYhLtBUYGx/oQja7DWJFD3xAcQh8vLxCQEEs0iAEszIIJ4AvIFqJkXdy" +
                "RdE4BOCus6bkZLyLwDkYa6nlL4BgE+xxOPo6dlKa2pTEMfZtl62Jt75PlBoTqeLERfG7MK5GzfBn8bOi" +
                "ZBA3cduRcXCRyQi+uuLyqTfRKG42JV9TsUSgV3DwqU2ICXwdJ0jNweaM1zlAQNSBETh/PprqChlBYnHL" +
                "IOCMYx+kwsL3YPbfjtZ5vQuy+hfxM654UcItSSitL78cTAT7psYmtvru8hxazv/fEvxwHBSAFkftw4ED" +
                "jmM9VhrxOQf6OlJRW8/p/Y8DzGOL+84xkJeQRnzwPlFJ94Grh9eO/HxyFFDxbW4otSCtlh/c8FVv0RWV" +
                "QWStwa2kg4ibaFa7OMLMOFoVS5ixpr7iG6X0rooPtFai0WQhn82h8c4VQaTW70Hw1VDVuRm2Qn1E6sGY" +
                "0Rx23qJbQPuQZO/OIYKX9xUs4Rnnq+hJHvPhT0WzvIruL+5w4iALyMvcYWHavh22aM+2F7pvH167LLOG" +
                "xZsuxR/a6h8hW/cPw2acfQZb7XI1pYtbn+Sabvk2Xg0IkT4sjgfl+XkROja+t5UyiZSXKFypzhb99zeu" +
                "fplQbUIVG/CZYc95r9/0Tt4T2g/amDzEFYnEQYXkmcs0V+LmBQeabqc5ViJW8/I48jAo9Ekae2dSfNjQ" +
                "KOtnK21U98oDGsJPXUDdVbIMOWs5PvdnL5wFVPWfBNJ83BQ//cc/xae7364hE9VjG3fxh0HmMSzuQFql" +
                "OtRK4kwKhIkas8MgW1vZi1VtaTt0T95Nx07iBo6ftbvwuxN0JFuMmdxiyEzp2xZEYiDJaWZM/vBEcTF1" +
                "0CtTZg0pvQ+VcWqelVvR8RvlqReVlptfrmHj0Eu9CjIiZQ1hDL8dNqnoQbn2pDwVq88Hv9YM7DCYTgMr" +
                "NZz0sPLcBcxOnQLxGjG+Gy63ATbIGZqVLvJ3j1jGy3HCSufLhi5w8j+PqdEBh3ztORjeQjkBXIIBoL5T" +
                "p3eXC2Q99jU5PA0m+AHxFONbYN2Mq3daN8iZ1dvHfgcCYTgWHxr0mEFKa1RirdkGDscia1MOWax+zTNy" +
                "HvnqjReBLw0SUOVhVcQU8oga52jxf1WjuOjDUJBz006lNb5x8vFwz+20hnx80seZ2manOx1YIEQttQ9z" +
                "/4EKJcvP0h8FFYcbWvTWpKzQB33hDdKQQdGukxZOMoX/wPCpp996nazoI6M+hgYOojWWRWvoJ31R5smq" +
                "vA+zdh6Cm+IfUzDYYH8KAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
