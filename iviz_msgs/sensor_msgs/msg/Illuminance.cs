/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Illuminance")]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp is the time the illuminance was measured
        // frame_id is the location and direction of the reading
        [DataMember (Name = "illuminance")] public double Illuminance_ { get; set; } // Measurement of the Photometric Illuminance in Lux.
        [DataMember (Name = "variance")] public double Variance { get; set; } // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public Illuminance()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Illuminance(StdMsgs.Header Header, double Illuminance_, double Variance)
        {
            this.Header = Header;
            this.Illuminance_ = Illuminance_;
            this.Variance = Variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Illuminance(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Illuminance_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Illuminance(ref b);
        }
        
        Illuminance IDeserializable<Illuminance>.RosDeserialize(ref Buffer b)
        {
            return new Illuminance(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Illuminance_);
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
        [Preserve] public const string RosMessageType = "sensor_msgs/Illuminance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8cf5febb0952fca9d650c3d11a81a188";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1UTW8TMRC9R8p/GKmHtkBaBIhDJQ5ICKgEEoIeuFVT72TXwmsv/kgafj1vvLtsckDi" +
                "QNQqsT1+8+a9GdMZfbO+dUJDF3LoJUdryDpXeuvZG6FeOJUovfh8RfTJtl2m1IXiGnoQ4pRKLw3lgNV6" +
                "RWdzfEPsgm8pd0JJfArxPNHjhh9togvd5ChMYUuNZDHZBk840YPD5hcNjr1cXlXAO+wdE5qSd7wDCD2n" +
                "EGkIyWaL9Y5dwa6v3KIYwWZDe5u7CnVC5ul3qmyGYH2GBqhhz7GpQW4sM5RoBCzq5S9/EWiiPdWtJemy" +
                "Kz17koMgk6ZUfjYfpuMKiLT1oG6OGcWbULAdlY8WBn0rtS6G0naoFzm2vJB66xwFAMYTeirAV27svD7y" +
                "ME361ds+ZCpJAI8iekmJW5lFX3bIsNfIKEMU1JJvasgnVSCUdFTHBSIbcXx9rB+FkoeSL5dLVbYLb3Oa" +
                "AscIGlCGtsUYehsjShhj95zzdT16RpLNWP1H4QY3uvFr+cBnC+qZ+2E2Rzfqj2Pj9pz+dCvg/vY5o23k" +
                "Xu5tM6O5YLh2rOrcWPRZXU3Og2UDzyrFrQucX786STtifl4smS8eO3h73GAesj1enQDuOI7S/CH5XNmp" +
                "FREuZZ2/tEQV/8OHvV+v3vznz3r1+duHG0q5ue9Tm65HT9YrPCsZ6ug8oSJuODNt0dAd/Ja4cbITR9Ui" +
                "MK2n+TBIQpFT7+GvFS+RnTtoj9YnxoS+L95C/dHSEwC9CqkYQxOzNcVxxIUQYYbGVxMrvv4n+VkwbEK3" +
                "724Q5ZOYoi8IkllvYGEdu9t3CC4Q9eULvYGLd/uwUZFbdNzSZbnjrIzlUSdEyXLCkJzRk7HGK8BDJEGi" +
                "Bs9f3bvHMl0S8oCFDMF0dAH6Xw65Qy9pP1T3HvA0A9lAB8Ce66VzzMcCrdRvCJ0SZvwRcknyL7iKMgFr" +
                "WZsO5jmVIJUWOuozGcPONoh9OFQU46z2rrMPkeNhvaojVpMC5L2KPT4r1Rt84yULxsKJ+T1OaHQkmIdL" +
                "2/s3TG2jFI4GAAA=";
                
    }
}
