using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Mesh")]
    public sealed class Mesh : IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "bounds")] public BoundingBox Bounds { get; set; }
        [DataMember (Name = "vertices")] public Vector3[] Vertices { get; set; }
        [DataMember (Name = "normals")] public Vector3[] Normals { get; set; }
        [DataMember (Name = "texCoords")] public Vector2[] TexCoords { get; set; }
        [DataMember (Name = "colors")] public Color[] Colors { get; set; }
        [DataMember (Name = "faces")] public Triangle[] Faces { get; set; }
        [DataMember (Name = "materialIndex")] public uint MaterialIndex { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Mesh()
        {
            Name = "";
            Vertices = System.Array.Empty<Vector3>();
            Normals = System.Array.Empty<Vector3>();
            TexCoords = System.Array.Empty<Vector2>();
            Colors = System.Array.Empty<Color>();
            Faces = System.Array.Empty<Triangle>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Mesh(string Name, in BoundingBox Bounds, Vector3[] Vertices, Vector3[] Normals, Vector2[] TexCoords, Color[] Colors, Triangle[] Faces, uint MaterialIndex)
        {
            this.Name = Name;
            this.Bounds = Bounds;
            this.Vertices = Vertices;
            this.Normals = Normals;
            this.TexCoords = TexCoords;
            this.Colors = Colors;
            this.Faces = Faces;
            this.MaterialIndex = MaterialIndex;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Mesh(Buffer b)
        {
            Name = b.DeserializeString();
            Bounds = new BoundingBox(b);
            Vertices = b.DeserializeStructArray<Vector3>();
            Normals = b.DeserializeStructArray<Vector3>();
            TexCoords = b.DeserializeStructArray<Vector2>();
            Colors = b.DeserializeStructArray<Color>();
            Faces = b.DeserializeStructArray<Triangle>();
            MaterialIndex = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Mesh(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Name);
            Bounds.RosSerialize(b);
            b.SerializeStructArray(Vertices, 0);
            b.SerializeStructArray(Normals, 0);
            b.SerializeStructArray(TexCoords, 0);
            b.SerializeStructArray(Colors, 0);
            b.SerializeStructArray(Faces, 0);
            b.Serialize(MaterialIndex);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException();
            if (Vertices is null) throw new System.NullReferenceException();
            if (Normals is null) throw new System.NullReferenceException();
            if (TexCoords is null) throw new System.NullReferenceException();
            if (Colors is null) throw new System.NullReferenceException();
            if (Faces is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 52;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += 12 * Vertices.Length;
                size += 12 * Normals.Length;
                size += 8 * TexCoords.Length;
                size += 4 * Colors.Length;
                size += 12 * Faces.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Mesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f3a3007861d23d32f54471df3b83e314";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71SsQrCMBDd7yvyB0K7iODSDuLgpLiIyLWNJZDmIElL2q83LY2N4GjN8u7eI3cv4Rmr" +
                "haqZwoZDRq2qfJeRY8VYG7jy0pJOb3fWcW1FyWNKkW5QBibxjOUuJ9L+Yk6StGfKEQ1ctEBVS+6ZJ45T" +
                "WqFsmrAGLfeSPKqKO4D9jw+czocdE50YHo2pzSZ6ITwl4WRBqKhB96H0sdLHyhArw/rW529/b11sLrb+" +
                "ZiP5amP15VOopuhsWcB6xmJGXN9GCHMIMYaiCEUJ8ALPHwsOWQMAAA==";
                
    }
}
