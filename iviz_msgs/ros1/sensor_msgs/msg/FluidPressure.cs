/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class FluidPressure : IDeserializable<FluidPressure>, IMessageRos1
    {
        // Single pressure reading.  This message is appropriate for measuring the
        // pressure inside of a fluid (air, water, etc).  This also includes
        // atmospheric or barometric pressure.
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
        public FluidPressure(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out FluidPressure_);
            b.Deserialize(out Variance);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FluidPressure(ref b);
        
        public FluidPressure RosDeserialize(ref ReadBuffer b) => new FluidPressure(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(FluidPressure_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/FluidPressure";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "804dc5cea1c5306d6a2eb80b9833befe";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61UwYrbMBC96ysGcthN6WZLW3oI9FBY2u6hUNi9h4k8sUVlyZXGSfP3fWNvkqVLoYcK" +
                "B8fWzNOb92ZMC3oIqY1CQ5FaxyJUhBu8WhE9dqFSj9fcCuEvD0PJQwmsQrtcsMXIQCxpJ44WF4yQamiE" +
                "8o6YdnEMDV1zKK/pgFTcRP3yhM+xZsT7ODZSDYS1z3XopARPOGTLJfei9nSCXzmL+5NdyvqCIX5ebs+0" +
                "fE7KXqlKqrlUw/mKaqVQN98ua0EaAK7cD1YGCnwqV3pJ6uhva0G7wr1sUDI4WVrMnjXkdII5s5lZgMMu" +
                "ZtYP72elNud9YH3a1hxHfekOFKPvXD3UWz1D2DNqT14ubN4YjZAgOyBUGuJ6iRrTj5QPyX38z8t9e/iy" +
                "pqrNpq9tvZ01dmg15dRwaSClcsPKk0ldaOH2TZS9RJoUB81pV4+DoL4nr3G1kqRwjEcaK4I0w9K+H1Pw" +
                "5vjZsVM+MqET08BFgx8jF8TnAgGnBjGjDB1XlZ+jmCT3d2trkyp+1ABCR+tNiF5N9Ps7ciPEfPfWEtzi" +
                "8ZBvTNsWrXNpF+1Yjaz8mlybNF/jjFdzcStgQxzBKU2l6+ndBo91STgEFGTIvqNrc/ioHRrHumbybBun" +
                "TofrEahXlnS1fIacJujEKZ/gZ8TLGf8Cm864VtNNB8+iVV/HFgIiEDO2x3g3tD1OID4GzATFsC1cjs6y" +
                "5iPd4rNpjCBkTY7YwNeavU1oQ4egnas6fUJOY+Pcb9ZVTmGUBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
