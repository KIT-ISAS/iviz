namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Wrench : IMessage
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        public Vector3 force;
        public Vector3 torque;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            force.Deserialize(ref ptr, end);
            torque.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            force.Serialize(ref ptr, end);
            torque.Serialize(ref ptr, end);
        }
    
        public int GetLength() => 48;
    
        public IMessage Create() => new Wrench();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Wrench";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4f539cf138b23283b520fd271b567936";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE61SwUrEMBC95ysG9qJQIqh4EDzLHgRB8SqzzTQbTJM6mVrr1zvZLl3Ws4WWafLem/cm" +
                "2cDrPhRgGpgKJSnQZW4JQoKOiaAM2FIDhQZkFHK6IdlsICgyhkTIgMnp68eotaKkWPNGrWS+gUXs9Kvf" +
                "z5GMefjnxzy9PN6Dp9yT8PzeF1+ujk3V69+ECF+HvfOQFhS6FVBsTnGGnjCJOj4xlegCKzXkZFWVmDSf" +
                "TicIuEwFUhbV6PFDJSkVqmwcBhVDEMZUIlZuXVbKBVlvG5j2lBZUSF6BquApEYcWOPjgFqY26lcywjFc" +
                "A9JdwxRiXDwvzWRPKsJZDoRLC9sO5jzCVANpweBQsArtaPWFu1j95gbGavwgcT7Q56xnr2MpBX29IEUI" +
                "nTWmixnl7ha+12peqx/zCz82wB5hAgAA";
                
    }
}
