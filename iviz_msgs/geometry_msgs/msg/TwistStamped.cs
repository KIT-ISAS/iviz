
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TwistStamped : IMessage
    {
        // A twist with reference coordinate frame and timestamp
        public std_msgs.Header header;
        public Twist twist;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TwistStamped";
    
        public IMessage Create() => new TwistStamped();
    
        public int GetLength()
        {
            int size = 48;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TwistStamped()
        {
            header = new std_msgs.Header();
            twist = new Twist();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            twist.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            twist.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "98d34b0043a2093cf9d9345ab6eef12e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9L/g/DPiQpNgqJKUHQw+F0jaHQiCh1zCWxtISaVfdHcVRf33frmQloZce" +
                "2hqB9THvzbyZN7umj6RHG5WOVhsKcpAgrhQqvQ+VdaxCh8CdELuK1HYSlbvefBWuJFCT/8xdZsg8ZmU+" +
                "/OXfyny7/bKjqNV9F+v4dsq9Mmu6VVTFoaJOlCtWpoNHUbZuJGxbeZSWcrlSUf6qYy+xAPCusZFw1eIk" +
                "cNuONEQEqYfurhucLZPwRe4JD6R1xNRzUFsOLYff+pTYcUX5MeQ+Xn/aIcZFKQe1KGgEQxmEo3U1PpIZ" +
                "rNOrywQw67uj3+JRarR2SU7asKZi5akPElOdHHfI8WYSV4Ab3RFkqSKd53f3eIwXhCQoQXpfNnSOym9G" +
                "bbwDodAjB8v7VhJxiQ6A9SyBzi5eMKeyd+TY+RP9xPic409o3cKbNG0bzKxN6uNQo4EI7IN/tBVC92Mm" +
                "KVsrTqm1+8BhNAk1pTTrz9mLmsaXJ4J/jtGXFgOosodN1JDY8zTubfXvDFmLh+/COLky78Dq5K3TrCJh" +
                "5ihP09xRk0BJz2jhPvgHcXgJz1mNEOsE3Uhbxq7O1koug1u/S6k+XNEc8vw8x/0vgXPeRWKQJBGDwgig" +
                "Mn18rbFIi3CdresdjN8JY6rQuyABrGwA1HpXgBVnDxZYNugIVR7Nc17B0fEDKAU+IqC570GGZQ7sYssJ" +
                "m14Dci5FXWzo2KCxOSr5IG9t3nNbUrC1xZonJBJ1C5hpVrchPVzCR2071TwlgylBErxmwEVB1wca/UDH" +
                "JAg3YT5ePO1R4lxXXgP1fpPOlpnidUdvPMaPtsTINTbGRcXJVhhzaD3r+3f0tNyNy93PlfkFmHf9aLQF" +
                "AAA=";
                
    }
}
