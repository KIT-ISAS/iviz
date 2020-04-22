
namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt8MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public byte[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt8MultiArray";
    
        public IMessage Create() => new UInt8MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 1 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt8MultiArray()
        {
            layout = new MultiArrayLayout();
            data = System.Array.Empty<0>();
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
        public const string Md5Sum = "82373f1612381bb6ee473b5cd6f5d89c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJTSFvIQKOylhUEHY4RQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "YbB9P885utKIvmcsLFOm9YGEI5cyPZWZU2tjxPujeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhkdk" +
                "GeU+Xfh0O42iT8Uoq9/VGpE9cqwSFddFEpLCiToqKlXhbjfbJhoreNs1otCpSYuiQbT6x2sQPT1/uyfr" +
                "5Etud/brR0YDCPEDsnW8IVScCcOWBO24YKPiynslFeSy4CmyDrhAgaMwTsUlsiqC7v3IU6KHJh6lDJM2" +
                "kg1LSozOCa3ZUK6tQ77TpIqi/j+TvS0BFdEeiq1bxRoXHY0+MhCwDYovFwHFi04Sy72tOgopVbEjzthv" +
                "O0A5j6Vwnf4oH8eYF20s2VSXmaT148/1r2d6ZToZ5RwXgErAnttzENYZJRkVRCGbqQDZwPPK8+rFJsp4" +
                "niPC0wl/oSb7yeGSVgHMps/hi09+qVps5tuxOrcstuM9LIdtNPIUgAUghJETWl7FqYC0Gd1cz35f385I" +
                "5f4wnJRLQQTYcILegDPWmTZUB1tUOQX2oN1xEfY+NEDnzWw7zcQr6gLuMGW1S92wc1n1h6H5itCxZw1o" +
                "YV2OgWbs0azobjG/mc2ILgrtuI6sxSRlaV9CuVAOagfsl3XBeR/BSUmXDjtPCwCNetYzAHjP7xaNe9Ev" +
                "V+sw7HxtwWXP1pYLsnzeScMJY5Iw3v5m8pIbfZrQHh/Qu8yLSZiWg/+vOk7/6xXw0EzkIPJccDZqCXBa" +
                "qi+IvlNvGPp2eJsjVgvir8B6dz4E0oU/KLgJqMS1ay/bxEo1n1h9fU4dRH8BQ/XiLdsFAAA=";
                
    }
}
