/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class TwistStamped : IDeserializable<TwistStamped>, IHasSerializer<TwistStamped>, IMessage
    {
        // A twist with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "twist")] public Twist Twist;
    
        public TwistStamped()
        {
            Twist = new Twist();
        }
        
        public TwistStamped(in StdMsgs.Header Header, Twist Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        public TwistStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Twist = new Twist(ref b);
        }
        
        public TwistStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Twist = new Twist(ref b);
        }
        
        public TwistStamped RosDeserialize(ref ReadBuffer b) => new TwistStamped(ref b);
        
        public TwistStamped RosDeserialize(ref ReadBuffer2 b) => new TwistStamped(ref b);
    
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 48; // Twist
            return size;
        }
    
        public const string MessageType = "geometry_msgs/TwistStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "98d34b0043a2093cf9d9345ab6eef12e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTW/UMBC9+1eMtIe2aBtEizhU4oCEgB6QKlpxQVU1G88mVhM72JNuw6/n2dlNqbhw" +
                "AFaRNh8zb+a9eeMVvSPduaS0c9pSlK1E8bVQHUK0zrMKbSP3QuwtqeslKfeD+SRsJVJb/sxNQSg4xrz9" +
                "yz/z+frjBSW1d31q0su5slnRtaIljpZ6UbasTNuAjlzTSjzt5EE6Kr2KpfJVp0FShcSb1iXC1YiXyF03" +
                "0ZgQpAGk+370rs6sF66HfGQ6T0wDR3X12HH8TaSMjivJ97GIePn+AjE+ST2qQ0MTEOoonJxv8JHM6Lye" +
                "n+UEWtG3LyG9ujWrm104xXtpIPDSBWnLmruWxyFKyg1zukCxFzPLCkWgkqCcTXRc3t3hMZ0QqqEXGULd" +
                "0jEoXE3aBg9AoQeOjjedZOAaUgD1KCcdnfyC7Au0Zx8O8DPiU40/gfULbuZ02mJ4XZYhjQ2UROAQw4Oz" +
                "CN1MBaTunHilzm0ix8nkrLmkWX0ojtQ8xzIa/HNKoXaYhC1ONkljRi9juXP2X9mykQD7xWn2ZtmDg8MO" +
                "g0qEyaM3zdNHQwIaA0O/TQz34vESznOawNQLpMiLxr4pBsteg2e/Sq0hntM+5Ol5H/d/2O2rHvhFyfww" +
                "IogPivnbc4JV3oXLYtrg4f1eGPME2SUTidZFpLrgK6Di7MEOyxpykA1QzocsZ8/3gBQ4KGfzMAAM+xzZ" +
                "p45zLhUF6ViqplrTroWqJSo7oCxuWXVXU3SNs3MmCvVLMtOe3Jp0ewYHdd3c81wMdgRIDFoSTiq63NIU" +
                "RtplQriJ+xMm0EaWvsoCaAjrfLzsIZ4LehUwe8iSEjfYFZ8UZ1tlzLYLrG9e0+NyNy13P8xPg+jC5rMF" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TwistStamped> CreateSerializer() => new Serializer();
        public Deserializer<TwistStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TwistStamped>
        {
            public override void RosSerialize(TwistStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TwistStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TwistStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TwistStamped msg) => msg.Ros2MessageLength;
            public override void RosValidate(TwistStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TwistStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TwistStamped msg) => msg = new TwistStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TwistStamped msg) => msg = new TwistStamped(ref b);
        }
    }
}
