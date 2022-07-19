/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Wrench : IDeserializableRos1<Wrench>, IDeserializableRos2<Wrench>, IMessageRos1, IMessageRos2
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        [DataMember (Name = "force")] public Vector3 Force;
        [DataMember (Name = "torque")] public Vector3 Torque;
    
        /// Constructor for empty message.
        public Wrench()
        {
        }
        
        /// Explicit constructor.
        public Wrench(in Vector3 Force, in Vector3 Torque)
        {
            this.Force = Force;
            this.Torque = Torque;
        }
        
        /// Constructor with buffer.
        public Wrench(ref ReadBuffer b)
        {
            b.Deserialize(out Force);
            b.Deserialize(out Torque);
        }
        
        /// Constructor with buffer.
        public Wrench(ref ReadBuffer2 b)
        {
            b.Deserialize(out Force);
            b.Deserialize(out Torque);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Wrench(ref b);
        
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 48;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Force);
            WriteBuffer2.AddLength(ref c, Torque);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Wrench";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4f539cf138b23283b520fd271b567936";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61SwUrEMBC95ysG9qJQIqh4EDzLHgRB8SqzzTQbTJM6mVrr1zvZLl3Ws4WWafLem/cm" +
                "2cDrPhRgGpgKJSnQZW4JQoKOiaAM2FIDhQZkFHK6IdlsICgyhkTIgMnp68eotaKkWPNGrWS+gUXs9Kvf" +
                "z5GMefjnxzy9PN6Dp9yT8PzeF1+ujk3V69+ECF+HvfOQFhS6FVBsTnGGnjCJOj4xlegCKzXkZFWVmDSf" +
                "TicIuEwFUhbV6PFDJSkVqmwcBhVDEMZUIlZuXVbKBVlvG5j2lBZUSF6BquApEYcWOPjgFqY26lcywjFc" +
                "A9JdwxRiXDwvzWRPKsJZDoRLC9sO5jzCVANpweBQsArtaPWFu1j95gbGavwgcT7Q56xnr2MpBX29IEUI" +
                "nTWmixnl7ha+12peqx/zCz82wB5hAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
