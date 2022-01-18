/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TriangleMesh : IDeserializable<TriangleMesh>, IMessage
    {
        //# Definition of a triangle mesh
        /// list of triangles; the index values refer to positions in vertices (and vertex_normals, if given)
        [DataMember (Name = "triangles")] public TriangleIndices[] Triangles;
        /// the actual vertices that make up the mesh
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
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
        public TriangleMesh(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TriangleMesh(ref b);
        
        public TriangleMesh RosDeserialize(ref ReadBuffer b) => new TriangleMesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
                "H4sIAAAAAAAAE71XS2/jNhA+l7+CgA9JdmNnsSmKIkXRR9Ld+hBgsZtbsDBoaSSxpUiFpPzIr+9HUpTt" +
                "vLqHpoIdU+TMN8N5ZzLhV1RJLb00mpuKC+6tFLpWxFtyDWM3w+tcl7Igd/t1JHB8wpV0PrCNez9x3xCX" +
                "uqQNXwnVg8pSRZZ7wzvjohyHc74i6wMgPxa6jG+0WWhjW6HcKZcVr+WK9AmrybTk7XbRutqdfTJSe+gw" +
                "ck+iPFH4Xqjdrm+E5634m3jfRYJ4l4npgnihLl5A3WnBnC/T+aVRxn7++PtvO5oibD1Dko2RiV4Uhq/v" +
                "baA1tnQsKJrorrG6Fp4ApkBeiYIW7fAOwaSdsYly3oqagtwE9RDkAzgvVe/ACqIirRxjP//HD7v+8vGC" +
                "72Q/iBz2fKgdudF1rId1zt/fno8GkgP7K+n7hHOg6U0jHS+M9kJqFyMoB2/SvAt0IYwrS8RdBxOzShnh" +
                "f/ieb8bVdlzdv5b6jwMwyTx/z+24qsfVclyJ1w+A/Qge/JpjdBErxBPpw2POsKUxijfC5fR4NfM9TKPs" +
                "e9zC4XUXA0LzXhem7ZBgjkouB+rjd6f83QkHC0qON91UUeXBZjWKHmJloGPsTxIltpr0MzwTPmx7CYle" +
                "tB13jelVyZehqt31coi6cL6D4089I1ZlRQsLl3tQofIVqJDxKOAU+LXiOSBjZS1jqCeGx0AFaZ8u+DLS" +
                "201mTimDNhCyCfhNzJ/YLV6609vtIUBp1vrbGO8PGfE1sQspoaMJ/hVhnmhGczZk4RR0q4PdAekymmGu" +
                "K/McXA4p4ZwpJPKi5Gvpm50eIdgqJQv/HEKgXFIjVtLYEHE9kggVlUqW06uhaNgdSwJO26epL0r0V923" +
                "y+Q+a9a56EKdEvo84o7bTzIjWftWozaHrCGuqA5dOLX9CjoSMqaUuubCUqyWEnOFs8VZBF7kYzcruo5F" +
                "g29Nz9ciBQryQZfClvIeRuOa1tixAa0KDRrX+QuOBZs1bto79LRfwzjiZs70tiAQ1TTT5KPLkOhlyGFq" +
                "hVS8sybU86BYxM2KzBgbRIyaZ1P8kTdw7U5uSDk+nfKiEVqTgm+FxuEpMgcZGFcOaj/tyOBJzCaheZg2" +
                "OjWPUUl4nI+kRqcu6Wy/Qj20WpP8/iO8sljKGldEPx08F7oW/pTCCz6e/ZI9jRGgO1DoQ69UiAX4UNcI" +
                "Amiw3PrQtaMEDA4RaY9jmLngCCs38TTdOYg+jvhvYnCdsH2E7zL77WdUkK+v3hZTSUSQfBmCCa7yIt4l" +
                "BGiDvCCLmr2CE2P9RVbGU7/tyM1yO8AHJkSCK7Xlfaj/iE90g7bXKIeedvU784MTNsSkIMJg0ytheRzx" +
                "pA7ksXoEdHwc3fXwKfH51UUoAY6K3mP4hSQEgSURA3V+xUfn0R2b3KzNFK9UHzSPIUc5bXKfEu4CMt6k" +
                "y82AHZoepJTRS9hb4NWdwHVBBepM0fBjaP5p6xuTyuxKoIMvVfQsar8C6lFgOjrZQ9YRWgttMnxC3Mn4" +
                "Flg94oY7TZFdpQq3d30tYslD5q5kCdLlNoIUSiKOkEBLK+yWxTYZRbLJh9i6dlkQWvRh6c3Jnmv5/zMS" +
                "7c3jbJIXw9g7zPl55h3/Y8maKrEkxdg/6t9UircNAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
