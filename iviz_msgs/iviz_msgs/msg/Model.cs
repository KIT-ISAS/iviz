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
            Name = "";
            Filename = "";
            OrientationHint = "";
            Meshes = System.Array.Empty<Mesh>();
            Materials = System.Array.Empty<Material>();
            Nodes = System.Array.Empty<Node>();
        }
        
        /// Constructor with buffer.
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Model(ref b);
        
        public Model RosDeserialize(ref ReadBuffer b) => new Model(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
