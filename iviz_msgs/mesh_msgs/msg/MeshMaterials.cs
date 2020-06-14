using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshMaterials")]
    public sealed class MeshMaterials : IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "clusters")] public MeshMsgs.MeshFaceCluster[] Clusters { get; set; }
        [DataMember (Name = "materials")] public MeshMsgs.MeshMaterial[] Materials { get; set; }
        [DataMember (Name = "cluster_materials")] public uint[] ClusterMaterials { get; set; }
        [DataMember (Name = "vertex_tex_coords")] public MeshMsgs.MeshVertexTexCoords[] VertexTexCoords { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshMaterials()
        {
            Clusters = System.Array.Empty<MeshMsgs.MeshFaceCluster>();
            Materials = System.Array.Empty<MeshMsgs.MeshMaterial>();
            ClusterMaterials = System.Array.Empty<uint>();
            VertexTexCoords = System.Array.Empty<MeshMsgs.MeshVertexTexCoords>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshMaterials(MeshMsgs.MeshFaceCluster[] Clusters, MeshMsgs.MeshMaterial[] Materials, uint[] ClusterMaterials, MeshMsgs.MeshVertexTexCoords[] VertexTexCoords)
        {
            this.Clusters = Clusters;
            this.Materials = Materials;
            this.ClusterMaterials = ClusterMaterials;
            this.VertexTexCoords = VertexTexCoords;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshMaterials(Buffer b)
        {
            Clusters = b.DeserializeArray<MeshMsgs.MeshFaceCluster>();
            for (int i = 0; i < this.Clusters.Length; i++)
            {
                Clusters[i] = new MeshMsgs.MeshFaceCluster(b);
            }
            Materials = b.DeserializeStructArray<MeshMsgs.MeshMaterial>();
            ClusterMaterials = b.DeserializeStructArray<uint>();
            VertexTexCoords = b.DeserializeArray<MeshMsgs.MeshVertexTexCoords>();
            for (int i = 0; i < this.VertexTexCoords.Length; i++)
            {
                VertexTexCoords[i] = new MeshMsgs.MeshVertexTexCoords(b);
            }
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new MeshMaterials(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(Clusters, 0);
            b.SerializeStructArray(Materials, 0);
            b.SerializeStructArray(ClusterMaterials, 0);
            b.SerializeArray(VertexTexCoords, 0);
        }
        
        public void RosValidate()
        {
            if (Clusters is null) throw new System.NullReferenceException();
            for (int i = 0; i < Clusters.Length; i++)
            {
                if (Clusters[i] is null) throw new System.NullReferenceException();
                Clusters[i].RosValidate();
            }
            if (Materials is null) throw new System.NullReferenceException();
            if (ClusterMaterials is null) throw new System.NullReferenceException();
            if (VertexTexCoords is null) throw new System.NullReferenceException();
            for (int i = 0; i < VertexTexCoords.Length; i++)
            {
                if (VertexTexCoords[i] is null) throw new System.NullReferenceException();
                VertexTexCoords[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                for (int i = 0; i < Clusters.Length; i++)
                {
                    size += Clusters[i].RosMessageLength;
                }
                size += 21 * Materials.Length;
                size += 4 * ClusterMaterials.Length;
                size += 8 * VertexTexCoords.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
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
