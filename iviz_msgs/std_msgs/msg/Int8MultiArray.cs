/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Int8MultiArray")]
    public sealed class Int8MultiArray : IDeserializable<Int8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public sbyte[] Data; // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int8MultiArray(MultiArrayLayout Layout, sbyte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int8MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int8MultiArray(ref b);
        }
        
        Int8MultiArray IDeserializable<Int8MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int8MultiArray(ref b);
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int8MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7c1af35a1b4781bbe79e03dd94b7c13";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJTSFvIQKOylhUEHY4RQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "YbB9P885utKIvmcsLFOm9YGEI5cyPZWZU2tjxPujeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhkdk" +
                "GeU+Xfh0O42iT8Uoq9/VGpE9cqwSFddFEpLCiToqUoW73WybYL+Ct10jCp2atCgaRKt/vAbR0/O3e7JO" +
                "vuR2Z79+ZDSAED8gW8cbQsWZMGxJ0I4LNiquvFdSQS4LniLrgAsUOArjVFwiqyLo3o88JXpo4lHKMGkj" +
                "2bCkxOic0JoN5do65DtNqijq/zPZ2xKQEe2h2LpVrHHR0egjAwHbqITky0VA8aKTxHJvq45CSlXsiDP2" +
                "2w5QzmMpXKc/yscx5kUbSzbVZSZp/fhz/euZXplORjnHBaASsOf2HIR1RklGBVHIZipANvC88rx6sYky" +
                "nueI8HTCX6jJfnK4pFUAs+lz+OKTX6oWm/l2rM4ti+14D8thG408BWABCGHkhJZXcSogbUY317Pf17cz" +
                "Urk/DCflUhABNpygN+CMdaYN1cEWVU6BPWh3XIS9Dw3QeTPbTjPxirqAO0xZ7VI37FxW/WFoviJ07FkD" +
                "WliXY6AZezQrulvMb2YzootCO64jazFJWdqXUC6Ug9oB+2VdcN5HcFLSpcPO0wJAo571DADe87tF4170" +
                "y9U6DDtfW3DZs7Xlgiyfd9JwwpgkjLe/mbzkRp8mtMcH9C7zYhKm5eD/q47T/3oFPDQTOYg8F5yNWgKc" +
                "luoLou/UG4a+Hd7miNWC+Cuw3p0PgXThDwpuAipx7drLNrFSzSdWX59TB9FfaUVwadsFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
