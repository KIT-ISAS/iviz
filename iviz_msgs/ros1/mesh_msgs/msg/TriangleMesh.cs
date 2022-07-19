/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class TriangleMesh : IDeserializableRos1<TriangleMesh>, IMessageRos1
    {
        //# Definition of a triangle mesh
        /// <summary> List of triangles; the index values refer to positions in vertices (and vertex_normals, if given) </summary>
        [DataMember (Name = "triangles")] public TriangleIndices[] Triangles;
        /// <summary> The actual vertices that make up the mesh </summary>
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
        
        /// Constructor with buffer.
        public TriangleMesh(ref ReadBuffer b)
        {
            b.DeserializeArray(out Triangles);
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new TriangleIndices(ref b);
            }
            b.DeserializeStructArray(out Vertices);
            b.DeserializeStructArray(out VertexNormals);
            b.DeserializeStructArray(out VertexColors);
            b.DeserializeStructArray(out TriangleColors);
            b.DeserializeStructArray(out VertexTextureCoords);
            b.DeserializeArray(out FaceMaterials);
            for (int i = 0; i < FaceMaterials.Length; i++)
            {
                FaceMaterials[i] = new MeshMsgs.MeshMaterial(ref b);
            }
            b.DeserializeArray(out Textures);
            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i] = new SensorMsgs.Image(ref b);
            }
            b.DeserializeArray(out Clusters);
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
            if (Triangles is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Triangles.Length; i++)
            {
                if (Triangles[i] is null) BuiltIns.ThrowNullReference(nameof(Triangles), i);
                Triangles[i].RosValidate();
            }
            if (Vertices is null) BuiltIns.ThrowNullReference();
            if (VertexNormals is null) BuiltIns.ThrowNullReference();
            if (VertexColors is null) BuiltIns.ThrowNullReference();
            if (TriangleColors is null) BuiltIns.ThrowNullReference();
            if (VertexTextureCoords is null) BuiltIns.ThrowNullReference();
            if (FaceMaterials is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < FaceMaterials.Length; i++)
            {
                if (FaceMaterials[i] is null) BuiltIns.ThrowNullReference(nameof(FaceMaterials), i);
                FaceMaterials[i].RosValidate();
            }
            if (Textures is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Textures.Length; i++)
            {
                if (Textures[i] is null) BuiltIns.ThrowNullReference(nameof(Textures), i);
                Textures[i].RosValidate();
            }
            if (Clusters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Clusters.Length; i++)
            {
                if (Clusters[i] is null) BuiltIns.ThrowNullReference(nameof(Clusters), i);
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
                size += WriteBuffer.GetArraySize(Textures);
                size += WriteBuffer.GetArraySize(Clusters);
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/TriangleMesh";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b112c5b670c2c3e8b1571aae11ccc3da";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XW2/bNhR+568g4IckbewU7TAUGYZdkqXzQ4CizVtQGLR0JHGjSIWkfMmv30dSlO3c" +
                "1odlgh1T5DnfOTz3TCb8kiqppZdGc1Nxwb2VQteKeEuuYexmeJ3rUhbkbr+NBI5PuJLOB7Zx7yfuG+JS" +
                "l7ThK6F6UFmqyHJveGdclONwzldkfQDkx0KX8Y02C21sK5Q75bLitVyRPmE1mZa83S5aV7uzz0ZqDx1G" +
                "7kmUJwrfC7Xb9Y3wvBV/E++7SBDvMjFdEC/U+QuoOy2Y82U6vzDK2C+ffv9tR1OErWdIsjEy0YvC8PW9" +
                "DbTGlo4FRRPdNVbXwhPAFMgrUdCiHd4hmLQzNlHOW1FTkJugHoJcgfNC9Q6sICrSyjH283/8sOuvn875" +
                "TvaDyGHPh9qRG13Heljnw/vbD6OB5MD+Svo+4RxoetNIxwujvZDaxQjKwZs07wJdCOPKEnHXwcSsUkb4" +
                "H3/gm3G1HVf3r6X+4wBMMj+853Zc1eNqOa7E6wfAfgQPfs0xuogV4on04TFn2NIYxRvhcnq8mvkeplH2" +
                "PW7h8LqLAaF5rwvTdkgwRyWXA/Xxu1P+7oSDBSXHm26qqPJgsxpFD7Ey0DH2J4kSW036GZ4JH7a9hEQv" +
                "2o67xvSq5MtQ1e56OURdON/B8aeeEauyooWFyz2oUPkKVMh4FHAK/FrxHJCxspYx1BPDY6CCtE8XfBnp" +
                "7SYzp5RBGwjZBPwm5k/sFi/d6e32EKA0a/19jPeHjPia2IWU0NEE/4owTzSjORuycAq61cHugHQRzTDX" +
                "lXkOLoeUcM4UEnlR8rX0zU6PEGyVkoV/DiFQLqkRK2lsiLgeSYSKSiXL6dVQNOyOJQGn7dPUFyX6q+7b" +
                "ZXKfNetcdKFOCX0eccftJ5mRrH2rUZtD1hBXVIcunNp+BR0JGVNKXXNhKVZLibnC2eIsAi/ysZsVXcei" +
                "wbem52uRAgX5oEthS3kPo3FNa+zYgFaFBo3r/AXHgs0aN+0detqvYRxxM2d6WxCIappp8tFlSPQy5DC1" +
                "QireWRPqeVAs4mZFZowNIkbNsyn+yBu4dic3pByfTnnRCK1JwbdC4/AUmYMMjCsHtZ92ZPAkZpPQPEwb" +
                "nZrHqCQ8zkdSo1OXdLZfoR5arUl+/wivLJayxhXRTwfPha6FP6Xwgo9nv2RPYwToDhS66pUKsQAf6hpB" +
                "AA2WWz/044+YGyLQHsPtF1SAb3nygjus3ESidPOgwHGU8iaG2MmrN8BU/BAOX4ewgVO8iGqHUGyQAWRR" +
                "nVdwV6y0yL946rcduVku/PjAWEhlpba8D5UekYi63/Yahc/TrlJnfnDCWpgJRBhheiUsj8Oc1IE81omA" +
                "jo+jux7eIz6/PA/J7qjoPcZcSIK7LYkYkvNLPrqJ7tjkZm2meKX6oE0M2chpkzuScOeQ8SZdbgbs0N4g" +
                "pYyewN4Cr+4E7gkqUGeKhh9D889b35hUUFcCvXqpovdQ5RVQjwLT0ckeso7QWmiT4RPiTsb3wOoRN9xp" +
                "ijwqVbi962sRixtydCVLkC63EaRQEhGHVFlaYbcsNsQokk2uYpPaxXtoxodFNqd1rtr/z/CzN3mzSV4M" +
                "A+4w0efpdvzfJGuqxJIUY/8Asav3gaENAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Textures) e.Dispose();
        }
    }
}
