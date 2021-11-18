/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt8MultiArray")]
    public sealed class UInt8MultiArray : IDeserializable<UInt8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public byte[] Data; // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public UInt8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt8MultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal UInt8MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt8MultiArray(ref b);
        }
        
        UInt8MultiArray IDeserializable<UInt8MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new UInt8MultiArray(ref b);
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
        [Preserve] public const string RosMessageType = "std_msgs/UInt8MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "82373f1612381bb6ee473b5cd6f5d89c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
