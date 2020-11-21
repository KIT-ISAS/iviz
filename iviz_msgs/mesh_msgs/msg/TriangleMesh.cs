/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/TriangleMesh")]
    public sealed class TriangleMesh : IDeserializable<TriangleMesh>, IMessage
    {
        //# Definition of a triangle mesh
        [DataMember (Name = "triangles")] public TriangleIndices[] Triangles { get; set; } // list of triangles; the index values refer to positions in vertices (and vertex_normals, if given)
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices { get; set; } // the actual vertices that make up the mesh
        //optional:
        [DataMember (Name = "vertex_normals")] public GeometryMsgs.Point[] VertexNormals { get; set; }
        [DataMember (Name = "vertex_colors")] public StdMsgs.ColorRGBA[] VertexColors { get; set; }
        [DataMember (Name = "triangle_colors")] public StdMsgs.ColorRGBA[] TriangleColors { get; set; }
        [DataMember (Name = "vertex_texture_coords")] public GeometryMsgs.Point[] VertexTextureCoords { get; set; }
        [DataMember (Name = "face_materials")] public MeshMsgs.MeshMaterial[] FaceMaterials { get; set; }
        [DataMember (Name = "textures")] public SensorMsgs.Image[] Textures { get; set; }
        [DataMember (Name = "clusters")] public MeshMsgs.MeshFaceCluster[] Clusters { get; set; }
    
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
        public TriangleMesh(ref Buffer b)
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
                foreach (var i in Textures)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Clusters)
                {
                    size += i.RosMessageLength;
                }
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
                "H4sIAAAAAAAACr1X227jNhB9N+B/IOCHJN3YWWyKYpGi6CVptn4IsOjmbVEYtDSS2FKkQlK+5Ot7hrrZ" +
                "8TrNQxMjTihq5nA4c+aSyUTcUKaMCsoaYTMhRXBKmlyTKMkX49F4dN9uzE2qEvJf/+pFvJgIrXxgxX7v" +
                "RxEKEsqktBErqWtIOcrIiWBFZX08yeO9WJELDChOpUnjE20WxrpSan8uVCZytSJzNh7lZEsKbrsofe4v" +
                "PltlAozo1SfxQJmEWuphNxQyiFL+Q6KuokBznYmt2ACpr57DHQwZj3xIG4Frq63789Nvvw5CCW8dk+k8" +
                "0ks9ex6+oXYsbF0KYTa3EbzD6k4GApyGfCYTWpTtM59NxlvXiM5LmRMf3YAdwNxC91rXHsqQSpoVpMaj" +
                "n/7nz3h09+XTlRiOf8IiROIo8058H8bxqIaXLj98vewdBWY1b17N6G/Eic29L5QXiTVBKhCYKdXRuTG/" +
                "YkEmduaIhK/g6/Eo01aGH74Xm2G5HZaPr3iLQ062515+EG5Y5sNyOSzlm3Bil9ldpDvucpwJXju8BmKA" +
                "1Xi0tFaLQvoudV7Tl09zrOcDLuPxPPBCGlGbxJYV0s9TKlQnfvr+XLw/E9BBYQq2mmrKAvScQW0EgTpB" +
                "vsYfJFPsFs2f9jMR7XZQODXIshK+sLVOxZLL30OtWjby+x3ETn/v04NlTpZwdrqDxSUyQS2NrxgowV8n" +
                "jyJZp3LQHoKNxiFSQgZRfgHUu02n3WQTegYnGg4oYmrF1vLstd5t9xFSuzYv1Hzc18TXxqalpYlu+G+I" +
                "eSPU+7Qgh9Cgu+3ttlDX0RVzk9mjeB27pPc2UciUVKxVKAZLmHeZVkk4CsGiSyrkSlnH5KuRVii7lDLP" +
                "2pQrKPp3UGqwm+3zppUq9GRTl8smjM6u+9oMk1LYdKAet7+pjQSuSxNLOKcRCU059+5mWshgKCGFUmVy" +
                "IeFALqkKA4l3yUWEXnSv/SypKgaB47e2FmvZcAbZYVLpUvUI3wlDa+w4hgM2Wue5+BsRZj1n/bT26IG/" +
                "8BzjZ97WLiFI5TQzFGLskPwppzWVUmlROctln02LwJ0pM75Ne0pvfeeQ37sNXL5SG9JeTKciKaQxpBFl" +
                "afDyHImEjIwrD8uPhJRjirGG24wtY3i7Eaw5Pc5WyqC5p3SxW7ieui4OdxzCjwjPYqly3BMNuA0hdzj8" +
                "SmWQon/3cx9zDA7Vnk23tdZMC8TS5KADjFhuQ9fBP2LciFA7Gu3Ehng4tYlvm3vz2acR/7vIs7O3aJJN" +
                "NWRKfGm5g7AEGW1mQhZIBXKo2SsELBZf5GJ8G7YV+VnfEPADZyGxtd4KUAu5b0H4sqwNSmGgoXp3AKwK" +
                "b2GAkDz01Fo6KIALyrB8LBsRn7+eHmqEkMT85oqT31NSB8zJOAwxdyQjNec3YogUPUDxfm2neKZ8r3+0" +
                "uSlo07Ur6TEYT+D5KDADPHc/HJTGoGBvgUd/hkixFVTZpBCnMP/zNhTce8DHlURDXyJheV6CHwB7wkon" +
                "Z7vQbPqVMNLYDr+BHA55Ca4ZgPlaU6RUqtkFvs7hR0giYVcqhexyG1ESrdCNkDRLJx1msdgt46EAuY0N" +
                "bGA+d+v92tsneVfO32xS2hneYWm/agfk9h+Dfjru/9HpDdZySXgaj/4F+GVz8PUNAAA=";
                
    }
}
