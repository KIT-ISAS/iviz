/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [Preserve, DataContract (Name = "diagnostic_msgs/DiagnosticStatus")]
    public sealed class DiagnosticStatus : IDeserializable<DiagnosticStatus>, IMessage
    {
        // This message holds the status of an individual component of the robot.
        // 
        // Possible levels of operations
        public const byte OK = 0;
        public const byte WARN = 1;
        public const byte ERROR = 2;
        public const byte STALE = 3;
        [DataMember (Name = "level")] public byte Level; // level of operation enumerated above 
        [DataMember (Name = "name")] public string Name; // a description of the test/component reporting
        [DataMember (Name = "message")] public string Message; // a description of the status
        [DataMember (Name = "hardware_id")] public string HardwareId; // a hardware unique string
        [DataMember (Name = "values")] public KeyValue[] Values; // an array of values associated with the status
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticStatus()
        {
            Name = string.Empty;
            Message = string.Empty;
            HardwareId = string.Empty;
            Values = System.Array.Empty<KeyValue>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DiagnosticStatus(byte Level, string Name, string Message, string HardwareId, KeyValue[] Values)
        {
            this.Level = Level;
            this.Name = Name;
            this.Message = Message;
            this.HardwareId = HardwareId;
            this.Values = Values;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DiagnosticStatus(ref Buffer b)
        {
            Level = b.Deserialize<byte>();
            Name = b.DeserializeString();
            Message = b.DeserializeString();
            HardwareId = b.DeserializeString();
            Values = b.DeserializeArray<KeyValue>();
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = new KeyValue(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DiagnosticStatus(ref b);
        }
        
        DiagnosticStatus IDeserializable<DiagnosticStatus>.RosDeserialize(ref Buffer b)
        {
            return new DiagnosticStatus(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Level);
            b.Serialize(Name);
            b.Serialize(Message);
            b.Serialize(HardwareId);
            b.SerializeArray(Values, 0);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (HardwareId is null) throw new System.NullReferenceException(nameof(HardwareId));
            if (Values is null) throw new System.NullReferenceException(nameof(Values));
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] is null) throw new System.NullReferenceException($"{nameof(Values)}[{i}]");
                Values[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Message);
                size += BuiltIns.GetStringSize(HardwareId);
                size += BuiltIns.GetArraySize(Values);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "diagnostic_msgs/DiagnosticStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d0ce08bc6e5ba34c7754f563a9cabaf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1STU/DMAy9R9p/sLQ74+OG1MMOE4cBQ90EB4SQ25rWWpuU2G21f0/SrmIXbkSK/By/" +
                "59hOlnCoWKAhESwJKlcXAloRiKJ2Au4L0ALbgnsuOqwhd03rLFmNoUj0LnN6ZZZgwn5xIpzVBDX1VI9y" +
                "15JHZWfFZCcl2G2T6wm9rdPn5GbCmzTdpcnt5OwP68dNcmcmb0wFy7O9zAhkuyZiKgAz1xMYUc+2BIsN" +
                "BQlCQZJ7bkf2uWAl0dVvG55a5zWIZu08iz/k02BmcoW+GNDTJxejYPahs/zdRXakmS2dXrHu6P0D+mgl" +
                "ki2g93iKic+HKOJyHvsZWKvL+8zCJP+8FuZp/3APBWNpnSjnn42UspprXcw9HukUyh0qVFAHNWbhGTT+" +
                "mrHqECALPdNwMcMpEgcyoaBTj/kRwiN5UG4o9PMDS9UTGHwCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
