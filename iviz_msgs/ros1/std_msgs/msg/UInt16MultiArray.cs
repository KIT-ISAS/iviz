/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt16MultiArray : IDeserializable<UInt16MultiArray>, IHasSerializer<UInt16MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public ushort[] Data;
    
        public UInt16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = EmptyArray<ushort>.Value;
        }
        
        public UInt16MultiArray(MultiArrayLayout Layout, ushort[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public UInt16MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ushort>.Value
                    : new ushort[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 2);
                }
                Data = array;
            }
        }
        
        public UInt16MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ushort>.Value
                    : new ushort[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 2);
                }
                Data = array;
            }
        }
        
        public UInt16MultiArray RosDeserialize(ref ReadBuffer b) => new UInt16MultiArray(ref b);
        
        public UInt16MultiArray RosDeserialize(ref ReadBuffer2 b) => new UInt16MultiArray(ref b);
    
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
                size += 2 * Data.Length;
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
            size += 2 * Data.Length;
            return size;
        }
    
        public const string MessageType = "std_msgs/UInt16MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "52f264f1c973c4b73790d384c6cb4484";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJQrXOsRLaCJDfr/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6rlk4Jm3MnoQnnzM9VtqrpbXi7UG8mcpTwc6JLZPkTJXKK1NSZmwyJGnSquDSi2jD" +
                "K7SmIoSLEO4mSfIhGenmWz9DcgdOVabSJklGUnjR7EoqVfrZzWpNvSf62/BY6RSWJMn9P36Sx6dvd+S8" +
                "3BRu6z6/5wMVfkCzjjRUSrWw7EjQlku2Kq29V1JBKweSQneoBRIchPUqrRBVc/NvB54QfT3tRyrLZKxk" +
                "y5IyawpCZbZUGBcAeEOqLJv/M83bFFAQ5SHXspXr5KKDNQcGAnZR7sU8otiYLHPcO6eDkFKVW2LN4cxd" +
                "aBdgKX0nPtKnKZrFWEcuN5WWtHz4uXx+ohemo1XecwmoBOyFOwfhvFWSkUGU8tQSIBt5XgVevb2ZsoHn" +
                "kPB2wl+o8W68v6T7CGbV5/ApBG/qEqvZeqTOLfP1aAfLfp0MAwVgAQhh5ZgWV2kuIK2mm+vpr+svU1JF" +
                "mISj8jmIABvG5xU4U6ONpWazQ5ZjZA/aHRfh7mIBVF5N1xMtXpAXcAc5q23uB53Lqd9MwYWKPWtEC+ti" +
                "BDSjgOaebuezm+mU6KI0npudjZikHO0qKBfTQe2I/bJJOOsjOCrp80HnaQGgUM96BgDf2e385J730zU6" +
                "DDpfm3DRs7XpoiwfT9JyxugktHe4loLk1hzHtMMCeldFOY7dsg//dcXJf5z/draSQASD0fDHqNQrKL5V" +
                "r+j4tnNP89WoES6/5mjebaSLMCW4BqjChesu28BashBYr/5S4w9Lviw21AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<UInt16MultiArray> CreateSerializer() => new Serializer();
        public Deserializer<UInt16MultiArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<UInt16MultiArray>
        {
            public override void RosSerialize(UInt16MultiArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(UInt16MultiArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(UInt16MultiArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(UInt16MultiArray msg) => msg.Ros2MessageLength;
            public override void RosValidate(UInt16MultiArray msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<UInt16MultiArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out UInt16MultiArray msg) => msg = new UInt16MultiArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out UInt16MultiArray msg) => msg = new UInt16MultiArray(ref b);
        }
    }
}
