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
                "H4sIAAAAAAAAE71WXW+bMBR9R+I/IO0HbEvarpvUBwpOY40vAWlXVRNyEif1FD5kkzTtr9814GBve1yT" +
                "h3A5x+Bzj30vFi1n1dapSEkt0ccbtqP6fc0ZrVrSsroqnlnVWiEVz08/nRIuVFghaSlnZCeRIRRWVK8p" +
                "ABVchGXd/OefFWZ33xx2YG9FKbbioxSk1ErltnVPV23NpxvQcKC8ZSsqDLCqeQk6Dawl1RYSNcElK0Y8" +
                "p0evrvlayNH0WKy6G9vy6l3NvWdSVVTasJK3xaq/l4+BJ9V2Jw3ZkE7JHmycTk5+Faxa06P9/kapxKzN" +
                "riZSAUyqwtcxfDuDlJOXutvK0HefXV+xfvmmE7VyZ5t/Ouk2wrXD7SHYqmCpAnKOpRj2p9qWRAVLFazO" +
                "UMNDLQx53wYo8gsfzdxFkDs3zifbIFzfxzm+R8B8ti3bLP7BXIeUS9m5RmDNNpu90EbQkgnBDnTc+nVD" +
                "VqzVamG5L5tCrMgO3j+iAhohq6gQ/4AKEEOrbfs8cpxudrDLIVv56mGJocuuixI6ZNdY2j2nfVuR0Tn2" +
                "4DDpICd/TFARxRHSze5AH89mi2ywWsOzBHmLwE2BmBiEG95iFMlVmxo4CnGW9Yt2YRBzhO/mcvylbapJ" +
                "QzfIAL8yJ57jCEcok8wXg4kT18P5I+DXf6SQJYHrobCX9dUgAzl56CYyQTPzFM0C5OU4jiRnZr+Ivkfx" +
                "Q0dM5A7sKXhNgqO7YpbGYbG4171UVJbMUWq4qRjvMcCRjwxDFXcb/9D9VDBkFRl+KmKUdzmqi5MihHLC" +
                "SfCoKwMY6klXBEi2uM1T18t1MQD7+B77SJcix4ZxnM+Hl1zoBL6LkD8QmpCH1E0K+afL6EAvcMNEl9Kh" +
                "IU7T2PClg33kuUGv5dQCGiILr29bw2e1f6J9baiKS9I0XT334/YH9QU+VX1XnPC1hi+TeqhuKO9OQQp4" +
                "4aTpCrjY/w0dzlDB8pSlt74hnYZA/2nlwYyz44XTclKJDRx4eno8uZ2jp0sFytWnz1cwt/PB4fULLMGv" +
                "mjuW9RtKOmeQgQoAAA==";
                
    }
}
