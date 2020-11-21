/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract (Name = "diagnostic_msgs/DiagnosticStatus")]
    public sealed class DiagnosticStatus : IDeserializable<DiagnosticStatus>, IMessage
    {
        // This message holds the status of an individual component of the robot.
        // 
        // Possible levels of operations
        public const byte OK = 0;
        public const byte WARN = 1;
        public const byte ERROR = 2;
        public const byte STALE = 3;
        [DataMember (Name = "level")] public byte Level { get; set; } // level of operation enumerated above 
        [DataMember (Name = "name")] public string Name { get; set; } // a description of the test/component reporting
        [DataMember (Name = "message")] public string Message { get; set; } // a description of the status
        [DataMember (Name = "hardware_id")] public string HardwareId { get; set; } // a hardware unique string
        [DataMember (Name = "values")] public KeyValue[] Values { get; set; } // an array of values associated with the status
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticStatus()
        {
            Name = "";
            Message = "";
            HardwareId = "";
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
        public DiagnosticStatus(ref Buffer b)
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
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Message);
                size += BuiltIns.UTF8.GetByteCount(HardwareId);
                foreach (var i in Values)
                {
                    size += i.RosMessageLength;
                }
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
                "H4sIAAAAAAAACq1STW+DMAy9I/EfLPW+7uM2iUMP1Q7d1olW22GaKgMeWIWExQbUf7+E0q477DaQiB2/" +
                "9/LsMINtxQINiWBJUNm6ENCKQBS1E7CfgAbYFNxz0WENuW1aa8hoKAWgs5nVqziaQRyF74sV4awmqKmn" +
                "elSwLTlUtkbiKDsowXqVXE/h2yJ9Tm6mZJmm6zS5nbLNdvG4TO6C7JiPgjCb1ktdINM1IaYCMLM9eS+i" +
                "jk0JBhvyHISCJHfcjvDJupLo/KchR6116kln8mksf/CPMzqjK3TFgI52XIyMUw6d4a8uwAMsjlZ0eMW6" +
                "o/cP6MMqAW0AncNDkJ42UcTmPPY0sFa/Tgxv8s9PHD1tHu6hYCyNFeV810gp85Pbc5t7OnjDQ4UKaqHG" +
                "zF+Ghp9o9O0LZKBnGi4HeSyFoRwjT1SH+R78XTlQbrx8HH0DgX9VC40CAAA=";
                
    }
}
