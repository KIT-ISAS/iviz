/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshMaterials : IDeserializable<MeshMaterials>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "clusters")] public MeshMsgs.MeshFaceCluster[] Clusters;
        [DataMember (Name = "materials")] public MeshMsgs.MeshMaterial[] Materials;
        [DataMember (Name = "cluster_materials")] public uint[] ClusterMaterials;
        [DataMember (Name = "vertex_tex_coords")] public MeshMsgs.MeshVertexTexCoords[] VertexTexCoords;
    
        public MeshMaterials()
        {
            Clusters = System.Array.Empty<MeshMsgs.MeshFaceCluster>();
            Materials = System.Array.Empty<MeshMsgs.MeshMaterial>();
            ClusterMaterials = System.Array.Empty<uint>();
            VertexTexCoords = System.Array.Empty<MeshMsgs.MeshVertexTexCoords>();
        }
        
        public MeshMaterials(MeshMsgs.MeshFaceCluster[] Clusters, MeshMsgs.MeshMaterial[] Materials, uint[] ClusterMaterials, MeshMsgs.MeshVertexTexCoords[] VertexTexCoords)
        {
            this.Clusters = Clusters;
            this.Materials = Materials;
            this.ClusterMaterials = ClusterMaterials;
            this.VertexTexCoords = VertexTexCoords;
        }
        
        public MeshMaterials(ref ReadBuffer b)
        {
            b.DeserializeArray(out Clusters);
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshMsgs.MeshFaceCluster(ref b);
            }
            b.DeserializeArray(out Materials);
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new MeshMsgs.MeshMaterial(ref b);
            }
            b.DeserializeStructArray(out ClusterMaterials);
            b.DeserializeArray(out VertexTexCoords);
            for (int i = 0; i < VertexTexCoords.Length; i++)
            {
                VertexTexCoords[i] = new MeshMsgs.MeshVertexTexCoords(ref b);
            }
        }
        
        public MeshMaterials(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Clusters);
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshMsgs.MeshFaceCluster(ref b);
            }
            b.DeserializeArray(out Materials);
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new MeshMsgs.MeshMaterial(ref b);
            }
            b.DeserializeStructArray(out ClusterMaterials);
            b.DeserializeArray(out VertexTexCoords);
            for (int i = 0; i < VertexTexCoords.Length; i++)
            {
                VertexTexCoords[i] = new MeshMsgs.MeshVertexTexCoords(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshMaterials(ref b);
        
        public MeshMaterials RosDeserialize(ref ReadBuffer b) => new MeshMaterials(ref b);
        
        public MeshMaterials RosDeserialize(ref ReadBuffer2 b) => new MeshMaterials(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Clusters);
            b.SerializeArray(Materials);
            b.SerializeStructArray(ClusterMaterials);
            b.SerializeArray(VertexTexCoords);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
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
                if (Clusters[i] is null) BuiltIns.ThrowNullReference(nameof(Clusters), i);
                Clusters[i].RosValidate();
            }
            if (Materials is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Materials.Length; i++)
            {
                if (Materials[i] is null) BuiltIns.ThrowNullReference(nameof(Materials), i);
                Materials[i].RosValidate();
            }
            if (ClusterMaterials is null) BuiltIns.ThrowNullReference();
            if (VertexTexCoords is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < VertexTexCoords.Length; i++)
            {
                if (VertexTexCoords[i] is null) BuiltIns.ThrowNullReference(nameof(VertexTexCoords), i);
                VertexTexCoords[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += WriteBuffer.GetArraySize(Clusters);
                size += 21 * Materials.Length;
                size += 4 * ClusterMaterials.Length;
                size += 8 * VertexTexCoords.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Clusters);
            WriteBuffer2.AddLength(ref c, Materials);
            WriteBuffer2.AddLength(ref c, ClusterMaterials);
            WriteBuffer2.AddLength(ref c, VertexTexCoords);
        }
    
        public const string MessageType = "mesh_msgs/MeshMaterials";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "e151c9a065aae90d545559129a79a70a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE72TQWrDMBBF93MKgQ8QaHeFLBJDs/KmCdmEIsbyxBHIVtBIwb19JMdRqEt3bQSCr9Ef" +
                "ePORClERn8TKe6fr4CkdGVuCLpZlxy0vkuEdFZUmsCd3+BTqpnhmqjAWNZro6CbJEHTvX18eTfJx9b17" +
                "T87TsKOhtNY1HDsuY0WmrcYawPKPF1TbzZv4dVYo7iKPcYy3UveNVsRQ2LPXtkcDHPPrW2GwJvMczHva" +
                "E5qIMfngRjYaIk9zs5bWWPexWa+ESgpqa404IcvJ/1+wPwHgaCwmUpdVm1WdFT4nvtlzg2L+EXZfZ8pQ" +
                "IasLwBX0cFhyNAMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
