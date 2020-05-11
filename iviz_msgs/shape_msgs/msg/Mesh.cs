using System.Runtime.Serialization;

namespace Iviz.Msgs.shape_msgs
{
    public sealed class Mesh : IMessage
    {
        // Definition of a mesh
        
        // list of triangles; the index values refer to positions in vertices[]
        public MeshTriangle[] triangles { get; set; }
        
        // the actual vertices that make up the mesh
        public geometry_msgs.Point[] vertices { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Mesh()
        {
            triangles = System.Array.Empty<MeshTriangle>();
            vertices = System.Array.Empty<geometry_msgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Mesh(MeshTriangle[] triangles, geometry_msgs.Point[] vertices)
        {
            this.triangles = triangles ?? throw new System.ArgumentNullException(nameof(triangles));
            this.vertices = vertices ?? throw new System.ArgumentNullException(nameof(vertices));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Mesh(Buffer b)
        {
            this.triangles = b.DeserializeArray<MeshTriangle>(0);
            this.vertices = b.DeserializeStructArray<geometry_msgs.Point>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Mesh(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.triangles, 0);
            b.SerializeStructArray(this.vertices, 0);
        }
        
        public void Validate()
        {
            if (triangles is null) throw new System.NullReferenceException();
            if (vertices is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 12 * triangles.Length;
                size += 24 * vertices.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "shape_msgs/Mesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "1ffdae9486cd3316a121c578b47a85cc";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7WRwWrDMAyG734KQQ87Dtaxw8Zug50Kg/UWShGpnIg5trGUku7pZydNOkqPm09Cln59" +
                "+rWCN7LsWTl4CBYQOpLWmBU4Fi0ZTYy+cSQvoC0B+wMNcETXk0AiSwk0QAwySkj+hyMl5Zqk2plNFtue" +
                "BardRasMKGpYa49u6cg5VOjwi6CPY8FI01DoSNNp30kj9x+BvWaxucmY1z9+ZvP5/gzSYqRp5O81Mvm1" +
                "ZfNad3KB6jPk+qFaT5w07LNx/0l7w6NMum1ZoA5ekb2Mhs6XmshjqSs3s4kIJGJNxrqA+vQIwxKdlujb" +
                "mB+gQ/ZdMgIAAA==";
                
    }
}
