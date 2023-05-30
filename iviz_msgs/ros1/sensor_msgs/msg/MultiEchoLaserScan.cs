/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class MultiEchoLaserScan : IHasSerializer<MultiEchoLaserScan>, IMessage
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
    
        public MultiEchoLaserScan()
        {
            Ranges = EmptyArray<LaserEcho>.Value;
            Intensities = EmptyArray<LaserEcho>.Value;
        }
        
        public MultiEchoLaserScan(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out AngleMin);
            b.Deserialize(out AngleMax);
            b.Deserialize(out AngleIncrement);
            b.Deserialize(out TimeIncrement);
            b.Deserialize(out ScanTime);
            b.Deserialize(out RangeMin);
            b.Deserialize(out RangeMax);
            {
                int n = b.DeserializeArrayLength();
                LaserEcho[] array;
                if (n == 0) array = EmptyArray<LaserEcho>.Value;
                else
                {
                    array = new LaserEcho[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new LaserEcho(ref b);
                    }
                }
                Ranges = array;
            }
            {
                int n = b.DeserializeArrayLength();
                LaserEcho[] array;
                if (n == 0) array = EmptyArray<LaserEcho>.Value;
                else
                {
                    array = new LaserEcho[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new LaserEcho(ref b);
                    }
                }
                Intensities = array;
            }
        }
        
        public MultiEchoLaserScan(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            b.Deserialize(out AngleMin);
            b.Deserialize(out AngleMax);
            b.Deserialize(out AngleIncrement);
            b.Deserialize(out TimeIncrement);
            b.Deserialize(out ScanTime);
            b.Deserialize(out RangeMin);
            b.Deserialize(out RangeMax);
            {
                int n = b.DeserializeArrayLength();
                LaserEcho[] array;
                if (n == 0) array = EmptyArray<LaserEcho>.Value;
                else
                {
                    array = new LaserEcho[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new LaserEcho(ref b);
                    }
                }
                Ranges = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                LaserEcho[] array;
                if (n == 0) array = EmptyArray<LaserEcho>.Value;
                else
                {
                    array = new LaserEcho[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new LaserEcho(ref b);
                    }
                }
                Intensities = array;
            }
        }
        
        public MultiEchoLaserScan RosDeserialize(ref ReadBuffer b) => new MultiEchoLaserScan(ref b);
        
        public MultiEchoLaserScan RosDeserialize(ref ReadBuffer2 b) => new MultiEchoLaserScan(ref b);
    
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
            b.Serialize(Ranges.Length);
            foreach (var t in Ranges)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Intensities.Length);
            foreach (var t in Intensities)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(AngleMin);
            b.Serialize(AngleMax);
            b.Serialize(AngleIncrement);
            b.Serialize(TimeIncrement);
            b.Serialize(ScanTime);
            b.Serialize(RangeMin);
            b.Serialize(RangeMax);
            b.Serialize(Ranges.Length);
            foreach (var t in Ranges)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(Intensities.Length);
            foreach (var t in Intensities)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Ranges, nameof(Ranges));
            BuiltIns.ThrowIfNull(Intensities, nameof(Intensities));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 36;
                size += Header.RosMessageLength;
                foreach (var msg in Ranges) size += msg.RosMessageLength;
                foreach (var msg in Intensities) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // AngleMin
            size += 4; // AngleMax
            size += 4; // AngleIncrement
            size += 4; // TimeIncrement
            size += 4; // ScanTime
            size += 4; // RangeMin
            size += 4; // RangeMax
            size += 4; // Ranges.Length
            foreach (var msg in Ranges) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Intensities.Length
            foreach (var msg in Intensities) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "sensor_msgs/MultiEchoLaserScan";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6fefb0c6da89d7c8abe4b339f5c2f8fb";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWTW8bNxC9768YQIfYraSizc1oe0raGmiMos6phmBQ3FktES65Jrn66K/vG+6H1oGt" +
                "5JAuBNnUzrwh38y84YLujdtZpqiVoyr4hhQ1nU1mxbr21FrlVCCrIgcKyu14VRlXcigWxYJuKzr5jmq1" +
                "Z1LOp3owAiSVvDea6WBSTaWpKg7sEm0ZxsYHuuL1bo1Y0QMfUCoEdbpeIiAjFkkQgpkOrBLAZxANx6h2" +
                "vKRoHAKotrVGq2S8i8A5GGupUZ8AoUywp37rq9iyNpXRpGLsmjZbk9r6LlGqTaRSJVUUf7DC0aju/8ye" +
                "BSWDuEk1LRkHFx6N4CsrpZ86E43gZlPyFRX02rPIPpUJMYGv0wgpOVhf8LoEaCR7CoHz96Mpl8gIEotT" +
                "BgZnKnaBSyx8B2a/tLXWy1mQ1X9IHXHEKw23xEFbrz8dTAT7psJLvOra60toOf//cvD9dlAAUhyVDwcV" +
                "sB3rsZKIxxzodaSisl6ltz/1MI8NzjvFQF5CGvDB+0glPQRVbj53VMezI4OKr3NDqQVupPzghp86i64o" +
                "DSJLDW45HZjdSLPYxQFmwpGqmMMMNfWKb2TtXRk3tBKi0WQh782h8S4VQaTG70Hwsq/q3Axbpi4i9WDM" +
                "SA5bb9EtoL1PsneXEMHL2xKW8IzTUWQnj3nz56KZH0Xez85w5iALyPPcYWGarulf0V7Zjumh2XzuMs8a" +
                "Fi+6FH9Kq7+HbD1s+pdx8ultpcvFlK7ufOIbulN3cdkjRPp5tj0oz6+z0LH2nS2FSaRco3C5vFj039+6" +
                "6nlCpQlFbMBnhr3kvXrRO3lPaL8o/0FckUhslImPSqepEtfPOJB0O8mxELGYlqeBh16hz9LYOZPiZk2D" +
                "rF+stEHdSw9oCD+1AXVX8jzkpOX43l88cBZQ0X9iSPNpXfzyjZ/iw/3vN5CJ8rGJu/hDL/MYFvcgrRQd" +
                "ajipTAqEiWqzwyBbWd6zpaz56J78Np1ajms4fpTuwmfH6EhlMWZyiyEz2jcNiNQytaaZMfoXWacVtdAr" +
                "o7OGaO9DaZyYZ+UWdHwiP3Us0nL77gY2Dr3UiSBbGRYyFKN08O07KjpQLj3JT2Dy4W8ff9wUi48Hv5JU" +
                "7DChzpMr1SrJrvnYBgxRGQfxBsG+60+5RhCw1HctXeXfHrGM18Oo5dbrmq5whL9Oqfb90NqrYNQWEgpg" +
                "DSqA+kac3lzPkF2GdrgjjPA94jnG18C6CVfOtKqRPCs0xG6n8iQeqhCdesog2hrRWmu2QYVTkUUqhywW" +
                "v+VhOc1+8cbVwGuDTJR5ahUxhTyrhoFa/F9liYbxoa/MqXvHGhsuO3l7OOd2XENHPsgtTWyz071MLhAi" +
                "ltKQuRFBhZDlpxkQGaWHE1o02SixEAq56vUakUHRt6MojnoVRRJnzf3SNWVB7xXqo+/kwFJjWb36xpKr" +
                "ZR6xwns/dKdpuC7+AxVVmCeICgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MultiEchoLaserScan> CreateSerializer() => new Serializer();
        public Deserializer<MultiEchoLaserScan> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MultiEchoLaserScan>
        {
            public override void RosSerialize(MultiEchoLaserScan msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MultiEchoLaserScan msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MultiEchoLaserScan msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MultiEchoLaserScan msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MultiEchoLaserScan msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MultiEchoLaserScan>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MultiEchoLaserScan msg) => msg = new MultiEchoLaserScan(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MultiEchoLaserScan msg) => msg = new MultiEchoLaserScan(ref b);
        }
    }
}
