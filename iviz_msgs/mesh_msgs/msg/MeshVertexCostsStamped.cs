using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshVertexCostsStamped : IMessage
    {
        // Mesh Attribute Message
        public std_msgs.Header header { get; set; }
        public string uuid { get; set; }
        public string type { get; set; }
        public mesh_msgs.MeshVertexCosts mesh_vertex_costs { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexCostsStamped()
        {
            header = new std_msgs.Header();
            uuid = "";
            type = "";
            mesh_vertex_costs = new mesh_msgs.MeshVertexCosts();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexCostsStamped(std_msgs.Header header, string uuid, string type, mesh_msgs.MeshVertexCosts mesh_vertex_costs)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
            this.type = type ?? throw new System.ArgumentNullException(nameof(type));
            this.mesh_vertex_costs = mesh_vertex_costs ?? throw new System.ArgumentNullException(nameof(mesh_vertex_costs));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshVertexCostsStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.uuid = b.DeserializeString();
            this.type = b.DeserializeString();
            this.mesh_vertex_costs = new mesh_msgs.MeshVertexCosts(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshVertexCostsStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.Serialize(this.uuid);
            b.Serialize(this.type);
            this.mesh_vertex_costs.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (uuid is null) throw new System.NullReferenceException();
            if (type is null) throw new System.NullReferenceException();
            if (mesh_vertex_costs is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(uuid);
                size += BuiltIns.UTF8.GetByteCount(type);
                size += mesh_vertex_costs.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshVertexCostsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "f65d52b48920bc9c2a071d643ab7b6b3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
