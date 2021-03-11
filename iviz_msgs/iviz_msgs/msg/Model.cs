/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Model")]
    public sealed class Model : IDeserializable<Model>, IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "filename")] public string Filename { get; set; }
        [DataMember (Name = "orientation_hint")] public string OrientationHint { get; set; }
        [DataMember (Name = "meshes")] public Mesh[] Meshes { get; set; }
        [DataMember (Name = "materials")] public Material[] Materials { get; set; }
        [DataMember (Name = "nodes")] public Node[] Nodes { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Model()
        {
            Name = string.Empty;
            Filename = string.Empty;
            OrientationHint = string.Empty;
            Meshes = System.Array.Empty<Mesh>();
            Materials = System.Array.Empty<Material>();
            Nodes = System.Array.Empty<Node>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Model(string Name, string Filename, string OrientationHint, Mesh[] Meshes, Material[] Materials, Node[] Nodes)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.OrientationHint = OrientationHint;
            this.Meshes = Meshes;
            this.Materials = Materials;
            this.Nodes = Nodes;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Model(ref Buffer b)
        {
            Name = b.DeserializeString();
            Filename = b.DeserializeString();
            OrientationHint = b.DeserializeString();
            Meshes = b.DeserializeArray<Mesh>();
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i] = new Mesh(ref b);
            }
            Materials = b.DeserializeArray<Material>();
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new Material(ref b);
            }
            Nodes = b.DeserializeArray<Node>();
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new Node(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Model(ref b);
        }
        
        Model IDeserializable<Model>.RosDeserialize(ref Buffer b)
        {
            return new Model(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(OrientationHint);
            b.SerializeArray(Meshes, 0);
            b.SerializeArray(Materials, 0);
            b.SerializeArray(Nodes, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Filename is null) throw new System.NullReferenceException(nameof(Filename));
            if (OrientationHint is null) throw new System.NullReferenceException(nameof(OrientationHint));
            if (Meshes is null) throw new System.NullReferenceException(nameof(Meshes));
            for (int i = 0; i < Meshes.Length; i++)
            {
                if (Meshes[i] is null) throw new System.NullReferenceException($"{nameof(Meshes)}[{i}]");
                Meshes[i].RosValidate();
            }
            if (Materials is null) throw new System.NullReferenceException(nameof(Materials));
            for (int i = 0; i < Materials.Length; i++)
            {
                if (Materials[i] is null) throw new System.NullReferenceException($"{nameof(Materials)}[{i}]");
                Materials[i].RosValidate();
            }
            if (Nodes is null) throw new System.NullReferenceException(nameof(Nodes));
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i] is null) throw new System.NullReferenceException($"{nameof(Nodes)}[{i}]");
                Nodes[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Filename);
                size += BuiltIns.UTF8.GetByteCount(OrientationHint);
                foreach (var i in Meshes)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Materials)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Nodes)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Model";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bd9be904104258bcedbf207e42db2852";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXW+bMBR9R8p/QNoP2Jq0XTepDwScxBpfApIuqibLISZhCh/CJE3763cNhhj1dSIP" +
                "8fW5xj732PfavK7S/KDnNGMab+0kPTG1X1Qpy2tap0VOjmleaw7jx9c/egYN45pDa1al9CQQaXLNLfYM" +
                "gBwark205//8m2hOuPypp5f0g2T8wL8KSpOOcEN+w+K6qGYJsLiwqk5jIKJgeVFlgqgC1TQ/QKADbJeS" +
                "Ho7Y1SyKas/FWHYlcdPRzOJUVOaR5jkTGsSiS+K2Dx+BHvnhJMRIqOBwBgVn014qkuZ7dh1Doi6qiZac" +
                "Cio4XHvrvbc+xqDSKzlRtZZ6jrC+umWTdgNn027vxmMwm06a4/CkV7I9yHYnWzrKbsgj2pKBM0A7o6UB" +
                "RjxKDsuU6ESZ28i1iIUWxtqO9Gf92wA3LAtHeIPAcacNMl9qq9NsJwpX39+nSXLmNz/LUs7TC+vPflHS" +
                "OK1vubA7ZyXhMT3B1D3IoQSmOeP8M0KABcsP9bF3VSw5wQGHGGFaubVQXPckg8IoCkp9rlhbToQ1ytGT" +
                "q3YqR1sfEddzkaJwg1l4sViHrb4KHPrIXNtGAPhUxQ1njpEr9mmmwsjBYdhu072KrxBersTohyGPwDHs" +
                "EODHwZor7GIXhcLxXXV4vmHiaAvw05B66NuGiZyW0A/VZ4t1HcMXcQ3iDdDCRmaEPVe4BjGv3V+u99Lg" +
                "U006YAofu0uyCDyHrDeKep0n9FcoUPXrHObWxq6FVAk719z7rSjYoRCMqyrY4TdeDx0tzycOpAv27a1C" +
                "CVDIF4UKAOF6HgWGGSksALXwBltI4SBGOp4XreQM9wqOly5qMlFl8BIYPhF/yvoNZtqG08iugg4OAk9V" +
                "okEtZBp2Q6LL7JJCWrXFqL0x29H1e8mkmdGyFInaDjpf5M3aJ3OTd3AHw3UjvyhKVjXPGtl/q2jZZCY5" +
                "f0IuY6SmCysNXzFtMCWFugLvLgqe671eVzTnCTxgWvftKTZOkRYc+kfE690jLK9/0aviDbbgb1HpQOMf" +
                "x63PDFYKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
