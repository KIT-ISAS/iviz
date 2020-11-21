/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/RelativeHumidity")]
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
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public RelativeHumidity(StdMsgs.Header Header, double RelativeHumidity_, double Variance)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACq1Uy27bMBC8G/A/LKBDkgJW3Qd6MNCb+8ihQIHkbqyltUSUIlWSsuu/7yzlV5oG7qEE" +
                "DFnU7Ozu7JBU0INxjRUKwjX+0Sb4jhivlpPZCrVDZ2qT9hTFRR9KoqVsjJNIqUUUQJ78hnoOybCdTqig" +
                "PkiMQxDd33GSQFvufaDkc0zkNCBO6sP2Cc4JiZN0veAzNsrpBHxfURgo2vFxuQpKppOYuOs1lXJ3wkrV" +
                "iUsIfXkV6JM7WZmazNiJ9ZX24o5Mf/SdS9lYz+nD+5M4qxOooE+/ch8XDM8kvFoRlJ+Xc9XpTTkvr8AV" +
                "idqdP2r/ku5XeJAKpWosRIt/JTtMDL09kWHLwbCr5ElVWpNxSA4CnTHHM25wP5zfgeTjf17TybeHLwuK" +
                "qV51sYmvR89MJ3B3YldzqOGMxDUnpg0s15qmlTCzshVL2UCoNH9N+14ipC/osdVOIjXi4Edr9zREoDCc" +
                "ynfd4Az8ImcHHgk01DgYOQtZDZYDAnzA4VJ8tl3m11+Un4OoMvfLBVAuSjWoZZDMuApHMuqRvF8CPEDU" +
                "d281AoGPOz9TkRvM+HwGUosThIpltGIWf6FpXo09lqCHSIJEdaTbvLfCa7wj5EEV0vuqpVuU/32fWjhZ" +
                "bZynt8YNAeYKOoD2RoNu7i6ptfQFOXb+yD9SnpP8C6+yHIi1rVmL4VmVIA4NdASyD35ramDX+8xSWQPb" +
                "kjXrwAEnTMPGpCD5rGIDhrg8Gzw5Rl+ZfPvsTGqnk5jCeO2N14Ea/DfcpkovFQUAAA==";
                
    }
}
