/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshVertexCostsStamped : IDeserializable<MeshVertexCostsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "mesh_vertex_costs")] public MeshMsgs.MeshVertexCosts MeshVertexCosts;
    
        /// Constructor for empty message.
        public MeshVertexCostsStamped()
        {
            Uuid = string.Empty;
            Type = string.Empty;
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts();
        }
        
        /// Explicit constructor.
        public MeshVertexCostsStamped(in StdMsgs.Header Header, string Uuid, string Type, MeshMsgs.MeshVertexCosts MeshVertexCosts)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Type = Type;
            this.MeshVertexCosts = MeshVertexCosts;
        }
        
        /// Constructor with buffer.
        internal MeshVertexCostsStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Uuid = b.DeserializeString();
            Type = b.DeserializeString();
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshVertexCostsStamped(ref b);
        
        MeshVertexCostsStamped IDeserializable<MeshVertexCostsStamped>.RosDeserialize(ref Buffer b) => new MeshVertexCostsStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            b.Serialize(Type);
            MeshVertexCosts.RosSerialize(ref b);
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
                size += BuiltIns.GetStringSize(Uuid);
                size += BuiltIns.GetStringSize(Type);
                size += MeshVertexCosts.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexCostsStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "f65d52b48920bc9c2a071d643ab7b6b3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTWsbMRC961cM+JCk4ATam6GH0tAmh0AhoZdSzFgarwRaaSuNnOy/79Map6EQ6CFd" +
                "BFqN3nvzqRXdSfX0SbWEXVPpx8qDmKpuO9ahXt0IOynklw3mEtJArQV3+td5EjNC5Yjvet+lqDx9zlUr" +
                "LTeHxbC13WLMxzf+zN391w39FbFZ0b1yclwcYlB2rEz7jEzC4KWsoxwkgsTjJI6W255JvQTxwYdKWIMk" +
                "KRzjTK0CpJlsHseWgmWUSgNye8kHMyRimrhosC1yAT4XF1KH7wuP0tWxqvxqkqzQ7fUGmFTFNg0IaIaC" +
                "LcK1V/b2mkwLST+87wSzenjMaxxlQD+enZN61h6sPE0FzUMwXDfw8e6Y3CW0URyBF1fpfLFtcawXBCcI" +
                "QaZsPZ0j8m+z+pwgKHTgEngXpQtbVACqZ510dvFCuYe9ocQpn+SPin98/ItsetbtOa09ehZ79rUNKCCA" +
                "U8mH4ADdzYuIjUGSUgy7wmU2nXV0aVZfeo0BAmvpCHauNduABjh6DOpPU7t0Y4sp/k/T+OqDQJ6vPLl9" +
                "zIxe//iJiVjeyW9TU88JnQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
