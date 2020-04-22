
namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class KeyValue : IMessage
    {
        public string key; // what to label this value when viewing
        public string value; // a value to track over time
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "diagnostic_msgs/KeyValue";
    
        public IMessage Create() => new KeyValue();
    
        public int GetLength()
        {
            int size = 8;
            size += key.Length;
            size += value.Length;
            return size;
        }
    
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cf57fdc6617a881a88c16e768132149c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACi2LQQrAIBDE7kL/MODLtmXQRaugW6W/r2BvgSTdmpaAxBceM4rBKrKczLCoHUPywyVY" +
                "MJRzta7vZRsP+Wl91uRKqIMNpjfd4T7qJe2SXwAAAA==";
                
    }
}
