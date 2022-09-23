/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class KeyValue : IHasSerializer<KeyValue>, IMessage
    {
        /// <summary> What to label this value when viewing </summary>
        [DataMember (Name = "key")] public string Key;
        /// <summary> A value to track over time </summary>
        [DataMember (Name = "value")] public string Value;
    
        public KeyValue()
        {
            Key = "";
            Value = "";
        }
        
        public KeyValue(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
        
        public KeyValue(ref ReadBuffer b)
        {
            Key = b.DeserializeString();
            Value = b.DeserializeString();
        }
        
        public KeyValue(ref ReadBuffer2 b)
        {
            b.Align4();
            Key = b.DeserializeString();
            b.Align4();
            Value = b.DeserializeString();
        }
        
        public KeyValue RosDeserialize(ref ReadBuffer b) => new KeyValue(ref b);
        
        public KeyValue RosDeserialize(ref ReadBuffer2 b) => new KeyValue(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Key);
            b.Serialize(Value);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Key);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Key is null) BuiltIns.ThrowNullReference();
            if (Value is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(Key);
                size += WriteBuffer.GetStringSize(Value);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Key);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Value);
            return size;
        }
    
        public const string MessageType = "diagnostic_msgs/KeyValue";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "cf57fdc6617a881a88c16e768132149c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEy2LQQrAIBDE7r5iwJdty1AXrYJulf6+C/YWSDKsa72Q+SJiJTFYQ5GDBZZ0YEp56IIV" +
                "U7m8DWMv20TIT/5ZlzOjTXaY3gzhA0OUpa5eAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<KeyValue> CreateSerializer() => new Serializer();
        public Deserializer<KeyValue> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<KeyValue>
        {
            public override void RosSerialize(KeyValue msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(KeyValue msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(KeyValue msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(KeyValue msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(KeyValue msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<KeyValue>
        {
            public override void RosDeserialize(ref ReadBuffer b, out KeyValue msg) => msg = new KeyValue(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out KeyValue msg) => msg = new KeyValue(ref b);
        }
    }
}
