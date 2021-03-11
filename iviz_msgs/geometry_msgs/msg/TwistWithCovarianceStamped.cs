/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/TwistWithCovarianceStamped")]
    public sealed class TwistWithCovarianceStamped : IDeserializable<TwistWithCovarianceStamped>, IMessage
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "twist")] public TwistWithCovariance Twist { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovarianceStamped()
        {
            Twist = new TwistWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwistWithCovarianceStamped(in StdMsgs.Header Header, TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwistWithCovarianceStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Twist = new TwistWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovarianceStamped(ref b);
        }
        
        TwistWithCovarianceStamped IDeserializable<TwistWithCovarianceStamped>.RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovarianceStamped(ref b);
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
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            Twist.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 336;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
