/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/NavSatFix")]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // satellite fix status information
        [DataMember (Name = "status")] public NavSatStatus Status { get; set; }
        // Latitude [degrees]. Positive is north of equator; negative is south.
        [DataMember (Name = "latitude")] public double Latitude { get; set; }
        // Longitude [degrees]. Positive is east of prime meridian; negative is west.
        [DataMember (Name = "longitude")] public double Longitude { get; set; }
        // Altitude [m]. Positive is above the WGS 84 ellipsoid
        // (quiet NaN if no altitude is available).
        [DataMember (Name = "altitude")] public double Altitude { get; set; }
        // Position covariance [m^2] defined relative to a tangential plane
        // through the reported position. The components are East, North, and
        // Up (ENU), in row-major order.
        //
        // Beware: this coordinate system exhibits singularities at the poles.
        [DataMember (Name = "position_covariance")] public double[/*9*/] PositionCovariance { get; set; }
        // If the covariance of the fix is known, fill it in completely. If the
        // GPS receiver provides the variance of each measurement, put them
        // along the diagonal. If only Dilution of Precision is available,
        // estimate an approximate covariance from that.
        public const byte COVARIANCE_TYPE_UNKNOWN = 0;
        public const byte COVARIANCE_TYPE_APPROXIMATED = 1;
        public const byte COVARIANCE_TYPE_DIAGONAL_KNOWN = 2;
        public const byte COVARIANCE_TYPE_KNOWN = 3;
        [DataMember (Name = "position_covariance_type")] public byte PositionCovarianceType { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public NavSatFix()
        {
            Header = new StdMsgs.Header();
            Status = new NavSatStatus();
            PositionCovariance = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
        public NavSatFix(StdMsgs.Header Header, NavSatStatus Status, double Latitude, double Longitude, double Altitude, double[] PositionCovariance, byte PositionCovarianceType)
        {
            this.Header = Header;
            this.Status = Status;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Altitude = Altitude;
            this.PositionCovariance = PositionCovariance;
            this.PositionCovarianceType = PositionCovarianceType;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public NavSatFix(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new NavSatStatus(ref b);
            Latitude = b.Deserialize<double>();
            Longitude = b.Deserialize<double>();
            Altitude = b.Deserialize<double>();
            PositionCovariance = b.DeserializeStructArray<double>(9);
            PositionCovarianceType = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new NavSatFix(ref b);
        }
        
        NavSatFix IDeserializable<NavSatFix>.RosDeserialize(ref Buffer b)
        {
            return new NavSatFix(ref b);
        }
    
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
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (PositionCovariance is null) throw new System.NullReferenceException(nameof(PositionCovariance));
            if (PositionCovariance.Length != 9) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 100;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/NavSatFix";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2d3a8cd499b9b4a0249fb98fd05cfa48";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVW72/bNhD9HsD/A4F8aDI4XpsGRZehA9zGzYylthE7bYeiM2jpLHGTSJWk7Pi/3ztS" +
                "spWiGTpgM/xFEu/dO967H8diIjcqk14ZLebSU1EoT2Kt7sXaWCH1TlwXZiWLb5+b75ynsnd0jL+YV5So" +
                "taJU1E7pTPicxIfruXh5ISytyZJOSLBh5YxKe0dsk5NMyQ6cl2UlXAPgguntdC68KikQ8blyoiTpaksl" +
                "aS9OcIQBml9irCVXGZ2yZ7cnGABKuRMrAonKWN+l1wFwpJ2xy9Jl7scFjG73jEtyTmZ0OmjCbCivrSxp" +
                "qVKhIt3wLMy6E+ve4WoXjuxpdfxaSkhtyPbBqpZFEU8WJolXDTx+ltqT1nIgxIIvAn/ZwRjVSaFSkroh" +
                "YamA9QbRm2C9oVwlBfWFNl7IA8EOxD4tiPLXEGATZ0yTe6AMZMvXYKGRmTLw7B1BHpDFPH6JB6LpDQ74" +
                "OiXxKaXMErnPAzEzTgWCCETjinKOk77U0hv7s9DEOotfnal9Dk7rwkj/4kIUDVqDbXT2j+AQjGfsygYd" +
                "kFWpkvqhiy053/XQYkYXw6KlX36FLVeGr/gg8o60j8XJl1qRR9VMhFojSCFbIDbdSFXIVRFE1TpuD0S/" +
                "0RMUkJiNtCANPX0q/zj/LFJaKw1RdbMshZc6Q10oVGpVSB1y63Nr6iwPHPdarBrgAaREQC9RNDAEKUti" +
                "hPvqiwmnpA/RhUjuKnEymtyd9pFwYc32rJR/oiKN5SpoauI1bWF+Gcs0MfimNCQjXOgPgu5ztVLwwYVX" +
                "F4jHc5VLH6hVpiAHpP1VfPrp857m8hB/vJhxrIjOtTQ1wsqE97+02eo+nopCKM+cOcaCIODdoLFmnOvZ" +
                "fF980IfZoIJiJXeBSSZ5t+/0RVUH1tz0kDKIJdhAVpnRsggejEYZX6mibkt4BkfK8UM3+X2GgPgUaohL" +
                "XMgKPO7jYye+tTUlnEhWae+oVtq/FG+m74e34+HkzWi5+H02Wt5NfptMP0zEK/H0sSPD2ex2+nH8brgY" +
                "XeHcs8fOXY2H19PJ8GbZIp4/drI98PzA6xtpW/pdFXL36j/+9Y7eza8v0WzS2LZj3wqTCNWQSpsic16m" +
                "0sswQ3KV5WTPCtpQwS2qrFAO4SszZAUe79sraolsaMe1wymUGFRU1lqhL8ep8gCATaE0KSppvUpY4t0y" +
                "CG054Idmik4X5sP46hKntKMESgGpHTASC7HxeBpfiXipz8/ZAoaLrTnDM2UQ7J5BEEZodfcVJiCTle6S" +
                "3fwQYxwAHpdEcJQ6cRLeLfHoTrkaeSJXBho/Af3ZzufQ6L4GoFFGTnAPgH3CRk9Ou9BM/VJoqU2LHyEP" +
                "Tr4HVx+AOayzHMkrwhivM9wjTjbluR+kGHe8AxRqZaXd9Y7ClA9OAfI2zMDQiUJuuOScM4lCJlKxVT7v" +
                "HTlv2UE7w/9PdXb2iu6M5GgfXb2aAftvNjDG+5ATbgfiMAJjkxsVN5U648aF0JsGmaIV2jKMEFznymD6" +
                "Nu0zZAClwJ0/riE8PsPlpmodloYwYMKylTARnp4YG00fTbGfDBmk8bSRWEvENifdRvTLKzFfDBd38+Xb" +
                "8cfQzkLXaN5NpvwaDUWcPWsXk2NR6yAYBMWwbYN5aNmY8e/pA8tu8A9N5q+HrV3HGcvjsO2crWQoqIjS" +
                "bDpdkOsDyPlXIBlmr06/idBgdLek1zwdw2RnXW6xr+XfkXaUW4ahE7PAMKFZNUXSDrf91Hj2QsxHt+/H" +
                "aN88/OJ9NWOg++0G3X/O35vG3/n2ZvpuFr9dtMGi4AtsLQ47gLoyNbx9jTe8Gd+MprB52WHiyG5UnOl/" +
                "AxVsSBKEDAAA";
                
    }
}
