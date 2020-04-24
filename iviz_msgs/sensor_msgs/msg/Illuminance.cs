namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Illuminance : IMessage
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
        
        public std_msgs.Header header; // timestamp is the time the illuminance was measured
        // frame_id is the location and direction of the reading
        
        public double illuminance; // Measurement of the Photometric Illuminance in Lux.
        
        public double variance; // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public Illuminance()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out illuminance, ref ptr, end);
            BuiltIns.Deserialize(out variance, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(illuminance, ref ptr, end);
            BuiltIns.Serialize(variance, ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 16;
            size += header.GetLength();
            return size;
        }
    
        public IMessage Create() => new Illuminance();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "sensor_msgs/Illuminance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "8cf5febb0952fca9d650c3d11a81a188";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
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
                
    }
}
