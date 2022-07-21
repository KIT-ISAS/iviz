/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int16MultiArray : IDeserializableCommon<Int16MultiArray>, IMessageCommon
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public short[] Data;
    
        public Int16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<short>();
        }
        
        public Int16MultiArray(MultiArrayLayout Layout, short[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public Int16MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public Int16MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Int16MultiArray(ref b);
        
        public Int16MultiArray RosDeserialize(ref ReadBuffer b) => new Int16MultiArray(ref b);
        
        public Int16MultiArray RosDeserialize(ref ReadBuffer2 b) => new Int16MultiArray(ref b);
    
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 2 * Data.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Layout.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        public const string MessageType = "std_msgs/Int16MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d9338d7f523fcb692fae9d0a0e9f067c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
