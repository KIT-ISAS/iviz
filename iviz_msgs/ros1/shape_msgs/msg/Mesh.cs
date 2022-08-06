/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract]
    public sealed class Mesh : IDeserializable<Mesh>, IMessage
    {
        // Definition of a mesh
        // list of triangles; the index values refer to positions in vertices[]
        [DataMember (Name = "triangles")] public MeshTriangle[] Triangles;
        // the actual vertices that make up the mesh
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
    
        public Mesh()
        {
            Triangles = System.Array.Empty<MeshTriangle>();
            Vertices = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        public Mesh(MeshTriangle[] Triangles, GeometryMsgs.Point[] Vertices)
        {
            this.Triangles = Triangles;
            this.Vertices = Vertices;
        }
        
        public Mesh(ref ReadBuffer b)
        {
            b.DeserializeArray(out Triangles);
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new MeshTriangle(ref b);
            }
            b.DeserializeStructArray(out Vertices);
        }
        
        public Mesh(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Triangles);
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new MeshTriangle(ref b);
            }
            b.DeserializeStructArray(out Vertices);
        }
        
        public Mesh RosDeserialize(ref ReadBuffer b) => new Mesh(ref b);
        
        public Mesh RosDeserialize(ref ReadBuffer2 b) => new Mesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Triangles);
            b.SerializeStructArray(Vertices);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Triangles);
            b.SerializeStructArray(Vertices);
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
        }
    
        public int RosMessageLength => 8 + 12 * Triangles.Length + 24 * Vertices.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.Align4(c);
            c += 4;  /* Triangles length */
            c += 12 * Triangles.Length;
            c += 4;  /* Vertices length */
            c = WriteBuffer2.Align8(c);
            c += 24 * Vertices.Length;
            return c;
        }
    
        public const string MessageType = "shape_msgs/Mesh";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1ffdae9486cd3316a121c578b47a85cc";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7WRwWrDMAyG734KQQ87Dtaxw8Zug50Kg/UWShGpnIg5trGUku7pZydNOkqPm09Cln59" +
                "+rWCN7LsWTl4CBYQOpLWmBU4Fi0ZTYy+cSQvoC0B+wMNcETXk0AiSwk0QAwySkj+hyMl5Zqk2plNFtue" +
                "BardRasMKGpYa49u6cg5VOjwi6CPY8FI01DoSNNp30kj9x+BvWaxucmY1z9+ZvP5/gzSYqRp5O81Mvm1" +
                "ZfNad3KB6jPk+qFaT5w07LNx/0l7w6NMum1ZoA5ekb2Mhs6XmshjqSs3s4kIJGJNxrqA+vQIwxKdlujb" +
                "mB+gQ/ZdMgIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
