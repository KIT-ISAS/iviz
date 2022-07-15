/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class RelativeHumidity : IDeserializable<RelativeHumidity>, IMessageRos2
    {
        // Single reading from a relative humidity sensor.
        // Defines the ratio of partial pressure of water vapor to the saturated vapor
        // pressure at a temperature.
        /// <summary> Timestamp of the measurement </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location of the humidity sensor
        /// <summary> Expression of the relative humidity </summary>
        [DataMember (Name = "relative_humidity")] public double RelativeHumidity_;
        // from 0.0 to 1.0.
        // 0.0 is no partial pressure of water vapor
        // 1.0 represents partial pressure of saturation
        /// <summary> 0 is interpreted as variance unknown </summary>
        [DataMember (Name = "variance")] public double Variance;
    
        /// Constructor for empty message.
        public RelativeHumidity()
        {
        }
        
        /// Explicit constructor.
        public RelativeHumidity(in StdMsgs.Header Header, double RelativeHumidity_, double Variance)
        {
            this.Header = Header;
            this.RelativeHumidity_ = RelativeHumidity_;
            this.Variance = Variance;
        }
        
        /// Constructor with buffer.
        public RelativeHumidity(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out RelativeHumidity_);
            b.Deserialize(out Variance);
        }
        
        public RelativeHumidity RosDeserialize(ref ReadBuffer2 b) => new RelativeHumidity(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RelativeHumidity_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, RelativeHumidity_);
            WriteBuffer2.Advance(ref c, Variance);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/RelativeHumidity";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
