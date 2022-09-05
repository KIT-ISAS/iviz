/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Wrench : IDeserializable<Wrench>, IHasSerializer<Wrench>, IMessage
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        [DataMember (Name = "force")] public Vector3 Force;
        [DataMember (Name = "torque")] public Vector3 Torque;
    
        public Wrench()
        {
        }
        
        public Wrench(in Vector3 Force, in Vector3 Torque)
        {
            this.Force = Force;
            this.Torque = Torque;
        }
        
        public Wrench(ref ReadBuffer b)
        {
            b.Deserialize(out Force);
            b.Deserialize(out Torque);
        }
        
        public Wrench(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Force);
            b.Deserialize(out Torque);
        }
        
        public Wrench RosDeserialize(ref ReadBuffer b) => new Wrench(ref b);
        
        public Wrench RosDeserialize(ref ReadBuffer2 b) => new Wrench(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Force);
            b.Serialize(in Torque);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in Force);
            b.Serialize(in Torque);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 48;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Wrench";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4f539cf138b23283b520fd271b567936";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61SwUrEMBC95ysG9qJQIqh4EDzLHgRB8SqzzTQbTJM6mVrr1zvZLl3Ws4WWafLem/cm" +
                "2cDrPhRgGpgKJSnQZW4JQoKOiaAM2FIDhQZkFHK6IdlsICgyhkTIgMnp68eotaKkWPNGrWS+gUXs9Kvf" +
                "z5GMefjnxzy9PN6Dp9yT8PzeF1+ujk3V69+ECF+HvfOQFhS6FVBsTnGGnjCJOj4xlegCKzXkZFWVmDSf" +
                "TicIuEwFUhbV6PFDJSkVqmwcBhVDEMZUIlZuXVbKBVlvG5j2lBZUSF6BquApEYcWOPjgFqY26lcywjFc" +
                "A9JdwxRiXDwvzWRPKsJZDoRLC9sO5jzCVANpweBQsArtaPWFu1j95gbGavwgcT7Q56xnr2MpBX29IEUI" +
                "nTWmixnl7ha+12peqx/zCz82wB5hAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Wrench> CreateSerializer() => new Serializer();
        public Deserializer<Wrench> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Wrench>
        {
            public override void RosSerialize(Wrench msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Wrench msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Wrench msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Wrench msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<Wrench>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Wrench msg) => msg = new Wrench(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Wrench msg) => msg = new Wrench(ref b);
        }
    }
}
