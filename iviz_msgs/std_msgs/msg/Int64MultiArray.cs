/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int64MultiArray")]
    public sealed class Int64MultiArray : IDeserializable<Int64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public long[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<long>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int64MultiArray(MultiArrayLayout Layout, long[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int64MultiArray(ref b);
        }
        
        Int64MultiArray IDeserializable<Int64MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int64MultiArray(ref b);
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
                size += 8 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "54865aa6c65be0448113a2afc6a49270";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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
                
    }
}
