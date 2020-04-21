
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
            data = new byte[0];
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
            "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5UUpb6ENgsJcWBh2MEUJQrXOsRLaCJDfr/vp9kh3bafc4" +
            "ZgyW73R33/fpTkP6plk4Jm3MnoQnnzM9VdqrpbXi7VG8mcpTwc6JLZPkTJXKK1NSZmwyJGnSquDSi2jD" +
            "K7SmIoSLEO4mSfIhGenmWz9DcgdOVabSJklGUnjR7EoqVfrb1Zq6J3qpC4+VTmFJkjz84yd5ev56T87L" +
            "TeG27vN7PlDhOzTrSEOlVAvLjgRtuWSr0tp7JRW0ciApdIdaIMFBWK/SClE1O/924AnRl9N+pLJMxkq2" +
            "LCmzpiBUZkuFcQGAN6TKsvk/07xNAQlRHnItW7lOLjpYc2AgYBflXswjio3JMse9czoIKVW5JdYcztyF" +
            "dgGW0nfiI32aolmMdeRyU2lJy8cfy5/P9MJ0tMp7LgGVgL1w5yCct0oyMohSnloCZCPPq8CrtzdTNvAc" +
            "Et5O+As13o33l/QQwaz6HD6F4E1dYjVbj9S5Zb4e7WDZr5NhoAAsACGsHNPiKs0FpNV0cz39dX07JVWE" +
            "STgqn4MIsGF8XoEzNdpYajY7ZDlG9qDdcRHuPhZA5dV0PdHiBXkBd5Cz2uZ+0Lmc+s0UXKjYs0a0sC5G" +
            "QDMKaB7obj67mU6JLkrjudnZiEnK0a6CcjEd1I7YL5uEsz6Co5I+H3SeFgAK9axnAPCd3c1P7nk/XaPD" +
            "oPO1CRc9W5suyvLxJC1njE5Ce4drKUhuzXFMOyygd1WU49gt+/BfV5z8x/lvZysJRDAYDX+MSr2C4lv1" +
            "io5vO/c0X40a4fJrjubdRroIU4JrgCpcuO6yDawlC4H16i81/gAxI/UV1AUAAA==";

    }
}
