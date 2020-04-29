using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshVertexCostsStamped : IMessage
    {
        // Mesh Attribute Message
        public std_msgs.Header header;
        public string uuid;
        public string type;
        public mesh_msgs.MeshVertexCosts mesh_vertex_costs;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexCostsStamped()
        {
            header = new std_msgs.Header();
            uuid = "";
            type = "";
            mesh_vertex_costs = new mesh_msgs.MeshVertexCosts();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out uuid, ref ptr, end);
            BuiltIns.Deserialize(out type, ref ptr, end);
            mesh_vertex_costs.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(uuid, ref ptr, end);
            BuiltIns.Serialize(type, ref ptr, end);
            mesh_vertex_costs.Serialize(ref ptr, end);
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
    
        public IMessage Create() => new MeshVertexCostsStamped();
    
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
