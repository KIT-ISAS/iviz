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
                "H4sIAAAAAAAAE61UwW7bMAy9+ysI5NBmW9JiG3YosMOAYluAFijWHnYrWJmxhcmSJ1FJs68fKduJeyiw" +
                "wwwDtijq8ZGPIizg3vrGEfRt4NARR2vAOpc769Ebgo4w5UgdeV4D3NimZUhtyK6GJwJMKXdUAwdZVbCY" +
                "3GtAF3wD3BIk8inEswTPK3y2Cc7ViJEQwhZqYjJsgwfZ0Y3D6g/0Dj0t14r3IKY5nTF0izvBgEsIEfqQ" +
                "LFtZ79BlsfrCLJIhMdawt9wq0gsqb39C4dIH69kq0bDHWBcnN+QYcjS0rvTo3Su1GSmPOWs6umxzhx7o" +
                "QBJHAyo5y4dxW/EkZrEX2xCOvAlZzFHJaFJS2cKrjSE3reQqIbY4MfriHARBiy+4aeo/sLbTeqZdGiun" +
                "h31gyIkEWxLoKCVsaCz2yQAGvTpG6iNJGnylHjeae8hplsK5ONbk8GJeNwiZ+8zL45lSr3NvOY1+gwP0" +
                "koL2QvHcxCjsB9c9Ml+UnXdAbDTt74S1uLfD5/SItlZYM3b9JIkays9crj2mY39W8NqzgG3Ejh5tPYG5" +
                "YLC0qNa3tnFs2FFuoViLUsJv6wLyp48vYg6ItychpmNz3TbznvJSsOf1DG6H0R6xCtylMlMFomjDetnS" +
                "ySv7Xz7sffX5Pz/V7f23K0hcP3apSReDGJXMD5ay6N2RZLBGRthK/7YiMsWVox05KNIIzbLLh57Suhq7" +
                "Td6GPEV07qBNWUaJCV2XvZWq00na6Xyl90fuQ4+RrckOo/iHKBqoe9FO0eVN9DuTlmRzfSU+PpHJOigk" +
                "kvVGdCs3bHMNVZZifnivB6rFwz6stLaN9Nipr7hFVrL0rNchlZpfSYw3Q3JrwZbikESpZcIV26Ms0xIk" +
                "iFCgPpgWzoX53YFb6R7tgaLZkyuDxEgFBPVMD50tZ8i+QEtzhAl+QDzF+BdYf8TVnFataOY0+5QbKaBO" +
                "whh2thbXp0MBMc5qtzr7FDEeqnKhSshq8VVrPEyPooh8ZVoFY5GneZu4DLLpJlXVX2W/CORpBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
