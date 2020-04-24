namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TwistWithCovarianceStamped : IMessage
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        public std_msgs.Header header;
        public TwistWithCovariance twist;
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovarianceStamped()
        {
            header = new std_msgs.Header();
            twist = new TwistWithCovariance();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            twist.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            twist.Serialize(ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 336;
            size += header.GetLength();
            return size;
        }
    
        public IMessage Create() => new TwistWithCovarianceStamped();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE71VTW/bMAy9+1cQyKHNkGRAO+RQYKcN23oYUKzFPjEUjM3YWm3Jo+Qm3q/fk5y4KdpD" +
                "D1sDA7Fl8pF8fKQndFUZTyqtihcbPLEl8cE0HKSgsDE+0MaECiZrUbG5UO6cFsbCgNbKjcAFlqaBGzft" +
                "IvsgXIhSlf6yqwjxBQhv3C2r4YiQYLPs9T/+ZR8v35+RD8V140v/csgjm9BlQIasBTUSuODAtHbIz5SV" +
                "6LyWW6kppY6C09vQt+IXcEzc4CrFinJd99T5yIoDB03TWZNHEsbS9/7wNJaYWtZg8q5mfcBZRMfl5XeX" +
                "OD1/ewYb6yXvgkFCPRByFfbGlnhJWWdsOD2JDtnkauPmeJQSLI/BKVQcYrKyjb2MebI/Q4wXQ3ELYIMc" +
                "QZTC03E6u8ajnxKCIAVpXV7RMTK/6EPlLACFUstWtUTgHAwA9Sg6HU0PkG2CtmzdHn5AvIvxFFg74saa" +
                "5hV6VsfqfVeCQBi26m5NAdNVn0Dy2kCxVJuVsvZZ9BpCZpN3SZchti91BP/svctNUnXUc+aDRvTUjWtT" +
                "/C81luKgOu0HST4yDHuZ7dvmCe1HpiFKAOkJimoZbKYp7OChgdH9fpENs7Wfpgl9cpt5w7+g7XGeORhQ" +
                "7taJsOV2CZGNU4gRV7NN8YWcmtEcugUpQdRHvUPIa7OVYs7bw02RTKOMz4GvGLRZinHgyypRf8fbGfUz" +
                "+jMjdbsAvHJdoK8UER8cf3v8+Hs6nmbr2nFYvvpxuvx5UMwztu/JDVupuxGLQ+wLg80KNQuUHLcl2zKt" +
                "hbghsGk+Sx6cntLO5O55Z/c81e2i7us7/CagxPjufoGLuMHO085xFhurEcY4otjRE46FUbhGqUSZ4SPi" +
                "VGaggwoH5qyLdDZ8A0jBAoje3LYAwxZWtr4eJJAYpGNZlIsZbSqwmqziAKd1mxa0yUlNaYrBE4Ga0Zlp" +
                "VxxEuj7BKNX1kPMQDMIFyF5w0wWdr6l3HW1iQbjR3XfB0UrGvNL+Cs7N0pAMEPcJvXDoPWjxnkusOusD" +
                "vkgY252EaTve9ePdn+wv5KrBh5MHAAA=";
                
    }
}
