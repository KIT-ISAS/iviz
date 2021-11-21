/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/TriangleMeshStamped")]
    public sealed class TriangleMeshStamped : IDeserializable<TriangleMeshStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "mesh")] public MeshMsgs.TriangleMesh Mesh;
    
        /// <summary> Constructor for empty message. </summary>
        public TriangleMeshStamped()
        {
            Mesh = new MeshMsgs.TriangleMesh();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TriangleMeshStamped(in StdMsgs.Header Header, MeshMsgs.TriangleMesh Mesh)
        {
            this.Header = Header;
            this.Mesh = Mesh;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TriangleMeshStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Mesh = new MeshMsgs.TriangleMesh(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TriangleMeshStamped(ref b);
        }
        
        TriangleMeshStamped IDeserializable<TriangleMeshStamped>.RosDeserialize(ref Buffer b)
        {
            return new TriangleMeshStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Mesh.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Mesh is null) throw new System.NullReferenceException(nameof(Mesh));
            Mesh.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Mesh.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleMeshStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3e766dd12107291d682eb5e6c7442b9d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X32/bNhB+F+D/gYAfkrSxU6zDUGQY9iNdujwEKNa8FYNBSyeJG0WqJBVb+ev3HSXK" +
                "dppkfVhiOLBE3h2Pd9/dffGhWDW+8md/kCzIiTr+ZA35eli/cUqaStM1VgQvZ7Psp//5M8uuP304F/7Q" +
                "l1k2F5+CNIV0BU4OspBBitLCSVXV5BaabklDSzYtFSLuhr4lv4TiTa28wLciQ05q3YvOQyhYkdum6YzK" +
                "ZSARFG60rw9NZYQUrXRB5Z2WDvLWFcqweOlkQ2wdX09fOjI5iav355AxnvIuKDjUw0LuSHplKmyKrFMm" +
                "vP2OFbL5zcYu8EoVQj0dLkItAztL29aRZz+lP8cZr4bLLWEb0SGcUnhxHNdWePUnAofABWptXotjeP6x" +
                "D7U1MEjiViJza01sOEcEYPWIlY5O9iyz2+fCSGOT+cHi7oxvMWsmu3ynRY2cab697yoEEIKts7eqgOi6" +
                "j0ZyrcgEodXaSddnrDUcmc0vOcYQglbMCH6l9zZXSEAhNirUmQ+OrcdsrFTxfIB8uAqAy7l4T6UyKihE" +
                "xZYATBgFhhLJkvyVKVRO/vNfk4AXc9zbB1ab1n6MUVGmoC0CrDtIOSoZI1a01sdzAGcjbolxie1jxDi+" +
                "0XZlrGuk9qdClaICBM1JVpFFxbh+8P6jBebgw6Q9j+fJPHRS71YjDBv5D4mOMTneZW5bPl7q8yes7rzI" +
                "piq+sNq6Pz/89utOJuelR0RSMJLQk4fhL3SOZVGcfq9dcYKuARUY0xAvZU6rZnzHwWS8dYPkVSMr4nMH" +
                "U/eNXELzQnceqhDKhyf/klgbscNt8DG0Hfkpe2Ob+fx2ihHgFDeezeUHMjRLrRfNJEgF0DKOEoQH51sW" +
                "ZDCXjlD2LQKdldrK8MP3Yjs99dPT3QtMnAmIs+FY9Gs3PVXT03p6ki+BhH0wz9IcSdCP3eKBUkLo8ZSt" +
                "rdWilj6VyjMG8X5RTSDATTzed2CQRnQGAziNOcXikD5+cyrenMReH9DzWoz2MkDNYXgzaEa5LDtgKWL8" +
                "zMW4vJuovradxrjhJvelUyP84pyZzCX1g89kK02XPVPcCDHyhi22k+PXyccMWacqoBxyg8LXhnIMweGC" +
                "T1t6vU3KQ+1gKnBZwX4dCykOj6fu9Lo/NFDYjfk2xbtDRfzZOJS0NDEE/2nhapCZwgnihqRgeB2sjpYu" +
                "YhiuTGkfM5cgdY8T7PxgsJVa5eExCyy5plreKjBJIK5DHaG7UpGlCqspBnanMhgelk8TWzsVpmvWQ/qc" +
                "3aQGDHcK+POVdlx+UBn12jXGZ7FqSGiqeCgPLIDZLlimBQGthETkuG0q0Azv8rNoeJW2/TJvwZ844L3t" +
                "xEYOQPEjg1Z3CJowtBGJPvG8xnX+RmKh5qxfgCI7/wuzE7/0tnM5QaiipaEQU4ZKBzU1ghqpNJM6buzs" +
                "WLSbHFlmiaFNnqdQ/J4WcO1WbUl7sViIHITRgMk3JA02T1E5qMD45OH2w4nkTIKq8BSxTUxqYlXD4ZEu" +
                "gcvqrqCz/RZ1P2pga5y5d8jKaq3w/0KB2Tpmzu8R0Wnv54nRB2oPHLrstGYsIIemAgjgwboP42x+BxoR" +
                "De0pjAwMeXBqG3eHK/PJ4Pkw/ypi6+Slps0e60EXH58StRjpVOIVEzFM6dZyTRqe/gtjWSs6VA4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
