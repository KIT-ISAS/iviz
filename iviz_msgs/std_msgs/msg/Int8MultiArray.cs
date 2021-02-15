/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Int8MultiArray")]
    public sealed class Int8MultiArray : IDeserializable<Int8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public sbyte[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int8MultiArray(MultiArrayLayout Layout, sbyte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int8MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int8MultiArray(ref b);
        }
        
        Int8MultiArray IDeserializable<Int8MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int8MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int8MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7c1af35a1b4781bbe79e03dd94b7c13";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
