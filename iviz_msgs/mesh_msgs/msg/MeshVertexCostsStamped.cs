/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshVertexCostsStamped")]
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
            Header = new StdMsgs.Header();
            Uuid = "";
            Type = "";
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexCostsStamped(StdMsgs.Header Header, string Uuid, string Type, MeshMsgs.MeshVertexCosts MeshVertexCosts)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACrVTTWvcMBC9G/wfBnJIUtgEmttCD6WhbQ6BQEIupSyz0qwtsCVXGm3if9833nYTCoEe" +
                "WmMjZjzvzZsPndCtlJ4+quawrSpmFu6kbYr6zVi6cvlV2EumfjnMn0PsqNbgj4bOExAjmA4Q43yUrPL8" +
                "KRUttPzZL46NM0/btM2Hf/y0ze39lzX9obttTuheOXrOHjqUPSvTLqGg0PWSV4PsZQCKx0k8LX+tnHJh" +
                "yIc+FMLbSZTMwzBTLYjSRC6NY43BMXqmAQW+JjBoiMQ0cdbg6sAZgJR9iBa/yzzKwm9fkR9VohO6uV4j" +
                "KhZxVQNEzeBwWbhYi2+uEVxD1Kv3hgDw4SmtYEuH4RwVkPaspliep4xRQhGXtaV5d6jxAvRokiCRL3S2" +
                "+DYwyzkhD1TIlFxPZ5B/N2ufIhiF9pwDbwcxZoc+gPbUQKfnr6lN+poix/Sb/0D5kuRveOMLsZW16jG8" +
                "wVpQaoc+InLKaR88YrfzwuKGIFFpCNvMeW4bgx2SguSzNRthwC2zwcmlJBcwCU9PQfvjGi9z2dhe/7ft" +
                "fPOOWLlvXcXdkBhz//Yd6/Hr8vwEekYfdLgDAAA=";
                
    }
}
