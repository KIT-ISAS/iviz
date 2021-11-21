/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/RelativeHumidity")]
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
        internal RelativeHumidity(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/RelativeHumidity";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1Uy27bMBC8E/A/LKBDkgJW3Qd6MNCb+8ihQIHkbqyltUSUIlWSsuu/76wUx3bTpj2U" +
                "MCCLmp3dnR2SCrqzvnFCUbjGP9rG0BHj1XG2O6F26Gxt84GS+BRiSbSSrfWSKLeIAihQ2FLPMVt2hgrq" +
                "o6Q0RNHtPWeJtOM+RMphDEmcB4RJ/bD9COeMvFm6XvAZG6Ux9BlVgaCdHueroGw7SZm7XhMpcyesRJ34" +
                "bC6wl6tAj9zJ2tZkpy5cqLQPfyT6pWfUsXWB87u3j7KsHyEFffgxtnAW/0S8v5UDyRflQhV6VS7K59EK" +
                "RN0+HDX/k+DP0yAR6tRQyJV+y/UwKTR2psCOo2VfyUVJWpD1yIxwnSynE27w33zY+5l5/5/XzHy5+7Sk" +
                "lOt1l5r0cvLKzMDSmX3NsYYjMtecmbYwWmubVuLcyU4cjcZBpePXfOgllQi8b7WRRI14mNC5Aw0JIMyl" +
                "Cl03eAufyMl4x3hEWg/zjiJWg+MIfIg4Twof3abs+CX5PoiqcrtaAuOTVIM6BZmsr3AGk57B2xWZAXq+" +
                "ea0Bprjfh7nK22C0J9fnFicGxcrkv1H2JXK8mJorwQ11BFnqRNfj3hqv6YaQpBKSPlQtXaPyr4fcwr1q" +
                "3XFsG9wHIK6gAFivNOjq5oxZy16SZx+O9BPjKce/0CrLxKs9zVvMzGn3aWggIIB9DDtbA7o5jCSVs/Aq" +
                "ObuJHA9Go6aUpvioGgOEqHEieHJKobLjRbO3uTUpx+l+m86+mZmf8Xdd9v0EAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
