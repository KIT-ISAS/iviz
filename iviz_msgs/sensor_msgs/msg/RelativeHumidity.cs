using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class RelativeHumidity : IMessage
    {
        // Single reading from a relative humidity sensor.  Defines the ratio of partial
        // pressure of water vapor to the saturated vapor pressure at a temperature.
        
        public std_msgs.Header header { get; set; } // timestamp of the measurement
        // frame_id is the location of the humidity sensor
        
        public double relative_humidity { get; set; } // Expression of the relative humidity
        // from 0.0 to 1.0.
        // 0.0 is no partial pressure of water vapor
        // 1.0 represents partial pressure of saturation
        
        public double variance { get; set; } // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public RelativeHumidity()
        {
            header = new std_msgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public RelativeHumidity(std_msgs.Header header, double relative_humidity, double variance)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.relative_humidity = relative_humidity;
            this.variance = variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal RelativeHumidity(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.relative_humidity = b.Deserialize<double>();
            this.variance = b.Deserialize<double>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new RelativeHumidity(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.Serialize(this.relative_humidity);
            b.Serialize(this.variance);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/RelativeHumidity";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
                
    }
}
