using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/FluidPressure")]
    public sealed class FluidPressure : IMessage
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
        internal FluidPressure(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            FluidPressure_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new FluidPressure(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Header);
            b.Serialize(this.FluidPressure_);
            b.Serialize(this.Variance);
        }
        
        public void Validate()
        {
            if (Header is null) throw new System.NullReferenceException();
            Header.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/FluidPressure";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "804dc5cea1c5306d6a2eb80b9833befe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
    }
}
