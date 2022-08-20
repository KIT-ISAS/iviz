/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class TriangleMesh : IDeserializable<TriangleMesh>, IMessage
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
        
        public TriangleMesh(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeArray(out Triangles);
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new TriangleIndices(ref b);
            }
            b.Align4();
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
            b.Align4();
            b.DeserializeArray(out Textures);
            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i] = new SensorMsgs.Image(ref b);
            }
            b.Align4();
            b.DeserializeArray(out Clusters);
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshMsgs.MeshFaceCluster(ref b);
            }
        }
        
        public TriangleMesh RosDeserialize(ref ReadBuffer b) => new TriangleMesh(ref b);
        
        public TriangleMesh RosDeserialize(ref ReadBuffer2 b) => new TriangleMesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Triangles.Length);
            foreach (var t in Triangles)
            {
                t.RosSerialize(ref b);
            }
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(VertexNormals);
            b.SerializeStructArray(VertexColors);
            b.SerializeStructArray(TriangleColors);
            b.SerializeStructArray(VertexTextureCoords);
            b.Serialize(FaceMaterials.Length);
            foreach (var t in FaceMaterials)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Textures.Length);
            foreach (var t in Textures)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Clusters.Length);
            foreach (var t in Clusters)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Triangles.Length);
            foreach (var t in Triangles)
            {
                t.RosSerialize(ref b);
            }
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(VertexNormals);
            b.SerializeStructArray(VertexColors);
            b.SerializeStructArray(TriangleColors);
            b.SerializeStructArray(VertexTextureCoords);
            b.Serialize(FaceMaterials.Length);
            foreach (var t in FaceMaterials)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Textures.Length);
            foreach (var t in Textures)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Clusters.Length);
            foreach (var t in Clusters)
            {
                t.RosSerialize(ref b);
            }
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Triangles length
            c += 12 * Triangles.Length;
            c += 4; // Vertices length
            c = WriteBuffer2.Align8(c);
            c += 24 * Vertices.Length;
            c += 4; // VertexNormals length
            c = WriteBuffer2.Align8(c);
            c += 24 * VertexNormals.Length;
            c += 4; // VertexColors length
            c += 16 * VertexColors.Length;
            c += 4; // TriangleColors length
            c += 16 * TriangleColors.Length;
            c += 4; // VertexTextureCoords length
            c = WriteBuffer2.Align8(c);
            c += 24 * VertexTextureCoords.Length;
            c += 4; // FaceMaterials length
            c += (21 + 3) * FaceMaterials.Length - 3;
            c = WriteBuffer2.Align4(c);
            c += 4; // Textures.Length
            foreach (var t in Textures)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align4(c);
            c += 4; // Clusters.Length
            foreach (var t in Clusters)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "mesh_msgs/TriangleMesh";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b112c5b670c2c3e8b1571aae11ccc3da";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XW2/bNhR+568g4Ickbex0zTAUGYZdkrX1Q4CizVsQGLR0JHGjSIWkHDu/fh9JUbZz" +
                "Wx+WCXZMked85/DcM5nwC6qkll4azU3FBfdWCl0r4i25hrGr4XWuS1mQu74ZCRyfcCWdD2zj3s/cN8Sl" +
                "LmnNV0L1oLJUkeXe8M64KMfhnK/I+gDID4Uu4xutF9rYVih3zGXFa7kifcRqMi15u1m0rnYnX4zUHjqM" +
                "3JMoTxS+F2q76xvheSv+Jt53kSDeZWK6IF6osxdQt1ow58t0fm6UsV8//fH7lqYIW8+QZGNkoheF4et7" +
                "G2iNLR0Liia6S6wuhSeAKZBXoqBFO7xDMGlnbKKct6KmIDdBPQT5CM5z1TuwgqhIK8fYL//xwy6/fTrj" +
                "W9kPIoc9H2oHbnQd62Gd0/fXp6OB5MD+Svo+4RxoetVIxwujvZDaxQjKwZs07wJdCOPKEnHXwcSsUkb4" +
                "n37k63G1GVf3r6X+4wBMMk/fczuu6nG1HFfi9QNgN4IHv+YYXcQK8UT68JgzbGmM4o1wOT1ezXwP0yj7" +
                "HrdweN3GgNC814VpOySYo5LLgfrw3TF/d8TBgpLjTTdVVHmwWY2ih1gZ6Bj7TKLEVpN+hmfCh20vIdGL" +
                "tuOuMb0q+TJUtdteDlEXzrdw/KlnxKqsaGHhcgcqVL4CFTIeBZwCv1Y8B2SsrGUM9cTwGKgg7dMFX0Z6" +
                "u87MKWXQBkI2Ab+J+RO7xUt3ervZByjNnf4+xvt9RnxN7EJK6GiCf0WYJ5rRnA1ZOAXdam93QDqPZpjr" +
                "yjwHl0NKOGcKibwo+Z30zVaPEGyVkoV/DiFQLqkRK2lsiLgeSYSKSiXL6dVQNOyWJQGn7ePUFyX6q+7b" +
                "ZXKfNXe56EKdEvo84o7bTzIjWftWozaHrCGuqA5dOLX9CjoSMqaUuubCUqyWEnOFs8VJBF7kYzcruo5F" +
                "g29Mz+9EChTkgy6FLeU9jMY13WHHBrQqNGhc5y84FmzWuGnv0NN+C+OImznT24JAVNNMk48uQ6KXIYep" +
                "FVLxzppQz4NiETcrMmNsEDFqnk3xZ97AtTu5JuX4dMqLRmhNCr4VGofHyBxkYFw5qP20I4MnMZuE5mHa" +
                "6NQ8RiXhcT6SGp26pJPdCvXQak3y+wd4ZbGUNa6Ifjp4LnQt/CmFF3w8+zV7GiNAt6fQx16pEAvwoa4R" +
                "BNBgufFDP/6AuSEC7TBcf0UFuMmTF9xh5ToSpZsHBQ6jlDcxxI5evQGm4odw+DaEDZziRVQ7hGKDDCCL" +
                "6ryCu2KlRf7FU7/pyM1y4ccHxkIqK7Xhfaj0iETU/bbXKHyetpU684MT1sJMIMII0ytheRzmpA7ksU4E" +
                "dHwc3fbwHvH5xVlIdkdF7zHmQhLcbUnEkJxf8NFNdBtNbdwPN2xydWem2Kd6r18MaclpnVuTcGcQ9ibd" +
                "cgYhoc9BXBldgr0FXt0R/BR0oc4UDT/EFb5sfGNSZV0JNO2lim5EuVdAPQhMB0c7yDpCa6FNhk+IWxnf" +
                "A6tH3HCnKRKqVMEMrq9FrHJI1pUsQbrcRJBCSYQecmZphd2w2BmjSDb5GLvVNvBDV96vtjm/c/n+f6ag" +
                "nRGcTfJimHSH0T6PueM/KVlTJZakGPsHI0k2NqoNAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Textures) e.Dispose();
        }
    }
}
