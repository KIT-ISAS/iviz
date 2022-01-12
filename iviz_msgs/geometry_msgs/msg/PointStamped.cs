/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PointStamped : IDeserializable<PointStamped>, IMessage
    {
        // This represents a Point with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "point")] public Point Point;
    
        /// Constructor for empty message.
        public PointStamped()
        {
        }
        
        /// Explicit constructor.
        public PointStamped(in StdMsgs.Header Header, in Point Point)
        {
            this.Header = Header;
            this.Point = Point;
        }
        
        /// Constructor with buffer.
        public PointStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Point);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PointStamped(ref b);
        
        public PointStamped RosDeserialize(ref ReadBuffer b) => new PointStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Point);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 24 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PointStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VTTYvcMAy9+1cI5rC7hdlCW3oY6K3041BY2L0PGltJDImdWsrspr++zw4zLfTSQxtM" +
                "bMfWk97Ty46ehqhUZC6ikkyJ6SHHZPQcbcD3TookL+RzLiEmNqGu8CTEKZDFSdR4mt0X4SCFhja5DWGu" +
                "b+c+/OPHfXv8fCC1cJy019dbZrejR0NJXAJNYhzYmLqMimI/SNmPcpaRWq0SqJ3aOoveI7ApgNFLksLj" +
                "uNKiuGQZpKdpSdFX1leul3hExgS5Zi4W/TJy+UOkio6h8n1pIn79eMCdpOIXiyhoBYIvwhpTj0NyCxR7" +
                "+6YGuN3Tc95jKz10vSYnG9hqsfJSO1brZD0gx6uN3D2wIY4gS1C6bd+O2OodIQlKkDn7gW5R+cNqQ04A" +
                "FDpziXwapQJ7KADUmxp0c/cbcmrQiVO+wG+Iv3L8DWy64lZO+wE9Gyt7XXoIiItzyecYcPW0NhA/RviS" +
                "xngqXFZXo7aUbvepGdFq+1pHMLNq9hENCM3ATq1U9NaNYwz/y429ZLiurJslm/0vxoJUxjFpIzNnjRYh" +
                "T+6qc9pvAs26IiA1sxfXjZnt/Tt6ua7W6+qHcz8BR2MSRbADAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
