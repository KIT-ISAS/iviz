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
                "H4sIAAAAAAAACr1XS2/jNhC+61cQ8CHJbuwsNkWxSFH0kTRbHwIsurktCoOWRhJbilRIyo/8+v2GEmU7" +
                "r91DU8GOKXLmm+G8M5mIKyqVUUFZI2wppAhOSVNpEg35Ostuh9e5KVRO/svfI4EXE6GVD8w27v0kQk1C" +
                "mYI2YiV1BypHJTkRrGitj3I8zsWKXGBAcSxNEd9oszDWNVL7U6FKUakVmZOsIttQcNtF4yt/9skqE6DD" +
                "yD2J8mQeOql3u6GWQTTyXxJdGwniXSa2ZfFSX7yAutMi86Hozy+ttu6vj7//tqPJeesZkmSMRPSiMHxD" +
                "55jWusJnrGhPd4PVjQwEMA3yUua0aIZ3CCbjresp542siOX2UA9BrsF5qTsPVhDl/cpn2c//8ZPdfP54" +
                "IXayH0RO9nyoHfnRdVkH65y//3I+GgixFA9eSd8nnANNb2vlRW5NkArhyhGUgrfXvGU6DuPSEQnfwsRZ" +
                "qa0MP/4gNuNqO67uX0v9xwHYyzx/L9y4qsbVclzJ1w+A/Qge/JpilL1KmyfSB0bHKltaq0UtfUqPVzPf" +
                "wzRKvsctPF53MSCN6ExumxYJ5qkQaqA+fncq3p0IsKDkBNtONZUBbM6g6CFWBros+5Nkga26/xmeiRi2" +
                "g4LEIJtW+Np2uhBLrmp3nRqijs93cIn94BmxSicbWLjYg+LKl6NCxiPGyfHr5HNA1qkKwQ26nuExUE4G" +
                "jv020ttNYu5TBm2Aswn4dcyf2C1eutPb7SFAYdfm+xjvDxnxtbELaWmiCb6JMO9pRnPW5OAUdKuD3QHp" +
                "Mpphbkr7HFwKKem9zRXyohBrFeqdHhxspVZ5eA6BKZdUy5WyjiOuQxKholKRpfSqKRp2x9ID99unfV9U" +
                "6K+ma5a9+5xdp6ILdQro84g7bj/JjGTtGoPazFlDQlPFXbhv+yV0JGRMoUwlJCzH1VJhrvAuP4vAi3Ts" +
                "Z3nbZtHgW9uJtewDBflgCukKdQ+jCUNr7DhGAzQ64an4B44Fm7N+2nn0tF95HPEzbzuXE4gqmhkK0WVI" +
                "9IJzmBqptGid5XrOikXcpMgsywYRo+bJFH+kDVy7VRvSXkynIq+lMaThW2lweIrMQQbGlYfaTzuSPYnZ" +
                "hJuHbaJT0xjVC4/zkTLo1AWd7Veoh1bDeMae+wCvLJaqwhXRTwfPcdfCn0IGKcazX5KnMQK0Bwpdd1pz" +
                "LMCHpkIQQIPlNgz9+APmhgi0xzCMXPCDU5t42l+ZJR9H+Dcxtk5evfP1VQ9x8HmIF3gjyKgvx2CN0CeH" +
                "sryCn2KJReLF07Btyc9SxccHVkIOa70VCCekuUWAN01nUPEC7Up04gcnzIRhQPLs0mnpQI8AUIbJY4Fg" +
                "dHw83XVwG4n51QVnuae8C5hvIQl+diRjLM6vxOgfussmt2s7xStVB/1hSENBm9SKpL+AjDf95WbA5r4G" +
                "KUX0BPYWePUncA+rQK3Na3EMzT9tQ82tBQG4kmjSSyQnjz2wAFCPmOnoZA+Z1b4QRhqb4HvEnYzvgTUj" +
                "Lt9pigQqNN/edxUMCEIk50oVIF1uI0iuFZoNcmTppNtmsRNGkdnkOnanXaBzFz6srimfU7n+f6aevZE7" +
                "m6TFMNkOo3waa8d/SpKmWi5JZ9lXaDXTE5oNAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
