/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Accel")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Accel : IMessage, System.IEquatable<Accel>, IDeserializable<Accel>
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear { get; set; }
        [DataMember (Name = "angular")] public Vector3 Angular { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Accel(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Accel(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Accel(ref b);
        }
        
        readonly Accel IDeserializable<Accel>.RosDeserialize(ref Buffer b)
        {
            return new Accel(ref b);
        }
        
        public override readonly int GetHashCode() => (Linear, Angular).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Accel s && Equals(s);
        
        public readonly bool Equals(Accel o) => (Linear, Angular) == (o.Linear, o.Angular);
        
        public static bool operator==(in Accel a, in Accel b) => a.Equals(b);
        
        public static bool operator!=(in Accel a, in Accel b) => !a.Equals(b);
    
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
        [Preserve] public const string RosMessageType = "geometry_msgs/Accel";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0pkddjITenrdja+3kyyRvRsY6GSqqqsq" +
                "V/jYhQI+DcKlcAE1DUcW0pATQkInzCgDNYyt5D1PHzUjaEEMiUlAqbXjD9HmgURL7T650Sz3OEP+3s84" +
                "557/+XGv7y9P8Jx7Vhm/+uLL7Xmru1oyCk8ZOZlzwvd8dxmwhkE3CsPmFEf0TElhYVemEdsgRrVyalNl" +
                "4S4LV1YH2mztpaym0dPeJDkVntg0DCZGUKFU4lLs3CCuufZ1hePOWp1RIXkDmoLnxBIaSPChXZi2qF/J" +
                "hHO4Ctrd4RhiXDwvy3THJiJZZ8JNjU2HMR9wnALZIGhJaRLa8uqLtnHymyscJuOzxGWhb9n+vdVSCnm2" +
                "7ooytbVzXcykjw84rdO4Tj/uF/F1eHFjAgAA";
                
    }
}
