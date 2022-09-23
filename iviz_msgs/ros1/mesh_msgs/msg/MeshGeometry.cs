/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshGeometry : IHasSerializer<MeshGeometry>, IMessage
    {
        // Mesh Geometry Message
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
        [DataMember (Name = "vertex_normals")] public GeometryMsgs.Point[] VertexNormals;
        [DataMember (Name = "faces")] public MeshMsgs.TriangleIndices[] Faces;
    
        public MeshGeometry()
        {
            Vertices = EmptyArray<GeometryMsgs.Point>.Value;
            VertexNormals = EmptyArray<GeometryMsgs.Point>.Value;
            Faces = EmptyArray<MeshMsgs.TriangleIndices>.Value;
        }
        
        public MeshGeometry(GeometryMsgs.Point[] Vertices, GeometryMsgs.Point[] VertexNormals, MeshMsgs.TriangleIndices[] Faces)
        {
            this.Vertices = Vertices;
            this.VertexNormals = VertexNormals;
            this.Faces = Faces;
        }
        
        public MeshGeometry(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Vertices = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                VertexNormals = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshMsgs.TriangleIndices>.Value
                    : new MeshMsgs.TriangleIndices[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.TriangleIndices(ref b);
                }
                Faces = array;
            }
        }
        
        public MeshGeometry(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Vertices = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                VertexNormals = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MeshMsgs.TriangleIndices>.Value
                    : new MeshMsgs.TriangleIndices[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MeshMsgs.TriangleIndices(ref b);
                }
                Faces = array;
            }
        }
        
        public MeshGeometry RosDeserialize(ref ReadBuffer b) => new MeshGeometry(ref b);
        
        public MeshGeometry RosDeserialize(ref ReadBuffer2 b) => new MeshGeometry(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(VertexNormals);
            b.Serialize(Faces.Length);
            foreach (var t in Faces)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(VertexNormals);
            b.Serialize(Faces.Length);
            foreach (var t in Faces)
            {
                t.RosSerialize(ref b);
            }
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
            get
            {
                int size = 12;
                size += 24 * Vertices.Length;
                size += 24 * VertexNormals.Length;
                size += 12 * Faces.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Vertices.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * Vertices.Length;
            size += 4; // VertexNormals.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * VertexNormals.Length;
            size += 4; // Faces.Length
            size += 12 * Faces.Length;
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshGeometry";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9a7ed3efa2a35ef81abaf7dcc675ed20";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VRsQrCQAzd7ysCDo6CFQfBTSgOBcFuRcpRc22gzZXLKa1f7xVLdVAnzfSSvISXvBkk" +
                "KBXEaBv0rh8y0SWqcizkjZSyOFhin53gis5TgfKljV3O1jW6FtWEzQ9C6khzWeOez8N4oBo9rFHbH4dK" +
                "jvEG3qhTM0grEigse00s4CuE1gp5sgzWgA5Z4AExGIcI0gaFytRW+/UKugn1E7r9S/7Hv4UjdmiIX0T7" +
                "kTGXpzmXcEe0zKLJEBrH1R0jxS0C7gEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshGeometry> CreateSerializer() => new Serializer();
        public Deserializer<MeshGeometry> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshGeometry>
        {
            public override void RosSerialize(MeshGeometry msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshGeometry msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshGeometry msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshGeometry msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshGeometry msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshGeometry>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshGeometry msg) => msg = new MeshGeometry(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshGeometry msg) => msg = new MeshGeometry(ref b);
        }
    }
}
