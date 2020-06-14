using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int16MultiArray")]
    public sealed class Int16MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public short[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<short>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int16MultiArray(MultiArrayLayout Layout, short[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int16MultiArray(Buffer b)
        {
            Layout = new MultiArrayLayout(b);
            Data = b.DeserializeStructArray<short>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Int16MultiArray(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Layout.RosSerialize(b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException();
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 2 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int16MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d9338d7f523fcb692fae9d0a0e9f067c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJQrXOsRLaCJDfr/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6rlk4Jm3MnoQnnzM9VtqrpbXi7UG8mcpTwc6JLZPkTJXKK1NSZmwyJGnSquDSi2jD" +
                "K7SmIoSLEO4mSfIhGenmWz9DcgdOVabSJklGUnjR7EpU6Wc3qzV1T/RSFx4rncKSJLn/x0/y+PTtjpyX" +
                "m8Jt3ef3fKDCD2jWkYZKqRaWHQnacslWpbX3Sipo5UBS6A61QIKDsF6lFaJqdv7twBOir6f9SGWZjJVs" +
                "WVJmTUGozJYK4wIAb0iVZfN/pnmbAhKiPORatnKdXHSw5sBAwC6poPdiHlFsTJY57p3TQUipyi2x5nDm" +
                "LrQLsJS+Ex/p0xTNYqwjl5tKS1o+/Fw+P9EL09Eq77kEVAL2wp2DcN4qycggSnlqCZCNPK8Cr97eTNnA" +
                "c0h4O+Ev1Hg33l/SfQSz6nP4FII3dYnVbD1S55b5erSDZb9OhoECsACEsHJMi6s0F5BW08319Nf1lymp" +
                "IkzCUfkcRIAN4/MKnKnRxlKz2SHLMbIH7Y6LcHexACqvpuuJFi/IC7iDnNU294PO5dRvpuBCxZ41ooV1" +
                "MQKaUUBzT7fz2c10SnRRGs/NzkZMUo52FZSL6aB2xH7ZJJz1ERyV9Pmg87QAUKhnPQOA7+x2fnLP++ka" +
                "HQadr0246NnadFGWjydpOWN0Eto7XEtBcmuOY9phAb2rohzHbtmH/7ri5D/OfztbSSCCwWj4Y1TqFRTf" +
                "qld0fNu5p/lq1AiXX3M07zbSRZgSXANU4cJ1l21gLVkIrFd/qfEH3/BMx9QFAAA=";
                
    }
}
