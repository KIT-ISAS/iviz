/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshGeometry : IDeserializableRos1<MeshGeometry>, IMessageRos1
    {
        // Mesh Geometry Message
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
        [DataMember (Name = "vertex_normals")] public GeometryMsgs.Point[] VertexNormals;
        [DataMember (Name = "faces")] public MeshMsgs.TriangleIndices[] Faces;
    
        /// Constructor for empty message.
        public MeshGeometry()
        {
            Vertices = System.Array.Empty<GeometryMsgs.Point>();
            VertexNormals = System.Array.Empty<GeometryMsgs.Point>();
            Faces = System.Array.Empty<MeshMsgs.TriangleIndices>();
        }
        
        /// Explicit constructor.
        public MeshGeometry(GeometryMsgs.Point[] Vertices, GeometryMsgs.Point[] VertexNormals, MeshMsgs.TriangleIndices[] Faces)
        {
            this.Vertices = Vertices;
            this.VertexNormals = VertexNormals;
            this.Faces = Faces;
        }
        
        /// Constructor with buffer.
        public MeshGeometry(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Vertices);
            b.DeserializeStructArray(out VertexNormals);
            b.DeserializeArray(out Faces);
            for (int i = 0; i < Faces.Length; i++)
            {
                Faces[i] = new MeshMsgs.TriangleIndices(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshGeometry(ref b);
        
        public MeshGeometry RosDeserialize(ref ReadBuffer b) => new MeshGeometry(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(VertexNormals);
            b.SerializeArray(Faces);
        }
        
        public void RosValidate()
        {
            if (Vertices is null) BuiltIns.ThrowNullReference();
            if (VertexNormals is null) BuiltIns.ThrowNullReference();
            if (Faces is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Faces.Length; i++)
            {
                if (Faces[i] is null) BuiltIns.ThrowNullReference(nameof(Faces), i);
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshGeometry";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9a7ed3efa2a35ef81abaf7dcc675ed20";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VRsQrCQAzd7ysCDo6CFQfBTSgOBcFuRcpRc22gzZXLKa1f7xVLdVAnzfSSvISXvBkk" +
                "KBXEaBv0rh8y0SWqcizkjZSyOFhin53gis5TgfKljV3O1jW6FtWEzQ9C6khzWeOez8N4oBo9rFHbH4dK" +
                "jvEG3qhTM0grEigse00s4CuE1gp5sgzWgA5Z4AExGIcI0gaFytRW+/UKugn1E7r9S/7Hv4UjdmiIX0T7" +
                "kTGXpzmXcEe0zKLJEBrH1R0jxS0C7gEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
