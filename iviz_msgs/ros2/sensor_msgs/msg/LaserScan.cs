/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class LaserScan : IDeserializableRos2<LaserScan>, IMessageRos2
    {
        // Single scan from a planar laser range-finder
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
        /// <summary> Range data [m] </summary>
        [DataMember (Name = "ranges")] public float[] Ranges;
        // (Note: values < range_min or > range_max should be discarded)
        /// <summary> Intensity data [device-specific units].  If your </summary>
        [DataMember (Name = "intensities")] public float[] Intensities;
        // device does not provide intensities, please leave
        // the array empty.
    
        /// Constructor for empty message.
        public LaserScan()
        {
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        /// Constructor with buffer.
        public LaserScan(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out AngleMin);
            b.Deserialize(out AngleMax);
            b.Deserialize(out AngleIncrement);
            b.Deserialize(out TimeIncrement);
            b.Deserialize(out ScanTime);
            b.Deserialize(out RangeMin);
            b.Deserialize(out RangeMax);
            b.DeserializeStructArray(out Ranges);
            b.DeserializeStructArray(out Intensities);
        }
        
        public LaserScan RosDeserialize(ref ReadBuffer2 b) => new LaserScan(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
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
            if (Ranges is null) BuiltIns.ThrowNullReference();
            if (Intensities is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, AngleMin);
            WriteBuffer2.AddLength(ref c, AngleMax);
            WriteBuffer2.AddLength(ref c, AngleIncrement);
            WriteBuffer2.AddLength(ref c, TimeIncrement);
            WriteBuffer2.AddLength(ref c, ScanTime);
            WriteBuffer2.AddLength(ref c, RangeMin);
            WriteBuffer2.AddLength(ref c, RangeMax);
            WriteBuffer2.AddLength(ref c, Ranges);
            WriteBuffer2.AddLength(ref c, Intensities);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/LaserScan";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
