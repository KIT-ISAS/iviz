using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshGeometry : IMessage
    {
        // Mesh Geometry Message
        public geometry_msgs.Point[] vertices;
        public geometry_msgs.Point[] vertex_normals;
        public mesh_msgs.TriangleIndices[] faces;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshGeometry()
        {
            vertices = System.Array.Empty<geometry_msgs.Point>();
            vertex_normals = System.Array.Empty<geometry_msgs.Point>();
            faces = System.Array.Empty<mesh_msgs.TriangleIndices>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStructArray(out vertices, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out vertex_normals, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out faces, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStructArray(vertices, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(vertex_normals, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(faces, ref ptr, end, 0);
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
    
        public IMessage Create() => new MeshGeometry();
    
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
