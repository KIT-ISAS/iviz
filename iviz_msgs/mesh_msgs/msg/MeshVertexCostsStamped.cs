using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshVertexCostsStamped")]
    public sealed class MeshVertexCostsStamped : IMessage
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
        internal MeshVertexCostsStamped(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Uuid = b.DeserializeString();
            Type = b.DeserializeString();
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshVertexCostsStamped(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Header);
            b.Serialize(this.Uuid);
            b.Serialize(this.Type);
            b.Serialize(MeshVertexCosts);
        }
        
        public void Validate()
        {
            if (Header is null) throw new System.NullReferenceException();
            Header.Validate();
            if (Uuid is null) throw new System.NullReferenceException();
            if (Type is null) throw new System.NullReferenceException();
            if (MeshVertexCosts is null) throw new System.NullReferenceException();
            MeshVertexCosts.Validate();
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
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexCostsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f65d52b48920bc9c2a071d643ab7b6b3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VTTWsbMRC961cM+JCk4ATam6GH0tAmh0AhoZdSzFgarwRaaSuNnOy/79O6TkMh0EO7" +
                "CLQavffmUyu6k+rpg2oJu6bSj5UHMVXddqxDvboRdlLILxvMJaSBWgvu9K/zJGaEyhHf9b5KUXn6mKtW" +
                "Wm4Oi2Fru8WY9//4M3f3nzf0R8RmRffKyXFxiEHZsTLtMzIJg5eyjnKQCBKPkzhabnsm9RLEBx8qYQ2S" +
                "pHCMM7UKkGayeRxbCpZRKg3I7SUfzJCIaeKiwbbIBfhcXEgdvi88SlfHqvKjSbJCt9cbYFIV2zQgoBkK" +
                "tgjXXtnbazItJH33thPM6uExr3GUAf14dk7qWXuw8jQVNA/BcN3Ax5tjcpfQRnEEXlyl88W2xbFeEJwg" +
                "BJmy9XSOyL/M6nOCoNCBS+BdlC5sUQGonnXS2cUL5bRIJ075JH9U/O3jb2TTs27Pae3Rs9izr21AAQGc" +
                "Sj4EB+huXkRsDJKUYtgVLrPprKNLs/rUawwQWEtHsHOt2QY0wNFjUH+a2qUbW0zxf5rGVx8E8nzlye1j" +
                "ZvT623f69U5+AlNTzwmdAwAA";
                
    }
}
