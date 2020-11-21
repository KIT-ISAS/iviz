/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int64MultiArray")]
    public sealed class Int64MultiArray : IDeserializable<Int64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public long[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<long>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int64MultiArray(MultiArrayLayout Layout, long[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int64MultiArray(ref b);
        }
        
        Int64MultiArray IDeserializable<Int64MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int64MultiArray(ref b);
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
        [Preserve] public const string RosMessageType = "std_msgs/Int64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "54865aa6c65be0448113a2afc6a49270";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBuuSuo7rjxCagA+GQC8pFFIoxZiw0Y6stSWt2V3FTX99364+rCQ9" +
                "li4ykmd2Zt57O7MJfStYWKZC6wMJRy5n+loXTq2NES/34kXXjkq2VuyYJGeqUk7pijJt4ighqdO65MqJ" +
                "YMQjioJKHy98vJ3GURy9y0dF+25WQvbIqcpU2qbJSAon2l1xpCp3fbXZdtuxgrtfCYViXZwvGUerf7xA" +
                "4+HLLVknH0u7s5/ekvJqfId4Z/KQKy2EYUuCdlyxUWnjvZQKmllQFcUZOnAndBTGqbRGWMPRvRx5SnTX" +
                "BSCXYdJGsmFJmdEloTYbKrUNEJwmVVWt4Y36fRZoCQjQbd3r1rnoaPSRAYJtHNVQfrkISB51llkeHNlR" +
                "SKmqHXHBvgEAzHk8lRseAyqkKZpHG0s213UhaX3/Y/3zgZ6YTkY5xxXwEhiU9jUO64yS7FOISnYNAs6B" +
                "7qVnN9icKRPYJuR/5yO4UJP95PCBVgHRZkjkow9/bKps5tuxem1ZbMd7WA5bJAw8AAhAhJETWl6muYDI" +
                "BV1fzX5dfZ6RKv14nJTLwQb4MFPPwJrqQhtqN0PPhE5BA5A/ExL2tq2B8pvZdlqIJ6QG5lHOape70cBn" +
                "1W+G/CtC1aE5gIZ5OQaksYe0opvF/Ho2I7qotON2ZysrKUv7GhKGfNA9EPjQZZwPQZyUdPlo4OoxoNTQ" +
                "/AoD3vObRe9fDDO2gowGzj7ncmjsM7YCvT9Ywxmjt9D1/uLy+ht9mtAeHxC/LqtJaJ+D/99Unf7fy+Gu" +
                "69E48nQwMK0OGKHmC+rv1DPmoG/nfvBaVfwF2Z7Tm5104YcHdwTVuJctDrCLbKTzkc3X36r8AQjV88L9" +
                "BQAA";
                
    }
}
