using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    [DataContract]
    public sealed class MeshMaterials : IMessage
    {
        // Mesh Attribute Message
        [DataMember] public mesh_msgs.MeshFaceCluster[] clusters { get; set; }
        [DataMember] public mesh_msgs.MeshMaterial[] materials { get; set; }
        [DataMember] public uint[] cluster_materials { get; set; }
        [DataMember] public mesh_msgs.MeshVertexTexCoords[] vertex_tex_coords { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshMaterials()
        {
            clusters = System.Array.Empty<mesh_msgs.MeshFaceCluster>();
            materials = System.Array.Empty<mesh_msgs.MeshMaterial>();
            cluster_materials = System.Array.Empty<uint>();
            vertex_tex_coords = System.Array.Empty<mesh_msgs.MeshVertexTexCoords>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshMaterials(mesh_msgs.MeshFaceCluster[] clusters, mesh_msgs.MeshMaterial[] materials, uint[] cluster_materials, mesh_msgs.MeshVertexTexCoords[] vertex_tex_coords)
        {
            this.clusters = clusters ?? throw new System.ArgumentNullException(nameof(clusters));
            this.materials = materials ?? throw new System.ArgumentNullException(nameof(materials));
            this.cluster_materials = cluster_materials ?? throw new System.ArgumentNullException(nameof(cluster_materials));
            this.vertex_tex_coords = vertex_tex_coords ?? throw new System.ArgumentNullException(nameof(vertex_tex_coords));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshMaterials(Buffer b)
        {
            this.clusters = b.DeserializeArray<mesh_msgs.MeshFaceCluster>();
            for (int i = 0; i < this.clusters.Length; i++)
            {
                this.clusters[i] = new mesh_msgs.MeshFaceCluster(b);
            }
            this.materials = b.DeserializeArray<mesh_msgs.MeshMaterial>();
            for (int i = 0; i < this.materials.Length; i++)
            {
                this.materials[i] = new mesh_msgs.MeshMaterial(b);
            }
            this.cluster_materials = b.DeserializeStructArray<uint>();
            this.vertex_tex_coords = b.DeserializeArray<mesh_msgs.MeshVertexTexCoords>();
            for (int i = 0; i < this.vertex_tex_coords.Length; i++)
            {
                this.vertex_tex_coords[i] = new mesh_msgs.MeshVertexTexCoords(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshMaterials(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.clusters, 0);
            b.SerializeArray(this.materials, 0);
            b.SerializeStructArray(this.cluster_materials, 0);
            b.SerializeArray(this.vertex_tex_coords, 0);
        }
        
        public void Validate()
        {
            if (clusters is null) throw new System.NullReferenceException();
            if (materials is null) throw new System.NullReferenceException();
            if (cluster_materials is null) throw new System.NullReferenceException();
            if (vertex_tex_coords is null) throw new System.NullReferenceException();
        }
    
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
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshMaterials";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e151c9a065aae90d545559129a79a70a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72TQWrDMBBF93MKgQ8QaHeFLBJDs/KmCdmEIsbyxBHIVtBIwb19JMdRqEt3bQSCr9Ef" +
                "ePORClERn8TKe6fr4CkdGVuCLpZlxy0vkuEdFZUmsCd3+BTqpnhmqjAWNZro6CbJEHTvX18eTfJx9b17" +
                "T87TsKOhtNY1HDsuY0WmrcYawPKPF1TbzZv4dVYo7iKPcYy3UveNVsRQ2LPXtkcDHPPrW2GwJvMczHva" +
                "E5qIMfngRjYaIk9zs5bWWPexWa+ESgpqa404IcvJ/1+wPwHgaCwmUpdVm1WdFT4nvtlzg2L+EXZfZ8pQ" +
                "IasLwBX0cFhyNAMAAA==";
                
    }
}
