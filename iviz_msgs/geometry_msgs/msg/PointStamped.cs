using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PointStamped : IMessage
    {
        // This represents a Point with reference coordinate frame and timestamp
        public std_msgs.Header header { get; set; }
        public Point point { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PointStamped()
        {
            header = new std_msgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointStamped(std_msgs.Header header, Point point)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.point = point;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PointStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.point = new Point(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new PointStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            this.point.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/PointStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
