/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int32MultiArray : IDeserializable<Int32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public int[] Data; // array of data
    
        /// Constructor for empty message.
        public Int32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public Int32MultiArray(MultiArrayLayout Layout, int[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Int32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int32MultiArray(ref b);
        
        Int32MultiArray IDeserializable<Int32MultiArray>.RosDeserialize(ref Buffer b) => new Int32MultiArray(ref b);
    
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Int32MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1d99f79f8b325b44fee908053e9c945b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9NlmT5KGUt5CEw2EsLgw5GCaGo1nWsxLaCJDfrfv2O5M+0exwT" +
                "Btv385wjXQ3pe8bCMmVaH0k4cinTQ5k5tTFGvN2LN106ytlasWeSnKhCOaULSrSJhiR1XOZcOBFseESW" +
                "Ue7ThU+3syj6UIyy+l2tIdkTxypRcV0kISmcqKMiVbjVcrtrorGCt11DCp2atCiK1v94RQ+P3+7IOvmc" +
                "2739/J4PVPgBzTrSUCnOhGFLgvZcsFFx5Z1KBa0sSIqsQy1Q4CSMU3GJrIqdezvxjOhrE49ShkkbyYYl" +
                "JUbnhM5sKNfWA3CaVFHU/xeatyUgIdpDrk0rV+Oik9EnBgK2URn0DiiedZJY7u3TSUipij1xxn7PAcp5" +
                "LIXrxEf5OMZh0caSTXWZSdrc/9w8PdIL09ko57gAVAL23F6CsM4oyaggCtkcCZANPKeeVy82UcbzHBKe" +
                "TvgrNTlMjiNaBzDbPodPPvm5arFd7Mbq0rLcjQ+wHHfR0FMAFoAQRk5oNY1TAWkzurme/7r+MieV+0k4" +
                "K5eCCLBhfF6BM9aZNlQHW1Q5B/ag3XER9i40QOftfDfLxAvqAu4gZbVP3aBzWfWbofma0LFnDWhhXY2B" +
                "ZuzRrOl2ubiZz4muCu24jqzFJGXpUEK5UA5qB+yjuuCij+CspEsHnacFgEY96wUAvBe3y8a97JerdRh0" +
                "vrbgqmdrywVZPu6k4YRxknC8/bXkJTf6PKEDPqB3mReTcFqO/r/qOPuP89/OVuSJYDBq/hiV6guK79Ur" +
                "Tnx7cpv5qtXwl1+9Ne8C6cpPCa4BKnHh2lGbWEnmE6uvv/T4A6p4ENnUBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
