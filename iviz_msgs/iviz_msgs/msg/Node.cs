/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Node : IDeserializable<Node>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "parent")] public int Parent;
        [DataMember (Name = "transform")] public Matrix4 Transform;
        [DataMember (Name = "meshes")] public int[] Meshes;
    
        /// Constructor for empty message.
        public Node()
        {
            Name = "";
            Transform = new Matrix4();
            Meshes = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public Node(string Name, int Parent, Matrix4 Transform, int[] Meshes)
        {
            this.Name = Name;
            this.Parent = Parent;
            this.Transform = Transform;
            this.Meshes = Meshes;
        }
        
        /// Constructor with buffer.
        public Node(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            b.Deserialize(out Parent);
            Transform = new Matrix4(ref b);
            Meshes = b.DeserializeStructArray<int>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Node(ref b);
        
        public Node RosDeserialize(ref ReadBuffer b) => new Node(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Parent);
            Transform.RosSerialize(ref b);
            b.SerializeStructArray(Meshes);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Transform is null) BuiltIns.ThrowNullReference();
            Transform.RosValidate();
            if (Meshes is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 76 + BuiltIns.GetStringSize(Name) + 4 * Meshes.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Node";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f9d8cac4a2655a1f6069aef339ab3144";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5crMKzE2UihILErNK+HyTQTKVJgolBQl5hWn5RflQqSjYxVyU4sz" +
                "Uou5uGypDLh8g92tFDLLMqvic4vTi/WhLuBKy8lPBNlsaAa0W0FZoSi/XCE3MSu/SIGLCwAqJvLSvwAA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
