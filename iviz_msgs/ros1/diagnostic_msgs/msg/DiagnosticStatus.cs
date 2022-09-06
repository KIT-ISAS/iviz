/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class DiagnosticStatus : IDeserializable<DiagnosticStatus>, IHasSerializer<DiagnosticStatus>, IMessage
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
            Values = EmptyArray<KeyValue>.Value;
        }
        
        public DiagnosticStatus(ref ReadBuffer b)
        {
            b.Deserialize(out Level);
            b.DeserializeString(out Name);
            b.DeserializeString(out Message);
            b.DeserializeString(out HardwareId);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<KeyValue>.Value
                    : new KeyValue[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new KeyValue(ref b);
                }
                Values = array;
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
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<KeyValue>.Value
                    : new KeyValue[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new KeyValue(ref b);
                }
                Values = array;
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
            get
            {
                int size = 17;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Message);
                size += WriteBuffer.GetStringSize(HardwareId);
                foreach (var msg in Values) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Level
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, HardwareId);
            size = WriteBuffer2.Align4(size);
            size += 4; // Values.Length
            foreach (var msg in Values) size = msg.AddRos2MessageLength(size);
            return size;
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
    
        public Serializer<DiagnosticStatus> CreateSerializer() => new Serializer();
        public Deserializer<DiagnosticStatus> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DiagnosticStatus>
        {
            public override void RosSerialize(DiagnosticStatus msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DiagnosticStatus msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DiagnosticStatus msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DiagnosticStatus msg) => msg.Ros2MessageLength;
            public override void RosValidate(DiagnosticStatus msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DiagnosticStatus>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DiagnosticStatus msg) => msg = new DiagnosticStatus(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DiagnosticStatus msg) => msg = new DiagnosticStatus(ref b);
        }
    }
}
