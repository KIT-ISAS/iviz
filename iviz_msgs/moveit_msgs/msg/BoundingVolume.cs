/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public BoundingVolume()
        {
            Primitives = System.Array.Empty<ShapeMsgs.SolidPrimitive>();
            PrimitivePoses = System.Array.Empty<GeometryMsgs.Pose>();
            Meshes = System.Array.Empty<ShapeMsgs.Mesh>();
            MeshPoses = System.Array.Empty<GeometryMsgs.Pose>();
        }
        
        /// Explicit constructor.
        public BoundingVolume(ShapeMsgs.SolidPrimitive[] Primitives, GeometryMsgs.Pose[] PrimitivePoses, ShapeMsgs.Mesh[] Meshes, GeometryMsgs.Pose[] MeshPoses)
        {
            this.Primitives = Primitives;
            this.PrimitivePoses = PrimitivePoses;
            this.Meshes = Meshes;
            this.MeshPoses = MeshPoses;
        }
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new BoundingVolume(ref b);
        
        BoundingVolume IDeserializable<BoundingVolume>.RosDeserialize(ref Buffer b) => new BoundingVolume(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Primitives);
            b.SerializeStructArray(PrimitivePoses);
            b.SerializeArray(Meshes);
            b.SerializeStructArray(MeshPoses);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/BoundingVolume";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "22db94010f39e9198032cb4a1aeda26e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W227cNhB911cMkAfb6GKbxkFRpNiHNHZjA03i2i7gC4IFLc2uiEqiSlKb3X59z5Ci" +
                "JNtx+tLaBmxRHJ45c+ZCvaAjXumGSdHGVF3NpBs6PMqyF/SWHHsyK3Km0gWt2dTsrc6ptbrWXm/YkS+V" +
                "p1r9ydS1WHAC8YaKiLuvHLC7RpvmIHOlanlZu7X7/kJAzxLS7ecJqji/BFZrHFzAwZdS52WAn7hWlqky" +
                "ufJcZD23XYQ+w7kp4DIACeppQ6oo8NI0QnFEm1HNrgRqrhq6Y3It53qluRAzHJF/4v/OdE2hm3WKc1+t" +
                "lW5mhCAfh/gBkOARkb8RVO/6XwMSuxTL4j/+yT5cvH9DT+YnG+rkzmxnkKdkyzPKd5VuCrZ4MtiToqmq" +
                "iBLjiUUQ5CvVBnVRsrajigATzbnxgCtwQjbo5Qy/8yzrdON/ol8+XS1+6J8vzk6Oz48Xr/rlu+vfTj8e" +
                "HZ8vDtOLTx+PF6+T1H7XstSvaBw49VbyPktGha65cUidu2+6qozyP76G7KNFOlOzaoT+9MDE7A2xQmoh" +
                "ifOq8b0I0izSXQVvpcdksTee2UPwVu3Ew6/Ghl0EHqjOwupqRteoM8hzM+UsIst2xc3al4lRbqxl15qg" +
                "MiCdLuC/34To81Hb5dXi5WR1PWgtqxtIPaUU9e9Zmaba4Y94q+EKSSSNNnBcRJ7a07ofEkxWFboTCkAL" +
                "moUKSjwi7vL87dHpHxfgM/WZkhwwJcHBPTo2BBpKh1CFkBbwVssLVFJlQuBic0Nqq92cJHWWV6ZXLOEu" +
                "T45P359c0r5g94uDMSaAQLeJ4mNMqOV1GSZkoNL3Au1LLxxEfzg9+InR9X7iYuLnKS9AGLSL6VOOn/b5" +
                "ThIiSqUtnH8wNCc9iUmXa5t3lbLz2DK6HWsoaCrnDZIk9d61s6gsfdeLmpr0gZhDST0IHsU16dRHxqMw" +
                "Yvj/jLjHkzVcdZZbdAzyoMLlAA1k0EqjrixLuapc5p1kCwXe78eLRNSOhRfOzik7E8EGg+z3DjPdyvUw" +
                "tXuuAEEljC20B1LqcV3F6h34IxYVc3w/3DQCaTs87Yanv5+H/ihdimFIFIr5np73ycvqr1F3dH2NYv12" +
                "ROnpyzNcr/JtkC7VSRrkipfhV2kXB4vVqllX7H6e3B4bVXXoZcwydL58xvSZdBL0hq3XObvbz5n4uOwB" +
                "cI/5hCUOBE3lvlPVcOLxx1xg85V6Alg69ExSpTC+IlkKa8+NpGSeHL66PYw8ebuEcJHtP2GRSwvwCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
