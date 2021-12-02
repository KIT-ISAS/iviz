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
        internal Mesh(ref Buffer b)
        {
            Triangles = b.DeserializeArray<MeshTriangle>();
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new MeshTriangle(ref b);
            }
            Vertices = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Mesh(ref b);
        
        Mesh IDeserializable<Mesh>.RosDeserialize(ref Buffer b) => new Mesh(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Triangles);
            b.SerializeStructArray(Vertices);
        }
        
        public void RosValidate()
        {
            if (Triangles is null) throw new System.NullReferenceException(nameof(Triangles));
            for (int i = 0; i < Triangles.Length; i++)
            {
                if (Triangles[i] is null) throw new System.NullReferenceException($"{nameof(Triangles)}[{i}]");
                Triangles[i].RosValidate();
            }
            if (Vertices is null) throw new System.NullReferenceException(nameof(Vertices));
        }
    
        public int RosMessageLength => 8 + 12 * Triangles.Length + 24 * Vertices.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "shape_msgs/Mesh";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1ffdae9486cd3316a121c578b47a85cc";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrWRwWrDMAyG73oKQQ49Dtaxw8Zug50Kg/UWShGZnJg5trGUkvbpZydLOtiOm09Cln59" +
                "+lXhMxvrrdrgMRgk7Fk6gAqdFS0ZTZZ861geUTtG6995xBO5gQUTG06oAWOQSULyP544qW1Y6gPsstj+" +
                "S6A+XLXKgKJGjQ7k1o6cI8WePhiHOBVMNC2HnjWdj720cvMarNcstjQBPP3xg93bywNKR5Hnkd/XgOqH" +
                "ZctaG7lCDRlye1tvZ04ej9m4/6T9xaNMuu+sYBO8ks23KYYul5rJY6krNzOJGSVSw2BcIL2/w3GNzmt0" +
                "AfgEoEP2XTICAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
