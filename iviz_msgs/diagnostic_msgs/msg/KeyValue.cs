using System.Runtime.Serialization;

namespace Iviz.Msgs.diagnostic_msgs
{
    [DataContract]
    public sealed class KeyValue : IMessage
    {
        [DataMember] public string key { get; set; } // what to label this value when viewing
        [DataMember] public string value { get; set; } // a value to track over time
    
        /// <summary> Constructor for empty message. </summary>
        public KeyValue()
        {
            key = "";
            value = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public KeyValue(string key, string value)
        {
            this.key = key ?? throw new System.ArgumentNullException(nameof(key));
            this.value = value ?? throw new System.ArgumentNullException(nameof(value));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal KeyValue(Buffer b)
        {
            this.key = b.DeserializeString();
            this.value = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new KeyValue(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.key);
            b.Serialize(this.value);
        }
        
        public void Validate()
        {
            if (key is null) throw new System.NullReferenceException();
            if (value is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(key);
                size += BuiltIns.UTF8.GetByteCount(value);
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "diagnostic_msgs/KeyValue";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cf57fdc6617a881a88c16e768132149c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEy2LQQrAIBDE7r5iwJdty1AXrYJulf6+C/YWSDKsa72Q+SJiJTFYQ5GDBZZ0YEp56IIV" +
                "U7m8DWMv20TIT/5ZlzOjTXaY3gzhA0OUpa5eAAAA";
                
    }
}
