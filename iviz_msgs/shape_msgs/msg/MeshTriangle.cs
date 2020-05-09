using System.Runtime.Serialization;

namespace Iviz.Msgs.shape_msgs
{
    public sealed class MeshTriangle : IMessage
    {
        // Definition of a triangle's vertices
        public uint[/*3*/] vertex_indices { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshTriangle()
        {
            vertex_indices = new uint[3];
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshTriangle(uint[] vertex_indices)
        {
            BuiltIns.AssertSize(vertex_indices, 3);
            this.vertex_indices = vertex_indices;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshTriangle(Buffer b)
        {
            this.vertex_indices = BuiltIns.DeserializeStructArray<uint>(b, 3);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshTriangle(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.vertex_indices, b, 3);
        }
        
        public void Validate()
        {
            BuiltIns.AssertSize(vertex_indices, 3);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 12;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "shape_msgs/MeshTriangle";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
    }
}
