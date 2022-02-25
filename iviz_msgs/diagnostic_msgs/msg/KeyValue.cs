/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class KeyValue : IDeserializable<KeyValue>, IMessage
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
            Key = b.DeserializeString();
            Value = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new KeyValue(ref b);
        
        public KeyValue RosDeserialize(ref ReadBuffer b) => new KeyValue(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Key);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Key is null) BuiltIns.ThrowNullReference(nameof(Key));
            if (Value is null) BuiltIns.ThrowNullReference(nameof(Value));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Key) + BuiltIns.GetStringSize(Value);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "diagnostic_msgs/KeyValue";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cf57fdc6617a881a88c16e768132149c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEy2LQQrAIBDE7r5iwJdty1AXrYJulf6+C/YWSDKsa72Q+SJiJTFYQ5GDBZZ0YEp56IIV" +
                "U7m8DWMv20TIT/5ZlzOjTXaY3gzhA0OUpa5eAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
