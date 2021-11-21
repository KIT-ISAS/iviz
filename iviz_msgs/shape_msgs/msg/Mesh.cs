/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [Preserve, DataContract (Name = "shape_msgs/Mesh")]
    public sealed class Mesh : IDeserializable<Mesh>, IMessage
    {
        // Definition of a mesh
        // list of triangles; the index values refer to positions in vertices[]
        [DataMember (Name = "triangles")] public MeshTriangle[] Triangles;
        // the actual vertices that make up the mesh
        [DataMember (Name = "vertices")] public GeometryMsgs.Point[] Vertices;
    
        /// <summary> Constructor for empty message. </summary>
        public Mesh()
        {
            Triangles = System.Array.Empty<MeshTriangle>();
            Vertices = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Mesh(MeshTriangle[] Triangles, GeometryMsgs.Point[] Vertices)
        {
            this.Triangles = Triangles;
            this.Vertices = Vertices;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Mesh(ref Buffer b)
        {
            Triangles = b.DeserializeArray<MeshTriangle>();
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new MeshTriangle(ref b);
            }
            Vertices = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Mesh(ref b);
        }
        
        Mesh IDeserializable<Mesh>.RosDeserialize(ref Buffer b)
        {
            return new Mesh(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Triangles, 0);
            b.SerializeStructArray(Vertices, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "shape_msgs/Mesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1ffdae9486cd3316a121c578b47a85cc";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrWRwUrEQAyG7wN9h8AePAqueFC8CZ4WBPdWliXUTBuczgyTdOn69M60tiviUecUMsmf" +
                "L3828ESWPSsHD8ECQk/SGbMBx6Ilo4nRt47kAbQjYP9GI5zQDSSQyFICDRCDTBKS/+FESbkhqQ9ml8X2" +
                "XwL14aJVBhQ1bHRAt3bkHCr0+E4wxKlgomkp9KTpfOylleuXwF6z2NJkKvP4x68yu9fne5AOI81Dvy9S" +
                "Zfifri2bXcmFa8ic25t6O6PSeMze/S/wL0YV2H3HAk3wipwvVGxd7jXDx1JYLmcTEUjEhox1AfXuFsY1" +
                "Oq/RR97gE6lpooA5AgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
