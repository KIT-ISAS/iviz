/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class Temperature : IDeserializableRos2<Temperature>, IMessageRos2
    {
        // Single temperature reading.
        /// <summary> Timestamp is the time the temperature was measured </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location of the temperature reading
        /// <summary> Measurement of the Temperature in Degrees Celsius. </summary>
        [DataMember (Name = "temperature")] public double Temperature_;
        /// <summary> 0 is interpreted as variance unknown. </summary>
        [DataMember (Name = "variance")] public double Variance;
    
        /// Constructor for empty message.
        public Temperature()
        {
        }
        
        /// Explicit constructor.
        public Temperature(in StdMsgs.Header Header, double Temperature_, double Variance)
        {
            this.Header = Header;
            this.Temperature_ = Temperature_;
            this.Variance = Variance;
        }
        
        /// Constructor with buffer.
        public Temperature(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Temperature_);
            b.Deserialize(out Variance);
        }
        
        public Temperature RosDeserialize(ref ReadBuffer2 b) => new Temperature(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Temperature_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Temperature_);
            WriteBuffer2.AddLength(ref c, Variance);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Temperature";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
