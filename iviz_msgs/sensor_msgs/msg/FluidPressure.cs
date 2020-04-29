using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class FluidPressure : IMessage
    {
        // Single pressure reading.  This message is appropriate for measuring the
        // pressure inside of a fluid (air, water, etc).  This also includes
        // atmospheric or barometric pressure.
        
        // This message is not appropriate for force/pressure contact sensors.
        
        public std_msgs.Header header; // timestamp of the measurement
        // frame_id is the location of the pressure sensor
        
        public double fluid_pressure; // Absolute pressure reading in Pascals.
        
        public double variance; // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public FluidPressure()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out fluid_pressure, ref ptr, end);
            BuiltIns.Deserialize(out variance, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(fluid_pressure, ref ptr, end);
            BuiltIns.Serialize(variance, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        public IMessage Create() => new FluidPressure();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/FluidPressure";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "804dc5cea1c5306d6a2eb80b9833befe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
