/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int64MultiArray : IDeserializable<Int64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// specification of data layout
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// array of data
        [DataMember (Name = "data")] public long[] Data;
    
        /// Constructor for empty message.
        public Int64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<long>();
        }
        
        /// Explicit constructor.
        public Int64MultiArray(MultiArrayLayout Layout, long[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int64MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<long>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Int64MultiArray(ref b);
        
        public Int64MultiArray RosDeserialize(ref ReadBuffer b) => new Int64MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 8 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Int64MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "54865aa6c65be0448113a2afc6a49270";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJQrXOsxJaCJDfr/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6XrBwTIUxexKefM70WBVeLa0Vbw/izVSeSnZObJkkZ0orr4ymzNhkSNKkVcnai2jD" +
                "K4qCyhAuQribJMmHZFQ03/oZkjtwqjKVNkkyksKLZleitL+5Xq2pe6KXuvBY6RSWJMn9P36Sx6dvd+S8" +
                "3JRu6z6/5wMVfkCzjjRUSgth2ZGgLWu2Kq29V1JBKweSouhQCyQ4COtVWiGqZuffDjwh+nraj1SWyVjJ" +
                "liVl1pSEymypNC4A8IaU1s3/meZtCkiI8pBr2cp1ctHBmgMDAbukgt6LeUSxMVnmuHdOByGl0lvigsOZ" +
                "u9AuwKJ9Jz7SpymaxVhHLjdVIWn58HP5/EQvTEervGcNqATspTsH4bxVkpFBaHlqCZCNPK8Cr97eTNnA" +
                "c0h4O+Ev1Hg33l/SfQSz6nP4FII3dYnVbD1S55b5erSDZb9OhoECsACEsHJMi6s0F5C2oJvr6a/rL1NS" +
                "ZZiEo/I5iAAbxucVOFNTGEvNZocsx8getDsuwt3FAqi8mq4nhXhBXsAd5Ky2uR90Lqd+MwUXKvasES2s" +
                "ixHQjAKae7qdz26mU6ILbTw3OxsxSTnaVVAupoPaEftlk3DWR3BU0ueDztMCQKGe9QwAvrPb+ck976dr" +
                "dBh0vjbhomdr00VZPp6k5YzRSWjvcC0Fya05jmmHBfSuSj2O3bIP/3XFyX+c/3a2kkAEg9Hwx6jUKyi+" +
                "Va/o+LZzT/PVqBEuv+Zo3m2kizAluAaowoXrLtvAWrIQWK/+UuMP7mz+Q9QFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
