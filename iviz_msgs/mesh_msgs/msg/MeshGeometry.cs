/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshGeometry")]
    public sealed class MeshGeometry : IDeserializable<MeshGeometry>, IMessage
    {
        // Mesh Geometry Message
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices { get; set; }
        [DataMember (Name = "vertex_normals")] public GeometryMsgs.Point[] VertexNormals { get; set; }
        [DataMember (Name = "faces")] public MeshMsgs.TriangleIndices[] Faces { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshGeometry()
        {
            Vertices = System.Array.Empty<GeometryMsgs.Point>();
            VertexNormals = System.Array.Empty<GeometryMsgs.Point>();
            Faces = System.Array.Empty<MeshMsgs.TriangleIndices>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshGeometry(GeometryMsgs.Point[] Vertices, GeometryMsgs.Point[] VertexNormals, MeshMsgs.TriangleIndices[] Faces)
        {
            this.Vertices = Vertices;
            this.VertexNormals = VertexNormals;
            this.Faces = Faces;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshGeometry(ref Buffer b)
        {
            Vertices = b.DeserializeStructArray<GeometryMsgs.Point>();
            VertexNormals = b.DeserializeStructArray<GeometryMsgs.Point>();
            Faces = b.DeserializeArray<MeshMsgs.TriangleIndices>();
            for (int i = 0; i < Faces.Length; i++)
            {
                Faces[i] = new MeshMsgs.TriangleIndices(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshGeometry(ref b);
        }
        
        MeshGeometry IDeserializable<MeshGeometry>.RosDeserialize(ref Buffer b)
        {
            return new MeshGeometry(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Vertices, 0);
            b.SerializeStructArray(VertexNormals, 0);
            b.SerializeArray(Faces, 0);
        }
        
        public void RosValidate()
        {
            if (Vertices is null) throw new System.NullReferenceException(nameof(Vertices));
            if (VertexNormals is null) throw new System.NullReferenceException(nameof(VertexNormals));
            if (Faces is null) throw new System.NullReferenceException(nameof(Faces));
            for (int i = 0; i < Faces.Length; i++)
            {
                if (Faces[i] is null) throw new System.NullReferenceException($"{nameof(Faces)}[{i}]");
                Faces[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += 24 * Vertices.Length;
                size += 24 * VertexNormals.Length;
                size += 12 * Faces.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshGeometry";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9a7ed3efa2a35ef81abaf7dcc675ed20";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VRsQrCQAzd7ysCDo6CFQfBTSgOBcFuRcpRc22gzZXLKa1f7xVLdVAnzfSSvISXvBkk" +
                "KBXEaBv0rh8y0SWqcizkjZSyOFhin53gis5TgfKljV3O1jW6FtWEzQ9C6khzWeOez8N4oBo9rFHbH4dK" +
                "jvEG3qhTM0grEigse00s4CuE1gp5sgzWgA5Z4AExGIcI0gaFytRW+/UKugn1E7r9S/7Hv4UjdmiIX0T7" +
                "kTGXpzmXcEe0zKLJEBrH1R0jxS0C7gEAAA==";
                
    }
}
