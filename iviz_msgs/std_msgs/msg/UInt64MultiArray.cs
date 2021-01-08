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
                "H4sIAAAAAAAACr1U32vbMBB+D+R/OOKXLkuz/ChlLeQhUNhLB4MOxgihqNY5VmJbQZKbdX/9Psmy47Z7" +
                "HDMOVu50d9/36U4JfStYWKZC6wMJRy5n+loXTq2NES/34kXXjkq2VuyYJGeqUk7pijJthoOEpE7rkisn" +
                "ghGvKAoqfbzw8XY6HAwH7/JREb/Nk5A9cqoylcY0GUnhRNw1HNSqctdXm227nxp39yQUirVxvuRwsPrH" +
                "D2g8fLkl6+RjaXf201tSXo3vEO9MHnKlhTBsSdCOKzYqbbyXUkEzC6qiOEMH7oSOwjiV1ghrOLqXI0+J" +
                "7toA5DJM2kg2LCkzuiTUZkOltgGC06SqKhreqN9lgZSAAN3WnW6ti45GHxkg2DbCLxcByaPOMsu9IzsK" +
                "KVW1Iy7YNwCAOY+ncv1jQIU0RfNoY8nmui4kre9/rH8+0BPTySjnuAJeAoPSvsZhnVGSfQpRybZBwDnQ" +
                "vfTsepszZQLbhPzvfAQXarKfHD7QKiDa9Il89OGPTZXNfDtWry2L7XgPy2GLhIEHAAGIMHJCy8s0FxC5" +
                "oOur2a+rzzNSpR+Pk3I52AAfZuoZWFNdaENxM/RM6BQ0APkzIWFvYw2U38y200I8ITUwj3JWu9yNej6r" +
                "fjPkXxGq9s0BNMzLMSCNPaQV3Szm17MZ0UWlHcedUVZSlvY1JAz5oHsg8KHNOO+DOCnp8lHP1WFAqb75" +
                "FQZ85zeLzr/oZ4yCjHrOLueyb+wyRoHeH6zhjNFb6Hp/cXn9jT5NaI8FxK/LahLa5+D/N1Wn//dyuGt7" +
                "dDjwdDAwUQeMULOC+jv1jDno2rkbvKiKvyDjOb3ZSRd+eHBHUI172eIA28hGOh/ZrP5W5Q9O0MKo/QUA" +
                "AA==";
                
    }
}
