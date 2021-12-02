/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Model : IDeserializable<Model>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "filename")] public string Filename;
        [DataMember (Name = "orientation_hint")] public string OrientationHint;
        [DataMember (Name = "meshes")] public Mesh[] Meshes;
        [DataMember (Name = "materials")] public Material[] Materials;
        [DataMember (Name = "nodes")] public Node[] Nodes;
    
        /// Constructor for empty message.
        public Model()
        {
            Name = string.Empty;
            Filename = string.Empty;
            OrientationHint = string.Empty;
            Meshes = System.Array.Empty<Mesh>();
            Materials = System.Array.Empty<Material>();
            Nodes = System.Array.Empty<Node>();
        }
        
        /// Explicit constructor.
        public Model(string Name, string Filename, string OrientationHint, Mesh[] Meshes, Material[] Materials, Node[] Nodes)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.OrientationHint = OrientationHint;
            this.Meshes = Meshes;
            this.Materials = Materials;
            this.Nodes = Nodes;
        }
        
        /// Constructor with buffer.
        internal Model(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Model(ref b);
        
        Model IDeserializable<Model>.RosDeserialize(ref Buffer b) => new Model(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(OrientationHint);
            b.SerializeArray(Meshes);
            b.SerializeArray(Materials);
            b.SerializeArray(Nodes);
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
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Filename);
                size += BuiltIns.GetStringSize(OrientationHint);
                size += BuiltIns.GetArraySize(Meshes);
                size += BuiltIns.GetArraySize(Materials);
                size += BuiltIns.GetArraySize(Nodes);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Model";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bd9be904104258bcedbf207e42db2852";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXW/aMBR9z6+ItB+wFdqum9SHkBiwli8lgQ5Vk2WCA57Ih+JAaX/9rhMnGPV1wAO2" +
                "z3Xsc4/vvbZoal5szYLmzBBdP+N7po/LmrOioQ0vC7LjRWN4TOxe/5g5NEwYHm1YzeleIqorDL/cMAAK" +
                "aIRhPP/nn+HFs58mP/IPkout+CoJ9Wxb5kuWNmU9zoDCkdUNT4GFhhVlnUuWGtTQYgteXmBrTgY4YSe7" +
                "LOuNkHPZiaTtwLDLfVnbO1oUTAqQyiFJuzF8BGIU271UIqOSwwHkG48GnQgvNux0fX16n4xsX1JJ4DT0" +
                "3ofex/V5DCLqKislr765flTduY1H/ZHdavvxqA2BJ7NW7Va1a9XSGxyCCso+GGnf6ThAJ71BxqoMUG5P" +
                "XOQ7xEFTa+Em5rP57QK3HAcneInAcGdc5LkS1aT5WtaoYbzhWXYQZzvLuRD8yIZgLyua8uYc/OtDXhGR" +
                "0j0sPYACqh0vmBCfEQIsWLFtdoOpZtkeghpchGXVoUId3ZAcaqAsH82hZl3xkL0bRJzaU5FJViEifuAj" +
                "Td4Wc/B0uog7cTU4DpG9cK0I8JGOW94EI18e0liHkYfjuDujex2fIzyby9kPlzwiz3JjgB8v9pxjH/so" +
                "lobvuiEILRsnK4CfLqnHoWvZyOsI/dBtrtzXs0Lp14W/EZq6yE5w4EvThc8L/5cfvLT4yFAGWCLE/oxM" +
                "o8Aji6WmXm+JwzmKdP16g71yse8gXcLeNAl+awr2KDjj6wr2+JnXQ08rCIkHuYJDd6VRAhSSRaMCQLyY" +
                "JJFlJxoLQB28xA7SOMiZXhAkc7XCvYbjmY/aNNQZvERWSOSftn+L2a7ltbLroIejKNCVaFEH2ZbbkujT" +
                "uqKQU10Z6i7HbnbzXvWBnNOqklnaTToc1SU6ZHKbdHDdwv2ivigrVrfPFzV+q2nVpiU5fEKO189LXxYE" +
                "vYp1nlQUKgo8rihYTvdmU9NCZPBQ6czn99YtarNk0Cv6evcIe5tfzLp8A/H/lrVpGP8ATUd6MTcKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
