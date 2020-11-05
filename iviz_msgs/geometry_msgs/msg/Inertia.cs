/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Inertia")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Inertia : IMessage, System.IEquatable<Inertia>, IDeserializable<Inertia>
    {
        // Mass [kg]
        [DataMember (Name = "m")] public double M { get; set; }
        // Center of mass [m]
        [DataMember (Name = "com")] public GeometryMsgs.Vector3 Com { get; set; }
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        [DataMember (Name = "ixx")] public double Ixx { get; set; }
        [DataMember (Name = "ixy")] public double Ixy { get; set; }
        [DataMember (Name = "ixz")] public double Ixz { get; set; }
        [DataMember (Name = "iyy")] public double Iyy { get; set; }
        [DataMember (Name = "iyz")] public double Iyz { get; set; }
        [DataMember (Name = "izz")] public double Izz { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Inertia(double M, in GeometryMsgs.Vector3 Com, double Ixx, double Ixy, double Ixz, double Iyy, double Iyz, double Izz)
        {
            this.M = M;
            this.Com = Com;
            this.Ixx = Ixx;
            this.Ixy = Ixy;
            this.Ixz = Ixz;
            this.Iyy = Iyy;
            this.Iyz = Iyz;
            this.Izz = Izz;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Inertia(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Inertia(ref b);
        }
        
        readonly Inertia IDeserializable<Inertia>.RosDeserialize(ref Buffer b)
        {
            return new Inertia(ref b);
        }
        
        public override readonly int GetHashCode() => (M, Com, Ixx, Ixy, Ixz, Iyy, Iyz, Izz).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Inertia s && Equals(s);
        
        public readonly bool Equals(Inertia o) => (M, Com, Ixx, Ixy, Ixz, Iyy, Iyz, Izz) == (o.M, o.Com, o.Ixx, o.Ixy, o.Ixz, o.Iyy, o.Iyz, o.Izz);
        
        public static bool operator==(in Inertia a, in Inertia b) => a.Equals(b);
        
        public static bool operator!=(in Inertia a, in Inertia b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 80;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Inertia";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61STUsDMRC951c86EVhXaGKB6EnD9JDQbB4kSrT3dlt6CYpSWq7S3+8k137IXg0MDC8" +
                "vPcyHxlhRiHgfV0vVNU4ig/3MEqN8MQ2soerYHqCWaianeHo208T6nD7xkV0/g6F6/lTyz5qwpxtcD4Z" +
                "3piP8UJu0jlA7/cSrUSHQ+Jj0qOCtCkG9MjtekR3CT2WJQ4XeXuRd+e8vcDbC7zrlJr881Gz1+dH/DkV" +
                "aWW+0gGeN56DjDKA8NXfQVtUnhlhQwXnSLOIEK6zTQvDZCOiOytFWGovUu1sLq7suXKeM+iI0nGAdVE8" +
                "DK3FUobPSU2bjZgRoicbGkraBIvkivM6z7BbsR1Y2tZCFIeaZYW6gNe1LgelPGROYsJPcxliNcZON81Q" +
                "8/BYXLGYeBd7wXWOaYXWbbFLDUniUVKkZLTkU120bFK9LsM2Fd5b/B7oi9OiNxwC1SyzC5GpzNVps+c/" +
                "cd58p74BX6ix6tcCAAA=";
                
    }
}
