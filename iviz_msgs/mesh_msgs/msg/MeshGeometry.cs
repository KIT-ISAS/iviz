/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshGeometry")]
    public sealed class MeshGeometry : IDeserializable<MeshGeometry>, IMessage
    {
        // Mesh Geometry Message
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
        [DataMember (Name = "vertex_normals")] public GeometryMsgs.Point[] VertexNormals;
        [DataMember (Name = "faces")] public MeshMsgs.TriangleIndices[] Faces;
    
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
        internal MeshGeometry(ref Buffer b)
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
                "H4sIAAAAAAAACrVRPQvCQAzdD/ofAg6OghUHwU0Qh4Kgm4gcmmsDvVy5nKL+eu9oqQ7qpJny8fJ4LxlA" +
                "gVLBEp3F4G+pEl2iKrvGwUopo7UjDrs9XNAHOqJ8GeP1wM5bXYuykbkFbD1pLmtc8SmtR6jRiSZT8x9H" +
                "porNcgZv9GVqANuKBI6OgyYWCBVC44QCOQZnQMcqAoEYjEcEaaJIZWqnw3QC1z679dn9fw4+Hi/5WKAh" +
                "ftEdOshQni86Ryv5eJf3b6F2P0p+AAkSDCr1AQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
