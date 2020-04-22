
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PolygonStamped : IMessage
    {
        // This represents a Polygon with reference coordinate frame and timestamp
        public std_msgs.Header header;
        public Polygon polygon;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PolygonStamped";
    
        public IMessage Create() => new PolygonStamped();
    
        public int GetLength()
        {
            int size = 0;
            size += header.GetLength();
            size += polygon.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public PolygonStamped()
        {
            header = new std_msgs.Header();
            polygon = new Polygon();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            polygon.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            polygon.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "c6be8f7dc3bee7fe9e8d296070f53340";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/UMBC9R9r/MNIe2iJaJLitxAGBgB6QKrU3hCqvPZtYOHbwONuGX88bJ9lW4sIB" +
                "iCLlw54382be85buOi+UecgsHIuQoZsUpjZFevClw8qBM0fLZFPKzkdTmA7Z9EwmOiq+ZymmH5rPbBxn" +
                "6uqjWTGG+dlsmrd/+do0X24/7UiKu++llVdz/k2zpduCykx21HMxzhRDh4TCfNtxvgx85EC1ZHZUV8s0" +
                "sFw1Sytwtxw5mxAmGgWbSgL3vh+jt0r+RHmNR6SP6NtgcvF2DCb/1itFxy38Y6y9vP6ww54obMfiUdAE" +
                "BJvZiI8tFqkZfSxvXmtAs717SJf45BbtPSWn0pmixfKjjk7rNLJDjhczuStgozuMLE7ovP67x6dcEJKg" +
                "BB6S7egcld9MpcOoSsd0NNmbfWAFtugAUM806OziGbKWvaNoYlrhZ8SnHH8CG0+4yumyw8yCspexRQOx" +
                "ccjp6B227qcKYoOHQCn4fTZ5ajRqTtlsP1Y9Fh1fnQieRiRZjwG4quNGSlb0Oo177/6dIFtO0F2eZlUu" +
                "PoAs35EMbP1BReTRl3RQyaxWgzSVAOTis5RqrWDwMiRMHmywCkZjP8txr26MkS3owWpVK1+/LZv/H7Wa" +
                "d7MaBxUV46NUGkMS/5wmtqpJDpkxtMFYPq+nCyS+9+CHXVCx9YKQCzXLddU2fsF47GbWMCPOJkWq7TrC" +
                "DkgjvgorSsEBoEBLXVekjluLW5BwLtSq8AeAUFifYFE1V04DPL/3wZephq6R8JuYtkrXsfg2zsUU851p" +
                "HChgeWakVUU4LsL4LaJDWogtIyyU4JKXmKN2oiragFFtUK35fUij09zNISSj9n88vU2nt5+b5he6T1q8" +
                "tQUAAA==";
                
    }
}
