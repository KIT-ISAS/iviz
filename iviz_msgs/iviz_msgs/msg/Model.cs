/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Model")]
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
            Name = "";
            Filename = "";
            OrientationHint = "";
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
        [Preserve] public const string RosMd5Sum = "53ee204bb5d992b0e9da1a0965685a6b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UPU/DMBDdLfU/WOIHIFKEEBITA2IoCxVLhaprck4OJXZku23or+fc5Jp2YKPJ4nfP" +
                "Vu7dZ4iebKktNKhCjw3VeG47T2gjRHJ2XZGNaoGhWn3phg8MagERPUGdmAEG9e4KZMLyEZR6/udPLT5e" +
                "nzTt6LBuQhlukyBRm5TP1Cfm0fm5YQ079JFyDBekdb5hncJliYvYvTjnC2ZfXM0PMybzhJhZcly2rFNQ" +
                "Bo5/23Iq5tkp5jdbYDe7fqwShDK1gySAnQr8GeFhMinZX1Ku7n+o0rESj9rPBlAK2AiACcRIg0hfgICN" +
                "gHyCQRia8XIYhjxpaDZpkkeiIGO24ewFNhQC7ZhZYhe3HuXJYE6Rx96TRNBCrCSBeypGo0Iqq3hKctv2" +
                "teb5LCDC9XWmDXeeZtULacFjWpHAN929jh5sMLxs+utxa07RCkmBDOTq7oF96xvt3Z531rfzWqlf42fJ" +
                "1f0FAAA=";
                
    }
}
