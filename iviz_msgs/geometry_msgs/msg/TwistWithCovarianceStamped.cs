/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/TwistWithCovarianceStamped")]
    public sealed class TwistWithCovarianceStamped : IDeserializable<TwistWithCovarianceStamped>, IMessage
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "twist")] public TwistWithCovariance Twist;
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovarianceStamped()
        {
            Twist = new TwistWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwistWithCovarianceStamped(in StdMsgs.Header Header, TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwistWithCovarianceStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Twist = new TwistWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovarianceStamped(ref b);
        }
        
        TwistWithCovarianceStamped IDeserializable<TwistWithCovarianceStamped>.RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovarianceStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            Twist.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 336;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
