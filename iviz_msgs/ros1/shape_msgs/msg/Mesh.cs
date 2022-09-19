/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract]
    public sealed class Mesh : IHasSerializer<Mesh>, IMessage
    {
        // Definition of a mesh
        // list of triangles; the index values refer to positions in vertices[]
        [DataMember (Name = "triangles")] public MeshTriangle[] Triangles;
        // the actual vertices that make up the mesh
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
    
        public Mesh()
        {
            Triangles = EmptyArray<MeshTriangle>.Value;
            Vertices = EmptyArray<GeometryMsgs.Point>.Value;
        }
        
        public Mesh(MeshTriangle[] Triangles, GeometryMsgs.Point[] Vertices)
        {
            this.Triangles = Triangles;
            this.Vertices = Vertices;
        }
        
        public Mesh(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshTriangle>.Value
                    : new MeshTriangle[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshTriangle(ref b);
                }
                Triangles = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(ref Unsafe.As<GeometryMsgs.Point, byte>(ref array[0]), n * 24);
                }
                Vertices = array;
            }
        }
        
        public Mesh(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshTriangle>.Value
                    : new MeshTriangle[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshTriangle(ref b);
                }
                Triangles = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(ref Unsafe.As<GeometryMsgs.Point, byte>(ref array[0]), n * 24);
                }
                Vertices = array;
            }
        }
        
        public Mesh RosDeserialize(ref ReadBuffer b) => new Mesh(ref b);
        
        public Mesh RosDeserialize(ref ReadBuffer2 b) => new Mesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Triangles.Length);
            foreach (var t in Triangles)
            {
                t.RosSerialize(ref b);
            }
            b.SerializeStructArray(Vertices);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Triangles.Length);
            foreach (var t in Triangles)
            {
                t.RosSerialize(ref b);
            }
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += 12 * Triangles.Length;
                size += 24 * Vertices.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Triangles.Length
            size += 12 * Triangles.Length;
            size += 4; // Vertices.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * Vertices.Length;
            return size;
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
    
        public Serializer<Mesh> CreateSerializer() => new Serializer();
        public Deserializer<Mesh> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Mesh>
        {
            public override void RosSerialize(Mesh msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Mesh msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Mesh msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Mesh msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Mesh msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Mesh>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Mesh msg) => msg = new Mesh(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Mesh msg) => msg = new Mesh(ref b);
        }
    }
}
