/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class FluidPressure : IDeserializableRos2<FluidPressure>, IMessageRos2
    {
        // Single pressure reading.  This message is appropriate for measuring the
        // pressure inside of a fluid (air, water, etc).  This also includes
        // atmospheric or barometric pressure.
        //
        // This message is not appropriate for force/pressure contact sensors.
        /// <summary> Timestamp of the measurement </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location of the pressure sensor
        /// <summary> Absolute pressure reading in Pascals. </summary>
        [DataMember (Name = "fluid_pressure")] public double FluidPressure_;
        /// <summary> 0 is interpreted as variance unknown </summary>
        [DataMember (Name = "variance")] public double Variance;
    
        /// Constructor for empty message.
        public FluidPressure()
        {
        }
        
        /// Explicit constructor.
        public FluidPressure(in StdMsgs.Header Header, double FluidPressure_, double Variance)
        {
            this.Header = Header;
            this.FluidPressure_ = FluidPressure_;
            this.Variance = Variance;
        }
        
        /// Constructor with buffer.
        public FluidPressure(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out FluidPressure_);
            b.Deserialize(out Variance);
        }
        
        public FluidPressure RosDeserialize(ref ReadBuffer2 b) => new FluidPressure(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(FluidPressure_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, FluidPressure_);
            WriteBuffer2.AddLength(ref c, Variance);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/FluidPressure";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
