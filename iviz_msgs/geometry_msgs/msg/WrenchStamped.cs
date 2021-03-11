/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/WrenchStamped")]
    public sealed class WrenchStamped : IDeserializable<WrenchStamped>, IMessage
    {
        // A wrench with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "wrench")] public Wrench Wrench { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public WrenchStamped()
        {
            Wrench = new Wrench();
        }
        
        /// <summary> Explicit constructor. </summary>
        public WrenchStamped(in StdMsgs.Header Header, Wrench Wrench)
        {
            this.Header = Header;
            this.Wrench = Wrench;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public WrenchStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Wrench = new Wrench(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new WrenchStamped(ref b);
        }
        
        WrenchStamped IDeserializable<WrenchStamped>.RosDeserialize(ref Buffer b)
        {
            return new WrenchStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Wrench is null) throw new System.NullReferenceException(nameof(Wrench));
            Wrench.RosValidate();
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
        [Preserve] public const string RosMessageType = "geometry_msgs/WrenchStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d78d3cb249ce23087ade7e7d0c40cfa7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7UMBC9W9p/GGkPbdFukFrEYSUOSAjoAalSKzhWs8kksUjsYDu7DV/PG2ebUsGB" +
                "A7CKNnHi92bezBuv6S0dg7iypaNNLQWpRZdCpfehso6TUB24F2JXUbK9xMT9YD4KVxKozTfz5USRb2Zl" +
                "3vzl38p8uv2wo5iq+z428eUcfWXWdJuQF4eKeklccWKqPdKyTSth28lBOsoJS0X5a5oGiQWAd62NhKsR" +
                "J4G7bqIxYlPyUN73o7OlSl8EP+KBtI6YBg7JlmPH4ZdKKTuuKN/GXMnrdzvscVHKMVkkNIGhDMLRugYf" +
                "yYzWpatLBZj13dFvsZQGxV2CU2o5abLyMASJmifHHWK8mMUV4EZ1BFGqSOf53T2W8YIQBCnI4NGec2R+" +
                "M6XWOxAKHThY3neixCUqANYzBZ1d/MSsae/IsfOP9DPjU4w/oXULr2ratuhZp+rj2KCA2DgEf7AVtu6n" +
                "TFJ2Vlyizu4Dh8koag5p1u+zG5O2L3cEd47RlxYNqLKLTUxB2XM37m317wzZiIfvwjS7cp6C1aO5gmiz" +
                "ICOqJ1Ez1KkOAiEDl7JBv2CinDQa7tVY2ImyCEqiw8auyf5Sq8Gyn6VMPlzRTPa0xD+M9r80nsL+TiTT" +
                "IX98rrPQWbjO7vUO3u+F0ViM2YIEsLIBUOtdAVYcQJCIAtlElZdIzidw9PwVlAIrEdA8DCDDPAd2sWPF" +
                "6mtAzqVoig0dW8Gc6i61Qh7cPOq2pGAbi0lXJAL1C5jppG5Dqb6ElbpuznkOBl+CJPiUARcFXdc0+ZGO" +
                "KggP4XTCeNojxVNeeRKS9xs9Xk4Uzyt649F+lCVGbtQjMeFwK4ypO8/p9St6WJ6m5en7yvwA0MuOH7oF" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
