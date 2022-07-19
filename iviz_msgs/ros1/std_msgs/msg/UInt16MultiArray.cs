/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt16MultiArray : IDeserializableRos1<UInt16MultiArray>, IDeserializableRos2<UInt16MultiArray>, IMessageRos1, IMessageRos2
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public ushort[] Data;
    
        /// Constructor for empty message.
        public UInt16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<ushort>();
        }
        
        /// Explicit constructor.
        public UInt16MultiArray(MultiArrayLayout Layout, ushort[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt16MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        /// Constructor with buffer.
        public UInt16MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new UInt16MultiArray(ref b);
        
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 2 * Data.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Layout.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt16MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "52f264f1c973c4b73790d384c6cb4484";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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
    }
}
