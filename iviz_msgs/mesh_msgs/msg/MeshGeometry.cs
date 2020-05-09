using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshGeometry : IMessage
    {
        // Mesh Geometry Message
        public geometry_msgs.Point[] vertices { get; set; }
        public geometry_msgs.Point[] vertex_normals { get; set; }
        public mesh_msgs.TriangleIndices[] faces { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshGeometry()
        {
            vertices = System.Array.Empty<geometry_msgs.Point>();
            vertex_normals = System.Array.Empty<geometry_msgs.Point>();
            faces = System.Array.Empty<mesh_msgs.TriangleIndices>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshGeometry(geometry_msgs.Point[] vertices, geometry_msgs.Point[] vertex_normals, mesh_msgs.TriangleIndices[] faces)
        {
            this.vertices = vertices ?? throw new System.ArgumentNullException(nameof(vertices));
            this.vertex_normals = vertex_normals ?? throw new System.ArgumentNullException(nameof(vertex_normals));
            this.faces = faces ?? throw new System.ArgumentNullException(nameof(faces));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshGeometry(Buffer b)
        {
            this.vertices = BuiltIns.DeserializeStructArray<geometry_msgs.Point>(b, 0);
            this.vertex_normals = BuiltIns.DeserializeStructArray<geometry_msgs.Point>(b, 0);
            this.faces = BuiltIns.DeserializeArray<mesh_msgs.TriangleIndices>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshGeometry(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.SerializeStructArray(this.vertices, b, 0);
            BuiltIns.SerializeStructArray(this.vertex_normals, b, 0);
            BuiltIns.SerializeArray(this.faces, b, 0);
        }
        
        public void Validate()
        {
            if (vertices is null) throw new System.NullReferenceException();
            if (vertex_normals is null) throw new System.NullReferenceException();
            if (faces is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += 24 * vertices.Length;
                size += 24 * vertex_normals.Length;
                size += 12 * faces.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshGeometry";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "9a7ed3efa2a35ef81abaf7dcc675ed20";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VRsQrCQAzd7ysCDo6CFQfBTSgOBcFuRcpRc22gzZXLKa1f7xVLdVAnzfSSvISXvBkk" +
                "KBXEaBv0rh8y0SWqcizkjZSyOFhin53gis5TgfKljV3O1jW6FtWEzQ9C6khzWeOez8N4oBo9rFHbH4dK" +
                "jvEG3qhTM0grEigse00s4CuE1gp5sgzWgA5Z4AExGIcI0gaFytRW+/UKugn1E7r9S/7Hv4UjdmiIX0T7" +
                "kTGXpzmXcEe0zKLJEBrH1R0jxS0C7gEAAA==";
                
    }
}
