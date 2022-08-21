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
            {
                int n = b.DeserializeArrayLength();
                Clusters = n == 0
                    ? System.Array.Empty<MeshMsgs.MeshFaceCluster>()
                    : new MeshMsgs.MeshFaceCluster[n];
                for (int i = 0; i < n; i++)
                {
                    Clusters[i] = new MeshMsgs.MeshFaceCluster(ref b);
                }
            }
            {
                int n = b.DeserializeArrayLength();
                Materials = n == 0
                    ? System.Array.Empty<MeshMsgs.MeshMaterial>()
                    : new MeshMsgs.MeshMaterial[n];
                for (int i = 0; i < n; i++)
                {
                    Materials[i] = new MeshMsgs.MeshMaterial(ref b);
                }
            }
            b.DeserializeStructArray(out ClusterMaterials);
            {
                int n = b.DeserializeArrayLength();
                VertexTexCoords = n == 0
                    ? System.Array.Empty<MeshMsgs.MeshVertexTexCoords>()
                    : new MeshMsgs.MeshVertexTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    VertexTexCoords[i] = new MeshMsgs.MeshVertexTexCoords(ref b);
                }
            }
        }
        
        public MeshMaterials(ref ReadBuffer2 b)
        {
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Clusters = n == 0
                    ? System.Array.Empty<MeshMsgs.MeshFaceCluster>()
                    : new MeshMsgs.MeshFaceCluster[n];
                for (int i = 0; i < n; i++)
                {
                    Clusters[i] = new MeshMsgs.MeshFaceCluster(ref b);
                }
            }
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Materials = n == 0
                    ? System.Array.Empty<MeshMsgs.MeshMaterial>()
                    : new MeshMsgs.MeshMaterial[n];
                for (int i = 0; i < n; i++)
                {
                    Materials[i] = new MeshMsgs.MeshMaterial(ref b);
                }
            }
            b.Align4();
            b.DeserializeStructArray(out ClusterMaterials);
            {
                int n = b.DeserializeArrayLength();
                VertexTexCoords = n == 0
                    ? System.Array.Empty<MeshMsgs.MeshVertexTexCoords>()
                    : new MeshMsgs.MeshVertexTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    VertexTexCoords[i] = new MeshMsgs.MeshVertexTexCoords(ref b);
                }
            }
        }
        
        public MeshMaterials RosDeserialize(ref ReadBuffer b) => new MeshMaterials(ref b);
        
        public MeshMaterials RosDeserialize(ref ReadBuffer2 b) => new MeshMaterials(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Clusters.Length);
            foreach (var t in Clusters)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Materials.Length);
            foreach (var t in Materials)
            {
                t.RosSerialize(ref b);
            }
            b.SerializeStructArray(ClusterMaterials);
            b.Serialize(VertexTexCoords.Length);
            foreach (var t in VertexTexCoords)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Clusters.Length);
            foreach (var t in Clusters)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Materials.Length);
            foreach (var t in Materials)
            {
                t.RosSerialize(ref b);
            }
            b.SerializeStructArray(ClusterMaterials);
            b.Serialize(VertexTexCoords.Length);
            foreach (var t in VertexTexCoords)
            {
                t.RosSerialize(ref b);
            }
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Clusters.Length
            foreach (var t in Clusters)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align4(c);
            c += 4; // Materials length
            c += (21 + 3) * Materials.Length - 3;
            c = WriteBuffer2.Align4(c);
            c += 4; // ClusterMaterials length
            c += 4 * ClusterMaterials.Length;
            c += 4; // VertexTexCoords length
            c += 8 * VertexTexCoords.Length;
            return c;
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
