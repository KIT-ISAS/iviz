/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class TriangleMesh : IHasSerializer<TriangleMesh>, System.IDisposable, IMessage
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
            Triangles = EmptyArray<TriangleIndices>.Value;
            Vertices = EmptyArray<GeometryMsgs.Point>.Value;
            VertexNormals = EmptyArray<GeometryMsgs.Point>.Value;
            VertexColors = EmptyArray<StdMsgs.ColorRGBA>.Value;
            TriangleColors = EmptyArray<StdMsgs.ColorRGBA>.Value;
            VertexTextureCoords = EmptyArray<GeometryMsgs.Point>.Value;
            FaceMaterials = EmptyArray<MeshMsgs.MeshMaterial>.Value;
            Textures = EmptyArray<SensorMsgs.Image>.Value;
            Clusters = EmptyArray<MeshMsgs.MeshFaceCluster>.Value;
        }
        
        public TriangleMesh(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                TriangleIndices[] array;
                if (n == 0) array = EmptyArray<TriangleIndices>.Value;
                else
                {
                    array = new TriangleIndices[n];
                    b.DeserializeStructArray(array);
                }
                Triangles = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.DeserializeStructArray(array);
                }
                Vertices = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.DeserializeStructArray(array);
                }
                VertexNormals = array;
            }
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.ColorRGBA[] array;
                if (n == 0) array = EmptyArray<StdMsgs.ColorRGBA>.Value;
                else
                {
                    array = new StdMsgs.ColorRGBA[n];
                    b.DeserializeStructArray(array);
                }
                VertexColors = array;
            }
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.ColorRGBA[] array;
                if (n == 0) array = EmptyArray<StdMsgs.ColorRGBA>.Value;
                else
                {
                    array = new StdMsgs.ColorRGBA[n];
                    b.DeserializeStructArray(array);
                }
                TriangleColors = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.DeserializeStructArray(array);
                }
                VertexTextureCoords = array;
            }
            {
                int n = b.DeserializeArrayLength();
                MeshMsgs.MeshMaterial[] array;
                if (n == 0) array = EmptyArray<MeshMsgs.MeshMaterial>.Value;
                else
                {
                    array = new MeshMsgs.MeshMaterial[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshMsgs.MeshMaterial(ref b);
                    }
                }
                FaceMaterials = array;
            }
            {
                int n = b.DeserializeArrayLength();
                SensorMsgs.Image[] array;
                if (n == 0) array = EmptyArray<SensorMsgs.Image>.Value;
                else
                {
                    array = new SensorMsgs.Image[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new SensorMsgs.Image(ref b);
                    }
                }
                Textures = array;
            }
            {
                int n = b.DeserializeArrayLength();
                MeshMsgs.MeshFaceCluster[] array;
                if (n == 0) array = EmptyArray<MeshMsgs.MeshFaceCluster>.Value;
                else
                {
                    array = new MeshMsgs.MeshFaceCluster[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshMsgs.MeshFaceCluster(ref b);
                    }
                }
                Clusters = array;
            }
        }
        
        public TriangleMesh(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                TriangleIndices[] array;
                if (n == 0) array = EmptyArray<TriangleIndices>.Value;
                else
                {
                    array = new TriangleIndices[n];
                    b.DeserializeStructArray(array);
                }
                Triangles = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Vertices = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                VertexNormals = array;
            }
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.ColorRGBA[] array;
                if (n == 0) array = EmptyArray<StdMsgs.ColorRGBA>.Value;
                else
                {
                    array = new StdMsgs.ColorRGBA[n];
                    b.DeserializeStructArray(array);
                }
                VertexColors = array;
            }
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.ColorRGBA[] array;
                if (n == 0) array = EmptyArray<StdMsgs.ColorRGBA>.Value;
                else
                {
                    array = new StdMsgs.ColorRGBA[n];
                    b.DeserializeStructArray(array);
                }
                TriangleColors = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                VertexTextureCoords = array;
            }
            {
                int n = b.DeserializeArrayLength();
                MeshMsgs.MeshMaterial[] array;
                if (n == 0) array = EmptyArray<MeshMsgs.MeshMaterial>.Value;
                else
                {
                    array = new MeshMsgs.MeshMaterial[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshMsgs.MeshMaterial(ref b);
                    }
                }
                FaceMaterials = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                SensorMsgs.Image[] array;
                if (n == 0) array = EmptyArray<SensorMsgs.Image>.Value;
                else
                {
                    array = new SensorMsgs.Image[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new SensorMsgs.Image(ref b);
                    }
                }
                Textures = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                MeshMsgs.MeshFaceCluster[] array;
                if (n == 0) array = EmptyArray<MeshMsgs.MeshFaceCluster>.Value;
                else
                {
                    array = new MeshMsgs.MeshFaceCluster[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshMsgs.MeshFaceCluster(ref b);
                    }
                }
                Clusters = array;
            }
        }
        
        public TriangleMesh RosDeserialize(ref ReadBuffer b) => new TriangleMesh(ref b);
        
        public TriangleMesh RosDeserialize(ref ReadBuffer2 b) => new TriangleMesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Triangles.Length);
            b.SerializeStructArray(Triangles);
            b.Serialize(Vertices.Length);
            b.SerializeStructArray(Vertices);
            b.Serialize(VertexNormals.Length);
            b.SerializeStructArray(VertexNormals);
            b.Serialize(VertexColors.Length);
            b.SerializeStructArray(VertexColors);
            b.Serialize(TriangleColors.Length);
            b.SerializeStructArray(TriangleColors);
            b.Serialize(VertexTextureCoords.Length);
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
            b.Align4();
            b.Serialize(Triangles.Length);
            b.SerializeStructArray(Triangles);
            b.Serialize(Vertices.Length);
            b.Align8();
            b.SerializeStructArray(Vertices);
            b.Serialize(VertexNormals.Length);
            b.Align8();
            b.SerializeStructArray(VertexNormals);
            b.Serialize(VertexColors.Length);
            b.SerializeStructArray(VertexColors);
            b.Serialize(TriangleColors.Length);
            b.SerializeStructArray(TriangleColors);
            b.Serialize(VertexTextureCoords.Length);
            b.Align8();
            b.SerializeStructArray(VertexTextureCoords);
            b.Serialize(FaceMaterials.Length);
            foreach (var t in FaceMaterials)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(Textures.Length);
            foreach (var t in Textures)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(Clusters.Length);
            foreach (var t in Clusters)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Triangles, nameof(Triangles));
            BuiltIns.ThrowIfNull(Vertices, nameof(Vertices));
            BuiltIns.ThrowIfNull(VertexNormals, nameof(VertexNormals));
            BuiltIns.ThrowIfNull(VertexColors, nameof(VertexColors));
            BuiltIns.ThrowIfNull(TriangleColors, nameof(TriangleColors));
            BuiltIns.ThrowIfNull(VertexTextureCoords, nameof(VertexTextureCoords));
            BuiltIns.ThrowIfNull(FaceMaterials, nameof(FaceMaterials));
            BuiltIns.ThrowIfNull(Textures, nameof(Textures));
            BuiltIns.ThrowIfNull(Clusters, nameof(Clusters));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 36;
                size += 12 * Triangles.Length;
                size += 24 * Vertices.Length;
                size += 24 * VertexNormals.Length;
                size += 16 * VertexColors.Length;
                size += 16 * TriangleColors.Length;
                size += 24 * VertexTextureCoords.Length;
                size += 21 * FaceMaterials.Length;
                foreach (var msg in Textures) size += msg.RosMessageLength;
                foreach (var msg in Clusters) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Triangles.Length
            size += 12 * Triangles.Length;
            size += 4; // Vertices.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * Vertices.Length;
            size += 4; // VertexNormals.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * VertexNormals.Length;
            size += 4; // VertexColors.Length
            size += 16 * VertexColors.Length;
            size += 4; // TriangleColors.Length
            size += 16 * TriangleColors.Length;
            size += 4; // VertexTextureCoords.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * VertexTextureCoords.Length;
            size += 4; // FaceMaterials.Length
            size += (21 + 3) * FaceMaterials.Length - 3;
            size = WriteBuffer2.Align4(size);
            size += 4; // Textures.Length
            foreach (var msg in Textures) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Clusters.Length
            foreach (var msg in Clusters) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/TriangleMesh";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b112c5b670c2c3e8b1571aae11ccc3da";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
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
    
        public Serializer<TriangleMesh> CreateSerializer() => new Serializer();
        public Deserializer<TriangleMesh> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TriangleMesh>
        {
            public override void RosSerialize(TriangleMesh msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TriangleMesh msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TriangleMesh msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TriangleMesh msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(TriangleMesh msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TriangleMesh>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TriangleMesh msg) => msg = new TriangleMesh(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TriangleMesh msg) => msg = new TriangleMesh(ref b);
        }
    }
}
