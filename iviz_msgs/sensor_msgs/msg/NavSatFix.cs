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
    
        /// <summary> Constructor for empty message. </summary>
        public NavSatFix()
        {
            Status = new NavSatStatus();
            PositionCovariance = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
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
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (PositionCovariance is null) throw new System.NullReferenceException(nameof(PositionCovariance));
            if (PositionCovariance.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(PositionCovariance), PositionCovariance.Length, 9);
        }
    
        public int RosMessageLength => 100 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/NavSatFix";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2d3a8cd499b9b4a0249fb98fd05cfa48";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWXW/bNhR9F5D/QMAPTQbHa9Oi6DJ0gNu4qbHUNuKk7VB0Bi1dS9wkUiUpO/73O5f6" +
                "sNI1QwdsQV5k8p77de65HIiZ3KpUemW0WEpPea48iY26ExtjhdR7cZmbtcy/fW+5d56KaBANxLKkWG0U" +
                "JaJySqfCZyQ+XC7Fi2fC0oYs6ZgEm5XOqCSCRUYyITtyXhalcI25C4bX86XwqqAQhM+UEwVJV1kqSHtx" +
                "jCuwb/5iYy250uiE3boutmBfyL1YEyIojfX92A72jrQzdlW41P14A5vrLtqCnJMpnYxCgk24GysLWqlE" +
                "qDrU8C3Mppdl5229D1e6mA5OLcWktmSHiKiSeV5fzE1cVxhw/C21J63lSIgbrgH+5QFiUsW5SkjqJgRL" +
                "OYy3SNwE4y1lKs5pKLTxQh7COyB03RhFb0NyTY7cHHePC+iRrxCARj+KEGIEPoAHy/qgPme7K5z6KiHx" +
                "KaHUErnPI7EwToXIkIBGZTLOj75U0hv7s9DEtKpPnal8Noo2uZH++TORN2AB2Oj0H5FBEM/ApQ2NJ6sS" +
                "JfV9/B0534NvIRl/nLeBF18By7Xhqh74fGDxQBx/qRR5TMdMqA2yE7LFYcutVLlc50yh1ml7zj5rL+h3" +
                "bLbSIlqQ51Px+9lnkdBGaTCo31QpvNQpBkBhHMtcam6lz6yp0iyE1/GubHBH4A0BvMB0wA4BWRIT1Gko" +
                "ZtyHIRjGSdyW4ngyuz0ZosPCmt1pIf/A4BnLhA/kf0U72J7XsxgbnCgNhggXBEDQXabWCg54vKocuXge" +
                "ZelDXKXJyY2itgSffvrcRbg6ZM4Fmda875WjmQQmITz/qc1OD/GV50J5jpaTywlc3Y8aa8BcLpbdhIEP" +
                "Zos5qae1j0syzvq6MhRlFQKGoKFNIEcwAYtSo2Ue8I3GqF6ovGrHdAE/yvFHv99DIIBqCrPCUyxkiSju" +
                "6s9echtrCviQ4GRUKe1fiNfz9+Pr6Xj2erK6+W0xWd3Ofp3NP8zES/H4gRvjxeJ6/nH6bnwzucC1Jw9c" +
                "u5iOL+ez8dWqxTt74GJ7/rQN6RutWvl9SdFR9PI//juK3i0vz6EmSS3HtSod8XYB9RNpE/TLy0R6GTZD" +
                "ptKM7GlOW8pZg4oS5A+nHCAoN+iEE3NDNght5XAJ4wTmFJVWUNx6V9yzhyXIJUUprVcxM7rP+iC4jM5C" +
                "CSELqj+9OMcd7SgGORDQHgixBb1440wvRCjn0zM2iAY3O3OKT0pB0M55oEKQsrsSK43jlO4cPn6okxsB" +
                "G9UheEmcOA6/rfDpTnjueL2WBpQ+RuSLvc/AyY7y4CQDx6gAUB+x0aOTHjKHfS601KaFrxEPPr4HVne4" +
                "nNNphp7lYSlXKQqIi80sdpsRG4wXeq7WVtp9FFZ2cBkN3oSlFvQmdITHyzkTKzQgETvls8h5y+jtRv4f" +
                "Cdl7IvT3HjPzwfdTszP/xTMKaB8yQl3ACSOwClmPWD2qlPUJaTcymEDwbBEWBAq5NtiojUhy6UF91vX6" +
                "RcFLMZQ1UZvwAAjbI7yZYo6CdyKWQqOWCZ4aY2A0frYSLwyxy0i32fzyUixvxje3y9Wb6UeoVlCI5pfZ" +
                "nH+EdIjTJ+0TYyAqHViChBi0FZN7ho0V/z2+Z9jP+57F8tW4Neu5YlIcHi6naxkmqAapHy19jMsDxtlX" +
                "GCl2qk6+BRAFhMNz5xUvvbCsmYk7vLiy7+g0hivFRqlrD5QgSc1ItHurWQlPnovl5Pr9FPLMW60uUy3y" +
                "/aMraPuSj2tZ7x29nr9b1EfP2hwx2TleIA5bXV2YavQ3tPHV9Goyh8mLLghHdquwpI+ivwAWTZXvMwwA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
