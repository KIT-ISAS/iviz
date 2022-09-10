/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshMaterials : IHasSerializer<MeshMaterials>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "clusters")] public MeshMsgs.MeshFaceCluster[] Clusters;
        [DataMember (Name = "materials")] public MeshMsgs.MeshMaterial[] Materials;
        [DataMember (Name = "cluster_materials")] public uint[] ClusterMaterials;
        [DataMember (Name = "vertex_tex_coords")] public MeshMsgs.MeshVertexTexCoords[] VertexTexCoords;
    
        public MeshMaterials()
        {
            Clusters = EmptyArray<MeshMsgs.MeshFaceCluster>.Value;
            Materials = EmptyArray<MeshMsgs.MeshMaterial>.Value;
            ClusterMaterials = EmptyArray<uint>.Value;
            VertexTexCoords = EmptyArray<MeshMsgs.MeshVertexTexCoords>.Value;
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
                var array = n == 0
                    ? EmptyArray<MeshMsgs.MeshFaceCluster>.Value
                    : new MeshMsgs.MeshFaceCluster[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.MeshFaceCluster(ref b);
                }
                Clusters = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshMsgs.MeshMaterial>.Value
                    : new MeshMsgs.MeshMaterial[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.MeshMaterial(ref b);
                }
                Materials = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<uint>.Value
                    : new uint[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                ClusterMaterials = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshMsgs.MeshVertexTexCoords>.Value
                    : new MeshMsgs.MeshVertexTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.MeshVertexTexCoords(ref b);
                }
                VertexTexCoords = array;
            }
        }
        
        public MeshMaterials(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshMsgs.MeshFaceCluster>.Value
                    : new MeshMsgs.MeshFaceCluster[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.MeshFaceCluster(ref b);
                }
                Clusters = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshMsgs.MeshMaterial>.Value
                    : new MeshMsgs.MeshMaterial[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.MeshMaterial(ref b);
                }
                Materials = array;
            }
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<uint>.Value
                    : new uint[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                ClusterMaterials = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshMsgs.MeshVertexTexCoords>.Value
                    : new MeshMsgs.MeshVertexTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.MeshVertexTexCoords(ref b);
                }
                VertexTexCoords = array;
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
            get
            {
                int size = 16;
                foreach (var msg in Clusters) size += msg.RosMessageLength;
                size += 21 * Materials.Length;
                size += 4 * ClusterMaterials.Length;
                size += 8 * VertexTexCoords.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Clusters.Length
            foreach (var msg in Clusters) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Materials.Length
            size += (21 + 3) * Materials.Length - 3;
            size = WriteBuffer2.Align4(size);
            size += 4; // ClusterMaterials.Length
            size += 4 * ClusterMaterials.Length;
            size += 4; // VertexTexCoords.Length
            size += 8 * VertexTexCoords.Length;
            return size;
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
    
        public Serializer<MeshMaterials> CreateSerializer() => new Serializer();
        public Deserializer<MeshMaterials> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshMaterials>
        {
            public override void RosSerialize(MeshMaterials msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshMaterials msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshMaterials msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshMaterials msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshMaterials msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshMaterials>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshMaterials msg) => msg = new MeshMaterials(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshMaterials msg) => msg = new MeshMaterials(ref b);
        }
    }
}
