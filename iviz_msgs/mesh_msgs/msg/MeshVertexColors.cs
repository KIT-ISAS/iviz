using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshVertexColors : IMessage
    {
        // Mesh Attribute Message
        public std_msgs.ColorRGBA[] vertex_colors { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexColors()
        {
            vertex_colors = System.Array.Empty<std_msgs.ColorRGBA>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexColors(std_msgs.ColorRGBA[] vertex_colors)
        {
            this.vertex_colors = vertex_colors ?? throw new System.ArgumentNullException(nameof(vertex_colors));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshVertexColors(Buffer b)
        {
            this.vertex_colors = BuiltIns.DeserializeStructArray<std_msgs.ColorRGBA>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshVertexColors(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.SerializeStructArray(this.vertex_colors, b, 0);
        }
        
        public void Validate()
        {
            if (vertex_colors is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 16 * vertex_colors.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshVertexColors";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "2af51ba6de42b829b6f716360dfdf4d9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O5iktS4nOL04v1nfNz8ouC3J0co2MVylKLSlIr" +
                "4pNBQsVcXLZUBly+we5WCpg2c6Xl5CeWGBspFMFZ6XBWEpyVyMUFAF0TsDnPAAAA";
                
    }
}
