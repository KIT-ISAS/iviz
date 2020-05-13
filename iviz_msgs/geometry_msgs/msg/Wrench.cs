using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [DataContract]
    public sealed class Wrench : IMessage
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        [DataMember] public Vector3 force { get; set; }
        [DataMember] public Vector3 torque { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Wrench()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Wrench(Vector3 force, Vector3 torque)
        {
            this.force = force;
            this.torque = torque;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Wrench(Buffer b)
        {
            this.force = new Vector3(b);
            this.torque = new Vector3(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Wrench(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.force);
            b.Serialize(this.torque);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 48;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Wrench";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4f539cf138b23283b520fd271b567936";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61SwUrEMBC95ysG9qJQIqh4EDzLHgRB8SqzzTQbTJM6mVrr1zvZLl3Ws4WWafLem/cm" +
                "2cDrPhRgGpgKJSnQZW4JQoKOiaAM2FIDhQZkFHK6IdlsICgyhkTIgMnp68eotaKkWPNGrWS+gUXs9Kvf" +
                "z5GMefjnxzy9PN6Dp9yT8PzeF1+ujk3V69+ECF+HvfOQFhS6FVBsTnGGnjCJOj4xlegCKzXkZFWVmDSf" +
                "TicIuEwFUhbV6PFDJSkVqmwcBhVDEMZUIlZuXVbKBVlvG5j2lBZUSF6BquApEYcWOPjgFqY26lcywjFc" +
                "A9JdwxRiXDwvzWRPKsJZDoRLC9sO5jzCVANpweBQsArtaPWFu1j95gbGavwgcT7Q56xnr2MpBX29IEUI" +
                "nTWmixnl7ha+12peqx/zCz82wB5hAgAA";
                
    }
}
