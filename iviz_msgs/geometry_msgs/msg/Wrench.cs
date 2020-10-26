/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Wrench")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Wrench : IMessage, System.IEquatable<Wrench>, IDeserializable<Wrench>
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        [DataMember (Name = "force")] public Vector3 Force { get; set; }
        [DataMember (Name = "torque")] public Vector3 Torque { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Wrench(in Vector3 Force, in Vector3 Torque)
        {
            this.Force = Force;
            this.Torque = Torque;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Wrench(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Wrench(ref b);
        }
        
        readonly Wrench IDeserializable<Wrench>.RosDeserialize(ref Buffer b)
        {
            return new Wrench(ref b);
        }
        
        public override readonly int GetHashCode() => (Force, Torque).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Wrench s && Equals(s);
        
        public readonly bool Equals(Wrench o) => (Force, Torque) == (o.Force, o.Torque);
        
        public static bool operator==(in Wrench a, in Wrench b) => a.Equals(b);
        
        public static bool operator!=(in Wrench a, in Wrench b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 48;
    
        public readonly string RosType => RosMessageType;
    
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
