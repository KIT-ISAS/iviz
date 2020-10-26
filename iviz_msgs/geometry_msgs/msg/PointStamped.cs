/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/PointStamped")]
    public sealed class PointStamped : IMessage, IDeserializable<PointStamped>
    {
        // This represents a Point with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "point")] public Point Point { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PointStamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointStamped(StdMsgs.Header Header, in Point Point)
        {
            this.Header = Header;
            this.Point = Point;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PointStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Point = new Point(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PointStamped(ref b);
        }
        
        PointStamped IDeserializable<PointStamped>.RosDeserialize(ref Buffer b)
        {
            return new PointStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Point.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PointStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VTTYvcMAy9+1cI5rC7hdlCW3oY6K3041BY2L0PGltJDImdWsrspr++zw4zLfTSQxtM" +
                "bMfWk97Ty46ehqhUZC6ikkyJ6SHHZPQcbcD3TookL+RzLiEmNqGu8CTEKZDFSdR4mt0X4SCFhja5DWGu" +
                "b+c+/OPHfXv8fCC1cJy019dbZrejR0NJXAJNYhzYmLqMimI/SNmPcpaRWq0SqJ3aOoveI7ApgNFLksLj" +
                "uNKiuGQZpKdpSdFX1leul3hExgS5Zi4W/TJy+UOkio6h8n1pIn79eMCdpOIXiyhoBYIvwhpTj0NyCxR7" +
                "+6YGuN3Tc95jKz10vSYnG9hqsfJSO1brZD0gx6uN3D2wIY4gS1C6bd+O2OodIQlKkDn7gW5R+cNqQ04A" +
                "FDpziXwapQJ7KADUmxp0c/cbcmrQiVO+wG+Iv3L8DWy64lZO+wE9Gyt7XXoIiItzyecYcPW0NhA/RviS" +
                "xngqXFZXo7aUbvepGdFq+1pHMLNq9hENCM3ATq1U9NaNYwz/y429ZLiurJslm/0vxoJUxjFpIzNnjRYh" +
                "T+6qc9pvAs26IiA1sxfXjZnt/Tt6ua7W6+qHcz8BR2MSRbADAAA=";
                
    }
}
