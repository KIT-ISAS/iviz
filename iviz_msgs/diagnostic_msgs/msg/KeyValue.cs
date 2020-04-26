using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class KeyValue : IMessage
    {
        public string key; // what to label this value when viewing
        public string value; // a value to track over time
    
        /// <summary> Constructor for empty message. </summary>
        public KeyValue()
        {
            key = "";
            value = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out key, ref ptr, end);
            BuiltIns.Deserialize(out value, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(key, ref ptr, end);
            BuiltIns.Serialize(value, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Encoding.UTF8.GetByteCount(key);
                size += Encoding.UTF8.GetByteCount(value);
                return size;
            }
        }
    
        public IMessage Create() => new KeyValue();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "diagnostic_msgs/KeyValue";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "cf57fdc6617a881a88c16e768132149c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEy2LQQrAIBDE7r5iwJdty1AXrYJulf6+C/YWSDKsa72Q+SJiJTFYQ5GDBZZ0YEp56IIV" +
                "U7m8DWMv20TIT/5ZlzOjTXaY3gzhA0OUpa5eAAAA";
                
    }
}
