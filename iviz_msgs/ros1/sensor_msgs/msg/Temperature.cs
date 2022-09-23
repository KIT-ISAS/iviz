/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Temperature : IHasSerializer<Temperature>, IMessage
    {
        // Single temperature reading.
        /// <summary> Timestamp is the time the temperature was measured </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location of the temperature reading
        /// <summary> Measurement of the Temperature in Degrees Celsius </summary>
        [DataMember (Name = "temperature")] public double Temperature_;
        /// <summary> 0 is interpreted as variance unknown </summary>
        [DataMember (Name = "variance")] public double Variance;
    
        public Temperature()
        {
        }
        
        public Temperature(in StdMsgs.Header Header, double Temperature_, double Variance)
        {
            this.Header = Header;
            this.Temperature_ = Temperature_;
            this.Variance = Variance;
        }
        
        public Temperature(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Temperature_);
            b.Deserialize(out Variance);
        }
        
        public Temperature(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align8();
            b.Deserialize(out Temperature_);
            b.Deserialize(out Variance);
        }
        
        public Temperature RosDeserialize(ref ReadBuffer b) => new Temperature(ref b);
        
        public Temperature RosDeserialize(ref ReadBuffer2 b) => new Temperature(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Temperature_);
            b.Serialize(Variance);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Temperature_);
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
            size += 8; // Temperature_
            size += 8; // Variance
            return size;
        }
    
        public const string MessageType = "sensor_msgs/Temperature";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ff71b307acdbe7c871a5a6d7ed359100";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TTYvbMBC961cM5LC7haSf9BDoqUs/Dgulu7dSwsSe2KKy5GrGSfPv+yQ3m5RS6KHC" +
                "YFl682bmvTEt6N7HLgiZDKNktikLZeEWpyvn6AO2kqmfX+e1IPODqPEwkleyXurBvLmgOrDSIKzYt47+" +
                "tha0yzzIxrcnspAaNp8ipd0fnL/KQ3W7kNhev/rtdua7m3MOEu1E8XAB8pFupcsiSm8lqJ/0gm3P2XNs" +
                "5Fzds1KWjyZ5zGLSErp6RE3xW0yH6N785+Xu7t+vSa3dDNrp09kJB8OMY8u5ha7GLRvTLsEh3/WSl0H2" +
                "Eqj6gjLrrR1H0RUCH/rShVInETqEcKRJAbJETRqGKXpILmdfT/GIhFxMI2fzzRQ4A58yLCjwalxhx6Py" +
                "fZIiycfbNTBRpZnMo6AjGBrYprANl+QmiPnyRQmAul8+J33+1S0eDmlZRO4waefpsp6tVC0/oL1qFX+N" +
                "ZE/mLldIApUE6Vql63q2wafeELKhFhlT09M1Wvh0tB4TVWahmrfF2IO4gRRgvSpBVzcXzLFSR47pRD8z" +
                "nnP8C2185C09LXuYF4oMOnVQEsAxp71vAd0eK0kTfJna4LeZ89HV36qmdIt3RWyAEFWtwZtVU+O5DOXB" +
                "W+/UcmE//U/O/QQqbgGn4wMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Temperature> CreateSerializer() => new Serializer();
        public Deserializer<Temperature> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Temperature>
        {
            public override void RosSerialize(Temperature msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Temperature msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Temperature msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Temperature msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Temperature msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Temperature>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Temperature msg) => msg = new Temperature(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Temperature msg) => msg = new Temperature(ref b);
        }
    }
}
