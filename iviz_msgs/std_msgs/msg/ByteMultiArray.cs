/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ByteMultiArray : IDeserializable<ByteMultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public byte[] Data; // array of data
    
        /// Constructor for empty message.
        public ByteMultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<byte>();
        }
        
        /// Explicit constructor.
        public ByteMultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal ByteMultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ByteMultiArray(ref b);
        
        ByteMultiArray IDeserializable<ByteMultiArray>.RosDeserialize(ref Buffer b) => new ByteMultiArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/ByteMultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "70ea476cbcfd65ac2f68f3cda1e891fe";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9tlmb5KGUt5CEw2EsLgw5GCSGo1nWsxLaCJDfLfv2O5M+0exwT" +
                "Btv385wjXQ3pe8bCMmVaH0g4cinTU5k5tTJGnB/FWZeOcrZW7JgkJ6pQTumCEm2iIUkdlzkXTgQbHpFl" +
                "lPt04dPtJIo+FKOsfldrSPbIsUpUXBdJSAon6qjo9ex4vWmC/Qredg0pdGrSoiha/uMVPT1/eyDr5Da3" +
                "O/v5PR+o8AOadaShUpwJw5YE7bhgo+LKeyMVtLIgKbIOtUCBozBOxSWyKnbufOQJ0dcmHqUMkzaSDUtK" +
                "jM4JndlQrq0H4DSpoqj/LzRvS0BDtIdcq1auxkVHo48MBGyjUhVuMQ8otjpJLPf26SikVMWOOGO/5wDl" +
                "PJbCdeKjfBzjsGhjyaa6zCStHn+uXp7plelklHNcACoBe24vQVhnlGRUEIVsjgTIBp43nlcvNlHG8xwS" +
                "nk74KzXejw/XtAxg1n0On3zytmqxnm1G6tIy34z2sBw20dBTABaAEEaOaXETpwLSZnR3O/11+2VKKveT" +
                "cFIuBRFgw/i8AWesM22oDraocgrsQbvjIuxDaIDO6+lmkolX1AXcQcpql7pB57LqN0PzJaFjzxrQwroY" +
                "Ac3Io1nS/Xx2N50SXRXacR1Zi0nK0r6EcqEc1A7Yr+uCsz6Ck5IuHXSeFgAa9awXAPCe3c8b97xfrtZh" +
                "0PnagouerS0XZPm4k4YTxknC8fbXkpfc6NOY9viA3mVejMNpOfj/quPkP85/O1uRJ4LBqPljVKovKL5T" +
                "bzjx7clt5qtWw19+9da8C6QrPyW4BqjEhWuv28RKMp9Yff2lxx/ycEl+1AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
