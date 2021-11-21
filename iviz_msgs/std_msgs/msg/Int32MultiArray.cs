/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Int32MultiArray")]
    public sealed class Int32MultiArray : IDeserializable<Int32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public int[] Data; // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int32MultiArray(MultiArrayLayout Layout, int[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int32MultiArray(ref b);
        }
        
        Int32MultiArray IDeserializable<Int32MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int32MultiArray(ref b);
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1d99f79f8b325b44fee908053e9c945b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "YbB9P885utKIfmQsLFOm9YGEI5cyPZaZU2tjxPuDeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhkdk" +
                "GeU+Xfh0O42iT8Uoq9/VGpE9cqwSFddFEpLCiToqUoVbLjbbJhoreNs1otCpSYuiQbT6x2sQPT59vyPr" +
                "5Etud/brR0YDCPETsnW8IVScCcOWBO24YKPiynslFeSy4CmyDrhAgaMwTsUlsiqC7v3IU6L7Jh6lDJM2" +
                "kg1LSozOCa3ZUK6tQ77TpIqi/j+TvS0BFdEeiq1bxRoXHY0+MhCwjcogeUDxopPEcm+rjkJKVeyIM/bb" +
                "DlDOYylcpz/KxzHmRRtLNtVlJmn98Lz+9USvTCejnOMCUAnYc3sOwjqjJKOCKGQzFSAbeF55Xr3YRBnP" +
                "c0R4OuEv1GQ/OVzSKoDZ9Dl88ckvVYvNfDtW55bFdryH5bCNRp4CsACEMHJCy6s4FZA2o5vr2e/rbzNS" +
                "uT8MJ+VSEAE2nKA34Ix1pg3VwRZVToE9aHdchL0LDdB5M9tOM/GKuoA7TFntUjfsXFb9YWi+InTsWQNa" +
                "WJdjoBl7NCu6XcxvZjOii0I7riNrMUlZ2pdQLpSD2gH7ZV1w3kdwUtKlw87TAkCjnvUMAN7z20XjXvTL" +
                "1ToMO19bcNmzteWCLJ930nDCmCSMt7+ZvORGnya0xwf0LvNiEqbl4P+rjtP/egXcNxM5iDwXnI1aApyW" +
                "6gui79Qbhr4d3uaI1YL4K7DenQ+BdOEPCm4CKnHt2ss2sVLNJ1Zfn1MH0V+gNst62wUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
