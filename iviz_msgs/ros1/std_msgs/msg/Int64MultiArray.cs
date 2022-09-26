/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int64MultiArray : IHasSerializer<Int64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public long[] Data;
    
        public Int64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = EmptyArray<long>.Value;
        }
        
        public Int64MultiArray(MultiArrayLayout Layout, long[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public Int64MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                int n = b.DeserializeArrayLength();
                long[] array;
                if (n == 0) array = EmptyArray<long>.Value;
                else
                {
                     array = new long[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public Int64MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                long[] array;
                if (n == 0) array = EmptyArray<long>.Value;
                else
                {
                     array = new long[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public Int64MultiArray RosDeserialize(ref ReadBuffer b) => new Int64MultiArray(ref b);
        
        public Int64MultiArray RosDeserialize(ref ReadBuffer2 b) => new Int64MultiArray(ref b);
    
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 8 * Data.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Layout.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Data.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * Data.Length;
            return size;
        }
    
        public const string MessageType = "std_msgs/Int64MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "54865aa6c65be0448113a2afc6a49270";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
    
        public Serializer<Int64MultiArray> CreateSerializer() => new Serializer();
        public Deserializer<Int64MultiArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Int64MultiArray>
        {
            public override void RosSerialize(Int64MultiArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Int64MultiArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Int64MultiArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Int64MultiArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Int64MultiArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Int64MultiArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Int64MultiArray msg) => msg = new Int64MultiArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Int64MultiArray msg) => msg = new Int64MultiArray(ref b);
        }
    }
}
