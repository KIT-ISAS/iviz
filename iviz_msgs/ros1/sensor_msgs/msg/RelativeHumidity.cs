/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class RelativeHumidity : IDeserializableRos1<RelativeHumidity>, IMessageRos1
    {
        // Single reading from a relative humidity sensor.  Defines the ratio of partial
        // pressure of water vapor to the saturated vapor pressure at a temperature.
        /// <summary> Timestamp of the measurement </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location of the humidity sensor
        /// <summary> Expression of the relative humidity </summary>
        [DataMember (Name = "relative_humidity")] public double RelativeHumidity_;
        // from 0.0 to 1.0.
        // 0.0 is no partial pressure of water vapor
        // 1.0 represents partial pressure of saturation
        /// <summary> 0 is interpreted as variance unknown </summary>
        [DataMember (Name = "variance")] public double Variance;
    
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
        public RelativeHumidity(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out RelativeHumidity_);
            b.Deserialize(out Variance);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new RelativeHumidity(ref b);
        
        public RelativeHumidity RosDeserialize(ref ReadBuffer b) => new RelativeHumidity(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RelativeHumidity_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/RelativeHumidity";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
