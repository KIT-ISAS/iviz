/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class KeyValue : IDeserializable<KeyValue>, IMessage
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
            b.DeserializeString(out Key);
            b.DeserializeString(out Value);
        }
        
        public KeyValue(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Key);
            b.Align4();
            b.DeserializeString(out Value);
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
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(Key) + WriteBuffer.GetStringSize(Value);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Key);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Value);
            return c;
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
    }
}
