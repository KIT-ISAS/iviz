/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Model : IDeserializable<Model>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "filename")] public string Filename;
        [DataMember (Name = "orientation_hint")] public string OrientationHint;
        [DataMember (Name = "meshes")] public Mesh[] Meshes;
        [DataMember (Name = "materials")] public Material[] Materials;
        [DataMember (Name = "nodes")] public Node[] Nodes;
    
        public Model()
        {
            Name = "";
            Filename = "";
            OrientationHint = "";
            Meshes = System.Array.Empty<Mesh>();
            Materials = System.Array.Empty<Material>();
            Nodes = System.Array.Empty<Node>();
        }
        
        public Model(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Filename);
            b.DeserializeString(out OrientationHint);
            b.DeserializeArray(out Meshes);
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i] = new Mesh(ref b);
            }
            b.DeserializeArray(out Materials);
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new Material(ref b);
            }
            b.DeserializeArray(out Nodes);
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new Node(ref b);
            }
        }
        
        public Model(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeString(out Filename);
            b.Align4();
            b.DeserializeString(out OrientationHint);
            b.Align4();
            b.DeserializeArray(out Meshes);
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i] = new Mesh(ref b);
            }
            b.Align4();
            b.DeserializeArray(out Materials);
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new Material(ref b);
            }
            b.Align4();
            b.DeserializeArray(out Nodes);
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new Node(ref b);
            }
        }
        
        public Model RosDeserialize(ref ReadBuffer b) => new Model(ref b);
        
        public Model RosDeserialize(ref ReadBuffer2 b) => new Model(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(OrientationHint);
            b.Serialize(Meshes.Length);
            foreach (var t in Meshes)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Materials.Length);
            foreach (var t in Materials)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Nodes.Length);
            foreach (var t in Nodes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(OrientationHint);
            b.Serialize(Meshes.Length);
            foreach (var t in Meshes)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Materials.Length);
            foreach (var t in Materials)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Nodes.Length);
            foreach (var t in Nodes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Filename is null) BuiltIns.ThrowNullReference();
            if (OrientationHint is null) BuiltIns.ThrowNullReference();
            if (Meshes is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Meshes.Length; i++)
            {
                if (Meshes[i] is null) BuiltIns.ThrowNullReference(nameof(Meshes), i);
                Meshes[i].RosValidate();
            }
            if (Materials is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Materials.Length; i++)
            {
                if (Materials[i] is null) BuiltIns.ThrowNullReference(nameof(Materials), i);
                Materials[i].RosValidate();
            }
            if (Nodes is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i] is null) BuiltIns.ThrowNullReference(nameof(Nodes), i);
                Nodes[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Filename);
                size += WriteBuffer.GetStringSize(OrientationHint);
                size += WriteBuffer.GetArraySize(Meshes);
                size += WriteBuffer.GetArraySize(Materials);
                size += WriteBuffer.GetArraySize(Nodes);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.AddLength(c, Name);
            c = WriteBuffer2.AddLength(c, Filename);
            c = WriteBuffer2.AddLength(c, OrientationHint);
            c = WriteBuffer2.Align4(c);
            c += 4; // Meshes.Length
            foreach (var t in Meshes)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align4(c);
            c += 4; // Materials.Length
            foreach (var t in Materials)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align4(c);
            c += 4; // Nodes.Length
            foreach (var t in Nodes)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Model";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bd9be904104258bcedbf207e42db2852";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71W34+jNhB+56+wdC+tVF17e3s/pXtggWxQgaBA9ro6VcgBJ3ELNrKdbPb++s6AoY6u" +
                "+9bdPAT7G3vmm/HM2NooLvZE0I55ehzveMvcuVScCUMNl6I6cGG8lOnDtz9JBx+mvZQapjhtEbFD7WWy" +
                "YQAI+GjP+/I//7y0uP1M+Il/rzq9178ioYntwHzPZMeMehzFuQTWb6+Az4kpw2ug9NQCIVWH/J+SGyr2" +
                "EIynF2x5Na8p2TmQUjUaN7JzVQ8TL5CtVMGBCsEwaDVOq3qcwyYIoNi3GL0dRarHQfUc24qLhp2fK6b/" +
                "6Zb3ipQHroEppAEXmpgDI73UHFOCyB2hMIOVhAuyU4wR3QPznx64ORBgvuVG46pesZpr2PLza9AYw3JN" +
                "AJJdxxrWECPJUTMy2CQPB6YYnBaa0XzbMtCtDaMNKrK0XhMCeiZyVpNohkRF3aCwV7KTBjdD8GTPFN3y" +
                "lpvHYeu0E/JY0z3DLZCtfC9GMob+zcixJy2IR4+QlSAabECiwe5WWseQjybUEClq9guhGiOBQaopeDQE" +
                "aOActPLYoG1v10qKp3qeR4/z6Puz18ucmE+msU3VZ2fi1sJYGNY8jF7KPCQ41thHoux3b79b+6XPT2Sq" +
                "+qna6TTYToP6BdqobTHW7ZskysIqjBb+JinJF/LbBe6HYVzGdxEI3ngXzdcGldBuixfHPG/4bgcVPs9Z" +
                "x6G2T2xOfAlFBsU5z7fHrq90TVsstwnUcAVxATX5I1IBCyb25jCLFNu1rDbgIqi1hwqXW1N1cDFhfzZH" +
                "xcbujKMXyDhr05Ip7/OoylZZ5IR3wMJ4sdgUY3AduMijYJP4a8CvXNxPb+Iow0N668JRGhfFeEbXLr6M" +
                "4tslrn53yWOd+kkB8PsLm8s4i7OoQMEHV7DK/SAu7wH+eEm9yBM/iNKR0CdXlqDd1M/Rrwt/19EiiYIy" +
                "XmUouvB5k/2erb4O+JVnBaAij7PbarFepdXmzoneJCnyZbR24zcJgvskzsLIDeEkuln94URwQsGZzI3g" +
                "hP/L691Ea5VXKdRKnCf3DiVAoVgcKgAUm5ty7QelwwLQML6Lw8jhgCvT1apcWg3XDh7fZlFo8ZnB17Wf" +
                "V/jn2B+wIPHT3OEwgGm8Xq/cSAxoGAV+MpCYyrqnUFNjGxpfH+Nq89hPidzRvscqHRcdT/aVMlfyUHTw" +
                "njFyarLDbYxXtZ0/KNoPZVkdf0BOz1+X+GC96GKjJz1V2MKgMyp+viZGUaF38Eb0povSPoJfojcjgymi" +
                "3968B9vkFVHyAYL/l1TwqPgHVmxGr8wLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
