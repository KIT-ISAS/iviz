/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt64MultiArray")]
    public sealed class UInt64MultiArray : IDeserializable<UInt64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public ulong[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public UInt64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<ulong>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt64MultiArray(MultiArrayLayout Layout, ulong[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<ulong>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt64MultiArray(ref b);
        }
        
        UInt64MultiArray IDeserializable<UInt64MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new UInt64MultiArray(ref b);
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
                size += 8 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6088f127afb1d6c72927aa1247e945af";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
