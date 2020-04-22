
namespace Iviz.Msgs.std_msgs
{
    public sealed class Int32MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public int[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int32MultiArray";
    
        public IMessage Create() => new Int32MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 4 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Int32MultiArray()
        {
            layout = new MultiArrayLayout();
            data = new int[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            layout.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            layout.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1d99f79f8b325b44fee908053e9c945b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
