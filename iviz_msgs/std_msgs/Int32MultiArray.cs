
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
            "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJQrXOsRLaCJDfr/vp9kh3bafc4" +
            "ZgyW73R33/fpTkP6rlk4Jm3MnoQnnzM9VtqrpbXi7UG8mcpTwc6JLZPkTJXKK1NSZmwyJGnSquDSi2jD" +
            "K7SmIoSLEO4mSfIhGenmWz9DcgdOVabSJklGUnjR7EpU6Rfz1Zq6J3qpC4+VTmFJktz/4yd5fPp2R87L" +
            "TeG27vN7PlDhBzTrSEOlVAvLjgRtuWSr0tp7JRW0ciApdIdaIMFBWK/SClE1O/924AnR19N+pLJMxkq2" +
            "LCmzpiBUZkuFcQGAN6TKsvk/07xNAQlRHnItW7lOLjpYc2AgYJdUUe+IYmOyzHHvnA5CSlVuiTWHM3eh" +
            "XYCl9J34SJ+maBZjHbncVFrS8uHn8vmJXpiOVnnPJaASsBfuHITzVklGBlHKU0uAbOR5FXj19mbKBp5D" +
            "wtsJf6HGu/H+ku4jmFWfw6cQvKlLrGbrkTq3zNejHSz7dTIMFIAFIISVY1pcpbmAtJpurqe/rr9MSRVh" +
            "Eo7K5yACbBifV+BMjTaWms0OWY6RPWh3XIS7iwVQeTVdT7R4QV7AHeSstrkfdC6nfjMFFyr2rBEtrIsR" +
            "0IwCmnu6nc9uplOii9J4bnY2YpJytKugXEwHtSP2yybhrI/gqKTPB52nBYBCPesZAHxnt/OTe95P1+gw" +
            "6HxtwkXP1qaLsnw8ScsZo5PQ3uFaCpJbcxzTDgvoXRXlOHbLPvzXFSf/cf7b2UoCEQxGwx+jUq+g+Fa9" +
            "ouPbzj3NV6NGuPyao3m3kS7ClOAaoAoXrrtsA2vJQmC9+kuNP6p4ENnUBQAA";

    }
}
