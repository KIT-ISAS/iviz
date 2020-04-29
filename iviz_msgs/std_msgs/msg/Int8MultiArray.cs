using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Int8MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public sbyte[] data; // array of data
        
    
        /// <summary> Constructor for empty message. </summary>
        public Int8MultiArray()
        {
            layout = new MultiArrayLayout();
            data = System.Array.Empty<sbyte>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            layout.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            layout.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += layout.RosMessageLength;
                size += 1 * data.Length;
                return size;
            }
        }
    
        public IMessage Create() => new Int8MultiArray();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "std_msgs/Int8MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "d7c1af35a1b4781bbe79e03dd94b7c13";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5UUpb6ENgsJcWBh2MEUJQrXOsRLaCJDfr/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6plk4Jm3MnoQnnzM9VdqrpbXi7VG8mcpTwc6JLZPkTJXKK1NSZmwyJGnSquDSi2jD" +
                "K7SmIoSLEO4mSfIhGenmWz9DcgdOVabSJklGUnjR7EpU6W9Xa+o90UtdeKx0CkuS5OEfP8nT89d7cl5u" +
                "Crd1n9/zgQrfoVlHGiqlWlh2JGjLJVuV1t4rqaCVA0mhO9QCCQ7CepVWiKrZ+bcDT4i+nPYjlWUyVrJl" +
                "SZk1BaEyWyqMCwC8IVWWzf+Z5m0KaIjykGvZynVy0cGaAwMBu6SC3ot5RLExWea4d04HIaUqt8Saw5m7" +
                "0C7AUvpOfKRPUzSLsY5cbiotafn4Y/nzmV6YjlZ5zyWgErAX7hyE81ZJRgZRylNLgGzkeRV49fZmygae" +
                "Q8LbCX+hxrvx/pIeIphVn8OnELypS6xm65E6t8zXox0s+3UyDBSABSCElWNaXKW5gLSabq6nv65vp6SK" +
                "MAlH5XMQATaMzytwpkYbS81mhyzHyB60Oy7C3ccCqLyaridavCAv4A5yVtvcDzqXU7+ZggsVe9aIFtbF" +
                "CGhGAc0D3c1nN9Mp0UVpPDc7GzFJOdpVUC6mg9oR+2WTcNZHcFTS54PO0wJAoZ71DAC+s7v5yT3vp2t0" +
                "GHS+NuGiZ2vTRVk+nqTljNFJaO9wLQXJrTmOaYcF9K6Kchy7ZR/+64qT/zj/7WwlgQgGo+GPUalXUHyr" +
                "XtHxbeee5qtRI1x+zdG820gXYUpwDVCFC9ddtoG1ZCGwXv2lxh+TsSw71AUAAA==";
                
    }
}
