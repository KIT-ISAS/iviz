/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Model")]
    public sealed class Model : IMessage, IDeserializable<Model>
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
                "H4sIAAAAAAAAE71UTU/DMAy951dE4gcgNoQQiNMOaIdxYeIyoSlr3daoTarY28p+PU6bdEziyNrL84dU" +
                "Pz87JvZoS21NA4oGu8AafvvOI1g2jM5uK7SsVkDV5lM3AkBqZRg8mjpEoknqzeUgAStASr3886dW769P" +
                "Gg942jZU0m0glNj2zD8gY+fnwuAAnjETEueQdb4JHIfITCIM3cI5n5NauNp5iWQBSa2lG1vWoZXChL/s" +
                "pf/5bGx0aXPort9f5K6K2plQvhut79E6TUVj9ieNqxfvJ9Pr/6gTlhF3Ec31aaSNSJtgkrFLRjbBvsf1" +
                "u9j5XiBtml14rdHLsSj2lHLQIBEeQK2h472HlI7uBOLFQpF2a7h6TrIdMecqORVgWfEobdsOA5ZnmBue" +
                "YMrhfF2IOxBpjQ/aivweu3vN3lgq5JoM6fNJnGIBAoP0/jZ3D1Jb32jvjnKbvmTWSv0AJAnW9NoFAAA=";
                
    }
}
