
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/RelativeHumidity";
    
        public IMessage Create() => new RelativeHumidity();
    
        public int GetLength()
        {
            int size = 16;
            size += header.GetLength();
            return size;
        }
    
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
