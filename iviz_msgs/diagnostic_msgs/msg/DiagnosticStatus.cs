using System.Runtime.Serialization;

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
        
        public byte level { get; set; } // level of operation enumerated above 
        public string name { get; set; } // a description of the test/component reporting
        public string message { get; set; } // a description of the status
        public string hardware_id { get; set; } // a hardware unique string
        public KeyValue[] values { get; set; } // an array of values associated with the status
        
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticStatus()
        {
            name = "";
            message = "";
            hardware_id = "";
            values = System.Array.Empty<KeyValue>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DiagnosticStatus(byte level, string name, string message, string hardware_id, KeyValue[] values)
        {
            this.level = level;
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
            this.message = message ?? throw new System.ArgumentNullException(nameof(message));
            this.hardware_id = hardware_id ?? throw new System.ArgumentNullException(nameof(hardware_id));
            this.values = values ?? throw new System.ArgumentNullException(nameof(values));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DiagnosticStatus(Buffer b)
        {
            this.level = b.Deserialize<byte>();
            this.name = b.DeserializeString();
            this.message = b.DeserializeString();
            this.hardware_id = b.DeserializeString();
            this.values = b.DeserializeArray<KeyValue>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new DiagnosticStatus(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.level);
            b.Serialize(this.name);
            b.Serialize(this.message);
            b.Serialize(this.hardware_id);
            b.SerializeArray(this.values, 0);
        }
        
        public void Validate()
        {
            if (name is null) throw new System.NullReferenceException();
            if (message is null) throw new System.NullReferenceException();
            if (hardware_id is null) throw new System.NullReferenceException();
            if (values is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += BuiltIns.UTF8.GetByteCount(name);
                size += BuiltIns.UTF8.GetByteCount(message);
                size += BuiltIns.UTF8.GetByteCount(hardware_id);
                for (int i = 0; i < values.Length; i++)
                {
                    size += values[i].RosMessageLength;
                }
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "diagnostic_msgs/DiagnosticStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "d0ce08bc6e5ba34c7754f563a9cabaf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61STW+DMAy951dY6n3dx20Shx6qHfbRiVbbYZoqQzywCgmLDYh/PwJF62W3RYr8HL/n" +
                "2E5WcChZoCYRLAhKX1kBLQlEUVsB/wXogJ3ljm2LFeS+brwjpzEUicFnXq/MCsy4X70IZxVBRR1Vk9w3" +
                "FFDZOzHZoAS7x+R6Ru+b9CW5mfE2TXdpcjs7+8PmaZvcmdmbUsHqbC8zArm2jpgsYOY7AiMa2BXgsKZR" +
                "gmBJ8sDNxD4XrCS6/m0jUOODjqJFu8ziD/k8mIVcYrA9BjqynQSLD63j7zayI8080vCGVUsfn9BFK5Hs" +
                "AEPAISY+H6KIz3nqp2ctL+8zJvnnZZ73D/dgGQvnRTk/1lLIeql0afBEw1hrX6KCeqgwG99A45eZSh4D" +
                "5KBj6i8GOEfiNGY06jRgfoLxhQIo12TMD2XzWS14AgAA";
                
    }
}
