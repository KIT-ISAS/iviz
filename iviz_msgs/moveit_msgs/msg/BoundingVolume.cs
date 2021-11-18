/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/BoundingVolume")]
    public sealed class BoundingVolume : IDeserializable<BoundingVolume>, IMessage
    {
        // Define a volume in 3D
        // A set of solid geometric primitives that make up the volume to define (as a union)
        [DataMember (Name = "primitives")] public ShapeMsgs.SolidPrimitive[] Primitives;
        // The poses at which the primitives are located
        [DataMember (Name = "primitive_poses")] public GeometryMsgs.Pose[] PrimitivePoses;
        // In addition to primitives, meshes can be specified to add to the bounding volume (again, as union)
        [DataMember (Name = "meshes")] public ShapeMsgs.Mesh[] Meshes;
        // The poses at which the meshes are located
        [DataMember (Name = "mesh_poses")] public GeometryMsgs.Pose[] MeshPoses;
    
        /// <summary> Constructor for empty message. </summary>
        public BoundingVolume()
        {
            Primitives = System.Array.Empty<ShapeMsgs.SolidPrimitive>();
            PrimitivePoses = System.Array.Empty<GeometryMsgs.Pose>();
            Meshes = System.Array.Empty<ShapeMsgs.Mesh>();
            MeshPoses = System.Array.Empty<GeometryMsgs.Pose>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoundingVolume(ShapeMsgs.SolidPrimitive[] Primitives, GeometryMsgs.Pose[] PrimitivePoses, ShapeMsgs.Mesh[] Meshes, GeometryMsgs.Pose[] MeshPoses)
        {
            this.Primitives = Primitives;
            this.PrimitivePoses = PrimitivePoses;
            this.Meshes = Meshes;
            this.MeshPoses = MeshPoses;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal BoundingVolume(ref Buffer b)
        {
            Primitives = b.DeserializeArray<ShapeMsgs.SolidPrimitive>();
            for (int i = 0; i < Primitives.Length; i++)
            {
                Primitives[i] = new ShapeMsgs.SolidPrimitive(ref b);
            }
            PrimitivePoses = b.DeserializeStructArray<GeometryMsgs.Pose>();
            Meshes = b.DeserializeArray<ShapeMsgs.Mesh>();
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i] = new ShapeMsgs.Mesh(ref b);
            }
            MeshPoses = b.DeserializeStructArray<GeometryMsgs.Pose>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BoundingVolume(ref b);
        }
        
        BoundingVolume IDeserializable<BoundingVolume>.RosDeserialize(ref Buffer b)
        {
            return new BoundingVolume(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Primitives, 0);
            b.SerializeStructArray(PrimitivePoses, 0);
            b.SerializeArray(Meshes, 0);
            b.SerializeStructArray(MeshPoses, 0);
        }
        
        public void RosValidate()
        {
            if (Primitives is null) throw new System.NullReferenceException(nameof(Primitives));
            for (int i = 0; i < Primitives.Length; i++)
            {
                if (Primitives[i] is null) throw new System.NullReferenceException($"{nameof(Primitives)}[{i}]");
                Primitives[i].RosValidate();
            }
            if (PrimitivePoses is null) throw new System.NullReferenceException(nameof(PrimitivePoses));
            if (Meshes is null) throw new System.NullReferenceException(nameof(Meshes));
            for (int i = 0; i < Meshes.Length; i++)
            {
                if (Meshes[i] is null) throw new System.NullReferenceException($"{nameof(Meshes)}[{i}]");
                Meshes[i].RosValidate();
            }
            if (MeshPoses is null) throw new System.NullReferenceException(nameof(MeshPoses));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetArraySize(Primitives);
                size += 56 * PrimitivePoses.Length;
                size += BuiltIns.GetArraySize(Meshes);
                size += 56 * MeshPoses.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/BoundingVolume";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "22db94010f39e9198032cb4a1aeda26e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W227cNhB9F+B/GCAPttHFJo2DIkixD068iQ00iWO7gC8IFrQ0uyIqiSopbXb79T1D" +
                "UhfbcfrS2AZsURyemXPmQj2jI17qiknR2hRtyaQrOjhKkmd0SI4bMktyptAZrdiU3FidUm11qRu9ZkdN" +
                "rhoq1V9MbY0FdyCNoSzg7ikH7LbSptpPXK5qXpRu5Z6fC+hph3TzdYQqzi+AVRsHF3DwLddp7uFHrpVl" +
                "KkyqGs6SGNs2QJ/i3Bhw4YEE9aQilWV4aSoJcUCbUMkuB2qqKrplcjWneqk5EzMckX/i/9a0VaarVcdz" +
                "T62UriYEkg8pfgQk4gjIPyAVXf8nIbGLXHaS2f/8s5N8PP/whh7N0E7Sl8qt2UygUM6WJ5RuC11lbPFk" +
                "sCd1UxQBJlAKdeAVzNUapZGztoOQABPZuWoAl+GEbNCLCX6nSdLqqnlNbz9fzn6Nz+enx/Oz+exlXL67" +
                "+uPk09H8bHbQvfj8aT571andbGuWEhaZfUzRSt4nnVGmS64csufumi4Lo5rfXkH5waI7U7KqJPzxgZHZ" +
                "G2KF7EIS16iqiSJIv0iDZbyRNpPF7nBmF+St2oqH98b6XRD3oU786nJCVyg1yHM9jllElu2Cq1WTdxGl" +
                "xlp2tfEqA9LpDP7jJkSfDtouLmcvRqurXmtZXUPqcUhB/xiVqYot/oi3Eq6QRNLoBMdZiFM3tIpzgsmq" +
                "TLcSAtC8Zr6CujgC7uLs8Ojkz3PEM/bZJdljSoK9ezStJ+pLh1CFkBbwVssLVFJhPHGxuSa10W5KkjrL" +
                "SxMV63AXx/OTD8cXtCfYcbE/cAIIdBspPnBCLa9yPyR9KLEXaE96YT/4w+neT2AX/YTFyM9jXoDQaxfS" +
                "pxw/7vOdJESU6rZw/t7cHPUkhl2qbdoWyk5Dy+h6qCGvqZw3SJLUe1tPgrL0SxS1a9J7YvYldY88imvU" +
                "qQ+MB2Fg+NOm3MPxKuPtkCzXaBqkQvkrAjLIuJVeXVqWilWpjDxJGGo87ofrRAQPtefPTik5Fc16g+RL" +
                "i8lu5ZIY2z0dRwQjJC9yNAkS2+DeCjXcUwAdFTJ9l3E3CGnTP237p3+eisGgX0+jTxeq+o6qd+OX1d+D" +
                "+mj/ElX7Y1Ld07cnuWrlS6G/YEfJkBtfBmGhXRgyVqtqVbD7fXSTrFXRoq8x1zAF5Ksm5tMJ7zXbRqfs" +
                "br4m4uQiAuBO67GSOCRV2rSq6E88/Lbz0XynrADWHXoytToi31OtY7brhrhkvBy8vDkIofJmAe1iwP8C" +
                "M63J3gMLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
