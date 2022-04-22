/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Mesh : IDeserializable<Mesh>, IMessage
    {
        // Definition of a mesh
        // list of triangles; the index values refer to positions in vertices[]
        [DataMember (Name = "triangles")] public MeshTriangle[] Triangles;
        // the actual vertices that make up the mesh
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
    
        /// Constructor for empty message.
        public Mesh()
        {
            Triangles = System.Array.Empty<MeshTriangle>();
            Vertices = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// Explicit constructor.
        public Mesh(MeshTriangle[] Triangles, GeometryMsgs.Point[] Vertices)
        {
            this.Triangles = Triangles;
            this.Vertices = Vertices;
        }
        
        /// Constructor with buffer.
        public Mesh(ref ReadBuffer b)
        {
            Triangles = b.DeserializeArray<MeshTriangle>();
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new MeshTriangle(ref b);
            }
            Vertices = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Mesh(ref b);
        
        public Mesh RosDeserialize(ref ReadBuffer b) => new Mesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Triangles);
            b.SerializeStructArray(Vertices);
        }
        
        public void RosValidate()
        {
            if (Triangles is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Triangles.Length; i++)
            {
                if (Triangles[i] is null) BuiltIns.ThrowNullReference($"{nameof(Triangles)}[{i}]");
                Triangles[i].RosValidate();
            }
            if (Vertices is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + 12 * Triangles.Length + 24 * Vertices.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "shape_msgs/Mesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1ffdae9486cd3316a121c578b47a85cc";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7WRwWrDMAyG734KQQ87Dtaxw8Zug50Kg/UWShGpnIg5trGUku7pZydNOkqPm09Cln59" +
                "+rWCN7LsWTl4CBYQOpLWmBU4Fi0ZTYy+cSQvoC0B+wMNcETXk0AiSwk0QAwySkj+hyMl5Zqk2plNFtue" +
                "BardRasMKGpYa49u6cg5VOjwi6CPY8FI01DoSNNp30kj9x+BvWaxucmY1z9+ZvP5/gzSYqRp5O81Mvm1" +
                "ZfNad3KB6jPk+qFaT5w07LNx/0l7w6NMum1ZoA5ekb2Mhs6XmshjqSs3s4kIJGJNxrqA+vQIwxKdlujb" +
                "mB+gQ/ZdMgIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
