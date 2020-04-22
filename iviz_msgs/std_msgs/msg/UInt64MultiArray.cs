
namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt64MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public ulong[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt64MultiArray";
    
        public IMessage Create() => new UInt64MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 8 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt64MultiArray()
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
        public const string Md5Sum = "6088f127afb1d6c72927aa1247e945af";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "Y7B8P885utKIfmQsLFOm9YGEI5cyPZaZU2tjxPuDeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhldk" +
                "GeU+Xfh0O42iT8Uoq7/VMyJ75FglKq6LJCSFE3VUVKrC3Vxvtk04Vd72GVHo1KRF0SBa/eNnED0+fb8j" +
                "6+RLbnf260dGAwjxE7J1vCFUnAnDlgTtuGCj4sp7JRXksuApsg64QIGjME7FJbIqgu79yFOi+yYepQyT" +
                "NpINS0qMzgmt2VCurUO+06SKov4/k70tARHRHoqtW8UaFx2NPjIQsA2KLxcBxYtOEsu9rToKKVWxI87Y" +
                "bztAOY+lcJ3+KB/HmBdtLNlUl5mk9cPz+tcTvTKdjHKOC0AlYM/tOQjrjJKMCqKQzVSAbOB55Xn1YhNl" +
                "PM8R4e2Ev1CT/eRwSasAZtPn8MUnv1QtNvPtWJ1bFtvxHpbDNhp5CsACEMLICS2v4lRA2oxurme/r7/N" +
                "SOX+MJyUS0EE2HCC3oAz1pk2VAdbVDkF9qDdcRH2LjRA581sO83EK+oC7jBltUvdsHNZ9Yeh+YrQsWcN" +
                "aGFdjoFm7NGs6HYxv5nNiC4K7biOrMUkZWlfQrlQDmoH7Jd1wXkfwUlJlw47TwsAjXrWMwD4zm8XjXvR" +
                "L1frMOx8bcFlz9aWC7J83knDCWOSMN7+ZvKSG32a0B4L6F3mxSRMy8H/Vx2n//UKuG8mchB5LjgbtQQ4" +
                "LdUKou/UG4a+Hd7miNWC+Cuw3p0PgXThDwpuAipx7drLNrFSzSdWq8+pg+gvThiEPdsFAAA=";
                
    }
}
