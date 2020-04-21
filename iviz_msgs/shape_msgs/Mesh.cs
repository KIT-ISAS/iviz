
namespace Iviz.Msgs.shape_msgs
{
    public sealed class Mesh : IMessage 
    {
        // Definition of a mesh
        
        // list of triangles; the index values refer to positions in vertices[]
        public MeshTriangle[] triangles;
        
        // the actual vertices that make up the mesh
        public geometry_msgs.Point[] vertices;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "shape_msgs/Mesh";

        public IMessage Create() => new Mesh();

        public int GetLength()
        {
            int size = 8;
            size += 12 * triangles.Length;
            size += 24 * vertices.Length;
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public Mesh()
        {
            triangles = new MeshTriangle[0];
            vertices = new geometry_msgs.Point[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out triangles, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out vertices, ref ptr, end, 0);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(triangles, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(vertices, ref ptr, end, 0);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1ffdae9486cd3316a121c578b47a85cc";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE7WRwWrDMAyG734KQQ87Dtaxw8Zug50Kg/UWShGpnIg5trGUku7pZydNOkqPm09Cln59" +
            "+rWCN7LsWTl4CBYQOpLWmBU4Fi0ZTYy+cSQvoC0B+wMNcETXk0AiSwk0QAwySkj+hyMl5Zqk2plNFtue" +
            "BardRasMKGpYa49u6cg5VOjwi6CPY8FI01DoSNNp30kj9x+BvWaxucmY1z9+ZvP5/gzSYqRp5O81Mvm1" +
            "ZfNad3KB6jPk+qFaT5w07LNx/0l7w6NMum1ZoA5ekb2Mhs6XmshjqSs3s4kIJGJNxrqA+vQIwxKdlujb" +
            "mB+gQ/ZdMgIAAA==";

    }
}
