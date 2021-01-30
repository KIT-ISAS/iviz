/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/BoundingVolume")]
    public sealed class BoundingVolume : IDeserializable<BoundingVolume>, IMessage
    {
        // Define a volume in 3D
        // A set of solid geometric primitives that make up the volume to define (as a union)
        [DataMember (Name = "primitives")] public ShapeMsgs.SolidPrimitive[] Primitives { get; set; }
        // The poses at which the primitives are located
        [DataMember (Name = "primitive_poses")] public GeometryMsgs.Pose[] PrimitivePoses { get; set; }
        // In addition to primitives, meshes can be specified to add to the bounding volume (again, as union)
        [DataMember (Name = "meshes")] public ShapeMsgs.Mesh[] Meshes { get; set; }
        // The poses at which the meshes are located
        [DataMember (Name = "mesh_poses")] public GeometryMsgs.Pose[] MeshPoses { get; set; }
    
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
        public BoundingVolume(ref Buffer b)
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
                foreach (var i in Primitives)
                {
                    size += i.RosMessageLength;
                }
                size += 56 * PrimitivePoses.Length;
                foreach (var i in Meshes)
                {
                    size += i.RosMessageLength;
                }
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
                "H4sIAAAAAAAAA71W227UMBB9z1eMxENbsVouRQiB9qHQhVaCUtoi9SK0cpPZjUViB9tZdvl6ZnxJsi0t" +
                "L9DuQ+N4fGbOmYvzCPZxLhWCgKWu2hpBKtjdz7JHsAcWHeg5WF3JAhaoa3RG5tAYWUsnl2jBlcJBLb4j" +
                "tA0tMIE4DUXA3RaWsFsltdrJbCkanNV2YZ+cMuhxQrr6NkBl52eE1WhLLsjBz1LmpYcfuBYGodK5cFhk" +
                "MbZ1gD6mc0PAmQdi1EMFoijopVYcYo82ghptSai5UHCNYBvM5VxiwWZ0hP+x/2vdqkKqReK5LRZCqhEQ" +
                "ydsUPxEkxRGQ7yEVXf+VENslLpN//Jd9Ov3wGu7MDwUf6+Rar0YkT4kGR5CvK6kKNPSkaY+LpqoCSuAT" +
                "isDLV4olMl1pehUJjDVH5QiuoBO8AU9H9BtnWSuVewVvP59PnsXn0+OD6cl08jwu3118PDzan55MdtOL" +
                "z0fTyYsktVs3yPXLGvuYohW/z5JRIWtUllJnN03nlRbu5QuSvbdIZ2oUisMfHhiYvQYUlFqSxDqhXBTB" +
                "eluWa8U9xout/swWkTdizR7ea+N3ibgPdeRX5yO4oDojeS6HMbPIvF2hWrgyRZRrY9A22qtMkFYW2PEj" +
                "0ce9trPzydPB6qLTmleXJPUwpKB/jEqrag2c9lzX5IqSCJLawGIR4pQOFnFIIBhRyJZDIDSvma+g8UZe" +
                "Zyd7+4dfTymeoc+UZI/JCfbubVAllA5UXJnkXBvJL6iSKu2Js80liJW0Y+DUGZzrqFjCnR1MDz8cnME2" +
                "Y8fFTs+JQEi3geI9J6rlRek6zWMvwDb3wk7wR6c7P4Fd9BMWAz93eSGETruQPmHxbp/vOCGsVNqi8zeG" +
                "5qAnadLl0uRtJcw4tIxs+hrymvJ5TUniem+bUVAWHkdRsxudGPXrSuoGeSquQafeMu6FYcP/M+JuT1Z/" +
                "1RlsqGMoD8JfDqQBD1pu1LlBLleR87zjbFGBx/1wkbDaofD82TFkxyxYZ5B9aWmmG+Vxe7uHIih9DZ+V" +
                "1B6UUkfXVajeLn7iIkKON+mmEQir7mndPf16mPB76RKHLlFUzBt6bgbPqx+97tT1NRXr/YzS088HuF75" +
                "2yBdqoM08BXPw6+SNgwWI4VaVGjfDG6PpahaZCXm1Pn8GRMzaZn0Eo2TOdqrbxn7OIsAdI91WFkcjCJ3" +
                "rai6E7c/5nw0f6gnAkuHHkiqROMPkiVaW7YPiufJ7vOr3RAnrmYkXIj2N2GRSwvwCgAA";
                
    }
}
