
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
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "diagnostic_msgs/DiagnosticStatus";
    
        public IMessage Create() => new DiagnosticStatus();
    
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
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticStatus()
        {
            name = "";
            message = "";
            hardware_id = "";
            values = new KeyValue[0];
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d0ce08bc6e5ba34c7754f563a9cabaf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1STU/DMAy9R9p/sLQ74+OG1MMOE4cBQ90EB4SQ25rWWpuU2G21f0/SrmIXbkSK/By/" +
                "59hOlnCoWKAhESwJKlcXAloRiKJ2Au4L0ALbgnsuOqwhd03rLFmNoUj0LnN6ZZZgwn5xIpzVBDX1VI9y" +
                "15JHZWfFZCcl2G2T6wm9rdPn5GbCmzTdpcnt5OwP68dNcmcmb0wFy7O9zAhkuyZiKgAz1xMYUc+2BIsN" +
                "BQlCQZJ7bkf2uWAl0dVvG55a5zWIZu08iz/k02BmcoW+GNDTJxejYPahs/zdRXakmS2dXrHu6P0D+mgl" +
                "ki2g93iKic+HKOJyHvsZWKvL+8zCJP+8FuZp/3APBWNpnSjnn42UspprXcw9HukUyh0qVFAHNWbhGTT+" +
                "mrHqECALPdNwMcMpEgcyoaBTj/kRwiN5UG4o9PMDS9UTGHwCAAA=";
                
    }
}
