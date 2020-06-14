using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Inertia")]
    public sealed class Inertia : IMessage
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
    
        /// <summary> Constructor for empty message. </summary>
        public Inertia()
        {
        }
        
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
        internal Inertia(Buffer b)
        {
            M = b.Deserialize<double>();
            Com = new GeometryMsgs.Vector3(b);
            Ixx = b.Deserialize<double>();
            Ixy = b.Deserialize<double>();
            Ixz = b.Deserialize<double>();
            Iyy = b.Deserialize<double>();
            Iyz = b.Deserialize<double>();
            Izz = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Inertia(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(M);
            Com.RosSerialize(b);
            b.Serialize(Ixx);
            b.Serialize(Ixy);
            b.Serialize(Ixz);
            b.Serialize(Iyy);
            b.Serialize(Iyz);
            b.Serialize(Izz);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 80;
    
        public string RosType => RosMessageType;
    
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
