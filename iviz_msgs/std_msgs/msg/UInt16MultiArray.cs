/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt16MultiArray")]
    public sealed class UInt16MultiArray : IDeserializable<UInt16MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public ushort[] Data; // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public UInt16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<ushort>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt16MultiArray(MultiArrayLayout Layout, ushort[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt16MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt16MultiArray(ref b);
        }
        
        UInt16MultiArray IDeserializable<UInt16MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new UInt16MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void Dispose()
        {
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
                size += 2 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt16MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "52f264f1c973c4b73790d384c6cb4484";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
