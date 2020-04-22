
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
            triangles = System.Array.Empty<MeshTriangle>();
            vertices = System.Array.Empty<geometry_msgs.Point>();
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
                "H4sIAAAAAAAACrWRwUrEQAyG7wN9h8AePAqueFC8CZ4WBPdWliXUTBuczgyTdOn69M60tiviUecUMsmf" +
                "L3828ESWPSsHD8ECQk/SGbMBx6Ilo4nRt47kAbQjYP9GI5zQDSSQyFICDRCDTBKS/+FESbkhqQ9ml8X2" +
                "XwL14aJVBhQ1bHRAt3bkHCr0+E4wxKlgomkp9KTpfOylleuXwF6z2NJkKvP4x68yu9fne5AOI81Dvy9S" +
                "Zfifri2bXcmFa8ic25t6O6PSeMze/S/wL0YV2H3HAk3wipwvVGxd7jXDx1JYLmcTEUjEhox1AfXuFsY1" +
                "Oq/RR97gE6lpooA5AgAA";
                
    }
}
