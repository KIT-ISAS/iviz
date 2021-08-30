/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Model")]
    public sealed class Model : IDeserializable<Model>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "filename")] public string Filename;
        [DataMember (Name = "orientation_hint")] public string OrientationHint;
        [DataMember (Name = "meshes")] public Mesh[] Meshes;
        [DataMember (Name = "materials")] public Material[] Materials;
        [DataMember (Name = "nodes")] public Node[] Nodes;
    
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
                "H4sIAAAAAAAAE71WXW/aMBR9z6+ItB+wFdqum9SHkBiwli8lgQ5VU2SCAU/kQ3agtL9+14kTHPV1wAO2" +
                "z3Xsc4/vvbaoOSt2ZkFyaoi2v2UHqo9LzmhRk5qVRbpnRW14VOxf/5g5NFQYHqkpZ+QgEdUVhl9uKAAF" +
                "NMIwnv/zz/Di2U+TndhHmoud+CoJdWwb5kua1SUfb4HCifKaZcBCw4qS55KlBtWk2IGXA2zN0h5O6Nku" +
                "S74Rci49p1kzMOzyUHJ7T4qCSgEyOUyzdgwfgRjF7iCV2BLJ4QjyjUe9TikrNvR8fX06n4ztoSSSwLnv" +
                "vfe9j+vz6EXUVVZKXn1z/ajacxuPuiO71fbjURMCTyZX7U61a9WSGxyCCsouGEnXWXed7AYZqzJAuT1x" +
                "ke+kDppaCzcxn81vA9xyHJzgJQLDnTHIcyWqSfK1rFH9eMO226O42GnOhGAn2gd7WZGM1ZfgXx/zKhUZ" +
                "OcDSPSig2rGCCvEZSYEFLXb1vjdxuj1AUIOLsKw6VKijmzSHGijLR33ktC0esneDiFN7KjLJKkSpH/hI" +
                "k7fBHDydLuJWXA2OQ2QvXCsCfKTjljfByJeHNNZh5OE4bs/oXsfnCM/mcvbDkEfkWW4M8ONgzzn2sY9i" +
                "afiuG4LQsnGyAvhpSD0OXctGXkvoh25z5b6eFUq/Bv5GaOoiO8GBL00Dnxf+Lz94afCRoQywRIj9WTqN" +
                "Ai9dLDX1OksczlGk69cZ7JWLfQfpEnamSfBbU7BDwRlfV7DDL7weOlpBmHqQKzh0VxolQCFZNCoAxItJ" +
                "Ell2orEA1MFL7CCNg5zpBUEyVyvcazie+chReM/gJbLCVP5p+zeY7VpeqHFoQA9HUaAr0aAOsi23IdGl" +
                "dUUgp9oy1F6O7ez6veoCOSdVJbO0nXQ8qUu0z+Qm6eC6hftFfVFWlDfPFzV+46Rq0jI9fkJO189L+TYa" +
                "VLHWk4pwWcKgMnJ2vjdrTgqxhYdKa768t25RmyWDTtHXu0fY2/xi8vINxP9bctMw/gFNR3oxNwoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
