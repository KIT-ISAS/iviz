/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class NavSatFix : IDeserializableRos1<NavSatFix>, IMessageRos1
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
        
        /// Constructor with buffer.
        public NavSatFix(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Status = new NavSatStatus(ref b);
            b.Deserialize(out Latitude);
            b.Deserialize(out Longitude);
            b.Deserialize(out Altitude);
            b.DeserializeStructArray(9, out PositionCovariance);
            b.Deserialize(out PositionCovarianceType);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new NavSatFix(ref b);
        
        public NavSatFix RosDeserialize(ref ReadBuffer b) => new NavSatFix(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (PositionCovariance is null) BuiltIns.ThrowNullReference();
            if (PositionCovariance.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(PositionCovariance.Length, 9);
        }
    
        public int RosMessageLength => 100 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/NavSatFix";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2d3a8cd499b9b4a0249fb98fd05cfa48";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWXW/bNhR9968g4Icmg+M1aVF0GTrAadzMWGobUdJ2KDqDlq4lbhKpkpQd//udS31Y" +
                "aZuhA7YgLzJ5z/0691wOxVxuVSq9MlpE0lOeK09io+7Fxlgh9V5c5WYt82/fi/bOUzEYDoYiKilWG0WJ" +
                "qJzSqfAZifdXkXj5XFjakCUdk2Cz0hmVDGCRkUzIjp2XRSlcY+6C4c0iEl4VFILwmXKiIOkqSwVpL45w" +
                "BfbNX2ysJVcanbBb18UW7Au5F2tCBKWxvh/bwd6RdsauCpe6H29hc9NFW5BzMqXjcUiwCXdjZUErlQhV" +
                "hxq+hdn0suy8rffhShfTwamlmNSW7AgRVTLP64u5iesKA46/pfaktRwLccs1wL88QEyrOFcJSd2EYCmH" +
                "8RaJm2C8pUzFOY2ENl7IQ3gHhK4b48GvIbkmR26Oe8AF9MhXCECjH0UIcQA+gAdRfVCfs901Tn2VkPiY" +
                "UGqJ3KexWBqnQmRIQKMyGedHnyvpjf1ZaEple+pM5bPxYJMb6V88F3kDFoCNTv8RGQTxDFza0HiyKlFS" +
                "P8TfkfM9+BaS8Sd5G3jxBbBcG67qgc8HFg/F0edKkcd0zIXaIDshWxy23EqVy3XOFGqdtufss/aCfsdm" +
                "Ky2ijdn9H2efREIbpcGgflOl8FKnGACFcSxzqbmVPrOmSrMQXse7ssEdgzcE8ALTATsEZElMUaeRmHMf" +
                "RmAYJ3FXiqPp/O54hA4La3YnhfwTg2csEz6Q/4J2sD2vZzE2OFEaDBEuCICg+0ytFRzweFU5cvE8ytKH" +
                "uEqTkxsP2hJ8/OlTF+HqkDkXZFbzvleOZhKYhPD8lzY7PcJXngvlOVpOLidwdT9urAFztYy6CQMfzBZz" +
                "Uk9rH5dknPV1ZSTKKgQMQUObTCNiYFFqtMwDvtEY1UuVV+2YLuFHOf7o93sEBFBNFVwjTKgsEcV9/dlL" +
                "bmNNAR8SnBxUSvuX4vXi3eRmNpm/nq5uf19OV3fz3+aL93PxSjx95MZkubxZfJi9ndxOL3Ht9JFrl7PJ" +
                "1WI+uV61eGePXGzPn7UhfaNVK78v0a9X//Hf4G10dQ4tSWoxrjWJVwt4n0iboFleJtLLsBYylWZkT3La" +
                "Ui7CEgHzwylHB74NO9XE0JANKls5XMIsgTZFpVUsm0XxwB6WYJYUpbRexUznPuWD2jI6qyRULEj+7PIc" +
                "d7SjuOJxhSelYwtu8bqZXYpQy2dnbDAY3u7MCT4pBTs754EHQcfuS+wzjlO6c/j4oU5uDGwUh+AlceIo" +
                "/LbCpzvmoePdWhrw+QiRL/c+AyE7voOQDByjAkB9wkZPjnvIOkBrqU0LXyMefHwPrO5wOaeTDD3Lw0au" +
                "UhQQF5tB7NYi1hdv81ytrbT7QdjXweVg+CZstCA2oSM8W86ZWEmWt53y2cB5y+jtOv7f2Nh7HfRXHtJ8" +
                "9OXUbMt/8YAC2vuMUBTL5MQSZCVi3ahSVibk3AhgAqmzRVgNqOLa+KyVR647eM+KXr8leB2GmiZqE1Z/" +
                "2BvhtRRzFLwNLbU6meCRMQFG42cr8bYQu4x0m80vr0R0O7m9i1ZvZh+gV0Ebml/mC/4RoiFOTtvHxVBU" +
                "OlAECTFoKyMPDBsr/nv6wLCf9wOL6GLSmvVcMSMOT5aTtQzjU4PIr7xeHTDOvsBIsU118i2AGuHw0Lng" +
                "dRfWNNNwh7dW9h2dxmSl2CV17YES9KiZh3ZjNcvg9IWIpjfvZhBm3md1mU6/OrqGqkdRJ+i9o9eLt8v6" +
                "6HmbI8Y6r3gXXpC6NNX4K7TJ9ex6uoDJyy4IR3areD3/DVxDv0osDAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
