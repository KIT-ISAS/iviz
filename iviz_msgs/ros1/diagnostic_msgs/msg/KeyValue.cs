/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class KeyValue : IDeserializableRos1<KeyValue>, IDeserializableRos2<KeyValue>, IMessageRos1, IMessageRos2
    {
        /// <summary> What to label this value when viewing </summary>
        [DataMember (Name = "key")] public string Key;
        /// <summary> A value to track over time </summary>
        [DataMember (Name = "value")] public string Value;
    
        /// Constructor for empty message.
        public KeyValue()
        {
            Key = "";
            Value = "";
        }
        
        /// Explicit constructor.
        public KeyValue(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        public KeyValue(ref ReadBuffer b)
        {
            b.DeserializeString(out Key);
            b.DeserializeString(out Value);
        }
        
        /// Constructor with buffer.
        public KeyValue(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Key);
            b.DeserializeString(out Value);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new KeyValue(ref b);
        
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
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(Key) + WriteBuffer.GetStringSize(Value);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Key);
            WriteBuffer2.AddLength(ref c, Value);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "diagnostic_msgs/KeyValue";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cf57fdc6617a881a88c16e768132149c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEy2LQQrAIBDE7r5iwJdty1AXrYJulf6+C/YWSDKsa72Q+SJiJTFYQ5GDBZZ0YEp56IIV" +
                "U7m8DWMv20TIT/5ZlzOjTXaY3gzhA0OUpa5eAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
