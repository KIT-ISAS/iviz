/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/TwistStamped")]
    public sealed class TwistStamped : IDeserializable<TwistStamped>, IMessage
    {
        // A twist with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "twist")] public Twist Twist { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwistStamped()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwistStamped(in StdMsgs.Header Header, in Twist Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwistStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Twist = new Twist(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwistStamped(ref b);
        }
        
        TwistStamped IDeserializable<TwistStamped>.RosDeserialize(ref Buffer b)
        {
            return new TwistStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "98d34b0043a2093cf9d9345ab6eef12e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
