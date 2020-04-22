
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Vector3Stamped : IMessage
    {
        // This represents a Vector3 with reference coordinate frame and timestamp
        public std_msgs.Header header;
        public Vector3 vector;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Vector3Stamped";
    
        public IMessage Create() => new Vector3Stamped();
    
        public int GetLength()
        {
            int size = 24;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Vector3Stamped()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            vector.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            vector.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "7b324c7325e683bf02a9b14b01090ec7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
