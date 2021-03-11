/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshVertexCostsStamped")]
    public sealed class MeshVertexCostsStamped : IDeserializable<MeshVertexCostsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "type")] public string Type { get; set; }
        [DataMember (Name = "mesh_vertex_costs")] public MeshMsgs.MeshVertexCosts MeshVertexCosts { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexCostsStamped()
        {
            Uuid = string.Empty;
            Type = string.Empty;
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexCostsStamped(in StdMsgs.Header Header, string Uuid, string Type, MeshMsgs.MeshVertexCosts MeshVertexCosts)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Type = Type;
            this.MeshVertexCosts = MeshVertexCosts;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshVertexCostsStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            Type = b.DeserializeString();
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshVertexCostsStamped(ref b);
        }
        
        MeshVertexCostsStamped IDeserializable<MeshVertexCostsStamped>.RosDeserialize(ref Buffer b)
        {
            return new MeshVertexCostsStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            b.Serialize(Type);
            MeshVertexCosts.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
            if (MeshVertexCosts is null) throw new System.NullReferenceException(nameof(MeshVertexCosts));
            MeshVertexCosts.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                size += BuiltIns.UTF8.GetByteCount(Type);
                size += MeshVertexCosts.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexCostsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f65d52b48920bc9c2a071d643ab7b6b3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTWvcMBC9C/Y/DOwhSWFTaG8LPZSGpDkECgm9lLLMSrO2QJZcabSJ/32fbDYNhUAO" +
                "rRHIGs178+ZDa7qT0tNn1ez3VaUdC3diirrdULry/quwk0z9vMGcfeyoVu9O/zqNYgawLP6N77tklacv" +
                "qWih+eY4G3a2WczKfPrH38rc3d9s6S/NK7Ome+XoODvIUHasTIeEZHzXS94EOUoAiodRHM23LZlyCeBD" +
                "7wthdRIlcwgT1QInTWTTMNToLaNa6pHeSzyQPhLTyFm9rYEz/FN2Pjb3Q+ZBGjtWkV9VohW6vdrCJxax" +
                "VT0ETWCwWbi04t5ekak+6scPDWDWD49pg6N0aMlzcNKetYmVpzGjfxDDZYsY75bkLsGN6giiuELns22H" +
                "Y7kgBIEEGZPt6RzKv03apwhCoSNnz/sgjdiiAmA9a6CzixfMTfaWIsd0ol8Y/8R4C2185m05bXr0LLTs" +
                "S+1QQDiOOR29g+t+mkls8BKVgt9nzpNpqCWkWV+3GsMJqLkj2LmUZD0a4OjRa38a3LkbOwzyfxvIV19F" +
                "G81XHt4hJEa7f/zEUCyv5Tc/o73epAMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
