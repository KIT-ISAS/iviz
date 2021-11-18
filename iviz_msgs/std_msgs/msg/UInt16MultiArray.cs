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
        internal UInt16MultiArray(ref Buffer b)
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
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 2 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt16MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "52f264f1c973c4b73790d384c6cb4484";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "Y7B8P885utKIfmQsLFOm9YGEI5cyPZaZU2tjxPuDeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhldk" +
                "GeU+Xfh0O42iT8Uoq7/VMyJ75FglKq6LJCSFE3VUVKrCzW822ybcP8HfpodOTVoUDaLVP34G0ePT9zuy" +
                "Tr7kdme/fmQ0gBA/IVvHG0LFmTBsSdCOCzYqrrxXUkEuC54i64ALFDgK41RcIqui596PPCW6b+JRyjBp" +
                "I9mwpMTonNCaDeXaOuQ7Taoo6v8z2dsSEBHtodi6Vaxx0dHoIwMB26D4chFQvOgksdzbqqOQUhU74oz9" +
                "tgOU81gK1+mP8nGMedHGkk11mUlaPzyvfz3RK9PJKOe4AFQC9tyeg7DOKMmoIArZTAXIBp5XnlcvNlHG" +
                "8xwR3k74CzXZTw6XtApgNn0OX3zyS9ViM9+O1bllsR3vYTlso5GnACwAIYyc0PIqTgWkzejmevb7+tuM" +
                "VO4Pw0m5FESADSfoDThjnWlDdbBFlVNgD9odF2HvQgN03sy200y8oi7gDlNWu9QNO5dVfxiarwgde9aA" +
                "FtblGGjGHs2Kbhfzm9mM6KLQjuvIWkxSlvYllAvloHbAflkXnPcRnJR06bDztADQqGc9A4Dv/HbRuBf9" +
                "crUOw87XFlz2bG25IMvnnTScMCYJ4+1vJi+50acJ7bGA3mVeTMK0HPx/1XH6X6+A+2YiB5HngrNRS4DT" +
                "Uq0g+k69Yejb4W2OWC2IvwLr3fkQSBf+oOAmoBLXrr1sEyvVfGK1+pw6iP4CQ2Jg8tsFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
