/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/CollisionObject")]
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
    
        /// <summary> Constructor for empty message. </summary>
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
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CollisionObject(ref b);
        }
        
        CollisionObject IDeserializable<CollisionObject>.RosDeserialize(ref Buffer b)
        {
            return new CollisionObject(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Id);
            Type.RosSerialize(ref b);
            b.SerializeArray(Primitives, 0);
            b.SerializeStructArray(PrimitivePoses, 0);
            b.SerializeArray(Meshes, 0);
            b.SerializeStructArray(MeshPoses, 0);
            b.SerializeArray(Planes, 0);
            b.SerializeStructArray(PlanePoses, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/CollisionObject";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "568a161b26dc46c54a5a07621ce82cf3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1YbW8bNxL+vkD+A9F8kH1RlDR2i54Kf7AjtXYRx6rtFkkMQ6B2KYk1l9yQXNvq4f77" +
                "PUMud9cvuV6Bi2XDFpfDeZ9nhvuc7bO14IWwQ1Y7UbClsUxqL2xlhZd6xfxasMo44bLDQNeQZ9lzdo4t" +
                "WTCzDERm8YfIPdvSvBSRl9Ts2FyLI7+dOW+JmSzSuYbabyrw0Iyzgnu+4A47S3alzY1uSFwW/8+tyM1K" +
                "Sy+Nnpdu5V6dhOfnxIHYJM65UUo6ULGVMKWAYOEYd87kkntodSP9uqfwKB6TNlrJuBWRxApXBRVNoKaF" +
                "XEow6BxwZhTsT2JyVllZQsFrOMuteSWinoFqlrYuLvtkzdlNpJxBg/7+PDoeko6FW9/lSk9AW8aNR/nQ" +
                "XsfiwNS6oCBUimsYuiU+15y8yaTrrBuyRe1j0ImM5VyzBXwFLyIrCgSWWOAhL4oQC66C47b7us3oKBkS" +
                "JH3BSNrrtNsvCtdPo8brRKVJosuFFiN2dCfXkKPX0tRObZi4lQ76DZn0ZI8VOJmLYpQtNl6w/clk7zWJ" +
                "ORUlMvKOpKU1ZVgLfS2t0SUMxXcvrQDfLXEt7MavYylwz0ruc3j8Xk7IYjtKOp0en/w+3fs22FRVQhdk" +
                "CvyV7CIeXFnkUKO0o/z/a1sLA6Ha+Hgo2YkodEbOZtP3k703JBpnO5mPiwtShkyLmybzm1A7sn+LKFLc" +
                "UBOWbxwra+eJQokl1Cgrv9mGKLlkzijyFVybEKORDA0L4eDJIqoYfLNDCp5Uwsbsg3vAE0tAT5kITdrO" +
                "nmV7/+fPs+z47Ocxc76ICRlx7RnVs+e64LZA5XhOgBTgcC1Xa2FfKmSCwileVgh42CXYcRFAYCl+V3Cp" +
                "5Qq+CAAI03JTlrWWOaCHeYmK7J8n7xH2Vdx6mdeKW9Abiyol8qUFkhJ3/DoUq9C5YEeTMWi0E3lNCAFJ" +
                "UucIbyjLownLaqD3zhs6kD0/vzEvCcxXgO1WeExBKCtuUT+O9ORuDBn/iMaNwBveAdpqlORWeDbH0m0z" +
                "CIEKojL5mm1B8xkKgyKIgF9zK/lCAcwpkZQC1wEdGmz3OJPaY6a5Nol95NjJ+F/Y6pYv2fRyjZipUDb1" +
                "Cg4EYWWRiKgLttgEJrki8GJKLiy3m4xORZHZ85/IxyCiRKWIyAetIrWuEI05GthXS8i/7nPI0b/9YScH" +
                "v0zfnlNu/P3DzYcK9q3RnktN8EGFGmuXL0zTLUIjR+umkkGfSc0VlYFYM39jkFHAHlAg2eFJTBjAULMS" +
                "OGwZKgT5jWQuxFKi63ANiZFFautXYpMmjb6EcXgSzwOFCbcByDTDhI0VakSzYpGiCC6JYbHoq+K8AUoh" +
                "+LDgl7OT969QuKk8Pu4fv2ORwYjtt7CK1t+CcsmvAnLCVkqa5JXcWBojkOo4C7lpyBmx6Wg1AojrR6Ie" +
                "sJ3AWBlzhay9EmP2zb8G5OHBePDW1Pl6cjAYsoE1xuPJ2vtq/OqVMigQeNsP/v1NNBGTDPJZEwhpuKHp" +
                "1jF6AdmgKBTueQGlBOEDHJIoS8DXlRANYCwV2sdCKuk3zcj0WMLCYBGdGKAQM9HkIOZGYEJWUS9qJCNS" +
                "qnCUXDX8RG33FlWpRICjn6BgYywtWWAzZq0DwjNyAZ7dd8H4u3/+sBspaByEllAOdA81HjSSzn59xxA2" +
                "J9ZGFW2c7gg++6wOE0XkHUSxwc3K7Xwfn1TG4sl3uztvwhLUlgikUuamoQC+3ADj7z2mqZkMSQLmzfQb" +
                "d0tT1Ir2oZYS3lSDlNBI7a/XIb80w1KvnMRKXZjbIeYgSrYhyzcA4nCXQMYJ6lv7SkU2cbCO5R0a45pf" +
                "U1LQ4L1IkymY0RxCk2YoRhsK/fUQP6MsdLYf2MHJB0xX8fvZ7HB6OsXEE5dvP747ej+ZnmLCaB6cvJ/u" +
                "7aaCTxAVhh/SqaGKl4eECmgOmm4P7i7pUhnuv9/F4NpRpDOl4GFu6x/okY2Z4GiY1LUxX/jGCXGCJHfd" +
                "JrAadGcGcebKmuykXRgeVB2G1Ych+zgM9fqprzM5mbaV0CvcYRqN7sOQA1C29sHpo8638w8YlLvVx9bX" +
                "tPpEw2VPpej/Riujgd8UdkJO/IelQB+ag6KeADVC42i35YWsSQXq4mGSpgxKekS+89P9ydFvZzS492Sm" +
                "IAeeFOA4hUWvxNQBZkINCG9vLVyZYDjRfGIcc/CIRYQEDIo7fOeH06OfD8/ZFvFuFtudTdSXln2PdzYh" +
                "l1dr3/q8qQW2RbWwHeUR1CU50bpGTlz05HxJCji0vovha+7Mj8tE16ZuWrRbON9dP+/XJObwXNowiFLn" +
                "pom16nIo+JTOGwSJ8r2uhtGz7EXj1FSk95zZptQ94+ma1FXqA+LOMSD8aij38HZK8LZPV0gUDUIRRx2a" +
                "XKhnoVaXVlDG4n45jAGjISHuh54SHB5zL5wdsWxGPmsJsl9x8xZWB74d3dPZCGWepWtLnga75k1PNCGM" +
                "ciHSdy1OQMhu22+b9tufT2VB57/WjDZcNMX0vXpXf1p97rxPwxCy9r8blb7dPEmrpRc7bYPtBYPe5hAQ" +
                "4uVWBBncjPQKw9KPvU5yzVWNugau0ZXPtPEMt368ycA1U7iLy4yEnDcM0NNaXiSAuPHc15gV04n06gND" +
                "bk3zoIjaPJJWYJYOPZm3kiGPeS1Zhpm21StekS92oqridg7fPZnCM3rvRZqePsAXFBxt0pvY9s1reAnX" +
                "vqnjt+wF3WpfsPxP/CnYHguvtTgb76GQxfLi9SWWi3b5LS3zdvmGlkW73LlsU/9i9zI8gw/+A2DxJFgU" +
                "FgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
