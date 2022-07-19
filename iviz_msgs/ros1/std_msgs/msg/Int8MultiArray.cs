/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int8MultiArray : IDeserializableRos1<Int8MultiArray>, IDeserializableRos2<Int8MultiArray>, IMessageRos1, IMessageRos2
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public sbyte[] Data;
    
        /// Constructor for empty message.
        public Int8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<sbyte>();
        }
        
        /// Explicit constructor.
        public Int8MultiArray(MultiArrayLayout Layout, sbyte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int8MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        /// Constructor with buffer.
        public Int8MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Int8MultiArray(ref b);
        
        public Int8MultiArray RosDeserialize(ref ReadBuffer b) => new Int8MultiArray(ref b);
        
        public Int8MultiArray RosDeserialize(ref ReadBuffer2 b) => new Int8MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) BuiltIns.ThrowNullReference();
            Layout.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + Data.Length;
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Layout.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int8MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d7c1af35a1b4781bbe79e03dd94b7c13";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
