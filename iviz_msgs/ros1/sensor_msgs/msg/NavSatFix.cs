/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class NavSatFix : IHasSerializer<NavSatFix>, IMessage
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
    
        public NavSatFix()
        {
            Status = new NavSatStatus();
            PositionCovariance = new double[9];
        }
        
        public NavSatFix(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new NavSatStatus(ref b);
            b.Deserialize(out Latitude);
            b.Deserialize(out Longitude);
            b.Deserialize(out Altitude);
            {
                var array = new double[9];
                b.DeserializeStructArray(array);
                PositionCovariance = array;
            }
            b.Deserialize(out PositionCovarianceType);
        }
        
        public NavSatFix(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new NavSatStatus(ref b);
            b.Align8();
            b.Deserialize(out Latitude);
            b.Deserialize(out Longitude);
            b.Deserialize(out Altitude);
            {
                var array = new double[9];
                b.DeserializeStructArray(array);
                PositionCovariance = array;
            }
            b.Deserialize(out PositionCovarianceType);
        }
        
        public NavSatFix RosDeserialize(ref ReadBuffer b) => new NavSatFix(ref b);
        
        public NavSatFix RosDeserialize(ref ReadBuffer2 b) => new NavSatFix(ref b);
    
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
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            b.Align8();
            b.Serialize(Latitude);
            b.Serialize(Longitude);
            b.Serialize(Altitude);
            b.SerializeStructArray(PositionCovariance, 9);
            b.Serialize(PositionCovarianceType);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Status, nameof(Status));
            Status.RosValidate();
            BuiltIns.ThrowIfNull(PositionCovariance, nameof(PositionCovariance));
            BuiltIns.ThrowIfWrongSize(PositionCovariance, nameof(PositionCovariance), 9);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 100;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 4; // Status
            size = WriteBuffer2.Align8(size);
            size += 8; // Latitude
            size += 8; // Longitude
            size += 8; // Altitude
            size += 8 * 9; // PositionCovariance
            size += 1; // PositionCovarianceType
            return size;
        }
    
        public const string MessageType = "sensor_msgs/NavSatFix";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2d3a8cd499b9b4a0249fb98fd05cfa48";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWXW/bNhR9968g4Icmg+M1aVB0GTrAadzMWGobsdN2KDKDlq4lbhKpkpQd//udS31Y" +
                "aZuhA7YgLzJ5z/0691z2xVRuVSK9MlospKcsU57ERj2IjbFC6r24zsxaZt++t9g7T3mv3+uLRUGR2iiK" +
                "RemUToRPSXy4XohX58LShizpiASbFc6ouAeLlGRMdui8zAvhanMXDG9nC+FVTiEInyoncpKutJST9uII" +
                "V2Bf/0XGWnKF0TG7dW1swT6Xe7EmRFAY67uxHewdaWfsKneJ+3EJm9s22pyckwkdD0OCdbgbK3NaqVio" +
                "KtTwLcymk2Xrbb0PV9qYDk4tRaS2ZAeIqJRZVl3MTFRVGHD8LbUnreVQiCXXAP/yADEuo0zFJHUdgqUM" +
                "xlskboLxllIVZTQQ2nghD+EdENpuDHu/huTqHLk57hEX0CNfIgCNfuQhxB74AB4sqoPqnO1ucOrLmMSn" +
                "mBJL5O6HYm6cCpEhAY3KpJwffS6lN/ZnoSmRzakzpU+HvU1mpH95LrIaLAAbnfwjMgjiGbiwofFkVayk" +
                "foy/I+c78A0k44+yJvD8C2C5NlzVA58PLO6Lo8+lIo/pmAq1QXZCNjhsuZUqk+uMKdQ4bc7ZZ+UF/Y7M" +
                "VlpEG7H7P87uRUwbpcGgblOl8FInGACFcSwyqbmVPrWmTNIQXsu7osYdgjcE8BzTATsEZEmMUaeBmHIf" +
                "BmAYJ3FXiKPx9O54gA4La3YnufwTg2csEz6Q/5J2sL2oZjEyOFEaDBEuCICgh1StFRzweJUZcvE8ytKH" +
                "uAqTkRv2mhJ8+um+jXB1yJwLMql43ylHPQlMQnj+S5udHuAry4TyHC0nlxG4uh/W1oC5ni/aCQMfzBZz" +
                "Uk1rF5dklHZ1ZSCKMgQMQUObTC1iYFFitMwCvtEY1SuVlc2YzuFHOf7o9nsABFBN5VwjTKgsEMVD9dlJ" +
                "bmNNDh8SnOyVSvtX4s3s/eh2Mpq+Ga+Wv8/Hq7vpb9PZh6l4LZ4/cWM0n9/OPk7ejZbjK1w7feLa1WR0" +
                "PZuOblYN3tkTF5vzF01I32jVyu8L9Ov1f/zXe7e4voCWxJUYV5rEqwW8j6WN0SwvY+llWAupSlKyJxlt" +
                "KRNhiYD54ZSjA9/6rWpiaMgGlS0dLmGWQJu81CqS9aJ4ZA9LMEuKQlqvIqZzl/JBbRmdVRIqFiR/cnWB" +
                "O9pRVPK4wpPSkQW3eN1MrkSo5YszNhB98enWuNP7Xn+5Myf4nRLQtI0iECII2kOBxcYBS3cBZz9UWQ7h" +
                "BFUiuIudOAq/rfDpjnn6eMkWBsQ+QgrzvU/BzJb4YCYDRygFUJ+x0bPjDrIO0Fpq08BXiAcf3wOrW1zO" +
                "6SRF87KwmssElcTFeiLb/Yg9xms9U2sr7b4XFndw2eu/DastqE5oDQ+ZcyZSknVup3zac94yerOX/zda" +
                "dp4J3d2HNJ98QtVr81+8pID2ISUUxTJLsQ1ZklhAyoQlCjnXShhD82wedgSquDY+bXSS644BYGmvHhW8" +
                "F0NNY7UJb4CwQMKzKeIoeC1aagQzxmtjBIzaz1bikSF2Kekmm19ei8VytLxbrN5OPkK4gkjUv0xn/CPU" +
                "Q5ycNq+Mvih1oAgSYtBGTx4Z1lb89/yRYTfvRxaLy1Fj1nHFjDi8XU7WMoxPBSK/8np9wDj7AiPBWtXx" +
                "twAqhMOL55L3XtjXTMMdHl3pd3Qak5VgqVS1B0oQpnoemtVVb4XTl2Ixvn0/gULzYqvKdPrV0Q3kfbFo" +
                "lb1z9Gb2bl4dnTc5YqyzkpfiJakrUw6/QhvdTG7GM5i8aoNwZLeK9/TfaDX2eDUMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<NavSatFix> CreateSerializer() => new Serializer();
        public Deserializer<NavSatFix> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<NavSatFix>
        {
            public override void RosSerialize(NavSatFix msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(NavSatFix msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(NavSatFix msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(NavSatFix msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(NavSatFix msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<NavSatFix>
        {
            public override void RosDeserialize(ref ReadBuffer b, out NavSatFix msg) => msg = new NavSatFix(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out NavSatFix msg) => msg = new NavSatFix(ref b);
        }
    }
}
