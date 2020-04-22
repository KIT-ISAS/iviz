
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Illuminance";
    
        public IMessage Create() => new Illuminance();
    
        public int GetLength()
        {
            int size = 16;
            size += header.GetLength();
            return size;
        }
    
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8cf5febb0952fca9d650c3d11a81a188";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1UTYvbSBC9C+Y/FPgwM9nYE7JhD4Y9LIQkAwmEJIfchppWWWrS6lb6wx7vr99XLXks" +
                "HxZyiBDYXap+9eq96qYVfbW+c0JjH3IYJEdryDpXBuvZG6FBOJUog/i8Ifpouz5T6kNxLT0KcUplkJZy" +
                "wKqh1Sm9JXbBd5R7oSQ+hXid6GnNTzbRjQY5ClPYUStZTLbBE77oh+P6Xxode7ndKN43hJZ05tI974FB" +
                "ryhEGkOy2WK9Z1cQ9ZVZFCMItnSwuVekCyp/fKfKZQzWZwiABg4c25rkph5DiUY2jW79/D/azJTnnrUd" +
                "XfZlYE9yFNTRgkrO5uP8WfFQs8ZrbCon3oSCcFQy2hSUrbz6GErXo1eU2PGJ0T/OUQBavOCmrX/h1p7W" +
                "C+/SrJxu9iFTSQJsNDBIStzJLPY5QIa9JkYZo6CNvNWMj9p7KGnRwg0SW3F8t9SNQsljybfPe6peN97m" +
                "NOdNCTSiBZ2FmnkfI9hPqQfO+a5+eUmSjbb9QbhFej/9nB94a8E68zCeLNFA/bO068DpeT6bxf7LZ0W7" +
                "yIM82PYE5oLhOqKqb2sxWXU12w2KLZwCv50LnP96c1FzQvx0NuK0benb/XKmPAR72izg9hwnTZ4JvlJm" +
                "6kCEN1kPWzpnFf/Dh4O/av7+zc9V8+nr+y2l3D4MqUt3kx1XDa6QDGX0+KAfbjkz7TDCPXyWuHayF0fV" +
                "HTCtX/NxlLRp5oHD24mXyM4ddS7rbWLCMBRvIfxk5sV+7IRMjEMSszXFcUR+iLBB06t9io43yc+CoyV0" +
                "/3aLHJ/EFL0rUMl6A+vqIbt/S02Bnn++1g3N6tshrFXeDmN2Hq3cc1ay8qQnQnly2qLGi6m5DbChjqBK" +
                "i0uuxh6wTLeEIkZIxmB6ugHzz8fcY4B0DKptj7h/AWygAFCvddP17QJZaW8J8xFO8BPiucavwCrKhKs9" +
                "rXt45rT7VDoIqJdhDHvbIvXxWEGMszqwzj5Gjsemnqlaslm9U42nC6Q6gl9cWMFYGDBfuQmjDfTTYWqu" +
                "mv8AH/CvH20GAAA=";
                
    }
}
