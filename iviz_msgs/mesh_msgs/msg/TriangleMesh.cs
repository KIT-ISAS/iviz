/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TriangleMesh : IDeserializable<TriangleMesh>, IMessage
    {
        //# Definition of a triangle mesh
        [DataMember (Name = "triangles")] public TriangleIndices[] Triangles; // list of triangles; the index values refer to positions in vertices (and vertex_normals, if given)
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices; // the actual vertices that make up the mesh
        //optional:
        [DataMember (Name = "vertex_normals")] public GeometryMsgs.Point[] VertexNormals;
        [DataMember (Name = "vertex_colors")] public StdMsgs.ColorRGBA[] VertexColors;
        [DataMember (Name = "triangle_colors")] public StdMsgs.ColorRGBA[] TriangleColors;
        [DataMember (Name = "vertex_texture_coords")] public GeometryMsgs.Point[] VertexTextureCoords;
        [DataMember (Name = "face_materials")] public MeshMsgs.MeshMaterial[] FaceMaterials;
        [DataMember (Name = "textures")] public SensorMsgs.Image[] Textures;
        [DataMember (Name = "clusters")] public MeshMsgs.MeshFaceCluster[] Clusters;
    
        /// Constructor for empty message.
        public TriangleMesh()
        {
            Triangles = System.Array.Empty<TriangleIndices>();
            Vertices = System.Array.Empty<GeometryMsgs.Point>();
            VertexNormals = System.Array.Empty<GeometryMsgs.Point>();
            VertexColors = System.Array.Empty<StdMsgs.ColorRGBA>();
            TriangleColors = System.Array.Empty<StdMsgs.ColorRGBA>();
            VertexTextureCoords = System.Array.Empty<GeometryMsgs.Point>();
            FaceMaterials = System.Array.Empty<MeshMsgs.MeshMaterial>();
            Textures = System.Array.Empty<SensorMsgs.Image>();
            Clusters = System.Array.Empty<MeshMsgs.MeshFaceCluster>();
        }
        
        /// Explicit constructor.
        public TriangleMesh(TriangleIndices[] Triangles, GeometryMsgs.Point[] Vertices, GeometryMsgs.Point[] VertexNormals, StdMsgs.ColorRGBA[] VertexColors, StdMsgs.ColorRGBA[] TriangleColors, GeometryMsgs.Point[] VertexTextureCoords, MeshMsgs.MeshMaterial[] FaceMaterials, SensorMsgs.Image[] Textures, MeshMsgs.MeshFaceCluster[] Clusters)
        {
            this.Triangles = Triangles;
            this.Vertices = Vertices;
            this.VertexNormals = VertexNormals;
            this.VertexColors = VertexColors;
            this.TriangleColors = TriangleColors;
            this.VertexTextureCoords = VertexTextureCoords;
            this.FaceMaterials = FaceMaterials;
            this.Textures = Textures;
            this.Clusters = Clusters;
        }
        
        /// Constructor with buffer.
        internal TriangleMesh(ref Buffer b)
        {
            Triangles = b.DeserializeArray<TriangleIndices>();
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new TriangleIndices(ref b);
            }
            Vertices = b.DeserializeStructArray<GeometryMsgs.Point>();
            VertexNormals = b.DeserializeStructArray<GeometryMsgs.Point>();
            VertexColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
            TriangleColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
            VertexTextureCoords = b.DeserializeStructArray<GeometryMsgs.Point>();
            FaceMaterials = b.DeserializeArray<MeshMsgs.MeshMaterial>();
            for (int i = 0; i < FaceMaterials.Length; i++)
            {
                FaceMaterials[i] = new MeshMsgs.MeshMaterial(ref b);
            }
            Textures = b.DeserializeArray<SensorMsgs.Image>();
            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i] = new SensorMsgs.Image(ref b);
            }
            Clusters = b.DeserializeArray<MeshMsgs.MeshFaceCluster>();
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshMsgs.MeshFaceCluster(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TriangleMesh(ref b);
        
        TriangleMesh IDeserializable<TriangleMesh>.RosDeserialize(ref Buffer b) => new TriangleMesh(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Triangles);
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(VertexNormals);
            b.SerializeStructArray(VertexColors);
            b.SerializeStructArray(TriangleColors);
            b.SerializeStructArray(VertexTextureCoords);
            b.SerializeArray(FaceMaterials);
            b.SerializeArray(Textures);
            b.SerializeArray(Clusters);
        }
        
        public void RosValidate()
        {
            if (Triangles is null) throw new System.NullReferenceException(nameof(Triangles));
            for (int i = 0; i < Triangles.Length; i++)
            {
                if (Triangles[i] is null) throw new System.NullReferenceException($"{nameof(Triangles)}[{i}]");
                Triangles[i].RosValidate();
            }
            if (Vertices is null) throw new System.NullReferenceException(nameof(Vertices));
            if (VertexNormals is null) throw new System.NullReferenceException(nameof(VertexNormals));
            if (VertexColors is null) throw new System.NullReferenceException(nameof(VertexColors));
            if (TriangleColors is null) throw new System.NullReferenceException(nameof(TriangleColors));
            if (VertexTextureCoords is null) throw new System.NullReferenceException(nameof(VertexTextureCoords));
            if (FaceMaterials is null) throw new System.NullReferenceException(nameof(FaceMaterials));
            for (int i = 0; i < FaceMaterials.Length; i++)
            {
                if (FaceMaterials[i] is null) throw new System.NullReferenceException($"{nameof(FaceMaterials)}[{i}]");
                FaceMaterials[i].RosValidate();
            }
            if (Textures is null) throw new System.NullReferenceException(nameof(Textures));
            for (int i = 0; i < Textures.Length; i++)
            {
                if (Textures[i] is null) throw new System.NullReferenceException($"{nameof(Textures)}[{i}]");
                Textures[i].RosValidate();
            }
            if (Clusters is null) throw new System.NullReferenceException(nameof(Clusters));
            for (int i = 0; i < Clusters.Length; i++)
            {
                if (Clusters[i] is null) throw new System.NullReferenceException($"{nameof(Clusters)}[{i}]");
                Clusters[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += 12 * Triangles.Length;
                size += 24 * Vertices.Length;
                size += 24 * VertexNormals.Length;
                size += 16 * VertexColors.Length;
                size += 16 * TriangleColors.Length;
                size += 24 * VertexTextureCoords.Length;
                size += 21 * FaceMaterials.Length;
                size += BuiltIns.GetArraySize(Textures);
                size += BuiltIns.GetArraySize(Clusters);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleMesh";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b112c5b670c2c3e8b1571aae11ccc3da";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
