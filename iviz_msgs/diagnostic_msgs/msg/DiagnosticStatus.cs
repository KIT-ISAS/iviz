namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class DiagnosticStatus : IMessage
    {
        // This message holds the status of an individual component of the robot.
        // 
        
        // Possible levels of operations
        public const byte OK = 0;
        public const byte WARN = 1;
        public const byte ERROR = 2;
        public const byte STALE = 3;
        
        public byte level; // level of operation enumerated above 
        public string name; // a description of the test/component reporting
        public string message; // a description of the status
        public string hardware_id; // a hardware unique string
        public KeyValue[] values; // an array of values associated with the status
        
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticStatus()
        {
            name = "";
            message = "";
            hardware_id = "";
            values = System.Array.Empty<KeyValue>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out level, ref ptr, end);
            BuiltIns.Deserialize(out name, ref ptr, end);
            BuiltIns.Deserialize(out message, ref ptr, end);
            BuiltIns.Deserialize(out hardware_id, ref ptr, end);
            BuiltIns.DeserializeArray(out values, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(level, ref ptr, end);
            BuiltIns.Serialize(name, ref ptr, end);
            BuiltIns.Serialize(message, ref ptr, end);
            BuiltIns.Serialize(hardware_id, ref ptr, end);
            BuiltIns.SerializeArray(values, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 17;
            size += name.Length;
            size += message.Length;
            size += hardware_id.Length;
            for (int i = 0; i < values.Length; i++)
            {
                size += values[i].GetLength();
            }
            return size;
        }
    
        public IMessage Create() => new DiagnosticStatus();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "diagnostic_msgs/DiagnosticStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "d0ce08bc6e5ba34c7754f563a9cabaf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE61STW+DMAy951dY6n3dx20Shx6qHfbRiVbbYZoqQzywCgmLDYh/PwJF62W3RYr8HL/n" +
                "2E5WcChZoCYRLAhKX1kBLQlEUVsB/wXogJ3ljm2LFeS+brwjpzEUicFnXq/MCsy4X70IZxVBRR1Vk9w3" +
                "FFDZOzHZoAS7x+R6Ru+b9CW5mfE2TXdpcjs7+8PmaZvcmdmbUsHqbC8zArm2jpgsYOY7AiMa2BXgsKZR" +
                "gmBJ8sDNxD4XrCS6/m0jUOODjqJFu8ziD/k8mIVcYrA9BjqynQSLD63j7zayI8080vCGVUsfn9BFK5Hs" +
                "AEPAISY+H6KIz3nqp2ctL+8zJvnnZZ73D/dgGQvnRTk/1lLIeql0afBEw1hrX6KCeqgwG99A45eZSh4D" +
                "5KBj6i8GOEfiNGY06jRgfoLxhQIo12TMD2XzWS14AgAA";
                
    }
}
