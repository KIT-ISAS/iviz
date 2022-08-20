/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class DiagnosticStatus : IDeserializable<DiagnosticStatus>, IMessage
    {
        // This message holds the status of an individual component of the robot.
        // 
        // Possible levels of operations
        public const byte OK = 0;
        public const byte WARN = 1;
        public const byte ERROR = 2;
        public const byte STALE = 3;
        /// <summary> Level of operation enumerated above </summary>
        [DataMember (Name = "level")] public byte Level;
        /// <summary> A description of the test/component reporting </summary>
        [DataMember (Name = "name")] public string Name;
        /// <summary> A description of the status </summary>
        [DataMember (Name = "message")] public string Message;
        /// <summary> A hardware unique string </summary>
        [DataMember (Name = "hardware_id")] public string HardwareId;
        /// <summary> An array of values associated with the status </summary>
        [DataMember (Name = "values")] public KeyValue[] Values;
    
        public DiagnosticStatus()
        {
            Name = "";
            Message = "";
            HardwareId = "";
            Values = System.Array.Empty<KeyValue>();
        }
        
        public DiagnosticStatus(ref ReadBuffer b)
        {
            b.Deserialize(out Level);
            b.DeserializeString(out Name);
            b.DeserializeString(out Message);
            b.DeserializeString(out HardwareId);
            b.DeserializeArray(out Values);
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = new KeyValue(ref b);
            }
        }
        
        public DiagnosticStatus(ref ReadBuffer2 b)
        {
            b.Deserialize(out Level);
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeString(out Message);
            b.Align4();
            b.DeserializeString(out HardwareId);
            b.Align4();
            b.DeserializeArray(out Values);
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = new KeyValue(ref b);
            }
        }
        
        public DiagnosticStatus RosDeserialize(ref ReadBuffer b) => new DiagnosticStatus(ref b);
        
        public DiagnosticStatus RosDeserialize(ref ReadBuffer2 b) => new DiagnosticStatus(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Level);
            b.Serialize(Name);
            b.Serialize(Message);
            b.Serialize(HardwareId);
            b.Serialize(Values.Length);
            foreach (var t in Values)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Level);
            b.Serialize(Name);
            b.Serialize(Message);
            b.Serialize(HardwareId);
            b.Serialize(Values.Length);
            foreach (var t in Values)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Message is null) BuiltIns.ThrowNullReference();
            if (HardwareId is null) BuiltIns.ThrowNullReference();
            if (Values is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] is null) BuiltIns.ThrowNullReference(nameof(Values), i);
                Values[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Message);
                size += WriteBuffer.GetStringSize(HardwareId);
                size += WriteBuffer.GetArraySize(Values);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Level
            c = WriteBuffer2.AddLength(c, Name);
            c = WriteBuffer2.AddLength(c, Message);
            c = WriteBuffer2.AddLength(c, HardwareId);
            c = WriteBuffer2.Align4(c);
            c += 4; // Values.Length
            foreach (var t in Values)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "diagnostic_msgs/DiagnosticStatus";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d0ce08bc6e5ba34c7754f563a9cabaf1";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61STW+DMAy951dY6n3dx20Shx6qHfbRiVbbYZoqQzywCgmLDYh/PwJF62W3RYr8HL/n" +
                "2E5WcChZoCYRLAhKX1kBLQlEUVsB/wXogJ3ljm2LFeS+brwjpzEUicFnXq/MCsy4X70IZxVBRR1Vk9w3" +
                "FFDZOzHZoAS7x+R6Ru+b9CW5mfE2TXdpcjs7+8PmaZvcmdmbUsHqbC8zArm2jpgsYOY7AiMa2BXgsKZR" +
                "gmBJ8sDNxD4XrCS6/m0jUOODjqJFu8ziD/k8mIVcYrA9BjqynQSLD63j7zayI8080vCGVUsfn9BFK5Hs" +
                "AEPAISY+H6KIz3nqp2ctL+8zJvnnZZ73D/dgGQvnRTk/1lLIeql0afBEw1hrX6KCeqgwG99A45eZSh4D" +
                "5KBj6i8GOEfiNGY06jRgfoLxhQIo12TMD2XzWS14AgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
