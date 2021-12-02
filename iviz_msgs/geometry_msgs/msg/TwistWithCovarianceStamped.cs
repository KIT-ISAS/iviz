/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwistWithCovarianceStamped : IDeserializable<TwistWithCovarianceStamped>, IMessage
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "twist")] public TwistWithCovariance Twist;
    
        /// Constructor for empty message.
        public TwistWithCovarianceStamped()
        {
            Twist = new TwistWithCovariance();
        }
        
        /// Explicit constructor.
        public TwistWithCovarianceStamped(in StdMsgs.Header Header, TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// Constructor with buffer.
        internal TwistWithCovarianceStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Twist = new TwistWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwistWithCovarianceStamped(ref b);
        
        TwistWithCovarianceStamped IDeserializable<TwistWithCovarianceStamped>.RosDeserialize(ref Buffer b) => new TwistWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            Twist.RosValidate();
        }
    
        public int RosMessageLength => 336 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/bMAy9+1cQyKHtkGRAO/QQYKcN23oYUKzFPjEUjM3YWm3Jk+Qm3q/fo+y4LdpD" +
                "D1sDA7Fl8pF8fKRndFmZQF5aL0FsDMSWJETTcJSC4taESFsTK5hsxIvNhXLnfGEsDGjjuRG4wNI0cOOm" +
                "XWYfhAvxVKW/7FIhvgDhjbthb1gREmyWvf7Hv+zjxfsVhVhcNaEML4c8shldRGTIvqBGIhccmTYO+Zmy" +
                "Er+o5UZqSqmj4PQ29q2EZTZyg6sUK57ruqcuKCsOHDRNZ02uJEyl7/3haSwxteyjybua/QPOFB1XkN9d" +
                "4vTs7Qo2NkjeRYOEeiDkXjgYW+IlZZ2x8eRYHbLZ5dYt8CglWJ6CU6w4arKy015qnhxWiPFiKG4JbJAj" +
                "iFIEOkxnV3gMR4QgSEFal1d0iMzP+1g5C0Ch1LJ1LQqcgwGgHqjTwdEdZE17RZat28MPiLcxngJrJ1yt" +
                "aVGhZ7VWH7oSBMKw9e7GFDBd9wkkrw0US7VZe/Z9pl5DyGz2LukyavtSR/DPIbjcJFWrnrMQvaKnblyZ" +
                "4n+psRQH1fl+kOQjw7CX2b5tgdB+ZBpVAkhPUFTLYDNNYQcPHxnd75fZMFv7aZrRJ7ddNPwL2p7mmaMB" +
                "5W6TCDvdnUJk0xRixL3ZpfhCzpvJHLoFKVF8UL1DyBuzk2LBu7ubIpmqjM+A7zFo8xTjji97Uf0d7ubU" +
                "z+nPnLwbA/DadZG+kiI+OP72+PH3dHyUbWrH8fTVj5PTn3eKecb2Pblha++uxeIQ+8Jgs0LNAiXrtmRb" +
                "prWgGwKb5rPk0fkTGk1un0e756lujJo98k1AifrufoFL3WBnaec4i43VCGMcUezkCcfCeLiqVFRm+Ig4" +
                "L3PQQYUDc9YpnQ1fA1KwAAje3LYAwxb2bEM9SCAxSIeyLJdz2lZgNVnpAKd1mxa0ycmb0mA/qycCQeCj" +
                "M9NYHES6OcYo1fWQ8xAMwgXIXnBHSzrbUO862mpBuPHjd8HRGimOeaX9FZ2bpyEZIO4Teu7Qe9ASApdY" +
                "dTZEfJEwtqOEaTfd9dPdn+wv5KrBh5MHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
