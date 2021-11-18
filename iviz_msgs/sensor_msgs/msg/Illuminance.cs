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
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp is the time the illuminance was measured
        // frame_id is the location and direction of the reading
        [DataMember (Name = "illuminance")] public double Illuminance_; // Measurement of the Photometric Illuminance in Lux.
        [DataMember (Name = "variance")] public double Variance; // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public Illuminance()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Illuminance(in StdMsgs.Header Header, double Illuminance_, double Variance)
        {
            this.Header = Header;
            this.Illuminance_ = Illuminance_;
            this.Variance = Variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Illuminance(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Illuminance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8cf5febb0952fca9d650c3d11a81a188";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
