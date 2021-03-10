/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/RelativeHumidity")]
    public sealed class RelativeHumidity : IDeserializable<RelativeHumidity>, IMessage
    {
        // Single reading from a relative humidity sensor.  Defines the ratio of partial
        // pressure of water vapor to the saturated vapor pressure at a temperature.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp of the measurement
        // frame_id is the location of the humidity sensor
        [DataMember (Name = "relative_humidity")] public double RelativeHumidity_ { get; set; } // Expression of the relative humidity
        // from 0.0 to 1.0.
        // 0.0 is no partial pressure of water vapor
        // 1.0 represents partial pressure of saturation
        [DataMember (Name = "variance")] public double Variance { get; set; } // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public RelativeHumidity()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public RelativeHumidity(in StdMsgs.Header Header, double RelativeHumidity_, double Variance)
        {
            this.Header = Header;
            this.RelativeHumidity_ = RelativeHumidity_;
            this.Variance = Variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public RelativeHumidity(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            RelativeHumidity_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RelativeHumidity(ref b);
        }
        
        RelativeHumidity IDeserializable<RelativeHumidity>.RosDeserialize(ref Buffer b)
        {
            return new RelativeHumidity(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RelativeHumidity_);
            b.Serialize(Variance);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/RelativeHumidity";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61Uy27bMBC88ysW0CFJAavuAz0Y6M195FCgQHI31tLaIkqRKknZ9d93loofTZu0hwoG" +
                "bMkzs7uzQ1FFd9ZvnVAUbvGLNjH0xLh1nO1OqBt729p8oCQ+hVgTLWVjvSTKHVgABQobGjhmy85QRUOU" +
                "lMYo+njPWSLteAiRciiUxHkETdqHxyc4Z9TN0g8SFSK1MfQZXUGgm74ur4qy7SVl7gctpMq9sAr14rOh" +
                "p68KM3IvK9uSnaZwodE5/FHo0czoY+MC53dvT7asTpCKPvwoI1zwfzPvb+3A8nk9V4de1fP6ebQC0bcP" +
                "R8+fMvx5GRRCn0qFXemPWg+bwmAXDuw4WvaN/NKSNmQ9KoOum+V0xo3+mw97b97/58t8ufu0oJTbVZ+2" +
                "6eWUFIM8Z/YtxxZxyNxyZtogZZ3ddhJnTnbiqKQGbZZ/82GQVIN43+kUibbikUDnDjQmgLCUJvT96C1C" +
                "IufUHflgWo/kFgeb0XEEPkQcJoWXqKk6Pkm+j6KW3C4XwPgkzagxQSXrGxzApAfwdklmhJlvXivBVPf7" +
                "MFNvt9jrOfK5w3FBszKFr3i+QI0X03A1tGGOoEqb6Lo8W+E23RCKoAUZQtPRNTr/esgdoqu5LTtb42UA" +
                "4QYOQPVKSVc3F8q+SHv24Sg/KZ5r/IusP+nqTLMOO3M6fRq3MBDAIYadbQFdH4pI4yyCSs6uI8eDUdZU" +
                "0lQf1WOAwCobwTenFBpb3jJ7mzuTcpxebtPBN+YniNWhcPkEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
