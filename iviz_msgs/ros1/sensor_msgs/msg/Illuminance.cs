/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Illuminance : IHasSerializer<Illuminance>, IMessage
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
        /// <summary> Timestamp is the time the illuminance was measured </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location and direction of the reading
        /// <summary> Measurement of the Photometric Illuminance in Lux. </summary>
        [DataMember (Name = "illuminance")] public double Illuminance_;
        /// <summary> 0 is interpreted as variance unknown </summary>
        [DataMember (Name = "variance")] public double Variance;
    
        public Illuminance()
        {
        }
        
        public Illuminance(in StdMsgs.Header Header, double Illuminance_, double Variance)
        {
            this.Header = Header;
            this.Illuminance_ = Illuminance_;
            this.Variance = Variance;
        }
        
        public Illuminance(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Illuminance_);
            b.Deserialize(out Variance);
        }
        
        public Illuminance(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Illuminance_);
            b.Deserialize(out Variance);
        }
        
        public Illuminance RosDeserialize(ref ReadBuffer b) => new Illuminance(ref b);
        
        public Illuminance RosDeserialize(ref ReadBuffer2 b) => new Illuminance(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Illuminance_);
            b.Serialize(Variance);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Illuminance_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 16;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 8; // Illuminance_
            size += 8; // Variance
            return size;
        }
    
        public const string MessageType = "sensor_msgs/Illuminance";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8cf5febb0952fca9d650c3d11a81a188";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61UTWvcMBC9+1cM7CFJ292kH/QQ6KEQ2gYSCEkOhVLCRJ61RWXJlUa72f76zsj2rnMI" +
                "9FBj2NV49ObNeyPBAu6sbxxB3wYOHXG0BqxzubMevSHoCFOO1JHnFcCVbVqG1IbsangkwJRyRzVwkFUF" +
                "iym9BnTBN8AtQSKfQjxK8LTEJ5vgWIMYCSGsoSYmwzZ4kC/6Ybf8A71DTycrxbuX0JzOWLrFjWDAGYQI" +
                "fUiWraw36LJEfWEWyZAEa9habhXpGZXX36Fw6YP1bJVo2GKsS5Ibegw5GlpVuvXmBW1GymPP2o4u29yh" +
                "B9qR1NGCSs7ybvyseFKzxEtsKEfehCzhqGS0KVG28GpjyE0rvUqJNU6MPjsHQdDiM27a+i3WdlrPvEuj" +
                "crrZB4acSLClgY5SwoZGsQ8BMOg1MVIfSdrgc8240t5DTrMWjiWxJoenc90gZO4zn+z3FL2OveU05g0J" +
                "0EsLOgsl8zJGYT+kbpH5tHx5A8RG2/5GWEt6O/wcHvHWCmvGrp8s0UD5M7dri2k/nxW89CxgHbGjB1tP" +
                "YC4YLCOq+tY2jgM72i0Ua3FK+K1dQP744VnNAfH6YMS0be7b5XymvAj2tJrBbTDaPVaBO1Nm6kAUb1gP" +
                "WzpkZf/Lh62vPv3np7q++3oOieuHLjXpdDCjkvuDRRY9O9IM1sgIa5nfVkymuHS0IQfFGqFZvvKup7Sq" +
                "xmmTtyFPEZ3b6VCWq8SErsveiup0sHbaX+n5kfPQY2RrssMo+SGKB5pevFN0eRP9zqSSXF6cS45PZLJe" +
                "FFLJeiO+lRN2eQFVFjHfv9MNou6P25De/qwW99uwVJEbGbbDgHGLrKzpSc9FKuKfS7FXQ5crKSIqkZSr" +
                "5aorsQdZphOQasKF+mBaOJYWbnbcyhjpMBTzHl25UYxIIahHuunoZIbsC7RMSZjgB8RDjX+B9Xtc7WnZ" +
                "inlOZUi5ESX1SoxhY2tJfdwVEOOsjq2zjxHjrionq5SsFl9U7OEaKdbIr1xbwVjk6eJNXG606UhV1V+Q" +
                "CTXScgYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Illuminance> CreateSerializer() => new Serializer();
        public Deserializer<Illuminance> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Illuminance>
        {
            public override void RosSerialize(Illuminance msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Illuminance msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Illuminance msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Illuminance msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Illuminance msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Illuminance>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Illuminance msg) => msg = new Illuminance(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Illuminance msg) => msg = new Illuminance(ref b);
        }
    }
}
