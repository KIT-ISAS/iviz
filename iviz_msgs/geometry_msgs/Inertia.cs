
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Inertia : IMessage 
    {
        // Mass [kg]
        public double m;
        
        // Center of mass [m]
        public geometry_msgs.Vector3 com;
        
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        public double ixx;
        public double ixy;
        public double ixz;
        public double iyy;
        public double iyz;
        public double izz;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Inertia";

        public IMessage Create() => new Inertia();

        public int GetLength() => 80;

        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out m, ref ptr, end);
            com.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out ixx, ref ptr, end);
            BuiltIns.Deserialize(out ixy, ref ptr, end);
            BuiltIns.Deserialize(out ixz, ref ptr, end);
            BuiltIns.Deserialize(out iyy, ref ptr, end);
            BuiltIns.Deserialize(out iyz, ref ptr, end);
            BuiltIns.Deserialize(out izz, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(m, ref ptr, end);
            com.Serialize(ref ptr, end);
            BuiltIns.Serialize(ixx, ref ptr, end);
            BuiltIns.Serialize(ixy, ref ptr, end);
            BuiltIns.Serialize(ixz, ref ptr, end);
            BuiltIns.Serialize(iyy, ref ptr, end);
            BuiltIns.Serialize(iyz, ref ptr, end);
            BuiltIns.Serialize(izz, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE61STUsDMRC951c86EVhXaGKB6EnD9JDQbB4kSrT3dlt6CYpSWq7S3+8k137IXg0MDC8" +
            "vPcyHxlhRiHgfV0vVNU4ig/3MEqN8MQ2soerYHqCWaianeHo208T6nD7xkV0/g6F6/lTyz5qwpxtcD4Z" +
            "3piP8UJu0jlA7/cSrUSHQ+Jj0qOCtCkG9MjtekR3CT2WJQ4XeXuRd+e8vcDbC7zrlJr881Gz1+dH/DkV" +
            "aWW+0gGeN56DjDKA8NXfQVtUnhlhQwXnSLOIEK6zTQvDZCOiOytFWGovUu1sLq7suXKeM+iI0nGAdVE8" +
            "DK3FUobPSU2bjZgRoicbGkraBIvkivM6z7BbsR1Y2tZCFIeaZYW6gNe1LgelPGROYsJPcxliNcZON81Q" +
            "8/BYXLGYeBd7wXWOaYXWbbFLDUniUVKkZLTkU120bFK9LsM2Fd5b/B7oi9OiNxwC1SyzC5GpzNVps+c/" +
            "cd58p74BX6ix6tcCAAA=";

    }
}
