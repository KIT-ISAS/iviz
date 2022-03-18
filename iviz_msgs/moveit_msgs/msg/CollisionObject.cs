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
            Id = "";
            Type = new ObjectRecognitionMsgs.ObjectType();
            Primitives = System.Array.Empty<ShapeMsgs.SolidPrimitive>();
            PrimitivePoses = System.Array.Empty<GeometryMsgs.Pose>();
            Meshes = System.Array.Empty<ShapeMsgs.Mesh>();
            MeshPoses = System.Array.Empty<GeometryMsgs.Pose>();
            Planes = System.Array.Empty<ShapeMsgs.Plane>();
            PlanePoses = System.Array.Empty<GeometryMsgs.Pose>();
        }
        
        /// Constructor with buffer.
        public CollisionObject(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new CollisionObject(ref b);
        
        public CollisionObject RosDeserialize(ref ReadBuffer b) => new CollisionObject(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
            if (Id is null) BuiltIns.ThrowNullReference(nameof(Id));
            if (Type is null) BuiltIns.ThrowNullReference(nameof(Type));
            Type.RosValidate();
            if (Primitives is null) BuiltIns.ThrowNullReference(nameof(Primitives));
            for (int i = 0; i < Primitives.Length; i++)
            {
                if (Primitives[i] is null) BuiltIns.ThrowNullReference($"{nameof(Primitives)}[{i}]");
                Primitives[i].RosValidate();
            }
            if (PrimitivePoses is null) BuiltIns.ThrowNullReference(nameof(PrimitivePoses));
            if (Meshes is null) BuiltIns.ThrowNullReference(nameof(Meshes));
            for (int i = 0; i < Meshes.Length; i++)
            {
                if (Meshes[i] is null) BuiltIns.ThrowNullReference($"{nameof(Meshes)}[{i}]");
                Meshes[i].RosValidate();
            }
            if (MeshPoses is null) BuiltIns.ThrowNullReference(nameof(MeshPoses));
            if (Planes is null) BuiltIns.ThrowNullReference(nameof(Planes));
            for (int i = 0; i < Planes.Length; i++)
            {
                if (Planes[i] is null) BuiltIns.ThrowNullReference($"{nameof(Planes)}[{i}]");
                Planes[i].RosValidate();
            }
            if (PlanePoses is null) BuiltIns.ThrowNullReference(nameof(PlanePoses));
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
                "H4sIAAAAAAAAE71YbVMbORL+Pr9CtXwwXBwnG9itPV/xAbB3YSsEFrirJBTlkmdkW8dYmkgaG+/V/fd7" +
                "Wi8zYyC3t1UXDAXWqNXv/XRrdtgRWwheCNNntRUFm2nDpHLCVEY4qebMLQSrtBU2O/V0kTzLdtgNtmTB" +
                "9MwT6ek/Re7YruJLEXhJxc71Spy5vcw6Q8xkkc5FarepBNFxVnDHp9wKYnev9FpFEpuF/xMjcj1X0kmt" +
                "Jks7t28u/PMb4kBsEudcl6W0oGJzoZcCgoVl3FqdS+6g1Vq6RUfhQTgmTbCScSMCiRG28ipqT00LOZNg" +
                "0DrgWpewP4nJWWXkEgqu4Cy74JUIenqqy7R1e9cli2c3gfISGnT3J8HxkHQu7GKbKz0B7TJsPMuH9loW" +
                "x7pWBQWhKrmCobviS83Jm0za1ro+m9YuBJ3IWM4Vm8JX8KIi79WWWOAhLwofC156x+11dbuko2SIl/QV" +
                "I2mv1e6oKGw3jaLXiUqRRJsLJQbsbCvXkKMrqWtbbph4kNaR+tKRPUbgZC6KQTbdOMGORqPDtyTmSiyR" +
                "kVuSZkYv/VqolTRaLWEovjtpBPjuipUwG7cIpcAdW3KXLyKHNidksRckXY3PL/4xPvze21RVQhVkCleN" +
                "XcSDlwY5FJW2lP9/bGuhIVRpFw4lOxGF1sjLy/GH0eE7Eo2zrcznxXkpfabEOmZ+DLUl+3eJIsUNNWH4" +
                "xrJlbR1RlGIGNZaV2+xBlJwxq0vyFVybECNKhoaFsPBkEVT0vtknBS8qYUL2wT3giSWgZ5kIddrOssP/" +
                "8yc7v/5lyKwrQjoGVKNidlwV3BQoG8cJjTwWLuR8IczrEmlQ4hBfVoi23yXMsQE9YCZ+5/Cn4SUc4dEP" +
                "duV6uayVzIE7zEmUY/c8uY6Ar+LGybwuuQG9NihRIp8ZwChxx69FpQqVC3Y2GoJGWZHXBA+QJFWO2Pqa" +
                "PBuxrAZ077+jA9nOzVq/JiSfA7Mb4SH/oKx4QPFY0pPbIWT8JRg3AG84B1CrUI+7/tkES7vHIAQqiErn" +
                "C7YLzS9RFTqk0oobyaelIMY5PACuPTrU2+twVp614kon9oFjK+N/YasavmTT6wViVvqaqedwIAgrgyxE" +
                "UbDpxjPJS0IuVsqp4WaT0akgMtv5mXwMIspSioh80idS3/LRmFD3+jbZ+MctLtv50x92cfzr+OSGEuPP" +
                "H44fKtUTrRyXioCDSjRULZ/q2Cd8C0fTpnpBh0ltFWWBQDO31kgnoA4okOlwI2YLoKeeCxw2DOWB5EYm" +
                "F2Im0W+4gsTAIjX0e7FJM0ZXwtA/CeeBv4TYgGKT4G2OAlGsmKYQgktiWEy7qlinjS8DWPDr9cWHN6ja" +
                "VBufjs7fs8BgwI4aQEXTb+B4ye89ZtowNiSv5NrQAKF9y4XcNN4M2HgwH/RJy6dB96hOMFxqfY+UvRdD" +
                "9t2/euTh3rB3out8MTru9VnPaO3wZOFcNXzzptSoDnjb9f79XTDR+JpRhEBqRZ7xfTpEz8Oa88HpeAF1" +
                "BOE9HJKoSWDXvRARLWYlGsdUltJtBlvT21a+wmARnOhxENPQ6DjkhmdCVlEXipIRqbKwlFw1/EQN9wEl" +
                "WQqPRT9DwWgsLZlnM2SNA/wzcgGePXbB8Ie//nQQKGgQhJZQDnRPNe5FSde/vWcImxULXRZNnLYEX38p" +
                "TxNF4O1Fsd56bvd/DE8qbfDkh4P9d34JakMEsiz1OlIAXNYA+EePaV4mQ5KASZx7w+5SF3VJ+46Q0Omq" +
                "lxIaqf2teuPXZldoNAplOtUPfYw/lGl9lm8Awf4KgXQT1LGOyjJwCfN0qG3fEhd8RRlB8/Y0DaRgRuMH" +
                "DZi+Eo2v8rd9/Awy39N+YscXHzFUhe/Xl6fjqzEGnbA8+fT+7MNofIXBIj64+DA+PEjVnvDJzzykU6QK" +
                "d4YECWgLii4Ndpt0VmrufjzAvNpSpDNLwf241j3QIRsywdEqqV9jsnDRCWFwJHc9JKTqtWd6YdTKYmrS" +
                "Lgz3qvb96mOffer7Yv3c1ZmcTNulUHNcXaJGjzHIAiUb++D0QevbyUfMx+3qU+NrWn2mmbKjUvB/1Eor" +
                "gDeFnWAT/5UfL2gC6kdQ8VAc7Da8kDWpQP3bD9CUQYOtuE6ujkZnf7+meb0jMwXZ86QAh/kreCWkDgBT" +
                "edxrLiu81N5wovnMOMbfAQvwONPRY4nv5HR89svpDdsl3nGx19pETWnW9XhrE3J5vnCNz2MtsF2qhb0g" +
                "j3AuyQnWRTlh0ZHzNSng0PguhC9elZ+XeaJ9Ky2aLZxvb52PaxLjdy6NH0EHoWRk1eaQ9ymd1wgS5Xtd" +
                "9YNn2avo1OxRJUb/NSn1yHi6HbWV+oS4dQwRfhuIe3olJdSiayMqBnEIQw7NLNStUKgzIyhdcafsh2jR" +
                "eBD2ZRiIaDDxiefPDlh2SQ5rCLLfcNsWRnm+Ld1LGSh9DvurSp7mufhqRyZbeYjxtrkJAtlD823TfPv9" +
                "ZdRvXZdsaAJl/T239ee28rT60vqdBiAk63+3KH1bv0B7pZc4qal2wkAvbgj8StzXfSHiHqTmmI7+1uke" +
                "K17WgjwxowuebiLpL/grQZdKYW/vMpJxExmgjzW8sgiMPHc1hsN0Ir3lwFRbV57Aa/NMPoFZOvRCrkpm" +
                "POOyZBYm2EapcBu+3Q96iocJHPdC2vo3YP5902NA4eHNVz++SWvftDWv4/gDe0W311cs/x1/CnbI/Lsr" +
                "zoaHKF4xu317h+W0WX5Py7xZvqNl0Sz375qMvz2488+y7D/5Tbse+BUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
