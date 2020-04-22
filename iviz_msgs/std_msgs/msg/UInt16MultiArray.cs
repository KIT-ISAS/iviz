
namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt16MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public ushort[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt16MultiArray";
    
        public IMessage Create() => new UInt16MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 2 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt16MultiArray()
        {
            layout = new MultiArrayLayout();
            data = new ushort[0];
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
        public const string Md5Sum = "52f264f1c973c4b73790d384c6cb4484";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
