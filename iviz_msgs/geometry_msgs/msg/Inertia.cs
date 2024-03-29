/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Inertia : IDeserializable<Inertia>, IMessage
    {
        // Mass [kg]
        [DataMember (Name = "m")] public double M;
        // Center of mass [m]
        [DataMember (Name = "com")] public GeometryMsgs.Vector3 Com;
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        [DataMember (Name = "ixx")] public double Ixx;
        [DataMember (Name = "ixy")] public double Ixy;
        [DataMember (Name = "ixz")] public double Ixz;
        [DataMember (Name = "iyy")] public double Iyy;
        [DataMember (Name = "iyz")] public double Iyz;
        [DataMember (Name = "izz")] public double Izz;
    
        /// Constructor for empty message.
        public Inertia()
        {
        }
        
        /// Constructor with buffer.
        public Inertia(ref ReadBuffer b)
        {
            b.Deserialize(out M);
            b.Deserialize(out Com);
            b.Deserialize(out Ixx);
            b.Deserialize(out Ixy);
            b.Deserialize(out Ixz);
            b.Deserialize(out Iyy);
            b.Deserialize(out Iyz);
            b.Deserialize(out Izz);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Inertia(ref b);
        
        public Inertia RosDeserialize(ref ReadBuffer b) => new Inertia(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(M);
            b.Serialize(in Com);
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Inertia";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61STUsDMRC951c86EVhXaGKB6EnD9JDQbB4kSrT3dlt6CYpSWq7S3+8k137IXg0MDC8" +
                "vPcyHxlhRiHgfV0vVNU4ig/3MEqN8MQ2soerYHqCWaianeHo208T6nD7xkV0/g6F6/lTyz5qwpxtcD4Z" +
                "3piP8UJu0jlA7/cSrUSHQ+Jj0qOCtCkG9MjtekR3CT2WJQ4XeXuRd+e8vcDbC7zrlJr881Gz1+dH/DkV" +
                "aWW+0gGeN56DjDKA8NXfQVtUnhlhQwXnSLOIEK6zTQvDZCOiOytFWGovUu1sLq7suXKeM+iI0nGAdVE8" +
                "DK3FUobPSU2bjZgRoicbGkraBIvkivM6z7BbsR1Y2tZCFIeaZYW6gNe1LgelPGROYsJPcxliNcZON81Q" +
                "8/BYXLGYeBd7wXWOaYXWbbFLDUniUVKkZLTkU120bFK9LsM2Fd5b/B7oi9OiNxwC1SyzC5GpzNVps+c/" +
                "cd58p74BX6ix6tcCAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
