namespace Iviz.Msgs.sensor_msgs
{
    public sealed class RelativeHumidity : IMessage
    {
        // Single reading from a relative humidity sensor.  Defines the ratio of partial
        // pressure of water vapor to the saturated vapor pressure at a temperature.
        
        public std_msgs.Header header; // timestamp of the measurement
        // frame_id is the location of the humidity sensor
        
        public double relative_humidity; // Expression of the relative humidity
        // from 0.0 to 1.0.
        // 0.0 is no partial pressure of water vapor
        // 1.0 represents partial pressure of saturation
        
        public double variance; // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public RelativeHumidity()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out relative_humidity, ref ptr, end);
            BuiltIns.Deserialize(out variance, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(relative_humidity, ref ptr, end);
            BuiltIns.Serialize(variance, ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 16;
            size += header.GetLength();
            return size;
        }
    
        public IMessage Create() => new RelativeHumidity();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/RelativeHumidity";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
