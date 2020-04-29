using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class TriangleMesh : IMessage
    {
        //# Definition of a triangle mesh
        
        public TriangleIndices[] triangles; // list of triangles; the index values refer to positions in vertices (and vertex_normals, if given)
        public geometry_msgs.Point[] vertices; // the actual vertices that make up the mesh
        //optional:
        public geometry_msgs.Point[] vertex_normals;
        public std_msgs.ColorRGBA[] vertex_colors;
        public std_msgs.ColorRGBA[] triangle_colors;
        public geometry_msgs.Point[] vertex_texture_coords;
        public mesh_msgs.MeshMaterial[] face_materials;
        public sensor_msgs.Image[] textures;
        public mesh_msgs.MeshFaceCluster[] clusters;
    
        /// <summary> Constructor for empty message. </summary>
        public TriangleMesh()
        {
            triangles = System.Array.Empty<TriangleIndices>();
            vertices = System.Array.Empty<geometry_msgs.Point>();
            vertex_normals = System.Array.Empty<geometry_msgs.Point>();
            vertex_colors = System.Array.Empty<std_msgs.ColorRGBA>();
            triangle_colors = System.Array.Empty<std_msgs.ColorRGBA>();
            vertex_texture_coords = System.Array.Empty<geometry_msgs.Point>();
            face_materials = System.Array.Empty<mesh_msgs.MeshMaterial>();
            textures = System.Array.Empty<sensor_msgs.Image>();
            clusters = System.Array.Empty<mesh_msgs.MeshFaceCluster>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStructArray(out triangles, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out vertices, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out vertex_normals, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out vertex_colors, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out triangle_colors, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out vertex_texture_coords, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out face_materials, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out textures, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out clusters, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStructArray(triangles, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(vertices, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(vertex_normals, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(vertex_colors, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(triangle_colors, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(vertex_texture_coords, ref ptr, end, 0);
            BuiltIns.SerializeArray(face_materials, ref ptr, end, 0);
            BuiltIns.SerializeArray(textures, ref ptr, end, 0);
            BuiltIns.SerializeArray(clusters, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += 12 * triangles.Length;
                size += 24 * vertices.Length;
                size += 24 * vertex_normals.Length;
                size += 16 * vertex_colors.Length;
                size += 16 * triangle_colors.Length;
                size += 24 * vertex_texture_coords.Length;
                size += 21 * face_materials.Length;
                for (int i = 0; i < textures.Length; i++)
                {
                    size += textures[i].RosMessageLength;
                }
                for (int i = 0; i < clusters.Length; i++)
                {
                    size += clusters[i].RosMessageLength;
                }
                return size;
            }
        }
    
        public IMessage Create() => new TriangleMesh();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/TriangleMesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "b112c5b670c2c3e8b1571aae11ccc3da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XS2/cNhC+61cMsAfbidcOkqIIXBR92HW6BwNB41tQLLjSSGJLkTJJ7cO/vh9JUet3" +
                "c6gr7Hopcuab4bw9m9EF11JLL40mU5Mgb6XQjWLq2LVFcT2+LnQlS3Zf/5wIHM1ISecD27T3A/mWSeqK" +
                "t7QWagCV5ZoteUO9cVGOwzmt2foASIdCV/GNt0ttbCeUOyZZUyPXrI+Khk3H3u6WnWvc6WcjtYcOE/cs" +
                "yhOlH4Ta7/pWeOrE30xDHwniXWamD+KFOnsBda9F4XyVzs+NMvaPT7/+sqcpw9YzJNkYmehFYfj6wQZa" +
                "YytXBEUT3RVWV8IzwBTIa1HyshvfIZi1MzZRLjrRcJCboB6CXILzXA0OrCAq08oVxY//8VNcffl0RnvZ" +
                "DyKneD7UDtzkumKAdT68//phMpAc2V9J3yecA02vW+moNNoLqV2MoBy8SfM+0IUwri0zuR4mLmplhP/+" +
                "O9pOq920un0t9R8HYJL54T3ZadVMq9W0Eq8fAHcjePRrjtFlrBBPpA/FnClWxihqhcvp8Wrme5hG2fe4" +
                "hcPrPgaEpkGXpuuRYI4rkiP14btjendEYEHJ8aafK6492KxG0UOsjHRF8TuLCltt+hmfGY3bXkKiF11P" +
                "rjWDqmgVqtrNIMeoC+d7OHrqmbBqKzpYuLoDFSpfiQoZjwJOiV8rngMyVjYyhnpieAxUsvbpgi8jvd1m" +
                "5pQyaAMhm4DfxvyJ3eKlO73d3QeozEZ/G+PtfUZ8TexCSuhogn9FWCSayZwtWzgF3ere7oh0Hs2w0LV5" +
                "Di6HlHDOlBJ5UdFG+navRwi2WsnSP4cQKFfcirU0NkTcgCRCReWqyOnVcjTsniUBp+3j1Bcl+qseulVy" +
                "nzWbXHShTgV9HnHH7SeZkaxDp1GbQ9YwKW5CF05tv4aOjIyppG5IWI7VUmKucLY8jcDLfOxOyr4vosF3" +
                "ZqCNSIGCfNCVsJW8hdFI8wY7NqDVoUHjOn/BsWCzxs0Hh572cxhH3Ikzgy0ZRA2faPbRZUj0KuQwd0Iq" +
                "6q0J9TwoFnGzIidFMYqYNM+m+C1v4Nq93LJyNJ9T2QqtWcG3QuPwGJmDDIwrB7WfdmTwJGaT0DxMF52a" +
                "x6gkPM5HUqNTV3x6t0I9tFqb/P4RXlmuZIMrop+OngtdC38q4QVNZz9lT2ME6O8pdDkoFWIBPtQNggAa" +
                "rHZ+7McfMTdEoDsM48gFP1i5jafpykHyYYR/E2Pr6NU7X6p6iIMvY7zAG15EfUMMtgh9tijLa/gpllgk" +
                "Xjz1u57dSa74+MBKyGGldjSEEo8QRMHvBo2K53lfojM/OGEmDAMizC6DEpbiFCd1II8FIqDj4/hmgNuY" +
                "FhdnIcsdl4PHfAtJ8LNlEWNxcUGTf/immF1vzByv3NzrD2MaEm9zKxLuDDLepMudADv0NUipoiewt8Sr" +
                "O4J7ggrcm7KlQ2j+eedbkyrpWqBJr1T0Hsq7AupBYDo4uoOsI7QW2mT4hLiX8S2wesINd5ojgSoVbu+G" +
                "RsSqhuRcywqkq10EKZVEs0GOrKywuyJ2wiiymF3G7rQP9NCF71fXnM+5XP8/U8+dkbuY5cU42Y6jfB5r" +
                "p39KsqZKrFgVxT9oNdMTmg0AAA==";
                
    }
}
