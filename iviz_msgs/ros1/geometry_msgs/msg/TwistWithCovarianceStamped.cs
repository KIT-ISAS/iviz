/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class TwistWithCovarianceStamped : IDeserializableRos1<TwistWithCovarianceStamped>, IDeserializableRos2<TwistWithCovarianceStamped>, IMessageRos1, IMessageRos2
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "twist")] public TwistWithCovariance Twist;
    
        /// Constructor for empty message.
        public TwistWithCovarianceStamped()
        {
            Twist = new TwistWithCovariance();
        }
        
        /// Explicit constructor.
        public TwistWithCovarianceStamped(in StdMsgs.Header Header, TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// Constructor with buffer.
        public TwistWithCovarianceStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Twist = new TwistWithCovariance(ref b);
        }
        
        /// Constructor with buffer.
        public TwistWithCovarianceStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Twist = new TwistWithCovariance(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TwistWithCovarianceStamped(ref b);
        
        public TwistWithCovarianceStamped RosDeserialize(ref ReadBuffer b) => new TwistWithCovarianceStamped(ref b);
        
        public TwistWithCovarianceStamped RosDeserialize(ref ReadBuffer2 b) => new TwistWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Twist is null) BuiltIns.ThrowNullReference();
            Twist.RosValidate();
        }
    
        public int RosMessageLength => 336 + Header.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Twist.AddRos2MessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
