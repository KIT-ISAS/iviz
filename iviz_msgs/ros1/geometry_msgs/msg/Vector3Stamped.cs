/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Vector3Stamped : IDeserializable<Vector3Stamped>, IHasSerializer<Vector3Stamped>, IMessage
    {
        // This represents a Vector3 with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "vector")] public Vector3 Vector;
    
        public Vector3Stamped()
        {
        }
        
        public Vector3Stamped(in StdMsgs.Header Header, in Vector3 Vector)
        {
            this.Header = Header;
            this.Vector = Vector;
        }
        
        public Vector3Stamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Vector);
        }
        
        public Vector3Stamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Vector);
        }
        
        public Vector3Stamped RosDeserialize(ref ReadBuffer b) => new Vector3Stamped(ref b);
        
        public Vector3Stamped RosDeserialize(ref ReadBuffer2 b) => new Vector3Stamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Vector);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Vector);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 24 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 24; // Vector
            return c;
        }
    
        public const string MessageType = "geometry_msgs/Vector3Stamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7b324c7325e683bf02a9b14b01090ec7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwW7UMBC9+ytG2kNbtA2iRRwqcUNAD0gVrbggVM3Gk8QisYM92W34ep6dbmjFhQOs" +
                "IsVr+72ZefMmG7rrXKIoY5QkXhMxfZFaQ7ykg9MOJ41E8bVQHUK0zrMKNZEHIfaW1A2SlIfRfBS2Eqkr" +
                "L3Pk2Je3MW//8c98uv1wRUnt/ZDa9HIJbjZ0q8iKo6VBlC0rUxOQlGs7iee97KWnkq5YKqc6j5IqAIsM" +
                "eFrxErnvZ5oSLmlA3cMweVfnwtdyj3ggnYdmI0d19dRz/EOnzI4nyY+p6Hj97gp3fJJ6UoeEZjDUUTg5" +
                "3+KQzOS8Xl5kAG3o6+eQXn0zm7tDOMe+tNB4zYK0Y81Zy0PuX06Y0xWCvViqrBAEKgnC2USnZe8ef9MZ" +
                "IRpykTHUHZ2ihJtZu+BBKLTn6HjXSyauIQVYTzLo5OwJsy/Unn040i+Mv2P8Da1feXNN5x2a12cZ0tRC" +
                "SVwcY9g7i6u7uZDUvYNLqXe7yHE2GbWENJv3xZSa+1hagzenFGqHTthiZpM0ZvbSlntn/5ctWwmwX5wX" +
                "bz6OwtFjz0ZtGY9soSYKKhm5liq75bq0NXi4YxBGxTDiigTQugioC74CKwYULpctOSUbJJEPCo6Bv4NS" +
                "oHFG8ziCDI6P7FPPGZu3ATmVqq22dOjEL7eyRsXaZRhcTdG1zi5IBBpW8Pqt2JI2F9C475ecl2BoGEhi" +
                "0AI4q+i6oTlMdMgFYREfZzDQTta8ikU0hG0ewEeK54LeBAwCZEmJW7jJJ8X0V8Y0fWB985oe1tW8rn6a" +
                "X/aZhrTqBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Vector3Stamped> CreateSerializer() => new Serializer();
        public Deserializer<Vector3Stamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Vector3Stamped>
        {
            public override void RosSerialize(Vector3Stamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Vector3Stamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Vector3Stamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Vector3Stamped msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<Vector3Stamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Vector3Stamped msg) => msg = new Vector3Stamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Vector3Stamped msg) => msg = new Vector3Stamped(ref b);
        }
    }
}
