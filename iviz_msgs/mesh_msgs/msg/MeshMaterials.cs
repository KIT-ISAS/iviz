using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshMaterials : IMessage
    {
        // Mesh Attribute Message
        public mesh_msgs.MeshFaceCluster[] clusters;
        public mesh_msgs.MeshMaterial[] materials;
        public uint[] cluster_materials;
        public mesh_msgs.MeshVertexTexCoords[] vertex_tex_coords;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshMaterials()
        {
            clusters = System.Array.Empty<mesh_msgs.MeshFaceCluster>();
            materials = System.Array.Empty<mesh_msgs.MeshMaterial>();
            cluster_materials = System.Array.Empty<uint>();
            vertex_tex_coords = System.Array.Empty<mesh_msgs.MeshVertexTexCoords>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out clusters, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out materials, ref ptr, end, 0);
            BuiltIns.Deserialize(out cluster_materials, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out vertex_tex_coords, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(clusters, ref ptr, end, 0);
            BuiltIns.SerializeArray(materials, ref ptr, end, 0);
            BuiltIns.Serialize(cluster_materials, ref ptr, end, 0);
            BuiltIns.SerializeArray(vertex_tex_coords, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 16;
                for (int i = 0; i < clusters.Length; i++)
                {
                    size += clusters[i].RosMessageLength;
                }
                size += 21 * materials.Length;
                size += 4 * cluster_materials.Length;
                size += 8 * vertex_tex_coords.Length;
                return size;
            }
        }
    
        public IMessage Create() => new MeshMaterials();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshMaterials";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "e151c9a065aae90d545559129a79a70a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72TQWrDMBBF93MKgQ8QaHeFLBJDs/KmCdmEIsbyxBHIVtBIwb19JMdRqEt3bQSCr9Ef" +
                "ePORClERn8TKe6fr4CkdGVuCLpZlxy0vkuEdFZUmsCd3+BTqpnhmqjAWNZro6CbJEHTvX18eTfJx9b17" +
                "T87TsKOhtNY1HDsuY0WmrcYawPKPF1TbzZv4dVYo7iKPcYy3UveNVsRQ2LPXtkcDHPPrW2GwJvMczHva" +
                "E5qIMfngRjYaIk9zs5bWWPexWa+ESgpqa404IcvJ/1+wPwHgaCwmUpdVm1WdFT4nvtlzg2L+EXZfZ8pQ" +
                "IasLwBX0cFhyNAMAAA==";
                
    }
}
