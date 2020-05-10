using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Inertia : IMessage
    {
        // Mass [kg]
        public double m { get; set; }
        
        // Center of mass [m]
        public geometry_msgs.Vector3 com { get; set; }
        
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        public double ixx { get; set; }
        public double ixy { get; set; }
        public double ixz { get; set; }
        public double iyy { get; set; }
        public double iyz { get; set; }
        public double izz { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Inertia()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Inertia(double m, geometry_msgs.Vector3 com, double ixx, double ixy, double ixz, double iyy, double iyz, double izz)
        {
            this.m = m;
            this.com = com;
            this.ixx = ixx;
            this.ixy = ixy;
            this.ixz = ixz;
            this.iyy = iyy;
            this.iyz = iyz;
            this.izz = izz;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Inertia(Buffer b)
        {
            this.m = b.Deserialize<double>();
            this.com = new geometry_msgs.Vector3(b);
            this.ixx = b.Deserialize<double>();
            this.ixy = b.Deserialize<double>();
            this.ixz = b.Deserialize<double>();
            this.iyy = b.Deserialize<double>();
            this.iyz = b.Deserialize<double>();
            this.izz = b.Deserialize<double>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Inertia(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.m);
            this.com.Serialize(b);
            b.Serialize(this.ixx);
            b.Serialize(this.ixy);
            b.Serialize(this.ixz);
            b.Serialize(this.iyy);
            b.Serialize(this.iyz);
            b.Serialize(this.izz);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 80;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/Inertia";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61STUsDMRC951c86EVhXaGKB6EnD9JDQbB4kSrT3dlt6CYpSWq7S3+8k137IXg0MDC8" +
                "vPcyHxlhRiHgfV0vVNU4ig/3MEqN8MQ2soerYHqCWaianeHo208T6nD7xkV0/g6F6/lTyz5qwpxtcD4Z" +
                "3piP8UJu0jlA7/cSrUSHQ+Jj0qOCtCkG9MjtekR3CT2WJQ4XeXuRd+e8vcDbC7zrlJr881Gz1+dH/DkV" +
                "aWW+0gGeN56DjDKA8NXfQVtUnhlhQwXnSLOIEK6zTQvDZCOiOytFWGovUu1sLq7suXKeM+iI0nGAdVE8" +
                "DK3FUobPSU2bjZgRoicbGkraBIvkivM6z7BbsR1Y2tZCFIeaZYW6gNe1LgelPGROYsJPcxliNcZON81Q" +
                "8/BYXLGYeBd7wXWOaYXWbbFLDUniUVKkZLTkU120bFK9LsM2Fd5b/B7oi9OiNxwC1SyzC5GpzNVps+c/" +
                "cd58p74BX6ix6tcCAAA=";
                
    }
}
