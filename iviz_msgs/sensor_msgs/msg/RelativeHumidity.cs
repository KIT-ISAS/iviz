/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class RelativeHumidity : IDeserializable<RelativeHumidity>, IMessage
    {
        // Single reading from a relative humidity sensor.  Defines the ratio of partial
        // pressure of water vapor to the saturated vapor pressure at a temperature.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp of the measurement
        // frame_id is the location of the humidity sensor
        [DataMember (Name = "relative_humidity")] public double RelativeHumidity_; // Expression of the relative humidity
        // from 0.0 to 1.0.
        // 0.0 is no partial pressure of water vapor
        // 1.0 represents partial pressure of saturation
        [DataMember (Name = "variance")] public double Variance; // 0 is interpreted as variance unknown
    
        /// Constructor for empty message.
        public RelativeHumidity()
        {
        }
        
        /// Explicit constructor.
        public RelativeHumidity(in StdMsgs.Header Header, double RelativeHumidity_, double Variance)
        {
            this.Header = Header;
            this.RelativeHumidity_ = RelativeHumidity_;
            this.Variance = Variance;
        }
        
        /// Constructor with buffer.
        internal RelativeHumidity(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            RelativeHumidity_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new RelativeHumidity(ref b);
        
        RelativeHumidity IDeserializable<RelativeHumidity>.RosDeserialize(ref Buffer b) => new RelativeHumidity(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RelativeHumidity_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/RelativeHumidity";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1Uy27bMBC88ysW0CFJAavuAz0Y6M195FCgQHI3NtJaIkqRKknZ1d93VopfaZv2UMKA" +
                "LGp2dnd2SCrozvrGCUXhGv9oG0NHjFfH2e6E2qGztc0jJfEpxJJoLVvrJVFuEQVQoLClnmO27AwV1EdJ" +
                "aYii23vOEmnHfYiUwxSSOA8Ik/px+wjnjLxZul7wGRulMfQZVYGgnR/nq6BsO0mZu14TKXMnrESd+Gwu" +
                "sJerQI/cycbWZOcuXKi0D38getIz6ti6wPnd26MsmyOkoA8/phbO4n8R72/lQPJluVSFXpXL8nm0AlG3" +
                "DwfN/yT48zRIhDo1FHKl33I9TgqNnSmw42jZV3JRkhZkPTIjXCfL6YQb/Dcf9t68/8/LfLn7tKKU602X" +
                "mvRydoqBnzP7mmMNO2SuOTNt4bLWNq3EhZOdOJpcgzKnr3nsJZUIvG+1i0SNeDjQuZGGBBCGUoWuG7yF" +
                "SeTkukM8Iq2HcycFq8FxBD5EHCaFT1ZTdvySfB9EJbldr4DxSapBbYJM1lc4gEkP4O2azAAx37zWAFPc" +
                "78NCtW0w15Plc4vjgmJlNt+k+Qo5XszNleCGOIIsdaLraW+D13RDSIISpA9VS9eo/OuYW1hXfTvN7AGX" +
                "AYgrKADWKw26ujlj1rJX5NmHA/3MeMrxL7TKMvNqT4sWM3PafRoaCAhgH8PO1oA+jBNJ5SyMSs4+RI6j" +
                "0ag5pSk+qsYAIWqaCJ6cUqjsdMvsbW5NynG+3OaDb8xPiNWhcPkEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
