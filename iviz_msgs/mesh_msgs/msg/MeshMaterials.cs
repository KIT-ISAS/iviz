/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshMaterials : IDeserializable<MeshMaterials>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "clusters")] public MeshMsgs.MeshFaceCluster[] Clusters;
        [DataMember (Name = "materials")] public MeshMsgs.MeshMaterial[] Materials;
        [DataMember (Name = "cluster_materials")] public uint[] ClusterMaterials;
        [DataMember (Name = "vertex_tex_coords")] public MeshMsgs.MeshVertexTexCoords[] VertexTexCoords;
    
        /// Constructor for empty message.
        public MeshMaterials()
        {
            Clusters = System.Array.Empty<MeshMsgs.MeshFaceCluster>();
            Materials = System.Array.Empty<MeshMsgs.MeshMaterial>();
            ClusterMaterials = System.Array.Empty<uint>();
            VertexTexCoords = System.Array.Empty<MeshMsgs.MeshVertexTexCoords>();
        }
        
        /// Explicit constructor.
        public MeshMaterials(MeshMsgs.MeshFaceCluster[] Clusters, MeshMsgs.MeshMaterial[] Materials, uint[] ClusterMaterials, MeshMsgs.MeshVertexTexCoords[] VertexTexCoords)
        {
            this.Clusters = Clusters;
            this.Materials = Materials;
            this.ClusterMaterials = ClusterMaterials;
            this.VertexTexCoords = VertexTexCoords;
        }
        
        /// Constructor with buffer.
        public MeshMaterials(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshMaterials(ref b);
        
        public MeshMaterials RosDeserialize(ref ReadBuffer b) => new MeshMaterials(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Clusters);
            b.SerializeArray(Materials);
            b.SerializeStructArray(ClusterMaterials);
            b.SerializeArray(VertexTexCoords);
        }
        
        public void RosValidate()
        {
            if (Clusters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Clusters.Length; i++)
            {
                if (Clusters[i] is null) BuiltIns.ThrowNullReference($"{nameof(Clusters)}[{i}]");
                Clusters[i].RosValidate();
            }
            if (Materials is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Materials.Length; i++)
            {
                if (Materials[i] is null) BuiltIns.ThrowNullReference($"{nameof(Materials)}[{i}]");
                Materials[i].RosValidate();
            }
            if (ClusterMaterials is null) BuiltIns.ThrowNullReference();
            if (VertexTexCoords is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < VertexTexCoords.Length; i++)
            {
                if (VertexTexCoords[i] is null) BuiltIns.ThrowNullReference($"{nameof(VertexTexCoords)}[{i}]");
                VertexTexCoords[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetArraySize(Clusters);
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
