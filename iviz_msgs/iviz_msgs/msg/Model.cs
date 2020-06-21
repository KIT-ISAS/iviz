using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Model")]
    public sealed class Model : IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "bounds")] public BoundingBox Bounds { get; set; }
        [DataMember (Name = "meshes")] public Mesh[] Meshes { get; set; }
        [DataMember (Name = "materials")] public Material[] Materials { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Model()
        {
            Name = "";
            Meshes = System.Array.Empty<Mesh>();
            Materials = System.Array.Empty<Material>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Model(string Name, in BoundingBox Bounds, Mesh[] Meshes, Material[] Materials)
        {
            this.Name = Name;
            this.Bounds = Bounds;
            this.Meshes = Meshes;
            this.Materials = Materials;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Model(Buffer b)
        {
            Name = b.DeserializeString();
            Bounds = new BoundingBox(b);
            Meshes = b.DeserializeArray<Mesh>();
            for (int i = 0; i < this.Meshes.Length; i++)
            {
                Meshes[i] = new Mesh(b);
            }
            Materials = b.DeserializeArray<Material>();
            for (int i = 0; i < this.Materials.Length; i++)
            {
                Materials[i] = new Material(b);
            }
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Model(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Name);
            Bounds.RosSerialize(b);
            b.SerializeArray(Meshes, 0);
            b.SerializeArray(Materials, 0);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException();
            if (Meshes is null) throw new System.NullReferenceException();
            for (int i = 0; i < Meshes.Length; i++)
            {
                if (Meshes[i] is null) throw new System.NullReferenceException();
                Meshes[i].RosValidate();
            }
            if (Materials is null) throw new System.NullReferenceException();
            for (int i = 0; i < Materials.Length; i++)
            {
                if (Materials[i] is null) throw new System.NullReferenceException();
                Materials[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += BuiltIns.UTF8.GetByteCount(Name);
                for (int i = 0; i < Meshes.Length; i++)
                {
                    size += Meshes[i].RosMessageLength;
                }
                for (int i = 0; i < Materials.Length; i++)
                {
                    size += Materials[i].RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Model";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b4ed03fcd6a7fe6eff7312eba9a19b89";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71TwWrDMAy9+yv8B4P2Mga7tIexQ08ru4wxnERJBLFdbCVL8/WTE7t1Yey0NBfpPRFb" +
                "fk/y5NA00igNYmd7UzHa2VEWIffiAL79+JSaAzBSBA5VF5iYeiGe//kTh7eXJ4kDTl/aN/4ha0vUnVW0" +
                "3UiNJgNqvKmc88o5r0x5ZVq/9SCf8H9L/A4lWbdlTQdwhCXklLFOB5EXZsMMwbi31vGPe9tZx0wZohdH" +
                "tsM0HTBTq3BKj2Z56eLUq6lgXP/JsfeL0Fdnrk7cQfko2K9trH757Mys/6NMsYmxiFGt30aaiDQJKiVF" +
                "Sso7rEAcv5s1mAWSShcIhiKqsK57n2qg0XscQBxhpN5BKkd4B/HiRVGpb6yoTaAFbFq6qHk6LZ7y5lWK" +
                "2Ngf3qy8mFUFAAA=";
                
    }
}
