/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/CollisionObject")]
    public sealed class CollisionObject : IDeserializable<CollisionObject>, IMessage
    {
        // A header, used for interpreting the poses
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The id of the object (name used in MoveIt)
        [DataMember (Name = "id")] public string Id { get; set; }
        // The object type in a database of known objects
        [DataMember (Name = "type")] public ObjectRecognitionMsgs.ObjectType Type { get; set; }
        // The collision geometries associated with the object.
        // Their poses are with respect to the specified header
        // Solid geometric primitives
        [DataMember (Name = "primitives")] public ShapeMsgs.SolidPrimitive[] Primitives { get; set; }
        [DataMember (Name = "primitive_poses")] public GeometryMsgs.Pose[] PrimitivePoses { get; set; }
        // Meshes
        [DataMember (Name = "meshes")] public ShapeMsgs.Mesh[] Meshes { get; set; }
        [DataMember (Name = "mesh_poses")] public GeometryMsgs.Pose[] MeshPoses { get; set; }
        // Bounding planes (equation is specified, but the plane can be oriented using an additional pose)
        [DataMember (Name = "planes")] public ShapeMsgs.Plane[] Planes { get; set; }
        [DataMember (Name = "plane_poses")] public GeometryMsgs.Pose[] PlanePoses { get; set; }
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
        [DataMember (Name = "operation")] public byte Operation { get; set; }
    
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
        public CollisionObject(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += Type.RosMessageLength;
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
                "H4sIAAAAAAAAE71YbVMbtxb+vjP8B035YLhxHAK00/qOPwB2C50QKNBOEobxyLuyrcta2khaG/dO/3uf" +
                "I2lfDKS9nbmBMGSlPTovz3nVbrMjNhc8E6bLSisyNtWGSeWEKYxwUs2YmwtWaCvsVnLqCSP9VrKVbLMb" +
                "vJUZ01NPpyf/EaljO4ovRGAnFTvXS3HmdrcS6wwxlFlzNB5w60IQKWcZd3zCrSCO90qvVCSB9PAwNiLV" +
                "MyWd1Gq8sDP75sLv3xAL4tMwT3WeSws6NhN6ISBdWMat1ankDrqtpJu31O7Fc9IEexk3ItAYYQuvpvbk" +
                "tJBTCQ5tJK51DiAqSSkrjFxAyyUBZ+e8EEFbT3ZZvbu926CLp9eB9BJatAnG0Q0k7VzY+SPOtAXyRXzz" +
                "LC962WZzrEuVkVOKnCuYvCM+l5ygZdI2dnbZpHQhEIiMpVyxCWADoIqALC2xwCbPMu8YnnsIdzf0u6Sz" +
                "ZI8X9SVj6WVbw6Mss+3Yij4gOkVSbSqU6LGzjQBE7C6lLm2+ZuJBWkcmSEc2GYGTqcjg7MnaCXY0HA72" +
                "gqQrsUCkbgibGr3wa6GW0mi1gL14dtIIsN4RS2HWbh6yhDu24C6dRw5NkMhsNwq7Gp1f/DYavI2WFYVQ" +
                "GRnEVW0dseG5QVxF1S3lxd9bnGnIVdqFQ5W18EfL1MvL0fvhYD9Ix/FG7PMSvaAuU2IVEyL63RIKO0RR" +
                "uRCpYvjaskVpHVHkYgpNFoVb75IsOWVW5wQZEK5qShQNLTNhAWgW1fQQHQQlLwphQjgCJfDFEvVpUdPq" +
                "6v1Wkgz+zz/J+fVPfWZdFuIz1D7Kc8dVxk2GXHKcypUvmXM5mwvzOkdI5DjEFwU8799STbI9X1pgK35n" +
                "QNXwHGj4CgnLUr1YlEqmqErMSeRo+zzhR5Wx4MbJtMy5Ab02yFoinxqUWuKOX4vkFSoV7GzYB42yIi2p" +
                "bkCSVCk87NP0bMiSEhX+YJ8OJNs3K/2aCv4Mlb0WHgIRyooH5JIlPbntQ8a/gnE98AY4KMUK6bnj98ZY" +
                "2l0GIVBBFDqdsx1ofokM0SGgltxIPskFMU6BALh26FBnt8VZedaKK12xDxwbGf8LW1XzJZtez+Gz3CdP" +
                "OQOAICwMQhHZwSZrzyTNqZixXE4MN+uETgWRyfaPhDGIKFTJI/JJF6lam/fGWGZfKxr/vgUm2//4h10c" +
                "/zw6uaHA+OeH409I1xOtHJeKCgilachcPtGxefguj75OGYO2U7VdJAZczdxKI6BQfUCBWAeQGEJQS/VM" +
                "4LBhSBCEN2I5E1OJJsQViQw8mp5/L9bVMNIW0vc7gQUKMpVw1GZTVboZskSxbFLPKGDT8MwmbYWs08an" +
                "A+z4+fri/Rtkb5UjH4/O37HAoseO6vKKuaCuzwt+7yuoDbNFhU2qDU0Z2ndjElwNQj026s16XdL0qfd9" +
                "naeqnGt9j9i9F332zX87BHSn3znRZTofHne6rGO0dtiZO1f037zJNdIEoLvOH99EI43PHkW1SC0JHt/E" +
                "gxd9gXPeSS0ckFGQ3sEhiexEFbsXItaNaY5GMpG5dOve5qS3EbqwWQQcfUnEzDQ8DkHiuZBd1JaiaPgr" +
                "z6wPsxJYUSN+QHrmwvZp80foGA32a+Y59VmNQtgkILD5GIj+tz98fxhJaGyEqtAQhE/V7lTSrn95x+A/" +
                "K+Y6z2p/bQq//pyfViSRvRfHOquZPfgubhXaYOvbw4P9sMYBQyQyz/WqokHJWaHsP96nUZsMqqSM47wc" +
                "Xy90VuZE4KhCOl106hincP9aXfNL8y50Gob0neiHLoYkirwuS9cozv4OgvAT1MuO8jxwCWN4yHnfLOd8" +
                "SQFCY/qkml7BjMYTmkZ9bhqf+ntd/Oslvtt9z44vPgzexufry9PR1WiwH5cnH9+dvR+OrgYH1cbF+9Hg" +
                "MImhW9UtPxORTpGK9pOKKEPDUHTZsJuk01xz990hRtuGojqzENxPdO0DLbI+ExxNlDo5Zg4XQQjjJcH1" +
                "UJWvTnOmE0axJMYovYXhXtWuX33oso9dn7yf2joTyPQ6F2qGG0/UaKMq0YSB0lnbB9B7DbbjD4O91upj" +
                "jTWtPgHqtkoB/6iVVijq5HYqpPhf+cGDZqNuLDK+Pge7Dc9kSSpQZ/djNkVQb8Ov46uj4dmv19CnLbNy" +
                "sudJDg6TWUAlhA4qqPJ1sL7Z8Fx7w4nmE+MYj3sslMupjohVfMeno7OfTm/YDvGOi93GJupV0zbijU2I" +
                "5dnc1ZjHXGA7lAu7QR5VvUpOsC7KCYuWnC9JAYcau+C+eMt+XuaJ9i02q1/hfHNTfZyTGM1Tafxw2gsp" +
                "I4smhjymdF7DSRTvZdENyLJXEdTkUSZG/OqQemQ8gquVqU+IG2CI8OuUuKe3V6padL9ExsAPYfihWYaa" +
                "FxJ1agSFKy6f3eAtGhjCexkGJZpWfOD5sz2WXBJgNUHyC67mwijPt6F7KQOlj2F/iUmrOS9+G5KVrTz4" +
                "eNPcqgSyh/ppXT/9/jLqN9BVNtSOsv4e3OC5qTytPje400CEYP1ri6qn1Qu0V/roUzXVlhvoOw8Vvxz3" +
                "eZ+IuCGpGWalf7e6x5LnpSAkpnT107Un/QeApaDrprC3dwnJuIkM0MdqXkksjDx1JYbF6kT1LQRzbll4" +
                "Aq/NM/EEZtWhF4KqMuMZyCqzMNHWSoV78u1B0FM8jAHcC2nrv5b5r1KPCwoPH8m68bNb81mu/nbHH9gr" +
                "ute+Yunv+JOxAdsjZ3HWHyB5xfR27w7LSb18S8u0Xu7TMquXB3d1xN8e3vm9JPkTWxa6WzkWAAA=";
                
    }
}
