/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshMaterials")]
    public sealed class MeshMaterials : IDeserializable<MeshMaterials>, IMessage
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
        public MeshMaterials(ref Buffer b)
        {
            Clusters = b.DeserializeArray<MeshMsgs.MeshFaceCluster>();
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshMsgs.MeshFaceCluster(ref b);
            }
            Materials = b.DeserializeArray<MeshMsgs.MeshMaterial>();
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new MeshMsgs.MeshMaterial(ref b);
            }
            ClusterMaterials = b.DeserializeStructArray<uint>();
            VertexTexCoords = b.DeserializeArray<MeshMsgs.MeshVertexTexCoords>();
            for (int i = 0; i < VertexTexCoords.Length; i++)
            {
                VertexTexCoords[i] = new MeshMsgs.MeshVertexTexCoords(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshMaterials(ref b);
        }
        
        MeshMaterials IDeserializable<MeshMaterials>.RosDeserialize(ref Buffer b)
        {
            return new MeshMaterials(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Clusters, 0);
            b.SerializeArray(Materials, 0);
            b.SerializeStructArray(ClusterMaterials, 0);
            b.SerializeArray(VertexTexCoords, 0);
        }
        
        public void RosValidate()
        {
            if (Clusters is null) throw new System.NullReferenceException(nameof(Clusters));
            for (int i = 0; i < Clusters.Length; i++)
            {
                if (Clusters[i] is null) throw new System.NullReferenceException($"{nameof(Clusters)}[{i}]");
                Clusters[i].RosValidate();
            }
            if (Materials is null) throw new System.NullReferenceException(nameof(Materials));
            for (int i = 0; i < Materials.Length; i++)
            {
                if (Materials[i] is null) throw new System.NullReferenceException($"{nameof(Materials)}[{i}]");
                Materials[i].RosValidate();
            }
            if (ClusterMaterials is null) throw new System.NullReferenceException(nameof(ClusterMaterials));
            if (VertexTexCoords is null) throw new System.NullReferenceException(nameof(VertexTexCoords));
            for (int i = 0; i < VertexTexCoords.Length; i++)
            {
                if (VertexTexCoords[i] is null) throw new System.NullReferenceException($"{nameof(VertexTexCoords)}[{i}]");
                VertexTexCoords[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                foreach (var i in Clusters)
                {
                    size += i.RosMessageLength;
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
                "H4sIAAAAAAAACr2TQWrDMBBF9wLdQeADFNpdIYvU0Ky8aUM3oYixPHEEshU0UnBv35ETx61Ld20Egq/R" +
                "H3j6kgpVIR3UOsZg6xQxLwlalKLjuu6opbvseAaDpUsUMezelTkrWroq4KoFx5buItmTbB8f7uc2/WXv" +
                "e/8bhojDFofS+9AQt5zGis7TjDUppFj98ZCiet08ql9PLEVxVdfD7Hlf276xBhmq8MdofQ9OCuIo+1Y5" +
                "qNHdjHZKfgJUnFhMYSTEIUM1Z3PpnQ8vm6e1MllJUXvv1AFIXzr+EZl+QEixdx4yL5NMsp1lPUu4WZaL" +
                "V8iXu/wk248j5zSRpVmeMuQnfbUacVQDAAA=";
                
    }
}
