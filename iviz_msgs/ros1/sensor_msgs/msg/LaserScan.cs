/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class LaserScan : IDeserializable<LaserScan>, IMessage
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
        /// <summary> Range data [m] (Note: values < range_min or > range_max should be discarded) </summary>
        [DataMember (Name = "ranges")] public float[] Ranges;
        /// <summary> Intensity data [device-specific units].  If your </summary>
        [DataMember (Name = "intensities")] public float[] Intensities;
        // device does not provide intensities, please leave
        // the array empty.
    
        public LaserScan()
        {
            Ranges = System.Array.Empty<float>();
            Intensities = System.Array.Empty<float>();
        }
        
        public LaserScan(ref ReadBuffer b)
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
        
        public LaserScan RosDeserialize(ref ReadBuffer b) => new LaserScan(ref b);
        
        public LaserScan RosDeserialize(ref ReadBuffer2 b) => new LaserScan(ref b);
    
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
            b.SerializeStructArray(Ranges);
            b.SerializeStructArray(Intensities);
        }
        
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; /* AngleMin */
            c += 4; /* AngleMax */
            c += 4; /* AngleIncrement */
            c += 4; /* TimeIncrement */
            c += 4; /* ScanTime */
            c += 4; /* RangeMin */
            c += 4; /* RangeMax */
            c += 4;  /* Ranges length */
            c += 4 * Ranges.Length;
            c += 4;  /* Intensities length */
            c += 4 * Intensities.Length;
            return c;
        }
    
        public const string MessageType = "sensor_msgs/LaserScan";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "90c7ef2dc6895d81024acba2ac42f369";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61VwY7jNgy9+ysI5LCZYpKi3dug7WnRdg4tis6eOggCRqZjYWXJI8nJpF/fR9lJ3EUn" +
                "u4caQQLF5KP4SD4u6Mn6vRNKhj01MXTE1Dv2HMlxkkiR/V5WjfW1xGpRLeixoVMYqOWDEPuQ28kIOFTL" +
                "wRqho80t1bZpJIrPtBMY2xBpKev9GgFSAD6gOEY+3d0joCAWaRCCmYnCGeAziE5S4r3cU7IeAbjvnTWc" +
                "bfAJOEfrHHX8CRBsozuNV1+lXoxtrCFOaej6Yk28C0Om3NpENWeuql+FkRq148/sWVC2iJu568l6uMjZ" +
                "CL56YvMy2GQVt5hSaKiit55F8WlsTBl8nc6QSvz6htctQKslYwQu31tb36MiqCayjALOOA1RahzCAGa/" +
                "dLU+aC6o6l/Er0hxaeCWJRoXzKejTWDfNniJV0N/dwut1P9viWG8DhpAm6MJ8cgR13EBJ434WgK9jVQ1" +
                "LnB+//0Is+2Q7yUG6hLzhA/ez1TSc+R687kjv14dBVR8nRtaLUqn7Qc3/DU4TEVtEVl7cCf5KOLPNKtd" +
                "mmAuONoVc5ipp97wTWKCr9OGVko0hiyWu3kM3q0mSNSFAwi+H7u6DMNOaEgoPRizWsM+OEwLaB+LHPwt" +
                "RPDyvoYlPNMlFb3Jtlz+2jTzVPT9LIcrB0VA/l07HGw3dOMrOrAbhJ67zecu86rh8J8uZ5/nzfgqzfIY" +
                "bXXK1ZSWv4csD6Nvoh9mF4Pm/DQLmtowuFo5RLENWlbqu1kcJdQri2OwxeWP0xRrVMGr/Aze5rRZ0ySd" +
                "N6s5KWgdAA5xpT6itrXMg170Et8H+dJUF40lgfyd1lX14//8VL89/fKAWay3Xdqnb0cthSI/YUZqHfZO" +
                "MhdWMP3U2j22xcrJQRwVYUWLlrf51Etaw/GjtjA+e0Hbs4OWlz7OgUzoOjBpdDVchPnsXxUxxOqCKFhT" +
                "BtWEEGvr1bzIo6Ljk+RlEJ3fxw8PsPFo2EFVz6ki6+ZJOiaPH6gawLk2vryAyuc/Q/puUy0+HsNKa7HH" +
                "Griuh9xy1lvLax+xqVRz0wOCfTNmuUYQsDSOBi3Lf1sc0920z6QPpqUlUvjjlNswboYDR8s76BSADagA" +
                "6jt1enc3Q/YF2mMRn+FHxGuMr4H1F1zNadWieE5pSMOey7qb2hBDcSogxlkVNGd3keOpKkpQQlaLn8tG" +
                "uixY9cb+DcaiEnVZDVXKsSyEaWtV1T+oEVNQhwgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
