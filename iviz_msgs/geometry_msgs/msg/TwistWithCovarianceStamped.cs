
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TwistWithCovarianceStamped : IMessage
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        public std_msgs.Header header;
        public TwistWithCovariance twist;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        public IMessage Create() => new TwistWithCovarianceStamped();
    
        public int GetLength()
        {
            int size = 336;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovarianceStamped()
        {
            header = new std_msgs.Header();
            twist = new TwistWithCovariance();
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
        public const string Md5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1VTWvbQBC9C/wfBnyIU2wXkuKDoaeWtj4UQhP6SQljaSRtI+2qu6vY6q/v25WsOCSH" +
                "HNoYgaXVzJuZN29GU7oqlSMrjRUn2jtiTeK8qtlLRn6nnKed8iVMcrGiU6HUGJspDQPKLdcCF1iqGm5c" +
                "N8vkg3Amlsr4l1wFiC9AeGNu2SoOCBE2mSSv//Fvkny8fL8m57Pr2hXuZZ/JJJnSpUeSbDOqxXPGnik3" +
                "SFEVpdhFJbdSUcweNce3vmvELZOBHlyFaLFcVR21LhBjQENdt1qlgYex+oM/PJUmpoatV2lbsX1AW0DH" +
                "5eR3G2ndvF3DRjtJW6+QUAeE1Ao7pQu8pKRV2p+fBYdkerUzCzxKAaLH4ORL9iFZ2Yd2hjzZrRHjRV/c" +
                "EthgRxAlczSLZ9d4dKeEIEhBGpOWNEPmF50vjQagUOzatpIAnIIBoJ4Ep5PTI+SQ9po0a3OA7xHvYjwF" +
                "Vo+4oaZFiZ5VoXrXFiAQho01tyqD6baLIGmlIFqq1Nay7ZLg1YdMpu+iNH1oX+wI/tk5k6oo7CDpxHkb" +
                "0GM3rlX2/wRZiIHubNer8pGJmByUduicIygAyfqgAmQoqKthEBpnsYWL9QwBdMukn7BhpoDzyewWNf+C" +
                "vMepZq/AuskjZ6v9CjobZxGDbtU+xhcyVo3mkC548WJdkDy0nKu9ZAveH++LaBqUvAG+xbDNY4wjX7YS" +
                "JDjbz6mb0585WTME4K1pPX2lgPjg+Nvjx9/j8WmSV4b96tWP89XPo2KetYNP79nWmhvROMTWUFix0LRA" +
                "z2Ftsi7icgh7Avvms6Te2HMaTO6eB7vnKnCIO5Z4/H1AleHl/RqXYZVt4vIxGqurFsZcot7RE46ZsnAN" +
                "ggliwwfFWJmDEcoMyNPGA6PmG0AKNgHBm5sGYFjHlrWreiFEEmkmy2I5p10JYqNVmOS4d+OmVilZVSgs" +
                "6uCJQJD54Mw0VAep5mcYqKrqc+6DQb4AOcjudEmbnDrT0i4UhBs7fCAMbZHikFdcZN6YeRyVHuI+oxcG" +
                "7QctznGBnaedx7cJwzsImfbjXTfe/ZkkfwGXtiR+oAcAAA==";
                
    }
}
