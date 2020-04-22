
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class NavSatFix : IMessage
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
        public std_msgs.Header header;
        
        // satellite fix status information
        public NavSatStatus status;
        
        // Latitude [degrees]. Positive is north of equator; negative is south.
        public double latitude;
        
        // Longitude [degrees]. Positive is east of prime meridian; negative is west.
        public double longitude;
        
        // Altitude [m]. Positive is above the WGS 84 ellipsoid
        // (quiet NaN if no altitude is available).
        public double altitude;
        
        // Position covariance [m^2] defined relative to a tangential plane
        // through the reported position. The components are East, North, and
        // Up (ENU), in row-major order.
        //
        // Beware: this coordinate system exhibits singularities at the poles.
        
        public double[/*9*/] position_covariance;
        
        // If the covariance of the fix is known, fill it in completely. If the
        // GPS receiver provides the variance of each measurement, put them
        // along the diagonal. If only Dilution of Precision is available,
        // estimate an approximate covariance from that.
        
        public const byte COVARIANCE_TYPE_UNKNOWN = 0;
        public const byte COVARIANCE_TYPE_APPROXIMATED = 1;
        public const byte COVARIANCE_TYPE_DIAGONAL_KNOWN = 2;
        public const byte COVARIANCE_TYPE_KNOWN = 3;
        
        public byte position_covariance_type;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/NavSatFix";
    
        public IMessage Create() => new NavSatFix();
    
        public int GetLength()
        {
            int size = 100;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public NavSatFix()
        {
            header = new std_msgs.Header();
            status = new NavSatStatus();
            position_covariance = new double[9];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            status.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out latitude, ref ptr, end);
            BuiltIns.Deserialize(out longitude, ref ptr, end);
            BuiltIns.Deserialize(out altitude, ref ptr, end);
            BuiltIns.Deserialize(out position_covariance, ref ptr, end, 9);
            BuiltIns.Deserialize(out position_covariance_type, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            status.Serialize(ref ptr, end);
            BuiltIns.Serialize(latitude, ref ptr, end);
            BuiltIns.Serialize(longitude, ref ptr, end);
            BuiltIns.Serialize(altitude, ref ptr, end);
            BuiltIns.Serialize(position_covariance, ref ptr, end, 9);
            BuiltIns.Serialize(position_covariance_type, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2d3a8cd499b9b4a0249fb98fd05cfa48";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
