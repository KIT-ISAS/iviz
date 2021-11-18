/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Node")]
    public sealed class Node : IDeserializable<Node>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "parent")] public int Parent;
        [DataMember (Name = "transform")] public Matrix4 Transform;
        [DataMember (Name = "meshes")] public int[] Meshes;
    
        /// <summary> Constructor for empty message. </summary>
        public Node()
        {
            Name = string.Empty;
            Transform = new Matrix4();
            Meshes = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Node(string Name, int Parent, Matrix4 Transform, int[] Meshes)
        {
            this.Name = Name;
            this.Parent = Parent;
            this.Transform = Transform;
            this.Meshes = Meshes;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Node(ref Buffer b)
        {
            Name = b.DeserializeString();
            Parent = b.Deserialize<int>();
            Transform = new Matrix4(ref b);
            Meshes = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Node(ref b);
        }
        
        Node IDeserializable<Node>.RosDeserialize(ref Buffer b)
        {
            return new Node(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Parent);
            Transform.RosSerialize(ref b);
            b.SerializeStructArray(Meshes, 0);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Transform is null) throw new System.NullReferenceException(nameof(Transform));
            Transform.RosValidate();
            if (Meshes is null) throw new System.NullReferenceException(nameof(Meshes));
        }
    
        public int RosMessageLength => 76 + BuiltIns.GetStringSize(Name) + 4 * Meshes.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Node";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f9d8cac4a2655a1f6069aef339ab3144";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1fIS8xN5crMKzE2UihILErNK+HyTQTKVJgolBQl5hWn5RflQqSjYxVyU4sz" +
                "Uou5eLlsqQx4uXyD3a0UMssyq+Jzi9OL9aFu4OVKy8lPBFluaAa0XkFZoSi/XCE3MSu/SAHoDAA65h7+" +
                "wwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
