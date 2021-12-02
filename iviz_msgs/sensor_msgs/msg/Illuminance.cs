/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Illuminance : IDeserializable<Illuminance>, IMessage
    {
        // Single photometric illuminance measurement.  Light should be assumed to be
        // measured along the sensor's x-axis (the area of detection is the y-z plane).
        // The illuminance should have a 0 or positive value and be received with
        // the sensor's +X axis pointing toward the light source.
        // Photometric illuminance is the measure of the human eye's sensitivity of the
        // intensity of light encountering or passing through a surface.
        // All other Photometric and Radiometric measurements should
        // not use this message.
        // This message cannot represent:
        // Luminous intensity (candela/light source output)
        // Luminance (nits/light output per area)
        // Irradiance (watt/area), etc.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp is the time the illuminance was measured
        // frame_id is the location and direction of the reading
        [DataMember (Name = "illuminance")] public double Illuminance_; // Measurement of the Photometric Illuminance in Lux.
        [DataMember (Name = "variance")] public double Variance; // 0 is interpreted as variance unknown
    
        /// Constructor for empty message.
        public Illuminance()
        {
        }
        
        /// Explicit constructor.
        public Illuminance(in StdMsgs.Header Header, double Illuminance_, double Variance)
        {
            this.Header = Header;
            this.Illuminance_ = Illuminance_;
            this.Variance = Variance;
        }
        
        /// Constructor with buffer.
        internal Illuminance(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Illuminance_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Illuminance(ref b);
        
        Illuminance IDeserializable<Illuminance>.RosDeserialize(ref Buffer b) => new Illuminance(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Illuminance_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/Illuminance";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8cf5febb0952fca9d650c3d11a81a188";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1UTY/TShC8+1e0lMPuwksWAeIQ6R2ehICVQELAgduqd9yxR4xnzHwkG3491WNn4xyQ" +
                "ODzLUjLtnurqqp6hFX21vnNCYx9yGCRHa8g6Vwbr2RuhQTiVKIP4vCH6aLs+U+pDcS09CHFKZZCWcsCq" +
                "odUpvSV2wXeUe6EkPoV4lehxzY820bUGOQpT2FErWUy2wRO+6Ifj+heNjr3cbBTvG0JLOnPpnvfAoBcU" +
                "Io0h2Wyx3rMriPrKLIoRBFs62Nwr0gWV59+pchmD9RkCoIEDx7YmuanHUKKRTaNbP/9Bm5ny3LO2o8u+" +
                "DOxJjoI6WlDJ2XycPyseatZ4jU3lxJtQEI5KRpuCspVXH0PpevSKEjs+MfrPOQpAixfctPUv3NrTeuFd" +
                "mpXTzT5kKkmAjQYGSYk7mcU+B8iw18QoYxS0kbea8VF7DyUtWrhGYiuOb5e6USh5LPnmaU/V69rbnOa8" +
                "KYFGtKCzUDPvYgT7KfXAOd/WL/+QZKNtfxBukd5PP+cH3lqwzjyMJ0s0UP8s7TpweprPZrH/8lnRLvIg" +
                "97Y9gblguI6o6ttaTFZdzXaDYgunwG/nAuc3ry9qToifzkacti19u1vOlIdgj5sF3J7jpMkTwRfKTB2I" +
                "8CbrYUvnrOJ/+HDwzb//89N8+vp+Sym390Pq0u1kRoP7I0MWPTtohlvOTDvMbw+TJa6d7MVRtQY069d8" +
                "HCVtmnna8HbiJbJzRx3KepWYMAzFW6g+OXmxHzuhEeOExGxNcRyRHyI80PTqnaLjTfKz4FwJ3b3dIscn" +
                "MUUvClSy3sC3esLu3lJTIOarl7qhWX07hLVq22HGznOVe85KVh71OChPTlvUeDY1twE2xBFUaXHD1dg9" +
                "lumGUAQUZAymp2sw/3zMPaZHZ6B69oDLF8AGCgD1Sjdd3SyQlfaWMBzhBD8hnmv8DayiTLja07qHZ067" +
                "T6WDgHoTxrC3LVIfjhXEOKvT6uxD5Hhs6oGqJZvVO9V4uj2qI/jFbRWMhQHzfZsw10A/naSm+Q1lvwjk" +
                "aQYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
