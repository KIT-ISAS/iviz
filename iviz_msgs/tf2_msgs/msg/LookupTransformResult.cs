/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LookupTransformResult : IDeserializable<LookupTransformResult>, IResult<LookupTransformActionResult>
    {
        [DataMember (Name = "transform")] public GeometryMsgs.TransformStamped Transform;
        [DataMember (Name = "error")] public Tf2Msgs.TF2Error Error;
    
        /// Constructor for empty message.
        public LookupTransformResult()
        {
            Error = new Tf2Msgs.TF2Error();
        }
        
        /// Explicit constructor.
        public LookupTransformResult(in GeometryMsgs.TransformStamped Transform, Tf2Msgs.TF2Error Error)
        {
            this.Transform = Transform;
            this.Error = Error;
        }
        
        /// Constructor with buffer.
        internal LookupTransformResult(ref Buffer b)
        {
            Transform = new GeometryMsgs.TransformStamped(ref b);
            Error = new Tf2Msgs.TF2Error(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LookupTransformResult(ref b);
        
        LookupTransformResult IDeserializable<LookupTransformResult>.RosDeserialize(ref Buffer b) => new LookupTransformResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Transform.RosSerialize(ref b);
            Error.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Error is null) throw new System.NullReferenceException(nameof(Error));
            Error.RosValidate();
        }
    
        public int RosMessageLength => 0 + Transform.RosMessageLength + Error.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3fe5db6a19ca9cfb675418c5ad875c36";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXW/bNhR916+4aB6aDIm8JW0xGE2BoHE6YYmdOk6wPRmMdC0RkUiVpOJ6v36H1Ifd" +
                "pNj2sMYwbPHyfp9zr52zrtiZzbKyuR0tjFB2pU1140RVc0auF0RuddzpXBxPjNGG2H9Gp//zK7q6+TSm" +
                "/B/TivZoUUhL/LU2bC1bEttMaWV0RanWJpNKOMZZVEwFi4xNHA5L6V04Ta7g55ppIctsuVXso1UIJXIm" +
                "/6itKzfUWLTofhPcQOu9oMLw6vRV4Vw9Ho3W8kHGRttYm3zkVq8+uNX7kfhAtUgf4Cj2NjcMh85SptOm" +
                "YuWEk1oR6kAMgyvlSwrCOIp+CzV0pUTWGanyJ+nSXsimrQRHvWqL9EqtNBq6uYPuD4LRuqxFsM3c1+uE" +
                "yoTJ0E0nMuFEqLWQecHmqORHLmHUci/cuk3NNu4hwDtnxUaUffcBYqqrqlEy9Qg6CZR27WEpFehRC+Nk" +
                "2pTCPAPce8fb8peGVcqUnI+hoyynjZNIaAMPqWFhfbeTc4oaqdzJsTeI9hZrfYQj58BlCI6WC0c7BM1I" +
                "2DFi/NQWF8M3msOIklnaD7IljvaAEAQpcK3TgvaR+fXGFSCEx/BRGCnuy0DAFB2A19fe6PXBjmef9piU" +
                "ULp333rcxvgvbtXg19d0VACz0ldvmxwNhGJt9KPMtuxPSwnyUinvjTCbyFu1IaO9i0BF5+ELiOBbWKtT" +
                "CQAyWktX9EweRu5ll0pPLsMeLJRhQ0nbhXLPbs2Mbq31M/KAkxhXgym2GGtwKbrj1Glz0tqXYXSjzw0M" +
                "jPKjbXQ74y9TZJfMd0oU9BjunuTvJyEJ3NUKzK9YAFYM2WAJw0wamPqVBK+MjYdNdYgthiWGfijt4KMS" +
                "D3DJIJK3FnUNZ2K3J14Mk32O8/iQ1gX6G7Q8EcLYhkGXKRmZY48NaAzGgrriDgm/TSBSWbY5t8EAIZz0" +
                "3T6IKVnRRje09gXhwXT7RQPeIa8wB07rQ79cOhffNvRaY9q3PwXKOmw2oL4qtXDv3tDX4WkzPP31IlBv" +
                "OfY9tBVp40e0bd83mPvTly1BfZP/taD+af2jaOye/t0IW/dXms6Wk/l8NqdT+rkTXc5mv99eD+JfOvHH" +
                "2XQ6+bhI7pLFn8PlcXc5+WMxP7ueXZ4tktl0uD3pbpPp3dllcr48m3+6vZpMF4PCm05hkVxNZrdb+dte" +
                "Pj+b3lzM5lfDzbuou2r/LXWbLhyW7SGK/gaZ3LurhAkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
