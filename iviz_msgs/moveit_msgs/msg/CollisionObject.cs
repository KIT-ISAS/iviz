/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class CollisionObject : IDeserializable<CollisionObject>, IMessage
    {
        // A header, used for interpreting the poses
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The id of the object (name used in MoveIt)
        [DataMember (Name = "id")] public string Id;
        // The object type in a database of known objects
        [DataMember (Name = "type")] public ObjectRecognitionMsgs.ObjectType Type;
        // The collision geometries associated with the object.
        // Their poses are with respect to the specified header
        // Solid geometric primitives
        [DataMember (Name = "primitives")] public ShapeMsgs.SolidPrimitive[] Primitives;
        [DataMember (Name = "primitive_poses")] public GeometryMsgs.Pose[] PrimitivePoses;
        // Meshes
        [DataMember (Name = "meshes")] public ShapeMsgs.Mesh[] Meshes;
        [DataMember (Name = "mesh_poses")] public GeometryMsgs.Pose[] MeshPoses;
        // Bounding planes (equation is specified, but the plane can be oriented using an additional pose)
        [DataMember (Name = "planes")] public ShapeMsgs.Plane[] Planes;
        [DataMember (Name = "plane_poses")] public GeometryMsgs.Pose[] PlanePoses;
        // Adds the object to the planning scene. If the object previously existed, it is replaced.
        public const byte ADD = 0;
        // Removes the object from the environment entirely (everything that matches the specified id)
        public const byte REMOVE = 1;
        // Append to an object that already exists in the planning scene. If the object does not exist, it is added.
        public const byte APPEND = 2;
        // If an object already exists in the scene, new poses can be sent (the geometry arrays must be left empty)
        // if solely moving the object is desired
        public const byte MOVE = 3;
        // Operation to be performed
        [DataMember (Name = "operation")] public byte Operation;
    
        /// Constructor for empty message.
        public CollisionObject()
        {
            Id = string.Empty;
            Type = new ObjectRecognitionMsgs.ObjectType();
            Primitives = System.Array.Empty<ShapeMsgs.SolidPrimitive>();
            PrimitivePoses = System.Array.Empty<GeometryMsgs.Pose>();
            Meshes = System.Array.Empty<ShapeMsgs.Mesh>();
            MeshPoses = System.Array.Empty<GeometryMsgs.Pose>();
            Planes = System.Array.Empty<ShapeMsgs.Plane>();
            PlanePoses = System.Array.Empty<GeometryMsgs.Pose>();
        }
        
        /// Explicit constructor.
        public CollisionObject(in StdMsgs.Header Header, string Id, ObjectRecognitionMsgs.ObjectType Type, ShapeMsgs.SolidPrimitive[] Primitives, GeometryMsgs.Pose[] PrimitivePoses, ShapeMsgs.Mesh[] Meshes, GeometryMsgs.Pose[] MeshPoses, ShapeMsgs.Plane[] Planes, GeometryMsgs.Pose[] PlanePoses, byte Operation)
        {
            this.Header = Header;
            this.Id = Id;
            this.Type = Type;
            this.Primitives = Primitives;
            this.PrimitivePoses = PrimitivePoses;
            this.Meshes = Meshes;
            this.MeshPoses = MeshPoses;
            this.Planes = Planes;
            this.PlanePoses = PlanePoses;
            this.Operation = Operation;
        }
        
        /// Constructor with buffer.
        internal CollisionObject(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Id = b.DeserializeString();
            Type = new ObjectRecognitionMsgs.ObjectType(ref b);
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
            Planes = b.DeserializeArray<ShapeMsgs.Plane>();
            for (int i = 0; i < Planes.Length; i++)
            {
                Planes[i] = new ShapeMsgs.Plane(ref b);
            }
            PlanePoses = b.DeserializeStructArray<GeometryMsgs.Pose>();
            Operation = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new CollisionObject(ref b);
        
        CollisionObject IDeserializable<CollisionObject>.RosDeserialize(ref Buffer b) => new CollisionObject(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Id);
            Type.RosSerialize(ref b);
            b.SerializeArray(Primitives);
            b.SerializeStructArray(PrimitivePoses);
            b.SerializeArray(Meshes);
            b.SerializeStructArray(MeshPoses);
            b.SerializeArray(Planes);
            b.SerializeStructArray(PlanePoses);
            b.Serialize(Operation);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
            Type.RosValidate();
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
            if (Planes is null) throw new System.NullReferenceException(nameof(Planes));
            for (int i = 0; i < Planes.Length; i++)
            {
                if (Planes[i] is null) throw new System.NullReferenceException($"{nameof(Planes)}[{i}]");
                Planes[i].RosValidate();
            }
            if (PlanePoses is null) throw new System.NullReferenceException(nameof(PlanePoses));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 29;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Id);
                size += Type.RosMessageLength;
                size += BuiltIns.GetArraySize(Primitives);
                size += 56 * PrimitivePoses.Length;
                size += BuiltIns.GetArraySize(Meshes);
                size += 56 * MeshPoses.Length;
                size += 32 * Planes.Length;
                size += 56 * PlanePoses.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/CollisionObject";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "568a161b26dc46c54a5a07621ce82cf3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1YbVMbORL+Pr9CtXwwXBwnG9itPV/xAWLvwlYIXuCuklCUS56RbR0aaSJpMN6r++/3" +
                "tDSaGQi5va26YCiwRq1+76dbs8OO2FrwQtghq50o2NJYJrUXtrLCS71ifi1YZZxw2Umga8izbIddYUsW" +
                "zCwDkVn8U+Se7WpeishLanZm7sSp38uct8RMFulcQ+23FXhoxlnBPV9wh50lu9VmoxsSl8X/cytys9LS" +
                "S6PnpVu5V+fh+RVxIDaJc26Ukg5UbCVMKSBYOMadM7nkHlptpF/3FB7FY9JGKxm3IpJY4aqgognUtJBL" +
                "CQadAy6Ngv1JTM4qK0soeAdnuTWvRNQzUM3S1vVNn6w5u42UM2jQ359Hx0PSmXDrh1zpCWjLuPEkH9rr" +
                "WBybWhcUhEpxDUN3xeeakzeZdJ11Q7aofQw6kbGca7aAr+BFZEWBwBILPORFEWLBVXDcXl+3GR0lQ4Kk" +
                "rxhJe512R0Xh+mnUeJ2oNEl0udBixE4f5Bpy9E6a2qktE/fSQb8hk57ssQInc1GMssXWC3Y0mRy+JjEX" +
                "okRGPpC0tKYMa6HvpDW6hKH47qUV4Lsr7oTd+nUsBe5ZyX0Ojz/KCVnsRUkX07Pzf0wPvw82VZXQBZkC" +
                "fyW7iAdXFjnUKO0o///Y1sJAqDY+Hkp2IgqdkbPZ9P3k8A2JxtlO5tPigpQh02LTZH4Takf27xJFihtq" +
                "wvKtY2XtPFEosYQaZeW3exAll8wZRb6CaxNiNJKhYSEcPFlEFYNv9knB80rYmH1wD3hiCegpE6FJ21l2" +
                "+H/+ZGeXv4yZ80VMx4hqVMye64LbAmXjOaFRwMK1XK2FfamQBgqHeFkh2mGXMMdF9ICZ+F3Bn5YrOCKg" +
                "H+zKTVnWWubAHeYlyrF/nlxHwFdx62VeK25BbyxKlMiXFjBK3PHrUKlC54KdTsag0U7kNcEDJEmdI7ah" +
                "Jk8nLKsB3ftv6EC2c7UxLwnJV8DsVnjMPygr7lE8jvTkbgwZf4nGjcAbzgHUatTjbng2x9LtMQiBCqIy" +
                "+ZrtQvMZqoLCh2jfcSv5QgHJKYuUAtcBHRrs9TiT2mOmuTaJfeTYyfhf2OqWL9n0co2YqVAz9QoOBGFl" +
                "kYUoCrbYBia5IuRiSi4st9uMTkWR2c7P5GMQUZZSROQXfSL1rRCNOXWvb5ONf9zisp0//WHnx79O315R" +
                "Yvz5w82HSvWt0Z5LTcBBJRqrli9M0ydCC0fTpnpBh0ltFWWBQDO/MUgnoA4okOlwI2YLoKdZCRy2DOWB" +
                "5EYmF2Ip0W+4hsTIIjX0W7FNM0Zfwjg8ieeBv4TYgGKaXsLGCgWiWbFIIQSXxLBY9FVx3gCfEHlY8Ovl" +
                "+ftXqNpUGx+Pzt6xyGDEjlpARdNv4bjktwEzYStlTPJKbiwNEMhznIXcNN6M2HS0GgG+9RNBD6hOMKyM" +
                "uUXK3oox++5fA/LwYDx4a+p8PTkeDNnAGuPxZO19NX71ShlUB7ztB//+LpqIGQbJrAmBNNzQ9OkYvQBr" +
                "UBQK97yAOoLwAQ5J1CSw61aIBi2WCo1jIZX022ZYeipfYbCITgw4iGlochxzIzAhq6gLNZIRKVU4Sq4a" +
                "fqKGe4+SVCJg0c9QsDGWliywGbPWAeEZuQDPHrtg/MNffzqIFDQIQksoB7ovNR40ki5/e8cQNifWRhVt" +
                "nB4IvvysThJF5B1EscFm5fZ/jE8qY/Hkh4P9N2EJaksEUimzaSgALhsA/KPHNC+TIUnAvJl7425pilrR" +
                "PtRSwptqkBIaqf2teuPXZldoNIllujD3Q4w/lGlDlm8BweEKgXQT1LGOlIpc4jwdazu0xDW/o4ygeXuR" +
                "BlIwo/GDBsxQiTZU+eshfkZZ6Gk/sePzDxiq4vfL2cn0YopBJy7ffnx3+n4yvcBg0Tw4fz89PEjVnvAp" +
                "zDykU0MV7wwJEtAWNF0a3EPSpTLc/3iAebWjSGdKwcO41j/QIxszwdEqqV9jsvCNE+LgSO66T0g16M4M" +
                "4qiVNalJuzA8qDoMqw9D9nEYivVTX2dyMm0roVe4ujQaPcYgB5Rs7YPTR51v5x8wH3erj62vafWJZsqe" +
                "StH/jVZGA7wp7ASb+A9LAT00AUU9gWgExdFuywtZkwrUv8MATRmU9Ih85xdHk9O/X9K83pOZghx4UoDj" +
                "/BW9ElMHgAk1ILy9rHBlguFE84lxjL8jFuERGCge8J2fTE9/Obliu8S7Wex1NlFTWvY93tmEXF6tfevz" +
                "phbYLtXCXpRHOJfkROsaOXHRk/M1KeDQ+i6Gr7kqPy0TLZtaadFu4Xx363xckxi/c2nDCEptm2bVqsuh" +
                "4FM6bxAkyve6GkbPsheNU1ORPnJmm1KPjKfbUVepXxB3jiHCbwNxX15JCbXo2oiKQRzikEMzC3UrFOrS" +
                "CkpX3CmHMVo0HsT90E2Ct2PihbMjls3IYS1B9htu28LqwLejey4DoUq6quRpnmte7UT9wwQXYvzQ3ASB" +
                "7L79tm2//f486neuSza0gaLJpe/Ph8rT6nPndxqAkKz/3aL0bfMM7ZVe4qSm2gsDvbgh8MN7rAgsuAfp" +
                "Faajv/W6xx1XNWoZWEYXPNNGMlzw8dICl0rhrm8yknHVMEAfa3mRAOLGc19jOEwn0lsOTLU1DYAiavNE" +
                "PoFZOvRMrkpmPOGyZBYm2FapeBu+3o96ivs5HPdM2s7o7VZ43/QYUFBktEevW9vXq+FNW/s6jt+zF3R7" +
                "fcHy3/GnYIcsvLvibHyI4hXL69c3WC7a5fe0zNvlG1oW7XL/ps3464Ob8CzL/gP5Tbse+BUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
