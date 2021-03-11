/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Vector3Stamped")]
    public sealed class Vector3Stamped : IDeserializable<Vector3Stamped>, IMessage
    {
        // This represents a Vector3 with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "vector")] public Vector3 Vector { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Vector3Stamped()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Vector3Stamped(in StdMsgs.Header Header, in Vector3 Vector)
        {
            this.Header = Header;
            this.Vector = Vector;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Vector3Stamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Vector = new Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Vector3Stamped(ref b);
        }
        
        Vector3Stamped IDeserializable<Vector3Stamped>.RosDeserialize(ref Buffer b)
        {
            return new Vector3Stamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Vector.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
        [Preserve] public const string RosMessageType = "geometry_msgs/Vector3Stamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7b324c7325e683bf02a9b14b01090ec7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWrcMBC9C/YfBvaQpGxcSEsPC72VtjkUAgm9hll7bIvKkiuNd+N+fZ/kjduSSw/t" +
                "YpBs6b2ZefNmt/TQ20RRxihJvCZi+iq1hviGTlZ7nLQSxddCdQixsZ5VqI08CLFvSO0gSXkYzWfhRiL1" +
                "ZTHPHMeymo15/49/G/Pl/tOekjaPQ+rS6yX8xmzpXpEYx4YGUW5YmdqAvGzXS7x2chRHJWNpqJzqPEqq" +
                "zFkJPJ14iezcTFPCJQ0ofRgmb+tc+1rxMx5I6yHbyFFtPTmOL6TK7HiSfJ+KlLcf9rjjk9STWiQ0g6GO" +
                "wsn6DodkJuv1zU0GmO3DKVzjVTqouwYn7VlzsvKUO5fz5LRHjFdLcRW4oY4gSpPosnx7xGu6IgRBCjKG" +
                "uqdLZH43ax88CIWOHC0fnGTiGgqA9SKDLq5+Y85p78mzD8/0C+OvGH9D61feXNN1j565XH2aOgiIi2MM" +
                "R9vg6mEuJLWz8Cc5e4gcZ5NRS0iz/VjsqLl9pSNYOaVQWzSgKTY2SWNmL914tM3/M2QnAb6L8+LK8xhk" +
                "W76cs2U2snnaKChm5Fqq7JPb0tng4YtBGEXDgisSwMZGQG3wFVgxnfC37MgqNUES+aDgGPgbKAUyE9A8" +
                "jiCD1yP75Dhj82dALqXqqh2deoGH860sUzF1GQNbU7SdxRRkJAINK3j9o9iRtjeQ2bkl5yUYegaSGLQA" +
                "riq6bWkOE51yQdjE8/QFOiDFc17FJRrCLo/emeJPRe8CZgGypMQdDOWTYvArY1oXWN+9pad1N6+7Hxvz" +
                "E98d2uDoBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
