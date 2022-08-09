/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Temperature : IDeserializable<Temperature>, IMessage
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
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Temperature_);
            b.Deserialize(out Variance);
        }
        
        public Temperature(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 8;  // Temperature_
            c += 8;  // Variance
            return c;
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
    }
}
