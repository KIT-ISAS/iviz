/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct TransformStamped : IMessage, System.IEquatable<TransformStamped>, IDeserializable<TransformStamped>
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id
        //
        // This message is mostly used by the 
        // <a href="http://wiki.ros.org/tf">tf</a> package. 
        // See its documentation for more information.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "child_frame_id")] public string ChildFrameId; // the frame id of the child frame
        [DataMember (Name = "transform")] public Transform Transform;
    
        /// Explicit constructor.
        public TransformStamped(in StdMsgs.Header Header, string ChildFrameId, in Transform Transform)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Transform = Transform;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TransformStamped(ref Buffer b)
        {
            Deserialize(ref b, out this);
        }
        
        internal static void Deserialize(ref Buffer b, out TransformStamped h)
        {
            StdMsgs.Header.Deserialize(ref b, out h.Header);
            h.ChildFrameId = b.DeserializeString();
            b.Deserialize(out h.Transform);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b) => new TransformStamped(ref b);
        
        readonly TransformStamped IDeserializable<TransformStamped>.RosDeserialize(ref Buffer b) => new TransformStamped(ref b);
        
        public override readonly int GetHashCode() => (Header, ChildFrameId, Transform).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is TransformStamped s && Equals(s);
        
        public readonly bool Equals(TransformStamped o) => (Header, ChildFrameId, Transform) == (o.Header, o.ChildFrameId, o.Transform);
        
        public static bool operator==(in TransformStamped a, in TransformStamped b) => a.Equals(b);
        
        public static bool operator!=(in TransformStamped a, in TransformStamped b) => !a.Equals(b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId ?? string.Empty);
            b.Serialize(ref Transform);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 60 + Header.RosMessageLength + BuiltIns.GetStringSize(ChildFrameId);
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/TransformStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b5764a33bfeb3588febc2682852579b0";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/UMBC9+1eM2ENb1GalgjisgFMF9IAEouJaTZNJYjWxU3vSJfx6np3dbEuR4ACs" +
                "Vlo78Yznfczsiq5aG0m+DUFilEhMGtjF2oee6uB7Kr0PlXWsgj33Qq1wJaHIm2tbmRWpJ23l6cmytV11" +
                "fThodrf1uIobobT0UbuJxigV3Uw5DU69ZmqD1G+etarDZr3e2ltbBB8LH5q11s/eav16zW9p4PIWiYoU" +
                "80WQUCNVvhx7ccpqvSPgwB0Br1yClB8WxnzIGHZQTNRgXfNTubTK1cxIsPX1DDIdmp+aq4WphTNj3vzl" +
                "j/n45f2GolbXfWzieq484VV2FYcKbCpXrJyxtrZpJZx1ci8dgrgfQGx+q9MgsdhLgG8jTgJ3e/YhYun7" +
                "fnS2TAqqhUoP4xFpHewxcFBbjh2HJ4Kn7PhGuRvFlUKXFxuccVHKUS0KmpChDMIxsX15QWa0Tl+cpwCz" +
                "utr6M2ylgS7L5aCclR4YtCKOG9zxfAZXIDfIEdxSRTrOz66xjSeES1CCDL5s6RiVf5q0hSGShvccLN90" +
                "2YAlGEDWoxR0dPIgcyp7Q46d36efMx7u+JO0bsmbMJ210KxL6OPYgEAcHIK/t9XB/WVnYV7q7E3gMJkU" +
                "NV9pVu+yFTXJlxXBL8foSwsBKtpabfdOXlruH7mxEQ/XhWm25NIGe3MFSWIBRsyQDgPlRnQrAra2/ol5" +
                "4Em0a0AXR7Q1vGS+Sqk+vJjju9y65vOIgOBSawc/9/j/Abkr5hcQme7zu5/qT51wmb3rHZzfC0NWNNkS" +
                "icDKBoSmkYSsgomHSXWKKYYhBj6cV+To+RYpBUZK0TwMSMYPOUmPEXIsRVOc0rYFv/lUMkJu29zotqRg" +
                "G8yxRY0lmGkH7pS0PoeRum6ueb4MEiLJnu2Tgi5rmvxI2wQIi7CbLx7yLnXlPlDvT9Nw2aV4TOgnj24/" +
                "/BW4qJhsUL3uPOurl/RtWU3L6vt/kfrgsV+p7ciH1KIzfY80T7u7g0ETyb8FtF9tjfkBtot2jIwHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
