/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class NavSatFix : IDeserializable<NavSatFix>, IMessage
    {
        // Navigation Satellite fix for any Global Navigation Satellite System
        //
        // Specified using the WGS 84 reference ellipsoid
        // header.stamp specifies the ROS time for this measurement (the
        //        corresponding satellite time may be reported using the
        //        sensor_msgs/TimeReference message).
        //
        // header.frame_id is the frame of reference reported by the satellite
        //        receiver, usually the location of the antenna.  This is a
        //        Euclidean frame relative to the vehicle, not a reference
        //        ellipsoid.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // satellite fix status information
        [DataMember (Name = "status")] public NavSatStatus Status;
        // Latitude [degrees]. Positive is north of equator; negative is south.
        [DataMember (Name = "latitude")] public double Latitude;
        // Longitude [degrees]. Positive is east of prime meridian; negative is west.
        [DataMember (Name = "longitude")] public double Longitude;
        // Altitude [m]. Positive is above the WGS 84 ellipsoid
        // (quiet NaN if no altitude is available).
        [DataMember (Name = "altitude")] public double Altitude;
        // Position covariance [m^2] defined relative to a tangential plane
        // through the reported position. The components are East, North, and
        // Up (ENU), in row-major order.
        //
        // Beware: this coordinate system exhibits singularities at the poles.
        [DataMember (Name = "position_covariance")] public double[/*9*/] PositionCovariance;
        // If the covariance of the fix is known, fill it in completely. If the
        // GPS receiver provides the variance of each measurement, put them
        // along the diagonal. If only Dilution of Precision is available,
        // estimate an approximate covariance from that.
        public const byte COVARIANCE_TYPE_UNKNOWN = 0;
        public const byte COVARIANCE_TYPE_APPROXIMATED = 1;
        public const byte COVARIANCE_TYPE_DIAGONAL_KNOWN = 2;
        public const byte COVARIANCE_TYPE_KNOWN = 3;
        [DataMember (Name = "position_covariance_type")] public byte PositionCovarianceType;
    
        /// Constructor for empty message.
        public NavSatFix()
        {
            Status = new NavSatStatus();
            PositionCovariance = new double[9];
        }
        
        /// Explicit constructor.
        public NavSatFix(in StdMsgs.Header Header, NavSatStatus Status, double Latitude, double Longitude, double Altitude, double[] PositionCovariance, byte PositionCovarianceType)
        {
            this.Header = Header;
            this.Status = Status;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Altitude = Altitude;
            this.PositionCovariance = PositionCovariance;
            this.PositionCovarianceType = PositionCovarianceType;
        }
        
        /// Constructor with buffer.
        internal NavSatFix(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Status = new NavSatStatus(ref b);
            Latitude = b.Deserialize<double>();
            Longitude = b.Deserialize<double>();
            Altitude = b.Deserialize<double>();
            PositionCovariance = b.DeserializeStructArray<double>(9);
            PositionCovarianceType = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new NavSatFix(ref b);
        
        NavSatFix IDeserializable<NavSatFix>.RosDeserialize(ref Buffer b) => new NavSatFix(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            b.Serialize(Latitude);
            b.Serialize(Longitude);
            b.Serialize(Altitude);
            b.SerializeStructArray(PositionCovariance, 9);
            b.Serialize(PositionCovarianceType);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (PositionCovariance is null) throw new System.NullReferenceException(nameof(PositionCovariance));
            if (PositionCovariance.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(PositionCovariance), PositionCovariance.Length, 9);
        }
    
        public int RosMessageLength => 100 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/NavSatFix";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "2d3a8cd499b9b4a0249fb98fd05cfa48";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWXW/bNhR9168g4Icmg+O1aVB0GTrAbdzMWGobsdN2KDqDlq4lbhKpkpQd//udS31Y" +
                "6dqiA7YgLzJ5z/0691wOxEzuVCq9Mlospac8V57EVt2LrbFC6oO4zs1G5l++tzw4T0U0iAZiWVKstooS" +
                "UTmlU+EzEu+ul+L5hbC0JUs6JsFmpTMqiWCRkUzIjpyXRSlcY+6C4e18KbwqKAThM+VEQdJVlgrSXpzg" +
                "Cuybv9hYS640OmG3rost2BfyIDaECEpjfT+2o70j7YxdFy51P65gc9tFW5BzMqXTUUiwCXdrZUFrlQhV" +
                "hxq+hdn2suy8bQ7hShfT0amlmNSO7BARVTLP64u5iesKA46/pfaktRwJseIa4F8eISZVnKuEpG5CsJTD" +
                "eIfETTDeUabinIZCGy/kMbwjQteNUfRrSK7JkZvjHnABPfIVAtDoRxFCjMAH8GBZH9TnbHeDU18lJD4k" +
                "lFoi93EkFsapEBkS0KhMxvnRp0p6Y38WmphW9akzlc9G0TY30j+7EHkDFoCNTr+JDIJ4Bi5taDxZlSip" +
                "H+LvyfkefAvJ+OO8Dbz4DFhuDFf1yOcjiwfi5FOlyGM6ZkJtkZ2QLQ5b7qTK5SZnCrVO23P2WXtBv2Oz" +
                "kxbRgjwfij/OP4qEtkqDQf2mSuGlTjEACuNY5lJzK31mTZVmIbyOd2WDOwJvCOAFpgN2CMiSmKBOQzHj" +
                "PgzBME7irhQnk9nd6RAdFtbszwr5JwbPWCZ8IP9L2sP2sp7F2OBEaTBEuCAAgu4ztVFwwONV5cjF8yhL" +
                "H+IqTU5uFLUl+PDTxy7C9TFzLsi05n2vHM0kMAnh+S9t9nqIrzwXynO0nFxO4Oph1FgD5nqx7CYMfDA7" +
                "zEk9rX1cknHW15WhKKsQMAQNbQI5gglYlBot84BvNEb1SuVVO6YL+FGOP/r9HgIBVFOYFZ5iIUtEcV9/" +
                "9pLbWlPAhwQno0pp/1y8mr8d307Hs1eT9er3xWR9N/ttNn83Ey/E46/cGC8Wt/P30zfj1eQK15585drV" +
                "dHw9n41v1i3e+VcutudP25C+0Kq1P5To14v/+C96s7y+hJYktRjXmsSrBbxPpE3QLC8T6WVYC5lKM7Jn" +
                "Oe0oZwEqSjA/nHJ04NugU00MDdmgspXDJcwSaFNUWkFu60XxwB6WYJYUpbRexUznPuWD2jI6qyRULEj+" +
                "9OoSd7SjGMxAQAcgxBbc4nUzvRKhlk/P2SAarPbmDJ+Ugp2d88CDoGP3JfYZxyndJXz8UCc3AjaKQ/CS" +
                "OHESflvj053y0PFuLQ34fILIFwefgZAd30FIBo5RAaA+YqNHpz1kDvtSaKlNC18jHn18D6zucDmnsww9" +
                "y8NGrlIUEBebQezWItYXb/Ncbay0hyjs6+AyGrwOGy2ITegIz5ZzJlZoQCL2ymeR85bR23X8v7Gx9zro" +
                "r7zoGy+nZlv+iwcU0N5lhKKAEEZgCbISsW5UKSsTcm4EMIHU2SKsBlRxY7BLG3nkuoP3rOj1W4LXYahp" +
                "orZh9Ye9EV5LMUfB2xDroNHJBI+MMTAaPzuJt4XYZ6TbbH55IZar8epuuX49fQ+9CtrQ/DKb848QDXH2" +
                "pH1cDESlA0WQEIO2MvLAsLHiv8cPDPt5P7BYvhy3Zj1XzIjjk+VsI8P41CD1c6WPcX3EOP8MI8U21cmX" +
                "AKKAcHzovOR1F9Y003CPt1b2HZ3GZKXYJXXtgRL0qJmHdmM1y+DJM7Gc3L6dQph5n9VlquW9f3QDVV/y" +
                "cS3ovaNX8zeL+uiizRFjnePt4bDP1ZWpRv9AG99MbyZzmDzvgnBkd4rX899cQ79KLAwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
