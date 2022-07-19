/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PointStamped : IDeserializableRos1<PointStamped>, IDeserializableRos2<PointStamped>, IMessageRos1, IMessageRos2
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
        
        /// Constructor with buffer.
        public PointStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Point);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new PointStamped(ref b);
        
        public PointStamped RosDeserialize(ref ReadBuffer b) => new PointStamped(ref b);
        
        public PointStamped RosDeserialize(ref ReadBuffer2 b) => new PointStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Point);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Point);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 24 + Header.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Point);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PointStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
