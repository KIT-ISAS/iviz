/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/TriangleMesh")]
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
    
        /// <summary> Constructor for empty message. </summary>
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
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TriangleMesh(ref b);
        }
        
        TriangleMesh IDeserializable<TriangleMesh>.RosDeserialize(ref Buffer b)
        {
            return new TriangleMesh(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Triangles, 0);
            b.SerializeStructArray(Vertices, 0);
            b.SerializeStructArray(VertexNormals, 0);
            b.SerializeStructArray(VertexColors, 0);
            b.SerializeStructArray(TriangleColors, 0);
            b.SerializeStructArray(VertexTextureCoords, 0);
            b.SerializeArray(FaceMaterials, 0);
            b.SerializeArray(Textures, 0);
            b.SerializeArray(Clusters, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleMesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b112c5b670c2c3e8b1571aae11ccc3da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XS2/cNhC+C/B/ILAH24nXDuKiCFwUfdhNugcDQeNbUCy40khiS5EySe3Dv77fUKJ2" +
                "16/kUFvY9VLkzDfDeXsyEVdUKqOCskbYUkgRnJKm0iQa8nWW3QyvM1OonPzXv0cCLyZCKx+Ybdz7SYSa" +
                "hDIFrcVS6g5UjkpyIljRWh/leJyLJbnAgOJImiK+0XpurGuk9idClaJSSzLHWUW2oeA288ZX/uyzVSZA" +
                "h5F7EuXJPHRSb3dDLYNo5L8kujYSxLtMbMvipb54BnWrReZD0Z9fWm3dX59+/21Lk/PWEyTJGInoWWH4" +
                "hs4xrXWFz1jRnu4aq2sZCGAa5KXMad4M7xBMxlvXU84aWRHL7aHug3wE56XuPFhBlPcrnx1kP//Pz0F2" +
                "/eXThdhKvxc7B9nT0XboR+9lHQx0/v7r+WgjhFM8eDGVH/EQK3tTKy9ya4JUCFqOoxTCvfItE3Iwl45I" +
                "+BaGzkptZfjxB7EeV5txdfdyN3gYiAe92PP3wo2ralwtxpV8jUjYDeaDwcEpXtm9tH4klWB6rLKFtVrU" +
                "0qdUeUEj3k+qMQhwE4/3bTBIIzqT26ZFvnkqhGJyUB+9OxHvjgVYUIGCbaeaygA2Z1ADETQDXZb9SbLA" +
                "Vt3/DM9EDNtBQWKQTSt8bTtdiAUXudtODeHH51u4xL73jFilkw2MXOxAcSHMUTDjEePk+HXyKSDrVIUo" +
                "B13P8BAoJwPnfhvp7Tox97mDrsBpBfw6JlJsHs/d6e1mH6CwK/N9jHf7jPja2JS0NNEE30SY9TSjOWty" +
                "cAqa197ugHQZzTAzpX0KLoWU9N7mCrlRiJUK9VYPDrZSqzw8hcCUC6rlUlnHEdchj1BdqchShtUUDbtl" +
                "6YH77ZO+TSq0W9M1i959zq5SAYY6BfR5wB23H2VGvnaN8VnMGhKaKm7K/RRQQkdCxhTKVELCclw2FcYM" +
                "7/KzCDxPx/40b9ssGnxjO7GSfaAgH0whXaHuYDRhaIUdx2iARmM8Ef/AsWBz1k87jxb3K08n/tTbzuUE" +
                "oopODYXoMmR6wTlMjVRatM5yYWfFIm5S5DTLBhGj5skUf6QNXLtVa9JeTKcir6UxpOFbaXB4gsxBBsaV" +
                "h9qPO5I9iVGFu4htolPTVNULj+OSMmjcBZ3tlqj7VsO0xp77AK/MF6rCFdFbB89x+8KfQgYpxrNfkqcx" +
                "EbR7Cn3stOZYgA9NhSCABotNGHrzB4wREWiHYZjA4Aen1vG0vzJLPorwb2JsHb9CC+zrHtfuL0PIwCFB" +
                "RpU5DGtEPzlU5iVcFassci+ehk1L/jQVfXxgKKSx1huBiEKmW8R403QGRS/QtkonfnDCUhgMJI8ynZYO" +
                "9IgBZZg81ghGx8fTbQfPkZhdXXCie8q7gIkXkuBqRzKG4+xKjC6i22xys7JTvFK11yKGTBS0Tt1I+gvI" +
                "eNNf7hTY3NsgpYjOwN4cr/4YHmIVqLV5LY6g+edNqLm7IAaXEr16gfzkEQgWAOohMx0e7yCz2hfCSGMT" +
                "fI+4lfE9sGbE5TtNkUOF5tv7roIBQYj8XKoCpItNBMm1Qr9BmiycdJssNsMoMpt8jA1qG+vciPcLbErp" +
                "VLFfa/zZGcMRmsMqzbrDfJ8G3fE/laSslgvS0PQ/lGuWzLANAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
