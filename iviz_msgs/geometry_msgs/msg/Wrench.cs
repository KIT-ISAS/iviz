/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Wrench : IDeserializable<Wrench>, IMessage
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Wrench(ref b);
        
        public Wrench RosDeserialize(ref ReadBuffer b) => new Wrench(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Force);
            b.Serialize(in Torque);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Wrench";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4f539cf138b23283b520fd271b567936";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61SwUrEMBC95ysG9qJQIqh4EDzLHgRB8SqzzTQbTJM6mVrr1zvZLl3Ws4WWafLem/cm" +
                "2cDrPhRgGpgKJSnQZW4JQoKOiaAM2FIDhQZkFHK6IdlsICgyhkTIgMnp68eotaKkWPNGrWS+gUXs9Kvf" +
                "z5GMefjnxzy9PN6Dp9yT8PzeF1+ujk3V69+ECF+HvfOQFhS6FVBsTnGGnjCJOj4xlegCKzXkZFWVmDSf" +
                "TicIuEwFUhbV6PFDJSkVqmwcBhVDEMZUIlZuXVbKBVlvG5j2lBZUSF6BquApEYcWOPjgFqY26lcywjFc" +
                "A9JdwxRiXDwvzWRPKsJZDoRLC9sO5jzCVANpweBQsArtaPWFu1j95gbGavwgcT7Q56xnr2MpBX29IEUI" +
                "nTWmixnl7ha+12peqx/zCz82wB5hAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
