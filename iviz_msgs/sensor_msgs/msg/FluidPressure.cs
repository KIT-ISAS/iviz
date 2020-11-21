/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/FluidPressure")]
    public sealed class FluidPressure : IDeserializable<FluidPressure>, IMessage
    {
        // Single pressure reading.  This message is appropriate for measuring the
        // pressure inside of a fluid (air, water, etc).  This also includes
        // atmospheric or barometric pressure.
        // This message is not appropriate for force/pressure contact sensors.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp of the measurement
        // frame_id is the location of the pressure sensor
        [DataMember (Name = "fluid_pressure")] public double FluidPressure_ { get; set; } // Absolute pressure reading in Pascals.
        [DataMember (Name = "variance")] public double Variance { get; set; } // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public FluidPressure()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FluidPressure(StdMsgs.Header Header, double FluidPressure_, double Variance)
        {
            this.Header = Header;
            this.FluidPressure_ = FluidPressure_;
            this.Variance = Variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FluidPressure(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            FluidPressure_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FluidPressure(ref b);
        }
        
        FluidPressure IDeserializable<FluidPressure>.RosDeserialize(ref Buffer b)
        {
            return new FluidPressure(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(FluidPressure_);
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
        [Preserve] public const string RosMessageType = "sensor_msgs/FluidPressure";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "804dc5cea1c5306d6a2eb80b9833befe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1UwW7TQBC9W/I/jJRDW0RSBIhDJA5IFdADElJ7jybrib1ivWt2xw35e97YTQ1USByw" +
                "Yjm2Z968eW/GtKI7H9sgNGQpZcxCWbjBow3RfecL9XjMrRD+8jDkNGTPKnRIGa8YGYgl7aSuaLWA+Fh8" +
                "I5QOxHQIo2/okn1+SUfk4iLqrs4FOJSEeBfGRsqEwtqnMnSSvSOU2XNOvajdnfE3dTVF/skwJn3GEqeT" +
                "6ydmLkVlp1QklpTLjPQZPUumbr4sx4rUA165H6wXtPnYtPQSFYl/O1Z0yNzLDo2DluWF5Fh9imecJ0Iz" +
                "kYnGISTWd29nxXZPEUD7sC8pjPrcJihHX7k4qLj5DeOBIUF0sjB6ZVR8hAEAUWmIyxI1xm8xHWNdvf/P" +
                "R119ufu0paLNri9tuZ6lrisMnnJsODeQVLlh5cmuzrdwfh3kQQJNyoPp9FZPg1iTj7bj10qUzCGcaCyI" +
                "0gR3+36MHlLLYt0ZwFIhF9PAWb0bA2ckpAwdp2ExxyZ8O4t8H8WUub3Z2tAUcaN6kDrZsEL9Yurf3iB4" +
                "hKhvXlsGEu+PaW0itxikZXi0YzXG8mMycBJ/a2VezD1uAA+RBIWaQpfTsx1uyxWhDljIkFxHl+b2STuM" +
                "kc3Q5N4eywtkTEAA7IUlXVz9Cm3UtxQ5pjP+DLkU+RdcQ3kEtrbWHcwLJkEZW+iISKzdA5a+of1pQnHB" +
                "Y0ko+H3mfKorS5uLAuSjiY0w5E3e2IeglORsbRs6eu3qqmDnUeC8STbePwG9l+i5sAQAAA==";
                
    }
}
